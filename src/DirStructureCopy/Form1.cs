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

namespace DirStructureCopy
{
    public partial class Form1 : Form
    {
        StructureCopier copier;

        public Form1()
        {
            InitializeComponent();
            copier = new StructureCopier();
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
                MessageBox.Show("Zdrojový adresář neexistuje.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error); // TODO
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
                    MessageBox.Show("Adresář byl zkopírován."); // TODO
                    break;
                case StructureCopier.ResultType.Error:
                    MessageBox.Show("Nastala chyba."); // TODO
                    break;
                case StructureCopier.ResultType.Canceled:
                    MessageBox.Show("Kopírování bylo zrušeno."); // TODO
                    break;
            }
        }

        private void copier_ProgressChanged(object sender, CopyProgressChangedEventArgs e)
        {
            progressBox.Text = e.CurrentDirectory;
        }
    }
}
