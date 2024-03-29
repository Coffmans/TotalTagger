﻿//using System;
//using System.Data;
//using System.Diagnostics;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;
//using GlobalVariables;
//using Newtonsoft.Json.Linq;
//using Utils;

//namespace MusicApiCollection
//{
//	/// <summary>
//	/// Class for Decibel API
//	/// </summary>
//	internal class Decibel : TotalTagger.MusicAPIs.QueryMusicService
//	{
//		/// <summary>
//		/// Gets ID3 data from Decibel API
//		/// <seealso href="https://developer.quantonemusic.com/authentication-v3"/>
//		/// <seealso href="https://developer.quantonemusic.com/rest-api-v3#classQueryAlbums"/>
//		/// <seealso href="https://developer.quantonemusic.com/object-documentation"/>
//		/// titleSearchType=PartialName has poorer results than without
//		/// </summary>
//		/// <param name="client">The HTTP client which is passed on to GetResponse method</param>
//		/// <param name="artist">The input artist to search for</param>
//		/// <param name="title">The input song title to search for</param>
//		/// <param name="cancelToken">The cancelation token which is passed on to GetResponse method</param>
//		/// <returns>
//		/// The ID3 tag object with the results from this API for:
//		/// 		Artist
//		/// 		Title
//		/// 		Album
//		/// 		Date
//		/// 		Genre
//		/// 		DiscNumber
//		/// 		DiscCount
//		/// 		TrackNumber
//		/// 		TrackCount
//		/// 		Cover URL
//		/// </returns>
//		//public async Task<Id3> GetTags(HttpMessageInvoker client, string artist, string title, CancellationToken cancelToken)
//		public async Task<bool> QueryForMetadata(System.Threading.CancellationToken cancellationToken)
//		{
//			Id3 o = new Id3 { Service = "Decibel (Quantone music)" };

//			Stopwatch sw = new Stopwatch();
//			sw.Start();

//			// ###########################################################################
//			foreach (var acc in User.Accounts["Decibel"])
//			{
//				if (acc["lastUsed"] == null)
//				{
//					acc["lastUsed"] = 0;
//				}
//			}

//			JToken account = (from acc in User.Accounts["Decibel"]
//							orderby acc["lastUsed"] ascending
//							select acc).FirstOrDefault();
//			account["lastUsed"] = DateTime.Now.Ticks;

//			string artistEncoded = WebUtility.UrlEncode(artist);
//			string titleEncoded = WebUtility.UrlEncode(title);

//			using (HttpRequestMessage searchRequest = new HttpRequestMessage())
//			{
//				searchRequest.RequestUri = new Uri("https://data.quantonemusic.com/v3/Recordings?artists=" + artistEncoded + "&title=" + titleEncoded + "&depth=genres&PageSize=1&PageNumber=1");
//				searchRequest.Headers.Add("DecibelAppID", (string)account["AppId"]);
//				searchRequest.Headers.Add("DecibelAppKey", (string)account["AppKey"]);

//				string searchContent = await Utils.GetHttpResponse(client, searchRequest, cancelToken);
//				JObject searchData = Utils.DeserializeJson(searchContent);

//				if (searchData?.SelectToken("Results") != null && searchData?.SelectToken("Results").ToString() != "[]")
//				{
//					o.Artist = (string)searchData.SelectToken("Results[0].MainArtistsLiteral");
//					o.Title = (string)searchData.SelectToken("Results[0].Title");
//					o.Album = (string)searchData.SelectToken("Results[0].OriginalAlbumTitle");
//					o.Date = (string)searchData.SelectToken("Results[0].OriginalReleaseDate");
//					o.DiscCount = null;
//					o.DiscNumber = null;

//					// ###########################################################################
//					using (HttpRequestMessage albumRequest = new HttpRequestMessage())
//					{
//						albumRequest.RequestUri = new Uri("https://data.quantonemusic.com/v3/Albums?artists=" + artistEncoded + "&recordings=" + titleEncoded + "&depth=Genres,Recordings&PageSize=1&PageNumber=1");
//						albumRequest.Headers.Add("DecibelAppID", (string)account["AppId"]);
//						albumRequest.Headers.Add("DecibelAppKey", (string)account["AppKey"]);

//						string albumContent = await Utils.GetHttpResponse(client, albumRequest, cancelToken);
//						JObject albumData = Utils.DeserializeJson(albumContent);

//						if (albumData?.SelectToken("Results") != null && albumData?.SelectToken("Results").ToString() != "[]")
//						{
//							o.Genre = (string)albumData.SelectToken("Results[0].Genres[0].Name");
//							o.TrackCount = (string)albumData.SelectToken("Results[0].Recordings[-1:].AlbumSequence");
//							o.Cover = "https://data.quantonemusic.com/v3/Images/" + (string)albumData.SelectToken("Results[0].ImageId"); // don't include in settings.json CoverOrder

//							foreach (JToken recording in albumData.SelectTokens("Results[0].Recordings[*]"))
//							{
//								if (((string)recording.SelectToken("Title")).ToLowerInvariant() == o.Title.ToLowerInvariant())
//								{
//									o.TrackNumber = (string)recording["AlbumSequence"];
//									break;
//								}
//							}
//						}
//					}
//				}
//			}

//			// ###########################################################################
//			sw.Stop();
//			o.Duration = $"{sw.Elapsed:s\\,f}";

//			return o;
//		}
//	}
//}
