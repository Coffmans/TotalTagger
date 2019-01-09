using DiscogsClient;
using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using DiscogsClient.Internal;
//using Newtonsoft.Json;
//using RestSharp;
//using RestSharpHelper.OAuth1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
//using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TotalTagger
{
    public class Discogs : TotalTagger.MusicAPIs.TagService
    {
        public async Task<bool> QueryForMetadata(System.Threading.CancellationToken cancellationToken, int limit = 25)
        {
            try
            {
                if (String.IsNullOrEmpty(ExistingMetadata.Title))
                {
                    QueryResult = "Title Needed To Perform Search From Discogs";
                    ResultOfQuery = false;
                    return false;
                }

                var tokenInformation = new TokenAuthenticationInformation("");
                var discogsClient = new DiscogsClient.DiscogsClient(tokenInformation);

                DiscogsSearch discogsSearch = new DiscogsSearch();
                discogsSearch.release_title = ExistingMetadata.Title;
                if (!String.IsNullOrEmpty(ExistingMetadata.Artist))
                {
                    discogsSearch.artist = ExistingMetadata.Artist;
                };

                var observable = discogsClient.Search(discogsSearch);
                await observable.ForEachAsync(OnResult);
                if (ListRetrievedTags.Any())
                {
                    QueryResult = "Success";
                    ResultOfQuery = true;
                    return true;
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

        public bool QueryForMetadataNonAsync(System.Threading.CancellationToken cancellationToken, int limit = 25)
        {
            try
            {
                if (String.IsNullOrEmpty(ExistingMetadata.Title))
                {
                    QueryResult = "Title Needed To Perform Search From Discogs";
                    ResultOfQuery = false;
                    return false;
                }

                string encodedArtist = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Artist);
                string encodedTitle = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Title);

                string searchTermEnc = "track=" + encodedTitle + "&artist=" + encodedArtist;
                string query = "https://api.discogs.com/database/search?q=" + searchTermEnc +
                        //"&format=album" +
                        "&type=master" +
                        "&per_page=1" +
                        "&key=" + MainWindow.serviceSettings.DiscogsClientID +
                        "&secret=" + MainWindow.serviceSettings.DiscogsClientKey;

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
                //request.UserAgent = "TotalTagger/1.2";
                ////request.Accept = "application/json";
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //string results = new StreamReader(response.GetResponseStream()).ReadToEnd();

                //if (results != null)
                //{
                //    var json = (JObject)JsonConvert.DeserializeObject(results);
                //    if (json != null)
                //    {
                //        if (json.SelectToken("results").Any())
                //        {
                //            string albumUrl = (string)json.SelectToken("results[0].resource_url");
                //            if (albumUrl != null )
                //            {
                //                query = albumUrl +
                //                    "&key=" + Properties.Settings.Default.DiscogsConsumerKey +
                //                    "&secret=" + Properties.Settings.Default.DiscogsConsumerSecret;
                //                request = (HttpWebRequest)WebRequest.Create(query);
                //                request.UserAgent = "TotalTagger/1.2";
                //                response = (HttpWebResponse)request.GetResponse();
                //                results = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //                if (results != null)
                //                {
                //                    json = (JObject)JsonConvert.DeserializeObject(results);
                //                    if (json != null)
                //                    {
                //                        int trackCount = 0;
                //                        var tracks = json["tracklist"];
                //                        foreach (var track in tracks)
                //                        {
                //                            RetrievedMetadata = new Id3Tag()
                //                            {
                //                                Cover = new System.Windows.Forms.PictureBox(),
                //                            };
                //                            RetrievedMetadata.Artist = (string)track["artistName"];
                //                            RetrievedMetadata.Title = (string)track["title"];
                //                            RetrievedMetadata.Album = (string)track["albumName"];
                //                            RetrievedMetadata.Album = (string)track["collectionName"];
                //                            RetrievedMetadata.Date = (string)track["year"];
                //                            RetrievedMetadata.Genre = (string)track["primaryGenreName"];
                //                            var genres = track["genres"];
                //                            foreach (var genre in genres)
                //                            {
                //                                RetrievedMetadata.Genre += (string)genre;
                //                            }

                //                            if (track["artworkUrl100"] != null)
                //                                RetrievedMetadata.Cover.ImageLocation = (string)track["artworkUrl100"];

                //                            if (limit > 1)
                //                                ListRetrievedTags.Add(RetrievedMetadata);

                //                            trackCount++;
                //                            if (trackCount > limit)
                //                            {
                //                                break;
                //                            }
                //                        }
                //            }
                //        }
                //    }
                //}
                ////else
                //{
                var tokenInformation = new TokenAuthenticationInformation("rynMHxmSpTwjTOQQAwRIBIikpFfGXJJYVXioZYFp");
                var discogsClient = new DiscogsClient.DiscogsClient(tokenInformation);

                DiscogsSearch discogsSearch = new DiscogsSearch();
                discogsSearch.release_title = ExistingMetadata.Title;
                if (!String.IsNullOrEmpty(ExistingMetadata.Artist))
                {
                    discogsSearch.artist = ExistingMetadata.Artist;
                };

                var observable = discogsClient.Search(discogsSearch);

                try
                {
                    Parallel.ForEach<DiscogsSearchResult>(observable.ToEnumerable(), item => OnResult(item, limit));
                    if (limit > 1)
                    {
                        if (!ListRetrievedTags.Any())
                        {
                            QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from Discogs";
                            ResultOfQuery = false;
                            return false;
                        }
                    }
                    QueryResult = "Success";
                    ResultOfQuery = true;
                    return true;
                }
                catch (System.Exception ex)
                {
                    QueryResult = ex.Message;
                }
            
                QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from Discogs";
                ResultOfQuery = false;
                return false;
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
            }

            ResultOfQuery = false;
            return false;
        }

        private void OnResult(DiscogsSearchResult result, int limit)
        {
            try
            {
                //Id3Tag Id3Tag = gSongMetaData;
                RetrievedMetadata = new Id3Tag
                {
                    Title = result.title,
                    //Artist = result.
                    //Album = result.
                    Date = result.year.ToString(),
                    Cover = new System.Windows.Forms.PictureBox()
                };

                foreach (var genre in result.genre)
                {
                    if (!String.IsNullOrEmpty(RetrievedMetadata.Genre))
                    {
                        RetrievedMetadata.Genre += ", ";
                    }

                    RetrievedMetadata.Genre += genre.ToString();
                }

                if (result.thumb != null && result.thumb.Length > 0)
                {
                    RetrievedMetadata.Cover.ImageLocation = result.thumb.ToString();
                }

                if( limit == 1)
                {
                    return;
                }

                if ( ListRetrievedTags.Count() <= limit)
                    ListRetrievedTags.Add(RetrievedMetadata);
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message, "OnResult", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
