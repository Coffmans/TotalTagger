using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TotalTagger
{
    class SearchServiceForAlbum
    {
        private BindingList<Id3Tag> listRetrievedMetadata = new BindingList<Id3Tag>();
        public BindingList<Id3Tag> ListRetrievedMetadata
        {
            get { return listRetrievedMetadata; }
            set { listRetrievedMetadata = value; }
        }

        private List<AlbumMetadata> albumsForArtist = new List<AlbumMetadata>();
        public List<AlbumMetadata> AlbumsForArtist
        {
            get { return albumsForArtist; }
            set { albumsForArtist = value; }
        }

        public string Result { get; set; }

        public bool CallServiceForAlbum(MetadataService metadataService, Id3Tag songToSearch)
        {
            switch (metadataService)
            {
                case MetadataService.MusicBrainz:
                    return GetAlbumsFromMB(songToSearch);
                case MetadataService.Napster:
                    return GetAlbumsFromNapster(songToSearch);
                case MetadataService.Deezer:
                    return GetAlbumsFromDeezer(songToSearch);
                //case MetadataService.Genius:
                //    return GetAlbumsFromGenius(songToSearch);
                case MetadataService.MusixMatch:
                    return GetAlbumsFromMusixMatch(songToSearch);
                case MetadataService.iTunes:
                    return GetAlbumsFromITunes(songToSearch);
                case MetadataService.LastFM:
                    return GetAlbumsFromLastFM(songToSearch);
                case MetadataService.Spotify:
                    return GetAlbumsFromSpotify(songToSearch);
            }

            return false;

        }


        private bool GetAlbumsFromMB(Id3Tag songToSearch)
        {
            MusicBrainz queryMusicBrainz = new MusicBrainz();

            queryMusicBrainz.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryMusicBrainz.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryMusicBrainz.QueryForRelease(cancellationToken);
            Result = queryMusicBrainz.QueryResult;
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
            return queryMusicBrainz.ResultOfQuery;
        }

        private bool GetAlbumsFromLastFM(Id3Tag songToSearch)
        {
            LastFM queryLastFM = new LastFM();

            queryLastFM.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryLastFM.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryLastFM.QueryForAlbum(cancellationToken);
            Result = queryLastFM.QueryResult;
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
            }
            return queryLastFM.ResultOfQuery;
        }

        private bool GetAlbumsFromSpotify(Id3Tag songToSearch)
        {
            Spotify querySpotify = new Spotify();

            querySpotify.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            querySpotify.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            querySpotify.QueryForAlbum(cancellationToken);
            Result = querySpotify.QueryResult;
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
            }
            return querySpotify.ResultOfQuery;
        }

        private bool GetAlbumsFromNapster(Id3Tag songToSearch)
        {
            Napster queryNapster = new Napster();

            queryNapster.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryNapster.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryNapster.QueryForAlbum(cancellationToken);
            Result = queryNapster.QueryResult;
            if (queryNapster.ResultOfQuery)
            {
                albumMetadata = queryNapster.ListRetrievedTags;
                foreach (var album in albumMetadata)
                {
                    AlbumMetadata TempAlbumMetdata = new AlbumMetadata();
                    TempAlbumMetdata.Source = "Napster";
                    TempAlbumMetdata.MusicMetatada = album;
                    AlbumsForArtist.Add(TempAlbumMetdata);
                }

            }
            return queryNapster.ResultOfQuery;
        }

        private bool GetAlbumsFromITunes(Id3Tag songToSearch)
        {
            ITunes queryITunes = new ITunes();

            queryITunes.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryITunes.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryITunes.QueryForMetadataNonAsync(cancellationToken, true);
            Result = queryITunes.QueryResult;
            if (queryITunes.ResultOfQuery)
            {

                albumMetadata = queryITunes.ListRetrievedTags;
                foreach (var album in albumMetadata)
                {
                    AlbumMetadata TempAlbumMetdata = new AlbumMetadata();
                    TempAlbumMetdata.Source = "iTunes";
                    TempAlbumMetdata.MusicMetatada = album;
                    AlbumsForArtist.Add(TempAlbumMetdata);
                }
            }
            return queryITunes.ResultOfQuery;
        }

        private bool GetAlbumsFromDeezer(Id3Tag songToSearch)
        {
            Deezer queryDeezer = new Deezer();

            queryDeezer.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryDeezer.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryDeezer.QueryForAlbum(cancellationToken);
            Result = queryDeezer.QueryResult;
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
            }
            return queryDeezer.ResultOfQuery;
        }

        private bool GetAlbumsFromMusixMatch(Id3Tag songToSearch)
        {
            MusixMatch queryMusixMatch = new MusixMatch();

            queryMusixMatch.ExistingMetadata = songToSearch;
            BindingList<Id3Tag> albumMetadata = new BindingList<Id3Tag>();
            queryMusixMatch.ListRetrievedTags = albumMetadata;

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = new CancellationToken();

            queryMusixMatch.QueryForAlbum(cancellationToken);
            Result = queryMusixMatch.QueryResult;
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
            }
            return queryMusixMatch.ResultOfQuery;
        }


    }
}
