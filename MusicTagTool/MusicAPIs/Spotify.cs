using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Auth; //All Authentication-related classes
using SpotifyAPI.Web.Enums; //Enums
using SpotifyAPI.Web.Models; //Models for the JSON-responses
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TotalTagger
{
    class Spotify : TotalTagger.MusicAPIs.TagService
    {
        public bool QueryForMetadataNonAsync(System.Threading.CancellationToken cancellationToken, int limit = 25)
        {
            try
            {
                string query = "";
                SearchType searchType = new SearchType();
                if (String.IsNullOrEmpty(ExistingMetadata.Title))
                {
                    QueryResult = "Title Needed To Perform Search From Spotify";
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
                query += "track:" + encodedTitle;
                searchType |= SearchType.Track;

                string encodedArtist = "";
                if (!String.IsNullOrEmpty(searchableArtist))
                {
                    encodedArtist = System.Web.HttpUtility.UrlEncode(searchableArtist);
                    query += "%20artist:" + encodedArtist;
                }

                //q = album:gold % 20artist: abba & type = album

                ClientCredentialsAuth auth = new ClientCredentialsAuth()
                {
                    //Your client Id
                    ClientId = MainWindow.serviceSettings.SpotifyClientID,
                    //Your client secret UNSECURE!!
                    ClientSecret = MainWindow.serviceSettings.SpotifyClientKey,
                    //How many permissions we need?
                    Scope = Scope.UserReadPrivate,
                };

                //With this token object, we now can make calls
                Token token = auth.DoAuth();
                var spotify = new SpotifyWebAPI()
                {
                    TokenType = token.TokenType,
                    AccessToken = token.AccessToken,
                    UseAuth = true
                };

                SearchItem item = spotify.SearchItems(query, searchType);
                if (item != null)
                {
                    if (item.Tracks.Items.Count > 0)
                    {
                        if (limit == 1)
                        {
                            return PerformBestMatchLookup(searchableTitle, searchableArtist, item.Tracks.Items);
                        }

                        foreach (var track in item.Tracks.Items)
                        {
                            RetrievedMetadata = new Id3Tag
                            {
                                Title = track.Name,
                                Cover = new System.Windows.Forms.PictureBox()
                            };

                            if (track.Artists.Count > 0)
                            {
                                SimpleArtist artist = track.Artists[0];
                                RetrievedMetadata.Artist = artist.Name;
                            }

                            RetrievedMetadata.Album = track.Album.Name;

                            ListRetrievedTags.Add(RetrievedMetadata);
                        }

                        if( ListRetrievedTags.Count > 0 )
                        {
                            QueryResult = "Success";
                            ResultOfQuery = true;
                            return true;
                        }
                    }
                }

                QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from Spotify";
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
            }

            return false;
        }
        public bool QueryForAlbum(System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                string query = "";
                SearchType searchType = new SearchType();

                string encodedArtist = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Artist.TrimEnd());
                query += "artist:" + encodedArtist;

                searchType |= SearchType.Album;


                ClientCredentialsAuth auth = new ClientCredentialsAuth()
                {
                    ClientId = MainWindow.serviceSettings.SpotifyClientID,
                    ClientSecret = MainWindow.serviceSettings.SpotifyClientKey,
                    Scope = Scope.UserReadPrivate,
                };

                Token token = auth.DoAuth();
                var spotify = new SpotifyWebAPI()
                {
                    TokenType = token.TokenType,
                    AccessToken = token.AccessToken,
                    UseAuth = true
                };

                SearchItem item = spotify.SearchItems(query, searchType);
                if (item != null)
                {
                    if (item.Albums.Items.Count > 0)
                    {
                        foreach (var album in item.Albums.Items)
                        {
                            RetrievedMetadata = new Id3Tag
                            {
                                Album = album.Name,
                            };

                            ListRetrievedTags.Add(RetrievedMetadata);
                        }
                    }

                    QueryResult = "Success";
                        ResultOfQuery = true;
                        return true;
                }

                QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from Spotify";
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
                string query = "";
                SearchType searchType = new SearchType();

                string encodedArtist = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Artist.TrimEnd());
                query = "artist:" + encodedArtist;

                string encodedAlbum = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Album.TrimEnd());
                query += "%20album:" + encodedAlbum;
                searchType |= SearchType.Album;

                ClientCredentialsAuth auth = new ClientCredentialsAuth()
                {
                    ClientId = MainWindow.serviceSettings.SpotifyClientID,
                    ClientSecret = MainWindow.serviceSettings.SpotifyClientKey,
                    Scope = Scope.UserReadPrivate,
                };

                Token token = auth.DoAuth();
                var spotify = new SpotifyWebAPI()
                {
                    TokenType = token.TokenType,
                    AccessToken = token.AccessToken,
                    UseAuth = true
                };

                SearchItem item = spotify.SearchItems(query, searchType);
                if (item != null)
                {
                    if( item.Albums.Items.Count > 0)
                    {
                        foreach (var album in item.Albums.Items)
                        {
                            RetrievedMetadata = new Id3Tag
                            {
                                Cover = new System.Windows.Forms.PictureBox()
                            };

                            foreach(var image in album.Images)
                            {
                                RetrievedMetadata.Cover.ImageLocation = image.Url;
                                QueryResult = "Success";
                                ResultOfQuery = true;
                                return true;
                            }
                        }
                    }
                }

                QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from Spotify";
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
            }

            return false;
        }

        private bool PerformBestMatchLookup(string searchableTitle, string searchableArtist, List<FullTrack> items)
        {
            try
            {
                foreach (var track in items)
                {
                    RetrievedMetadata = new Id3Tag
                    {
                        Cover = new System.Windows.Forms.PictureBox()
                    };
                    if (track.Name.Equals(searchableTitle, StringComparison.OrdinalIgnoreCase) || track.Name.IndexOf(searchableTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        RetrievedMetadata.Title = track.Name;
                    }

                    if (track.Artists.Count > 0)
                    {
                        foreach(var artist in track.Artists)
                        {
                            if (artist.Name.Equals(searchableArtist, StringComparison.OrdinalIgnoreCase) || artist.Name.IndexOf(searchableArtist, StringComparison.OrdinalIgnoreCase) > 0)
                            {
                                RetrievedMetadata.Artist = artist.Name;
                                break;
                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(RetrievedMetadata.Title) && !String.IsNullOrEmpty(RetrievedMetadata.Artist))
                    {
                        RetrievedMetadata.Album = track.Album.Name;
                        QueryResult = "Success";
                        ResultOfQuery = true;
                        return true;
                    }
                }


                foreach (var track in items)
                {
                    RetrievedMetadata = new Id3Tag
                    {
                        Cover = new System.Windows.Forms.PictureBox()
                    };
                    if (track.Name.Equals(searchableTitle, StringComparison.OrdinalIgnoreCase) || track.Name.IndexOf(searchableTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        RetrievedMetadata.Title = track.Name;
                    }
                    else
                    {
                        string[] wordsInTitle = searchableTitle.Split(' ');
                        bool[] wordMatches = new bool[wordsInTitle.Length];
                        int matchCount = 0;
                        foreach (var word in wordsInTitle)
                        {
                            if (track.Name.IndexOf(word, StringComparison.CurrentCultureIgnoreCase) >= 0)
                            {
                                wordMatches[matchCount] = true;
                                matchCount++;
                            }
                        }

                        if (matchCount == wordsInTitle.Length)
                        {
                            RetrievedMetadata.Title = track.Name;
                        }
                    }

                    if (track.Artists.Count > 0)
                    {
                        foreach (var artist in track.Artists)
                        {
                            if (artist.Name.Equals(searchableArtist, StringComparison.OrdinalIgnoreCase) || artist.Name.IndexOf(searchableArtist, StringComparison.OrdinalIgnoreCase) > 0)
                            {
                                RetrievedMetadata.Artist = artist.Name;
                                break;
                            }
                            else
                            {
                                string[] wordsInArtistName = searchableArtist.Split(' ');
                                bool[] wordMatches = new bool[wordsInArtistName.Length];
                                int matchCount = 0;
                                foreach (var word in wordsInArtistName)
                                {
                                    if (artist.Name.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                                    {
                                        wordMatches[matchCount] = true;
                                        matchCount++;
                                    }
                                }

                                if (matchCount == wordsInArtistName.Length)
                                {
                                    RetrievedMetadata.Artist = artist.Name;
                                    break;
                                }
                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(RetrievedMetadata.Title) && (track.Artists.Count == 0 || !String.IsNullOrEmpty(RetrievedMetadata.Artist)) )
                    {
                        RetrievedMetadata.Album = track.Album.Name;
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
