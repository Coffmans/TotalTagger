using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TotalTagger
{
    class SearchServiceForArtwork
    {
        private BindingList<Id3Tag> listRetrievedMetadata = new BindingList<Id3Tag>();
        public BindingList<Id3Tag> ListRetrievedMetadata
        {
            get { return listRetrievedMetadata; }
            set { listRetrievedMetadata = value; }
        }

        public Id3Tag gSongMetaData { get; set; }
        public string Result = "";

        public bool CallServiceForArtwork(MetadataService metadataService, Id3Tag songToSearch)
        {
            switch (metadataService)
            {
                case MetadataService.Deezer:
                    return GetArtworkFromDeezer(songToSearch);
                case MetadataService.iTunes:
                    return GetArtworkFromiTunes(songToSearch);
                case MetadataService.LastFM:
                    return GetArtworkFromLastFM(songToSearch);
                case MetadataService.Spotify:
                    return GetArtworkFromSpotify(songToSearch);
                //case MetadataService.Galiboo:
                //    return RetrieveMetadataFromGaliboo(songToSearch);
            }

            return false;

        }
        private bool GetArtworkFromLastFM(Id3Tag songToSearch)
        {
            LastFM queryLastFM = new LastFM();
            queryLastFM.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> artistMetadata = new BindingList<Id3Tag>();
            queryLastFM.ListRetrievedTags = artistMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();
            queryLastFM.QueryForArtworkNonAsync(cancellationToken);
            if (queryLastFM.ResultOfQuery)
            {
                gSongMetaData = queryLastFM.RetrievedMetadata;
            }

            Result = queryLastFM.QueryResult;
            return queryLastFM.ResultOfQuery;

        }

        private bool GetArtworkFromSpotify(Id3Tag songToSearch)
        {
            Spotify querySpotify = new Spotify();

            querySpotify.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> artistMetadata = new BindingList<Id3Tag>();
            querySpotify.ListRetrievedTags = artistMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            querySpotify.QueryForArtworkNonAsync(cancellationToken);
            if (querySpotify.ResultOfQuery)
            {
                gSongMetaData = querySpotify.RetrievedMetadata;
            }

            Result = querySpotify.QueryResult;
            return querySpotify.ResultOfQuery;
        }

        private bool GetArtworkFromiTunes(Id3Tag songToSearch)
        {
            ITunes queryiTunes = new ITunes();

            queryiTunes.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> artistMetadata = new BindingList<Id3Tag>();
            queryiTunes.ListRetrievedTags = artistMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryiTunes.QueryForArtworkNonAsync(cancellationToken);
            if (queryiTunes.ResultOfQuery)
            {
                gSongMetaData = queryiTunes.RetrievedMetadata;
            }
            Result = queryiTunes.QueryResult;
            return queryiTunes.ResultOfQuery;

        }

        private bool GetArtworkFromDeezer(Id3Tag songToSearch)
        {
            Deezer queryDeezer = new Deezer();

            queryDeezer.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> artistMetadata = new BindingList<Id3Tag>();
            queryDeezer.ListRetrievedTags = artistMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryDeezer.QueryForArtworkNonAsync(cancellationToken);
            if (queryDeezer.ResultOfQuery)
            {
                gSongMetaData = queryDeezer.RetrievedMetadata;
            }
            Result = queryDeezer.QueryResult;
            return queryDeezer.ResultOfQuery;

        }

        //private bool GetArtworkFromMusixMatch(Id3Tag songToSearch)
        //{
        //    MusixMatch queryMusixMatch = new MusixMatch();

        //    queryMusixMatch.ExistingMetadata = songToSearch;
        //    BindingList<Id3Tag> artistMetadata = new BindingList<Id3Tag>();
        //    queryMusixMatch.ListRetrievedTags = artistMetadata;

        //    CancellationTokenSource source = new CancellationTokenSource();
        //    CancellationToken cancellationToken = new CancellationToken();

        //    queryMusixMatch.QueryForArtworkNonAsync(cancellationToken);
        //    if (queryMusixMatch.ResultOfQuery)
        //    {
        //        songToSearch = queryMusixMatch.RetrievedMetadata;
        //    }

        //    return queryMusixMatch.QueryResult;
        //}

    }
}
