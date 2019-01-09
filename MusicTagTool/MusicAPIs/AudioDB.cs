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
