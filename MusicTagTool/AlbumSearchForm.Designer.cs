namespace TotalTagger
{
    partial class AlbumSearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumSearchForm));
            this.lvSongsWithMissingAlbums = new System.Windows.Forms.ListView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSearchForAlbums = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveTags = new System.Windows.Forms.Button();
            this.rbITunes = new System.Windows.Forms.RadioButton();
            this.rbLastFM = new System.Windows.Forms.RadioButton();
            this.rbNapster = new System.Windows.Forms.RadioButton();
            this.lblProgress = new System.Windows.Forms.Label();
            this.rbDeezer = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvSongsWithMissingAlbums
            // 
            this.lvSongsWithMissingAlbums.CheckBoxes = true;
            this.lvSongsWithMissingAlbums.GridLines = true;
            this.lvSongsWithMissingAlbums.Location = new System.Drawing.Point(13, 33);
            this.lvSongsWithMissingAlbums.Name = "lvSongsWithMissingAlbums";
            this.lvSongsWithMissingAlbums.Size = new System.Drawing.Size(915, 484);
            this.lvSongsWithMissingAlbums.TabIndex = 0;
            this.lvSongsWithMissingAlbums.UseCompatibleStateImageBehavior = false;
            this.lvSongsWithMissingAlbums.View = System.Windows.Forms.View.Details;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancel.Location = new System.Drawing.Point(853, 563);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSearchForAlbums
            // 
            this.btnSearchForAlbums.Location = new System.Drawing.Point(13, 560);
            this.btnSearchForAlbums.Name = "btnSearchForAlbums";
            this.btnSearchForAlbums.Size = new System.Drawing.Size(75, 23);
            this.btnSearchForAlbums.TabIndex = 3;
            this.btnSearchForAlbums.Text = "Search";
            this.btnSearchForAlbums.UseVisualStyleBackColor = true;
            this.btnSearchForAlbums.Click += new System.EventHandler(this.btnSearchForAlbums_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Songs and Albums";
            // 
            // btnSaveTags
            // 
            this.btnSaveTags.Location = new System.Drawing.Point(693, 566);
            this.btnSaveTags.Name = "btnSaveTags";
            this.btnSaveTags.Size = new System.Drawing.Size(104, 23);
            this.btnSaveTags.TabIndex = 5;
            this.btnSaveTags.Text = "Write To File";
            this.btnSaveTags.UseVisualStyleBackColor = true;
            this.btnSaveTags.Click += new System.EventHandler(this.btnSaveTags_Click);
            // 
            // rbITunes
            // 
            this.rbITunes.AutoSize = true;
            this.rbITunes.Location = new System.Drawing.Point(4, 3);
            this.rbITunes.Name = "rbITunes";
            this.rbITunes.Size = new System.Drawing.Size(57, 17);
            this.rbITunes.TabIndex = 6;
            this.rbITunes.TabStop = true;
            this.rbITunes.Text = "iTunes";
            this.rbITunes.UseVisualStyleBackColor = true;
            // 
            // rbLastFM
            // 
            this.rbLastFM.AutoSize = true;
            this.rbLastFM.Location = new System.Drawing.Point(67, 3);
            this.rbLastFM.Name = "rbLastFM";
            this.rbLastFM.Size = new System.Drawing.Size(60, 17);
            this.rbLastFM.TabIndex = 7;
            this.rbLastFM.TabStop = true;
            this.rbLastFM.Text = "LastFM";
            this.rbLastFM.UseVisualStyleBackColor = true;
            // 
            // rbNapster
            // 
            this.rbNapster.AutoSize = true;
            this.rbNapster.Location = new System.Drawing.Point(133, 3);
            this.rbNapster.Name = "rbNapster";
            this.rbNapster.Size = new System.Drawing.Size(62, 17);
            this.rbNapster.TabIndex = 8;
            this.rbNapster.TabStop = true;
            this.rbNapster.Text = "Napster";
            this.rbNapster.UseVisualStyleBackColor = true;
            // 
            // lblProgress
            // 
            this.lblProgress.Location = new System.Drawing.Point(13, 520);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(915, 18);
            this.lblProgress.TabIndex = 9;
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbDeezer
            // 
            this.rbDeezer.AutoSize = true;
            this.rbDeezer.Location = new System.Drawing.Point(201, 3);
            this.rbDeezer.Name = "rbDeezer";
            this.rbDeezer.Size = new System.Drawing.Size(59, 17);
            this.rbDeezer.TabIndex = 10;
            this.rbDeezer.TabStop = true;
            this.rbDeezer.Text = "Deezer";
            this.rbDeezer.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbDeezer);
            this.panel1.Controls.Add(this.rbITunes);
            this.panel1.Controls.Add(this.rbLastFM);
            this.panel1.Controls.Add(this.rbNapster);
            this.panel1.Location = new System.Drawing.Point(94, 559);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(277, 26);
            this.panel1.TabIndex = 11;
            // 
            // AlbumSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 601);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnSaveTags);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearchForAlbums);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lvSongsWithMissingAlbums);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(960, 644);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(960, 644);
            this.Name = "AlbumSearchForm";
            this.Text = "Songs Without Missing Albums";
            this.Load += new System.EventHandler(this.AlbumSearchForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvSongsWithMissingAlbums;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSearchForAlbums;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveTags;
        private System.Windows.Forms.RadioButton rbITunes;
        private System.Windows.Forms.RadioButton rbLastFM;
        private System.Windows.Forms.RadioButton rbNapster;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.RadioButton rbDeezer;
        private System.Windows.Forms.Panel panel1;
    }
}