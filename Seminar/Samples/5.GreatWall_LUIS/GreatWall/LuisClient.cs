using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

using GreatWall.Entities;

namespace GreatWall
{
    public class LuisClient
    {
        public static async Task<GreatWallLUIS> ParseUserInput(string strInput)
        {
            const string APPLICATION_ID = "<application_id>";
            const string SUBSCRIPTION_KEY = "<subscription_key>";

            string strRet = string.Empty;
            string strEscaped = Uri.EscapeDataString(strInput);

            using (var client = new HttpClient())
            {
                string uri = string.Format("https://api.projectoxford.ai/luis/v1/application?id={0}&subscription-key={1}&q={2}", APPLICATION_ID, SUBSCRIPTION_KEY, strEscaped);
                HttpResponseMessage msg = await client.GetAsync(uri);

                if (msg.IsSuccessStatusCode)
                {
                    var jsonResponse = await msg.Content.ReadAsStringAsync();
                    var _Data = JsonConvert.DeserializeObject<GreatWallLUIS>(jsonResponse);
                    return _Data;
                }
            }
            return null;
        }
    } 
}