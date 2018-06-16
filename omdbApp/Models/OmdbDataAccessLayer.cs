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
        private static string searchKey = "s=";
        private static string titleKey = "t=";
        private static string idKey = "i=";
        private static string pageKey = "&page=";
        private static string fullPlotKey = "&plot=full";
        private static string seasonKey = "&Season=";
        private static string TypeKey = "&type=";
        WebClient client = new WebClient();

        //string Title 1 2 3 
        //string Year 1 3
        //string Type 1 3
        //string Poster 1 3
        //string Plot 1
        //List<Episode> Episodes 2


        // t plot: title year type poster plot (1) 
        //t plot season:  title Episodes (1)
        //s page: 10 each page: title year type poster


        public List<MediaItem> search(Search search)
        {
            string term = search.search;

            List<MediaItem> mediaItems = new List<MediaItem>();
            string sQueryUrl = dataApi + searchKey + term + apiKey;
            SQuery sQuery = (SQuery)getResObject(sQueryUrl, typeof(SQuery));

            int numOfPages = (sQuery.totalResults / 10) + 1;
            List<SQueryInner> items = sQuery.Search;
            if (items != null)
            {
                getAllDataOfItemPerPage(items, mediaItems);

                //for(int i = 2; i <= numOfPages; i++)
                //{
                //    sQueryUrl = dataApi + searchKey + term + pageKey + i + apiKey;
                //    sQuery = (SQuery)getResObject(sQueryUrl, typeof(SQuery));
                //    items = sQuery.Search;
                //    getAllDataOfItemPerPage(items, mediaItems);
                //}
            }
            return mediaItems;
        }

        private void getAllDataOfItemPerPage(List<SQueryInner> items, List<MediaItem> mediaItems)
        {        
            for (int i = 0; i < items.Count; i++)
            {
                MediaItem mediaItem = new MediaItem();
                TPlotQuery tplotQuery = new TPlotQuery();
                TSeasonQuery tSeasonQuery = new TSeasonQuery();

                string imdbID = items[i].imdbID;
                string titleSearch = items[i].Title;
                string tPlotQueryUrl = dataApi + idKey + imdbID + fullPlotKey + apiKey;
                tplotQuery = (TPlotQuery)getResObject(tPlotQueryUrl, typeof(TPlotQuery));

                if(tplotQuery.Type == "movie" || tplotQuery.Type == "series")
                {
                    mediaItem.Title = tplotQuery.Title;
                    mediaItem.Released = tplotQuery.Released;
                    mediaItem.Type = tplotQuery.Type;
                    mediaItem.Poster = tplotQuery.Poster;
                    mediaItem.Plot = tplotQuery.Plot;

                    string tSeasonQueryUrl = dataApi + titleKey + titleSearch + fullPlotKey + seasonKey + "1" + apiKey;
                    tSeasonQuery = (TSeasonQuery)getResObject(tSeasonQueryUrl, typeof(TSeasonQuery));
                    mediaItem.Episodes = tSeasonQuery.Episodes;

                    mediaItems.Add(mediaItem);
                }
               
            }
        }

        private Object getResObject(string sQueryUrl, Type type)
        {
            string responseFromServer = client.DownloadString(sQueryUrl);
            if (type == typeof(SQuery))
            {
                return JsonConvert.DeserializeObject<SQuery>(responseFromServer);
            }
            if (type == typeof(TPlotQuery))
            {
                return JsonConvert.DeserializeObject<TPlotQuery>(responseFromServer);
            }
            if (type == typeof(TSeasonQuery))
            {
                return JsonConvert.DeserializeObject<TSeasonQuery>(responseFromServer);
            }
            else
            {
                return null;
            }    
        }

        //    string[] urls = getUrls(term);

        //    string[] responseByString = new string[urls.Length];
        //    SQuery[] responseData = new SQuery[urls.Length];

        //    for (int i = 0; i < urls.Length; i++)
        //    {
        //        responseByString[i] = getResponseByString(urls[i]);
        //        responseData[i] = getResponseByObject(responseByString[i]);
        //    }

        //    List<MediaItem> mediaItems = responseData[3].Search;
        //    if (mediaItems != null)
        //    {
        //        int size = mediaItems.Count;

        //        for (int i = 0; i < size; i++)
        //        {
        //            String title = mediaItems[i].Title;
        //            getUrls(title);
        //        }
        //    }

        //}

        //public string getResponseByString(string url)
        //{
        //    string responseFromServer = client.DownloadString(url);
        //    Console.WriteLine(responseFromServer);            
        //    return responseFromServer;
        //}

        //public SQuery getResponseByObject(string response)
        //{
        //    SQuery responseData = JsonConvert.DeserializeObject<SQuery>(response);
        //    return responseData;
        //}

        //public string[] getUrls(string term)
        //{
        //    string[] urls = 
        //    {
        //        dataApi + "t=" + term + "&plot=full" + apiKey,
        //        dataApi + "t=" + term + "&plot=full" + "&Season=1" + apiKey,
        //        dataApi + "s=" + term + "&Season=1" + apiKey
        //    };           

        //    return urls;
        //}
    }
}
