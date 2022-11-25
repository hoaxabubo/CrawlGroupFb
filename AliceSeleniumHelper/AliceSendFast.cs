using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class AliceSendFast
    {
        public static AliceSeleniumHelperReturn ByJScript(ChromeDriver chrome, string XpathValue, string SendValue)
        {
            AliceSeleniumHelperReturn aliceSeleniumHelperReturn = new AliceSeleniumHelperReturn
            {
                Status = true,
                StatusText = ""
            };
            int TimeStart = Environment.TickCount;
            XpathValue = XpathValue.Replace("'", "\"");

            string Script = "var xpath = '" + XpathValue + "';matchingElement = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.value ='" + SendValue + "'; return 'Success';";
            try
            {
                string Re = chrome.ExecuteScript(Script).ToString();
                if (Re == "Success")
                {
                    aliceSeleniumHelperReturn.Status = true;
                    aliceSeleniumHelperReturn.StatusText = $"Send Success -> {XpathValue}";
                    return aliceSeleniumHelperReturn;
                }
            }
            catch
            {

            }
            aliceSeleniumHelperReturn.Status = false;
            aliceSeleniumHelperReturn.StatusText = $"Send False -> {XpathValue}";
            return aliceSeleniumHelperReturn;

        }

        public static AliceSeleniumHelperReturn BySelenium(ChromeDriver chrome, string xpathValue, string sendValue)
        {
            AliceSeleniumHelperReturn aliceSeleniumHelperReturn = new AliceSeleniumHelperReturn
            {
                Status = true,
                StatusText = ""
            };
            try
            {
                var element = chrome.FindElement(By.XPath(xpathValue));
                if (element != null)
                {
                    element.SendKeys(sendValue);
                    aliceSeleniumHelperReturn.Status = true;
                    aliceSeleniumHelperReturn.StatusText = $"Send Success -> {xpathValue} .";
                    Thread.Sleep(1000);
                    return aliceSeleniumHelperReturn;
                }
            }
            catch
            {

            }
            aliceSeleniumHelperReturn.Status = false;
            aliceSeleniumHelperReturn.StatusText = $"Send False -> {xpathValue} ";
            return aliceSeleniumHelperReturn;

        }

       
    }
}
