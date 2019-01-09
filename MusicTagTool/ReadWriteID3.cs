using System.IO;
using System.Linq;
using System.Windows.Forms;
using System;
using TagLib;
using System.Drawing.Imaging;
using System.Globalization;

namespace TotalTagger
{
    class ReadWriteID3
    {
        public static Id3Tag ReadID3Tags(string filename)
        {
            Id3Tag _Id3Tag = new Id3Tag
            {
                Filepath = /*Path.GetFileName(*/filename/*)*/,

                Cover = new PictureBox(),

                Title = "",
                Artist = "",
                Album = ""
            };

            var file = TagLib.File.Create(filename);
            if (file != null)
            {
                var artist = "";
                if (file.Tag.FirstAlbumArtist != null && file.Tag.FirstAlbumArtist.Length > 0)
                {
                    artist = file.Tag.FirstAlbumArtist;
                }
                else if (file.Tag.FirstPerformer != null && file.Tag.FirstPerformer.Length > 0)
                {
                    artist = file.Tag.FirstPerformer;
                }
                else if (file.Tag.JoinedPerformers != null && file.Tag.JoinedPerformers.Any())
                {
                    artist = file.Tag.JoinedPerformers[0].ToString();
                }
                _Id3Tag.Title = file.Tag.Title;
                _Id3Tag.Artist = artist;
                _Id3Tag.Album = file.Tag.Album;
                _Id3Tag.Date = file.Tag.Year.ToString();
                _Id3Tag.Genre = file.Tag.FirstGenre;
                if (file.Tag.Pictures.Any())
                {
                    MemoryStream ms = new MemoryStream(file.Tag.Pictures[0].Data.Data);
                    System.Drawing.Image albumArt;
                    albumArt = System.Drawing.Image.FromStream(ms);
                    _Id3Tag.Cover.Image = albumArt.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                }
                else
                {
                    _Id3Tag.Cover.Image = null;
                }
            }

            return _Id3Tag;
        }

        public static string WriteID3Tags(string filename, Id3Tag NewId3Tags)
        {
            try
            {
                var file = TagLib.File.Create(filename);
                if (file != null)
                {
                    if( !String.IsNullOrEmpty(NewId3Tags.Title))
                    {
                        file.Tag.Title = NewId3Tags.Title;
                    }
                    if (!String.IsNullOrEmpty(NewId3Tags.Album))
                    {
                        file.Tag.Album = NewId3Tags.Album;
                    }

                    string delimStr = ",/";
                    if (!String.IsNullOrEmpty(NewId3Tags.Artist))
                    {                    
                        char[] delimiter = delimStr.ToCharArray();
                        string[] artists = null;
                        artists = NewId3Tags.Artist.Split(delimiter);
                        file.Tag.AlbumArtists = artists;
                    }

                    if (!String.IsNullOrEmpty(NewId3Tags.Genre) )
                    {
                        delimStr = " ,";
                        char[] delimiter = delimStr.ToCharArray();
                        string[] genres = null;
                        genres = NewId3Tags.Genre.Split(delimiter);
                        file.Tag.Genres = genres;
                    }

                    try
                    {
                        if (NewId3Tags.Cover != null && !String.IsNullOrEmpty(NewId3Tags.Cover.ImageLocation))
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                TagLib.Picture pic = new TagLib.Picture
                                {
                                    Type = TagLib.PictureType.FrontCover,
                                    MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg,
                                    Description = "Cover"
                                };

                                NewId3Tags.Cover.Image.Save(memoryStream, ImageFormat.Jpeg);
                                memoryStream.Position = 0;
                                pic.Data = TagLib.ByteVector.FromStream(memoryStream);
                                file.Tag.Pictures = new TagLib.IPicture[1] { pic };

                            }
                        }
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        if (!String.IsNullOrEmpty(NewId3Tags.Date))
                        {
                            if (NewId3Tags.Date.Length > 4)
                            {
                                file.Tag.Year = (uint)DateTime.Parse(NewId3Tags.Date, new CultureInfo("en-US")).Year;
                            }
                            else if (NewId3Tags.Date.Length == 4)
                            {
                                file.Tag.Year = Convert.ToUInt32(NewId3Tags.Date);
                            }

                        }
                    }
                    catch (Exception)
                    {
                    }

                    //if (!String.IsNullOrEmpty(NewId3Tags.TrackCount))
                    //    file.Tag.TrackCount = Convert.ToUInt32(NewId3Tags.TrackCount);         

                    file.Save();
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Unable to Write Tags to " + filename;
        }

        public static string RemoveID3Tags(string filename)
        {
            try
            {
                var file = TagLib.File.Create(filename);
                if (file != null)
                {
                    file.RemoveTags(TagTypes.AllTags);

                    file.Save();
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Unable to Remove Tags From File!";
        }

    }
}
