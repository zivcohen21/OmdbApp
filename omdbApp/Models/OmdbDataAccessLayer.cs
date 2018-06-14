using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace omdbApp.Models
{
    public class OmdbDataAccessLayer
    {
        private static string apiKey = "&apikey=31c2c97d";
        private static string dataApi = "http://www.omdbapi.com/?";
        public string search(Search value)
        {
            string url = dataApi + "t=" + value.search + "&plot=full" + "&page=100" + apiKey;

            WebClient client = new WebClient();
            string responseFromServer = client.DownloadString(url);
            //var json_data = client.DownloadString(url);
            ResponseData m = JsonConvert.DeserializeObject<ResponseData>(responseFromServer);
            Console.WriteLine(responseFromServer);
     
            return responseFromServer;
        }
    }
}
