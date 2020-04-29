using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParkSquare.Gracenote;
using DiscogsClient;
using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using System.Net;
using DiscogsClient.Internal;
using MetaBrainz.MusicBrainz;
using System.Reactive.Linq;
using System.Web;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Threading;

namespace TotalTagger
{
    public partial class DisplayMetadataOnAllMatches : Form
    {
        private const int GRID_RETRIEVED_TITLE      = 0;
        private const int GRID_RETRIEVED_ARTIST     = 1;
        private const int GRID_RETRIEVED_ALBUM      = 2;
        private const int GRID_RETRIEVED_GENRE      = 3;
        //private const string GRID_RETRIEVED_ARTWORK = "Artwork";

        private BackgroundWorker _backgroundWorkerMetadata;
        delegate void InvokeToDataGridDelegate(Id3Tag metaData, int nRow);
        private InvokeToDataGridDelegate _InvokeToDataGrid = null;
        delegate void InvokeToProgressLabelDelegate(string sText);
        private InvokeToProgressLabelDelegate _InvokeToProgressBarLabel = null;

        delegate void InvokeToDataLabelDelegate(string text);

        private BindingList<Id3Tag> listRetrievedMetadata;

        private string songFileName = "";
        public Id3Tag gSongMetaData { get; set; }
        public string gOnlineSource { get; set; }

        public Id3Tag songToSearch { get; set; }

        private int selectedIndex = -1;

        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken cancellationToken = new CancellationToken();

        List<AlbumMetadata> AlbumsForArtist = new List<AlbumMetadata>();
        //Id3Tag _TempMetadata = new Id3Tag();
        AlbumMetadata gAlbumMetdata = new AlbumMetadata();

        private MetadataService flagMetadataService = 0;
        private QueryForMetadata flagQueryForData = 0;

