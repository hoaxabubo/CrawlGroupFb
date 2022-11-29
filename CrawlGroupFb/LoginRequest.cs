﻿using CrawlGroupFb.Models;
using Leaf.xNet;
using OtpNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using HttpRequest = Leaf.xNet.HttpRequest;

namespace CrawlGroupFb
{
    internal class LoginRequest
    {
        public static bool CheckLiveCookie(string cookie, string keyWord)
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
                            string html = request.Get($"https://mbasic.facebook.com/search/groups/?q={keyWord}&source=filter&isTrending=0&paipv=0").ToString();
                            MatchCollection dataFulls = Regex.Matches(html, "ch\"><span>(.*?);is_inline");
                            List<string> list = new List<string>();
                            foreach (var dataFull in dataFulls)
                            {
                                string status = Regex.Match(dataFull.ToString(), "<span>Nhóm (.*?)</span>").Groups[1].Value;


                                if (status == "Riêng tư")
                                {
                                    continue;

                                }
                                string matches = Regex.Match(dataFull.ToString(), "group_id=(.*?)&amp").Groups[1].Value;
                                list.Add(matches);
                            }
                            
                            {
                                //click Next

                                for (int i = 2; i < 20; i++)
                                {
                                    string cursor = Regex.Match(html, "cursor=(.*?)&").Groups[1].Value;
                                    string html2 = request.Get($"https://mbasic.facebook.com/graphsearch/str/hot/keywords_groups?f=AbrryWQ4ejlO5q5BaPM3xlyoxfsJiKuMsIqaftD7HTQmmxFFaSHGsUDkThCB5VxdK_IvP9Un-nDligk0RLvmBfhOMq3cbhX0uFLfjdAaJexpBvGYS1qh62rDTYzqPvK39hc&source=pivot&__xts__%5B0%5D=12.AboiCcaBQ0Hwc8FNfxNqAtJ3iIEAA70hUPt2CBYCNyrSPT2SfAofyuXwyU-2far_uUbzrezd9PsOmOJme00HSjoskA7g39zC4GQHevchYfhrNDX1Ly3OfnLMrY3TqdsOsq4cV6CIcXT2dSI_xbwDY5YvmPbDjGj28WrYL4Y9xJ_4XAxzFxmv3mBuA_Jq-kEa7YzWF3V8NIbalDkYuBNfJpc_Qw0SCW2ETa0UuyGHwxU56fxN6YPWdvmY2MvnBCTkyhZkOYMH0uiO9QKSCFC59NLVUVXMPkYRJLYRnVkyg4w-DPeE0h-b6m-7AXJOj0PvBTt1Eg5tfRcScIk5XNGmtrB9KPg6S53B1A0lohTYy4kGOGTO7Bv7T_i-l7Q0KbE3aaBxT0cMMk2QzQXKIWrNMODO5hzIwKC1g5JzF95uJ0zeig&paipv=0&eav=AfaS6n8bNNWEjKxus5CpQryRdu-v6NT4RHXve0IOHv7HtjqQAzv5hsYdRi_W7l2UO74&cursor={cursor}&pn={i}&usid=fb39c2532ae3fc5d4d4e4eb43fa21a82&tsid").ToString();
                                    MatchCollection dataFulls2 = Regex.Matches(html, "ch\"><span>(.*?);is_inline");
                                   
                                    foreach (var dataFull2 in dataFulls2)
                                    {
                                        string status2 = Regex.Match(dataFull2.ToString(), "<span>Nhóm (.*?)</span>").Groups[1].Value;


                                        if (status2 == "Riêng tư")
                                        {
                                            continue;

                                        }
                                        string matches2 = Regex.Match(dataFull2.ToString(), "group_id=(.*?)&amp").Groups[1].Value;
                                        list.Add(matches2);
                                    }

                                    string endPage = Regex.Match(html2, "Cuối kết quả tìm kiếm").Value;
                                    if (!string.IsNullOrEmpty(endPage))
                                    {
                                        break;
                                    }
                                }



                            }
                            File.AppendAllLines("idGroup.txt", list);



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