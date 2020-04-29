using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TotalTagger
{
    public class TheAudioDBData
    {

        public class Rootobject
        {
            public Track[] track { get; set; }
        }

        public class Track
        {
            public string idTrack { get; set; }
            public string idAlbum { get; set; }
            public string idArtist { get; set; }
            public object idLyric { get; set; }
            public string idIMVDB { get; set; }
            public string strTrack { get; set; }
            public string strAlbum { get; set; }
            public string strArtist { get; set; }
            public object strArtistAlternate { get; set; }
            public object intCD { get; set; }
            public string intDuration { get; set; }
            public string strGenre { get; set; }
            public string strMood { get; set; }
            public string strStyle { get; set; }
            public string strTheme { get; set; }
            public string strDescriptionEN { get; set; }
            public string strTrackThumb { get; set; }
            public string strTrackLyrics { get; set; }
            public string strMusicVid { get; set; }
            public string strMusicVidDirector { get; set; }
            public string strMusicVidCompany { get; set; }
            public object strMusicVidScreen1 { get; set; }
            public object strMusicVidScreen2 { get; set; }
            public object strMusicVidScreen3 { get; set; }
            public string intMusicVidViews { get; set; }
            public string intMusicVidLikes { get; set; }
            public string intMusicVidDislikes { get; set; }
            public string intMusicVidFavorites { get; set; }
            public string intMusicVidComments { get; set; }
            public string intTrackNumber { get; set; }
            public string intLoved { get; set; }
            public string intScore { get; set; }
            public string intScoreVotes { get; set; }
            public string intTotalListeners { get; set; }
            public string intTotalPlays { get; set; }
            public string strMusicBrainzID { get; set; }
            public string strMusicBrainzAlbumID { get; set; }
            public string strMusicBrainzArtistID { get; set; }
            public string strLocked { get; set; }
        }

    }
    public class AudioDB : TotalTagger.MusicAPIs.TagService
    {

        public bool QueryForMetadata()
        {
            ListRetrievedTags = new BindingList<Id3Tag>();
            string encodedTitle = System.Web.HttpUtility.UrlEncode(MetadataTitle);
            string encodeArtist = System.Web.HttpUtility.UrlEncode(MetadataArtist);
            var query = Properties.Settings.Default.AudioDBUrl + encodeArtist + "&t=" + encodedTitle;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (content != null)
                {

                    var json = (JObject)JsonConvert.DeserializeObject(content);

                    if (json != null && json["track"] != null)
                    {
                        if (json["track"].Any())
                        {
                            string trackTitle = (string)json["track"][0]["strTrack"];
                            string trackArtist = (string)json["track"][0]["strArtist"];
                            string trackAlbum = (string)json["track"][0]["strAlbum"];
                            string trackGenre = (string)json["track"][0]["strGenre"];

                            RetrievedMetadata = new Id3Tag
                            {
                                Title = trackTitle,
                                Artist = trackArtist,
                                Album = trackAlbum,
                                Genre = trackGenre
                            };

                            if (json["track"][0]["strTrackThumb"] != null)
                            {
                                //string imageLocation = track["strTrackThumb"].ToString();
                                RetrievedMetadata.Cover.ImageLocation = json["track"][0]["strTrackThumb"].ToString();
                            }

                            ListRetrievedTags.Add(RetrievedMetadata);
                            return true;
                        }
                    }
                }
                QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from TheAudioDB";
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
            }

            return false;
        }

    }
}
