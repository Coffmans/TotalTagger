using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TotalTagger
{
    public class GeniusTrackData
    {

        public class Rootobject
        {
            public Meta meta { get; set; }
            public Response response { get; set; }
        }

        public class Meta
        {
            public int status { get; set; }
        }

        public class Response
        {
            public Hit[] hits { get; set; }
        }

        public class Hit
        {
            public object[] highlights { get; set; }
            public string index { get; set; }
            public string type { get; set; }
            public Result result { get; set; }
        }

        public class Result
        {
            public int annotation_count { get; set; }
            public string api_path { get; set; }
            public string full_title { get; set; }
            public string header_image_thumbnail_url { get; set; }
            public string header_image_url { get; set; }
            public int id { get; set; }
            public int lyrics_owner_id { get; set; }
            public string lyrics_state { get; set; }
            public string path { get; set; }
            public int? pyongs_count { get; set; }
            public string song_art_image_thumbnail_url { get; set; }
            public Stats stats { get; set; }
            public string title { get; set; }
            public string title_with_featured { get; set; }
            public string url { get; set; }
            public Primary_Artist primary_artist { get; set; }
        }

        public class Stats
        {
            public bool hot { get; set; }
            public int unreviewed_annotations { get; set; }
            public int concurrents { get; set; }
            public int pageviews { get; set; }
        }

        public class Primary_Artist
        {
            public string api_path { get; set; }
            public string header_image_url { get; set; }
            public int id { get; set; }
            public string image_url { get; set; }
            public bool is_meme_verified { get; set; }
            public bool is_verified { get; set; }
            public string name { get; set; }
            public string url { get; set; }
            public int iq { get; set; }
        }
    }

    public class GeniusData
    {

        public class Rootobject
        {
            public Meta meta { get; set; }
            public Response response { get; set; }
        }

        public class Meta
        {
            public int status { get; set; }
        }

        public class Response
        {
            public Hit[] hits { get; set; }
        }

        public class Hit
        {
            public object[] highlights { get; set; }
            public string index { get; set; }
            public string type { get; set; }
            public Result result { get; set; }
        }

        public class Result
        {
            public int annotation_count { get; set; }
            public string api_path { get; set; }
            public string full_title { get; set; }
            public string header_image_thumbnail_url { get; set; }
            public string header_image_url { get; set; }
            public int id { get; set; }
            public int lyrics_owner_id { get; set; }
            public string lyrics_state { get; set; }
            public string path { get; set; }
            public int? pyongs_count { get; set; }
            public string song_art_image_thumbnail_url { get; set; }
            public Stats stats { get; set; }
            public string title { get; set; }
            public string title_with_featured { get; set; }
            public string url { get; set; }
            public Primary_Artist primary_artist { get; set; }
        }

        public class Stats
        {
            public bool hot { get; set; }
            public int unreviewed_annotations { get; set; }
            public int concurrents { get; set; }
            public int pageviews { get; set; }
        }

        public class Primary_Artist
        {
            public string api_path { get; set; }
            public string header_image_url { get; set; }
            public int id { get; set; }
            public string image_url { get; set; }
            public bool is_meme_verified { get; set; }
            public bool is_verified { get; set; }
            public string name { get; set; }
            public string url { get; set; }
            public int iq { get; set; }
        }

    }
    class Genius : TotalTagger.MusicAPIs.TagService
    {
        public bool QueryForMetadataNonAsync(System.Threading.CancellationToken cancellationToken, bool searchForAlbum = false, int limit = 25)
        {
            if (String.IsNullOrEmpty(ExistingMetadata.Title))
            {
                QueryResult = "Title Needed To Perform Search From Genius";
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
                QueryResult = "Title Needed To Perform Search From Genius";
                ResultOfQuery = false;
                return false;
            }

            string encodedArtist = "";
            if (!String.IsNullOrEmpty(searchableArtist))
            {
                encodedArtist = System.Web.HttpUtility.UrlEncode(searchableArtist = searchableArtist.TrimEnd());
            }

            string searchTermEnc = WebUtility.UrlEncode(searchableArtist + "-" + searchableTitle);

            string query = Properties.Settings.Default.GeniusURL;
            query = query.Replace("%SEARCHTERM%", searchTermEnc);
            query = query.Replace("%APIKEY%", MainWindow.serviceSettings.GeniusClientKey);

            try
            {
                using (WebClient client = new WebClient())
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    string results = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    if (results != null)
                    {
                        var json = (JObject)JsonConvert.DeserializeObject(results);
                        if (json != null)
                        {
                            if (json != null && json["response"] != null)
                            {
                                var hits = json["response"]["hits"];

                                if (hits != null && hits.Any())
                                {
                                    if (limit == 1)
                                    {
                                        return PerformBestMatchLookup(searchableTitle, searchableArtist, json, hits);
                                    }

                                    int trackCount = 0;

                                    foreach (var track in hits)
                                    {
                                        RetrievedMetadata = new Id3Tag()
                                        {
                                            Cover = new System.Windows.Forms.PictureBox(),
                                        };

                                        var result = track["result"];

                                        RetrievedMetadata.Title = (string)result["title"];

                                        var artist = result["primary_artist"];
                                        RetrievedMetadata.Artist = (string)artist["name"];

                                        if (result["song_art_image_thumbnail_url"] != null)
                                            RetrievedMetadata.Cover.ImageLocation = (string)result["song_art_image_thumbnail_url"];

                                        query = Properties.Settings.Default.GeniusURL;
                                        query = query.Replace("/search?q=%SEARCHTERM%&", result.SelectToken("api_path") + "?");
                                        query = query.Replace("%APIKEY%", MainWindow.serviceSettings.GeniusClientKey);

                                        request = (HttpWebRequest)WebRequest.Create(query);
                                        response = (HttpWebResponse)request.GetResponse();
                                        results = new StreamReader(response.GetResponseStream()).ReadToEnd();

                                        if (results != null)
                                        {
                                            json = (JObject)JsonConvert.DeserializeObject(results);
                                            if (json != null)
                                            {
                                                if (json != null && json["response"] != null)
                                                {
                                                    var song = json["response"]["song"];
                                                    if (song != null)
                                                    {
                                                        RetrievedMetadata.Date = (string)song["release_date"];
                                                        var album = song["album"];
                                                        if (album != null)
                                                        {
                                                            RetrievedMetadata.Album = (string)album["full_title"];
                                                            //RetrievedMetadata.Cover.ImageLocation = (string)album["cover_art_url"];
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        ListRetrievedTags.Add(RetrievedMetadata);
                                        trackCount++;
                                        if (trackCount > limit)
                                        {
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
                QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from Discogs";
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
            }

            ResultOfQuery = false;
            return false;
        }

        private bool PerformBestMatchLookup(string searchableTitle, string searchableArtist, JObject json, JToken tracks)
        {
            try
            {
                foreach (var track in tracks)
                {
                    RetrievedMetadata = new Id3Tag()
                    {
                        Cover = new System.Windows.Forms.PictureBox(),
                    };

                    var result = track["result"];

                    string trackName = (string)result["title"];
                    if (trackName.Equals(searchableTitle, StringComparison.OrdinalIgnoreCase) || trackName.IndexOf(searchableTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        RetrievedMetadata.Title = trackName;
                    }

                    var artist = result["primary_artist"];
                    string artistName = (string)artist["name"];
                    if (artistName.Equals(searchableArtist, StringComparison.OrdinalIgnoreCase) || artistName.IndexOf(searchableArtist, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        RetrievedMetadata.Artist = artistName;
                    }
                    if (!String.IsNullOrEmpty(RetrievedMetadata.Title) && !String.IsNullOrEmpty(RetrievedMetadata.Artist))
                    {
                        if (result["song_art_image_thumbnail_url"] != null)
                            RetrievedMetadata.Cover.ImageLocation = (string)result["song_art_image_thumbnail_url"];

                        string query = Properties.Settings.Default.GeniusURL;
                        query = query.Replace("/search?q=%SEARCHTERM%&", result.SelectToken("api_path") + "?");
                        query = query.Replace("%APIKEY%", MainWindow.serviceSettings.GeniusClientKey);
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        string results = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        if (results != null)
                        {
                            json = (JObject)JsonConvert.DeserializeObject(results);
                            if (json != null)
                            {
                                if (json != null && json["response"] != null)
                                {
                                    var song = json["response"]["song"];
                                    if (song != null)
                                    {
                                        RetrievedMetadata.Date = (string)song["release_date"];
                                        var album = song["album"];
                                        if (album != null)
                                        {
                                            RetrievedMetadata.Album = (string)album["full_title"];
                                            //RetrievedMetadata.Cover.ImageLocation = (string)album["cover_art_url"];
                                        }
                                    }
                                }
                            }
                        }

                        QueryResult = "Success";
                        ResultOfQuery = true;
                        return true;
                    }
                }

                foreach (var track in tracks)
                {
                    RetrievedMetadata = new Id3Tag()
                    {
                        Cover = new System.Windows.Forms.PictureBox(),
                    };

                    var result = track["result"];

                    string trackName = (string)result["title"];
                    if (trackName.Equals(searchableTitle, StringComparison.OrdinalIgnoreCase) || trackName.IndexOf(searchableTitle, StringComparison.OrdinalIgnoreCase) > 0)
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

                    var artist = result["primary_artist"];
                    string artistName = (string)artist["name"];
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
                        if (result["song_art_image_thumbnail_url"] != null)
                            RetrievedMetadata.Cover.ImageLocation = (string)result["song_art_image_thumbnail_url"];

                        string query = Properties.Settings.Default.GeniusURL;
                        query = query.Replace("/search?q=%SEARCHTERM%&", result.SelectToken("api_path") + "?");
                        query = query.Replace("%APIKEY%", MainWindow.serviceSettings.GeniusClientKey);

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        string results = new StreamReader(response.GetResponseStream()).ReadToEnd();

                        if (results != null)
                        {
                            json = (JObject)JsonConvert.DeserializeObject(results);
                            if (json != null)
                            {
                                if (json != null && json["response"] != null)
                                {
                                    var song = json["response"]["song"];
                                    if (song != null)
                                    {
                                        RetrievedMetadata.Date = (string)song["release_date"];
                                        var album = song["album"];
                                        if (album != null)
                                        {
                                            RetrievedMetadata.Album = (string)album["full_title"];
                                            RetrievedMetadata.Cover.ImageLocation = (string)album["cover_art_url"];
                                        }
                                    }
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
        public bool QueryForArtworkNonAsync(System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                string artistEncoded = WebUtility.UrlEncode(ExistingMetadata.Artist);
                string titleEncoded = WebUtility.UrlEncode(ExistingMetadata.Title);

                string query = "http://api.deezer.com/search?q=artist:\"" + artistEncoded + "\"+track:\"" + titleEncoded + "\"&limit=" + 1 + "&order=RANKING";

                query = query.Replace("%SEARCHARTIST%", artistEncoded);
                query = query.Replace("%SEARCHTITLE%", titleEncoded);

                Utils.HttpRequestClass client = new Utils.HttpRequestClass();
                if (client != null)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    string results = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    if (results != null)
                    {
                        var json = (JObject)JsonConvert.DeserializeObject(results);
                        if (json != null)
                        {
                            if (json?.SelectToken("data") != null)
                            {
                                var tracks = json["data"];
                                foreach (var track in tracks)
                                {
                                    RetrievedMetadata = new Id3Tag()
                                    {
                                        Cover = new System.Windows.Forms.PictureBox(),
                                    };
                                    var album = track["album"];
                                    RetrievedMetadata.Album = (string)album["title"];

                                    if (album["cover_medium"] != null)
                                    {
                                        RetrievedMetadata.Cover.ImageLocation = (string)album["cover_medium"];
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
            catch (Exception ex)
            {
                QueryResult = ex.Message;
                return false;
            }

            QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from Deezer";
            return false;
        }
        public bool QueryForAlbum(System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                string artistEncoded = WebUtility.UrlEncode(ExistingMetadata.Artist);
                string titleEncoded = WebUtility.UrlEncode(ExistingMetadata.Title);

                string query = "http://api.deezer.com/search?q=artist:\"" + artistEncoded + "\"+track:\"" + titleEncoded + "\"&limit=" + 1 + "&order=RANKING";

                query = query.Replace("%SEARCHARTIST%", artistEncoded);
                query = query.Replace("%SEARCHTITLE%", titleEncoded);

                Utils.HttpRequestClass client = new Utils.HttpRequestClass();
                if (client != null)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    string results = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    if (results != null)
                    {
                        var json = (JObject)JsonConvert.DeserializeObject(results);
                        if (json != null)
                        {
                            if (json?.SelectToken("data") != null)
                            {
                                var tracks = json["data"];
                                foreach (var track in tracks)
                                {
                                    RetrievedMetadata = new Id3Tag()
                                    {
                                        Cover = new System.Windows.Forms.PictureBox(),
                                    };
                                    var album = track["album"];
                                    RetrievedMetadata.Album = (string)album["title"];
                                    ListRetrievedTags.Add(RetrievedMetadata);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
                return false;
            }

            QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from Deezer";
            return false;
        }

    }
}
