namespace TotalTagger
{
    partial class FindDuplicatesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindDuplicatesForm));
            this.lvDuplicateSongs = new System.Windows.Forms.ListView();
            this.chkTitleOnly = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvDuplicateSongs
            // 
            this.lvDuplicateSongs.CheckBoxes = true;
            this.lvDuplicateSongs.GridLines = true;
            this.lvDuplicateSongs.Location = new System.Drawing.Point(13, 26);
            this.lvDuplicateSongs.Name = "lvDuplicateSongs";
            this.lvDuplicateSongs.Size = new System.Drawing.Size(1108, 529);
            this.lvDuplicateSongs.TabIndex = 0;
            this.lvDuplicateSongs.UseCompatibleStateImageBehavior = false;
            this.lvDuplicateSongs.View = System.Windows.Forms.View.Details;
            // 
            // chkTitleOnly
            // 
            this.chkTitleOnly.AutoSize = true;
            this.chkTitleOnly.Location = new System.Drawing.Point(13, 583);
            this.chkTitleOnly.Name = "chkTitleOnly";
            this.chkTitleOnly.Size = new System.Drawing.Size(222, 17);
            this.chkTitleOnly.TabIndex = 1;
            this.chkTitleOnly.Text = "Find Duplicate Songs Based on Title Only";
            this.chkTitleOnly.UseVisualStyleBackColor = true;
            this.chkTitleOnly.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(513, 583);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(108, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete Songs";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1046, 586);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.Location = new System.Drawing.Point(13, 558);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(1108, 13);
            this.lblProgress.TabIndex = 4;
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "List of Duplicate Songs in the Collection";
            // 
            // FindDuplicatesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 617);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.chkTitleOnly);
            this.Controls.Add(this.lvDuplicateSongs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1149, 660);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1149, 660);
            this.Name = "FindDuplicatesForm";
            this.Text = "Duplicate Songs";
            this.Load += new System.EventHandler(this.FindDuplicatesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvDuplicateSongs;
        private System.Windows.Forms.CheckBox chkTitleOnly;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label1;
    }
}