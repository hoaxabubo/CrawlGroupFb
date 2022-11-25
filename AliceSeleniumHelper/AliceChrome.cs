using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class AliceChrome
    {
 
        public static bool GotoUrl(ChromeDriver chrome, string link, int count = 10)
        {
            for (int i = 0; i < count; i++)
            {
                try
                {
                    chrome.Navigate().GoToUrl(link);
                    return true;
                }
                catch
                {
                   

                }
            }
            return false;
        }
       
        public static void QuitChrome(ChromeDriver chrome)
        {
            try
            {
                var tabs = chrome.WindowHandles;
                foreach (var tab in tabs)
                {
                    chrome.SwitchTo().Window(tab);
                    chrome.Close();
                }
            }
            catch
            {

            }
            try
            {
                chrome.Quit();
            }
            catch
            {

            }
        }

        public static bool isChromeRunning(ChromeDriver chrome)
        {
            try
            {
                var a = chrome.Title;
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
