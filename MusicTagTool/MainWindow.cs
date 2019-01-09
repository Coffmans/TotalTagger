using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections.Concurrent;
using ParkSquare.Gracenote;
using System.Reflection;
using System.Net;
using System.Threading;
using RestSharp;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Un4seen.Bass;
//using ManagedBass;
using System.Diagnostics;

namespace TotalTagger
{
    public partial class MainWindow : Form
    {
        string[] extension = { "*.mp3" };
        private string songsDirectory = "";
        private string songFileBeingProcessed = "";
        private int selectedListIndex = -1;

        enum SelectedService
        {
            None = 0,
            iTunes = 1,
            Discogs = 2,
            MusicBrainz = 3,
            AudioDB = 4,
            Deezer = 5,
            LastFM = 6,
            Spotify = 7,
            Napster = 8,
            Amazon = 9,
            MusixMatch = 10,
            Galiboo = 11,
            Genius = 12
        }

        enum ActionToPerform
        {
            LoadFiles = 0,
            RetrieveMetadata,
            LoadfilesRetrieveMetadata,
            UpdateAll
        };

        private BackgroundWorker _backgroundWorker;
        delegate void InvokeToDataGridDelegate(Id3Tag metaData, int nRow);
        delegate void InvokeToDataGridSourceDelegate(bool refreshOnly);
        delegate void InvokeToBindingListDelegate(Id3Tag song);
        delegate void InvokeToProgressBarLabelDelegate(string sText);
        delegate void InvokeToProgressBarDelegate(int nPercentage);

        private const int GRID_FILENAME = 0;
        private const int GRID_ID3_TITLE = 1;
        private const int GRID_ID3_ARTIST = 2;
        private const int GRID_ID3_ALBUM = 3;
        private const int GRID_RETRIEVED_TITLE = 4;
        private const int GRID_METADATA_PULLED = 5;
        private const int GRID_RETRIEVED_ARTWORK = 6;

        private const string GRID_FILE_ARTWORK_HEADER = "ID3AlbumArt";
        private const string GRID_SERVICE_ARTWORK_HEADER = "ServiceAlbumArt";
        private const string GRID_SERVICE_METADATA = "ServiceMetadata";

        private BindingList<Id3Tag> listId3TagForAllFiles;

        private TaggingTool.GenreDataTable m_dtTaggingToolTable;

        public static AppSettings serviceSettings = new AppSettings();
        IEnumerable<string> songList;

        public static Font fontProgressBarText = new Font("Courier New", 8, FontStyle.Regular);
        public static Font fontProgressBarColor = new Font("Consolas", 12, FontStyle.Regular);

        public static bool streamLoaded = false;
        private static int streamHandle = -1;
        public static int GetStreamHandle
        {
            get { return streamHandle; }
            set { streamHandle = value; }
        }

        public static IntPtr theHandle;
        public static Stopwatch timeListenedTracker = new Stopwatch();
        public MainWindow()
        {
            InitializeComponent();

            theHandle = Handle;

            playProgressBar.Minimum = 0;
            playProgressBar.Maximum = 400;

            _backgroundWorker = new BackgroundWorker
            {
                //_backgroundWorker.WorkerReportsProgress = true;
                WorkerSupportsCancellation = true
            };
            _backgroundWorker.DoWork += new DoWorkEventHandler(Bw_DoWork);
            //_backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bw_RunWorkerCompleted);

            listId3TagForAllFiles = new BindingList<Id3Tag>();
            listId3TagForAllFiles.AllowNew = true;
            listId3TagForAllFiles.AllowRemove = false;
            listId3TagForAllFiles.RaiseListChangedEvents = true;
            listId3TagForAllFiles.AllowEdit = true;

            //Bass.Init();
            TotalTagger.BASS.BassLibrary.InitSoundDevice();

            progressRefreshTimer.Start();
        }

        private string BrowseForFolder(string sExistingDirectory)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog
            {
                SelectedPath = sExistingDirectory
            };
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                return folderBrowserDialog1.SelectedPath;
            }

