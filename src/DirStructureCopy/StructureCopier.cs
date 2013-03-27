using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zip;
using System.ComponentModel;

namespace DirStructureCopy
{
    class StructureCopier
    {
        public enum ResultType { Success, Error, Canceled };
        private class CanceledException : Exception { }

        #region Private data members
        private const string logFile = "dirstructurecopy.log"; // TODO
        private const string zipExtension = ".zip";
        private const string comment = "Original size was {0} B"; // TODO

        private string sourcePath;
        private string destinationPath;
        private ResultType result;
        private ZipFile zipFile;
        private BackgroundWorker backgroundWorker;

        private bool flattenPaths = false;
        private bool browseZipArchives = false;
        private bool isStarted = false;
        #endregion

        #region Public data members
        /// <summary>
        /// Event fired when copying is stopped. 
        /// </summary>
        public event EventHandler<CopyStoppedEventArgs> Stopped;

        /// <summary>
        /// Event informing about the progress of the background copy operation.
        /// EventArgs contains name of the currently processed directory.
        /// </summary>
        public event EventHandler<CopyProgressChangedEventArgs> ProgressChanged;

        /// <summary>
        /// If set to true, the directory structure of the copyied directory is removed
        /// and all contained files (including subdirectories) will be directly in the output archive.
        /// In case of conflicting names, only the first file will be present in the output archive.
        /// </summary>
        public bool FlattenPaths
        {
            get
            {
                return flattenPaths;
            }
            set
            {
                if (flattenPaths == value)
                {
                    return;
                }
                if (isStarted)
                {
                    throw new InvalidOperationException();
                }
                flattenPaths = value;
            }
        }

        /// <summary>
        /// If set to true, the contents of copied zip archives will be added to the 
        /// output archive too.
        /// </summary>
        public bool BrowseZipArchives
        {
            get
            {
                return browseZipArchives;
            }
            set
            {
                if (browseZipArchives == value)
                {
                    return;
                }
                if (isStarted)
                {
                    throw new InvalidOperationException();
                }
                browseZipArchives = value;
            }
        }
        #endregion

        public StructureCopier()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(this.doWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorkerProgressChanged);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorkerCompleted);
        }

        /// <summary>
        /// Runs the copy operation asynchronously.
        /// </summary>
        /// <param name="source">Path to the directory whose structure shall be copied</param>
        /// <param name="destination">Path to the destination archive</param>
        public void RunAsync(string source, string destination)
        {
            if (isStarted)
            {
                throw new InvalidOperationException("RunAsync() cannot be called while copy is in progress."); // TODO
            }

            DirectoryInfo sourceDir = new DirectoryInfo(source);
            if (!sourceDir.Exists)
            {
                throw new IOException("Source directory doesn't exist"); // TODO
            }


            sourcePath = sourceDir.FullName;
            destinationPath = destination;

            //zipFile = openZipFile();
            zipFile = new ZipFile();
            zipFile.UseZip64WhenSaving = Zip64Option.Always;

            isStarted = true;
            backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Stops execution of the background copy process.
        /// </summary>
        public void CancelAsync()
        {
            if (!isStarted)
                return;
            backgroundWorker.CancelAsync();
        }

        private void backgroundWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            zipFile.Save(destinationPath);
            zipFile.Dispose();
            zipFile = null;

            isStarted = false;
            if (Stopped != null)
            {
                Stopped(this, new CopyStoppedEventArgs(result));
            }
        }

        private void backgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new CopyProgressChangedEventArgs((string)e.UserState));
            }
        }

        /// <summary>
        /// The copy task executed by BackgroundWorker.
        /// </summary>
        private void doWork(object sender, DoWorkEventArgs e)
        {
            result = ResultType.Error;
            try
            {
                DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
                copyDirectoryStructure(sourceDir);
                result = backgroundWorker.CancellationPending ? ResultType.Canceled : ResultType.Success;
            }
            catch (Exception)
            {
                result = ResultType.Error;
            }
        }

        /// <summary>
        /// The actual implementation of the directory structure copy.
        /// Implemented using recursion.
        /// </summary>
        /// <param name="sourceDir">Directory to be copied</param>
        private void copyDirectoryStructure(DirectoryInfo sourceDir)
        {
            if (backgroundWorker.CancellationPending)
            {
                return; // BackgroundWorker.CancelAsync() has been called
            }

            backgroundWorker.ReportProgress(0, stripSourcePath(sourceDir.FullName));

            foreach (FileInfo sourceFile in sourceDir.GetFiles())
            {
                try
                {
                    addFile(sourceFile);
                    if (browseZipArchives && Path.GetExtension(sourceFile.Name) == zipExtension)
                    {
                        FileStream sourceFileStream = sourceFile.OpenRead();
                        if (ZipFile.IsZipFile(sourceFileStream, true))
                        {
                            sourceFileStream.Position = 0;
                            using (ZipFile sourceZipFile = ZipFile.Read(sourceFileStream))
                            {
                                copyZipArchiveStructure(sourceZipFile, sourceFile);
                            }
                        }

                        sourceFileStream.Close();
                    }
                }
                catch (UnauthorizedAccessException) { }
                catch (Exception e)
                {
                    log("Exception when proccessing file {0} in {1}:\n{2}\n", sourceFile.Name, sourceDir.FullName, e.Message);
                }
            }

            foreach (DirectoryInfo sourceSubdir in sourceDir.GetDirectories())
            {
                if (backgroundWorker.CancellationPending)
                {
                    return; // BackgroundWorker.CancelAsync() has been called
                }

                try
                {
                    addDirectory(sourceSubdir);
                    copyDirectoryStructure(sourceSubdir); // recursion
                }
                catch (UnauthorizedAccessException) { }
                catch (Exception e)
                {
                    log("Exception when proccessing directory '{0}' in '{1}':\n{2}\n", sourceDir.Name, sourceDir.FullName, e.Message);
                }
            }
        }

        private void copyZipArchiveStructure(ZipFile sourceZipFile, FileInfo sourceFile)
        {
            if (backgroundWorker.CancellationPending)
            {
                return; // BackgroundWorker.CancelAsync() has been called
            }

            string archivePath = stripSourcePath(sourceFile.FullName);
            backgroundWorker.ReportProgress(0, archivePath);

            try
            {
                foreach (ZipEntry sourceEntry in sourceZipFile)
                {
                    addZipEntry(sourceEntry, archivePath);
                }
            }
            catch (Exception e)
            {
                log("Exception when proccessing zip file '{0}' in '{1}':\n{2}\n", sourceFile.Name, sourceFile.Directory.FullName, e.Message);
            }
        }

        private void addFile(FileInfo sourceFile)
        {
            string path = flattenPaths ? sourceFile.Name : stripSourcePath(sourceFile.FullName);
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

            entry.Comment = String.Format(comment, sourceFile.Length); // TODO documentation
            entry.IsText = true;
        }

        private void addDirectory(DirectoryInfo sourceDir)
        {
            if (!flattenPaths)
            {
                string path = stripSourcePath(sourceDir.FullName);

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
                // TODO: nìjak ošetøit duplicity?
                string path = archivePath + "$" + Path.DirectorySeparatorChar + sourceEntry.FileName; // TODO documentation
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
                newEntry.Comment = String.Format(comment, sourceEntry.UncompressedSize);
                newEntry.IsText = true;
            }
        }

        private string stripSourcePath(string path)
        {

            if (path.StartsWith(sourcePath))
            {
                return path.Substring(sourcePath.Length);
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
    }
}
