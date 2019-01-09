using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TotalTagger
{
    class Galiboo : TotalTagger.MusicAPIs.TagService
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
                    string query = Properties.Settings.Default.GalibooUrl;
                    query = query.Replace("%APIKEY%", MainWindow.serviceSettings.GalibooClientKey);
                    query = query.Replace("%SEARCHTITLE%", titleEncoded);
                    //query = query.Replace("%SEARCHARTIST%", artistEncoded);

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
                                    if (genre["music_genre"]["music_genre_name"] != null)
                                        RetrievedMetadata.Genre += (string)genre["music_genre"]["music_genre_name"];
                                }

                                if (track["first_release_date"] != null)
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


    }
}