            return "";
        }

        public static IEnumerable<string> GetFiles(string path, string[] searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return searchPatterns.AsParallel().SelectMany(searchPattern => Directory.EnumerateFiles(path, searchPattern, searchOption));
        }

        private void LoadFilesIntoGrid(string sMusicDirectory)
        {
            listId3TagForAllFiles.Clear();

            try
            {
                int nSong = 0;
                int nAllSongs = songList.Count();

                Id3Tag _DummyID3 = new Id3Tag();

                foreach (var f in songList)
                {
                    //InvokeToProgressBarLabel("Reading In " + f.nSong + " of " + nAllSongs + " songs");

                    Id3Tag _ID3Tags = ReadWriteID3.ReadID3Tags(f);
                    listId3TagForAllFiles.Add(_ID3Tags);
                    InvokeToProgressBarLabel("Reading " +_ID3Tags.Title + " from " + _ID3Tags.Artist);
                    nSong++;
                }

                InvokeToDataGridSource(false);

                if (listId3TagForAllFiles != null && listId3TagForAllFiles.Any())
                {
                    for(int song=0; song < listId3TagForAllFiles.Count; song++)
                    {
                        if(listId3TagForAllFiles[song].Cover != null )
                        {
                            InvokeToProgressBarLabel("Reading In " + nSong + " of " + listId3TagForAllFiles.Count + " songs");
                            InvokeToDataGrid(listId3TagForAllFiles[song], song);
                        }
                    }
                }

                InvokeToProgressBarLabel("Completed");
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            using (Stream stream = myAssembly.GetManifestResourceStream("TotalTagger.Genres.xml"))
            {
                m_dtTaggingToolTable = new TaggingTool.GenreDataTable();
                m_dtTaggingToolTable.ReadXml(stream);
            }

            LoadAppSettings();
            dataGridSongFiles.DataSource = null;
            dataGridSongFiles.Rows.Clear();
            dataGridSongFiles.Refresh();

            dataGridSongFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridSongFiles.Enabled = false;

            listId3TagForAllFiles = new BindingList<Id3Tag>();
            listId3TagForAllFiles.AllowEdit = true;
            listId3TagForAllFiles.AllowNew = true;
            listId3TagForAllFiles.AllowRemove = true;

            LoadServiceComboBox();

            //ComboboxItem cbItem = new ComboboxItem();
            //cbItem.Text = "Deezer";
            //cbItem.Value = SelectedService.Deezer;
            //cbServiceSimpleLookup.Items.Add(cbItem);

            //if( !String.IsNullOrEmpty(serviceSettings.DiscogsClientID))
            //{
            //    cbItem = new ComboboxItem();
            //    cbItem.Text = "Discogs";
            //    cbItem.Value = SelectedService.Discogs;
            //    cbServiceSimpleLookup.Items.Add(cbItem);
            //}
            //if (!String.IsNullOrEmpty(serviceSettings.GalibooClientKey))
            //{
            //    cbItem = new ComboboxItem();
            //    cbItem.Text = "Galiboo";
            //    cbItem.Value = SelectedService.Galiboo;
            //    cbServiceSimpleLookup.Items.Add(cbItem);
            //}

            //if (!String.IsNullOrEmpty(serviceSettings.GeniusClientKey))
            //{
            //    cbItem = new ComboboxItem();
            //    cbItem.Text = "Genius";
            //    cbItem.Value = SelectedService.Genius;
            //    cbServiceSimpleLookup.Items.Add(cbItem);
            //}

            //cbItem = new ComboboxItem();
            //cbItem.Text = "iTunes";
            //cbItem.Value = SelectedService.iTunes;
            //int index = cbServiceSimpleLookup.Items.Add(cbItem);
            //cbServiceSimpleLookup.SelectedIndex = index;

            //if (!String.IsNullOrEmpty(serviceSettings.LastFMClientKey))
            //{
            //    cbItem = new ComboboxItem();
            //    cbItem.Text = "LastFM";
            //    cbItem.Value = SelectedService.LastFM;
            //    cbServiceSimpleLookup.Items.Add(cbItem);
            //}

            //if (!String.IsNullOrEmpty(serviceSettings.NapsterClientKey))
            //{
            //    cbItem = new ComboboxItem();
            //    cbItem.Text = "Napster";
            //    cbItem.Value = SelectedService.Napster;
            //    cbServiceSimpleLookup.Items.Add(cbItem);
            //}

            //cbItem = new ComboboxItem();
            //cbItem.Text = "MusicBrainz";
            //cbItem.Value = SelectedService.MusicBrainz;
            //cbServiceSimpleLookup.Items.Add(cbItem);

            //if (!String.IsNullOrEmpty(serviceSettings.MusixMatchKey))
            //{
            //    cbItem = new ComboboxItem();
            //    cbItem.Text = "MusixMatch";
            //    cbItem.Value = SelectedService.MusixMatch;
            //    cbServiceSimpleLookup.Items.Add(cbItem);
            //}

            //if (!String.IsNullOrEmpty(serviceSettings.SpotifyClientID))
            //{
            //    cbItem = new ComboboxItem();
            //    cbItem.Text = "Spotify";
            //    cbItem.Value = SelectedService.Spotify;
            //    cbServiceSimpleLookup.Items.Add(cbItem);
            //}

#if DEBUG
            string selectedDirectory = @"c:\users\scoff\downloads\test\";

            if (!String.IsNullOrEmpty(selectedDirectory))
            {
                lblFolder.Text = selectedDirectory;
                if (songsDirectory != lblFolder.Text)
                {
                    songsDirectory = lblFolder.Text;
                    if(!songsDirectory.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
                    {
                        songsDirectory += Path.DirectorySeparatorChar;
                    }
                    songList = GetFiles(songsDirectory, extension, SearchOption.AllDirectories);

                    if (songList != null && songList.Count() == 0)
                    {
                        return;
                    }

                    PerformProcessingForSongFiles(ActionToPerform.LoadFiles);
                }
            }

#endif
        }

        private void LoadAppSettings()
        {
            serviceSettings = new AppSettings();
            string ID = ConfigData.GetConfigData("DCCI");
            if(!String.IsNullOrEmpty(ID))
                serviceSettings.DiscogsClientID = ConfigData.DecryptString(ID, Properties.Resources.Pass + Properties.Resources.Phrase);
            string key = ConfigData.GetConfigData("DCCK");
            if( !String.IsNullOrEmpty(key))
                serviceSettings.DiscogsClientKey = ConfigData.DecryptString(key, Properties.Resources.Pass+Properties.Resources.Phrase);

            key = ConfigData.GetConfigData("GACK");
            if (!String.IsNullOrEmpty(key))
                serviceSettings.GalibooClientKey = ConfigData.DecryptString(key, Properties.Resources.Pass + Properties.Resources.Phrase);

            key = ConfigData.GetConfigData("GECK");
            if (!String.IsNullOrEmpty(key))
                serviceSettings.GeniusClientKey = ConfigData.DecryptString(key, Properties.Resources.Pass + Properties.Resources.Phrase);

            key = ConfigData.GetConfigData("LFCK");
            if (!String.IsNullOrEmpty(key))
                serviceSettings.LastFMClientKey = ConfigData.DecryptString(key, Properties.Resources.Pass + Properties.Resources.Phrase);

            key = ConfigData.GetConfigData("MXCK");
            if (!String.IsNullOrEmpty(key))
                serviceSettings.MusixMatchKey = ConfigData.DecryptString(key, Properties.Resources.Pass + Properties.Resources.Phrase);

            key = ConfigData.GetConfigData("NACK");
            if (!String.IsNullOrEmpty(key))
                serviceSettings.NapsterClientKey = ConfigData.DecryptString(key, Properties.Resources.Pass + Properties.Resources.Phrase);

            ID = ConfigData.GetConfigData("SPCI");
            if (!String.IsNullOrEmpty(ID))
                serviceSettings.SpotifyClientID = ConfigData.DecryptString(ID, Properties.Resources.Pass + Properties.Resources.Phrase);

            key = ConfigData.GetConfigData("SPCK");
            if (!String.IsNullOrEmpty(key))
                serviceSettings.SpotifyClientKey = ConfigData.DecryptString(key, Properties.Resources.Pass + Properties.Resources.Phrase);
        }

        private void LoadServiceComboBox()
        {
            cbServiceSimpleLookup.Items.Clear();
            ComboboxItem cbItem = new ComboboxItem();
            cbItem.Text = "Deezer";
            cbItem.Value = SelectedService.Deezer;
            cbServiceSimpleLookup.Items.Add(cbItem);

            if (!String.IsNullOrEmpty(serviceSettings.DiscogsClientID))
            {
                cbItem = new ComboboxItem();
                cbItem.Text = "Discogs";
                cbItem.Value = SelectedService.Discogs;
                cbServiceSimpleLookup.Items.Add(cbItem);
            }
            if (!String.IsNullOrEmpty(serviceSettings.GalibooClientKey))
            {
                cbItem = new ComboboxItem();
                cbItem.Text = "Galiboo";
                cbItem.Value = SelectedService.Galiboo;
                cbServiceSimpleLookup.Items.Add(cbItem);
            }

            if (!String.IsNullOrEmpty(serviceSettings.GeniusClientKey))
            {
                cbItem = new ComboboxItem();
                cbItem.Text = "Genius";
                cbItem.Value = SelectedService.Genius;
                cbServiceSimpleLookup.Items.Add(cbItem);
            }

            cbItem = new ComboboxItem();
            cbItem.Text = "iTunes";
            cbItem.Value = SelectedService.iTunes;
            int index = cbServiceSimpleLookup.Items.Add(cbItem);
            cbServiceSimpleLookup.SelectedIndex = index;

            if (!String.IsNullOrEmpty(serviceSettings.LastFMClientKey))
            {
                cbItem = new ComboboxItem();
                cbItem.Text = "LastFM";
                cbItem.Value = SelectedService.LastFM;
                cbServiceSimpleLookup.Items.Add(cbItem);
            }

            if (!String.IsNullOrEmpty(serviceSettings.NapsterClientKey))
            {
                cbItem = new ComboboxItem();
                cbItem.Text = "Napster";
                cbItem.Value = SelectedService.Napster;
                cbServiceSimpleLookup.Items.Add(cbItem);
            }

            cbItem = new ComboboxItem();
            cbItem.Text = "MusicBrainz";
            cbItem.Value = SelectedService.MusicBrainz;
            cbServiceSimpleLookup.Items.Add(cbItem);

            if (!String.IsNullOrEmpty(serviceSettings.MusixMatchKey))
            {
                cbItem = new ComboboxItem();
                cbItem.Text = "MusixMatch";
                cbItem.Value = SelectedService.MusixMatch;
                cbServiceSimpleLookup.Items.Add(cbItem);
            }

            if (!String.IsNullOrEmpty(serviceSettings.SpotifyClientID))
            {
                cbItem = new ComboboxItem();
                cbItem.Text = "Spotify";
                cbItem.Value = SelectedService.Spotify;
                cbServiceSimpleLookup.Items.Add(cbItem);
            }

        }
        private void UpdateMetadataForSong(bool bReplaceMetadata, Id3Tag metaData, int nSongIndex)
        {
            //try
            //{
            //    var file = TagLib.File.Create(metaData.Filepath);

            //    if (bReplaceMetadata)
            //    {
            //        metaData.Title = file.Tag.Title = metaData.Title;
            //        file.Tag.AlbumArtists = new string[] { metaData.Artist };
            //        metaData.Artist = metaData.Artist;
            //        metaData.Album = file.Tag.Album = metaData.Album;
            //        file.Tag.Year = Convert.ToUInt32(metaData.Date);
            //        metaData.Date = metaData.Date;
            //        if( !String.IsNullOrEmpty(metaData.Genre ) )
            //            file.Tag.Genres = new string[] { metaData.Genre };
            //        metaData.Genre = metaData.Genre;

            //        if (!String.IsNullOrEmpty(metaData.Cover.ImageLocation))
            //        {
            //            var request = WebRequest.Create(metaData.Cover.ImageLocation);

            //            using (var response = request.GetResponse())
            //            using (var stream = response.GetResponseStream())
            //            {
            //                System.Drawing.Image currentImage = Bitmap.FromStream(stream);
            //                metaData.Cover.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
            //            }

            //            TagLib.Picture pic = new TagLib.Picture
            //            {
            //                Type = TagLib.PictureType.FrontCover,
            //                MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg,
            //                Description = "Cover"
            //            };
            //            MemoryStream ms = new MemoryStream();
            //            metaData.Cover.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // <-- Error doesn't occur anymore
            //            ms.Position = 0;
            //            pic.Data = TagLib.ByteVector.FromStream(ms);
            //            file.Tag.Pictures = new TagLib.IPicture[1] { pic };
            //        }
            //    }
            //    else
            //    {
            //        metaData.Title = file.Tag.Title = metaData.Title;
            //        file.Tag.AlbumArtists = new string[] { metaData.Artist };
            //        if( metaData.Date != null && metaData.Date.Any() )
            //        {
            //            file.Tag.Year = Convert.ToUInt32(metaData.Date);
            //        }
            //        else
            //        {
            //            file.Tag.Year = 0;
            //        }

            //        file.Tag.Genres = new string[] { metaData.Genre };
            //    }

            //    file.Save();

            //    listId3TagForAllFiles[nSongIndex] = metaData;

            //    InvokeToDataGrid(metaData, nSongIndex);

            //    InvokeToProgressBarLabel("Song \"" + metaData.Filepath + "\" with Title \"" + metaData.Title + "\" Updated!");
            //}
            //catch (System.Exception ex)
            //{
            //    InvokeToProgressBarLabel(ex.ToString());	
            //}
        }

        private void BtnRetrieveMetadata_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridSongFiles.Rows.Count > 0 && songList != null && songList.Count() > 0)
                {
                    PerformProcessingForSongFiles(ActionToPerform.RetrieveMetadata);
                }
                else
                {
                    songList = GetFiles(songsDirectory, extension, SearchOption.AllDirectories);

                    if (songList != null && songList.Count() == 0)
                    {
                        return;
                    }

                    EnableDisableControls(false);
                    PerformProcessingForSongFiles(ActionToPerform.LoadfilesRetrieveMetadata);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformProcessingForSongFiles(ActionToPerform eAction)
        {
            try
            {
                if (_backgroundWorker.IsBusy)
                {
                    return;
                }

                dataGridSongFiles.Enabled = false;

                if (eAction == ActionToPerform.LoadFiles)
                {
                    dataGridSongFiles.DataSource = null;
                    dataGridSongFiles.Rows.Clear();
                    dataGridSongFiles.Refresh();
                }
                else if (eAction == ActionToPerform.LoadfilesRetrieveMetadata)
                {
                    dataGridSongFiles.DataSource = null;
                    dataGridSongFiles.Rows.Clear();
                    dataGridSongFiles.Refresh();
                }


                //lblSongDirectory.Font = new Font(lblSongDirectory.Font, FontStyle.Regular);
                //lblProgressBar.Visible = true;
                EnableDisableControls(false);
                lblProgressBar.Visible = true;
                _backgroundWorker.RunWorkerAsync(eAction);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InvokeToDataGrid(Id3Tag musicData, int nRow)
        {
            // If it's coming from another thread, Invoke _InvokeToListView trough the _InvokeToListViewDelegate and end this thing.
            if (dataGridSongFiles.InvokeRequired)
            {
                this.Invoke(new InvokeToDataGridDelegate(InvokeToDataGrid), musicData, nRow);
                return;
            }

            dataGridSongFiles.Rows[nRow].Cells[GRID_ID3_TITLE].Value = musicData.Title;

            dataGridSongFiles.Rows[nRow].Cells[GRID_ID3_ARTIST].Value = musicData.Artist;
            dataGridSongFiles.Rows[nRow].Cells[GRID_ID3_ALBUM].Value = musicData.Album;

            if (musicData.Cover != null)
            {
                if (  musicData.Cover.Image != null)
                {
                    dataGridSongFiles.Rows[nRow].Cells[GRID_FILE_ARTWORK_HEADER].Value = musicData.Cover.Image.GetThumbnailImage(25, 25, null, System.IntPtr.Zero);
                }
                else if( !String.IsNullOrEmpty(musicData.Cover.ImageLocation))
                {
                    var request = WebRequest.Create(musicData.Cover.ImageLocation);

                    using (var response = request.GetResponse())
                    using (var stream = response.GetResponseStream())
                    {
                        System.Drawing.Image currentImage = Bitmap.FromStream(stream);
                        dataGridSongFiles.Rows[nRow].Cells[GRID_FILE_ARTWORK_HEADER].Value = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                    }
                }
            }
        }

        private void InvokeToBindingList(Id3Tag song)
        {
            if (dataGridSongFiles.InvokeRequired)
            {
                this.Invoke(new InvokeToBindingListDelegate(InvokeToBindingList),song);
                return;
            }
            listId3TagForAllFiles.Add(song);
        }
        private void InvokeToDataGridSource(bool refreshOnly=false)
        {
            // If it's coming from another thread, Invoke _InvokeToListView trough the _InvokeToListViewDelegate and end this thing.
            if (dataGridSongFiles.InvokeRequired)
            {
                this.Invoke(new InvokeToDataGridSourceDelegate(InvokeToDataGridSource), refreshOnly);
                return;
            }

            if( refreshOnly)
            {
                dataGridSongFiles.Refresh();
                return;
            }
            if (dataGridSongFiles.Columns[GRID_FILE_ARTWORK_HEADER] != null)
            {
                dataGridSongFiles.Columns.Remove(GRID_FILE_ARTWORK_HEADER);
            }
            dataGridSongFiles.DataSource = listId3TagForAllFiles;
            dataGridSongFiles.Columns["Cover"].Visible = false;
            //if( dataGridSongFiles.Columns[GRID_FILE_ARTWORK_HEADER] == null )
            {
                DataGridViewImageColumn imgFileAlbumArt = new DataGridViewImageColumn
                {
                    Name = GRID_FILE_ARTWORK_HEADER,
                    HeaderText = "Cover Art",
                    Image = null,
                    Width = 60,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    Visible = true
                };
                dataGridSongFiles.Columns.Add(imgFileAlbumArt);
            }

            dataGridSongFiles.Columns[GRID_FILENAME].Name = "FileName";
            dataGridSongFiles.Columns[GRID_FILENAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dataGridSongFiles.Refresh();
        }

        private void InvokeToProgressBarLabel(string sText)
        {
            if (lblProgressBar.InvokeRequired)
            {
                this.Invoke(new InvokeToProgressBarLabelDelegate(InvokeToProgressBarLabel), sText);
                return;
            }

            lblProgressBar.Text = sText;
        }

        private void RetrievedMetaDataFromService()
        {
            var client = new ParkSquare.Gracenote.GracenoteClient("712956455-629D246656DAF564E93DE72F0153A5D2");

            int nSong = 0;
            int nAllSongs = listId3TagForAllFiles.Count;

            for (nSong = 0; nSong < nAllSongs; nSong++)
            {
                InvokeToProgressBarLabel("Retrieving Metadata for " + nSong + " of " + nAllSongs + " songs");

                Id3Tag fileData = (Id3Tag)listId3TagForAllFiles[nSong];

                var x = client.Search(new ParkSquare.Gracenote.SearchCriteria
                {
                    TrackTitle = fileData.Title,
                    Artist = fileData.Artist,
                    AlbumTitle = fileData.Album,
                    SearchMode = SearchMode.BestMatchWithCoverArt,
                    SearchOptions = SearchOptions.ArtistImage
                });

                if (x != null && x.Count > 0)
                {
                    var song = x.Albums.First();
                    fileData.Title = song.Tracks.First().Title;
                    fileData.Artist = song.Artist;
                    fileData.Album = song.Title;
                    if (song.Genre.Any())
                        fileData.Genre = song.Genre.First().ToString();
                    fileData.Date = song.Year.ToString();

                    if (song.Artwork.Any())
                    {
                        string imageLocation = song.Artwork.First().Uri.ToString();
                        fileData.Cover.ImageLocation = song.Artwork.First().Uri.ToString();
                    }

                    listId3TagForAllFiles[nSong] = fileData;

                    InvokeToDataGrid(fileData, nSong);
                }
            }

            //_backgroundWorker.ReportProgress(100);
            //InvokeToProgressBarLabel("Completed");
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ActionToPerform value = (ActionToPerform)e.Argument;

                //if (value == ActionToPerform.RetrieveMetadata)
                //{
                //    RetrievedMetaDataFromService();
                //}
                //else if (value == ActionToPerform.LoadfilesRetrieveMetadata)
                //{
                //    InvokeToProgressBarLabel("Loading of Songs in Folder, Please Wait...");

                //    LoadFilesIntoGrid(songsDirectory);

                //    InvokeToProgressBarLabel("Retrieving Metadata for Songs from Online Service, Please Wait...");

                //    RetrievedMetaDataFromService();
                //}
                //else if (value == ActionToPerform.UpdateAll)
                //{
                //    InvokeToProgressBarLabel("Updating Metadata for All Songs, Please Wait...");

                //    UpdateAllSongsMetadata();
                //}
                //else
                {
                    InvokeToProgressBarLabel("Starting Loading of Songs, Please Wait...");

                    LoadFilesIntoGrid(songsDirectory);
                }
            }
            catch (System.Exception ex)
            {
                InvokeToProgressBarLabel(ex.ToString());
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
                dataGridSongFiles.Enabled = true;
            }

            lblProgressBar.Visible = false;
            EnableDisableControls(true);

        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void DataGridSongFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int nItems = dataGridSongFiles.Rows.Count;

                if (nItems > 0)
                {
                    selectedListIndex = dataGridSongFiles.CurrentRow.Index;

                    if (selectedListIndex < 0)
                    {
                        MessageBox.Show("Please select a song!", "No Song Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];

                    WriteToLogWindow("Lookup for " + metaData.Filepath);

                    txtNewAlbum.Text = "";
                    txtNewArtist.Text = "";
                    txtNewTitle.Text = "";
                    txtNewDate.Text = "";
                    txtNewGenre.Text = "";
                    picNewAlbumArt.Image = null;
                    picNewAlbumArt.ImageLocation = "";


                    MetadataService flag = 0;
                    ComboboxItem cbSelectedItem = (ComboboxItem)cbServiceSimpleLookup.SelectedItem;
                    switch (cbSelectedItem.Value)
                    {
                        case SelectedService.Amazon:
                            {
                                flag |= MetadataService.Amazon;
                                break;
                            }
                        case SelectedService.AudioDB:
                            {
                                flag |= MetadataService.AudioDB;
                                break;
                            }
                        case SelectedService.Deezer:
                            {
                                flag |= MetadataService.Deezer;
                                break;
                            }
                        case SelectedService.Discogs:
                            {
                                flag |= MetadataService.Discogs;
                                break;
                            }
                        case SelectedService.iTunes:
                            {
                                flag |= MetadataService.iTunes;
                                break;
                            }
                        case SelectedService.LastFM:
                            {
                                flag |= MetadataService.LastFM;
                                break;
                            }
                        case SelectedService.MusicBrainz:
                            {
                                flag |= MetadataService.MusicBrainz;
                                break;
                            }
                        case SelectedService.Napster:
                            {
                                flag |= MetadataService.Napster;
                                break;
                            }
                        case SelectedService.Spotify:
                            {
                                flag |= MetadataService.Spotify;
                                break;
                            }
                        case SelectedService.MusixMatch:
                            {
                                flag |= MetadataService.MusixMatch;
                                break;
                            }
                        case SelectedService.Galiboo:
                            {
                                flag |= MetadataService.Galiboo;
                                break;
                            }
                        case SelectedService.Genius:
                            {
                                flag |= MetadataService.Genius;
                                break;
                            }
                        default:
                            return;
                    }

                    WriteToLogWindow("Search for Data For " + metaData.Filepath);

                    SearchServiceForMetadata lookup = new SearchServiceForMetadata();
                    lookup.Limit = 1;
                    lookup.ListRetrievedMetadata = null;
                    if (lookup.CallServiceForMetadata(flag, metaData))
                    {
                        if (!String.IsNullOrEmpty(lookup.gSongMetaData.Album))
                            txtNewAlbum.Text = lookup.gSongMetaData.Album;
                        txtNewArtist.Text = lookup.gSongMetaData.Artist;
                        txtNewTitle.Text = lookup.gSongMetaData.Title;
                        if (!String.IsNullOrEmpty(lookup.gSongMetaData.Date))
                            txtNewDate.Text = lookup.gSongMetaData.Date;
                        if (!String.IsNullOrEmpty(lookup.gSongMetaData.Genre))
                            txtNewGenre.Text = lookup.gSongMetaData.Genre;
                        if (lookup.gSongMetaData.Cover != null && !String.IsNullOrEmpty(lookup.gSongMetaData.Cover.ImageLocation))
                        {
                            picNewAlbumArt.ImageLocation = lookup.gSongMetaData.Cover.ImageLocation;

                            var request = WebRequest.Create(picNewAlbumArt.ImageLocation);

                            using (var response = request.GetResponse())
                            using (var stream = response.GetResponseStream())
                            {
                                System.Drawing.Image currentImage = Bitmap.FromStream(stream);
                                picNewAlbumArt.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                            }

                        }
                    }

                    WriteToLogWindow("Search Completed!");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //try
            //{
            //    int nItems = dataGridSongFiles.Rows.Count;

            //    if (nItems > 0)
            //    {
            //        selectedListIndex = dataGridSongFiles.CurrentRow.Index;
            //        Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];

            //        txtID3Title.Text = metaData.Title;
            //        txtID3Artist.Text = metaData.Artist;
            //        txtID3Year.Text = metaData.Date;
            //        txtID3Album.Text = metaData.Album;
            //        txtID3Genre.Text = metaData.Genre;

            //        if (metaData.Cover.Image != null )
            //        {
            //            picID3AlbumArt.Image = metaData.Cover.Image;
            //        }
            //        else
            //        {
            //            picID3AlbumArt.Image = null;
            //        }

            //        songFileBeingProcessed = metaData.Filepath;

            //        txtNewTitle.Text = "";
            //        txtNewGenre.Text = "";
            //        txtNewDate.Text = "";
            //        txtNewArtist.Text = "";
            //        txtNewAlbum.Text = "";

            //        if (TotalTagger.BASS.BassLibrary.IsPaused())
            //        {
            //            TotalTagger.BASS.BassLibrary.Play();
            //            return;
            //        }

            //        TotalTagger.BASS.BassLibrary.LoadAudioFile(metaData.Filepath);
            //        TotalTagger.BASS.BassLibrary.Play();

            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void BtnUpdateAll_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult nYesOrNo = MessageBox.Show("This process will change ALL files and could take a long time to complete. \n\nAre you sure you want to update all files?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (nYesOrNo == DialogResult.Yes)
                {
                    InvokeToProgressBarLabel("Start of Metadata Update Process for All Songs");

                    PerformProcessingForSongFiles(ActionToPerform.UpdateAll);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void UpdateAllSongsMetadata()
        //{
        //    try
        //    {
        //        int nSong = 0;
        //        int nAllSongs = listId3TagForAllFiles.Count;

        //        for (nSong = 0; nSong < nAllSongs; nSong++)
        //        {
        //            InvokeToProgressBarLabel("Updating the Metadata for " + nSong + " of " + nAllSongs + " songs");
        //            Id3Tag metaData = listId3TagForAllFiles[nSong];
        //            UpdateMetadataForSong(true, metaData, nSong);
        //        }

        //        InvokeToProgressBarLabel("Completed");
        //    }
        //    catch (System.Exception ex)
        //    {
        //        InvokeToProgressBarLabel(ex.ToString());
        //    }
        //}

        private void EnableDisableControls(bool bEnable)
        {
            dataGridSongFiles.Enabled = bEnable;
        }

        private void DataGridSongFiles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (!dataGridSongFiles.Rows[e.RowIndex].IsNewRow)
                {
                    if (dataGridSongFiles.Columns[e.ColumnIndex].Name == GRID_SERVICE_METADATA)
                    {
                        if ((int)dataGridSongFiles.Rows[e.RowIndex].Cells[GRID_METADATA_PULLED].Value == 1)
                        {
                            if (!String.IsNullOrEmpty((string)dataGridSongFiles.Rows[e.RowIndex].Cells[GRID_RETRIEVED_TITLE].Value))
                            {
                                e.Value = Properties.Resources.check_mark_T;
                            }
                            else
                            {
                                e.Value = Properties.Resources.red_x;
                            }
                        }
                    }
                    //else if (dataGridSongFiles.Columns[e.ColumnIndex].Name == GRID_SERVICE_ARTWORK_HEADER)
                    //{
                    //    if ((int)dataGridSongFiles.Rows[e.RowIndex].Cells[GRID_METADATA_PULLED].Value == 1)
                    //    {
                    //        if ((int)dataGridSongFiles.Rows[e.RowIndex].Cells[GRID_RETRIEVED_ARTWORK].Value == 1)
                    //        {
                    //            e.Value = Properties.Resources.check_mark_T;
                    //        }
                    //        else
                    //        {
                    //            e.Value = Properties.Resources.red_x;
                    //        }
                    //    }
                    //}
                }
                else
                {
                    //if (dataGridSongFiles.Columns[e.ColumnIndex].Name == GRID_FILE_ARTWORK_HEADER)
                    //{
                    //    e.Value = Properties.Resources.blank;
                    //}

                    //if (dataGridSongFiles.Columns[e.ColumnIndex].Name == GRID_SERVICE_ARTWORK_HEADER)
                    //{
                    //    e.Value = Properties.Resources.blank;
                    //}

                    //if (dataGridSongFiles.Columns[e.ColumnIndex].Name == GRID_SERVICE_METADATA)
                    //{
                    //    e.Value = Properties.Resources.blank;
                    //}

                }
            }
            catch (System.Exception ex)
            {
                InvokeToProgressBarLabel(ex.ToString());
            }

        }

        private void DataGridSongFiles_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                    if (!c.Selected)
                    {
                        c.DataGridView.ClearSelection();
                        c.DataGridView.CurrentCell = c;
                        c.Selected = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                InvokeToProgressBarLabel(ex.ToString());
            }
        }

        private void QueryForAllResultsFromOnlineServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int nItems = dataGridSongFiles.Rows.Count;

                if (nItems > 0)
                {
                    selectedListIndex = dataGridSongFiles.CurrentRow.Index;
                    Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];

                    DisplayMetadataOnAllMatches displayAllForm = new DisplayMetadataOnAllMatches(metaData.Filepath, metaData);
                    DialogResult result = displayAllForm.ShowDialog();

                    if( result == DialogResult.OK)
                    {
                        metaData = displayAllForm.gSongMetaData;

                        listId3TagForAllFiles[selectedListIndex] = metaData;
                        txtID3Title.Text = metaData.Title;
                        txtID3Artist.Text = metaData.Artist;
                        txtID3Year.Text = metaData.Date;
                        txtID3Album.Text = metaData.Album;
                        txtID3Genre.Text = metaData.Genre;

                        picID3AlbumArt.Image = null;
                        if (metaData.Cover != null && !String.IsNullOrEmpty(metaData.Cover.ImageLocation))
                        {
                            var request = WebRequest.Create(metaData.Cover.ImageLocation);

                            using (var response = request.GetResponse())
                            using (var stream = response.GetResponseStream())
                            {
                                System.Drawing.Image currentImage = Bitmap.FromStream(stream);
                                picID3AlbumArt.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                            }
                        }

                        InvokeToDataGrid(metaData, selectedListIndex);
                    }

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedDirectory = BrowseForFolder(lblFolder.Text);

            if (!String.IsNullOrEmpty(selectedDirectory))
            {
                lblFolder.Text = selectedDirectory;
                if (songsDirectory != lblFolder.Text)
                {
                    songsDirectory = lblFolder.Text;
                    if (!songsDirectory.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
                    {
                        songsDirectory += Path.DirectorySeparatorChar;
                    }

                    songList = GetFiles(songsDirectory, extension, SearchOption.AllDirectories);

                    if (songList != null && songList.Count() == 0)
                    {
                        return;
                    }

                    PerformProcessingForSongFiles(ActionToPerform.LoadFiles);
                }
            }

        }

        private void searchForMetadataForSelectedAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int nItems = dataGridSongFiles.SelectedRows.Count;

                if (nItems > 0)
                {
                    selectedListIndex = dataGridSongFiles.SelectedRows[0].Index;
                    Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];
                    DisplayMetadataOnAllMatches displayAllForm = new DisplayMetadataOnAllMatches(metaData.Filepath, metaData);
                    DialogResult result = displayAllForm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        metaData = displayAllForm.gSongMetaData;

                        listId3TagForAllFiles[selectedListIndex] = metaData;
                        txtID3Title.Text = metaData.Title;
                        txtID3Artist.Text = metaData.Artist;
                        txtID3Year.Text = metaData.Date;
                        if(metaData.Album.Length > 0 )
                            txtID3Album.Text = metaData.Album;
                        if( metaData.Genre.Length > 0 )
                            txtID3Genre.Text = metaData.Genre;

                        picID3AlbumArt.Image = null;
                        if (metaData.Cover != null && !String.IsNullOrEmpty(metaData.Cover.ImageLocation))
                        {
                            var request = WebRequest.Create(metaData.Cover.ImageLocation);

                            using (var response = request.GetResponse())
                            using (var stream = response.GetResponseStream())
                            {
                                System.Drawing.Image currentImage = Bitmap.FromStream(stream);
                                picID3AlbumArt.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                                picID3AlbumArt.ImageLocation = metaData.Cover.ImageLocation;
                            }
                        }

                        //UpdateMetadataForSong(true, metaData, selectedListIndex);
                        string writeResult = ReadWriteID3.WriteID3Tags(metaData.Filepath, metaData);
                        if(writeResult.Length > 0 )
                        {
                            MessageBox.Show(writeResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        metaData.Title =  metaData.Title;
                        metaData.Artist = metaData.Artist;
                        metaData.Album = metaData.Album;
                        metaData.Date = metaData.Date;
                        metaData.Genre = metaData.Genre;

                        if (!String.IsNullOrEmpty(metaData.Cover.ImageLocation))
                        {
                            metaData.Cover = picID3AlbumArt;
                        }

                        listId3TagForAllFiles[selectedListIndex] = metaData;
    
                        InvokeToDataGrid(metaData, selectedListIndex);
                    }

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btnSaveMetadata_Click(object sender, EventArgs e)
        {
            try
            {
                Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];
                metaData.Title = txtID3Title.Text;

                metaData.Artist = txtID3Artist.Text;
                metaData.Date = txtID3Year.Text;
                metaData.Album = txtID3Album.Text;
                metaData.Genre = txtID3Genre.Text;

                metaData.Cover.Image = picID3AlbumArt.Image;
                metaData.Cover.ImageLocation = picID3AlbumArt.ImageLocation;

                string writeResult = ReadWriteID3.WriteID3Tags(metaData.Filepath, metaData);
                if (writeResult.Length > 0)
                {
                    MessageBox.Show(writeResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                metaData.Title = metaData.Title;
                metaData.Artist = metaData.Artist;
                metaData.Album = metaData.Album;
                metaData.Date = metaData.Date;
                metaData.Genre = metaData.Genre;

                metaData.Cover.Image = metaData.Cover.Image;
                metaData.Cover.ImageLocation = metaData.Cover.ImageLocation;

                listId3TagForAllFiles[selectedListIndex] = metaData;

                InvokeToDataGrid(metaData, selectedListIndex);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchLastFMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string encodedArtist = System.Web.HttpUtility.UrlEncode(txtID3Artist.Text.TrimEnd());
            string encodedAlbum = System.Web.HttpUtility.UrlEncode(txtID3Album.Text);
            
            try
            {
                using (WebClient client = new WebClient())
                {
                    string query = Properties.Settings.Default.LastFMAlbumGetInto + MainWindow.serviceSettings.LastFMClientKey +
                        "&format=json&autocorrect=1&artist=" + encodedArtist + "&album=" + encodedAlbum;

                    string result = client.DownloadString(query);

                    if (result != null)
                    {
                        var json = (JObject)JsonConvert.DeserializeObject(result);

                        if (json != null)
                        {
                            var album = json["album"];
                            if (album != null)
                            {
                                var images = album["image"];
                                if (images != null)
                                {
                                    foreach (var image in images)
                                    {
                                        if (image["size"].ToString().Equals("medium"))
                                        {
                                            string ImageLocation = image["#text"].ToString();
                                            var request = WebRequest.Create(ImageLocation);

                                            using (var response = request.GetResponse())
                                            using (var stream = response.GetResponseStream())
                                            {
                                                System.Drawing.Image currentImage = Bitmap.FromStream(stream);
                                                picID3AlbumArt.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                                                picID3AlbumArt.ImageLocation = ImageLocation;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void removeAlbumArtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picID3AlbumArt.Image = null;
            picID3AlbumArt.ImageLocation = "";
        }

        #region UnusedCode

        #endregion



        private void toolStripButtonAlbumArt_Click(object sender, EventArgs e)
        {
            string encodedArtist = System.Web.HttpUtility.UrlEncode(txtID3Artist.Text.TrimEnd());
            string encodedAlbum = System.Web.HttpUtility.UrlEncode(txtID3Album.Text);

            try
            {
                using (WebClient client = new WebClient())
                {
                    string query = Properties.Settings.Default.LastFMAlbumGetInto + MainWindow.serviceSettings.LastFMClientKey +
                        "&format=json&autocorrect=1&artist=" + encodedArtist + "&album=" + encodedAlbum;

                    string result = client.DownloadString(query);

                    if (result != null)
                    {
                        var json = (JObject)JsonConvert.DeserializeObject(result);

                        if (json != null)
                        {
                            var album = json["album"];
                            if (album != null)
                            {
                                var images = album["image"];
                                if (images != null)
                                {
                                    foreach (var image in images)
                                    {
                                        if (image["size"].ToString().Equals("medium"))
                                        {
                                            string ImageLocation = image["#text"].ToString();
                                            var request = WebRequest.Create(ImageLocation);

                                            using (var response = request.GetResponse())
                                            using (var stream = response.GetResponseStream())
                                            {
                                                System.Drawing.Image currentImage = Bitmap.FromStream(stream);
                                                picID3AlbumArt.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                                                picID3AlbumArt.ImageLocation = ImageLocation;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void toolStripButtonRemoveTags_Click(object sender, EventArgs e)
        {

        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            string selectedDirectory = BrowseForFolder(lblFolder.Text);

            if (!String.IsNullOrEmpty(selectedDirectory))
            {
                lblFolder.Text = selectedDirectory;
                if (songsDirectory != lblFolder.Text)
                {
                    songsDirectory = lblFolder.Text;
                    songList = GetFiles(songsDirectory, extension, SearchOption.AllDirectories);

                    if (songList != null && songList.Count() == 0)
                    {
                        return;
                    }

                    PerformProcessingForSongFiles(ActionToPerform.LoadFiles);
                }
            }
        }

        private void btnOpenFiles_Click(object sender, EventArgs e)
        {
            string selectedDirectory = BrowseForFolder(lblFolder.Text);

            if (!String.IsNullOrEmpty(selectedDirectory))
            {
                lblFolder.Text = selectedDirectory;
                if (songsDirectory != lblFolder.Text)
                {
                    songsDirectory = lblFolder.Text;
                    songList = GetFiles(songsDirectory, extension, SearchOption.AllDirectories);

                    if (songList != null && songList.Count() == 0)
                    {
                        return;
                    }

                    PerformProcessingForSongFiles(ActionToPerform.LoadFiles);
                }
            }

        }

        private void btnAdvancedLookup_Click(object sender, EventArgs e)
        {
            try
            {

                int nItems = dataGridSongFiles.SelectedRows.Count;

                if (nItems > 0)
                {
                    selectedListIndex = dataGridSongFiles.SelectedRows[0].Index;
                    Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];
                    DisplayMetadataOnAllMatches displayAllForm = new DisplayMetadataOnAllMatches(metaData.Filepath, metaData);
                    DialogResult result = displayAllForm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        metaData = displayAllForm.gSongMetaData;

                        listId3TagForAllFiles[selectedListIndex] = metaData;
                        txtID3Title.Text = metaData.Title;
                        txtID3Artist.Text = metaData.Artist;
                        txtID3Year.Text = metaData.Date;
                        if (metaData.Album.Length > 0)
                            txtID3Album.Text = metaData.Album;
                        if (metaData.Genre.Length > 0)
                            txtID3Genre.Text = metaData.Genre;

                        picID3AlbumArt.Image = null;
                        if (metaData.Cover != null && !String.IsNullOrEmpty(metaData.Cover.ImageLocation))
                        {
                            var request = WebRequest.Create(metaData.Cover.ImageLocation);

                            using (var response = request.GetResponse())
                            using (var stream = response.GetResponseStream())
                            {
                                System.Drawing.Image currentImage = Bitmap.FromStream(stream);
                                picID3AlbumArt.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                                picID3AlbumArt.ImageLocation = metaData.Cover.ImageLocation;
                            }
                        }

                        //UpdateMetadataForSong(true, metaData, selectedListIndex);
                        string writeResult = ReadWriteID3.WriteID3Tags(metaData.Filepath, metaData);
                        if (writeResult.Length > 0)
                        {
                            MessageBox.Show(writeResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        //metaData.Title = metaData.Title;
                        //metaData.Artist = metaData.Artist;
                        //metaData.Album = metaData.Album;
                        //metaData.Date = metaData.Date;
                        //metaData.Genre = metaData.Genre;

                        //if (!String.IsNullOrEmpty(metaData.Cover.ImageLocation))
                        //{
                        //    metaData.Cover = picID3AlbumArt;
                        //    metaData.Cover = 
                        //}

                        listId3TagForAllFiles[selectedListIndex] = metaData;

                        InvokeToDataGrid(metaData, selectedListIndex);
                    }

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSaveTags_Click(object sender, EventArgs e)
        {
            try
            {

                int nItems = dataGridSongFiles.Rows.Count;

                if (nItems > 0)
                {
                    selectedListIndex = dataGridSongFiles.CurrentRow.Index;

                    if (selectedListIndex < 0)
                    {
                        MessageBox.Show("Please select a song!", "No Song Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if(txtNewTitle.Text.Length == 0 && 
                       txtNewAlbum.Text.Length == 0 &&
                       txtNewArtist.Text.Length == 0 &&
                       txtNewDate.Text.Length == 0 &&
                       txtNewGenre.Text.Length == 0 )
                    {
                        MessageBox.Show("No new metadata to write to file!", "No New Metadata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];
                    WriteToLogWindow("Saving Tags for " + metaData.Filepath);
                    metaData.Title = txtNewTitle.Text;

                    metaData.Artist = txtNewArtist.Text;
                    metaData.Date = txtNewDate.Text;
                    metaData.Album = txtNewAlbum.Text;
                    metaData.Genre = txtNewGenre.Text;

                    metaData.Cover.Image = picNewAlbumArt.Image;
                    metaData.Cover.ImageLocation = picNewAlbumArt.ImageLocation;
                    SaveChangesToID3(metaData, false);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void SaveChangesToID3(Id3Tag metaData, bool RemoveTags)
        { 
            try
            {
                string writeResult;
                if ( RemoveTags)
                {
                    writeResult = ReadWriteID3.RemoveID3Tags(metaData.Filepath);
                    if (writeResult.Length > 0)
                    {
                        WriteToLogWindow(writeResult);
                        MessageBox.Show(writeResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    WriteToLogWindow("Tags Removed For " + metaData.Filepath);
                }
                else
                {
                    writeResult = ReadWriteID3.WriteID3Tags(metaData.Filepath, metaData);
                    if (writeResult.Length > 0  )
                    {
                        WriteToLogWindow(writeResult);
                        MessageBox.Show(writeResult,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    WriteToLogWindow("New Tags Saved for " + metaData.Filepath);
                }
                metaData.Title = metaData.Title;
                metaData.Artist = metaData.Artist;
                metaData.Album = metaData.Album;
                metaData.Date = metaData.Date;
                metaData.Genre = metaData.Genre;

                metaData.Cover.Image = metaData.Cover.Image;
                metaData.Cover.ImageLocation = metaData.Cover.ImageLocation;

                listId3TagForAllFiles[selectedListIndex] = metaData;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnBestMatch_Click(object sender, EventArgs e)
        {
            try
            {
                int nItems = dataGridSongFiles.SelectedRows.Count;

                if (nItems > 0)
                {
                    List<BestMatch> listSelectedSongs = new List<BestMatch>();
                    for(int item=0; item < nItems; item++)
                    {
                        selectedListIndex = dataGridSongFiles.SelectedRows[item].Index;
                        Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];
                        BestMatch match = new BestMatch();
                        match.SongData = metaData;
                        match.Index = item;
                        listSelectedSongs.Add(match);
                    }

                    if( listSelectedSongs.Any())
                    {
                        SingleLookupForm bestMatchForm = new SingleLookupForm(songsDirectory, listSelectedSongs);
                        DialogResult result = bestMatchForm.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSimpleLookup_Click(object sender, EventArgs e)
        {
            try
            {

                int nItems = dataGridSongFiles.Rows.Count;

                if (nItems > 0)
                {
                    selectedListIndex = dataGridSongFiles.CurrentRow.Index;

                    if (selectedListIndex < 0)
                    {
                        MessageBox.Show("Please select a song!", "No Song Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];

                    WriteToLogWindow("Lookup for " + metaData.Filepath);

                    txtNewAlbum.Text = "";
                    txtNewArtist.Text = "";
                    txtNewTitle.Text = "";
                    txtNewDate.Text = "";
                    txtNewGenre.Text = "";
                    picNewAlbumArt.Image = null;
                    picNewAlbumArt.ImageLocation = "";


                    MetadataService flag = 0;
                    ComboboxItem cbSelectedItem = (ComboboxItem)cbServiceSimpleLookup.SelectedItem;
                    switch (cbSelectedItem.Value)
                    {
                        case SelectedService.Amazon:
                            {
                                flag |= MetadataService.Amazon;
                                break;
                            }
                        case SelectedService.AudioDB:
                            {
                                flag |= MetadataService.AudioDB;
                                break;
                            }
                        case SelectedService.Deezer:
                            {
                                flag |= MetadataService.Deezer;
                                break;
                            }
                        case SelectedService.Discogs:
                            {
                                flag |= MetadataService.Discogs;
                                break;
                            }
                        case SelectedService.iTunes:
                            {
                                flag |= MetadataService.iTunes;
                                break;
                            }
                        case SelectedService.LastFM:
                            {
                                flag |= MetadataService.LastFM;
                                break;
                            }
                        case SelectedService.MusicBrainz:
                            {
                                flag |= MetadataService.MusicBrainz;
                                break;
                            }
                        case SelectedService.Napster:
                            {
                                flag |= MetadataService.Napster;
                                break;
                            }
                        case SelectedService.Spotify:
                            {
                                flag |= MetadataService.Spotify;
                                break;
                            }
                        case SelectedService.MusixMatch:
                            {
                                flag |= MetadataService.MusixMatch;
                                break;
                            }
                        case SelectedService.Galiboo:
                            {
                                flag |= MetadataService.Galiboo;
                                break;
                            }
                        case SelectedService.Genius:
                            {
                                flag |= MetadataService.Genius;
                                break;
                            }
                        default:
                            return;
                    }

                    WriteToLogWindow("Search for Data For " + metaData.Filepath);
                    SearchServiceForMetadata lookup = new SearchServiceForMetadata();
                    lookup.Limit = 1;
                    lookup.ListRetrievedMetadata = null;
                    if ( lookup.CallServiceForMetadata(flag, metaData) )
                    {
                        if (!String.IsNullOrEmpty(lookup.gSongMetaData.Album))
                            txtNewAlbum.Text = lookup.gSongMetaData.Album;
                        txtNewArtist.Text = lookup.gSongMetaData.Artist;
                        txtNewTitle.Text = lookup.gSongMetaData.Title;
                        if (!String.IsNullOrEmpty(lookup.gSongMetaData.Date))
                            txtNewDate.Text = lookup.gSongMetaData.Date;
                        if (!String.IsNullOrEmpty(lookup.gSongMetaData.Genre))
                            txtNewGenre.Text = lookup.gSongMetaData.Genre;
                        if (lookup.gSongMetaData.Cover != null && !String.IsNullOrEmpty(lookup.gSongMetaData.Cover.ImageLocation))
                        {
                            picNewAlbumArt.ImageLocation = lookup.gSongMetaData.Cover.ImageLocation;

                            var request = WebRequest.Create(picNewAlbumArt.ImageLocation);

                            using (var response = request.GetResponse())
                            using (var stream = response.GetResponseStream())
                            {
                                System.Drawing.Image currentImage = Bitmap.FromStream(stream);
                                picNewAlbumArt.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                            }
                        }
                    }
                    WriteToLogWindow("Search Completed!");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveTags_Click(object sender, EventArgs e)
        {
            try
            {

                int nItems = dataGridSongFiles.Rows.Count;

                if (nItems > 0)
                {
                    selectedListIndex = dataGridSongFiles.CurrentRow.Index;

                    if (selectedListIndex < 0)
                    {
                        MessageBox.Show("Please select a song!", "No Song Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];
                    WriteToLogWindow("Removing Tags For " + metaData.Filepath);
                    metaData.Title = "";

                    metaData.Artist = "";
                    metaData.Date = "";
                    metaData.Album = "";
                    metaData.Genre = "";

                    metaData.Cover.Image = null;
                    metaData.Cover.ImageLocation = "";
                    SaveChangesToID3(metaData, true);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFindAlbums_Click(object sender, EventArgs e)
        {
            WriteToLogWindow("Searching For Albums");
            //search files for missing albums
            AlbumSearchForm missingAlbumForm = new AlbumSearchForm(songsDirectory);
            missingAlbumForm.ListAllSongs = listId3TagForAllFiles;
            if( DialogResult.OK == missingAlbumForm.ShowDialog())
            {
                dataGridSongFiles.Refresh();
            }
            WriteToLogWindow("Search For Albums Completed");
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void WriteToLogWindow(string text)
        {
            txtLogging.Text = DateTime.Now.ToShortTimeString() + " " + text + "\r\n" + txtLogging.Text;
        }

        private void label13_Click_1(object sender, EventArgs e)
        {

        }

        private void txtLogging_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridSongFiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int nItems = dataGridSongFiles.Rows.Count;

                if (nItems > 0)
                {
                    selectedListIndex = dataGridSongFiles.CurrentRow.Index;
                    Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];

                    txtID3Title.Text = metaData.Title;
                    txtID3Artist.Text = metaData.Artist;
                    txtID3Year.Text = metaData.Date;
                    txtID3Album.Text = metaData.Album;
                    txtID3Genre.Text = metaData.Genre;

                    if (metaData.Cover.Image != null)
                    {
                        picID3AlbumArt.Image = metaData.Cover.Image;
                    }
                    else
                    {
                        picID3AlbumArt.Image = null;
                    }

                    songFileBeingProcessed = metaData.Filepath;

                    txtNewTitle.Text = "";
                    txtNewGenre.Text = "";
                    txtNewDate.Text = "";
                    txtNewArtist.Text = "";
                    txtNewAlbum.Text = "";
                    picNewAlbumArt.Image = null;

                    if( TotalTagger.BASS.BassLibrary.IsPlaying())
                    {
                        TotalTagger.BASS.BassLibrary.StopPlayer();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                int nItems = dataGridSongFiles.Rows.Count;

                if (nItems > 0)
                {
                    selectedListIndex = dataGridSongFiles.CurrentRow.Index;
                    Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];

                    TotalTagger.BASS.BassLibrary.LoadAudioFile(metaData.Filepath);
                    TotalTagger.BASS.BassLibrary.Play();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static long GetPosition()
        {
            //return Bass.BASS_ChannelGetPosition(streamHandle);
            return -1;
        }

        public static void SetPosition(long pos)
        {
            //Bass.BASS_ChannelSetPosition(streamHandle, pos);
        }

        public static long GetLength()
        {
            //return Bass.BASS_ChannelGetLength(streamHandle);
            return -1;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {

                int nItems = dataGridSongFiles.Rows.Count;

                if (nItems > 0)
                {
                    selectedListIndex = dataGridSongFiles.CurrentRow.Index;
                    Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];

                    if(TotalTagger.BASS.BassLibrary.IsPaused())
                    {
                        TotalTagger.BASS.BassLibrary.Play();
                        return;
                    }

                    TotalTagger.BASS.BassLibrary.LoadAudioFile(metaData.Filepath);
                    TotalTagger.BASS.BassLibrary.Play();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void FlushTimeListened()
        {
            if (timeListenedTracker.IsRunning) timeListenedTracker.Restart();
            else timeListenedTracker.Reset();
        }

        void playbackEnded(int handle, int channel, int data, IntPtr user)
        {
            timeListenedTracker.Stop();
            //if (streamLoaded)
            //{
            //    if (repeat < 2) advanceFlag = true;
            //}
        }

        void playbackStalled(int handle, int channel, int data, IntPtr user)
        {
            if ((int)data == 1) timeListenedTracker.Start();
            else timeListenedTracker.Stop();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            TotalTagger.BASS.BassLibrary.PausePlayer();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            TotalTagger.BASS.BassLibrary.StopPlayer();
        }

        private void progressRefreshTimer_Tick(object sender, EventArgs e)
        {
            if (streamLoaded)
            {
                int val = (int)(((double)GetPosition() / (double)GetLength()) * playProgressBar.Maximum);
                if (val > playProgressBar.Maximum) val = playProgressBar.Maximum;
                if (val < playProgressBar.Minimum) val = playProgressBar.Minimum;
                playProgressBar.Value = val;
                playProgressBar.Refresh();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }

        private void btnPlayFile_Click(object sender, EventArgs e)
        {
            try
            {
                int nItems = dataGridSongFiles.Rows.Count;

                if (nItems > 0)
                {
                    selectedListIndex = dataGridSongFiles.CurrentRow.Index;
                    Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];

                    txtID3Title.Text = metaData.Title;
                    txtID3Artist.Text = metaData.Artist;
                    txtID3Year.Text = metaData.Date;
                    txtID3Album.Text = metaData.Album;
                    txtID3Genre.Text = metaData.Genre;

                    if (metaData.Cover.Image != null)
                    {
                        picID3AlbumArt.Image = metaData.Cover.Image;
                    }
                    else
                    {
                        picID3AlbumArt.Image = null;
                    }

                    songFileBeingProcessed = metaData.Filepath;

                    txtNewTitle.Text = "";
                    txtNewGenre.Text = "";
                    txtNewDate.Text = "";
                    txtNewArtist.Text = "";
                    txtNewAlbum.Text = "";

                    if (TotalTagger.BASS.BassLibrary.IsPaused())
                    {
                        TotalTagger.BASS.BassLibrary.Play();
                        return;
                    }

                    TotalTagger.BASS.BassLibrary.LoadAudioFile(metaData.Filepath);
                    TotalTagger.BASS.BassLibrary.Play();

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int nItems = dataGridSongFiles.Rows.Count;

                if (nItems > 0)
                {
                    selectedListIndex = dataGridSongFiles.CurrentRow.Index;

                    if( selectedListIndex >= 0 )
                    {
                        if( DialogResult.No == MessageBox.Show("Once a file is deleted, it might not be able to be recovered.\n\n Do you want to proceed with delete?", "Are You Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) )
                        {
                            return;
                        }

                        Id3Tag metaData = listId3TagForAllFiles[selectedListIndex];
                        File.Delete(metaData.Filepath);
                        listId3TagForAllFiles.RemoveAt(selectedListIndex);

                        txtID3Title.Text = "";
                        txtID3Artist.Text = "";
                        txtID3Year.Text = "";
                        txtID3Album.Text = "";
                        txtID3Genre.Text = "";

                        picID3AlbumArt.Image = null;

                        txtNewTitle.Text = "";
                        txtNewGenre.Text = "";
                        txtNewDate.Text = "";
                        txtNewArtist.Text = "";
                        txtNewAlbum.Text = "";

                        dataGridSongFiles.Refresh();
                    }

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnRefreshList_Click(object sender, EventArgs e)
        {
            txtID3Title.Text = "";
            txtID3Artist.Text = "";
            txtID3Year.Text = "";
            txtID3Album.Text = "";
            txtID3Genre.Text = "";

            picID3AlbumArt.Image = null;

            txtNewTitle.Text = "";
            txtNewGenre.Text = "";
            txtNewDate.Text = "";
            txtNewArtist.Text = "";
            txtNewAlbum.Text = "";

            PerformProcessingForSongFiles(ActionToPerform.LoadFiles);
        }

        private void BtnFindDuplicates_Click(object sender, EventArgs e)
        {
            try
            {
                FindDuplicatesForm duplicateSongs = new FindDuplicatesForm(listId3TagForAllFiles);
                DialogResult dlg = duplicateSongs.ShowDialog();
                if (dlg == DialogResult.OK)
                {
                    foreach (var song in duplicateSongs.DeletedSongs)
                    {
                        listId3TagForAllFiles.Remove(song);
                    }

                    dataGridSongFiles.Refresh();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Look What Happened!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCopyOldToNew_Click(object sender, EventArgs e)
        {
            if( txtID3Title.Text.Length > 0 && txtID3Artist.Text.Length > 0)
            {
                txtNewTitle.Text = txtID3Title.Text;
                txtNewArtist.Text = txtID3Artist.Text;
                if(txtID3Album.Text.Length > 0 )
                    txtNewAlbum.Text = txtID3Album.Text;
                if (txtID3Genre.Text.Length > 0)
                    txtNewGenre.Text = txtID3Genre.Text;
                if (txtID3Year.Text.Length > 0)
                    txtNewDate.Text = txtID3Year.Text;
                if( picID3AlbumArt.Image != null )
                {
                    picNewAlbumArt.Image = null;
                    picNewAlbumArt.Image = picID3AlbumArt.Image;
                }
            }
        }

        private void btnSettings_Click_1(object sender, EventArgs e)
        {
            SettingsForm settingsWindows = new SettingsForm(serviceSettings);
            if( DialogResult.OK == settingsWindows.ShowDialog())
            {
                LoadAppSettings();
                LoadServiceComboBox();
            }
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
