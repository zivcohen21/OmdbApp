﻿using System;
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
        private static string yearKey = "&y=";
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


        public Results search(Search search)
        {
            string title = search.title;
            string year = search.year;
            Results results = new Results();
            List<MediaItem> mediaItems = new List<MediaItem>();
            string sQueryUrl = "";
            if(year != null)
            {
                sQueryUrl = dataApi + searchKey + title + yearKey + year + apiKey;
            }
            else
            {
                sQueryUrl = dataApi + searchKey + title + apiKey;
            } 
            SQuery sQuery = (SQuery)getResObject(sQueryUrl, typeof(SQuery));
            if (sQuery.Response == true)
            {
                int numOfPages = 0;
                if (sQuery.totalResults != null && sQuery.totalResults != "N/A")
                {
                    numOfPages = (Int32.Parse(sQuery.totalResults) / 10) + 1;
                }
                
                List<SQueryInner> items = sQuery.Search;
                if (items != null)
                {
                    getAllDataOfItemPerPage(items, mediaItems);
    
                    for (int i = 2; i <= numOfPages; i++)
                    {
                        sQueryUrl = dataApi + searchKey + title + pageKey + i + apiKey;
                        sQuery = (SQuery)getResObject(sQueryUrl, typeof(SQuery));
                        items = sQuery.Search;
                        getAllDataOfItemPerPage(items, mediaItems);
                    }
                }
            }
            else
            {
                results.message = sQuery.Error;
            }
            results.MediaItems = mediaItems;
            
            return results;
        }

        private void getAllDataOfItemPerPage(List<SQueryInner> items, List<MediaItem> mediaItems)
        {        
            for (int i = 0; i < items.Count; i++)
            {
                MediaItem mediaItem = new MediaItem();
                TPlotQuery tplotQuery = new TPlotQuery();
                

                string imdbID = items[i].imdbID;
                string titleSearch = items[i].Title;
                string tPlotQueryUrl = dataApi + idKey + imdbID + fullPlotKey + apiKey;
                tplotQuery = (TPlotQuery)getResObject(tPlotQueryUrl, typeof(TPlotQuery));

                if(tplotQuery.Type == "movie" || tplotQuery.Type == "series")
                {
                    mediaItem.Title = tplotQuery.Title;
                    mediaItem.Released = tplotQuery.Released;
                    mediaItem.Type = tplotQuery.Type;
                    mediaItem.Genre = tplotQuery.Genre;
                    mediaItem.Poster = tplotQuery.Poster;
                    mediaItem.Plot = tplotQuery.Plot;

                    if(tplotQuery.Type == "series")
                    {
                        int numOfSeasons = 0;
                        if (tplotQuery.totalSeasons != null && tplotQuery.totalSeasons != "N/A")
                        {
                            numOfSeasons = Int32.Parse(tplotQuery.totalSeasons);
                        }                            
                        List<TSeasonQuery> seasons = new List<TSeasonQuery>();
                        for (int seasonIndex = 1; seasonIndex <= numOfSeasons; seasonIndex++)
                        {
                            TSeasonQuery tSeasonQuery = new TSeasonQuery();
                            string tSeasonQueryUrl = dataApi + titleKey + titleSearch + fullPlotKey + seasonKey + seasonIndex + apiKey;                            
                            tSeasonQuery = (TSeasonQuery)getResObject(tSeasonQueryUrl, typeof(TSeasonQuery));
                            //season.Title = tSeasonQuery.Title;
                            //season.SeasonNumber = tSeasonQuery.Season;
                            //season.Episodes = tSeasonQuery.Episodes;
                            seasons.Add(tSeasonQuery);
                        }
                        mediaItem.Seasons = seasons;
                    }
                   

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

        //    string[] urls = getUrls(title);

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

        //public string[] getUrls(string title)
        //{
        //    string[] urls = 
        //    {
        //        dataApi + "t=" + title + "&plot=full" + apiKey,
        //        dataApi + "t=" + title + "&plot=full" + "&Season=1" + apiKey,
        //        dataApi + "s=" + title + "&Season=1" + apiKey
        //    };           

        //    return urls;
        //}
    }
}
