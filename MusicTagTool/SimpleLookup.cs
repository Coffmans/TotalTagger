//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace TotalTagger
//{
//    class SimpleLookup
//    {
//        public Id3Tag gSongMetaData { get; set; }

//        public Id3Tag gReturnedSongData { get; set; }

//        private MetadataService flagMetadataService = 0;

//        CancellationTokenSource source = new CancellationTokenSource();
//        CancellationToken cancellationToken = new CancellationToken();

//        private bool success = false;
//        public bool Success
//        {
//            get { return success; }
//            set { success = value; }
//        }


//        private bool running = false;
//        public bool Running
//        {
//            get { return running; }
//            set { running = value; }
//        }

//        public SimpleLookup(Id3Tag selectedSong, MetadataService serviceToUse)
//        {
//            gSongMetaData = selectedSong;
//            flagMetadataService = serviceToUse;
//        }

//        public void CallService()
//        {
//            if (flagMetadataService.HasFlag(MetadataService.MusicBrainz))
//            {
//                success = RetrieveMetadataFromMusicBrainz();
//            }
//            else if (flagMetadataService.HasFlag(MetadataService.iTunes))
//            {
//                success = RetrieveMetadataFromiTunes();
//            }
//            else if (flagMetadataService.HasFlag(MetadataService.Napster))
//            {
//                success = RetrieveMetadataFromNapster();
//            }
//            else if (flagMetadataService.HasFlag(MetadataService.Deezer))
//            {
//                success = RetrieveMetadataFromDeezer();
//            }
//            else if (flagMetadataService.HasFlag(MetadataService.Discogs))
//            {
//                success = RetrieveMetadataFromDiscogs();
//            }
//            else if (flagMetadataService.HasFlag(MetadataService.LastFM))
//            {
//                success = RetrieveMetadataFromLastFM();
//            }
//            else if (flagMetadataService.HasFlag(MetadataService.Discogs))
//            {
//                success = RetrieveMetadataFromDiscogs();
//            }
//            else if (flagMetadataService.HasFlag(MetadataService.Spotify))
//            {
//                success = RetrieveMetadataFromSpotify();
//            }
//            else if (flagMetadataService.HasFlag(MetadataService.MusixMatch))
//            {
//                success = RetrieveMetadataFromMusixMatch();
//            }
//        }

//        private bool RetrieveMetadataFromMusicBrainz()
//        {
//            MusicBrainz queryMusicBrainz = new MusicBrainz();
//            queryMusicBrainz.ExistingMetadata = gSongMetaData;
//            queryMusicBrainz.QueryForMetadataNonAsync(cancellationToken,  1);
//            if (queryMusicBrainz.ResultOfQuery)
//            {
//                gReturnedSongData = queryMusicBrainz.RetrievedMetadata;
//                return true;
//            }
//            return false;
//        }

//        private bool RetrieveMetadataFromNapster()
//        {
//            Napster queryNapster = new Napster();
//            queryNapster.ExistingMetadata = gSongMetaData;
//            queryNapster.QueryForMetadataNonAsync(cancellationToken, false, 1);
//            if (queryNapster.ResultOfQuery)
//            {
//                gReturnedSongData = queryNapster.RetrievedMetadata;
//                return true;
//            }
//            return false;
//        }

//        private bool RetrieveMetadataFromDeezer()
//        {

//            Deezer queryDeezer = new Deezer();
//            queryDeezer.ExistingMetadata = gSongMetaData;
//            queryDeezer.QueryForMetadataNonAsync(cancellationToken, false, 1);
//            if (queryDeezer.ResultOfQuery)
//            {
//                gReturnedSongData = queryDeezer.RetrievedMetadata;
//                return true;
//            }
//            return false;
//        }

//        private bool RetrieveMetadataFromMusixMatch()
//        {

//            MusixMatch queryMusixMatch = new MusixMatch();
//            queryMusixMatch.ExistingMetadata = gSongMetaData;
//            queryMusixMatch.QueryForMetadataNonAsync(cancellationToken, false, 1);
//            if (queryMusixMatch.ResultOfQuery)
//            {
//                gReturnedSongData = queryMusixMatch.RetrievedMetadata;
//                return true;
//            }
//            return false;
//        }


//        private bool RetrieveMetadataFromiTunes()
//        {
//            ITunes queryITunes = new ITunes();
//            queryITunes.ExistingMetadata = gSongMetaData;
//            queryITunes.QueryForMetadataNonAsync(cancellationToken, false, 1);
//            if (queryITunes.ResultOfQuery)
//            {
//                gReturnedSongData = queryITunes.RetrievedMetadata;
//                return true;
//            }
//            return false;
//        }

//        private bool RetrieveMetadataFromLastFM()
//        {
//            LastFM queryLastFM = new LastFM();
//            queryLastFM.ExistingMetadata = gSongMetaData;
//            queryLastFM.QueryForMetadataNonAsync(cancellationToken, 1);
//            if (queryLastFM.ResultOfQuery)
//            {
//                gReturnedSongData = queryLastFM.RetrievedMetadata;
//                return true;
//            }
//            return false;
//        }

//        private bool RetrieveMetadataFromDiscogs()
//        {
//            Discogs queryDiscogs = new Discogs();
//            queryDiscogs.ExistingMetadata = gSongMetaData;
//            queryDiscogs.QueryForMetadataNonAsync(cancellationToken, 1);
//            if (queryDiscogs.ResultOfQuery)
//            {
//                gReturnedSongData = queryDiscogs.RetrievedMetadata;
//                return true;
//            }
//            return false;
//        }

//        private bool RetrieveMetadataFromSpotify()
//        {
//            Spotify querySpotify = new Spotify();
//            querySpotify.ExistingMetadata = gSongMetaData;
//            querySpotify.QueryForMetadataNonAsync(cancellationToken, 1);
//            if (querySpotify.ResultOfQuery)
//            {
//                gReturnedSongData = querySpotify.RetrievedMetadata;
//                return true;
//            }
//            return false;
//        }
//    }
//}
