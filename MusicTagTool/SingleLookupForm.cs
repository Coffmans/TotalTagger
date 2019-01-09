using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalTagger
{
    public partial class SingleLookupForm : Form
    {
        private List<BestMatch> listBestMatches = new List<BestMatch>();
        private List<BestMatch> listReturnBestMatches = new List<BestMatch>();
        private BindingList<BestMatchMinimal> listBestMatchDataGrid = new BindingList<BestMatchMinimal>();
        private List<BestMatch> songsSelected = new List<BestMatch>();

        delegate void InvokeToDataGridDelegate(int row, string imageLocation= null);
        private InvokeToDataGridDelegate _InvokeToDataGrid = null;
        delegate void InvokeToProgressLabelDelegate(string sText);
        private InvokeToProgressLabelDelegate _InvokeToProgressBarLabel = null;

        private BackgroundWorker _backgroundWorkerMetadata;

        public Id3Tag gSongMetaData { get; set; }

        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken cancellationToken = new CancellationToken();

        private MetadataService flagMetadataService = 0;

        private bool success = false;

        private bool tagsUpdated = false;

        private string songsDirectory = "";

        public SingleLookupForm(string directory, List<BestMatch> listAllSongs)
        {
            InitializeComponent();

            songsDirectory = directory;
            for(int song=0; song < listAllSongs.Count; song++)
            {
                BestMatch match = new BestMatch();
                match.SongData = listAllSongs[song].SongData;
                match.Index = song;
                listBestMatches.Add(match);

                BestMatchMinimal minimal = new BestMatchMinimal();
                minimal.Include = true;
                minimal.ExistingArtist = listAllSongs[song].SongData.Artist;
                minimal.ExistingTitle = listAllSongs[song].SongData.Title;
                minimal.Filepath = listAllSongs[song].SongData.Filepath;
                listBestMatchDataGrid.Add(minimal);
            }

            listBestMatchDataGrid.AllowNew = true;
            listBestMatchDataGrid.AllowRemove = false;
            listBestMatchDataGrid.RaiseListChangedEvents = true;
            listBestMatchDataGrid.AllowEdit = true;


            _backgroundWorkerMetadata = new BackgroundWorker();
            _backgroundWorkerMetadata.WorkerSupportsCancellation = true;
            _backgroundWorkerMetadata.DoWork += new DoWorkEventHandler(Bw_DoWork);
            _backgroundWorkerMetadata.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bw_RunWorkerCompleted);

            _InvokeToDataGrid = new InvokeToDataGridDelegate(InvokeToDataGrid);
            _InvokeToProgressBarLabel = new InvokeToProgressLabelDelegate(InvokeToProgressLabel);

        }

        private void BestMatchForm_Load(object sender, EventArgs e)
        {
            dgvBestMatchResults.DataSource = listBestMatchDataGrid;
            dgvBestMatchResults.Columns["Index"].Visible = false;
            dgvBestMatchResults.Columns["Cover"].Visible = false;
            DataGridViewTextBoxColumn imageLocationColumn = new DataGridViewTextBoxColumn();
            imageLocationColumn.Name = "CoverArt";
            imageLocationColumn.HeaderText = "Cover Art";
            dgvBestMatchResults.Columns.Add(imageLocationColumn);

            rbDeezer.Checked = true;
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

        private void btnSearchForAlbums_Click(object sender, EventArgs e)
        {
            foreach(var song in listBestMatchDataGrid)
            {
                song.Album = "";
                song.Artist = "";
                song.Date = "";
                song.Genre = "";
                song.Title = "";
                song.Cover = new PictureBox();
                song.Cover.Image = null;
                song.Cover.ImageLocation = "";
            }


            for(int row  = 0; row < dgvBestMatchResults.Rows.Count; row++)
            {
                dgvBestMatchResults.Rows[row].Cells["CoverArt"].Value = "";
            }

            dgvBestMatchResults.Refresh();


            SetMetadataService();

            if (LoadSelectedIntoList())
            {
                EnableControls(false);
                _backgroundWorkerMetadata.RunWorkerAsync(false);
            }
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
            else if (rbMusicBrainz.Checked)
            {
                flagMetadataService |= MetadataService.MusicBrainz;
            }
            else if (rbSpotify.Checked)
            {
                flagMetadataService |= MetadataService.Spotify;
            }
            else if( rbDiscogs.Checked)
            {
                flagMetadataService |= MetadataService.Discogs;
            }
            else if (rbMusixMatch.Checked)
            {
                flagMetadataService |= MetadataService.MusixMatch;
            }
        }

        private bool LoadSelectedIntoList(bool WriteTags=false)
        {
            bool selected = false;
            try
            {
                songsSelected = new List<BestMatch>();
                int nItems = dgvBestMatchResults.Rows.Count;

                if (nItems > 0)
                {
                    for (int song = 0; song < nItems; song++)
                    {
                        if ((bool)dgvBestMatchResults.Rows[song].Cells[0].Value == true)
                        {
                            int selectedListIndex = dgvBestMatchResults.Rows[song].Index;
                            BestMatch match = new BestMatch();
                            if (WriteTags)
                            {
                                match.SongData = new Id3Tag();
                                match.SongData.Artist = listBestMatchDataGrid[selectedListIndex].Artist;
                                match.SongData.Title = listBestMatchDataGrid[selectedListIndex].Title;
                                match.SongData.Album = listBestMatchDataGrid[selectedListIndex].Album;
                                match.SongData.Genre = listBestMatchDataGrid[selectedListIndex].Genre;
                                match.SongData.Date = listBestMatchDataGrid[selectedListIndex].Date;
                                match.SongData.Cover = listBestMatchDataGrid[selectedListIndex].Cover;
                                match.SongData.Filepath = listBestMatchDataGrid[selectedListIndex].Filepath;

                            }
                            else
                            {
                                match = listBestMatches[selectedListIndex];
                            }
                            songsSelected.Add(match);

                            selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return selected;
        }

        private void InvokeToDataGrid(int row = -1, string imageLocation=null)
        {
            // If it's coming from another thread, Invoke _InvokeToListView trough the _InvokeToListViewDelegate and end this thing.
            if (dgvBestMatchResults.InvokeRequired)
            {
                this.Invoke(new InvokeToDataGridDelegate(InvokeToDataGrid), row, imageLocation);
                return;
            }

            dgvBestMatchResults.Refresh();
            if (!String.IsNullOrEmpty(imageLocation))
                dgvBestMatchResults.Rows[row].Cells["CoverArt"].Value = imageLocation;
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

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach(var song in songsSelected)
            {
                if( (bool)e.Argument == true)
                {
                    WriteTags(song);
                    continue;
                }
                Id3Tag songData = new Id3Tag();
                songData = song.SongData;

                InvokeToProgressLabel("Searching for Tags For " + songData.Title + " - " + songData.Artist);
                if (flagMetadataService.HasFlag(MetadataService.MusicBrainz))
                {
                    success = RetrieveMetadataFromMusicBrainz(ref songData);
                }
                else if (flagMetadataService.HasFlag(MetadataService.iTunes))
                {
                    success = RetrieveMetadataFromiTunes(ref songData);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Napster))
                {
                    success = RetrieveMetadataFromNapster(ref songData);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Deezer))
                {
                    success = RetrieveMetadataFromDeezer(ref songData);

                }
                else if (flagMetadataService.HasFlag(MetadataService.LastFM))
                {
                    success = RetrieveMetadataFromLastFM(ref songData);

                }
                else if (flagMetadataService.HasFlag(MetadataService.Discogs))
                {
                    success = RetrieveMetadataFromDiscogsAsync(ref songData);
                }
                else if (flagMetadataService.HasFlag(MetadataService.Spotify))
                {
                    success = RetrieveMetadataFromSpotify(ref songData);

                }
                else if (flagMetadataService.HasFlag(MetadataService.MusixMatch))
                {
                    success = RetrieveMetadataFromMusixMatch(ref songData);
                }

                if ( success)
                {
                    InvokeToProgressLabel("Found Tags For " + songData.Title + " - " + songData.Artist);

                    listBestMatchDataGrid[song.Index].Title = songData.Title;
                    listBestMatchDataGrid[song.Index].Artist = songData.Artist;
                    listBestMatchDataGrid[song.Index].Album = songData.Album;
                    listBestMatchDataGrid[song.Index].Date = songData.Date;
                    listBestMatchDataGrid[song.Index].Genre = songData.Genre;
                    

                    if (songData.Cover != null && !String.IsNullOrEmpty(songData.Cover.ImageLocation))
                    {
                        try
                        {
                            var request = WebRequest.Create(songData.Cover.ImageLocation);

                            using (var response = request.GetResponse())
                            using (var stream = response.GetResponseStream())
                            {
                                listBestMatchDataGrid[song.Index].Cover = new PictureBox();
                                listBestMatchDataGrid[song.Index].Cover.ImageLocation = songData.Cover.ImageLocation;
                                System.Drawing.Image currentImage = Bitmap.FromStream(stream);
                                listBestMatchDataGrid[song.Index].Cover.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }

                    InvokeToDataGrid(song.Index, listBestMatchDataGrid[song.Index].Cover.ImageLocation);
                }
                else
                {
                    InvokeToProgressLabel("Unable to Find Tags For " + songData.Title + " - " + songData.Artist);
                }

            }
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                InvokeToProgressLabel("Operation Cancelled!");

            }
            else if (!(e.Error == null))
            {
                InvokeToProgressLabel("Operation Errored!");
            }
            else
            {
                InvokeToProgressLabel("Operation Completed!");
            }


            EnableControls(true);
        }

        private void EnableControls(bool bEnable)
        {
            dgvBestMatchResults.Enabled = bEnable;
            rbDeezer.Enabled = bEnable;
            rbDiscogs.Enabled = bEnable;
            rbITunes.Enabled = bEnable;
            rbLastFM.Enabled = bEnable;
            rbMusicBrainz.Enabled = bEnable;
            rbNapster.Enabled = bEnable;
            rbSpotify.Enabled = bEnable;
            rbMusixMatch.Enabled = bEnable;

            btnWriteTags.Enabled = bEnable;
            btnClose.Enabled = bEnable;
        }

        private bool RetrieveMetadataFromMusicBrainz(ref Id3Tag songData)
        {
            MusicBrainz queryMusicBrainz = new MusicBrainz();
            queryMusicBrainz.ExistingMetadata = songData;
            queryMusicBrainz.QueryForMetadataNonAsync(cancellationToken, 1);
            if (queryMusicBrainz.ResultOfQuery)
            {
                songData = queryMusicBrainz.RetrievedMetadata;
                return true;
            }
            return false;
        }

        private bool RetrieveMetadataFromNapster(ref Id3Tag songData)
        {
            Napster queryNapster = new Napster();
            queryNapster.ExistingMetadata = songData;
            queryNapster.QueryForMetadataNonAsync(cancellationToken, false, 1);
            if (queryNapster.ResultOfQuery)
            {
                songData = queryNapster.RetrievedMetadata;
                return true;
            }
            return false;
        }

        private bool RetrieveMetadataFromDeezer(ref Id3Tag songData)
        {
            Deezer queryDeezer = new Deezer();
            queryDeezer.ExistingMetadata = songData;
            queryDeezer.QueryForMetadataNonAsync(cancellationToken, false, 1);
            if (queryDeezer.ResultOfQuery)
            {
                songData = queryDeezer.RetrievedMetadata;
                return true;
            }
            return false;
        }

        private bool RetrieveMetadataFromMusixMatch(ref Id3Tag songData)
        {
            MusixMatch musixMatch = new MusixMatch();
            musixMatch.ExistingMetadata = songData;
            musixMatch.QueryForMetadataNonAsync(cancellationToken, false, 1);
            if (musixMatch.ResultOfQuery)
            {
                songData = musixMatch.RetrievedMetadata;
                return true;
            }
            return false;
        }

        private bool RetrieveMetadataFromiTunes(ref Id3Tag songData)
        {
            ITunes queryITunes = new ITunes();
            queryITunes.ExistingMetadata = songData;
            queryITunes.QueryForMetadataNonAsync(cancellationToken, false, 1);
            if (queryITunes.ResultOfQuery)
            {
                songData = queryITunes.RetrievedMetadata;
                return true;
            }
            return false;
        }

        private bool RetrieveMetadataFromLastFM(ref Id3Tag songData)
        {
            LastFM queryLastFM = new LastFM();
            queryLastFM.ExistingMetadata = songData;
            queryLastFM.QueryForMetadataNonAsync(cancellationToken, 1);
            if (queryLastFM.ResultOfQuery)
            {
                songData = queryLastFM.RetrievedMetadata;
                return true;
            }
            return false;
        }

        private bool RetrieveMetadataFromDiscogsAsync(ref Id3Tag songData)
        {
            Discogs queryDiscogs = new Discogs();
            queryDiscogs.ExistingMetadata = songData;
            queryDiscogs.QueryForMetadataNonAsync(cancellationToken, 1);
            if (queryDiscogs.ResultOfQuery)
            {
                songData = queryDiscogs.RetrievedMetadata;
                return true;
            }
            return false;
        }

        private bool RetrieveMetadataFromSpotify(ref Id3Tag songData)
        {
            Spotify querySpotify = new Spotify();
            querySpotify.ExistingMetadata = songData;
            querySpotify.QueryForMetadataNonAsync(cancellationToken, 1);
            if (querySpotify.ResultOfQuery)
            {
                songData = querySpotify.RetrievedMetadata;
                return true;
            }
            return false;
        }

        private void btnClose_Click(object sender, EventArgs e)
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

        }

        private void btnWriteTags_Click(object sender, EventArgs e)
        {
            if( LoadSelectedIntoList(true) )
            {
                EnableControls(false);
                _backgroundWorkerMetadata.RunWorkerAsync(true);
            }
        }

        private void WriteTags(BestMatch match)
        { 
            tagsUpdated = false;
            try
            {
                listReturnBestMatches = new List<BestMatch>();
                BestMatch songMatch = new BestMatch();
                songMatch.SongData = new Id3Tag();
                if( match.SongData.Title.Length == 0 || match.SongData.Artist.Length == 0 )
                {
                    return;
                }
                songMatch.SongData.Filepath = match.SongData.Filepath;
                songMatch.SongData.Title = match.SongData.Title;
                songMatch.SongData.Artist = match.SongData.Artist;
                songMatch.SongData.Album = match.SongData.Album;
                songMatch.SongData.Date = match.SongData.Date;
                songMatch.SongData.Genre = match.SongData.Genre;
                songMatch.SongData.Cover = new PictureBox();
                if (match.SongData.Cover != null && !String.IsNullOrEmpty(match.SongData.Cover.ImageLocation))
                {
                    match.SongData.Cover.ImageLocation = match.SongData.Cover.ImageLocation;
                    match.SongData.Cover.Image = match.SongData.Cover.Image;
                }

                InvokeToProgressLabel("Writing Tags For " + match.SongData.Title + " - " + match.SongData.Artist);

                string writeResult = ReadWriteID3.WriteID3Tags(match.SongData.Filepath, match.SongData);
                if (writeResult.Length > 0)
                {
                    InvokeToProgressLabel("Error Writing Tags For " + match.SongData.Title + " - " + match.SongData.Artist);
                    MessageBox.Show(writeResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    InvokeToProgressLabel(writeResult);
                }
                else
                {
                    InvokeToProgressLabel("Success Writing Tags For " + match.SongData.Title + " - " + match.SongData.Artist);
                    listReturnBestMatches.Add(match);
                    tagsUpdated = true;
                }
            }
            catch (Exception ex)
            {
                InvokeToProgressLabel(ex.Message);
            }
        }

        private void rbDiscogs_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
