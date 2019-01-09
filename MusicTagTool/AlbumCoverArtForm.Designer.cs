namespace TotalTagger
{
    partial class AlbumCoverArtForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumCoverArtForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnWriteCoverArtToFile = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbSpotify = new System.Windows.Forms.RadioButton();
            this.rbMusicBrainz = new System.Windows.Forms.RadioButton();
            this.rbDiscogs = new System.Windows.Forms.RadioButton();
            this.rbDeezer = new System.Windows.Forms.RadioButton();
            this.rbITunes = new System.Windows.Forms.RadioButton();
            this.rbLastFM = new System.Windows.Forms.RadioButton();
            this.rbNapster = new System.Windows.Forms.RadioButton();
            this.btnSearchForAlbums = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1028, 528);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnWriteCoverArtToFile
            // 
            this.btnWriteCoverArtToFile.Location = new System.Drawing.Point(768, 572);
            this.btnWriteCoverArtToFile.Name = "btnWriteCoverArtToFile";
            this.btnWriteCoverArtToFile.Size = new System.Drawing.Size(75, 23);
            this.btnWriteCoverArtToFile.TabIndex = 1;
            this.btnWriteCoverArtToFile.Text = "Write To File";
            this.btnWriteCoverArtToFile.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(966, 576);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbSpotify);
            this.panel1.Controls.Add(this.rbMusicBrainz);
            this.panel1.Controls.Add(this.rbDiscogs);
            this.panel1.Controls.Add(this.rbDeezer);
            this.panel1.Controls.Add(this.rbITunes);
            this.panel1.Controls.Add(this.rbLastFM);
            this.panel1.Controls.Add(this.rbNapster);
            this.panel1.Location = new System.Drawing.Point(95, 572);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 26);
            this.panel1.TabIndex = 15;
            // 
            // rbSpotify
            // 
            this.rbSpotify.AutoSize = true;
            this.rbSpotify.Location = new System.Drawing.Point(349, 3);
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
            this.rbMusicBrainz.Location = new System.Drawing.Point(261, 4);
            this.rbMusicBrainz.Name = "rbMusicBrainz";
            this.rbMusicBrainz.Size = new System.Drawing.Size(82, 17);
            this.rbMusicBrainz.TabIndex = 12;
            this.rbMusicBrainz.TabStop = true;
            this.rbMusicBrainz.Text = "MusicBrainz";
            this.rbMusicBrainz.UseVisualStyleBackColor = true;
            // 
            // rbDiscogs
            // 
            this.rbDiscogs.AutoSize = true;
            this.rbDiscogs.Location = new System.Drawing.Point(412, 3);
            this.rbDiscogs.Name = "rbDiscogs";
            this.rbDiscogs.Size = new System.Drawing.Size(63, 17);
            this.rbDiscogs.TabIndex = 11;
            this.rbDiscogs.TabStop = true;
            this.rbDiscogs.Text = "Discogs";
            this.rbDiscogs.UseVisualStyleBackColor = true;
            this.rbDiscogs.Visible = false;
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
            // btnSearchForAlbums
            // 
            this.btnSearchForAlbums.Location = new System.Drawing.Point(14, 573);
            this.btnSearchForAlbums.Name = "btnSearchForAlbums";
            this.btnSearchForAlbums.Size = new System.Drawing.Size(75, 23);
            this.btnSearchForAlbums.TabIndex = 14;
            this.btnSearchForAlbums.Text = "Search";
            this.btnSearchForAlbums.UseVisualStyleBackColor = true;
            // 
            // AlbumCoverArtForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 618);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSearchForAlbums);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnWriteCoverArtToFile);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1069, 661);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1069, 661);
            this.Name = "AlbumCoverArtForm";
            this.Text = "AlbumCoverArtForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnWriteCoverArtToFile;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbSpotify;
        private System.Windows.Forms.RadioButton rbMusicBrainz;
        private System.Windows.Forms.RadioButton rbDiscogs;
        private System.Windows.Forms.RadioButton rbDeezer;
        private System.Windows.Forms.RadioButton rbITunes;
        private System.Windows.Forms.RadioButton rbLastFM;
        private System.Windows.Forms.RadioButton rbNapster;
        private System.Windows.Forms.Button btnSearchForAlbums;
    }
}