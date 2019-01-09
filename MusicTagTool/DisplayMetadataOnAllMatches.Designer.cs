namespace TotalTagger
{
    partial class DisplayMetadataOnAllMatches
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayMetadataOnAllMatches));
            this.dataGridAllResults = new System.Windows.Forms.DataGridView();
            this.lblProgress = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUseSelectedData = new System.Windows.Forms.Button();
            this.txtRetrievedTitle = new System.Windows.Forms.TextBox();
            this.txtRetrievedArtist = new System.Windows.Forms.TextBox();
            this.txtRetrievedAlbum = new System.Windows.Forms.TextBox();
            this.txtRetrievedYear = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRetrievedGenre = new System.Windows.Forms.TextBox();
            this.picRetrievedArt = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.chkGetAlbum = new System.Windows.Forms.CheckBox();
            this.chkGetArtist = new System.Windows.Forms.CheckBox();
            this.chkGetTitle = new System.Windows.Forms.CheckBox();
            this.txtExistingTitle = new System.Windows.Forms.TextBox();
            this.txtExistingArtist = new System.Windows.Forms.TextBox();
            this.txtExistingAlbum = new System.Windows.Forms.TextBox();
            this.txtExistingReleaseDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExistingGenre = new System.Windows.Forms.TextBox();
            this.picExistingArt = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbSpotify = new System.Windows.Forms.RadioButton();
            this.rbNapster = new System.Windows.Forms.RadioButton();
            this.rbMusixMatch = new System.Windows.Forms.RadioButton();
            this.rbMusicBrainz = new System.Windows.Forms.RadioButton();
            this.rbLastFM = new System.Windows.Forms.RadioButton();
            this.rbITunes = new System.Windows.Forms.RadioButton();
            this.rbGenius = new System.Windows.Forms.RadioButton();
            this.rbGaliboo = new System.Windows.Forms.RadioButton();
            this.rbDiscogs = new System.Windows.Forms.RadioButton();
            this.rbDeezer = new System.Windows.Forms.RadioButton();
            this.btnSearchForAlbum = new System.Windows.Forms.Button();
            this.btnQueryForMetadata = new System.Windows.Forms.Button();
            this.btnSearchForArtwork = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAllResults)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRetrievedArt)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExistingArt)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridAllResults
            // 
            this.dataGridAllResults.AllowUserToAddRows = false;
            this.dataGridAllResults.AllowUserToDeleteRows = false;
            this.dataGridAllResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAllResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridAllResults.Location = new System.Drawing.Point(580, 25);
            this.dataGridAllResults.MultiSelect = false;
            this.dataGridAllResults.Name = "dataGridAllResults";
            this.dataGridAllResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridAllResults.Size = new System.Drawing.Size(663, 474);
            this.dataGridAllResults.TabIndex = 3;
            this.dataGridAllResults.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridAllResults_CellDoubleClick);
            // 
            // lblProgress
            // 
            this.lblProgress.Location = new System.Drawing.Point(577, 500);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(662, 18);
            this.lblProgress.TabIndex = 0;
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnUseSelectedData);
            this.groupBox2.Controls.Add(this.txtRetrievedTitle);
            this.groupBox2.Controls.Add(this.txtRetrievedArtist);
            this.groupBox2.Controls.Add(this.txtRetrievedAlbum);
            this.groupBox2.Controls.Add(this.txtRetrievedYear);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtRetrievedGenre);
            this.groupBox2.Controls.Add(this.picRetrievedArt);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(14, 329);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 170);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Metadata From Online Service";
            // 
            // btnUseSelectedData
            // 
            this.btnUseSelectedData.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnUseSelectedData.Location = new System.Drawing.Point(468, 88);
            this.btnUseSelectedData.Name = "btnUseSelectedData";
            this.btnUseSelectedData.Size = new System.Drawing.Size(65, 56);
            this.btnUseSelectedData.TabIndex = 55;
            this.btnUseSelectedData.Text = "Use This Metadata";
            this.btnUseSelectedData.UseVisualStyleBackColor = true;
            this.btnUseSelectedData.Click += new System.EventHandler(this.BtnUseSelectedData_Click);
            // 
            // txtRetrievedTitle
            // 
            this.txtRetrievedTitle.BackColor = System.Drawing.Color.White;
            this.txtRetrievedTitle.Location = new System.Drawing.Point(13, 38);
            this.txtRetrievedTitle.Name = "txtRetrievedTitle";
            this.txtRetrievedTitle.Size = new System.Drawing.Size(302, 20);
            this.txtRetrievedTitle.TabIndex = 43;
            // 
            // txtRetrievedArtist
            // 
            this.txtRetrievedArtist.BackColor = System.Drawing.Color.White;
            this.txtRetrievedArtist.Location = new System.Drawing.Point(13, 79);
            this.txtRetrievedArtist.Name = "txtRetrievedArtist";
            this.txtRetrievedArtist.Size = new System.Drawing.Size(302, 20);
            this.txtRetrievedArtist.TabIndex = 44;
            // 
            // txtRetrievedAlbum
            // 
            this.txtRetrievedAlbum.BackColor = System.Drawing.Color.White;
            this.txtRetrievedAlbum.Location = new System.Drawing.Point(321, 38);
            this.txtRetrievedAlbum.Name = "txtRetrievedAlbum";
            this.txtRetrievedAlbum.Size = new System.Drawing.Size(229, 20);
            this.txtRetrievedAlbum.TabIndex = 45;
            // 
            // txtRetrievedYear
            // 
            this.txtRetrievedYear.BackColor = System.Drawing.Color.White;
            this.txtRetrievedYear.Location = new System.Drawing.Point(272, 126);
            this.txtRetrievedYear.Name = "txtRetrievedYear";
            this.txtRetrievedYear.Size = new System.Drawing.Size(43, 20);
            this.txtRetrievedYear.TabIndex = 46;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(321, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Album Art";
            // 
            // txtRetrievedGenre
            // 
            this.txtRetrievedGenre.BackColor = System.Drawing.Color.White;
            this.txtRetrievedGenre.Location = new System.Drawing.Point(13, 126);
            this.txtRetrievedGenre.Name = "txtRetrievedGenre";
            this.txtRetrievedGenre.Size = new System.Drawing.Size(253, 20);
            this.txtRetrievedGenre.TabIndex = 54;
            // 
            // picRetrievedArt
            // 
            this.picRetrievedArt.Location = new System.Drawing.Point(324, 79);
            this.picRetrievedArt.Name = "picRetrievedArt";
            this.picRetrievedArt.Size = new System.Drawing.Size(96, 82);
            this.picRetrievedArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRetrievedArt.TabIndex = 52;
            this.picRetrievedArt.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 47;
            this.label13.Text = "Title";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 60);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 13);
            this.label12.TabIndex = 48;
            this.label12.Text = "Artist";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(318, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 49;
            this.label11.Text = "Album";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Genres";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(269, 110);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 50;
            this.label10.Text = "Year";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(577, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Results";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.chkGetAlbum);
            this.groupBox1.Controls.Add(this.chkGetArtist);
            this.groupBox1.Controls.Add(this.chkGetTitle);
            this.groupBox1.Controls.Add(this.txtExistingTitle);
            this.groupBox1.Controls.Add(this.txtExistingArtist);
            this.groupBox1.Controls.Add(this.txtExistingAlbum);
            this.groupBox1.Controls.Add(this.txtExistingReleaseDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtExistingGenre);
            this.groupBox1.Controls.Add(this.picExistingArt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(14, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 303);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Metadata From Audio File";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 235);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "Track #";
            this.label14.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(18, 251);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(95, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(316, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Query";
            // 
            // chkGetAlbum
            // 
            this.chkGetAlbum.AutoSize = true;
            this.chkGetAlbum.Location = new System.Drawing.Point(324, 126);
            this.chkGetAlbum.Name = "chkGetAlbum";
            this.chkGetAlbum.Size = new System.Drawing.Size(15, 14);
            this.chkGetAlbum.TabIndex = 11;
            this.chkGetAlbum.UseVisualStyleBackColor = true;
            // 
            // chkGetArtist
            // 
            this.chkGetArtist.AutoSize = true;
            this.chkGetArtist.Checked = true;
            this.chkGetArtist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGetArtist.Location = new System.Drawing.Point(324, 82);
            this.chkGetArtist.Name = "chkGetArtist";
            this.chkGetArtist.Size = new System.Drawing.Size(15, 14);
            this.chkGetArtist.TabIndex = 8;
            this.chkGetArtist.UseVisualStyleBackColor = true;
            // 
            // chkGetTitle
            // 
            this.chkGetTitle.AutoSize = true;
            this.chkGetTitle.Checked = true;
            this.chkGetTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGetTitle.Enabled = false;
            this.chkGetTitle.Location = new System.Drawing.Point(324, 42);
            this.chkGetTitle.Name = "chkGetTitle";
            this.chkGetTitle.Size = new System.Drawing.Size(15, 14);
            this.chkGetTitle.TabIndex = 4;
            this.chkGetTitle.UseVisualStyleBackColor = true;
            // 
            // txtExistingTitle
            // 
            this.txtExistingTitle.BackColor = System.Drawing.Color.White;
            this.txtExistingTitle.Location = new System.Drawing.Point(18, 40);
            this.txtExistingTitle.Name = "txtExistingTitle";
            this.txtExistingTitle.Size = new System.Drawing.Size(301, 20);
            this.txtExistingTitle.TabIndex = 2;
            // 
            // txtExistingArtist
            // 
            this.txtExistingArtist.BackColor = System.Drawing.Color.White;
            this.txtExistingArtist.Location = new System.Drawing.Point(18, 79);
            this.txtExistingArtist.Name = "txtExistingArtist";
            this.txtExistingArtist.Size = new System.Drawing.Size(301, 20);
            this.txtExistingArtist.TabIndex = 7;
            // 
            // txtExistingAlbum
            // 
            this.txtExistingAlbum.BackColor = System.Drawing.Color.White;
            this.txtExistingAlbum.Location = new System.Drawing.Point(18, 123);
            this.txtExistingAlbum.Name = "txtExistingAlbum";
            this.txtExistingAlbum.Size = new System.Drawing.Size(301, 20);
            this.txtExistingAlbum.TabIndex = 10;
            // 
            // txtExistingReleaseDate
            // 
            this.txtExistingReleaseDate.BackColor = System.Drawing.Color.White;
            this.txtExistingReleaseDate.Location = new System.Drawing.Point(18, 208);
            this.txtExistingReleaseDate.Name = "txtExistingReleaseDate";
            this.txtExistingReleaseDate.Size = new System.Drawing.Size(95, 20);
            this.txtExistingReleaseDate.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Album Art";
            // 
            // txtExistingGenre
            // 
            this.txtExistingGenre.BackColor = System.Drawing.Color.White;
            this.txtExistingGenre.Location = new System.Drawing.Point(18, 165);
            this.txtExistingGenre.Name = "txtExistingGenre";
            this.txtExistingGenre.Size = new System.Drawing.Size(301, 20);
            this.txtExistingGenre.TabIndex = 13;
            // 
            // picExistingArt
            // 
            this.picExistingArt.Location = new System.Drawing.Point(229, 208);
            this.picExistingArt.Name = "picExistingArt";
            this.picExistingArt.Size = new System.Drawing.Size(96, 82);
            this.picExistingArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picExistingArt.TabIndex = 52;
            this.picExistingArt.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Title";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Artist";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Album";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Genres";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Date";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbSpotify);
            this.groupBox3.Controls.Add(this.rbNapster);
            this.groupBox3.Controls.Add(this.rbMusixMatch);
            this.groupBox3.Controls.Add(this.rbMusicBrainz);
            this.groupBox3.Controls.Add(this.rbLastFM);
            this.groupBox3.Controls.Add(this.rbITunes);
            this.groupBox3.Controls.Add(this.rbGenius);
            this.groupBox3.Controls.Add(this.rbGaliboo);
            this.groupBox3.Controls.Add(this.rbDiscogs);
            this.groupBox3.Controls.Add(this.rbDeezer);
            this.groupBox3.Controls.Add(this.btnSearchForAlbum);
            this.groupBox3.Controls.Add(this.btnQueryForMetadata);
            this.groupBox3.Controls.Add(this.btnSearchForArtwork);
            this.groupBox3.Location = new System.Drawing.Point(378, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(196, 301);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search Parameters";
            // 
            // rbSpotify
            // 
            this.rbSpotify.AutoSize = true;
            this.rbSpotify.Location = new System.Drawing.Point(104, 111);
            this.rbSpotify.Name = "rbSpotify";
            this.rbSpotify.Size = new System.Drawing.Size(57, 17);
            this.rbSpotify.TabIndex = 9;
            this.rbSpotify.TabStop = true;
            this.rbSpotify.Text = "Spotify";
            this.rbSpotify.UseVisualStyleBackColor = true;
            // 
            // rbNapster
            // 
            this.rbNapster.AutoSize = true;
            this.rbNapster.Location = new System.Drawing.Point(13, 111);
            this.rbNapster.Name = "rbNapster";
            this.rbNapster.Size = new System.Drawing.Size(62, 17);
            this.rbNapster.TabIndex = 8;
            this.rbNapster.TabStop = true;
            this.rbNapster.Text = "Napster";
            this.rbNapster.UseVisualStyleBackColor = true;
            // 
            // rbMusixMatch
            // 
            this.rbMusixMatch.AutoSize = true;
            this.rbMusixMatch.Location = new System.Drawing.Point(104, 88);
            this.rbMusixMatch.Name = "rbMusixMatch";
            this.rbMusixMatch.Size = new System.Drawing.Size(82, 17);
            this.rbMusixMatch.TabIndex = 7;
            this.rbMusixMatch.TabStop = true;
            this.rbMusixMatch.Text = "MusixMatch";
            this.rbMusixMatch.UseVisualStyleBackColor = true;
            // 
            // rbMusicBrainz
            // 
            this.rbMusicBrainz.AutoSize = true;
            this.rbMusicBrainz.Location = new System.Drawing.Point(13, 88);
            this.rbMusicBrainz.Name = "rbMusicBrainz";
            this.rbMusicBrainz.Size = new System.Drawing.Size(82, 17);
            this.rbMusicBrainz.TabIndex = 6;
            this.rbMusicBrainz.TabStop = true;
            this.rbMusicBrainz.Text = "MusicBrainz";
            this.rbMusicBrainz.UseVisualStyleBackColor = true;
            // 
            // rbLastFM
            // 
            this.rbLastFM.AutoSize = true;
            this.rbLastFM.Location = new System.Drawing.Point(104, 65);
            this.rbLastFM.Name = "rbLastFM";
            this.rbLastFM.Size = new System.Drawing.Size(60, 17);
            this.rbLastFM.TabIndex = 5;
            this.rbLastFM.TabStop = true;
            this.rbLastFM.Text = "LastFM";
            this.rbLastFM.UseVisualStyleBackColor = true;
            // 
            // rbITunes
            // 
            this.rbITunes.AutoSize = true;
            this.rbITunes.Location = new System.Drawing.Point(13, 65);
            this.rbITunes.Name = "rbITunes";
            this.rbITunes.Size = new System.Drawing.Size(57, 17);
            this.rbITunes.TabIndex = 4;
            this.rbITunes.TabStop = true;
            this.rbITunes.Text = "iTunes";
            this.rbITunes.UseVisualStyleBackColor = true;
            // 
            // rbGenius
            // 
            this.rbGenius.AutoSize = true;
            this.rbGenius.Location = new System.Drawing.Point(104, 42);
            this.rbGenius.Name = "rbGenius";
            this.rbGenius.Size = new System.Drawing.Size(58, 17);
            this.rbGenius.TabIndex = 3;
            this.rbGenius.TabStop = true;
            this.rbGenius.Text = "Genius";
            this.rbGenius.UseVisualStyleBackColor = true;
            // 
            // rbGaliboo
            // 
            this.rbGaliboo.AutoSize = true;
            this.rbGaliboo.Location = new System.Drawing.Point(13, 42);
            this.rbGaliboo.Name = "rbGaliboo";
            this.rbGaliboo.Size = new System.Drawing.Size(61, 17);
            this.rbGaliboo.TabIndex = 2;
            this.rbGaliboo.TabStop = true;
            this.rbGaliboo.Text = "Galiboo";
            this.rbGaliboo.UseVisualStyleBackColor = true;
            // 
            // rbDiscogs
            // 
            this.rbDiscogs.AutoSize = true;
            this.rbDiscogs.Location = new System.Drawing.Point(104, 19);
            this.rbDiscogs.Name = "rbDiscogs";
            this.rbDiscogs.Size = new System.Drawing.Size(63, 17);
            this.rbDiscogs.TabIndex = 1;
            this.rbDiscogs.TabStop = true;
            this.rbDiscogs.Text = "Discogs";
            this.rbDiscogs.UseVisualStyleBackColor = true;
            // 
            // rbDeezer
            // 
            this.rbDeezer.AutoSize = true;
            this.rbDeezer.Location = new System.Drawing.Point(13, 19);
            this.rbDeezer.Name = "rbDeezer";
            this.rbDeezer.Size = new System.Drawing.Size(59, 17);
            this.rbDeezer.TabIndex = 0;
            this.rbDeezer.TabStop = true;
            this.rbDeezer.Text = "Deezer";
            this.rbDeezer.UseVisualStyleBackColor = true;
            this.rbDeezer.CheckedChanged += new System.EventHandler(this.rbDeezer_CheckedChanged);
            // 
            // btnSearchForAlbum
            // 
            this.btnSearchForAlbum.Location = new System.Drawing.Point(13, 194);
            this.btnSearchForAlbum.Name = "btnSearchForAlbum";
            this.btnSearchForAlbum.Size = new System.Drawing.Size(173, 40);
            this.btnSearchForAlbum.TabIndex = 11;
            this.btnSearchForAlbum.Text = "Search for Album";
            this.btnSearchForAlbum.UseVisualStyleBackColor = true;
            this.btnSearchForAlbum.Click += new System.EventHandler(this.btnSearchForAlbum_Click);
            // 
            // btnQueryForMetadata
            // 
            this.btnQueryForMetadata.Location = new System.Drawing.Point(13, 145);
            this.btnQueryForMetadata.Name = "btnQueryForMetadata";
            this.btnQueryForMetadata.Size = new System.Drawing.Size(173, 36);
            this.btnQueryForMetadata.TabIndex = 10;
            this.btnQueryForMetadata.Text = "Search for Metadata";
            this.btnQueryForMetadata.UseVisualStyleBackColor = true;
            this.btnQueryForMetadata.Click += new System.EventHandler(this.btnQueryForMetadata_Click);
            // 
            // btnSearchForArtwork
            // 
            this.btnSearchForArtwork.Location = new System.Drawing.Point(13, 247);
            this.btnSearchForArtwork.Name = "btnSearchForArtwork";
            this.btnSearchForArtwork.Size = new System.Drawing.Size(173, 39);
            this.btnSearchForArtwork.TabIndex = 12;
            this.btnSearchForArtwork.Text = "Search for Artwork";
            this.btnSearchForArtwork.UseVisualStyleBackColor = true;
            this.btnSearchForArtwork.Click += new System.EventHandler(this.btnSearchForArtwork_Click);
            // 
            // DisplayMetadataOnAllMatches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 518);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.dataGridAllResults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1275, 561);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1275, 561);
            this.Name = "DisplayMetadataOnAllMatches";
            this.Text = "All Results From Metadata Query";
            this.Load += new System.EventHandler(this.DisplayMetadataOnAllMatches_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAllResults)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRetrievedArt)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExistingArt)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridAllResults;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtRetrievedTitle;
        private System.Windows.Forms.TextBox txtRetrievedArtist;
        private System.Windows.Forms.TextBox txtRetrievedAlbum;
        private System.Windows.Forms.TextBox txtRetrievedYear;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRetrievedGenre;
        private System.Windows.Forms.PictureBox picRetrievedArt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnUseSelectedData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtExistingTitle;
        private System.Windows.Forms.TextBox txtExistingArtist;
        private System.Windows.Forms.TextBox txtExistingAlbum;
        private System.Windows.Forms.TextBox txtExistingReleaseDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExistingGenre;
        private System.Windows.Forms.PictureBox picExistingArt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkGetAlbum;
        private System.Windows.Forms.CheckBox chkGetArtist;
        private System.Windows.Forms.CheckBox chkGetTitle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSearchForAlbum;
        private System.Windows.Forms.Button btnSearchForArtwork;
        private System.Windows.Forms.Button btnQueryForMetadata;
        private System.Windows.Forms.RadioButton rbITunes;
        private System.Windows.Forms.RadioButton rbGenius;
        private System.Windows.Forms.RadioButton rbGaliboo;
        private System.Windows.Forms.RadioButton rbDiscogs;
        private System.Windows.Forms.RadioButton rbDeezer;
        private System.Windows.Forms.RadioButton rbSpotify;
        private System.Windows.Forms.RadioButton rbNapster;
        private System.Windows.Forms.RadioButton rbMusixMatch;
        private System.Windows.Forms.RadioButton rbMusicBrainz;
        private System.Windows.Forms.RadioButton rbLastFM;
    }
}