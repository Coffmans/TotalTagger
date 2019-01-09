using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalTagger
{
    public partial class ArtistAlbumsForm : Form
    {
        private List<AlbumMetadata> listAlbumMetadata = new List<AlbumMetadata>();
        public string ReturnAlbum = "";
        public ArtistAlbumsForm(List<AlbumMetadata> fetchedAlbums, string artistName)
        {
            InitializeComponent();

            listAlbumMetadata = fetchedAlbums;

            lblHeader.Text = "Albums Fetched For " + artistName;
        }

        private void ArtistAlbumsForm_Load(object sender, EventArgs e)
        {
            lvAlbumListings.Clear();

            ColumnHeader columnHeader = new ColumnHeader();
            columnHeader.Text = "Album Listings";
            columnHeader.Width = -2;
            lvAlbumListings.Columns.Add(columnHeader);


            ListViewGroup lvGroupMusicBrainz = new ListViewGroup();
            lvGroupMusicBrainz.Header = "MusicBrainz";
            lvGroupMusicBrainz.Tag = 0;
            lvAlbumListings.Groups.Add(lvGroupMusicBrainz);

            ListViewGroup lvGroup = new ListViewGroup();
            string previousSource = "";
            foreach (var album in listAlbumMetadata)
            {
                if( !previousSource.Equals(album.Source) )
                {
                    lvGroup = new ListViewGroup();
                    lvGroup.Header = album.Source;
                    lvGroupMusicBrainz.Tag = 0;
                    lvAlbumListings.Groups.Add(lvGroup);
                    previousSource = album.Source;
                }
                ListViewItem lvAlbum = new ListViewItem(lvGroup);
                lvAlbum.Checked = false;
                lvAlbum.Tag = lvAlbum.Text = album.MusicMetatada.Album;
                lvAlbumListings.Items.Add(lvAlbum);
            }
        }

        public void AddAlbumToList(string albumName)
        {
            
        }

        private void lvAlbumListings_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            lvAlbumListings.ItemChecked -= lvAlbumListings_ItemChecked;
            foreach (var item in lvAlbumListings.CheckedItems)
            {
                if (e.Item != item)
                {
                    ((ListViewItem)item).Checked = false;

                }

            }
            lvAlbumListings.ItemChecked += lvAlbumListings_ItemChecked;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // ...
            if (keyData == (Keys.Escape))
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnSaveAlbum_Click(object sender, EventArgs e)
        {
            if( lvAlbumListings.Items.Count > 0 )
            {
                ListViewItem lvItem;
                for(int item=0; item < lvAlbumListings.Items.Count; item++)
                {
                    lvItem = lvAlbumListings.Items[item];
                    if( lvItem.Checked)
                    {
                        ReturnAlbum = lvItem.Text;
                        this.DialogResult = DialogResult.OK;
                        Close();
                        return;
                    }
                }
            }

            this.DialogResult = DialogResult.Cancel;
            Close();

        }
    }
}
