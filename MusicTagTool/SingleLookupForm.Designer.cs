namespace TotalTagger
{
    partial class SingleLookupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleLookupForm));
            this.dgvBestMatchResults = new System.Windows.Forms.DataGridView();
            this.btnWriteTags = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbGenius = new System.Windows.Forms.RadioButton();
            this.rbGaliboo = new System.Windows.Forms.RadioButton();
            this.rbMusixMatch = new System.Windows.Forms.RadioButton();
            this.rbSpotify = new System.Windows.Forms.RadioButton();
            this.rbMusicBrainz = new System.Windows.Forms.RadioButton();
            this.rbNapster = new System.Windows.Forms.RadioButton();
            this.rbDiscogs = new System.Windows.Forms.RadioButton();
            this.rbDeezer = new System.Windows.Forms.RadioButton();
            this.rbITunes = new System.Windows.Forms.RadioButton();
            this.rbLastFM = new System.Windows.Forms.RadioButton();
            this.btnSearchForAlbums = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBestMatchResults)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvBestMatchResults
            // 
            this.dgvBestMatchResults.AllowUserToAddRows = false;
            this.dgvBestMatchResults.AllowUserToDeleteRows = false;
            this.dgvBestMatchResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBestMatchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBestMatchResults.Location = new System.Drawing.Point(13, 26);
            this.dgvBestMatchResults.Name = "dgvBestMatchResults";
            this.dgvBestMatchResults.Size = new System.Drawing.Size(1452, 525);
            this.dgvBestMatchResults.TabIndex = 0;
            // 
            // btnWriteTags
            // 
            this.btnWriteTags.Location = new System.Drawing.Point(1095, 587);
            this.btnWriteTags.Name = "btnWriteTags";
            this.btnWriteTags.Size = new System.Drawing.Size(119, 23);
            this.btnWriteTags.TabIndex = 1;
            this.btnWriteTags.Text = "Write to File(s)";
            this.btnWriteTags.UseVisualStyleBackColor = true;
            this.btnWriteTags.Click += new System.EventHandler(this.btnWriteTags_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1363, 587);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(102, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbGenius);
            this.panel1.Controls.Add(this.rbGaliboo);
            this.panel1.Controls.Add(this.rbMusixMatch);
            this.panel1.Controls.Add(this.rbSpotify);
            this.panel1.Controls.Add(this.rbMusicBrainz);
            this.panel1.Controls.Add(this.rbNapster);
            this.panel1.Controls.Add(this.rbDiscogs);
            this.panel1.Controls.Add(this.rbDeezer);
            this.panel1.Controls.Add(this.rbITunes);
            this.panel1.Controls.Add(this.rbLastFM);
            this.panel1.Location = new System.Drawing.Point(93, 586);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 26);
            this.panel1.TabIndex = 13;
            // 
            // rbGenius
            // 
            this.rbGenius.AutoSize = true;
            this.rbGenius.Location = new System.Drawing.Point(131, 4);
            this.rbGenius.Name = "rbGenius";
            this.rbGenius.Size = new System.Drawing.Size(58, 17);
            this.rbGenius.TabIndex = 17;
            this.rbGenius.TabStop = true;
            this.rbGenius.Text = "Genius";
            this.rbGenius.UseVisualStyleBackColor = true;
            // 
            // rbGaliboo
            // 
            this.rbGaliboo.AutoSize = true;
            this.rbGaliboo.Location = new System.Drawing.Point(65, 4);
            this.rbGaliboo.Name = "rbGaliboo";
            this.rbGaliboo.Size = new System.Drawing.Size(61, 17);
            this.rbGaliboo.TabIndex = 16;
            this.rbGaliboo.TabStop = true;
            this.rbGaliboo.Text = "Galiboo";
            this.rbGaliboo.UseVisualStyleBackColor = true;
            // 
            // rbMusixMatch
            // 
            this.rbMusixMatch.AutoSize = true;
            this.rbMusixMatch.Location = new System.Drawing.Point(408, 4);
            this.rbMusixMatch.Name = "rbMusixMatch";
            this.rbMusixMatch.Size = new System.Drawing.Size(82, 17);
            this.rbMusixMatch.TabIndex = 14;
            this.rbMusixMatch.TabStop = true;
            this.rbMusixMatch.Text = "MusixMatch";
            this.rbMusixMatch.UseVisualStyleBackColor = true;
            // 
            // rbSpotify
            // 
            this.rbSpotify.AutoSize = true;
            this.rbSpotify.Location = new System.Drawing.Point(562, 4);
            this.rbSpotify.Name = "rbSpotify";
            this.rbSpotify.Size = new System.Drawing.Size(57, 17);
            this.rbSpotify.TabIndex = 13;
            this.rbSpotify.TabStop = true;
            this.rbSpotify.Text = "Spotify";
            this.rbSpotify.UseVisualStyleBackColor = true;
            // 
            // rbMusicBrainz
            // 
            this.rbMusicBrainz.AutoSize = true;
            this.rbMusicBrainz.Location = new System.Drawing.Point(321, 4);
            this.rbMusicBrainz.Name = "rbMusicBrainz";
            this.rbMusicBrainz.Size = new System.Drawing.Size(82, 17);
            this.rbMusicBrainz.TabIndex = 12;
            this.rbMusicBrainz.TabStop = true;
            this.rbMusicBrainz.Text = "MusicBrainz";
            this.rbMusicBrainz.UseVisualStyleBackColor = true;
            // 
            // rbNapster
            // 
            this.rbNapster.AutoSize = true;
            this.rbNapster.Location = new System.Drawing.Point(495, 4);
            this.rbNapster.Name = "rbNapster";
            this.rbNapster.Size = new System.Drawing.Size(62, 17);
            this.rbNapster.TabIndex = 8;
            this.rbNapster.TabStop = true;
            this.rbNapster.Text = "Napster";
            this.rbNapster.UseVisualStyleBackColor = true;
            // 
            // rbDiscogs
            // 
            this.rbDiscogs.AutoSize = true;
            this.rbDiscogs.Location = new System.Drawing.Point(625, 4);
            this.rbDiscogs.Name = "rbDiscogs";
            this.rbDiscogs.Size = new System.Drawing.Size(63, 17);
            this.rbDiscogs.TabIndex = 11;
            this.rbDiscogs.TabStop = true;
            this.rbDiscogs.Text = "Discogs";
            this.rbDiscogs.UseVisualStyleBackColor = true;
            this.rbDiscogs.Visible = false;
            this.rbDiscogs.CheckedChanged += new System.EventHandler(this.rbDiscogs_CheckedChanged);
            // 
            // rbDeezer
            // 
            this.rbDeezer.AutoSize = true;
            this.rbDeezer.Location = new System.Drawing.Point(3, 4);
            this.rbDeezer.Name = "rbDeezer";
            this.rbDeezer.Size = new System.Drawing.Size(59, 17);
            this.rbDeezer.TabIndex = 10;
            this.rbDeezer.TabStop = true;
            this.rbDeezer.Text = "Deezer";
            this.rbDeezer.UseVisualStyleBackColor = true;
            // 
            // rbITunes
            // 
            this.rbITunes.AutoSize = true;
            this.rbITunes.Location = new System.Drawing.Point(194, 4);
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
            this.rbLastFM.Location = new System.Drawing.Point(256, 4);
            this.rbLastFM.Name = "rbLastFM";
            this.rbLastFM.Size = new System.Drawing.Size(60, 17);
            this.rbLastFM.TabIndex = 7;
            this.rbLastFM.TabStop = true;
            this.rbLastFM.Text = "LastFM";
            this.rbLastFM.UseVisualStyleBackColor = true;
            // 
            // btnSearchForAlbums
            // 
            this.btnSearchForAlbums.Location = new System.Drawing.Point(12, 587);
            this.btnSearchForAlbums.Name = "btnSearchForAlbums";
            this.btnSearchForAlbums.Size = new System.Drawing.Size(75, 23);
            this.btnSearchForAlbums.TabIndex = 12;
            this.btnSearchForAlbums.Text = "Search";
            this.btnSearchForAlbums.UseVisualStyleBackColor = true;
            this.btnSearchForAlbums.Click += new System.EventHandler(this.btnSearchForAlbums_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Best Match Results";
            // 
            // lblProgress
            // 
            this.lblProgress.Location = new System.Drawing.Point(12, 554);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(1453, 19);
            this.lblProgress.TabIndex = 15;
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SingleLookupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1477, 630);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSearchForAlbums);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnWriteTags);
            this.Controls.Add(this.dgvBestMatchResults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1497, 673);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1497, 673);
            this.Name = "SingleLookupForm";
            this.Text = "Best Match for File(s)";
            this.Load += new System.EventHandler(this.BestMatchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBestMatchResults)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBestMatchResults;
        private System.Windows.Forms.Button btnWriteTags;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbSpotify;
        private System.Windows.Forms.RadioButton rbMusicBrainz;
        private System.Windows.Forms.RadioButton rbDiscogs;
        private System.Windows.Forms.RadioButton rbDeezer;
        private System.Windows.Forms.RadioButton rbITunes;
        private System.Windows.Forms.RadioButton rbLastFM;
        private System.Windows.Forms.RadioButton rbNapster;
        private System.Windows.Forms.Button btnSearchForAlbums;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.RadioButton rbMusixMatch;
        private System.Windows.Forms.RadioButton rbGenius;
        private System.Windows.Forms.RadioButton rbGaliboo;
    }
}