namespace TotalTagger
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowSpotifyID = new System.Windows.Forms.Button();
            this.btnShowDiscogsID = new System.Windows.Forms.Button();
            this.btnShowMusixMatch = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMusixMatchKey = new System.Windows.Forms.TextBox();
            this.chkEnableMusixMatch = new System.Windows.Forms.CheckBox();
            this.btnShowGenius = new System.Windows.Forms.Button();
            this.btnShowGaliboo = new System.Windows.Forms.Button();
            this.btnShowNapster = new System.Windows.Forms.Button();
            this.btnShowDiscogs = new System.Windows.Forms.Button();
            this.btnShowLastFM = new System.Windows.Forms.Button();
            this.btnShowSpotify = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGeniusAccessToken = new System.Windows.Forms.TextBox();
            this.chkEnableGenius = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGalibooApiKey = new System.Windows.Forms.TextBox();
            this.chkEnableGaliboo = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNapsterAPIKey = new System.Windows.Forms.TextBox();
            this.chkEnableNapster = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDiscogsConsumerSecret = new System.Windows.Forms.TextBox();
            this.txtDiscogsConsumerKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLastFMApiKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSpotifySecretClient = new System.Windows.Forms.TextBox();
            this.txtSpotifyClientID = new System.Windows.Forms.TextBox();
            this.chkEnableDiscogs = new System.Windows.Forms.CheckBox();
            this.chkLastFM = new System.Windows.Forms.CheckBox();
            this.chkEnableSpotify = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(160, 458);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 91;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(306, 458);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 92;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnShowSpotifyID);
            this.groupBox1.Controls.Add(this.btnShowDiscogsID);
            this.groupBox1.Controls.Add(this.btnShowMusixMatch);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtMusixMatchKey);
            this.groupBox1.Controls.Add(this.chkEnableMusixMatch);
            this.groupBox1.Controls.Add(this.btnShowGenius);
            this.groupBox1.Controls.Add(this.btnShowGaliboo);
            this.groupBox1.Controls.Add(this.btnShowNapster);
            this.groupBox1.Controls.Add(this.btnShowDiscogs);
            this.groupBox1.Controls.Add(this.btnShowLastFM);
            this.groupBox1.Controls.Add(this.btnShowSpotify);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtGeniusAccessToken);
            this.groupBox1.Controls.Add(this.chkEnableGenius);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtGalibooApiKey);
            this.groupBox1.Controls.Add(this.chkEnableGaliboo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtNapsterAPIKey);
            this.groupBox1.Controls.Add(this.chkEnableNapster);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtDiscogsConsumerSecret);
            this.groupBox1.Controls.Add(this.txtDiscogsConsumerKey);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLastFMApiKey);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSpotifySecretClient);
            this.groupBox1.Controls.Add(this.txtSpotifyClientID);
            this.groupBox1.Controls.Add(this.chkEnableDiscogs);
            this.groupBox1.Controls.Add(this.chkLastFM);
            this.groupBox1.Controls.Add(this.chkEnableSpotify);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 428);
            this.groupBox1.TabIndex = 93;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Services";
            // 
            // btnShowSpotifyID
            // 
            this.btnShowSpotifyID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowSpotifyID.Image = ((System.Drawing.Image)(resources.GetObject("btnShowSpotifyID.Image")));
            this.btnShowSpotifyID.Location = new System.Drawing.Point(561, 356);
            this.btnShowSpotifyID.Name = "btnShowSpotifyID";
            this.btnShowSpotifyID.Size = new System.Drawing.Size(31, 20);
            this.btnShowSpotifyID.TabIndex = 37;
            this.btnShowSpotifyID.UseVisualStyleBackColor = true;
            this.btnShowSpotifyID.Click += new System.EventHandler(this.btnShowSpotifyID_Click);
            // 
            // btnShowDiscogsID
            // 
            this.btnShowDiscogsID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowDiscogsID.Image = ((System.Drawing.Image)(resources.GetObject("btnShowDiscogsID.Image")));
            this.btnShowDiscogsID.Location = new System.Drawing.Point(561, 44);
            this.btnShowDiscogsID.Name = "btnShowDiscogsID";
            this.btnShowDiscogsID.Size = new System.Drawing.Size(31, 20);
            this.btnShowDiscogsID.TabIndex = 36;
            this.btnShowDiscogsID.UseVisualStyleBackColor = true;
            this.btnShowDiscogsID.Click += new System.EventHandler(this.btnShowDiscogsID_Click);
            // 
            // btnShowMusixMatch
            // 
            this.btnShowMusixMatch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowMusixMatch.Image = ((System.Drawing.Image)(resources.GetObject("btnShowMusixMatch.Image")));
            this.btnShowMusixMatch.Location = new System.Drawing.Point(561, 265);
            this.btnShowMusixMatch.Name = "btnShowMusixMatch";
            this.btnShowMusixMatch.Size = new System.Drawing.Size(31, 20);
            this.btnShowMusixMatch.TabIndex = 35;
            this.btnShowMusixMatch.UseVisualStyleBackColor = true;
            this.btnShowMusixMatch.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 269);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "API Key";
            // 
            // txtMusixMatchKey
            // 
            this.txtMusixMatchKey.Location = new System.Drawing.Point(124, 265);
            this.txtMusixMatchKey.Name = "txtMusixMatchKey";
            this.txtMusixMatchKey.PasswordChar = '*';
            this.txtMusixMatchKey.Size = new System.Drawing.Size(431, 20);
            this.txtMusixMatchKey.TabIndex = 33;
            // 
            // chkEnableMusixMatch
            // 
            this.chkEnableMusixMatch.AutoSize = true;
            this.chkEnableMusixMatch.Location = new System.Drawing.Point(11, 249);
            this.chkEnableMusixMatch.Name = "chkEnableMusixMatch";
            this.chkEnableMusixMatch.Size = new System.Drawing.Size(83, 17);
            this.chkEnableMusixMatch.TabIndex = 32;
            this.chkEnableMusixMatch.Text = "MusixMatch";
            this.chkEnableMusixMatch.UseVisualStyleBackColor = true;
            this.chkEnableMusixMatch.CheckedChanged += new System.EventHandler(this.chkEnableMusixMatch_CheckedChanged);
            // 
            // btnShowGenius
            // 
            this.btnShowGenius.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowGenius.Image = ((System.Drawing.Image)(resources.GetObject("btnShowGenius.Image")));
            this.btnShowGenius.Location = new System.Drawing.Point(561, 167);
            this.btnShowGenius.Name = "btnShowGenius";
            this.btnShowGenius.Size = new System.Drawing.Size(31, 20);
            this.btnShowGenius.TabIndex = 31;
            this.btnShowGenius.UseVisualStyleBackColor = true;
            this.btnShowGenius.Click += new System.EventHandler(this.btnShowGenius_Click);
            // 
            // btnShowGaliboo
            // 
            this.btnShowGaliboo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowGaliboo.Image = ((System.Drawing.Image)(resources.GetObject("btnShowGaliboo.Image")));
            this.btnShowGaliboo.Location = new System.Drawing.Point(561, 116);
            this.btnShowGaliboo.Name = "btnShowGaliboo";
            this.btnShowGaliboo.Size = new System.Drawing.Size(31, 20);
            this.btnShowGaliboo.TabIndex = 30;
            this.btnShowGaliboo.UseVisualStyleBackColor = true;
            this.btnShowGaliboo.Click += new System.EventHandler(this.btnShowGaliboo_Click);
            // 
            // btnShowNapster
            // 
            this.btnShowNapster.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowNapster.Image = ((System.Drawing.Image)(resources.GetObject("btnShowNapster.Image")));
            this.btnShowNapster.Location = new System.Drawing.Point(561, 312);
            this.btnShowNapster.Name = "btnShowNapster";
            this.btnShowNapster.Size = new System.Drawing.Size(31, 20);
            this.btnShowNapster.TabIndex = 29;
            this.btnShowNapster.UseVisualStyleBackColor = true;
            this.btnShowNapster.Click += new System.EventHandler(this.btnShowNapster_Click);
            // 
            // btnShowDiscogs
            // 
            this.btnShowDiscogs.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowDiscogs.Image = ((System.Drawing.Image)(resources.GetObject("btnShowDiscogs.Image")));
            this.btnShowDiscogs.Location = new System.Drawing.Point(561, 69);
            this.btnShowDiscogs.Name = "btnShowDiscogs";
            this.btnShowDiscogs.Size = new System.Drawing.Size(31, 20);
            this.btnShowDiscogs.TabIndex = 28;
            this.btnShowDiscogs.UseVisualStyleBackColor = true;
            this.btnShowDiscogs.Click += new System.EventHandler(this.btnShowDiscogs_Click);
            // 
            // btnShowLastFM
            // 
            this.btnShowLastFM.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowLastFM.Image = ((System.Drawing.Image)(resources.GetObject("btnShowLastFM.Image")));
            this.btnShowLastFM.Location = new System.Drawing.Point(561, 218);
            this.btnShowLastFM.Name = "btnShowLastFM";
            this.btnShowLastFM.Size = new System.Drawing.Size(31, 20);
            this.btnShowLastFM.TabIndex = 27;
            this.btnShowLastFM.UseVisualStyleBackColor = true;
            this.btnShowLastFM.Click += new System.EventHandler(this.btnShowLastFM_Click);
            // 
            // btnShowSpotify
            // 
            this.btnShowSpotify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowSpotify.Image = ((System.Drawing.Image)(resources.GetObject("btnShowSpotify.Image")));
            this.btnShowSpotify.Location = new System.Drawing.Point(561, 382);
            this.btnShowSpotify.Name = "btnShowSpotify";
            this.btnShowSpotify.Size = new System.Drawing.Size(31, 20);
            this.btnShowSpotify.TabIndex = 26;
            this.btnShowSpotify.UseVisualStyleBackColor = true;
            this.btnShowSpotify.Click += new System.EventHandler(this.btnShowSpotify_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 171);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Access Token";
            // 
            // txtGeniusAccessToken
            // 
            this.txtGeniusAccessToken.Location = new System.Drawing.Point(124, 167);
            this.txtGeniusAccessToken.Name = "txtGeniusAccessToken";
            this.txtGeniusAccessToken.PasswordChar = '*';
            this.txtGeniusAccessToken.Size = new System.Drawing.Size(431, 20);
            this.txtGeniusAccessToken.TabIndex = 24;
            // 
            // chkEnableGenius
            // 
            this.chkEnableGenius.AutoSize = true;
            this.chkEnableGenius.Location = new System.Drawing.Point(11, 151);
            this.chkEnableGenius.Name = "chkEnableGenius";
            this.chkEnableGenius.Size = new System.Drawing.Size(59, 17);
            this.chkEnableGenius.TabIndex = 23;
            this.chkEnableGenius.Text = "Genius";
            this.chkEnableGenius.UseVisualStyleBackColor = true;
            this.chkEnableGenius.CheckedChanged += new System.EventHandler(this.chkEnableGenius_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "API Key";
            // 
            // txtGalibooApiKey
            // 
            this.txtGalibooApiKey.Location = new System.Drawing.Point(124, 116);
            this.txtGalibooApiKey.Name = "txtGalibooApiKey";
            this.txtGalibooApiKey.PasswordChar = '*';
            this.txtGalibooApiKey.Size = new System.Drawing.Size(431, 20);
            this.txtGalibooApiKey.TabIndex = 21;
            // 
            // chkEnableGaliboo
            // 
            this.chkEnableGaliboo.AutoSize = true;
            this.chkEnableGaliboo.Location = new System.Drawing.Point(11, 97);
            this.chkEnableGaliboo.Name = "chkEnableGaliboo";
            this.chkEnableGaliboo.Size = new System.Drawing.Size(62, 17);
            this.chkEnableGaliboo.TabIndex = 20;
            this.chkEnableGaliboo.Text = "Galiboo";
            this.chkEnableGaliboo.UseVisualStyleBackColor = true;
            this.chkEnableGaliboo.CheckedChanged += new System.EventHandler(this.chkEnableGaliboo_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 316);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "API Key";
            // 
            // txtNapsterAPIKey
            // 
            this.txtNapsterAPIKey.Location = new System.Drawing.Point(124, 312);
            this.txtNapsterAPIKey.Name = "txtNapsterAPIKey";
            this.txtNapsterAPIKey.PasswordChar = '*';
            this.txtNapsterAPIKey.Size = new System.Drawing.Size(431, 20);
            this.txtNapsterAPIKey.TabIndex = 18;
            // 
            // chkEnableNapster
            // 
            this.chkEnableNapster.AutoSize = true;
            this.chkEnableNapster.Location = new System.Drawing.Point(11, 296);
            this.chkEnableNapster.Name = "chkEnableNapster";
            this.chkEnableNapster.Size = new System.Drawing.Size(63, 17);
            this.chkEnableNapster.TabIndex = 17;
            this.chkEnableNapster.Text = "Napster";
            this.chkEnableNapster.UseVisualStyleBackColor = true;
            this.chkEnableNapster.CheckedChanged += new System.EventHandler(this.chkEnableNapster_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Consumer Secret";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Consumer Key";
            // 
            // txtDiscogsConsumerSecret
            // 
            this.txtDiscogsConsumerSecret.Location = new System.Drawing.Point(124, 69);
            this.txtDiscogsConsumerSecret.Name = "txtDiscogsConsumerSecret";
            this.txtDiscogsConsumerSecret.PasswordChar = '*';
            this.txtDiscogsConsumerSecret.Size = new System.Drawing.Size(431, 20);
            this.txtDiscogsConsumerSecret.TabIndex = 14;
            // 
            // txtDiscogsConsumerKey
            // 
            this.txtDiscogsConsumerKey.Location = new System.Drawing.Point(124, 44);
            this.txtDiscogsConsumerKey.Name = "txtDiscogsConsumerKey";
            this.txtDiscogsConsumerKey.PasswordChar = '*';
            this.txtDiscogsConsumerKey.Size = new System.Drawing.Size(431, 20);
            this.txtDiscogsConsumerKey.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "API Key";
            // 
            // txtLastFMApiKey
            // 
            this.txtLastFMApiKey.Location = new System.Drawing.Point(124, 218);
            this.txtLastFMApiKey.Name = "txtLastFMApiKey";
            this.txtLastFMApiKey.PasswordChar = '*';
            this.txtLastFMApiKey.Size = new System.Drawing.Size(431, 20);
            this.txtLastFMApiKey.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 385);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Secret Client";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Client ID";
            // 
            // txtSpotifySecretClient
            // 
            this.txtSpotifySecretClient.Location = new System.Drawing.Point(124, 382);
            this.txtSpotifySecretClient.Name = "txtSpotifySecretClient";
            this.txtSpotifySecretClient.PasswordChar = '*';
            this.txtSpotifySecretClient.Size = new System.Drawing.Size(431, 20);
            this.txtSpotifySecretClient.TabIndex = 8;
            // 
            // txtSpotifyClientID
            // 
            this.txtSpotifyClientID.Location = new System.Drawing.Point(124, 356);
            this.txtSpotifyClientID.Name = "txtSpotifyClientID";
            this.txtSpotifyClientID.PasswordChar = '*';
            this.txtSpotifyClientID.Size = new System.Drawing.Size(431, 20);
            this.txtSpotifyClientID.TabIndex = 7;
            // 
            // chkEnableDiscogs
            // 
            this.chkEnableDiscogs.AutoSize = true;
            this.chkEnableDiscogs.Location = new System.Drawing.Point(11, 28);
            this.chkEnableDiscogs.Name = "chkEnableDiscogs";
            this.chkEnableDiscogs.Size = new System.Drawing.Size(64, 17);
            this.chkEnableDiscogs.TabIndex = 3;
            this.chkEnableDiscogs.Text = "Discogs";
            this.chkEnableDiscogs.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkEnableDiscogs.UseVisualStyleBackColor = true;
            this.chkEnableDiscogs.CheckedChanged += new System.EventHandler(this.chkEnableDiscogs_CheckedChanged);
            // 
            // chkLastFM
            // 
            this.chkLastFM.AutoSize = true;
            this.chkLastFM.Location = new System.Drawing.Point(11, 200);
            this.chkLastFM.Name = "chkLastFM";
            this.chkLastFM.Size = new System.Drawing.Size(61, 17);
            this.chkLastFM.TabIndex = 2;
            this.chkLastFM.Text = "LastFM";
            this.chkLastFM.UseVisualStyleBackColor = true;
            this.chkLastFM.CheckedChanged += new System.EventHandler(this.chkLastFM_CheckedChanged);
            // 
            // chkEnableSpotify
            // 
            this.chkEnableSpotify.AutoSize = true;
            this.chkEnableSpotify.Location = new System.Drawing.Point(11, 338);
            this.chkEnableSpotify.Name = "chkEnableSpotify";
            this.chkEnableSpotify.Size = new System.Drawing.Size(58, 17);
            this.chkEnableSpotify.TabIndex = 0;
            this.chkEnableSpotify.Text = "Spotify";
            this.chkEnableSpotify.UseVisualStyleBackColor = true;
            this.chkEnableSpotify.CheckedChanged += new System.EventHandler(this.chkEnableSpotify_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 494);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Settings for TotalTagger";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtGeniusAccessToken;
        private System.Windows.Forms.CheckBox chkEnableGenius;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtGalibooApiKey;
        private System.Windows.Forms.CheckBox chkEnableGaliboo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNapsterAPIKey;
        private System.Windows.Forms.CheckBox chkEnableNapster;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDiscogsConsumerSecret;
        private System.Windows.Forms.TextBox txtDiscogsConsumerKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLastFMApiKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSpotifySecretClient;
        private System.Windows.Forms.TextBox txtSpotifyClientID;
        private System.Windows.Forms.CheckBox chkEnableDiscogs;
        private System.Windows.Forms.CheckBox chkLastFM;
        private System.Windows.Forms.CheckBox chkEnableSpotify;
        private System.Windows.Forms.Button btnShowGenius;
        private System.Windows.Forms.Button btnShowGaliboo;
        private System.Windows.Forms.Button btnShowNapster;
        private System.Windows.Forms.Button btnShowDiscogs;
        private System.Windows.Forms.Button btnShowLastFM;
        private System.Windows.Forms.Button btnShowSpotify;
        private System.Windows.Forms.Button btnShowMusixMatch;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMusixMatchKey;
        private System.Windows.Forms.CheckBox chkEnableMusixMatch;
        private System.Windows.Forms.Button btnShowSpotifyID;
        private System.Windows.Forms.Button btnShowDiscogsID;
    }
}