using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;

namespace TotalTagger
{
    class DownloadArtwork
    {
        static public Bitmap DownloadBitmapImage(string _URL)
        {
            System.Drawing.Bitmap bmp = null;
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(_URL);
                myRequest.Method = "GET";
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
                myResponse.Close();

                return bmp;
            }
            catch (System.Exception )
            {
                //Logging.LogErrorMessage("Exception caught in process: " + ex.ToString());
            }

            return bmp;
        }

        static public Image DownloadImage(string _URL)
        {
            Image image = null;
            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_URL);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();

                System.IO.Stream stream = webResponse.GetResponseStream();

                image = System.Drawing.Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception )
            {
                //Logging.LogErrorMessage("Exception caught in process: " + _Exception.ToString());
                return null;
            }
            return image;
        }

    }
}
