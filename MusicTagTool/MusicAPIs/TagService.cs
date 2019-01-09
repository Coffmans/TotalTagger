using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalTagger.MusicAPIs
{
    public partial class TagService
    {
        private string metadataTitle = "";
        private string metadataArtist = "";
        private string metadataAlbum = "";

        public string MetadataAlbum { get => metadataAlbum; set => metadataAlbum = value; }
        public string MetadataArtist { get => metadataArtist; set => metadataArtist = value; }
        public string MetadataTitle { get => metadataTitle; set => metadataTitle = value; }

        private Id3Tag existingMetadata = new Id3Tag();
        public Id3Tag ExistingMetadata { get => existingMetadata; set => existingMetadata = value; }

        private Id3Tag retrievedMetadata = new Id3Tag();
        public Id3Tag RetrievedMetadata { get => retrievedMetadata; set => retrievedMetadata = value; }

        private BindingList<Id3Tag> listRetrievedTags = null;
        public BindingList<Id3Tag> ListRetrievedTags
        {
            get { return listRetrievedTags; }
            set { listRetrievedTags = value; }
        }

        private bool _resultOfQuery = false;
        public bool ResultOfQuery
        {
            get { return _resultOfQuery; }
            set { _resultOfQuery = value; }
        }

        private bool _lastFMSearch = true;
        public bool LastFMSearch
        {
            get { return _lastFMSearch; }
            set { _lastFMSearch = value; }
        }
        private string queryResult;
        public string QueryResult { get => queryResult; set => queryResult = value; }

        public void Initialize(string title, string artist, string album)
        {
            MetadataAlbum = album;
            metadataArtist = artist;
            metadataTitle = title;
        }
    }
}
