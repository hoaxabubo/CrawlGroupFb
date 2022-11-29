using CrawlGroupFb.Models;
using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGroupFb
{
    internal class RequestIdGroup
    {
        public static RequestResult CrawlId(string keyWord)
        {
            HttpRequest httpRequest = new HttpRequest();
            string html = httpRequest.Get($"https://mbasic.facebook.com/search/groups/?q={keyWord}&source=filter&isTrending=0&paipv=0").ToString();
            return null;
        }
    }
}
