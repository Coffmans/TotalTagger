using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalTagger
{
    public class ConfigData
    {
        public static string GetConfigData(string sKey)
        {
            return ConfigurationManager.AppSettings[sKey];
        }

        public static void SetConfigData(string sKey, string sValue)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(sKey);
            config.AppSettings.Settings.Add(sKey, sValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string EncryptData(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return "";
            }

            using (Rijndael myRijndael = Rijndael.Create())
            {
                byte[] byteKey = myRijndael.Key;
                if (string.IsNullOrEmpty(Properties.Resources.RijndaelKey) == false)
                {
                    byteKey = Encoding.ASCII.GetBytes(Properties.Resources.RijndaelKey);
                }

                byte[] byteIV = myRijndael.IV;
                if (string.IsNullOrEmpty(Properties.Resources.RijndaelIV) == false)
                {
                    byteIV = Encoding.ASCII.GetBytes(Properties.Resources.RijndaelIV);
                }

                // Encrypt the string to an array of bytes. 
                byte[] encrypted = Encryption.EncryptStringToBytes(password, byteKey, byteIV);

                return System.Convert.ToBase64String(encrypted);
            }
        }

        public static string DecryptData(string data)
        {
            if (String.IsNullOrEmpty(data))
            {
                return "";
            }

            byte[] encryptedPassword = System.Convert.FromBase64String(data);

            using (Rijndael myRijndael = Rijndael.Create())
            {
                myRijndael.GenerateKey();
                myRijndael.GenerateIV();
                string key = myRijndael.Key.ToString();
                string iv = myRijndael.IV.ToString();
                byte[] byteKey = myRijndael.Key;
                key = byteKey.ToString();
                if (string.IsNullOrEmpty(Properties.Resources.RijndaelKey) == false)
                {
                    byteKey = System.Convert.FromBase64String(Properties.Resources.RijndaelKey);
                }

                byte[] byteIV = myRijndael.IV;
                if (string.IsNullOrEmpty(Properties.Resources.RijndaelKey) == false)
                {
                    byteIV = System.Convert.FromBase64String(Properties.Resources.RijndaelIV);
                }

                // Encrypt the string to an array of bytes. 
                return Encryption.DecryptStringFromBytes(encryptedPassword, byteKey, byteIV);
            }

        }

        public static string EncryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }

        public static string DecryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }
    }

    public class AppSettings
    {
        public string SpotifyClientID { get; set; }
        public string SpotifyClientKey { get; set; }
        public string NapsterClientKey { get; set; }
        public string LastFMClientKey { get; set; }
        public string GeniusClientKey { get; set; }
        public string GalibooClientKey { get; set; }
        public string DiscogsClientID { get; set; }
        public string DiscogsClientKey { get; set; }

        public string MusixMatchKey { get; set; }

        public AppSettings()
        {
            SpotifyClientID = "";
            SpotifyClientKey = "";
            NapsterClientKey = "";
            LastFMClientKey = "";
            GeniusClientKey = "";
            GalibooClientKey = "";
            DiscogsClientID = "";
            DiscogsClientKey = "";
        }
    }

    [Flags]
    enum MetadataService
    {
        None        = 0x00000000,
        iTunes      = 0x00000001,
        Discogs     = 0x00000002,
        MusicBrainz = 0x00000004,
        AudioDB     = 0x00000008,
        Deezer      = 0x00000010,
        LastFM      = 0x00000020,
        Spotify     = 0x00000040,
        Napster     = 0x00000080,
        Amazon      = 0x00000100,
        MusixMatch  = 0x00000200,
        Galiboo     = 0x00000400,
        Genius      = 0x00000800
    }

    enum QueryForMetadata
    {
        None = 0x0000,
        Metadata = 0x0001,
        Albums = 0x0002,
        Artwork = 0x0004
    }

    public class MusicMetadata
    {
        public Id3Tag ExistingTag { get; set; }
        public Id3Tag RetrievedTag { get; set; }

        ////fields saved in the MP3 file
        //public string fileName { get; set; }
        //public string Title { get; set; }
        //public string MetadataID3Artist { get; set; }
        //public string MetadataID3Album { get; set; }
        //public string Date { get; set; }
        //public string Genre { get; set; }
        //public PictureBox Cover { get; set; }

        ////fields used in the temporary retrieval of metadata
        //public string Title { get; set; }
        //public string Artist { get; set; }
        //public string Album { get; set; }
        //public string Date { get; set; }
        //public string Genre { get; set; }
        //public PictureBox Cover { get; set; }

        public MusicMetadata()
        { }
    }

    public class AlbumMetadata
    {
        public Id3Tag MusicMetatada { get; set; }
        public string Source { get; set; }

        public AlbumMetadata()
        { }
    }

    public class Id3Tag
    {
        public string Filepath { get; set; }
        //public string Service { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        
        public string Album { get; set; }
        public string Date { get; set; }
        public string Genre { get; set; }
        //public string DiscNumber { get; set; }
        //public string DiscCount { get; set; }
        //public string TrackNumber { get; set; }
        //public string TrackCount { get; set; }
        //public string Lyrics { get; set; }
        public PictureBox Cover { get; set; }
        ////public string Duration { get; set; }

    }

    public class Id3TagAndSource
    {
        public Id3Tag SongData { get; set; }
        public string Source { get; set; }

        public Id3TagAndSource()
        {

        }

    }
    public class BestMatch
    {
        public Id3Tag SongData { get; set; }
        public int Index { get; set; }

        public BestMatch()
        { }
    }
    public class BestMatchMinimal
    {
        public bool Include { get; set; }
        public string ExistingArtist { get; set; }
        public string ExistingTitle { get; set; }
        public string Filepath { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Date { get; set; }
        public string Genre { get; set; }
        public PictureBox Cover { get; set; }
        public int Index { get; set; }

        public BestMatchMinimal()
        {
            Include = false;
            Title = "";
            Artist = "";
            Album = "";
            Genre = "";
            Date = "";
            Cover = new PictureBox();
        }
    }
}
