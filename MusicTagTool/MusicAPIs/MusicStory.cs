//using RestSharp;
//using RestSharp.Authenticators;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace TotalTagger
//{
//    class MusicStory : TotalTagger.MusicAPIs.TagService
//    {
//        public void Can_Authenticate_With_OAuth()
//        {
//            string consumerKey = Properties.Settings.Default.MusicStoryConsumerKey;
//            string consumerSecret = Properties.Settings.Default.MusicStoryConsumerSecret;

//            var baseUrl = new Uri("http://api.music-story.com");
//            var client = new RestClient(baseUrl)
//            {
//                Authenticator = OAuth1Authenticator.ForRequestToken(consumerKey, consumerSecret)
//            };
//            var request = new RestRequest("oauth/request_token");
//            var response = client.Execute(request);

//            var qs = HttpUtility.ParseQueryString(response.Content);
//            var oauthToken = qs["oauth_token"];
//            var oauthTokenSecret = qs["oauth_token_secret"];
//            var applicationName = qs["application_name"];

//            var requestTokenRequest = new RestRequest("requestToken");
//            var requestTokenResponse = client.Execute(requestTokenRequest);

//            var requestTokenResponseParameters = HttpUtility.ParseQueryString(requestTokenResponse.Content);
//            var requestToken = requestTokenResponseParameters["oauth_token"];
//            var requestSecret = requestTokenResponseParameters["oauth_token_secret"];

//            requestTokenRequest = new RestRequest("authenticate?oauth_token=" + requestToken);

//            var redirectUri = client.BuildUri(requestTokenRequest);

//            Process.Start(redirectUri.ToString());

//            var requestTokenQueryParameters = HttpUtility.ParseQueryString(new Uri(redirectUri.ToString()).Query);
//            var requestVerifier = requestTokenQueryParameters["oauth_verifier"];

//            client.Authenticator = OAuth1Authenticator.ForAccessToken(
//                consumerKey, consumerSecret, requestToken, requestSecret, requestVerifier);

//            var requestAccessTokenRequest = new RestRequest("accessToken");
//            var requestActionTokenResponse = client.Execute(requestAccessTokenRequest);

//            var requestActionTokenResponseParameters = HttpUtility.ParseQueryString(requestActionTokenResponse.Content);
//            var accessToken = requestActionTokenResponseParameters["oauth_token"];
//            var accessSecret = requestActionTokenResponseParameters["oauth_token_secret"];

//        }

//        public bool QueryForMetadata()
//        {
//            return false;
//        }

//    }
//}
