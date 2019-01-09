using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TotalTagger
{
    class MusicBrainz : TotalTagger.MusicAPIs.TagService
    {
        public bool QueryForMetadataNonAsync(System.Threading.CancellationToken cancellationToken, int limit = 25)
        {
            if (String.IsNullOrEmpty(ExistingMetadata.Title))
            {
                QueryResult = "Title Needed To Perform Search From MusicBrainz";
                ResultOfQuery = false;
                return false;
            }

            string encodedTitle = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Title);
            if (String.IsNullOrEmpty(encodedTitle))
            {
                QueryResult = "Title Needed To Perform Search From MusicBrainz";
                ResultOfQuery = false;
                return false;
            }

            string encodedArtist = "";
            string query = "";
            if (!String.IsNullOrEmpty(ExistingMetadata.Artist))
            {
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

                encodedArtist = System.Web.HttpUtility.UrlEncode(searchableArtist);
                query = Properties.Settings.Default.MBRecordingUrl + "recording:" + encodedTitle + "%20AND%20artist:" + encodedArtist;
            }
            else
            {
                query = Properties.Settings.Default.MBRecordingUrl + "recording:" + encodedTitle;
            }

            try
            {
                string fullUserAgent = $"{Properties.Settings.Default.MBBaseUrl} {Properties.Settings.Default.MBBaseUrl}/{Properties.Settings.Default.MBVersion} ({Properties.Settings.Default.MBUserAgentUrl})";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
                request.UserAgent = fullUserAgent;
                request.Accept = "application/xml";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string results = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (results != null)
                {
                    XElement root = XElement.Parse(results);
                    XNamespace nameSpace = root.Name.Namespace;
                    var recordinglist = root.Element(nameSpace + "recording-list");
                    if (recordinglist == null)
                    {
                        QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from MusicBrainz";
                        ResultOfQuery = false;
                        return false;
                    }

                    var recording = recordinglist.Element(nameSpace + "recording");
                    if (recording == null)
                    {
                        QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from MusicBrainz";
                        ResultOfQuery = false;
                        return false;
                    }
                    string trackArtist = recording.Element(nameSpace + "artist-credit").Element(nameSpace + "name-credit").Element(nameSpace + "artist").Element(nameSpace + "name").Value.ToString();
                    string trackTitle = recording.Element(nameSpace + "title").Value.ToString();

                    var releases = from f in recording.Elements(nameSpace + "release-list").Elements(nameSpace + "release")
                                   select f;
                    if (releases.Any())
                    {
                        if (limit == 1)
                        {
                            foreach (var release in releases)
                            {
                                string trackAlbum = "";
                                if (release.Element(nameSpace + "album") != null)
                                {
                                    trackAlbum = release.Element(nameSpace + "album").Value.ToString();
                                }

                                RetrievedMetadata = new Id3Tag
                                {
                                    Title = trackTitle,
                                    Artist = trackArtist,
                                    Album = trackAlbum,
                                };
                                ResultOfQuery = true;
                                return true;
                            }
                        }
                        else
                        {
                            int trackCount = 1;
                            foreach (var release in releases)
                            {
                                string trackAlbum = "";
                                if (release.Element(nameSpace + "album") != null)
                                {
                                    trackAlbum = release.Element(nameSpace + "album").Value.ToString();
                                }

                                RetrievedMetadata = new Id3Tag
                                {
                                    Title = trackTitle,
                                    Artist = trackArtist,
                                    Album = trackAlbum,
                                };
                                if (limit > 1)
                                    ListRetrievedTags.Add(RetrievedMetadata);
                                trackCount++;
                                if (trackCount > limit)
                                {
                                    break;
                                }
                            }

                            ResultOfQuery = true;
                            return true;
                        }
                    }
                }
                QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from MusicBrainz";
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
            }

            return false;
        }

        public bool QueryForRelease(System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                string encodedArtist = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Artist.TrimEnd());
                string query = query = Properties.Settings.Default.MBReleaseUrl + "artist:" + encodedArtist;
                string fullUserAgent = $"{Properties.Settings.Default.MBBaseUrl} {Properties.Settings.Default.MBBaseUrl}/{Properties.Settings.Default.MBVersion} ({Properties.Settings.Default.MBUserAgentUrl})";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
                request.UserAgent = fullUserAgent;
                request.Accept = "application/xml";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string results = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (results != null)
                {
                    XElement root = XElement.Parse(results);
                    XNamespace nameSpace = root.Name.Namespace;
                    var releases = from f in root.Elements(nameSpace + "release-list").Elements(nameSpace + "release")
                                   select f;
                    if (releases.Any())
                    {
                        foreach (var release in releases)
                        {
                            string trackAlbum = "";
                            if (release.Element(nameSpace + "release-group") != null)
                            {
                                if (release.Element(nameSpace + "release-group").Element(nameSpace + "title") != null)
                                    trackAlbum = release.Element(nameSpace + "release-group").Element(nameSpace + "title").Value.ToString();
                            }

                            RetrievedMetadata = new Id3Tag
                            {
                                Album = trackAlbum,
                            };
                            ListRetrievedTags.Add(RetrievedMetadata);
                        }

                        ResultOfQuery = true;
                        return true;
                    }
                }
                QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from MusicBrainz";
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
            }

            return false;
        }

    }
}
