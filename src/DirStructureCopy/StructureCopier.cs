using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zip;
using System.Resources;

namespace DirStructureCopy
{
    /// <summary>
    /// The actual workhorse for copying a directory structure to a zip archive.
    /// This class is not thread safe
    /// </summary>
    class StructureCopier : IDisposable
    {
        private const string logFile = "dirstructurecopy.log";
        private const string zipExtension = ".zip";

        /// <summary>
        /// Event informing about the progress of the background copy operation.
        /// EventArgs contains name of the currently processed directory.
        /// </summary>
        public event EventHandler<CopyProgressChangedEventArgs> ProgressChanged;

        private bool disposed = false;

        private ResourceManager resources;
        private bool browseZipArchives;
        private bool flattenPaths;
        private ZipFile zipFile;
        private String destinationPath;

        public StructureCopier(String destinationPath, bool browseZipArchives, bool flattenPaths, ResourceManager resources)
        {
            this.resources = resources;
            this.browseZipArchives = browseZipArchives;
            this.flattenPaths = flattenPaths;
            this.destinationPath = destinationPath;
            
            this.zipFile = new ZipFile();
            this.zipFile.UseZip64WhenSaving = Zip64Option.Always;
        }

        /// <summary>
        /// Copy contents of the sourceDir to 
        /// </summary>
        /// <param name="sourceDir"></param>
        public void CopyDirectoryStructure(DirectoryInfo sourceDir, Func<bool> stopCondition)
        {
            if (disposed) 
            {
                throw new InvalidOperationException(this.GetType().Name + " has been already disposed");
            }
            if (!sourceDir.Exists)
            {
                throw new IOException("Source directory doesn't exist");
            }
            copyDirectoryStructure(sourceDir, sourceDir, stopCondition);
        }

        /// <summary>
        /// The actual implementation of the directory structure copy.
        /// Implemented using recursion.
        /// </summary>
        /// <param name="sourceDir">Directory to be copied</param>
        private void copyDirectoryStructure(DirectoryInfo sourceDir, DirectoryInfo rootSourceDir, Func<bool> stopCondition)
        {
            if (stopCondition())
            {
                return; 
            }

            progressChanged(stripSourcePath(sourceDir.FullName, rootSourceDir));

            foreach (FileInfo sourceFile in sourceDir.GetFiles())
            {
                try
                {
                    addFile(sourceFile, rootSourceDir);
                    if (browseZipArchives && Path.GetExtension(sourceFile.Name) == zipExtension)
                    {
                        FileStream sourceFileStream = sourceFile.OpenRead();
                        if (ZipFile.IsZipFile(sourceFileStream, true))
                        {
                            sourceFileStream.Position = 0;
                            using (ZipFile sourceZipFile = ZipFile.Read(sourceFileStream))
                            {
                                copyZipArchiveStructure(sourceZipFile, sourceFile, rootSourceDir, stopCondition);
                            }
                        }

                        sourceFileStream.Close();
                    }
                }
                catch (UnauthorizedAccessException) { }
                catch (Exception e)
                {
                    log(resources.GetString("fileProcessingException") + ":\n{2}\n", sourceFile.Name, sourceDir.FullName, e.Message);
                }
            }

            foreach (DirectoryInfo sourceSubdir in sourceDir.GetDirectories())
            {
                if (stopCondition())
                {
                    return; 
                }

                try
                {
                    addDirectory(sourceSubdir, rootSourceDir);
                    copyDirectoryStructure(sourceSubdir, rootSourceDir, stopCondition); // recursion
                }
                catch (UnauthorizedAccessException) { }
                catch (Exception e)
                {
                    log(resources.GetString("dirProcessingException") + ":\n{2}\n", sourceDir.Name, sourceDir.FullName, e.Message);
                }
            }
        }

        private void copyZipArchiveStructure(ZipFile sourceZipFile, FileInfo sourceFile, DirectoryInfo rootSourceDir, Func<bool> stopCondition)
        {
            string archivePath = stripSourcePath(sourceFile.FullName, rootSourceDir);
            progressChanged(archivePath);

            try
            {
                foreach (ZipEntry sourceEntry in sourceZipFile)
                {
                    if (stopCondition())
                    {
                        return; 
                    }
                    addZipEntry(sourceEntry, archivePath);
                }
            }
            catch (Exception e)
            {
                log(resources.GetString("zipProcessingException") + ":\n{2}\n", sourceFile.Name, sourceFile.Directory.FullName, e.Message);
            }
        }

