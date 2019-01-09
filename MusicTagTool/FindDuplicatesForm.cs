using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalTagger
{
    public partial class FindDuplicatesForm : Form
    {
        private const int LISTVIEW_COLUMN_TITLE = 0;
        private const int LISTVIEW_COLUMN_ARTIST = 1;
        private const int LISTVIEW_COLUMN_ALBUM = 2;
        private const int LISTVIEW_COLUMN_FILENAME = 3;

        private bool songsDeleted = false;
        private BindingList<Id3Tag> listOfSongs = new BindingList<Id3Tag>();


        private Dictionary<int, Id3Tag> selectedForDeletion = new Dictionary<int, Id3Tag>();
        private List<int> deletedIndex = new List<int>();

        private List<Id3Tag> deletedSongs = new List<Id3Tag>();
        public List<Id3Tag> DeletedSongs
        {
            get { return deletedSongs; }
        }

        private BackgroundWorker _backgroundWorkerMetadata;
        delegate void InvokeToProgressLabelDelegate(string sText);
        private InvokeToProgressLabelDelegate _InvokeToProgressBarLabel = null;

        public FindDuplicatesForm(BindingList<Id3Tag> mainList)
        {
            InitializeComponent();

            listOfSongs = mainList;

            deletedSongs = new List<Id3Tag>();

            _backgroundWorkerMetadata = new BackgroundWorker();
            _backgroundWorkerMetadata.WorkerSupportsCancellation = true;
            _backgroundWorkerMetadata.DoWork += new DoWorkEventHandler(Bw_DoWork);
            _backgroundWorkerMetadata.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bw_RunWorkerCompleted);

            _InvokeToProgressBarLabel = new InvokeToProgressLabelDelegate(InvokeToProgressLabel);

        }

        private void FindDuplicatesForm_Load(object sender, EventArgs e)
        {
            ColumnHeader columnHeader = new ColumnHeader();
            columnHeader.Text = "Title";
            columnHeader.Width = 250;
            lvDuplicateSongs.Columns.Add(columnHeader);

            columnHeader = new ColumnHeader();
            columnHeader.Text = "Artist";
            columnHeader.Width = 200;
            lvDuplicateSongs.Columns.Add(columnHeader);

            columnHeader = new ColumnHeader();
            columnHeader.Text = "Album";
            columnHeader.Width = 200;
            lvDuplicateSongs.Columns.Add(columnHeader);

            columnHeader = new ColumnHeader();
            columnHeader.Text = "Filename";
            columnHeader.Width = 400;
            lvDuplicateSongs.Columns.Add(columnHeader);

            var sortedListInstance = listOfSongs.GroupBy(x => (x.Title.ToLower(), x.Artist.ToLower()))
                  .Where(x => x.Count() > 1)
                  .SelectMany(x => x);

            EnableControls(false);
            btnClose.Enabled = true;
            if( sortedListInstance.Any())
            {
                foreach (var song in sortedListInstance)
                {
                    ListViewItem lvSong = new ListViewItem();
                    lvSong.Checked = false;
                    lvSong.Text = song.Title;
                    lvSong.SubItems.Add(song.Artist);
                    lvSong.SubItems.Add(song.Album);
                    lvSong.SubItems.Add(song.Filepath);
                    lvSong.Tag = song;
                    lvDuplicateSongs.Items.Add(lvSong);
                }
                EnableControls(true);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                selectedForDeletion = new Dictionary<int, Id3Tag>();

                for (int item = 0; item < lvDuplicateSongs.Items.Count; item++)
                {
                    ListViewItem lvSong = lvDuplicateSongs.Items[item];
                    if (lvSong.Checked)
                    {
                        Id3Tag songToDelete = (Id3Tag)lvSong.Tag;

                        selectedForDeletion.Add(item, songToDelete);
                    }
                }

                EnableControls(false);
                lblProgress.Visible = true;
                _backgroundWorkerMetadata.RunWorkerAsync();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Look What Happened!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if( songsDeleted)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
            Close();
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            List<int> keys = new List<int>(selectedForDeletion.Keys);
            foreach (int key in keys)
            {
                try
                {
                    Id3Tag deleteThisSong = (Id3Tag)selectedForDeletion[key];
                    string songFile = deleteThisSong.Filepath;
                    InvokeToProgressLabel("Deleting Song - " + songFile);
                    if (File.Exists(songFile))
                    {
                        File.Delete(songFile);
                        deletedSongs.Add(deleteThisSong);
                        songsDeleted = true;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {

            }
            else if (!(e.Error == null))
            {

            }
            else
            {
                try
                {
                    List<int> keys = new List<int>(selectedForDeletion.Keys.Reverse());

                    foreach (int key in keys)
                    {
                        lvDuplicateSongs.Items[key].Remove();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Look What Happened!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            EnableControls(true);
            lblProgress.Visible = false;
        }

        private void EnableControls(bool bEnable)
        {
            lvDuplicateSongs.Enabled = bEnable;
            btnDelete.Enabled = bEnable;
            btnClose.Enabled = bEnable;
            chkTitleOnly.Enabled = bEnable;
        }

        private void InvokeToProgressLabel(string sText)
        {
            if (lblProgress.InvokeRequired)
            {
                this.Invoke(new InvokeToProgressLabelDelegate(InvokeToProgressLabel), sText);
                return;
            }

            lblProgress.Text = sText;
        }

    }
}
