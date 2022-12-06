using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrawlGroupFb.BUS
{
    internal class CheckLive
    {
        public static bool CheckLiveCookie(string cookie)
        {
            try
            {
                #region Khai báo request

                HttpRequest request = new HttpRequest();
                request.KeepAlive = true;
                request.Cookies = new CookieStorage();

                request.AddHeader(HttpHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                request.AddHeader(HttpHeader.AcceptLanguage, "en-US,en;q=0.5");
                request.AddHeader("origin", "https://www.facebook.com");
                request.AddHeader("sec-fetch-dest", "empty");
                request.AddHeader("sec-fetch-mode", "cors");
                request.AddHeader("sec-fetch-site", "same-origin");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36";


                cookie = cookie.Replace(" ", "");
                var temp = cookie.Split(';');
                foreach (var item in temp)
                {
                    var temp2 = item.Split('=');
                    if (temp2.Count() > 1)
                    {
                        Cookie cookieTemp = new Cookie(temp2[0], temp2[1]) { Domain = ".facebook.com" };
                        request.Cookies.Add(cookieTemp);
                    }
                }
                #endregion
                try
                {
                    request.Referer = "https://m.facebook.com/";
                    string response = request.Get("https://m.facebook.com/profile").ToString();
                    if (request.Address.AbsoluteUri.Contains("https://m.facebook.com/checkpoint/"))
                    {
                        return false;
                    }

                    if (request.Address.AbsoluteUri.Contains("https://m.facebook.com/"))
                    {
                        if (response.Contains("checkpointSubmitButton") || response.Contains("checkpoint/dyi") || response.Contains("checkpointBottomBar"))
                        {
                            return false;
                        }

                        string fb_dtsg = Regex.Match(response.Replace("\\", ""), "name=\"fb_dtsg\" value=\"(.*?)\"").Groups[1].Value;

                        if (fb_dtsg == string.Empty)
                            fb_dtsg = Regex.Match(response, "\"name\":\"fb_dtsg\",\"value\":\"(.*?)\"").Groups[1].Value;

                        if (!response.Contains("checkpointSubmitButton") && !response.Contains("checkpointBottomBar") && !response.Contains("checkpoint/dyi") && !string.IsNullOrEmpty(fb_dtsg))
                        {
                            return true;
                        }
                    }

                }
                catch
                {

                }
            }
            catch
            {

            }
            return false;
        }
    }
}
