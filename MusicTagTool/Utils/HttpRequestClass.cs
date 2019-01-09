using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TotalTagger.Utils
{
    class HttpRequestClass
    {
        public async Task<bool> GetTags(string query, System.Threading.CancellationToken cancellationToken)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            string results = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();

            if (results != null)
            {
                var json = (JObject)JsonConvert.DeserializeObject(results);
                if (json != null)
                {

                }
            }

            return true;
        }
    }
}
