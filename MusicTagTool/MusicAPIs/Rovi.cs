//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

//namespace TotalTagger
//{
//    class Rovi : TotalTagger.MusicAPIs.QueryMusicService
//    {

//        private string CreateMD5(string input)
//        {
//            StringBuilder sb = new StringBuilder();
//            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
//            {
//                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
//                byte[] hashBytes = md5.ComputeHash(inputBytes);

                
//                for(int i=0; i < hashBytes.Length; i++)
//                {
//                    sb.Append(hashBytes[i].ToString("X2"));
//                }

//            }
//            return sb.ToString().ToLower();
//        }
//        public bool QueryForMetadata()
//        {
//            string sigKey = CreateMD5(Properties.Settings.Default.RoviSearchKey + Properties.Settings.Default.RoviSearchSharedSecret + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString());
//            string encodedTitle = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Title);

//            string encodeArtist = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Artist.TrimEnd());
//            var query = Properties.Settings.Default.RoviBaseUrl + Properties.Settings.Default.RoviBaseUrlApiKey + "&sig=" + sigKey + "&track=" + encodedTitle + "&artist=" + encodeArtist;

//            try
//            {
//                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
//                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//                string results = new StreamReader(response.GetResponseStream()).ReadToEnd();

//                if (results != null)
//                {
//                    XElement root = XElement.Parse(results);
//                    if (root != null)
//                    {
//                        string artwork = "";

//                        string title = root.Element("song").Element("title").Value.ToString();
//                        var artists = from f in root.Element("song").Element("primaryArtists").Elements("AlbumArtist")
//                                     select f;
//                        string primaryArtists = "";
//                        if( artists.Any())
//                        {
//                            foreach(var primaryArtist in artists)
//                            {
//                                if( primaryArtists.Length > 0 )
//                                {
//                                    primaryArtists += "/";
//                                }
//                                primaryArtists += primaryArtist.Element("name").Value.ToString();
//                            }
//                        }
//                        var genres = from f in root.Element("song").Element("genres").Elements("Genre")
//                                      select f;
//                        string primaryGenres = "";
//                        if (genres.Any())
//                        {
//                            foreach (var primaryGenre in genres)
//                            {
//                                if (primaryGenres.Length > 0)
//                                {
//                                    primaryArtists += ",";
//                                }
//                                primaryGenres += primaryGenre.Element("name").Value.ToString();
//                            }
//                        }
//                        string album = "";

//                        RetrievedMetadata = new Id3Tag
//                        {
//                            Title = title,
//                            Artist = primaryArtists,
//                            Album = album,
//                            Genre = primaryGenres,
//                        };
//                        ListMetadata.Add(RetrievedMetadata);
//                        QueryResult = "Success";
//                        return true;
//                    }
//                }
//                QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from Rovi";
//            }
//            catch (Exception ex)
//            {
//                QueryResult = ex.Message;
//            }

//            return false;
//        }

//    }
//}
