using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TotalTagger
{

    /// <summary>
    /// Class for MusixMatch API
    /// </summary>
    internal class MusixMatch : TotalTagger.MusicAPIs.TagService
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

                Utils.HttpRequestClass client = new Utils.HttpRequestClass();
                if (client != null)
                {
                    string query = "http://api.musixmatch.com/ws/1.1/matcher.track.get?q_artist=" + artistEncoded + "&q_track=" + titleEncoded + "&apikey=" + MainWindow.serviceSettings.MusixMatchKey;

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    string results = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    if (results != null)
                    {
                        var json = (JObject)JsonConvert.DeserializeObject(results);
                        if (json != null)
                        {
                            if (json?.SelectToken("message.body.track") != null)
                            {
                                var track = json?.SelectToken("message.body.track");
                                RetrievedMetadata = new Id3Tag()
                                {
                                    Cover = new System.Windows.Forms.PictureBox(),
                                };
                                RetrievedMetadata.Artist = (string)track["artist_name"];
                                RetrievedMetadata.Title = (string)track["track_name"];
                                RetrievedMetadata.Album = (string)track["album_name"];

                                string coverArt = (string)track["album_coverart_100x100"];
                                if (String.IsNullOrEmpty(coverArt))
                                {
                                    coverArt = (string)track["album_coverart_350x350"];
                                }

                                //if (!String.IsNullOrEmpty(coverArt))
                                //{
                                //    RetrievedMetadata.Cover.ImageLocation = coverArt;
                                //}

                                var primaryGenres = track.SelectToken("primary_genres");
                                foreach (var genre in primaryGenres["music_genre_list"])
                                {
                                    if(genre["music_genre"]["music_genre_name"] != null )
                                        RetrievedMetadata.Genre += (string)genre["music_genre"]["music_genre_name"];
                                }

                                if(track["first_release_date"] != null )
                                {
                                    RetrievedMetadata.Date = (string)track["first_release_date"];
                                }
                                
                                if (ListRetrievedTags != null)
                                {
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
        public bool QueryForArtworkNonAsync(System.Threading.CancellationToken cancellationToken)
        {
            //try
            //{
            //    string searchableTitle = ExistingMetadata.Title;
            //    if (searchableTitle.Contains("("))
            //    {
            //        searchableTitle = searchableTitle.Remove(searchableTitle.IndexOf("("));
            //    }
            //    searchableTitle = searchableTitle.TrimEnd();

            //    string searchableArtist = ExistingMetadata.Artist;
            //    int nIndex = searchableArtist.IndexOf("feat");
            //    if (nIndex > 0)
            //        searchableArtist = searchableArtist.Remove(nIndex);

            //    if (searchableArtist.IndexOfAny(new char[] { '/', '(', '&' }) > 0)
            //    {
            //        nIndex = searchableArtist.IndexOf("(");
            //        if (nIndex > 0)
            //            searchableArtist = searchableArtist.Remove(nIndex);

            //        nIndex = searchableArtist.IndexOf("/");
            //        if (nIndex > 0)
            //            searchableArtist = searchableArtist.Remove(nIndex);

            //        nIndex = searchableArtist.IndexOf("&");
            //        if (nIndex > 0)
            //            searchableArtist = searchableArtist.Remove(nIndex);
            //    }

            //    searchableArtist = searchableArtist.TrimEnd();

            //    string artistEncoded = WebUtility.UrlEncode(searchableArtist);
            //    string titleEncoded = WebUtility.UrlEncode(searchableTitle);

            //    Utils.HttpRequestClass client = new Utils.HttpRequestClass();
            //    if (client != null)
            //    {
            //        string query = "http://api.musixmatch.com/ws/1.1/matcher.track.get?q_artist=" + artistEncoded + "&q_track=" + titleEncoded + "&apikey=7a34b0e264e038038d1addbe61e14f69";

            //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
            //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //        string results = new StreamReader(response.GetResponseStream()).ReadToEnd();

            //        if (results != null)
            //        {
            //            var json = (JObject)JsonConvert.DeserializeObject(results);
            //            if (json != null)
            //            {
            //                if (json?.SelectToken("message.body.track") != null)
            //                {
            //                    var track = json?.SelectToken("message.body.track");
            //                    if( track != null )
            //                    {
            //                        string albumid = (string)track["album_id"];
            //                        query = "http://api.musixmatch.com/ws/1.1/album.get?album_id=" + albumid + "&apikey=7a34b0e264e038038d1addbe61e14f69";
            //                        request = (HttpWebRequest)WebRequest.Create(query);
            //                        response = (HttpWebResponse)request.GetResponse();
            //                        results = new StreamReader(response.GetResponseStream()).ReadToEnd();

            //                        if (results != null)
            //                        {
            //                            json = (JObject)JsonConvert.DeserializeObject(results);
            //                            if (json != null)
            //                            {
            //                                if (json?.SelectToken("message.body.album") != null)
            //                                {
            //                                    var album = json?.SelectToken("message.body.album");
            //                                    if (album != null)
            //                                    {
            //                                        RetrievedMetadata = new Id3Tag()
            //                                        {
            //                                            Cover = new System.Windows.Forms.PictureBox(),
            //                                        };

            //                                        RetrievedMetadata.Cover.ImageLocation =  (string)track["album_coverart_100x100"];

            //                                        QueryResult = "Success";
            //                                        ResultOfQuery = true;
            //                                        return true;
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    QueryResult = ex.Message;
            //    return false;
            //}
            //QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from Deezer";
            return false;
        }
        public bool QueryForAlbum(System.Threading.CancellationToken cancellationToken)
        {
            //try
            //{
            //    string artistEncoded = WebUtility.UrlEncode(ExistingMetadata.Artist);
            //    string titleEncoded = WebUtility.UrlEncode(ExistingMetadata.Title);

            //    string query = "http://api.deezer.com/search?q=artist:\"" + artistEncoded + "\"+track:\"" + titleEncoded + "\"&limit=" + 1 + "&order=RANKING";

            //    query = query.Replace("%SEARCHARTIST%", artistEncoded);
            //    query = query.Replace("%SEARCHTITLE%", titleEncoded);

            //    Utils.HttpRequestClass client = new Utils.HttpRequestClass();
            //    if (client != null)
            //    {
            //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
            //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //        string results = new StreamReader(response.GetResponseStream()).ReadToEnd();

            //        if (results != null)
            //        {
            //            var json = (JObject)JsonConvert.DeserializeObject(results);
            //            if (json != null)
            //            {
            //                if (json?.SelectToken("data") != null)
            //                {
            //                    var tracks = json["data"];
            //                    foreach (var track in tracks)
            //                    {
            //                        RetrievedMetadata = new Id3Tag()
            //                        {
            //                            Cover = new System.Windows.Forms.PictureBox(),
            //                        };
            //                        var album = track["album"];
            //                        RetrievedMetadata.Album = (string)album["title"];
            //                        ListRetrievedTags.Add(RetrievedMetadata);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    QueryResult = ex.Message;
            //    return false;
            //}

            QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from Deezer";
            return false;
        }
    }
}
