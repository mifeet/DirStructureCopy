using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Resources;

namespace DirStructureCopy
{
    public partial class MainForm : Form
    {
        StructureCopier copier;
        ResourceManager resources;

        public MainForm()
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("cs-CZ");

            InitializeComponent();
            resources = new ResourceManager("DirStructureCopy.UIStrings", typeof(MainForm).Assembly);
            copier = new StructureCopier(resources);
            copier.Stopped += new EventHandler<CopyStoppedEventArgs>(copier_Stopped);
            copier.ProgressChanged += new EventHandler<CopyProgressChangedEventArgs>(copier_ProgressChanged);
        }

        private void sourceBrowseBtn_Click(object sender, EventArgs e)
        {
            sourceFolderBrowserDialog.SelectedPath = sourceDirBox.Text;
            if (sourceFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                sourceDirBox.Text = sourceFolderBrowserDialog.SelectedPath;
            }
        }

        private void destinationBrowsBtn_Click(object sender, EventArgs e)
        {
            destinationFileDialog.FileName = destinationFileBox.Text;
            if (destinationFileDialog.ShowDialog() == DialogResult.OK)
            {
                destinationFileBox.Text = destinationFileDialog.FileName;
            }
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            string source = sourceDirBox.Text;
            if (!Directory.Exists(source))
            {
                MessageBox.Show(resources.GetString("sourceDirectoryDoesntExist"), resources.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string destination = destinationFileBox.Text;
            runBtn.Enabled = false;
            copier.FlattenPaths = flattenPathsCb.Checked;
            copier.BrowseZipArchives = browseArchivesCb.Checked;
            copier.RunAsync(source, destination);
            cancelBtn.Enabled = true;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            copier.CancelAsync();
        }

        private void copier_Stopped(object sender, CopyStoppedEventArgs e)
        {
            progressBox.Text = "";
            cancelBtn.Enabled = false;
            runBtn.Enabled = true;
            switch (e.Result)
            {
                case StructureCopier.ResultType.Success:
                    MessageBox.Show(resources.GetString("successResult")); 
                    break;
                case StructureCopier.ResultType.Error:
                    MessageBox.Show(resources.GetString("errorResult")); 
                    break;
                case StructureCopier.ResultType.Canceled:
                    MessageBox.Show(resources.GetString("canceledResult")); 
                    break;
            }
        }

        private void copier_ProgressChanged(object sender, CopyProgressChangedEventArgs e)
        {
            progressBox.Text = e.CurrentDirectory;
        }
    }
}
