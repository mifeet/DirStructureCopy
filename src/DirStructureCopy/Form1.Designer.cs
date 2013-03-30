namespace DirStructureCopy
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.sourceBrowseBtn = new System.Windows.Forms.Button();
            this.sourceDirBox = new System.Windows.Forms.TextBox();
            this.sourceDirLabel = new System.Windows.Forms.Label();
            this.sourceFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.destinationDirLabel = new System.Windows.Forms.Label();
            this.destinationFileBox = new System.Windows.Forms.TextBox();
            this.destinationBrowseBtn = new System.Windows.Forms.Button();
            this.runBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.destinationFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.progressBox = new System.Windows.Forms.TextBox();
            this.flattenPathsCb = new System.Windows.Forms.CheckBox();
            this.browseArchivesCb = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // sourceBrowseBtn
            // 
            resources.ApplyResources(this.sourceBrowseBtn, "sourceBrowseBtn");
            this.sourceBrowseBtn.Name = "sourceBrowseBtn";
            this.sourceBrowseBtn.TabStop = false;
            this.sourceBrowseBtn.UseVisualStyleBackColor = true;
            this.sourceBrowseBtn.Click += new System.EventHandler(this.sourceBrowseBtn_Click);
            // 
            // sourceDirBox
            // 
            resources.ApplyResources(this.sourceDirBox, "sourceDirBox");
            this.sourceDirBox.Name = "sourceDirBox";
            // 
            // sourceDirLabel
            // 
            resources.ApplyResources(this.sourceDirLabel, "sourceDirLabel");
            this.sourceDirLabel.Name = "sourceDirLabel";
            // 
            // sourceFolderBrowserDialog
            // 
            resources.ApplyResources(this.sourceFolderBrowserDialog, "sourceFolderBrowserDialog");
            // 
            // destinationDirLabel
            // 
            resources.ApplyResources(this.destinationDirLabel, "destinationDirLabel");
            this.destinationDirLabel.Name = "destinationDirLabel";
            // 
            // destinationFileBox
            // 
            resources.ApplyResources(this.destinationFileBox, "destinationFileBox");
            this.destinationFileBox.Name = "destinationFileBox";
            // 
            // destinationBrowseBtn
            // 
            resources.ApplyResources(this.destinationBrowseBtn, "destinationBrowseBtn");
            this.destinationBrowseBtn.Name = "destinationBrowseBtn";
            this.destinationBrowseBtn.TabStop = false;
            this.destinationBrowseBtn.UseVisualStyleBackColor = true;
            this.destinationBrowseBtn.Click += new System.EventHandler(this.destinationBrowsBtn_Click);
            // 
            // runBtn
            // 
            resources.ApplyResources(this.runBtn, "runBtn");
            this.runBtn.Name = "runBtn";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // cancelBtn
            // 
            resources.ApplyResources(this.cancelBtn, "cancelBtn");
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // destinationFileDialog
            // 
            this.destinationFileDialog.DefaultExt = "zip";
            resources.ApplyResources(this.destinationFileDialog, "destinationFileDialog");
            // 
            // progressBox
            // 
            resources.ApplyResources(this.progressBox, "progressBox");
            this.progressBox.Name = "progressBox";
            // 
            // flattenPathsCb
            // 
            resources.ApplyResources(this.flattenPathsCb, "flattenPathsCb");
            this.flattenPathsCb.Name = "flattenPathsCb";
            this.flattenPathsCb.UseVisualStyleBackColor = true;
            // 
            // browseArchivesCb
            // 
            resources.ApplyResources(this.browseArchivesCb, "browseArchivesCb");
            this.browseArchivesCb.Name = "browseArchivesCb";
            this.browseArchivesCb.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.destinationDirLabel);
            this.Controls.Add(this.browseArchivesCb);
            this.Controls.Add(this.flattenPathsCb);
            this.Controls.Add(this.progressBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.destinationFileBox);
            this.Controls.Add(this.destinationBrowseBtn);
            this.Controls.Add(this.sourceDirLabel);
            this.Controls.Add(this.sourceDirBox);
            this.Controls.Add(this.sourceBrowseBtn);
            this.Name = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sourceBrowseBtn;
        private System.Windows.Forms.TextBox sourceDirBox;
        private System.Windows.Forms.Label sourceDirLabel;
        private System.Windows.Forms.FolderBrowserDialog sourceFolderBrowserDialog;
        private System.Windows.Forms.Label destinationDirLabel;
        private System.Windows.Forms.TextBox destinationFileBox;
        private System.Windows.Forms.Button destinationBrowseBtn;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.SaveFileDialog destinationFileDialog;
        private System.Windows.Forms.TextBox progressBox;
        private System.Windows.Forms.CheckBox flattenPathsCb;
        private System.Windows.Forms.CheckBox browseArchivesCb;

    }
}

