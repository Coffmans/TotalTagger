using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TotalTagger
{
    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<TheAudioDBData> GetAudioData(string query)
        {
            TheAudioDBData audioData = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    audioData = JsonConvert.DeserializeObject<TheAudioDBData>(content);
                }
                else
                {
                    //logger.LogErrorMessage("Error calling OpenWeather - " +response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
                //logger.LogErrorMessage(ex.ToString());
            }

            return audioData;
        }

        public async Task<LastFMSongData> GetLastFMSongData(string query)
        {
            LastFMSongData audioData = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    audioData = JsonConvert.DeserializeObject<LastFMSongData>(content);
                }
                else
                {
                    //logger.LogErrorMessage("Error calling OpenWeather - " +response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
                //logger.LogErrorMessage(ex.ToString());
            }

            return audioData;
        }

        //public async Task<WeatherBitData> GetWeatherBitData(string query)
        //{
        //    WeatherBitData weatherData = null;
        //    try
        //    {
        //        var response = await _client.GetAsync(query);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            weatherData = JsonConvert.DeserializeObject<WeatherBitData>(content);
        //        }
        //        else
        //        {
        //            //logger.LogErrorMessage("Error calling WeatherBit - " + response.ReasonPhrase);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("\t\tERROR {0}", ex.Message);
        //        logger.LogErrorMessage(ex.ToString());
        //    }

        //    return weatherData;
        //}

        //public async Task<WeatherUnlockedData> GetWeatherUnlockedData(string query)
        //{
        //    WeatherUnlockedData weatherData = null;
        //    try
        //    {
        //        var response = await _client.GetAsync(query);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            weatherData = JsonConvert.DeserializeObject<WeatherUnlockedData>(content);
        //        }
        //        else
        //        {
        //            logger.LogErrorMessage("Error calling Weather Unlocked - " + response.ReasonPhrase);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogErrorMessage(ex.ToString());
        //        Debug.WriteLine("\t\tERROR {0}", ex.Message);
        //    }

        //    return weatherData;
        //}
    }
}
