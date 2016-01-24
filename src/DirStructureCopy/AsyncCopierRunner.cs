using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zip;
using System.ComponentModel;
using System.Resources;

namespace DirStructureCopy
{
    class AsyncCopierRunner
    {
        public enum ResultType { Success, Error, Canceled };
        private class CanceledException : Exception { }

        #region Private data members
        private DirectoryInfo sourceDir;
        private string destinationPath;
        private ResultType result;
        private BackgroundWorker backgroundWorker;
        private ResourceManager resources;

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

        public AsyncCopierRunner(ResourceManager resources)
        {
            this.resources = resources;
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(this.doWork);
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
                throw new InvalidOperationException("RunAsync() cannot be called while copy is in progress.");
            }

            sourceDir = new DirectoryInfo(source);
            if (!sourceDir.Exists)
            {
                throw new IOException("Source directory doesn't exist");
            }
            destinationPath = destination;

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
            isStarted = false;
            if (Stopped != null)
            {
                Stopped(this, new CopyStoppedEventArgs(result));
            }
        }

        private void workerProgressChanged(object sender, CopyProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, e);
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
                using (StructureCopier copier = new StructureCopier(destinationPath, browseZipArchives, flattenPaths, resources))
                {
                    copier.ProgressChanged += new EventHandler<CopyProgressChangedEventArgs>(workerProgressChanged);
                    copier.CopyDirectoryStructure(sourceDir, () => backgroundWorker.CancellationPending);
                    result = backgroundWorker.CancellationPending ? ResultType.Canceled : ResultType.Success;
                }
            }
            catch (Exception)
            {
                result = ResultType.Error;
            }
        }
    }
}
