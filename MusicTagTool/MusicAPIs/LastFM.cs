using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TotalTagger
{
    public class LastFMSongData
    {
        //public class Rootobject
        //{
        public Results results { get; set; }
        //}
    }

        public class Results
        {
            public OpensearchQuery opensearchQuery { get; set; }
            public string opensearchtotalResults { get; set; }
            public string opensearchstartIndex { get; set; }
            public string opensearchitemsPerPage { get; set; }
            public Trackmatches trackmatches { get; set; }
            public Attr attr { get; set; }
        }

        public class OpensearchQuery
        {
            public string text { get; set; }
            public string role { get; set; }
            public string startPage { get; set; }
        }

        public class Trackmatches
        {
            public Track[] track { get; set; }
        }

        public class Track
        {
            public string name { get; set; }
            public string artist { get; set; }
            public string url { get; set; }
            public string streamable { get; set; }
            public string listeners { get; set; }
            public Image[] image { get; set; }
            public string mbid { get; set; }
        }

        public class Image
        {
            public string text { get; set; }
            public string size { get; set; }
        }

        public class Attr
        {
        }
    //}

    public class LastFMTrackInfo
    {

        public class Rootobject
        {
            public Track track { get; set; }
        }

        public class Track
        {
            public string name { get; set; }
            public string url { get; set; }
            public string duration { get; set; }
            public Streamable streamable { get; set; }
            public string listeners { get; set; }
            public string playcount { get; set; }
            public Artist artist { get; set; }
            public Album album { get; set; }
            public Toptags toptags { get; set; }
            public Wiki wiki { get; set; }
        }

        public class Streamable
        {
            public string text { get; set; }
            public string fulltrack { get; set; }
        }

        public class Artist
        {
            public string name { get; set; }
            public string mbid { get; set; }
            public string url { get; set; }
        }

        public class Album
        {
            public string artist { get; set; }
            public string title { get; set; }
            public string url { get; set; }
            public Image[] image { get; set; }
        }

        public class Image
        {
            public string text { get; set; }
            public string size { get; set; }
        }

        public class Toptags
        {
            public Tag[] tag { get; set; }
        }

        public class Tag
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Wiki
        {
            public string published { get; set; }
            public string summary { get; set; }
            public string content { get; set; }
        }
    }

    public class LastFMAlbum
    {

        public class Rootobject
        {
            public Topalbums topalbums { get; set; }
        }

        public class Topalbums
        {
            public Album[] album { get; set; }
            public Attr attr { get; set; }
        }

        public class Attr
        {
            public string artist { get; set; }
            public string page { get; set; }
            public string perPage { get; set; }
            public string totalPages { get; set; }
            public string total { get; set; }
        }

        public class Album
        {
            public string name { get; set; }
            public int playcount { get; set; }
            public string url { get; set; }
            public Artist artist { get; set; }
            public Image[] image { get; set; }
            public string mbid { get; set; }
        }

        public class Artist
        {
            public string name { get; set; }
            public string mbid { get; set; }
            public string url { get; set; }
        }

        public class Image
        {
            public string text { get; set; }
            public string size { get; set; }
        }
    }
    class LastFM : TotalTagger.MusicAPIs.TagService
    {
        public async Task<bool> QueryForMetadataNonAsync(System.Threading.CancellationToken cancellationToken, int limit = 25)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    if (String.IsNullOrEmpty(ExistingMetadata.Title))
                    {
                        QueryResult = "Title Needed To Perform Search From LastFM";
                        ResultOfQuery = false;
                        return false;
                    }

                    string searchableTitle = ExistingMetadata.Title;
                    if (searchableTitle.Contains("("))
                    {
                        searchableTitle = searchableTitle.Remove(searchableTitle.IndexOf("("));
                    }
                    searchableTitle = searchableTitle.TrimEnd();

                    string searchableArtist = ExistingMetadata.Artist;
                    int nIndex = searchableArtist.IndexOf("feat");
                    if (nIndex > 0)
                        searchableArtist = searchableArtist.Remove(nIndex);

                    if (searchableArtist.IndexOfAny(new char[] { '/', '(', '&' }) > 0)
                    {
                        nIndex = searchableArtist.IndexOf("(");
                        if (nIndex > 0)
                            searchableArtist = searchableArtist.Remove(nIndex);

                        nIndex = searchableArtist.IndexOf("/");
                        if (nIndex > 0)
                            searchableArtist = searchableArtist.Remove(nIndex);

                        nIndex = searchableArtist.IndexOf("&");
                        if (nIndex > 0)
                            searchableArtist = searchableArtist.Remove(nIndex);
                    }

                    searchableArtist = searchableArtist.TrimEnd();

                    string encodedTitle = System.Web.HttpUtility.UrlEncode(searchableTitle);
                    if (String.IsNullOrEmpty(encodedTitle))
                    {
                        QueryResult = "Title Needed To Perform Search From LastFM";
                        ResultOfQuery = false;
                        return false;
                    }

                    string encodedArtist = "";
                    if (!String.IsNullOrEmpty(searchableArtist))
                    {
                        encodedArtist = System.Web.HttpUtility.UrlEncode(searchableArtist = searchableArtist.TrimEnd());
                    }

                    string query = null;
                    //if (LastFMSearch)
//                    {
                        query = Properties.Settings.Default.LastFMTrackSearchUrl + MainWindow.serviceSettings.LastFMClientKey +
                            "&format=json&autocorrect=1&track=" + encodedTitle +
                            (encodedArtist.Length > 0 ? "&artist=" + encodedArtist : "");
                    //                    }
                    //else
                    //{
                    //    query = Properties.Settings.Default.LastFMTrackGetInfo + MainWindow.serviceSettings.LastFMClientKey +
                    //        "&format=json&autocorrect=1&track=" + encodedTitle +
                    //        (encodedArtist.Length > 0 ? "&artist=" + encodedArtist : "");
                    //}
                    RestService _restService = new RestService();
                    LastFMSongData _lastFMData = await  _restService.GetLastFMSongData(query);
                    if( _lastFMData != null && _lastFMData.results.trackmatches.track.Any())
                    {
                        int trackCount = 0;
                        foreach(var lastFM in _lastFMData.results.trackmatches.track)
                        {
                            string trackTitle = lastFM.name;
                            string trackArtist = lastFM.artist;

                            RetrievedMetadata = new Id3Tag
                            {
                                Title = trackTitle,
                                Artist = trackArtist,
                                Cover = new System.Windows.Forms.PictureBox()
                            };

                            if (lastFM.image != null)
                            {
                                //string imageLocation = track["strTrackThumb"].ToString();
                                foreach (var image in lastFM.image)
                                {
                                    if (image.size.ToString().Equals("medium"))
                                    {
                                        RetrievedMetadata.Cover.ImageLocation = image.text;
                                    }
                                }
                            }

                            trackCount++;
                            if (trackCount >= limit)
                            {
                                QueryResult = "Success";
                                ResultOfQuery = true;
                                return true;

                            }
                            ListRetrievedTags.Add(RetrievedMetadata);

                        }
                        QueryResult = "Success";
                        ResultOfQuery = true;
                        return true;
                    }
                    string response = client.DownloadString(query);

                    if (response != null)
                    {
                        var json = (JObject)JsonConvert.DeserializeObject(response);

                        //if (LastFMSearch)
                        {
                            if (json != null && json["results"] != null)
                            {
                                var trackmatches = json["results"]["trackmatches"];

                                if (trackmatches != null && trackmatches.Any())
                                {
                                    if (trackmatches["track"].Any())
                                    {
                                        if (limit == 1)
                                        {
                                            return PerformBestMatchLookup(searchableTitle, searchableArtist, json, trackmatches);
                                        }

                                        int trackCount = 0;
                                        foreach (var track in trackmatches["track"])
                                        {
                                            string trackTitle = (string)track["name"];
                                            string trackArtist = (string)track["artist"];

                                            RetrievedMetadata = new Id3Tag
                                            {
                                                Title = trackTitle,
                                                Artist = trackArtist,
                                                Cover = new System.Windows.Forms.PictureBox()
                                            };

                                            if (track["image"] != null)
                                            {
                                                //string imageLocation = track["strTrackThumb"].ToString();
                                                foreach (var image in track["image"])
                                                {
                                                    if (image["size"].ToString().Equals("medium"))
                                                    {
                                                        RetrievedMetadata.Cover.ImageLocation = image["#text"].ToString();
                                                    }
                                                }
                                            }

                                            trackCount++;
                                            if (trackCount >= limit)
                                            {
                                                QueryResult = "Success";
                                                ResultOfQuery = true;
                                                return true;

                                            }
                                            ListRetrievedTags.Add(RetrievedMetadata);
                                        }

                                        QueryResult = "Success";
                                        ResultOfQuery = true;
                                        return true;
                                    }
                                }
                            }
                        }
                        //else
                        //{
                        //    if (json != null)
                        //    {
                        //        var track = json["track"];
                        //        string trackTitle = (string)track["name"];

                        //        RetrievedMetadata = new Id3Tag
                        //        {
                        //            Title = trackTitle,
                        //            Cover = new System.Windows.Forms.PictureBox()
                        //        };

                        //        var artist = track["artist"];
                        //        if (artist != null)
                        //        {
                        //            RetrievedMetadata.Artist = (string)artist["name"];
                        //        }

                        //        var album = track["album"];
                        //        if (album != null)
                        //        {
                        //            RetrievedMetadata.Album = (string)album["title"];

                        //            var images = album["image"];
                        //            if (images != null)
                        //            {
                        //                //string imageLocation = track["strTrackThumb"].ToString();
                        //                foreach (var image in images)
                        //                {
                        //                    if (image["size"].ToString().Equals("medium"))
                        //                    {
                        //                        RetrievedMetadata.Cover.ImageLocation = image["#text"].ToString();
                        //                    }
                        //                }
                        //            }
                        //        }

                        //        ListRetrievedTags.Add(RetrievedMetadata);

                        //        QueryResult = "Success";
                        //        ResultOfQuery = true;
                        //        return true;
                        //    }
                        //}
                    }

                    QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from LastFM";
                }
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
            }

            return false;
        }


        public bool QueryForAlbum(System.Threading.CancellationToken cancellationToken, int limit = 25)
        {
            try
            {
                string encodedArtist = "";
                if (!String.IsNullOrEmpty(ExistingMetadata.Artist))
                {
                    encodedArtist = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Artist.TrimEnd());
                }

                using (WebClient client = new WebClient())
                {
                    string query = Properties.Settings.Default.LastFMArtistTopAlbums + MainWindow.serviceSettings.LastFMClientKey +
                        "&format=json&autocorrect=1&artist=" + encodedArtist;

                    string result = client.DownloadString(query);

                    if (result != null)
                    {
                        var json = (JObject)JsonConvert.DeserializeObject(result);

                        if (json != null)
                        {
                            var topalbums = json["topalbums"];
                            if (topalbums != null)
                            {
                                var albums = topalbums["album"];
                                if (albums != null)
                                {
                                    int albumCount = 0;
                                    foreach (var album in albums)
                                    {
                                        RetrievedMetadata = new Id3Tag
                                        {
                                            Album = (string)album["name"],
                                        };

                                        if (limit > 1)
                                            ListRetrievedTags.Add(RetrievedMetadata);
                                        albumCount++;
                                        if (albumCount > limit)
                                        {
                                            break;
                                        }
                                    }
                                }
                                QueryResult = "Success";
                                ResultOfQuery = true;
                                return true;
                            }
                        }
                    }
                }
                QueryResult = "No Results for " + ExistingMetadata.Artist.TrimEnd() + " from LastFM";
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
            }

            return false;
        }


        public bool QueryForArtworkNonAsync(System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                string encodedArtist = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Artist.TrimEnd());
                string encodedAlbum = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Album.TrimEnd());

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
                                            RetrievedMetadata = new Id3Tag
                                            {
                                                Cover = new System.Windows.Forms.PictureBox()
                                            };


                                            RetrievedMetadata.Cover.ImageLocation = image["#text"].ToString();
                                            QueryResult = "Success";
                                            ResultOfQuery = true;
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                QueryResult = "No Results for " + ExistingMetadata.Artist.TrimEnd() + " from LastFM";
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
            }

            return false;
        }

        private bool PerformBestMatchLookup(string searchableTitle, string searchableArtist, JObject json, JToken tracks)
        {
            try
            {

                foreach (var track in tracks["track"])
                {
                    RetrievedMetadata = new Id3Tag
                    {
                        Cover = new System.Windows.Forms.PictureBox()
                    };

                    string trackName = (string)track["name"];
                    if (trackName.Equals(searchableTitle, StringComparison.OrdinalIgnoreCase) || trackName.IndexOf(searchableTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        RetrievedMetadata.Title = trackName;
                    }

                    string artistName = (string)track["artist"];
                    if (artistName.Equals(searchableArtist, StringComparison.OrdinalIgnoreCase) || artistName.IndexOf(searchableArtist, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        RetrievedMetadata.Artist = artistName;
                    }
                    if (!String.IsNullOrEmpty(RetrievedMetadata.Title) && !String.IsNullOrEmpty(RetrievedMetadata.Artist))
                    {
                        if (track["image"] != null)
                        {
                            //string imageLocation = track["strTrackThumb"].ToString();
                            foreach (var image in track["image"])
                            {
                                if (image["size"].ToString().Equals("medium"))
                                {
                                    RetrievedMetadata.Cover.ImageLocation = image["#text"].ToString();
                                }
                            }
                        }
                        QueryResult = "Success";
                        ResultOfQuery = true;
                        return true;
                    }
                }

                foreach (var track in tracks["track"])
                {
                    RetrievedMetadata = new Id3Tag();

                    string trackName = (string)track["trackName"];
                    if (trackName.Equals(searchableTitle, StringComparison.OrdinalIgnoreCase) || trackName.IndexOf(searchableTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        RetrievedMetadata.Title = trackName;
                    }
                    else
                    {
                        string[] wordsInTitle = searchableTitle.Split(' ');
                        bool[] wordMatches = new bool[wordsInTitle.Length];
                        int matchCount = 0;
                        foreach (var word in wordsInTitle)
                        {
                            if (trackName.IndexOf(word, StringComparison.CurrentCultureIgnoreCase) >= 0)
                            {
                                wordMatches[matchCount] = true;
                                matchCount++;
                            }
                        }

                        if (matchCount == wordsInTitle.Length)
                        {
                            RetrievedMetadata.Title = trackName;
                        }
                    }

                    string artistName = (string)track["artistName"];
                    if (artistName.Equals(searchableArtist, StringComparison.OrdinalIgnoreCase) || artistName.IndexOf(searchableArtist, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        RetrievedMetadata.Artist = artistName;
                    }
                    else
                    {
                        string[] wordsInArtistName = searchableArtist.Split(' ');
                        bool[] wordMatches = new bool[wordsInArtistName.Length];
                        int matchCount = 0;
                        foreach (var word in wordsInArtistName)
                        {
                            if (artistName.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                wordMatches[matchCount] = true;
                                matchCount++;
                            }
                        }

                        if (matchCount == wordsInArtistName.Length)
                        {
                            RetrievedMetadata.Artist = artistName;
                        }
                    }

                    if (!String.IsNullOrEmpty(RetrievedMetadata.Title) && !String.IsNullOrEmpty(RetrievedMetadata.Artist))
                    {
                        if (track["image"] != null)
                        {
                            //string imageLocation = track["strTrackThumb"].ToString();
                            foreach (var image in track["image"])
                            {
                                if (image["size"].ToString().Equals("medium"))
                                {
                                    RetrievedMetadata.Cover.ImageLocation = image["#text"].ToString();
                                }
                            }
                        }
                        QueryResult = "Success";
                        ResultOfQuery = true;
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
                ResultOfQuery = false;
                return false;
            }

            QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from Napster!";
            ResultOfQuery = false;
            return false;
        }
    }
}
