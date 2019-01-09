//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TotalTagger.GraceNote
//{
//    class GraceNote
//    {
//        private void RetrievedMetaDataFromGraceNote()
//        {


//            var client = new ParkSquare.Gracenote.GracenoteClient("712956455-629D246656DAF564E93DE72F0153A5D2");

//            InvokeToProgressLabel("Retrieving Metadata for " + songFileName);

//            var x = client.Search(new ParkSquare.Gracenote.SearchCriteria
//            {
//                TrackTitle = gSongMetaData.Title,
//                //Artist = gSongMetaData.Artist,
//                //AlbumTitle = gSongMetaData.Album,
//                SearchMode = SearchMode.BestMatch,
//                //SearchOptions = SearchOptions.Cover | SearchOptions.ArtistImage
//            });


//            Id3Tag Id3Tag = gSongMetaData;

//            if (x != null && x.Count > 0)
//            {
//                listRetrievedMetadata = new BindingList<Id3Tag>();
//                foreach (var song in x.Albums)
//                {
//                    Id3Tag.Title = song.Tracks.First().Title;
//                    Id3Tag.Artist = song.Artist;
//                    Id3Tag.Album = song.Title;
//                    if (song.Genre.Any())
//                        Id3Tag.Genre = song.Genre.First().ToString();
//                    Id3Tag.Date = song.Year.ToString();

//                    if (song.Artwork.Any())
//                    {
//                        string imageLocation = song.Artwork.First().Uri.ToString();
//                        Id3Tag.Cover.ImageLocation = song.Artwork.First().Uri.ToString();
//                    }

//                    listRetrievedMetadata.Add(Id3Tag);
//                    InvokeToDataGrid(Id3Tag, -1);
//                }
//            }
//        }

//    }
//}
