using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Interactions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    namespace AliceSeleniumHelper
    {
        public class AliceClickFast
        {
            public static AliceSeleniumHelperReturn ByJScript(ChromeDriver chrome, string XpathValue)
            {
                AliceSeleniumHelperReturn aliceSeleniumHelperReturn = new AliceSeleniumHelperReturn
                {
                    Status = true,
                    StatusText = ""
                };
                string Script = "var xpath = '" + XpathValue + "';matchingElement = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.click(); return 'Success';";
                try
                {
                    string Re = chrome.ExecuteScript(Script).ToString();
                    if (Re == "Success")
                    {
                        aliceSeleniumHelperReturn.Status = true;
                        aliceSeleniumHelperReturn.StatusText = $"Click Success -> {XpathValue}";
                        return aliceSeleniumHelperReturn;
                    }
                }
                catch
                {

                }
                aliceSeleniumHelperReturn.Status = false;
                aliceSeleniumHelperReturn.StatusText = $"Click False -> {XpathValue}";
                return aliceSeleniumHelperReturn;
            }

            public static AliceSeleniumHelperReturn BySelenium(ChromeDriver chrome, string XpathValue)
            {
                AliceSeleniumHelperReturn aliceSeleniumHelperReturn = new AliceSeleniumHelperReturn
                {
                    Status = true,
                    StatusText = ""
                };
                try
                {
                    var element = chrome.FindElement(By.XPath(XpathValue));
                    if (element != null)
                    {
                        element.Click();
                        aliceSeleniumHelperReturn.Status = true;
                        aliceSeleniumHelperReturn.StatusText = $"Click Success -> {XpathValue}";
                        return aliceSeleniumHelperReturn;
                    }
                }
                catch
                {
                   
                }
                aliceSeleniumHelperReturn.Status = false;
                aliceSeleniumHelperReturn.StatusText = $"Click False -> {XpathValue}";
                return aliceSeleniumHelperReturn;

            }

        }
    }

}
