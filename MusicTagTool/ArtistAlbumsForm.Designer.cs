namespace TotalTagger
{
    partial class ArtistAlbumsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArtistAlbumsForm));
            this.lvAlbumListings = new System.Windows.Forms.ListView();
            this.btnSaveAlbum = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvAlbumListings
            // 
            this.lvAlbumListings.CheckBoxes = true;
            this.lvAlbumListings.FullRowSelect = true;
            this.lvAlbumListings.GridLines = true;
            this.lvAlbumListings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvAlbumListings.Location = new System.Drawing.Point(12, 33);
            this.lvAlbumListings.MultiSelect = false;
            this.lvAlbumListings.Name = "lvAlbumListings";
            this.lvAlbumListings.Size = new System.Drawing.Size(564, 542);
            this.lvAlbumListings.TabIndex = 1;
            this.lvAlbumListings.UseCompatibleStateImageBehavior = false;
            this.lvAlbumListings.View = System.Windows.Forms.View.Details;
            this.lvAlbumListings.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvAlbumListings_ItemChecked);
            // 
            // btnSaveAlbum
            // 
            this.btnSaveAlbum.Location = new System.Drawing.Point(410, 588);
            this.btnSaveAlbum.Name = "btnSaveAlbum";
            this.btnSaveAlbum.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAlbum.TabIndex = 2;
            this.btnSaveAlbum.Text = "OK";
            this.btnSaveAlbum.UseVisualStyleBackColor = true;
            this.btnSaveAlbum.Click += new System.EventHandler(this.btnSaveAlbum_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(501, 588);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblHeader
            // 
            this.lblHeader.Location = new System.Drawing.Point(12, 17);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(565, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Albums Fetched For";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ArtistAlbumsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 619);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveAlbum);
            this.Controls.Add(this.lvAlbumListings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(605, 662);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(605, 662);
            this.Name = "ArtistAlbumsForm";
            this.Text = "Albums Fetched from Online Sources";
            this.Load += new System.EventHandler(this.ArtistAlbumsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lvAlbumListings;
        private System.Windows.Forms.Button btnSaveAlbum;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblHeader;
    }
}