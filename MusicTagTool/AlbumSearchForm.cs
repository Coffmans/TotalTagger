using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalTagger
{
    public partial class AlbumSearchForm : Form
    {
        private BindingList<Id3Tag> listAllSongs = new BindingList<Id3Tag>();
        public BindingList<Id3Tag> ListAllSongs
        {
            get { return listAllSongs; }
            set { listAllSongs = value; }
        }

        const int COLUMN_TITLE = 0;
        const int COLUMN_ARTIST = 1;
        const int COLUMN_ALBUM = 2;
        const int COLUMN_RETRIEVED_ALBUM = 3;

        //private List<AlbumMetadata> listSongsToSearch;
        private List<SongMetadataWithIndex> listSongsToSearch;

        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken cancellationToken = new CancellationToken();

        private BackgroundWorker _backgroundWorkerMetadata;
        delegate void InvokeToListViewDelegate(Id3Tag metaData, int nRow,bool updateExistingAlbum= false);
        private InvokeToListViewDelegate _InvokeToListView = null;
        delegate void InvokeToProgressLabelDelegate(string sText);
        private InvokeToProgressLabelDelegate _InvokeToProgressBarLabel = null;

        public Id3Tag SongMetadata { get; set; }

        [Flags]
        enum MetadataService
        {
            None = 0x0000,
            iTunes = 0x0001,
            Discogs = 0x0002,
            MusicBrainz = 0x0004,
            AudioDB = 0x0008,
            Deezer = 0x0010,
            LastFM = 0x0020,
            Spotify = 0x0040,
            Napster = 0x0080,
            Amazon = 0x0100
        }

        private MetadataService flagMetadataService = 0;

        private string songsDirectory = "";

        private bool tagsUpdated = false;

        public AlbumSearchForm(string directory)
        {
            InitializeComponent();

            _backgroundWorkerMetadata = new BackgroundWorker();
            //_backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorkerMetadata.WorkerSupportsCancellation = true;
            _backgroundWorkerMetadata.DoWork += new DoWorkEventHandler(Bw_DoWork);
            //_backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            _backgroundWorkerMetadata.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bw_RunWorkerCompleted);

            _InvokeToListView = new InvokeToListViewDelegate(InvokeToListView);

            _InvokeToProgressBarLabel = new InvokeToProgressLabelDelegate(InvokeToProgressLabel);

            songsDirectory = directory;

        }

        private void AlbumSearchForm_Load(object sender, EventArgs e)
        {
            lvSongsWithMissingAlbums.Clear();

            ColumnHeader columnHeader = new ColumnHeader();
            columnHeader.Text = "Title";
            columnHeader.Width = 250;
            lvSongsWithMissingAlbums.Columns.Add(columnHeader);

            columnHeader = new ColumnHeader();
            columnHeader.Text = "Artist";
            columnHeader.Width = 200;
            lvSongsWithMissingAlbums.Columns.Add(columnHeader);

            columnHeader = new ColumnHeader();
            columnHeader.Text = "Current Album";
            columnHeader.Width = 225;
            lvSongsWithMissingAlbums.Columns.Add(columnHeader);

            columnHeader = new ColumnHeader();
            columnHeader.Text = "Retrieved Album";
            columnHeader.Width = 225;
            lvSongsWithMissingAlbums.Columns.Add(columnHeader);

            foreach (var song in listAllSongs)
            {
                ListViewItem lvSong = new ListViewItem();
                lvSong.Checked = false;
                lvSong.Text = song.Title;
                lvSong.SubItems.Add(song.Artist);
                lvSong.SubItems.Add(song.Album);
                lvSong.SubItems.Add("");
                lvSong.Tag = song.Filepath;
                lvSongsWithMissingAlbums.Items.Add(lvSong);
            }

            rbITunes.Checked = true;
            btnSaveTags.Enabled = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // ...
            if (keyData == (Keys.Escape))
            {
                if (tagsUpdated)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }

                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void InvokeToListView(Id3Tag musicData, int nRow, bool updateExistingAlbum=false)
        {
            // If it's coming from another thread, Invoke _InvokeToListView trough the _InvokeToListViewDelegate and end this thing.
            if (lvSongsWithMissingAlbums.InvokeRequired)
            {
                this.Invoke(new InvokeToListViewDelegate(InvokeToListView), musicData, nRow, updateExistingAlbum);
                return;
            }

            ListViewItem lvSong = lvSongsWithMissingAlbums.Items[nRow];
            //lvSong.Checked = false;
            if(updateExistingAlbum)
                lvSong.SubItems[COLUMN_ALBUM].Text = musicData.Album;
            else
                lvSong.SubItems[COLUMN_RETRIEVED_ALBUM].Text = musicData.Album;
            lvSongsWithMissingAlbums.Items[nRow] = lvSong;
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

        private void btnSearchForAlbums_Click(object sender, EventArgs e)
        {
            int songs = 0;
            SetMetadataService();
            listSongsToSearch = new List<SongMetadataWithIndex>();
            for (int item=0; item < lvSongsWithMissingAlbums.Items.Count; item++)
            {
                ListViewItem lvSong = lvSongsWithMissingAlbums.Items[item];
                if( lvSong.Checked)
                {
                    SongMetadataWithIndex albumMetadata = new SongMetadataWithIndex();
                    albumMetadata.MusicMetatada.Title = lvSong.Text;
                    albumMetadata.MusicMetatada.Artist = lvSong.SubItems[COLUMN_ARTIST].Text;
                    albumMetadata.Index = item;
                    listSongsToSearch.Add(albumMetadata);
                    songs++;
                }

                if (songs >= 25)
                    break;
            }

            EnableControls(false);
            _backgroundWorkerMetadata.RunWorkerAsync();
        }

        private void RetreiveAlbumFromService()
        {

        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var song in listSongsToSearch)
            {
                if (flagMetadataService.HasFlag(MetadataService.iTunes))
                {
                    InvokeToProgressLabel("Retrieving Albums from iTunes for " + song.MusicMetatada.Title);
                    string result = GetAlbumsFromITunes(song);
                    InvokeToProgressLabel(result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Deezer))
                {
                    InvokeToProgressLabel("Retrieving Albums from Deezer for " + song.MusicMetatada.Title);
                    string result = GetAlbumsFromDeezer(song);
                    InvokeToProgressLabel(result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Napster))
                {
                    InvokeToProgressLabel("Retrieving Albums from Napster for " + song.MusicMetatada.Title);
                    string result = GetAlbumsFromNapster(song);
                    InvokeToProgressLabel(result);
                }
                //else if (flagMetadataService.HasFlag(MetadataService.MusicBrainz))
                //{
                //    InvokeToProgressLabel("Retrieving Albums from MusicBrainz for " + song.MusicMetatada.Title);
                //    string result = GetAlbumsFromMBAsync();
                //    InvokeToProgressLabel(result);
                //}
                else if (flagMetadataService.HasFlag(MetadataService.LastFM))
                {
                    InvokeToProgressLabel("Retrieving Albums from LastFM for " + song.MusicMetatada.Title);
                    string result = GetAlbumsFromLastFM(song);
                    InvokeToProgressLabel(result);
                }
                //else if (flagMetadataService.HasFlag(MetadataService.Spotify))
                //{
                //    InvokeToProgressLabel("Retrieving Albums from Spotify for " + song.MusicMetatada.Title);
                //    string result = GetAlbumsFromSpotifyAsync();
                //    InvokeToProgressLabel(result);
                //}
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
            }
            EnableControls(true);
            btnSearchForAlbums.Text = "Search";


            //lblProgress.Visible = false;
        }

        private void EnableControls(bool bEnable)
        {
            lvSongsWithMissingAlbums.Enabled = bEnable;
            btnCancel.Enabled = bEnable;
            btnSaveTags.Enabled = bEnable;
            rbITunes.Enabled = bEnable;
            rbLastFM.Enabled = bEnable;
            rbNapster.Enabled = bEnable;
            btnSaveTags.Enabled = bEnable;
        }

        private void SetMetadataService()
        {
            flagMetadataService = 0;
            if (rbITunes.Checked)
            {
                flagMetadataService |= MetadataService.iTunes;
            }
            else if (rbNapster.Checked)
            {
                flagMetadataService |= MetadataService.Napster;
            }
            else if (rbLastFM.Checked)
            {
                flagMetadataService |= MetadataService.LastFM;
            }
            else if (rbDeezer.Checked)
            {
                flagMetadataService |= MetadataService.Deezer;
            }
            //else if (chkMusicBrainz.Checked)
            //{
            //    flagMetadataService |= MetadataService.MusicBrainz;
            //}
            //else if (chkSpotify.Checked)
            //{
            //    flagMetadataService |= MetadataService.Spotify;
            //}
        }


        private string GetAlbumsFromITunes(SongMetadataWithIndex searchForAlbum)
        {
            ITunes queryITunes = new ITunes();
            try
            {
                queryITunes.ExistingMetadata = searchForAlbum.MusicMetatada;
                queryITunes.QueryForMetadataNonAsync(cancellationToken, true);
                if (queryITunes.ResultOfQuery)
                {
                    searchForAlbum.MusicMetatada = queryITunes.RetrievedMetadata;
                    InvokeToListView(searchForAlbum.MusicMetatada, searchForAlbum.Index);

                    return "Results Returned from iTunes";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return queryITunes.QueryResult;
        }


        private string GetAlbumsFromDeezer(SongMetadataWithIndex searchForAlbum)
        {

            Deezer queryDeezer = new Deezer();
            queryDeezer.ExistingMetadata = searchForAlbum.MusicMetatada;
            queryDeezer.QueryForMetadataNonAsync(cancellationToken, true);
            if (queryDeezer.ResultOfQuery)
            {
                searchForAlbum.MusicMetatada = queryDeezer.RetrievedMetadata;
                InvokeToListView(searchForAlbum.MusicMetatada, searchForAlbum.Index);
                return "Results Returned from Deezer";
            }
            return queryDeezer.QueryResult;
        }

        private string GetAlbumsFromLastFM(SongMetadataWithIndex searchForAlbum)
        {
            LastFM queryLastFM = new LastFM();
            queryLastFM.ExistingMetadata = searchForAlbum.MusicMetatada;
            queryLastFM.QueryForMetadataNonAsync(cancellationToken, 1);
            if (queryLastFM.ResultOfQuery)
            {
                searchForAlbum.MusicMetatada = queryLastFM.RetrievedMetadata;
                InvokeToListView(searchForAlbum.MusicMetatada, searchForAlbum.Index);
                return "Results Returned from LastFM";
            }
            return queryLastFM.QueryResult;
        }

        private string GetAlbumsFromNapster(SongMetadataWithIndex searchForAlbum)
        {
            Napster queryNapster = new Napster();
            queryNapster.ExistingMetadata = searchForAlbum.MusicMetatada;
            queryNapster.QueryForMetadataNonAsync(cancellationToken, true, 1);
            if (queryNapster.ResultOfQuery)
            {
                searchForAlbum.MusicMetatada = queryNapster.RetrievedMetadata;
                InvokeToListView(searchForAlbum.MusicMetatada, searchForAlbum.Index);
                return "Results Returned from Napster";
            }
            return queryNapster.QueryResult;
        }

        private void btnSaveTags_Click(object sender, EventArgs e)
        {
            SongMetadata = new Id3Tag();
            for (int item = 0; item < lvSongsWithMissingAlbums.Items.Count; item++)
            {
                ListViewItem lvSong = lvSongsWithMissingAlbums.Items[item];
                if (lvSong.Checked)
                {
                    SongMetadata = new Id3Tag();
                    SongMetadata.Album = lvSong.SubItems[COLUMN_RETRIEVED_ALBUM].Text;

                    if( ReadWriteID3.WriteID3Tags(lvSong.Tag.ToString(), SongMetadata).Equals("Success") )
                    {
                        tagsUpdated = true;
                        listAllSongs[item].Album = SongMetadata.Album;
                        InvokeToListView(listAllSongs[item], item, true);
                        InvokeToProgressLabel("ID3 Tags Updated for " + lvSong.Tag.ToString());
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if( tagsUpdated)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }

            Close();
        }
    }

    public class SongMetadataWithIndex
    {
        public Id3Tag MusicMetatada { get; set; }
        public int Index { get; set; }

        public SongMetadataWithIndex()
        {
            MusicMetatada = new Id3Tag();
        }
    }
}
