using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TotalTagger
{
    public class NapsterData
    {

        public class Rootobject
        {
            public Meta meta { get; set; }
            public Links links { get; set; }
            public Datum[] data { get; set; }
        }

        public class Meta
        {
            public int totalCount { get; set; }
        }

        public class Links
        {
            public string self { get; set; }
        }

        public class Datum
        {
            public string type { get; set; }
            public string id { get; set; }
            public int index { get; set; }
            public int disc { get; set; }
            public string href { get; set; }
            public int playbackSeconds { get; set; }
            public bool _explicit { get; set; }
            public bool isStreamable { get; set; }
            public string name { get; set; }
            public string isrc { get; set; }
            public string shortcut { get; set; }
            public object[] blurbs { get; set; }
            public string artistName { get; set; }
            public string albumName { get; set; }
            public Format[] formats { get; set; }
            public string albumId { get; set; }
            public Contributors contributors { get; set; }
            public Links1 links { get; set; }
            public string previewURL { get; set; }
            public Linked linked { get; set; }
        }

        public class Contributors
        {
            public string primaryArtist { get; set; }
            public string engineer { get; set; }
            public string remixer { get; set; }
            public string producer { get; set; }
            public string composer { get; set; }
        }

        public class Links1
        {
            public Artists artists { get; set; }
            public Albums albums { get; set; }
            public Composers composers { get; set; }
            public Genres genres { get; set; }
            public Tags tags { get; set; }
        }

        public class Artists
        {
            public string[] ids { get; set; }
            public string href { get; set; }
        }

        public class Albums
        {
            public string[] ids { get; set; }
            public string href { get; set; }
        }

        public class Composers
        {
            public string[] ids { get; set; }
            public string href { get; set; }
        }

        public class Genres
        {
            public string[] ids { get; set; }
            public string href { get; set; }
        }

        public class Tags
        {
            public string[] ids { get; set; }
            public string href { get; set; }
        }

        public class Linked
        {
            public Genre[] genres { get; set; }
        }

        public class Genre
        {
            public string type { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string href { get; set; }
            public string shortcut { get; set; }
        }

        public class Format
        {
            public string type { get; set; }
            public int bitrate { get; set; }
            public string name { get; set; }
        }

    }
    /// <summary>
    /// Class for Napster API
    /// </summary>
    internal class Napster : TotalTagger.MusicAPIs.TagService
    {
        public bool QueryForMetadataNonAsync(System.Threading.CancellationToken cancellationToken, bool searchForAlbum = false, int limit = 25)
        {
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

            string searchTermEnc = WebUtility.UrlEncode(searchableArtist + " - " + searchableTitle);

            string query = Properties.Settings.Default.NapsterURL;
            query = query.Replace("%SEARCHTERM%", searchTermEnc);
            query = query.Replace("%APIKEY%", MainWindow.serviceSettings.NapsterClientKey);

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
                            if (json?.SelectToken("meta") != null)
                            {
                                if ((int)json.SelectToken("meta.totalCount") == 0)
                                {
                                    QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from MusicBrainz";
                                    return false;
                                }
                            }
                            if (json?.SelectToken("data") != null)
                            {
                                if (searchForAlbum)
                                {
                                    RetrievedMetadata = new Id3Tag
                                    {
                                        Album = (string)json.SelectToken("data[0].albumName"),
                                    };
                                    QueryResult = "Success";
                                    ResultOfQuery = true;
                                    return true;
                                }
                                else
                                {
                                    int trackCount = 0;
                                    var tracks = json["data"];
                                    if (tracks != null)
                                    {
                                        if (limit == 1)
                                        {
                                            return PerformBestMatchLookup(searchableTitle, searchableArtist, json, tracks);
                                        }

                                        foreach (var track in tracks)
                                        {
                                            RetrievedMetadata = new Id3Tag();

                                            RetrievedMetadata.Artist = (string)track["artistName"];
                                            RetrievedMetadata.Title = (string)track["name"];
                                            RetrievedMetadata.Album = (string)track["albumName"];
                                            if (track["linked"] != null)
                                            {
                                                var linked = track["linked"];
                                                if (linked != null)
                                                {
                                                    var genres = linked["genres"];
                                                    foreach (var genre in genres)
                                                    {
                                                        if (!String.IsNullOrEmpty(RetrievedMetadata.Genre))
                                                        {
                                                            RetrievedMetadata.Genre += ", ";
                                                        }
                                                        RetrievedMetadata.Genre += (string)json.SelectToken("genre.name");
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
        public bool QueryForAlbum(System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                string searchTermEnc = WebUtility.UrlEncode(ExistingMetadata.Artist + " - " + ExistingMetadata.Title);

                string query = Properties.Settings.Default.NapsterURL;
                query = query.Replace("%SEARCHTERM%", searchTermEnc);
                query = query.Replace("%APIKEY%", MainWindow.serviceSettings.NapsterClientKey);

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
                            if (json?.SelectToken("meta") != null)
                            {
                                if ((int)json.SelectToken("meta.totalCount") == 0)
                                {
                                    QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from MusicBrainz";
                                    return false;
                                }
                            }
                            if (json?.SelectToken("data") != null)
                            {
                                var tracks = json["data"];
                                foreach (var track in tracks)
                                {
                                    RetrievedMetadata = new Id3Tag
                                    {
                                        Album = (string)track["albumName"]
                                    };
                                    ListRetrievedTags.Add(RetrievedMetadata);
                                }

                                QueryResult = "Success";
                                ResultOfQuery = true;
                                return true;
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

        private bool PerformBestMatchLookup(string searchableTitle, string searchableArtist, JObject json, JToken tracks)
        {
            try
            {
                foreach (var track in tracks)
                {
                    RetrievedMetadata = new Id3Tag();

                    string trackName = (string)track["name"];
                    if (trackName.Equals(searchableTitle, StringComparison.OrdinalIgnoreCase) || trackName.IndexOf(searchableTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        RetrievedMetadata.Title = trackName;
                    }

                    string artistName = (string)track["artistName"];
                    if (artistName.Equals(searchableArtist, StringComparison.OrdinalIgnoreCase) || artistName.IndexOf(searchableArtist, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        RetrievedMetadata.Artist = artistName;
                    }
                    if (!String.IsNullOrEmpty(RetrievedMetadata.Title) && !String.IsNullOrEmpty(RetrievedMetadata.Artist))
                    {
                        RetrievedMetadata.Album = (string)track["albumName"];
                        if (track["linked"] != null)
                        {
                            if (track["linked"] != null)
                            {
                                var linked = track["linked"];
                                if (linked != null)
                                {
                                    var genres = linked["genres"];
                                    foreach (var genre in genres)
                                    {
                                        if (!String.IsNullOrEmpty(RetrievedMetadata.Genre))
                                        {
                                            RetrievedMetadata.Genre += ", ";
                                        }
                                        RetrievedMetadata.Genre += (string)json.SelectToken("genre.name");
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
                    RetrievedMetadata = new Id3Tag();

                    string trackName = (string)track["name"];
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
                        RetrievedMetadata.Album = (string)track["albumName"];
                        if (track["linked"] != null)
                        {
                            var linked = track["linked"];
                            if (linked != null)
                            {
                                var genres = linked["genres"];
                                foreach (var genre in genres)
                                {
                                    if (!String.IsNullOrEmpty(RetrievedMetadata.Genre))
                                    {
                                        RetrievedMetadata.Genre += ", ";
                                    }
                                    RetrievedMetadata.Genre += (string)json.SelectToken("genre.name");
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
