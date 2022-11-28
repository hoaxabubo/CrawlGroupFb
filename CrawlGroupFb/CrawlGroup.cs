using CrawlGroupFb.Models;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AliceSeleniumHelper;
using System.Threading;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace CrawlGroupFb
{
    internal class CrawlGroup
    {
        public static AuraeResult CrawlGroupData(ChromeDriver chrome,string keyWord)
        {
            {
                //Send Keyword
                string xAttribute = "//input[@name='query' and @autocomplete]";
                string sendValue = keyWord;
                var re = AliceSendWait.BySelenium(chrome, xAttribute, sendValue);
                if (!re.Status)
                {
                    return new AuraeResult();
                }
            }
            {
                //Click Search
                string xAttribute = "//input[@type='submit' and @value='Tìm kiếm']";
                var re = AliceClickWait.BySelenium(chrome, xAttribute);
                if (!re.Status)
                {
                    return new AuraeResult();
                }
            }
            Thread.Sleep(1000);
            {
                //Click xem them
                string xAttribute = "//a[@role='button']";
                var re = AliceClickWait.BySelenium(chrome, xAttribute);
                if (!re.Status)
                {
                    return new AuraeResult();
                }
            }
            Thread.Sleep(1000);
            {
                //Click xem Group
                string xAttribute = "//a[contains(@href,'groups')]";
                var re = AliceClickWait.BySelenium(chrome, xAttribute);
                if (!re.Status)
                {
                    return new AuraeResult();
                }
            }
            List<string> LinkPorts = new List<string>();
            List<string> linkGroup = new List<string>();
            while(linkGroup.Count <= 100)
            {
                var elements = chrome.FindElements(By.XPath("//table[@role='presentation' and @align]"));
                try
                {
                    foreach (var element in elements)
                    {
                        string e = element.Text;
                        var re = Regex.Match(e, "Công khai").Value;
                        if (re == "")
                        {
                            continue;
                        }
                        var innerHtml = element.GetAttribute("innerHTML");
                        string id = Regex.Match(innerHtml, "group_id=(.*?)&amp").Groups[1].Value;
                        LinkPorts.Add($"{id}");

                    }
                    {
                        //Click xem Group
                        string xAttribute = "//a[contains(@href,'https://mbasic.facebook.com/search/groups')]";
                        var re1 = AliceClickWait.BySelenium(chrome, xAttribute,5);
                        if (!re1.Status)
                        {
                            linkGroup = LinkPorts.Distinct().ToList();
                            File.AppendAllLines("idGroup.txt", linkGroup);
                            chrome.FindElement(By.XPath("//input[@name='query' and @autocomplete]")).Clear();
                            return new AuraeResult();
                        }
                    }
                }
                catch
                {


                }
            }
            
            return null;
        }
    }
}
