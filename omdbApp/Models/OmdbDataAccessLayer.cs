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
        WebClient client = new WebClient();

        //string Title 1
        //string Year 1
        //string Type 1
        //string Poster 1
        //string Plot 1
        //List<Episode> Episodes 2

        public void search(Search search)
        {           
            string term = search.search;
            string[] urls = getUrls(term);

            string[] responseByString = new string[urls.Length];
            ResponseData[] responseData = new ResponseData[urls.Length];

            for (int i = 0; i < urls.Length; i++)
            {
                responseByString[i] = getResponseByString(urls[i]);
                responseData[i] = getResponseByObject(responseByString[i]);
            }

            List<MediaItem> mediaItems = responseData[3].Search;
            if (mediaItems != null)
            {
                int size = mediaItems.Count;

                for (int i = 0; i < size; i++)
                {
                    String title = mediaItems[i].Title;
                    getUrls(title);
                }
            }
            
        }

        public string getResponseByString(string url)
        {
            string responseFromServer = client.DownloadString(url);
            Console.WriteLine(responseFromServer);            
            return responseFromServer;
        }

        public ResponseData getResponseByObject(string response)
        {
            ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(response);
            return responseData;
        }

        public string[] getUrls(string term)
        {
            string[] urls = 
            {
                dataApi + "t=" + term + "&plot=full" + apiKey,
                dataApi + "t=" + term + "&plot=full" + "&Season=1" + apiKey,
                dataApi + "s=" + term + "&Season=1" + apiKey
            };           

            return urls;
        }
    }
}
