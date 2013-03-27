namespace DirStructureCopy
{
    partial class Form1
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
            this.sourceBrowseBtn = new System.Windows.Forms.Button();
            this.sourceDirBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sourceFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.destinationFileBox = new System.Windows.Forms.TextBox();
            this.destinationBrowsBtn = new System.Windows.Forms.Button();
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
            this.sourceBrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceBrowseBtn.Location = new System.Drawing.Point(288, 10);
            this.sourceBrowseBtn.Name = "sourceBrowseBtn";
            this.sourceBrowseBtn.Size = new System.Drawing.Size(75, 23);
            this.sourceBrowseBtn.TabIndex = 0;
            this.sourceBrowseBtn.TabStop = false;
            this.sourceBrowseBtn.Text = "Procházet...";
            this.sourceBrowseBtn.UseVisualStyleBackColor = true;
            this.sourceBrowseBtn.Click += new System.EventHandler(this.sourceBrowseBtn_Click);
            // 
            // sourceDirBox
            // 
            this.sourceDirBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceDirBox.Location = new System.Drawing.Point(105, 12);
            this.sourceDirBox.Name = "sourceDirBox";
            this.sourceDirBox.Size = new System.Drawing.Size(177, 20);
            this.sourceDirBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Zdrojový adresář:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cílový archiv:";
            // 
            // destinationFileBox
            // 
            this.destinationFileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationFileBox.Location = new System.Drawing.Point(105, 47);
            this.destinationFileBox.Name = "destinationFileBox";
            this.destinationFileBox.Size = new System.Drawing.Size(177, 20);
            this.destinationFileBox.TabIndex = 4;
            this.destinationFileBox.Text = "C:\\users\\mifeet\\Desktop\\test.zip";
            // 
            // destinationBrowsBtn
            // 
            this.destinationBrowsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationBrowsBtn.Location = new System.Drawing.Point(288, 45);
            this.destinationBrowsBtn.Name = "destinationBrowsBtn";
            this.destinationBrowsBtn.Size = new System.Drawing.Size(75, 23);
            this.destinationBrowsBtn.TabIndex = 3;
            this.destinationBrowsBtn.TabStop = false;
            this.destinationBrowsBtn.Text = "Procházet...";
            this.destinationBrowsBtn.UseVisualStyleBackColor = true;
            this.destinationBrowsBtn.Click += new System.EventHandler(this.destinationBrowsBtn_Click);
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(41, 133);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(132, 44);
            this.runBtn.TabIndex = 6;
            this.runBtn.Text = "Zkopírovat strukturu";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Enabled = false;
            this.cancelBtn.Location = new System.Drawing.Point(218, 133);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(122, 44);
            this.cancelBtn.TabIndex = 7;
            this.cancelBtn.Text = "Zrušit";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // destinationFileDialog
            // 
            this.destinationFileDialog.DefaultExt = "zip";
            this.destinationFileDialog.Filter = "Zip archivy (*.zip)|*.zip|Všechny soubory (*.*)|*.*";
            // 
            // progressBox
            // 
            this.progressBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBox.Enabled = false;
            this.progressBox.Location = new System.Drawing.Point(15, 193);
            this.progressBox.Name = "progressBox";
            this.progressBox.Size = new System.Drawing.Size(348, 20);
            this.progressBox.TabIndex = 8;
            // 
            // flattenPathsCb
            // 
            this.flattenPathsCb.AutoSize = true;
            this.flattenPathsCb.Location = new System.Drawing.Point(15, 80);
            this.flattenPathsCb.Name = "flattenPathsCb";
            this.flattenPathsCb.Size = new System.Drawing.Size(175, 17);
            this.flattenPathsCb.TabIndex = 9;
            this.flattenPathsCb.Text = "Odstranit adresářovou strukturu";
            this.flattenPathsCb.UseVisualStyleBackColor = true;
            // 
            // browseArchivesCb
            // 
            this.browseArchivesCb.AutoSize = true;
            this.browseArchivesCb.Location = new System.Drawing.Point(15, 107);
            this.browseArchivesCb.Name = "browseArchivesCb";
            this.browseArchivesCb.Size = new System.Drawing.Size(130, 17);
            this.browseArchivesCb.TabIndex = 10;
            this.browseArchivesCb.Text = "Procházet .zip archivy";
            this.browseArchivesCb.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 226);
            this.Controls.Add(this.browseArchivesCb);
            this.Controls.Add(this.flattenPathsCb);
            this.Controls.Add(this.progressBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.destinationFileBox);
            this.Controls.Add(this.destinationBrowsBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sourceDirBox);
            this.Controls.Add(this.sourceBrowseBtn);
            this.Name = "Form1";
            this.Text = "Kopírování struktury adresářů";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sourceBrowseBtn;
        private System.Windows.Forms.TextBox sourceDirBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog sourceFolderBrowserDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox destinationFileBox;
        private System.Windows.Forms.Button destinationBrowsBtn;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.SaveFileDialog destinationFileDialog;
        private System.Windows.Forms.TextBox progressBox;
        private System.Windows.Forms.CheckBox flattenPathsCb;
        private System.Windows.Forms.CheckBox browseArchivesCb;

    }
}