        public DisplayMetadataOnAllMatches(string fileName, Id3Tag musicData)
        {
            InitializeComponent();

            songFileName = fileName;
            gSongMetaData = musicData;

            _backgroundWorkerMetadata = new BackgroundWorker();
            //_backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorkerMetadata.WorkerSupportsCancellation = true;
            _backgroundWorkerMetadata.DoWork += new DoWorkEventHandler(Bw_DoWork);
            //_backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            _backgroundWorkerMetadata.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bw_RunWorkerCompleted);

            _InvokeToDataGrid = new InvokeToDataGridDelegate(InvokeToDataGrid);

            _InvokeToProgressBarLabel = new InvokeToProgressLabelDelegate(InvokeToProgressLabel);

            rbDeezer.Checked = true;
        }
        #region Main UI
        private void DisplayMetadataOnAllMatches_Load(object sender, EventArgs e)
        {
            dataGridAllResults.ColumnCount = 4;
            dataGridAllResults.Columns[GRID_RETRIEVED_TITLE].Name = "Title";
            dataGridAllResults.Columns[GRID_RETRIEVED_TITLE].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridAllResults.Columns[GRID_RETRIEVED_ARTIST].Name = "Artist";
            dataGridAllResults.Columns[GRID_RETRIEVED_ARTIST].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridAllResults.Columns[GRID_RETRIEVED_ALBUM].Name = "Album";
            dataGridAllResults.Columns[GRID_RETRIEVED_ALBUM].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridAllResults.Columns[GRID_RETRIEVED_GENRE].Name = "Genre";
            dataGridAllResults.Columns[GRID_RETRIEVED_GENRE].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridAllResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridAllResults.Enabled = false;

            this.Text = gSongMetaData.Title + " by " + gSongMetaData.Artist;

            try
            {
                txtExistingTitle.Text = gSongMetaData.Title;
                txtExistingArtist.Text = gSongMetaData.Artist;
                txtExistingAlbum.Text = gSongMetaData.Album;
                txtExistingGenre.Text = gSongMetaData.Genre;
                txtExistingReleaseDate.Text = gSongMetaData.Date;
                if (gSongMetaData.Cover.Image != null)
                    picExistingArt.Image = gSongMetaData.Cover.Image;

                chkGetTitle.Checked = chkGetArtist.Checked = true;
                if (_backgroundWorkerMetadata.IsBusy)
                {
                    return;
                }

                dataGridAllResults.Enabled = false;
                EnableRetrievedMetadataControls(false);
                dataGridAllResults.DataSource = null;
                dataGridAllResults.Rows.Clear();
                dataGridAllResults.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            EnableServiceRadioButtons();

        }

        private void EnableServiceRadioButtons()
        {
            rbDiscogs.Enabled = (MainWindow.serviceSettings.DiscogsClientID.Length > 0);
            rbGaliboo.Enabled = (MainWindow.serviceSettings.GalibooClientKey.Length > 0);
            rbGenius.Enabled = (MainWindow.serviceSettings.GeniusClientKey.Length > 0);
            rbLastFM.Enabled = (MainWindow.serviceSettings.LastFMClientKey.Length > 0);
            rbMusixMatch.Enabled = (MainWindow.serviceSettings.MusixMatchKey.Length > 0);
            rbNapster.Enabled = (MainWindow.serviceSettings.NapsterClientKey.Length > 0);
            rbSpotify.Enabled = (MainWindow.serviceSettings.SpotifyClientID.Length > 0);
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

        #endregion
        #region Button Presses
        private void btnSearchForAlbum_Click(object sender, EventArgs e)
        {
            if (_backgroundWorkerMetadata.IsBusy)
            {
                return;
            }

            if (!VerifyOptionsChecked())
            {
                return;
            }

            dataGridAllResults.Enabled = false;

            EnableControls(false);
            EnableExistingMetadataControls(false);
            EnableRetrievedMetadataControls(false);
            EnableSearchMetadataControls(false);
            EnableSearchAlbumsControls(false);
            EnableSearchArtworkControls(false);

            dataGridAllResults.DataSource = null;
            dataGridAllResults.Rows.Clear();
            dataGridAllResults.Refresh();

            flagMetadataService = 0;
            SetMetadataService();

            flagQueryForData = 0;
            flagQueryForData |= QueryForMetadata.Albums;

            songToSearch = new Id3Tag();
            if (chkGetTitle.Checked)
                songToSearch.Title = txtExistingTitle.Text;

            if (chkGetArtist.Checked)
                songToSearch.Artist = txtExistingArtist.Text;

            if (chkGetAlbum.Checked)
                songToSearch.Album = txtExistingAlbum.Text;

            _backgroundWorkerMetadata.RunWorkerAsync();
        }

        private void btnSearchForArtwork_Click(object sender, EventArgs e)
        {
            if (_backgroundWorkerMetadata.IsBusy)
            {
                return;
            }

            if (!VerifyOptionsChecked())
            {
                return;
            }

            if (txtExistingAlbum.Text.Length == 0 || txtExistingArtist.Text.Length == 0)
            {
                MessageBox.Show("Search for Artwork Requires Artist and Album!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dataGridAllResults.Enabled = false;

            flagMetadataService = 0;
            SetMetadataService(QueryForMetadata.Artwork);
            if( flagMetadataService == 0 )
            {
                MessageBox.Show("This services does not support the retrieval of Cover Art!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            flagQueryForData = 0;
            flagQueryForData |= QueryForMetadata.Artwork;


            EnableControls(false);
            EnableExistingMetadataControls(false);
            EnableRetrievedMetadataControls(false);

            EnableSearchMetadataControls(false);
            EnableSearchAlbumsControls(false);
            EnableSearchArtworkControls(false);

            dataGridAllResults.DataSource = null;
            dataGridAllResults.Rows.Clear();
            dataGridAllResults.Refresh();

            songToSearch = new Id3Tag();
            songToSearch.Title = txtExistingTitle.Text;
            songToSearch.Artist = txtExistingArtist.Text;
            songToSearch.Album = txtExistingAlbum.Text;

            _backgroundWorkerMetadata.RunWorkerAsync();
        }

        private void BtnUseSelectedData_Click(object sender, EventArgs e)
        {
            if (txtRetrievedTitle.Text.Length == 0 &&
                txtRetrievedArtist.Text.Length == 0 &&
                txtRetrievedGenre.Text.Length == 0 &&
                txtRetrievedAlbum.Text.Length == 0 &&
                txtRetrievedYear.Text.Length == 0 &&
                String.IsNullOrEmpty(picRetrievedArt.ImageLocation))
            {
                this.DialogResult = DialogResult.Cancel;
                Close();
            }

            try
            {
                gSongMetaData.Title = txtRetrievedTitle.Text.Length > 0 ? txtRetrievedTitle.Text : gSongMetaData.Title;
                gSongMetaData.Artist = txtRetrievedArtist.Text.Length > 0 ? txtRetrievedArtist.Text : gSongMetaData.Artist;
                gSongMetaData.Genre = txtRetrievedGenre.Text.Length > 0 ? txtRetrievedGenre.Text : gSongMetaData.Genre;

                if ( txtRetrievedYear.Text.Length > 4)
                    gSongMetaData.Date = DateTime.Parse(txtRetrievedYear.Text).Year.ToString();
                else if( txtRetrievedYear.Text.Length ==4 )
                {
                    gSongMetaData.Date = txtRetrievedYear.Text;
                }
                gSongMetaData.Album = txtRetrievedAlbum.Text.Length > 0 ? txtRetrievedAlbum.Text : gSongMetaData.Album;
                gSongMetaData.Cover = !String.IsNullOrEmpty(picRetrievedArt.ImageLocation) ? picRetrievedArt : gSongMetaData.Cover;
                this.DialogResult = DialogResult.OK;
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnQueryForMetadata_Click(object sender, EventArgs e)
        {
            if (_backgroundWorkerMetadata.IsBusy)
            {
                source.Cancel();
                _backgroundWorkerMetadata.CancelAsync();
                btnQueryForMetadata.Text = "Query for Metadata";
            }
            else
            {
                StartSearchForMetadata();
            }
        }

        #endregion

        #region BackGroundWorker
        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            listRetrievedMetadata = new BindingList<Id3Tag>();
            if (flagQueryForData.HasFlag(QueryForMetadata.Metadata))
            {
                //listRetrievedMetadata = new BindingList<Id3Tag>();
                SearchServiceForMetadata searchForMetadata = new SearchServiceForMetadata();
                searchForMetadata.ListRetrievedMetadata = listRetrievedMetadata;

                searchForMetadata.Limit = 25;
                if (flagMetadataService.HasFlag(MetadataService.MusicBrainz))
                {
                    InvokeToProgressLabel("Searching MusicBrainz for " + songToSearch.Title);
                    searchForMetadata.CallServiceForMetadata(MetadataService.MusicBrainz, songToSearch);
                    InvokeToProgressLabel(searchForMetadata.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.LastFM))
                {
                    InvokeToProgressLabel("Searching LastFM for " + songToSearch.Title);
                    searchForMetadata.CallServiceForMetadata(MetadataService.LastFM, songToSearch);
                    InvokeToProgressLabel(searchForMetadata.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Discogs))
                {
                    InvokeToProgressLabel("Searching Discogs for " + songToSearch.Title);
                    searchForMetadata.CallServiceForMetadata(MetadataService.Discogs, songToSearch);
                    InvokeToProgressLabel(searchForMetadata.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Spotify))
                {
                    InvokeToProgressLabel("Searching Spotify for " + songToSearch.Title);
                    searchForMetadata.CallServiceForMetadata(MetadataService.Spotify, songToSearch);
                    InvokeToProgressLabel(searchForMetadata.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.iTunes))
                {
                    InvokeToProgressLabel("Searching Spotify for " + songToSearch.Title);
                    searchForMetadata.CallServiceForMetadata(MetadataService.iTunes, songToSearch);
                    InvokeToProgressLabel(searchForMetadata.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Deezer))
                {
                    InvokeToProgressLabel("Searching Deezer for " + songToSearch.Title);
                    searchForMetadata.CallServiceForMetadata(MetadataService.Deezer, songToSearch);
                    InvokeToProgressLabel(searchForMetadata.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.MusixMatch))
                {
                    InvokeToProgressLabel("Searching MusixMatch for " + songToSearch.Title);
                    searchForMetadata.CallServiceForMetadata(MetadataService.MusixMatch, songToSearch);
                    InvokeToProgressLabel(searchForMetadata.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Galiboo))
                {
                    InvokeToProgressLabel("Searching Galiboo for " + songToSearch.Title);
                    searchForMetadata.CallServiceForMetadata(MetadataService.Galiboo, songToSearch);
                    InvokeToProgressLabel(searchForMetadata.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Napster))
                {
                    InvokeToProgressLabel("Searching Napster for " + songToSearch.Title);
                    searchForMetadata.CallServiceForMetadata(MetadataService.Napster, songToSearch);
                    InvokeToProgressLabel(searchForMetadata.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Genius))
                {
                    InvokeToProgressLabel("Searching Genius for " + songToSearch.Title);
                    searchForMetadata.CallServiceForMetadata(MetadataService.Genius, songToSearch);
                    InvokeToProgressLabel(searchForMetadata.Result);
                }

                LoadResultsIntoGrid();
                InvokeToProgressLabel("Search " + (listRetrievedMetadata.Count > 0 ? "Completed!" : "Found No Matches!"));
            }
            else if (flagQueryForData.HasFlag(QueryForMetadata.Artwork))
            {
                Id3Tag songSelected = songToSearch;
                SearchServiceForArtwork searchForArtwork = new SearchServiceForArtwork();
//                searchForArtwork.ListRetrievedMetadata = listRetrievedMetadata;

                if (flagMetadataService.HasFlag(MetadataService.LastFM))
                {
                    InvokeToProgressLabel("Retrieving Artwork from LastFM for " + songToSearch.Artist);
                    searchForArtwork.CallServiceForArtwork(MetadataService.LastFM, songToSearch);
                    songToSearch = searchForArtwork.gSongMetaData;
                    InvokeToProgressLabel(searchForArtwork.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.iTunes))
                {
                    InvokeToProgressLabel("Retrieving Artwork from iTunes for " + songToSearch.Artist);
                    searchForArtwork.CallServiceForArtwork(MetadataService.iTunes, songToSearch);
                    songToSearch = searchForArtwork.gSongMetaData;
                    InvokeToProgressLabel(searchForArtwork.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Deezer))
                {
                    InvokeToProgressLabel("Retrieving Artwork from Deezer for " + songToSearch.Artist);
                    searchForArtwork.CallServiceForArtwork(MetadataService.Deezer, songToSearch);
                    songToSearch = searchForArtwork.gSongMetaData;
                    InvokeToProgressLabel(searchForArtwork.Result);
                }
                else
                {
                    InvokeToProgressLabel("Retrieving Artwork from Spotify for " + songToSearch.Artist);
                    searchForArtwork.CallServiceForArtwork(MetadataService.Spotify, songToSearch);
                    songToSearch = searchForArtwork.gSongMetaData;
                    InvokeToProgressLabel(searchForArtwork.Result);
                }
                //LoadResultsIntoGrid();
                //InvokeToProgressLabel("Search " + (listRetrievedMetadata.Count > 0 ? "Completed!" : "Found No Matches!"));
            }
            else if (flagQueryForData.HasFlag(QueryForMetadata.Albums))
            {
                AlbumsForArtist = new List<AlbumMetadata>();
                Id3Tag songSelected = songToSearch;
                SearchServiceForAlbum searchForAlbum= new SearchServiceForAlbum();
                searchForAlbum.AlbumsForArtist = AlbumsForArtist;

                if (flagMetadataService.HasFlag(MetadataService.iTunes))
                {
                    InvokeToProgressLabel("Retrieving Albums from iTunes for " + songToSearch.Artist);
                    searchForAlbum.CallServiceForAlbum(MetadataService.iTunes, songToSearch);
                    InvokeToProgressLabel(searchForAlbum.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Deezer))
                {
                    InvokeToProgressLabel("Retrieving Albums from MusicBrainz for " + songToSearch.Artist);
                    searchForAlbum.CallServiceForAlbum(MetadataService.Deezer, songToSearch);
                    InvokeToProgressLabel(searchForAlbum.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Napster))
                {
                    InvokeToProgressLabel("Retrieving Albums from MusicBrainz for " + songToSearch.Artist);
                    searchForAlbum.CallServiceForAlbum(MetadataService.Napster, songToSearch);
                    InvokeToProgressLabel(searchForAlbum.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.MusicBrainz))
                {
                    InvokeToProgressLabel("Retrieving Albums from MusicBrainz for " + songToSearch.Artist);
                    searchForAlbum.CallServiceForAlbum(MetadataService.MusicBrainz, songToSearch);
                    InvokeToProgressLabel(searchForAlbum.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.LastFM))
                {
                    InvokeToProgressLabel("Retrieving Albums from LastFM for " + songToSearch.Artist);
                    searchForAlbum.CallServiceForAlbum(MetadataService.LastFM, songToSearch);
                    InvokeToProgressLabel(searchForAlbum.Result);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Spotify))
                {
                    InvokeToProgressLabel("Retrieving Albums from Spotify for " + songToSearch.Artist);
                    searchForAlbum.CallServiceForAlbum(MetadataService.Spotify, songToSearch);
                    InvokeToProgressLabel(searchForAlbum.Result);
                }
                LoadResultsIntoGrid();
                InvokeToProgressLabel("Search " + (listRetrievedMetadata.Count > 0 ? "Completed!" : "Found No Matches!"));
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
                dataGridAllResults.Enabled = true;
                EnableControls(true);
                EnableExistingMetadataControls(true);
                EnableSearchMetadataControls(true);
                EnableSearchAlbumsControls(true);
                EnableSearchArtworkControls(true);
                EnableServiceRadioButtons();

                if (flagQueryForData.HasFlag(QueryForMetadata.Artwork))
                {
                    if (songToSearch != null && songToSearch.Cover != null && !String.IsNullOrEmpty(songToSearch.Cover.ImageLocation))
                    {
                        picRetrievedArt.ImageLocation = songToSearch.Cover.ImageLocation;
                        var request = WebRequest.Create(songToSearch.Cover.ImageLocation);

                        using (var response = request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        {
                            System.Drawing.Image currentImage = Bitmap.FromStream(stream);
                            picRetrievedArt.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                            EnableRetrievedMetadataControls(true);
                        }
                    }
                }
                else if (flagQueryForData.HasFlag(QueryForMetadata.Albums))
                {
                    if (AlbumsForArtist.Any())
                    {
                        ArtistAlbumsForm albumsForm = new ArtistAlbumsForm(AlbumsForArtist, songToSearch.Artist);
                        if (DialogResult.OK == albumsForm.ShowDialog())
                        {
                            txtRetrievedAlbum.Text = albumsForm.ReturnAlbum;
                            EnableRetrievedMetadataControls(true);
                        }
                    }
                }
            }
            btnQueryForMetadata.Text = "Search";


            //lblProgress.Visible = false;
        }

        #endregion

        // private void RetrievedMetaDataFromGraceNote()
        //{
        //    var client = new ParkSquare.Gracenote.GracenoteClient("712956455-629D246656DAF564E93DE72F0153A5D2");

        //    InvokeToProgressLabel("Retrieving Metadata for " + songFileName);

        //    var x = client.Search(new ParkSquare.Gracenote.SearchCriteria
        //    {
        //        TrackTitle = gSongMetaData.Title,
        //        SearchMode = SearchMode.BestMatch,
        //    });


        //    Id3Tag Id3Tag = gSongMetaData;

        //    if (x != null && x.Count > 0)
        //    {
        //        listRetrievedMetadata = new BindingList<Id3Tag>();
        //        foreach ( var song in x.Albums)
        //        {
        //            Id3Tag.Title = song.Tracks.First().Title;
        //            Id3Tag.Artist = song.Artist;
        //            Id3Tag.Album = song.Title;
        //            if (song.Genre.Any())
        //                Id3Tag.Genre = song.Genre.First().ToString();
        //            Id3Tag.Date = song.Year.ToString();

        //            if (song.Artwork.Any())
        //            {
        //                string imageLocation = song.Artwork.First().Uri.ToString();
        //                Id3Tag.Cover.ImageLocation = song.Artwork.First().Uri.ToString();
        //            }

        //            listRetrievedMetadata.Add(Id3Tag);
        //            InvokeToDataGrid(Id3Tag, -1);
        //        }
        //   }
        //}

        #region UIManipulation
        private void LoadResultsIntoGrid()
        {
            if (listRetrievedMetadata.Any())
            {
                foreach (var metadata in listRetrievedMetadata)
                {
                    InvokeToDataGrid(metadata, -1);
                }
            }
        }

        private void EnableControls(bool bEnable)
        {
            groupBox2.Enabled = bEnable;
            dataGridAllResults.Enabled = bEnable;
        }

        private void EnableExistingMetadataControls(bool bEnable)
        {
            txtExistingTitle.Enabled = bEnable;
            txtExistingArtist.Enabled = bEnable;
            txtExistingAlbum.Enabled = bEnable;
            txtExistingGenre.Enabled = bEnable;
            txtExistingReleaseDate.Enabled = bEnable;
            picExistingArt.Enabled = bEnable;
            chkGetAlbum.Enabled = bEnable;
            chkGetArtist.Enabled = bEnable;
        }
        private void EnableRetrievedMetadataControls(bool bEnable)
        {
            txtRetrievedTitle.Enabled = bEnable;
            txtRetrievedArtist.Enabled = bEnable;
            txtRetrievedAlbum.Enabled = bEnable;
            txtRetrievedGenre.Enabled = bEnable;
            txtRetrievedYear.Enabled = bEnable;
            picRetrievedArt.Enabled = bEnable;

            btnUseSelectedData.Enabled = bEnable;
        }


        private void EnableSearchMetadataControls(bool bEnable)
        {
            rbSpotify.Enabled = rbDeezer.Enabled = rbNapster.Enabled = rbMusicBrainz.Enabled = rbLastFM.Enabled = rbITunes.Enabled = rbDiscogs.Enabled = bEnable = rbMusixMatch.Enabled = bEnable;
        }

        private void EnableSearchAlbumsControls(bool bEnable)
        {
            rbSpotify.Enabled = rbDeezer.Enabled = rbNapster.Enabled = rbMusicBrainz.Enabled = rbLastFM.Enabled = rbITunes.Enabled = rbDiscogs.Enabled = bEnable;
            btnSearchForAlbum.Enabled = bEnable;
        }

        private void EnableSearchArtworkControls(bool bEnable)
        {
            rbSpotify.Enabled = rbDeezer.Enabled = rbNapster.Enabled = rbMusicBrainz.Enabled = rbLastFM.Enabled = rbITunes.Enabled = rbDiscogs.Enabled = rbMusixMatch.Enabled = bEnable;
            btnSearchForArtwork.Enabled = bEnable;
        }

        private void InvokeToDataGrid(Id3Tag musicData, int nRow)
        {
            // If it's coming from another thread, Invoke _InvokeToListView trough the _InvokeToListViewDelegate and end this thing.
            if (dataGridAllResults.InvokeRequired)
            {
                this.Invoke(new InvokeToDataGridDelegate(InvokeToDataGrid), musicData, nRow);
                return;
            }

            if (nRow < 0)
            {
                dataGridAllResults.Rows.Add(musicData.Title,
                                            musicData.Artist,
                                            musicData.Album,
                                            musicData.Genre
                                            );
            }
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

        #endregion

        private void DataGridAllResults_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int nItems = dataGridAllResults.Rows.Count;

                if (nItems > 0)
                {
                    selectedIndex = dataGridAllResults.CurrentRow.Index;
                    Id3Tag metaData = listRetrievedMetadata[selectedIndex];

                    //Load the Retrieved ID3 Metadata into the specific fields
                    txtRetrievedTitle.Text = metaData.Title;
                    txtRetrievedArtist.Text = metaData.Artist;
                    txtRetrievedYear.Text = DateTime.Parse(metaData.Date).Year.ToString();
                    txtRetrievedAlbum.Text = metaData.Album;
                    txtRetrievedGenre.Text = metaData.Genre;

                    picRetrievedArt.Image = null;
                    if (metaData.Cover != null && !String.IsNullOrEmpty(metaData.Cover.ImageLocation))
                    {
                        var request = WebRequest.Create(metaData.Cover.ImageLocation);

                        using (var response = request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        {
                            System.Drawing.Image currentImage = Bitmap.FromStream(stream);
                            picRetrievedArt.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridAllResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int nItems = dataGridAllResults.Rows.Count;

                if (nItems > 0)
                {
                    selectedIndex = dataGridAllResults.CurrentRow.Index;
                    Id3Tag metaData = listRetrievedMetadata[selectedIndex];

                    //Load the Retrieved ID3 Metadata into the specific fields
                    txtRetrievedTitle.Text = metaData.Title;
                    txtRetrievedArtist.Text = metaData.Artist;
                    if( !String.IsNullOrEmpty(metaData.Date) )
                    {
                        txtRetrievedYear.Text = DateTime.Parse(metaData.Date).Year.ToString();
                    }
                        
                    txtRetrievedAlbum.Text = metaData.Album;
                    txtRetrievedGenre.Text = metaData.Genre;

                    picRetrievedArt.Image = null;
                    if (metaData.Cover != null && !String.IsNullOrEmpty(metaData.Cover.ImageLocation))
                    {
                        var request = WebRequest.Create(metaData.Cover.ImageLocation);

                        using (var response = request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        {
                            System.Drawing.Image currentImage = Bitmap.FromStream(stream);
                            picRetrievedArt.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                            picRetrievedArt.ImageLocation = metaData.Cover.ImageLocation;
                        }
                    }

                    EnableRetrievedMetadataControls(true);

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayMetadataOnAllMatches_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        private void StartSearchForMetadata()
        {
            try
            {
                if(!VerifyOptionsChecked())
                {
                    return;
                }

                source = new CancellationTokenSource();
                cancellationToken = source.Token;

                if (_backgroundWorkerMetadata.IsBusy)
                {
                    return;
                }

                if (!chkGetTitle.Checked && !chkGetArtist.Checked && !chkGetAlbum.Checked)
                {
                    MessageBox.Show("Search of service requires title, artist, and/or album!", "Unable to Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                songToSearch = new Id3Tag();
                if (chkGetTitle.Checked)
                    songToSearch.Title = txtExistingTitle.Text;

                if (chkGetArtist.Checked)
                    songToSearch.Artist = txtExistingArtist.Text;

                if (chkGetAlbum.Checked)
                    songToSearch.Album = txtExistingAlbum.Text;

                flagMetadataService = 0;
                if (rbDiscogs.Checked)
                {
                    flagMetadataService |= MetadataService.Discogs;
                }

                if (rbITunes.Checked)
                {
                    flagMetadataService |= MetadataService.iTunes;
                }

                if (rbNapster.Checked)
                {
                    flagMetadataService |= MetadataService.Napster;
                }

                if (rbDeezer.Checked)
                {
                    flagMetadataService |= MetadataService.Deezer;
                }

                if (rbMusixMatch.Checked)
                {
                    flagMetadataService |= MetadataService.MusixMatch;
                }

                if (rbGaliboo.Checked)
                {
                    flagMetadataService |= MetadataService.Galiboo;
                }

                if (rbGenius.Checked)
                {
                    flagMetadataService |= MetadataService.Genius;
                }

                if (rbLastFM.Checked)
                {
                    if (songToSearch.Artist == null || songToSearch.Artist.Length == 0)
                    {
                        MessageBox.Show("Search with LastFM Get requires Title and Artist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    flagMetadataService |= MetadataService.LastFM;
                }

                if (rbMusicBrainz.Checked)
                {
                    flagMetadataService |= MetadataService.MusicBrainz;
                }

                if (rbSpotify.Checked)
                {
                    flagMetadataService |= MetadataService.Spotify;
                }

                dataGridAllResults.Enabled = false;

                EnableControls(false);
                EnableExistingMetadataControls(false);
                EnableRetrievedMetadataControls(false);
                EnableSearchMetadataControls(false);
                EnableSearchAlbumsControls(false);
                EnableSearchArtworkControls(false);
                dataGridAllResults.DataSource = null;
                dataGridAllResults.Rows.Clear();
                dataGridAllResults.Refresh();

                flagQueryForData = 0;
                flagQueryForData |= QueryForMetadata.Metadata;

                songToSearch = new Id3Tag();
                if (chkGetTitle.Checked)
                    songToSearch.Title = txtExistingTitle.Text;

                if (chkGetArtist.Checked)
                    songToSearch.Artist = txtExistingArtist.Text;

                if (chkGetAlbum.Checked)
                    songToSearch.Album = txtExistingAlbum.Text;

                _backgroundWorkerMetadata.RunWorkerAsync();

                btnQueryForMetadata.Text = "Stop Search";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Fetch Functions
        //private string RetrieveMetadataFromMusicBrainz(Id3Tag songToSearch)
        //{
        //    MusicBrainz queryMusicBrainz = new MusicBrainz();
        //    queryMusicBrainz.ExistingMetadata = songToSearch;
        //    queryMusicBrainz.ListRetrievedTags = listRetrievedMetadata;
        //    queryMusicBrainz.QueryForMetadataNonAsync(cancellationToken);
        //    if (queryMusicBrainz.ResultOfQuery)
        //        return "Results Returned from MusicBrainz";

        //    return queryMusicBrainz.QueryResult;
        //}

        //private string RetrieveMetadataFromNapster(Id3Tag songToSearch)
        //{
        //    Napster queryNapster = new Napster();
        //    queryNapster.ExistingMetadata = songToSearch;
        //    queryNapster.ListRetrievedTags = listRetrievedMetadata;
        //    queryNapster.QueryForMetadataNonAsync(cancellationToken);
        //    if (queryNapster.ResultOfQuery)
        //        return "Results Returned from Napster";
        //    return queryNapster.QueryResult;
        //}

        //private string RetrieveMetadataFromDeezer(Id3Tag songToSearch)
        //{

        //    Deezer queryDeezer = new Deezer();
        //    queryDeezer.ExistingMetadata = songToSearch;
        //    queryDeezer.ListRetrievedTags = listRetrievedMetadata;
        //    queryDeezer.QueryForMetadataNonAsync(cancellationToken, false);
        //    if (queryDeezer.ResultOfQuery)
        //        return "Results Returned from Deezer";
        //    return queryDeezer.QueryResult;
        //}

        //private string RetrieveMetadataFromMusixMatch(Id3Tag songToSearch)
        //{

        //    MusixMatch queryMusixMatch = new MusixMatch();
        //    queryMusixMatch.ExistingMetadata = songToSearch;
        //    queryMusixMatch.ListRetrievedTags = listRetrievedMetadata;
        //    queryMusixMatch.QueryForMetadataNonAsync(cancellationToken, false);
        //    if (queryMusixMatch.ResultOfQuery)
        //        return "Results Returned from MusixMatch";
        //    return queryMusixMatch.QueryResult;
        //}

        //private string RetrieveMetadataFromGaliboo(Id3Tag songToSearch)
        //{

        //    Galiboo queryGaliboo = new Galiboo();
        //    queryGaliboo.ExistingMetadata = songToSearch;
        //    queryGaliboo.ListRetrievedTags = listRetrievedMetadata;
        //    queryGaliboo.QueryForMetadataNonAsync(cancellationToken, false);
        //    if (queryGaliboo.ResultOfQuery)
        //        return "Results Returned from Galiboo";
        //    return queryGaliboo.QueryResult;
        //}

        //private string RetrieveMetadataFromItunes(Id3Tag songToSearch)
        //{
        //    ITunes queryITunes = new ITunes();
        //    queryITunes.ExistingMetadata = songToSearch;
        //    queryITunes.ListRetrievedTags = listRetrievedMetadata;
        //    queryITunes.QueryForMetadataNonAsync(cancellationToken, false);
        //    if (queryITunes.ResultOfQuery)
        //        return "Results Returned from iTunes";
        //    return queryITunes.QueryResult;
        //}

        //private string RetrieveMetadataFromLastFM(Id3Tag songToSearch)
        //{
        //    LastFM queryLastFM = new LastFM();
        //    queryLastFM.ExistingMetadata = songToSearch;
        //    queryLastFM.ListRetrievedTags = listRetrievedMetadata;
        //    queryLastFM.QueryForMetadataNonAsync(cancellationToken);
        //    if (queryLastFM.ResultOfQuery)
        //        return "Results Returned from LastFM";

        //    return queryLastFM.QueryResult;
        //}

        //private string RetrieveMetadataFromDiscogs(Id3Tag songToSearch)
        //{
        //    Discogs queryDiscogs = new Discogs();
        //    queryDiscogs.ExistingMetadata = songToSearch;
        //    queryDiscogs.ListRetrievedTags = listRetrievedMetadata;
        //    queryDiscogs.QueryForMetadataNonAsync(cancellationToken);
        //    if (queryDiscogs.ResultOfQuery)
        //        return "Results Returned from Discogs";

        //    return queryDiscogs.QueryResult;
        //}

        //private string RetrieveMetadataFromSpotify(Id3Tag songToSearch)
        //{
        //    Spotify querySpotify = new Spotify();
        //    querySpotify.ExistingMetadata = songToSearch;
        //    querySpotify.ListRetrievedTags = listRetrievedMetadata;
        //    querySpotify.QueryForMetadataNonAsync(cancellationToken);
        //    if (querySpotify.ResultOfQuery)
        //        return "Results Returned from Spotify";

        //    return querySpotify.QueryResult;
        //}

        //private async Task<string> SpotifyGetTags(Id3Tag songToSearch)
        //{
        //    Spotify querySpotify = new Spotify();
        //    querySpotify.ExistingMetadata = songToSearch;
        //    querySpotify.ListRetrievedTags = listRetrievedMetadata;
        //    System.Net.Http.HttpClient client = InitiateHttpClient();
        //    await querySpotify.GetTags(client, "", "", cancellationToken);
        //    if (querySpotify.ResultOfQuery)
        //        return "Results Returned from Spotify";

        //    return querySpotify.QueryResult;
        //}

        //private System.Net.Http.HttpClient InitiateHttpClient()
        //{
        //    // Defaul settings for all requests
        //    // Compression is enabled by default (depends on if server supports it)
        //    // "UseCookies = false" is needed for Netease
        //    System.Net.Http.HttpClientHandler handler = new System.Net.Http.HttpClientHandler
        //    {
        //        UseCookies = false,
        //        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
        //    };

        //    System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(handler);
        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.ConnectionClose = false;           // Will attempt to keep connections open which makes more efficient use of the client.
        //    client.DefaultRequestHeaders.Connection.Add("Keep-Alive");      // Will attempt to keep connections open which makes more efficient use of the client.
        //    client.Timeout = TimeSpan.FromSeconds(15);                      // Musicbrainz has 15s timeout in response header. Don't know if this setting is needed
        //    client.MaxResponseContentBufferSize = 256000000;
        //    ServicePointManager.DefaultConnectionLimit = 24;                // Not sure if it's needed since this limit applies to connections per remote host (per API), not in total per client

        //    return client;
        //}


        private string GetArtworkFromLastFM(ref Id3Tag songToSearch)
        {
            LastFM queryLastFM = new LastFM();

            queryLastFM.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryLastFM.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryLastFM.QueryForArtworkNonAsync(cancellationToken);
            if (queryLastFM.ResultOfQuery)
            {
                songToSearch = queryLastFM.RetrievedMetadata;
            }

            return queryLastFM.QueryResult;
        }

        private string GetArtworkFromSpotify(ref Id3Tag songToSearch)
        {
            Spotify querySpotify = new Spotify();

            querySpotify.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            querySpotify.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            querySpotify.QueryForArtworkNonAsync(cancellationToken);
            if (querySpotify.ResultOfQuery)
            {
                songToSearch = querySpotify.RetrievedMetadata;
            }

            return querySpotify.QueryResult;
        }

        private string GetArtworkFromiTunes(ref Id3Tag songToSearch)
        {
            ITunes queryiTunes = new ITunes();

            queryiTunes.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryiTunes.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryiTunes.QueryForArtworkNonAsync(cancellationToken);
            if (queryiTunes.ResultOfQuery)
            {
                songToSearch = queryiTunes.RetrievedMetadata;
            }

            return queryiTunes.QueryResult;
        }

        private string GetArtworkFromDeezer(ref Id3Tag songToSearch)
        {
            Deezer queryDeezer = new Deezer();

            queryDeezer.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryDeezer.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryDeezer.QueryForArtworkNonAsync(cancellationToken);
            if (queryDeezer.ResultOfQuery)
            {
                songToSearch = queryDeezer.RetrievedMetadata;
            }

            return queryDeezer.QueryResult;
        }

        private string GetArtworkFromMusixMatch(ref Id3Tag songToSearch)
        {
            MusixMatch queryMusixMatch = new MusixMatch();

            queryMusixMatch.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryMusixMatch.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryMusixMatch.QueryForArtworkNonAsync(cancellationToken);
            if (queryMusixMatch.ResultOfQuery)
            {
                songToSearch = queryMusixMatch.RetrievedMetadata;
            }

            return queryMusixMatch.QueryResult;
        }

        private string GetAlbumsFromMB(Id3Tag songToSearch)
        {
            MusicBrainz queryMusicBrainz = new MusicBrainz();

            queryMusicBrainz.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryMusicBrainz.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryMusicBrainz.QueryForRelease(cancellationToken);
            if (queryMusicBrainz.ResultOfQuery)
            {
                albumMetadata = queryMusicBrainz.ListRetrievedTags;
                foreach (var album in albumMetadata)
                {
                    AlbumMetadata TempAlbumMetdata = new AlbumMetadata();
                    TempAlbumMetdata.Source = "MusicBrainz";
                    TempAlbumMetdata.MusicMetatada = album;
                    AlbumsForArtist.Add(TempAlbumMetdata);
                }
            }
            return "";
        }

        private string GetAlbumsFromLastFM(Id3Tag songToSearch)
        {
            LastFM queryLastFM = new LastFM();

            queryLastFM.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryLastFM.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryLastFM.QueryForAlbum(cancellationToken);
            if (queryLastFM.ResultOfQuery)
            {

                albumMetadata = queryLastFM.ListRetrievedTags;
                foreach (var album in albumMetadata)
                {
                    AlbumMetadata TempAlbumMetdata = new AlbumMetadata();
                    TempAlbumMetdata.Source = "LastFM";
                    TempAlbumMetdata.MusicMetatada = album;
                    AlbumsForArtist.Add(TempAlbumMetdata);
                }

                return "";
            }
            return queryLastFM.QueryResult;
        }

        private string GetAlbumsFromSpotify(Id3Tag songToSearch)
        {
            Spotify querySpotify = new Spotify();

            querySpotify.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            querySpotify.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            querySpotify.QueryForAlbum(cancellationToken);
            if (querySpotify.ResultOfQuery)
            {

                albumMetadata = querySpotify.ListRetrievedTags;
                foreach (var album in albumMetadata)
                {
                    AlbumMetadata TempAlbumMetdata = new AlbumMetadata();
                    TempAlbumMetdata.Source = "Spotify";
                    TempAlbumMetdata.MusicMetatada = album;
                    AlbumsForArtist.Add(TempAlbumMetdata);
                }

                return "";
            }
            return querySpotify.QueryResult;
        }

        private string GetAlbumsFromNapster(Id3Tag songToSearch)
        {
            LastFM queryLastFM = new LastFM();

            queryLastFM.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryLastFM.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryLastFM.QueryForAlbum(cancellationToken);
            if (queryLastFM.ResultOfQuery)
            {
                albumMetadata = queryLastFM.ListRetrievedTags;
                foreach (var album in albumMetadata)
                {
                    AlbumMetadata TempAlbumMetdata = new AlbumMetadata();
                    TempAlbumMetdata.Source = "Napster";
                    TempAlbumMetdata.MusicMetatada = album;
                    AlbumsForArtist.Add(TempAlbumMetdata);
                }

                return "";
            }
            return queryLastFM.QueryResult;
        }

        private string GetAlbumsFromITunes(Id3Tag songToSearch)
        {
            LastFM queryLastFM = new LastFM();

            queryLastFM.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryLastFM.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryLastFM.QueryForAlbum(cancellationToken);
            if (queryLastFM.ResultOfQuery)
            {

                albumMetadata = queryLastFM.ListRetrievedTags;
                foreach (var album in albumMetadata)
                {
                    AlbumMetadata TempAlbumMetdata = new AlbumMetadata();
                    TempAlbumMetdata.Source = "iTunes";
                    TempAlbumMetdata.MusicMetatada = album;
                    AlbumsForArtist.Add(TempAlbumMetdata);
                }

                return "";
            }
            return queryLastFM.QueryResult;
        }

        private string GetAlbumsFromDeezer(Id3Tag songToSearch)
        {
            Deezer queryDeezer = new Deezer();

            queryDeezer.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryDeezer.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryDeezer.QueryForAlbum(cancellationToken);
            if (queryDeezer.ResultOfQuery)
            {

                albumMetadata = queryDeezer.ListRetrievedTags;
                foreach (var album in albumMetadata)
                {
                    AlbumMetadata TempAlbumMetdata = new AlbumMetadata();
                    TempAlbumMetdata.Source = "Deezer";
                    TempAlbumMetdata.MusicMetatada = album;
                    AlbumsForArtist.Add(TempAlbumMetdata);
                }

                return "";
            }
            return queryDeezer.QueryResult;
        }

        private string GetAlbumsFromMusixMatch(Id3Tag songToSearch)
        {
            MusixMatch queryMusixMatch = new MusixMatch();

            queryMusixMatch.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryMusixMatch.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryMusixMatch.QueryForAlbum(cancellationToken);
            if (queryMusixMatch.ResultOfQuery)
            {

                albumMetadata = queryMusixMatch.ListRetrievedTags;
                foreach (var album in albumMetadata)
                {
                    AlbumMetadata TempAlbumMetdata = new AlbumMetadata();
                    TempAlbumMetdata.Source = "MusixMatch";
                    TempAlbumMetdata.MusicMetatada = album;
                    AlbumsForArtist.Add(TempAlbumMetdata);
                }

                return "";
            }
            return queryMusixMatch.QueryResult;
        }

        #endregion

        private void SetMetadataService(QueryForMetadata flag=QueryForMetadata.Metadata)
        {
            flagMetadataService = 0;
            if (rbDiscogs.Checked)
            {
                if( !flag.HasFlag(QueryForMetadata.Artwork))
                {
                    flagMetadataService |= MetadataService.Discogs;
                }
            }

            if (rbITunes.Checked)
            {
                flagMetadataService |= MetadataService.iTunes;
            }

            if (rbNapster.Checked)
            {
                if (!flag.HasFlag(QueryForMetadata.Artwork))
                {
                    flagMetadataService |= MetadataService.Napster;
                }
            }

            if (rbDeezer.Checked)
            {
                flagMetadataService |= MetadataService.Deezer;
            }

            if (rbLastFM.Checked)
            {
                flagMetadataService |= MetadataService.LastFM;
            }

            if (rbMusicBrainz.Checked)
            {
                flagMetadataService |= MetadataService.MusicBrainz;
            }

            if (rbSpotify.Checked)
            {
                flagMetadataService |= MetadataService.Spotify;
            }

            if (rbMusixMatch.Checked)
            {
                if (!flag.HasFlag(QueryForMetadata.Artwork))
                {
                    flagMetadataService |= MetadataService.MusixMatch;
                }
            }

            if (rbGaliboo.Checked)
            {
                if (!flag.HasFlag(QueryForMetadata.Artwork))
                {
                    flagMetadataService |= MetadataService.Galiboo;
                }
            }

            if (rbGenius.Checked)
            {
                if (!flag.HasFlag(QueryForMetadata.Artwork))
                {
                    flagMetadataService |= MetadataService.Genius;
                }
            }
        }
        private bool VerifyOptionsChecked()
        {
            if (!rbMusicBrainz.Checked &&
                !rbITunes.Checked &&
                !rbDiscogs.Checked &&
                !rbLastFM.Checked &&
                !rbITunes.Checked &&
                !rbDeezer.Checked &&
                !rbNapster.Checked &&
                !rbSpotify.Checked &&
                !rbMusixMatch.Checked &&
                !rbGaliboo.Checked && 
                !rbGenius.Checked)
            {
                MessageBox.Show("Please select at least one service to search!", "No Service Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void rbDeezer_CheckedChanged(object sender, EventArgs e)
        {

        }
    }


}