        private void addFile(FileInfo sourceFile, DirectoryInfo rootSourceDir)
        {
            string path = flattenPaths ? sourceFile.Name : stripSourcePath(sourceFile.FullName, rootSourceDir);
            if (flattenPaths && zipFile.ContainsEntry(path))
            {
                return;
            }

            ZipEntry entry = zipFile.AddEntry(path, String.Empty);
            entry.AccessedTime = sourceFile.LastAccessTime;
            entry.Attributes = sourceFile.Attributes;
            entry.CreationTime = sourceFile.CreationTime;
            entry.LastModified = sourceFile.LastWriteTime;
            entry.ModifiedTime = sourceFile.LastWriteTime;

            entry.Comment = String.Format(resources.GetString("fileComment"), sourceFile.Length);
            entry.IsText = true;
        }

        private void addDirectory(DirectoryInfo sourceDir, DirectoryInfo rootSourceDir)
        {
            if (!flattenPaths)
            {
                string path = stripSourcePath(sourceDir.FullName, rootSourceDir);

                ZipEntry entry = zipFile.AddDirectoryByName(path);
                entry.AccessedTime = sourceDir.LastAccessTime;
                entry.Attributes = sourceDir.Attributes;
                entry.CreationTime = sourceDir.CreationTime;
                entry.LastModified = sourceDir.LastWriteTime;
                entry.ModifiedTime = sourceDir.LastWriteTime;
            }
        }

        private void addZipEntry(ZipEntry sourceEntry, string archivePath)
        {
            if (sourceEntry.IsDirectory)
            {
                if (flattenPaths)
                {
                    return;
                }
                // TODO: treat duplicities?
                string path = archivePath + "$" + Path.DirectorySeparatorChar + sourceEntry.FileName;
                ZipEntry newEntry = zipFile.AddDirectoryByName(path);
                newEntry.Attributes = sourceEntry.Attributes;
                newEntry.AccessedTime = safeUtcTime(sourceEntry.AccessedTime);
                newEntry.CreationTime = safeUtcTime(sourceEntry.CreationTime);
                newEntry.LastModified = safeUtcTime(sourceEntry.LastModified);
                newEntry.ModifiedTime = safeUtcTime(sourceEntry.ModifiedTime);
            }
            else
            {
                string path = flattenPaths
                    ? Path.GetFileName(sourceEntry.FileName)
                    : archivePath + "$" + Path.DirectorySeparatorChar + sourceEntry.FileName;
                if (flattenPaths && zipFile.ContainsEntry(path))
                {
                    return;
                }

                ZipEntry newEntry = zipFile.AddEntry(path, String.Empty);
                newEntry.Attributes = sourceEntry.Attributes;
                newEntry.AccessedTime = safeUtcTime(sourceEntry.AccessedTime);
                newEntry.CreationTime = safeUtcTime(sourceEntry.CreationTime);
                newEntry.LastModified = safeUtcTime(sourceEntry.LastModified);
                newEntry.ModifiedTime = safeUtcTime(sourceEntry.ModifiedTime);
                newEntry.Comment = String.Format(resources.GetString("fileComment"), sourceEntry.UncompressedSize);
                newEntry.IsText = true;
            }
        }

        private string stripSourcePath(string path, DirectoryInfo rootSourceDirectory)
        {

            if (path.StartsWith(rootSourceDirectory.FullName))
            {
                return path.Substring(rootSourceDirectory.FullName.Length);
            }
            else
            {
                return path;
            }
        }

        private void log(string format, params object[] args)
        {
            string logMessage = String.Format(format, args);
            using (StreamWriter writer = new StreamWriter(logFile, true))
            {
                writer.Write(DateTime.Now);
                writer.WriteLine(": " + logMessage);
            }
        }

        private DateTime safeUtcTime(DateTime time)
        {
            try
            {
                long utcTime = time.ToFileTimeUtc();
                return DateTime.FromFileTimeUtc(utcTime);
            }
            catch (ArgumentOutOfRangeException)
            {
                return DateTime.FromFileTimeUtc(0);
            }
        }

        private void progressChanged(string archivePath)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new CopyProgressChangedEventArgs(archivePath));
            }
        }

        public void Dispose()
        {
            disposed = true;
            zipFile.Save(destinationPath);
            zipFile.Dispose();
        }
    }
}
