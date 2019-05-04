namespace TotalTagger
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.contextMenuStripGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridSongFiles = new System.Windows.Forms.DataGridView();
            this.txtID3Year = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.picID3AlbumArt = new System.Windows.Forms.PictureBox();
            this.contextMenuAlbumArt = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.searchLastFMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.removeAlbumArtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtID3Genre = new System.Windows.Forms.TextBox();
            this.txtID3Album = new System.Windows.Forms.TextBox();
            this.txtID3Artist = new System.Windows.Forms.TextBox();
            this.txtID3Title = new System.Windows.Forms.TextBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNewTitle = new System.Windows.Forms.TextBox();
            this.txtNewArtist = new System.Windows.Forms.TextBox();
            this.txtNewDate = new System.Windows.Forms.TextBox();
            this.txtNewAlbum = new System.Windows.Forms.TextBox();
            this.txtNewGenre = new System.Windows.Forms.TextBox();
            this.picNewAlbumArt = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSaveTags = new System.Windows.Forms.Button();
            this.btnRemoveTags = new System.Windows.Forms.Button();
            this.btnAdvancedLookup = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.btnOpenFiles = new System.Windows.Forms.Button();
            this.btnSimpleLookup = new System.Windows.Forms.Button();
            this.btnFindAlbumArt = new System.Windows.Forms.Button();
            this.btnFindAlbums = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnBestMatch = new System.Windows.Forms.Button();
            this.txtLogging = new System.Windows.Forms.TextBox();
            this.cbServiceSimpleLookup = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.progressRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.lblProgressBar = new System.Windows.Forms.Label();
            this.btnFindDuplicates = new System.Windows.Forms.Button();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.btnCopyOldToNew = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.chkTransferTitle = new System.Windows.Forms.CheckBox();
            this.chkTransferArtist = new System.Windows.Forms.CheckBox();
            this.chkTransferAlbum = new System.Windows.Forms.CheckBox();
            this.chkTransferGenre = new System.Windows.Forms.CheckBox();
            this.chkTransferDate = new System.Windows.Forms.CheckBox();
            this.chkTransferArt = new System.Windows.Forms.CheckBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.playProgressBar = new TotalTagger.ModifiedProgressBarSeek();
            this.contextMenuStripGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSongFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picID3AlbumArt)).BeginInit();
            this.contextMenuAlbumArt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNewAlbumArt)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripGrid
            // 
            this.contextMenuStripGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playAudioToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteFileToolStripMenuItem});
            this.contextMenuStripGrid.Name = "contextMenuStripGrid";
            this.contextMenuStripGrid.Size = new System.Drawing.Size(132, 54);
            // 
            // playAudioToolStripMenuItem
            // 
            this.playAudioToolStripMenuItem.Name = "playAudioToolStripMenuItem";
            this.playAudioToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.playAudioToolStripMenuItem.Text = "Play Audio";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(128, 6);
            // 
            // deleteFileToolStripMenuItem
            // 
            this.deleteFileToolStripMenuItem.Name = "deleteFileToolStripMenuItem";
            this.deleteFileToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.deleteFileToolStripMenuItem.Text = "Delete File";
            this.deleteFileToolStripMenuItem.Click += new System.EventHandler(this.deleteFileToolStripMenuItem_Click);
            // 
            // dataGridSongFiles
            // 
            this.dataGridSongFiles.AllowUserToAddRows = false;
            this.dataGridSongFiles.AllowUserToDeleteRows = false;
            this.dataGridSongFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridSongFiles.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridSongFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridSongFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSongFiles.ContextMenuStrip = this.contextMenuStripGrid;
            this.dataGridSongFiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridSongFiles.Location = new System.Drawing.Point(12, 85);
            this.dataGridSongFiles.Name = "dataGridSongFiles";
            this.dataGridSongFiles.Size = new System.Drawing.Size(1461, 397);
            this.dataGridSongFiles.TabIndex = 16;
            this.dataGridSongFiles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridSongFiles_CellClick);
            this.dataGridSongFiles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridSongFiles_CellDoubleClick);
            // 
            // txtID3Year
            // 
            this.txtID3Year.Location = new System.Drawing.Point(66, 588);
            this.txtID3Year.Name = "txtID3Year";
            this.txtID3Year.Size = new System.Drawing.Size(298, 20);
            this.txtID3Year.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(12, 568);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 20);
            this.label5.TabIndex = 23;
            this.label5.Text = "Genre";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picID3AlbumArt
            // 
            this.picID3AlbumArt.Location = new System.Drawing.Point(389, 484);
            this.picID3AlbumArt.Name = "picID3AlbumArt";
            this.picID3AlbumArt.Size = new System.Drawing.Size(272, 172);
            this.picID3AlbumArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picID3AlbumArt.TabIndex = 5;
            this.picID3AlbumArt.TabStop = false;
            // 
            // contextMenuAlbumArt
            // 
            this.contextMenuAlbumArt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchLastFMToolStripMenuItem,
            this.toolStripSeparator3,
            this.removeAlbumArtToolStripMenuItem});
            this.contextMenuAlbumArt.Name = "contextMenuAlbumArt";
            this.contextMenuAlbumArt.Size = new System.Drawing.Size(227, 54);
            // 
            // searchLastFMToolStripMenuItem
            // 
            this.searchLastFMToolStripMenuItem.Name = "searchLastFMToolStripMenuItem";
            this.searchLastFMToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.searchLastFMToolStripMenuItem.Text = "Search LastFM for Album Art";
            this.searchLastFMToolStripMenuItem.Click += new System.EventHandler(this.searchLastFMToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(223, 6);
            // 
            // removeAlbumArtToolStripMenuItem
            // 
            this.removeAlbumArtToolStripMenuItem.Name = "removeAlbumArtToolStripMenuItem";
            this.removeAlbumArtToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.removeAlbumArtToolStripMenuItem.Text = "Remove Album Art";
            this.removeAlbumArtToolStripMenuItem.Click += new System.EventHandler(this.removeAlbumArtToolStripMenuItem_Click);
            // 
            // txtID3Genre
            // 
            this.txtID3Genre.Location = new System.Drawing.Point(66, 568);
            this.txtID3Genre.Name = "txtID3Genre";
            this.txtID3Genre.Size = new System.Drawing.Size(298, 20);
            this.txtID3Genre.TabIndex = 24;
            // 
            // txtID3Album
            // 
            this.txtID3Album.Location = new System.Drawing.Point(66, 548);
            this.txtID3Album.Name = "txtID3Album";
            this.txtID3Album.Size = new System.Drawing.Size(298, 20);
            this.txtID3Album.TabIndex = 22;
            // 
            // txtID3Artist
            // 
            this.txtID3Artist.Location = new System.Drawing.Point(66, 528);
            this.txtID3Artist.Name = "txtID3Artist";
            this.txtID3Artist.Size = new System.Drawing.Size(298, 20);
            this.txtID3Artist.TabIndex = 20;
            // 
            // txtID3Title
            // 
            this.txtID3Title.Location = new System.Drawing.Point(66, 508);
            this.txtID3Title.Name = "txtID3Title";
            this.txtID3Title.Size = new System.Drawing.Size(298, 20);
            this.txtID3Title.TabIndex = 18;
            // 
            // lblFolder
            // 
            this.lblFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolder.Location = new System.Drawing.Point(1146, 853);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(327, 19);
            this.lblFolder.TabIndex = 64;
            this.lblFolder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(12, 508);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Title";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(12, 528);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "Artist";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(12, 548);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 20);
            this.label9.TabIndex = 21;
            this.label9.Text = "Album";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(12, 588);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 20);
            this.label10.TabIndex = 25;
            this.label10.Text = "Date";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNewTitle
            // 
            this.txtNewTitle.Location = new System.Drawing.Point(820, 508);
            this.txtNewTitle.Name = "txtNewTitle";
            this.txtNewTitle.Size = new System.Drawing.Size(298, 20);
            this.txtNewTitle.TabIndex = 30;
            // 
            // txtNewArtist
            // 
            this.txtNewArtist.Location = new System.Drawing.Point(820, 528);
            this.txtNewArtist.Name = "txtNewArtist";
            this.txtNewArtist.Size = new System.Drawing.Size(298, 20);
            this.txtNewArtist.TabIndex = 32;
            // 
            // txtNewDate
            // 
            this.txtNewDate.Location = new System.Drawing.Point(820, 588);
            this.txtNewDate.Name = "txtNewDate";
            this.txtNewDate.Size = new System.Drawing.Size(298, 20);
            this.txtNewDate.TabIndex = 38;
            // 
            // txtNewAlbum
            // 
            this.txtNewAlbum.Location = new System.Drawing.Point(820, 548);
            this.txtNewAlbum.Name = "txtNewAlbum";
            this.txtNewAlbum.Size = new System.Drawing.Size(298, 20);
            this.txtNewAlbum.TabIndex = 34;
            // 
            // txtNewGenre
            // 
            this.txtNewGenre.Location = new System.Drawing.Point(820, 568);
            this.txtNewGenre.Name = "txtNewGenre";
            this.txtNewGenre.Size = new System.Drawing.Size(298, 20);
            this.txtNewGenre.TabIndex = 36;
            // 
            // picNewAlbumArt
            // 
            this.picNewAlbumArt.Location = new System.Drawing.Point(1125, 485);
            this.picNewAlbumArt.Name = "picNewAlbumArt";
            this.picNewAlbumArt.Size = new System.Drawing.Size(272, 172);
            this.picNewAlbumArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNewAlbumArt.TabIndex = 17;
            this.picNewAlbumArt.TabStop = false;
            this.picNewAlbumArt.Click += new System.EventHandler(this.PicNewAlbumArt_Click);
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 485);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(352, 23);
            this.label11.TabIndex = 66;
            this.label11.Text = "Old Tags";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(766, 485);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(352, 23);
            this.label12.TabIndex = 28;
            this.label12.Text = "New Tags";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSaveTags
            // 
            this.btnSaveTags.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveTags.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveTags.Image")));
            this.btnSaveTags.Location = new System.Drawing.Point(964, 12);
            this.btnSaveTags.Name = "btnSaveTags";
            this.btnSaveTags.Size = new System.Drawing.Size(75, 67);
            this.btnSaveTags.TabIndex = 10;
            this.btnSaveTags.Text = "Save Tags";
            this.btnSaveTags.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveTags.UseVisualStyleBackColor = true;
            this.btnSaveTags.Click += new System.EventHandler(this.btnSaveTags_Click);
            // 
            // btnRemoveTags
            // 
            this.btnRemoveTags.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveTags.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveTags.Image")));
            this.btnRemoveTags.Location = new System.Drawing.Point(889, 12);
            this.btnRemoveTags.Name = "btnRemoveTags";
            this.btnRemoveTags.Size = new System.Drawing.Size(75, 67);
            this.btnRemoveTags.TabIndex = 9;
            this.btnRemoveTags.Text = "Remove Tags";
            this.btnRemoveTags.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRemoveTags.UseVisualStyleBackColor = true;
            this.btnRemoveTags.Click += new System.EventHandler(this.btnRemoveTags_Click);
            // 
            // btnAdvancedLookup
            // 
            this.btnAdvancedLookup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdvancedLookup.Image = ((System.Drawing.Image)(resources.GetObject("btnAdvancedLookup.Image")));
            this.btnAdvancedLookup.Location = new System.Drawing.Point(252, 12);
            this.btnAdvancedLookup.Name = "btnAdvancedLookup";
            this.btnAdvancedLookup.Size = new System.Drawing.Size(75, 67);
            this.btnAdvancedLookup.TabIndex = 3;
            this.btnAdvancedLookup.Text = "Advanced Lookup";
            this.btnAdvancedLookup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdvancedLookup.UseVisualStyleBackColor = true;
            this.btnAdvancedLookup.Click += new System.EventHandler(this.btnAdvancedLookup_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFolder.Image")));
            this.btnOpenFolder.Location = new System.Drawing.Point(12, 12);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(75, 67);
            this.btnOpenFolder.TabIndex = 0;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // btnOpenFiles
            // 
            this.btnOpenFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFiles.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFiles.Image")));
            this.btnOpenFiles.Location = new System.Drawing.Point(87, 12);
            this.btnOpenFiles.Name = "btnOpenFiles";
            this.btnOpenFiles.Size = new System.Drawing.Size(75, 67);
            this.btnOpenFiles.TabIndex = 1;
            this.btnOpenFiles.Text = "Open File(s)";
            this.btnOpenFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOpenFiles.UseVisualStyleBackColor = true;
            this.btnOpenFiles.Click += new System.EventHandler(this.btnOpenFiles_Click);
            // 
            // btnSimpleLookup
            // 
            this.btnSimpleLookup.FlatAppearance.BorderSize = 0;
            this.btnSimpleLookup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpleLookup.Image = ((System.Drawing.Image)(resources.GetObject("btnSimpleLookup.Image")));
            this.btnSimpleLookup.Location = new System.Drawing.Point(155, -1);
            this.btnSimpleLookup.Name = "btnSimpleLookup";
            this.btnSimpleLookup.Size = new System.Drawing.Size(75, 67);
            this.btnSimpleLookup.TabIndex = 2;
            this.btnSimpleLookup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSimpleLookup.UseVisualStyleBackColor = true;
            this.btnSimpleLookup.Click += new System.EventHandler(this.btnSimpleLookup_Click);
            // 
            // btnFindAlbumArt
            // 
            this.btnFindAlbumArt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindAlbumArt.Image = ((System.Drawing.Image)(resources.GetObject("btnFindAlbumArt.Image")));
            this.btnFindAlbumArt.Location = new System.Drawing.Point(709, 12);
            this.btnFindAlbumArt.Name = "btnFindAlbumArt";
            this.btnFindAlbumArt.Size = new System.Drawing.Size(75, 67);
            this.btnFindAlbumArt.TabIndex = 7;
            this.btnFindAlbumArt.Text = "Scan For Cover Art";
            this.btnFindAlbumArt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFindAlbumArt.UseVisualStyleBackColor = true;
            this.btnFindAlbumArt.Visible = false;
            // 
            // btnFindAlbums
            // 
            this.btnFindAlbums.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindAlbums.Image = ((System.Drawing.Image)(resources.GetObject("btnFindAlbums.Image")));
            this.btnFindAlbums.Location = new System.Drawing.Point(634, 12);
            this.btnFindAlbums.Name = "btnFindAlbums";
            this.btnFindAlbums.Size = new System.Drawing.Size(75, 67);
            this.btnFindAlbums.TabIndex = 6;
            this.btnFindAlbums.Text = "Scan For Album";
            this.btnFindAlbums.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFindAlbums.UseVisualStyleBackColor = true;
            this.btnFindAlbums.Click += new System.EventHandler(this.btnFindAlbums_Click);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(766, 588);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.TabIndex = 37;
            this.label3.Text = "Date";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(766, 508);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 20);
            this.label6.TabIndex = 29;
            this.label6.Text = "Title";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(766, 528);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 20);
            this.label8.TabIndex = 31;
            this.label8.Text = "Artist";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label14.Location = new System.Drawing.Point(766, 548);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 20);
            this.label14.TabIndex = 33;
            this.label14.Text = "Album";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label15.Location = new System.Drawing.Point(766, 568);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 20);
            this.label15.TabIndex = 35;
            this.label15.Text = "Genre";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBestMatch
            // 
            this.btnBestMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBestMatch.Image = ((System.Drawing.Image)(resources.GetObject("btnBestMatch.Image")));
            this.btnBestMatch.Location = new System.Drawing.Point(559, 12);
            this.btnBestMatch.Name = "btnBestMatch";
            this.btnBestMatch.Size = new System.Drawing.Size(75, 67);
            this.btnBestMatch.TabIndex = 5;
            this.btnBestMatch.Text = "First Find";
            this.btnBestMatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBestMatch.UseVisualStyleBackColor = true;
            this.btnBestMatch.Click += new System.EventHandler(this.btnBestMatch_Click);
            // 
            // txtLogging
            // 
            this.txtLogging.Location = new System.Drawing.Point(1453, 500);
            this.txtLogging.Multiline = true;
            this.txtLogging.Name = "txtLogging";
            this.txtLogging.Size = new System.Drawing.Size(20, 149);
            this.txtLogging.TabIndex = 86;
            this.txtLogging.Visible = false;
            this.txtLogging.TextChanged += new System.EventHandler(this.txtLogging_TextChanged);
            // 
            // cbServiceSimpleLookup
            // 
            this.cbServiceSimpleLookup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServiceSimpleLookup.FormattingEnabled = true;
            this.cbServiceSimpleLookup.Location = new System.Drawing.Point(10, 32);
            this.cbServiceSimpleLookup.Name = "cbServiceSimpleLookup";
            this.cbServiceSimpleLookup.Size = new System.Drawing.Size(144, 21);
            this.cbServiceSimpleLookup.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Simple Lookup";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSimpleLookup);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.cbServiceSimpleLookup);
            this.panel1.Location = new System.Drawing.Point(327, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 67);
            this.panel1.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1428, 484);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 13);
            this.label13.TabIndex = 91;
            this.label13.Text = "Logging";
            this.label13.Visible = false;
            this.label13.Click += new System.EventHandler(this.label13_Click_1);
            // 
            // btnStop
            // 
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(1227, 21);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(43, 45);
            this.btnStop.TabIndex = 14;
            this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.FlatAppearance.BorderSize = 0;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(1187, 21);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(43, 45);
            this.btnPause.TabIndex = 13;
            this.btnPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(1149, 21);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(43, 45);
            this.btnPlay.TabIndex = 12;
            this.btnPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // progressRefreshTimer
            // 
            this.progressRefreshTimer.Tick += new System.EventHandler(this.progressRefreshTimer_Tick);
            // 
            // lblProgressBar
            // 
            this.lblProgressBar.Location = new System.Drawing.Point(12, 465);
            this.lblProgressBar.Name = "lblProgressBar";
            this.lblProgressBar.Size = new System.Drawing.Size(1461, 16);
            this.lblProgressBar.TabIndex = 97;
            this.lblProgressBar.Text = "label1";
            this.lblProgressBar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblProgressBar.Visible = false;
            // 
            // btnFindDuplicates
            // 
            this.btnFindDuplicates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindDuplicates.Image = ((System.Drawing.Image)(resources.GetObject("btnFindDuplicates.Image")));
            this.btnFindDuplicates.Location = new System.Drawing.Point(784, 12);
            this.btnFindDuplicates.Name = "btnFindDuplicates";
            this.btnFindDuplicates.Size = new System.Drawing.Size(75, 67);
            this.btnFindDuplicates.TabIndex = 8;
            this.btnFindDuplicates.Text = "Find Duplicates";
            this.btnFindDuplicates.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFindDuplicates.UseVisualStyleBackColor = true;
            this.btnFindDuplicates.Click += new System.EventHandler(this.BtnFindDuplicates_Click);
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshList.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshList.Image")));
            this.btnRefreshList.Location = new System.Drawing.Point(162, 12);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(75, 67);
            this.btnRefreshList.TabIndex = 2;
            this.btnRefreshList.Text = "Refresh Songs";
            this.btnRefreshList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRefreshList.UseVisualStyleBackColor = true;
            this.btnRefreshList.Click += new System.EventHandler(this.BtnRefreshList_Click);
            // 
            // btnCopyOldToNew
            // 
            this.btnCopyOldToNew.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyOldToNew.Image")));
            this.btnCopyOldToNew.Location = new System.Drawing.Point(697, 485);
            this.btnCopyOldToNew.Name = "btnCopyOldToNew";
            this.btnCopyOldToNew.Size = new System.Drawing.Size(49, 171);
            this.btnCopyOldToNew.TabIndex = 27;
            this.btnCopyOldToNew.UseVisualStyleBackColor = true;
            this.btnCopyOldToNew.Click += new System.EventHandler(this.BtnCopyOldToNew_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.Location = new System.Drawing.Point(1061, 12);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 67);
            this.btnSettings.TabIndex = 11;
            this.btnSettings.Text = "Settings";
            this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click_1);
            // 
            // chkTransferTitle
            // 
            this.chkTransferTitle.AutoSize = true;
            this.chkTransferTitle.Location = new System.Drawing.Point(366, 511);
            this.chkTransferTitle.Name = "chkTransferTitle";
            this.chkTransferTitle.Size = new System.Drawing.Size(15, 14);
            this.chkTransferTitle.TabIndex = 98;
            this.chkTransferTitle.UseVisualStyleBackColor = true;
            // 
            // chkTransferArtist
            // 
            this.chkTransferArtist.AutoSize = true;
            this.chkTransferArtist.Location = new System.Drawing.Point(366, 531);
            this.chkTransferArtist.Name = "chkTransferArtist";
            this.chkTransferArtist.Size = new System.Drawing.Size(15, 14);
            this.chkTransferArtist.TabIndex = 99;
            this.chkTransferArtist.UseVisualStyleBackColor = true;
            // 
            // chkTransferAlbum
            // 
            this.chkTransferAlbum.AutoSize = true;
            this.chkTransferAlbum.Location = new System.Drawing.Point(366, 551);
            this.chkTransferAlbum.Name = "chkTransferAlbum";
            this.chkTransferAlbum.Size = new System.Drawing.Size(15, 14);
            this.chkTransferAlbum.TabIndex = 100;
            this.chkTransferAlbum.UseVisualStyleBackColor = true;
            // 
            // chkTransferGenre
            // 
            this.chkTransferGenre.AutoSize = true;
            this.chkTransferGenre.Location = new System.Drawing.Point(366, 571);
            this.chkTransferGenre.Name = "chkTransferGenre";
            this.chkTransferGenre.Size = new System.Drawing.Size(15, 14);
            this.chkTransferGenre.TabIndex = 101;
            this.chkTransferGenre.UseVisualStyleBackColor = true;
            // 
            // chkTransferDate
            // 
            this.chkTransferDate.AutoSize = true;
            this.chkTransferDate.Location = new System.Drawing.Point(366, 591);
            this.chkTransferDate.Name = "chkTransferDate";
            this.chkTransferDate.Size = new System.Drawing.Size(15, 14);
            this.chkTransferDate.TabIndex = 102;
            this.chkTransferDate.UseVisualStyleBackColor = true;
            // 
            // chkTransferArt
            // 
            this.chkTransferArt.AutoSize = true;
            this.chkTransferArt.Location = new System.Drawing.Point(667, 554);
            this.chkTransferArt.Name = "chkTransferArt";
            this.chkTransferArt.Size = new System.Drawing.Size(15, 14);
            this.chkTransferArt.TabIndex = 103;
            this.chkTransferArt.UseVisualStyleBackColor = true;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(366, 488);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(15, 14);
            this.chkAll.TabIndex = 104;
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.ChkAll_CheckedChanged);
            // 
            // playProgressBar
            // 
            this.playProgressBar.BackColor = System.Drawing.SystemColors.Control;
            this.playProgressBar.Location = new System.Drawing.Point(1276, 29);
            this.playProgressBar.Name = "playProgressBar";
            this.playProgressBar.Size = new System.Drawing.Size(187, 28);
            this.playProgressBar.TabIndex = 15;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1485, 669);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.chkTransferArt);
            this.Controls.Add(this.chkTransferDate);
            this.Controls.Add(this.chkTransferGenre);
            this.Controls.Add(this.chkTransferAlbum);
            this.Controls.Add(this.chkTransferArtist);
            this.Controls.Add(this.chkTransferTitle);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnCopyOldToNew);
            this.Controls.Add(this.btnRefreshList);
            this.Controls.Add(this.btnFindDuplicates);
            this.Controls.Add(this.lblProgressBar);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.playProgressBar);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtLogging);
            this.Controls.Add(this.btnFindAlbums);
            this.Controls.Add(this.btnBestMatch);
            this.Controls.Add(this.btnFindAlbumArt);
            this.Controls.Add(this.btnOpenFiles);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnAdvancedLookup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSaveTags);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.btnRemoveTags);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridSongFiles);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtID3Album);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtID3Genre);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtID3Year);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtID3Artist);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtID3Title);
            this.Controls.Add(this.picNewAlbumArt);
            this.Controls.Add(this.picID3AlbumArt);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtNewTitle);
            this.Controls.Add(this.txtNewDate);
            this.Controls.Add(this.txtNewArtist);
            this.Controls.Add(this.txtNewAlbum);
            this.Controls.Add(this.txtNewGenre);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1501, 708);
            this.MinimumSize = new System.Drawing.Size(1501, 708);
            this.Name = "MainWindow";
            this.Text = "Total Tagger";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStripGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSongFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picID3AlbumArt)).EndInit();
            this.contextMenuAlbumArt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picNewAlbumArt)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGrid;
        private System.Windows.Forms.ToolStripMenuItem playAudioToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridSongFiles;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtID3Year;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picID3AlbumArt;
        private System.Windows.Forms.TextBox txtID3Genre;
        private System.Windows.Forms.TextBox txtID3Album;
        private System.Windows.Forms.TextBox txtID3Artist;
        private System.Windows.Forms.TextBox txtID3Title;
        private System.Windows.Forms.ContextMenuStrip contextMenuAlbumArt;
        private System.Windows.Forms.ToolStripMenuItem searchLastFMToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem removeAlbumArtToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNewTitle;
        private System.Windows.Forms.TextBox txtNewArtist;
        private System.Windows.Forms.TextBox txtNewDate;
        private System.Windows.Forms.TextBox txtNewAlbum;
        private System.Windows.Forms.TextBox txtNewGenre;
        private System.Windows.Forms.PictureBox picNewAlbumArt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnSaveTags;
        private System.Windows.Forms.Button btnRemoveTags;
        private System.Windows.Forms.Button btnAdvancedLookup;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnOpenFiles;
        private System.Windows.Forms.Button btnSimpleLookup;
        private System.Windows.Forms.Button btnFindAlbumArt;
        private System.Windows.Forms.Button btnFindAlbums;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnBestMatch;
        private System.Windows.Forms.TextBox txtLogging;
        private System.Windows.Forms.ComboBox cbServiceSimpleLookup;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
        private ModifiedProgressBarSeek playProgressBar;
        public System.Windows.Forms.Timer progressRefreshTimer;
        private System.Windows.Forms.Label lblProgressBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteFileToolStripMenuItem;
        private System.Windows.Forms.Button btnFindDuplicates;
        private System.Windows.Forms.Button btnRefreshList;
        private System.Windows.Forms.Button btnCopyOldToNew;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.CheckBox chkTransferTitle;
        private System.Windows.Forms.CheckBox chkTransferArtist;
        private System.Windows.Forms.CheckBox chkTransferAlbum;
        private System.Windows.Forms.CheckBox chkTransferGenre;
        private System.Windows.Forms.CheckBox chkTransferDate;
        private System.Windows.Forms.CheckBox chkTransferArt;
        private System.Windows.Forms.CheckBox chkAll;
    }
}

