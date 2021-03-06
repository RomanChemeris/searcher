﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Searcher.Core.Models;
using Searcher.Core.Structures;

namespace Searcher.Core.SearchEngins
{
    public class YandexSearchEngine : ISearchEngine
    {
        public async Task<SearchEngineResult> DoSearch(string textToSearch)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            ServicePointManager.Expect100Continue = false;

            string command =
                $@"<?xml version=""1.0"" encoding=""UTF-8""?>   
            <request>   
                <query>{HttpUtility.HtmlEncode(textToSearch)}</query>
                <groupings>
                <groupby attr=""d"" 
                    mode=""deep"" 
                    groups-on-page=""11"" 
                    docs-in-group=""1"" />   
                </groupings>   
            </request>";

            byte[] bytes = Encoding.UTF8.GetBytes(command);

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(@"https://yandex.com/search/xml?l10n=ru&user=" + ConfigurationManager.AppSettings["YandexUser"] + "&key=" + ConfigurationManager.AppSettings["YandexKey"]);
            request.Method = "POST";
            request.ContentLength = bytes.Length;
            request.ContentType = "text/xml";

            using (Stream requestStream = await request.GetRequestStreamAsync())
            {
                await requestStream.WriteAsync(bytes, 0, bytes.Length);
            }

            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            var responseStream = response.GetResponseStream();
            if (responseStream == null)
            {
                return new SearchEngineResult
                {
                    EngineName = "Yandex - Error"
                };
            }
            XmlReader xmlReader = XmlReader.Create(responseStream);
            XDocument xmlResponse = XDocument.Load(xmlReader);
            stopwatch.Stop();

            return new SearchEngineResult
            {
                EngineName = "Yandex",
                SearchItems = xmlResponse.Elements().Elements("response").Elements("results").Elements("grouping")
                    .Elements("group").Elements("doc").Select(x => new SearchItem
                    {
                        Link = x.Element("url")?.Value,
                        LinkText = x.Element("title")?.Value
                    }).ToList(),
                Ticks = stopwatch.ElapsedMilliseconds
            };
        }
    }
}
