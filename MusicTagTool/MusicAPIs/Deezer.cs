//-----------------------------------------------------------------------
// <copyright file="Deezer.cs" company="Shiny ID3 Tagger">
// Copyright (c) Shiny ID3 Tagger. All rights reserved.
// </copyright>
// <author>ShinyId3Tagger Team</author>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    /// Class for Deezer API
    /// </summary>
    internal class Deezer : TotalTagger.MusicAPIs.TagService
    {
        public bool QueryForMetadataNonAsync(System.Threading.CancellationToken cancellationToken, bool searchForAlbum, int limit = 25)
        {
            try
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

                string artistEncoded = WebUtility.UrlEncode(searchableArtist);
                string titleEncoded = WebUtility.UrlEncode(searchableTitle);

                string query = "http://api.deezer.com/search?q=artist:\"" + artistEncoded + "\"+track:\"" + titleEncoded + "\"&limit=25&order=RANKING";

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
                                if (searchForAlbum)
                                {
                                    RetrievedMetadata = new Id3Tag
                                    {
                                        Album = (string)json.SelectToken("data[0].album.title"),
                                    };
                                }
                                else
                                {
                                    int trackCount = 0;
                                    var tracks = json["data"];
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
                                        var artist = track["artist"];
                                        RetrievedMetadata.Artist = (string)artist["name"];
                                        RetrievedMetadata.Title = (string)track["title"];

                                        var album = track["album"];
                                        RetrievedMetadata.Album = (string)album["title"];

                                        if (album["cover_medium"] != null)
                                            RetrievedMetadata.Cover.ImageLocation = (string)album["cover_medium"];

                                        ListRetrievedTags.Add(RetrievedMetadata);

                                        trackCount++;
                                        if (trackCount >= limit)
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
            catch (Exception ex)
            {
                QueryResult = ex.Message;
                return false;
            }
            QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from Deezer";
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

                    string trackName = (string)track["title"];
                    if (trackName.Equals(searchableTitle, StringComparison.OrdinalIgnoreCase) || trackName.IndexOf(searchableTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        RetrievedMetadata.Title = trackName;
                    }

                    var artist = track["artist"];
                    string artistName = (string)artist["name"];
                    if (artistName.Equals(searchableArtist, StringComparison.OrdinalIgnoreCase) || artistName.IndexOf(searchableArtist, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        RetrievedMetadata.Artist = artistName;
                    }

                    if (!String.IsNullOrEmpty(RetrievedMetadata.Title) && !String.IsNullOrEmpty(RetrievedMetadata.Artist))
                    {
                        var album = track["album"];
                        RetrievedMetadata.Album = (string)album["title"];

                        if (album["cover_medium"] != null)
                            RetrievedMetadata.Cover.ImageLocation = (string)album["cover_medium"];

                        QueryResult = "Success";
                        ResultOfQuery = true;
                        return true;
                    }
                }

                foreach (var track in tracks)
                {
                    RetrievedMetadata = new Id3Tag();

                    string trackName = (string)track["title"];
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

                    var artist = track["artist"];
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
                        var album = track["album"];
                        RetrievedMetadata.Album = (string)album["title"];

                        if (album["cover_medium"] != null)
                            RetrievedMetadata.Cover.ImageLocation = (string)album["cover_medium"];

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
