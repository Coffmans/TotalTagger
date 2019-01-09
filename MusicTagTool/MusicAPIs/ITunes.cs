using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
//using GlobalVariables;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using Utils;

namespace TotalTagger
{
    /// <summary>
    /// Class for Itunes API
    /// </summary>
    internal class ITunes : TotalTagger.MusicAPIs.TagService
    {
        /// <summary>
        /// Gets ID3 data from Itunes API
        /// <seealso href="https://www.apple.com/itunes/affiliates/resources/documentation/itunes-store-web-service-search-api.html"/>
        /// </summary>
        /// <param name="client">The HTTP client which is passed on to GetResponse method</param>
        /// <param name="artist">The input artist to search for</param>
        /// <param name="title">The input song title to search for</param>
        /// <param name="cancelToken">The cancelation token which is passed on to GetResponse method</param>
        /// <returns>
        /// The ID3 tag object with the results from this API for:
        /// 		Artist
        /// 		Title
        /// 		Album
        /// 		Date
        /// 		Genre
        /// 		DiscNumber
        /// 		DiscCount
        /// 		TrackNumber
        /// 		TrackCount
        /// 		Cover URL
        /// </returns>
        public async Task<bool> QueryForMetadata(System.Threading.CancellationToken cancellationToken, bool searchforAlbum=false, int limit=10)
        {
            try
            {
                string searchTermEnc = WebUtility.UrlEncode(ExistingMetadata.Artist + " - " + ExistingMetadata.Title);

                string query = Properties.Settings.Default.ITunesURL.Replace("%SEARCHTERM%", searchTermEnc);

                using (WebClient client = new WebClient())
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
                    HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                    string results = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();

                    if (results != null)
                    {
                        var json = (JObject)JsonConvert.DeserializeObject(results);
                        if (json != null)
                        {
                            if (json?.SelectToken("resultCount") != null)
                            {
                                if ((int)json.SelectToken("resultCount") == 0)
                                {
                                    QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from iTunes";
                                    return false;
                                }
                            }

                            if (json?.SelectToken("results") != null)
                            {
                                if (searchforAlbum)
                                {
                                    RetrievedMetadata = new Id3Tag
                                    {
                                        Album = (string)json.SelectToken("results[0].collectionName"),
                                    };
                                }
                                else
                                {
                                    int trackCount = 0;
                                    var tracks = json["results"];
                                    foreach (var track in tracks)
                                    {
                                        RetrievedMetadata = new Id3Tag()
                                        {
                                            Cover = new System.Windows.Forms.PictureBox(),
                                        };
                                        RetrievedMetadata.Artist = (string)track["artistName"];
                                        RetrievedMetadata.Title = (string)track["name"];
                                        RetrievedMetadata.Album = (string)track["albumName"];
                                        RetrievedMetadata.Album = (string)track["collectionName"];
                                        RetrievedMetadata.Date = (string)track["releaseDate"];
                                        RetrievedMetadata.Genre = (string)track["primaryGenreName"];
                                        //RetrievedMetadata.TrackCount = (string)track["trackCount"];
                                        //RetrievedMetadata.TrackNumber = (string)track["trackNumber"];
                                        if (json.SelectToken("results[0].artworkUrl100") != null)
                                            RetrievedMetadata.Cover.ImageLocation = (string)track["artworkUrl100"];

                                        if (limit > 1)
                                            ListRetrievedTags.Add(RetrievedMetadata);
                                        trackCount++;
                                        if (trackCount > limit)
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
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
                return false;
            }

            QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from iTunes";
            return false;
        }
        public bool QueryForMetadataNonAsync(System.Threading.CancellationToken cancellationToken, bool searchforAlbum=false, int limit=10)
        {
            try
            {
                string searchableTitle = ExistingMetadata.Title;
                if( searchableTitle.Contains("("))
                {
                    searchableTitle = searchableTitle.Remove(searchableTitle.IndexOf("("));
                }
                searchableTitle = searchableTitle.TrimEnd();

                string searchableArtist = ExistingMetadata.Artist;
                int nIndex = searchableArtist.IndexOf("feat");
                if (nIndex > 0)
                    searchableArtist = searchableArtist.Remove(nIndex);

                if (searchableArtist.IndexOfAny(new char[] { '/', '(', '&' } ) > 0 )
                {
                    nIndex = searchableArtist.IndexOf("(");
                    if( nIndex > 0 )
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

                string query = Properties.Settings.Default.ITunesURL.Replace("%SEARCHTERM%", searchTermEnc);

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
                            if (json?.SelectToken("resultCount") != null)
                            {
                                if ((int)json.SelectToken("resultCount") == 0)
                                {
                                    QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from iTunes";
                                    return false;
                                }
                            }

                            if (json?.SelectToken("results") != null)
                            {
                                if (searchforAlbum)
                                {
                                    RetrievedMetadata = new Id3Tag
                                    {
                                        Album = (string)json.SelectToken("results[0].collectionName"),
                                    };
                                }
                                else
                                {
                                    int trackCount = 0;
                                    var tracks = json["results"];
                                    if (limit == 1)
                                    {
                                        return PerformBestMatchLookup(searchableTitle, searchableArtist, json, tracks);
                                    }

                                    foreach (var track in tracks)
                                    {
                                        RetrievedMetadata = new Id3Tag()
                                        {
                                            Cover = new System.Windows.Forms.PictureBox(),
                                        };

                                        RetrievedMetadata.Title = (string)track["trackName"];
                                        RetrievedMetadata.Artist = (string)track["artistName"];
                                        RetrievedMetadata.Album = (string)track["collectionName"];
                                        RetrievedMetadata.Date = (string)track["releaseDate"];
                                        RetrievedMetadata.Genre = (string)track["primaryGenreName"];
                                        if (track["artworkUrl100"] != null)
                                            RetrievedMetadata.Cover.ImageLocation = (string)track["artworkUrl100"];

                                        ListRetrievedTags.Add(RetrievedMetadata);
                                        trackCount++;
                                        if (trackCount > limit)
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

            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
                return false;
            }

            QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from iTunes";
            return false;
        }

        public bool QueryForArtworkNonAsync(System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                string searchTermEnc = WebUtility.UrlEncode(ExistingMetadata.Artist + " - " + ExistingMetadata.Title);

                string query = Properties.Settings.Default.ITunesURL.Replace("%SEARCHTERM%", searchTermEnc);

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
                            if (json?.SelectToken("resultCount") != null)
                            {
                                if ((int)json.SelectToken("resultCount") == 0)
                                {
                                    QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from iTunes";
                                    return false;
                                }
                            }

                            if (json?.SelectToken("results") != null)
                            {
                                var tracks = json["results"];
                                foreach (var track in tracks)
                                {
                                    RetrievedMetadata = new Id3Tag()
                                    {
                                        Cover = new System.Windows.Forms.PictureBox(),
                                    };
                                    if (track["artworkUrl100"] != null)
                                        RetrievedMetadata.Cover.ImageLocation = (string)track["artworkUrl100"];

                                    break;
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

            QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from iTunes";
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


                    string trackName = (string)track["trackName"];
                    if (trackName.Equals(searchableTitle, StringComparison.OrdinalIgnoreCase) || trackName.IndexOf(searchableTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        RetrievedMetadata.Title = trackName;
                    }

                    string artistName = (string)track["artistName"];
                    if (artistName.Contains(searchableArtist))
                    {
                        RetrievedMetadata.Artist = artistName;
                    }
                    if (!String.IsNullOrEmpty(RetrievedMetadata.Title) && !String.IsNullOrEmpty(RetrievedMetadata.Artist))
                    {
                        RetrievedMetadata.Album = (string)track["collectionName"];
                        RetrievedMetadata.Date = (string)track["releaseDate"];
                        RetrievedMetadata.Genre = (string)track["primaryGenreName"];
                        if (track["artworkUrl100"] != null)
                            RetrievedMetadata.Cover.ImageLocation = (string)track["artworkUrl100"];
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
                        RetrievedMetadata.Album = (string)track["collectionName"];
                        RetrievedMetadata.Date = (string)track["releaseDate"];
                        RetrievedMetadata.Genre = (string)track["primaryGenreName"];
                        if (track["artworkUrl100"] != null)
                            RetrievedMetadata.Cover.ImageLocation = (string)track["artworkUrl100"];
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

