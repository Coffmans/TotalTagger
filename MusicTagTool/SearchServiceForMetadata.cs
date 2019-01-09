using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TotalTagger
{
    class SearchServiceForMetadata
    {
        private BindingList<Id3Tag> listRetrievedMetadata = new BindingList<Id3Tag>();
        public BindingList<Id3Tag> ListRetrievedMetadata
        {
            get { return listRetrievedMetadata; }
            set { listRetrievedMetadata = value; }
        }

        public Id3Tag gSongMetaData { get; set; }

        CancellationToken cancellationToken = new CancellationToken();

        public string Result = "";

        public int Limit { get; set; }

        public bool CallServiceForMetadata(MetadataService metadataService, Id3Tag songToSearch)
        {
            switch(metadataService)
            {
                case MetadataService.MusicBrainz:
                    return RetrieveMetadataFromMusicBrainz(songToSearch);
                case MetadataService.Genius:
                    return RetrieveMetadataFromGenius(songToSearch);
                case MetadataService.Napster:
                    return RetrieveMetadataFromNapster(songToSearch);
                case MetadataService.Deezer:
                    return RetrieveMetadataFromDeezer(songToSearch);
                case MetadataService.Discogs:
                    return RetrieveMetadataFromDiscogs(songToSearch);
                case MetadataService.MusixMatch:
                    return RetrieveMetadataFromMusixMatch(songToSearch);
                case MetadataService.iTunes:
                    return RetrieveMetadataFromItunes(songToSearch);
                case MetadataService.LastFM:
                    return RetrieveMetadataFromLastFM(songToSearch);
                case MetadataService.Spotify:
                    return RetrieveMetadataFromSpotify(songToSearch);
                case MetadataService.Galiboo:
                    return RetrieveMetadataFromGaliboo(songToSearch);
            }

            return false;
           
        }

        private bool RetrieveMetadataFromMusicBrainz(Id3Tag songToSearch)
        {
            MusicBrainz queryMusicBrainz = new MusicBrainz();
            queryMusicBrainz.ExistingMetadata = songToSearch;
            if (Limit > 1 && listRetrievedMetadata != null)
                queryMusicBrainz.ListRetrievedTags = listRetrievedMetadata;
            
            queryMusicBrainz.QueryForMetadataNonAsync(cancellationToken, Limit);
            Result = queryMusicBrainz.QueryResult;
            if (queryMusicBrainz.ResultOfQuery)
            {
                if( Limit == 1)
                {
                    gSongMetaData = queryMusicBrainz.RetrievedMetadata;
                }
            }

            return queryMusicBrainz.ResultOfQuery;
        }

        private bool RetrieveMetadataFromGenius(Id3Tag songToSearch)
        {
            Genius queryGenius = new Genius();
            queryGenius.ExistingMetadata = songToSearch;
            if (Limit > 1 && listRetrievedMetadata != null)
                queryGenius.ListRetrievedTags = listRetrievedMetadata;

            queryGenius.QueryForMetadataNonAsync(cancellationToken, false, Limit);
            Result = queryGenius.QueryResult;
            if (queryGenius.ResultOfQuery)
            {
                if (Limit == 1)
                {
                    gSongMetaData = queryGenius.RetrievedMetadata;
                }
            }

            return queryGenius.ResultOfQuery;
        }

        private bool RetrieveMetadataFromNapster(Id3Tag songToSearch)
        {
            Napster queryNapster = new Napster();
            queryNapster.ExistingMetadata = songToSearch;
            if (Limit > 1 && listRetrievedMetadata != null)
                queryNapster.ListRetrievedTags = listRetrievedMetadata;

            queryNapster.QueryForMetadataNonAsync(cancellationToken);
            Result = queryNapster.QueryResult;
            if (queryNapster.ResultOfQuery)
            {
                if (Limit == 1)
                {
                    gSongMetaData = queryNapster.RetrievedMetadata;
                }
            }

            return queryNapster.ResultOfQuery;

        }

        private bool RetrieveMetadataFromDeezer(Id3Tag songToSearch)
        {
            Deezer queryDeezer = new Deezer();
            queryDeezer.ExistingMetadata = songToSearch;
            if (Limit > 1 && listRetrievedMetadata != null)
                queryDeezer.ListRetrievedTags = listRetrievedMetadata;
            queryDeezer.QueryForMetadataNonAsync(cancellationToken, false, Limit);
            Result = queryDeezer.QueryResult;
            if (queryDeezer.ResultOfQuery)
            {
                if (Limit == 1)
                {
                    gSongMetaData = queryDeezer.RetrievedMetadata;
                }
            }

            return queryDeezer.ResultOfQuery;
        }

        private bool RetrieveMetadataFromMusixMatch(Id3Tag songToSearch)
        {

            MusixMatch queryMusixMatch = new MusixMatch();
            queryMusixMatch.ExistingMetadata = songToSearch;
            if (Limit > 1 && listRetrievedMetadata != null)
                queryMusixMatch.ListRetrievedTags = listRetrievedMetadata;
            queryMusixMatch.QueryForMetadataNonAsync(cancellationToken, false, Limit);
            
            if (queryMusixMatch.ResultOfQuery)
            { 
                if (Limit == 1)
                {
                    gSongMetaData = queryMusixMatch.RetrievedMetadata;
                }
            }

            Result = queryMusixMatch.QueryResult;
            return queryMusixMatch.ResultOfQuery;
        }

        private bool RetrieveMetadataFromGaliboo(Id3Tag songToSearch)
        {
            Galiboo queryGaliboo = new Galiboo();
            queryGaliboo.ExistingMetadata = songToSearch;
            if (Limit > 1 && listRetrievedMetadata != null)
                queryGaliboo.ListRetrievedTags = listRetrievedMetadata;
            queryGaliboo.QueryForMetadataNonAsync(cancellationToken, false, Limit);
            if (queryGaliboo.ResultOfQuery)
            {
                if (Limit == 1)
                {
                    gSongMetaData = queryGaliboo.RetrievedMetadata;
                }
            }
            Result = queryGaliboo.QueryResult;

            return queryGaliboo.ResultOfQuery;
        }

        private bool RetrieveMetadataFromItunes(Id3Tag songToSearch)
        {
            ITunes queryITunes = new ITunes();
            queryITunes.ExistingMetadata = songToSearch;
            if (Limit > 1 && listRetrievedMetadata != null)
                queryITunes.ListRetrievedTags = listRetrievedMetadata;
            queryITunes.QueryForMetadataNonAsync(cancellationToken, false, Limit);
            if (queryITunes.ResultOfQuery)
            {
                if (Limit == 1)
                {
                    gSongMetaData = queryITunes.RetrievedMetadata;
                }
            }

            Result = queryITunes.QueryResult;
            return queryITunes.ResultOfQuery;
        }

        private bool RetrieveMetadataFromLastFM(Id3Tag songToSearch)
        {
            LastFM queryLastFM = new LastFM();
            queryLastFM.ExistingMetadata = songToSearch;
            if (Limit > 1 && listRetrievedMetadata != null)
                queryLastFM.ListRetrievedTags = listRetrievedMetadata;
            queryLastFM.QueryForMetadataNonAsync(cancellationToken, Limit);
            if (queryLastFM.ResultOfQuery)
            {
                if (Limit == 1)
                {
                    gSongMetaData = queryLastFM.RetrievedMetadata;
                }
            }

            Result = queryLastFM.QueryResult;
            return queryLastFM.ResultOfQuery;
        }

        private bool RetrieveMetadataFromDiscogs(Id3Tag songToSearch)
        {
            Discogs queryDiscogs = new Discogs();
            queryDiscogs.ExistingMetadata = songToSearch;
            if (Limit > 1 && listRetrievedMetadata != null)
                queryDiscogs.ListRetrievedTags = listRetrievedMetadata;
            queryDiscogs.QueryForMetadataNonAsync(cancellationToken, Limit);
            if (queryDiscogs.ResultOfQuery)
            {
                if (Limit == 1)
                {
                    gSongMetaData = queryDiscogs.RetrievedMetadata;
                }
            }

            Result = queryDiscogs.QueryResult;
            return queryDiscogs.ResultOfQuery;
        }

        private bool RetrieveMetadataFromSpotify(Id3Tag songToSearch)
        {
            Spotify querySpotify = new Spotify();
            querySpotify.ExistingMetadata = songToSearch;
            if (Limit > 1 && listRetrievedMetadata != null)
                querySpotify.ListRetrievedTags = listRetrievedMetadata;
            querySpotify.QueryForMetadataNonAsync(cancellationToken, Limit);
            if (querySpotify.ResultOfQuery)
            {
                if (Limit == 1)
                {
                    gSongMetaData = querySpotify.RetrievedMetadata;
                }
            }

            Result = querySpotify.QueryResult;
            return querySpotify.ResultOfQuery;
        }


    }
}
