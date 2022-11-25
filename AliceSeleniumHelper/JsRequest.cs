using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class JsRequest
    {

        public static string Get(ChromeDriver chrome, string link)
        {
            try
            {
                string dataGet = "dmFyIHJlcXVlc3RPcHRpb25zID0gewogIG1ldGhvZDogJ0dFVCcsCiAgcmVkaXJlY3Q6ICdmb2xsb3cnCn07Cgp2YXIgcmUgPSBmZXRjaCgieHh4bGluayIsIHJlcXVlc3RPcHRpb25zKQogIC50aGVuKHJlc3BvbnNlID0+IHJlc3BvbnNlLnRleHQoKSkKICAudGhlbihyZXN1bHQgPT4ge3JldHVybiByZXN1bHR9KTsKcmV0dXJuIHJlOw==";
                dataGet = Base64Decode(dataGet);
                dataGet = dataGet.Replace("xxxlink", link);
                string getHtml = chrome.ExecuteScript(dataGet).ToString();
                return getHtml;
            }
            catch
            {
                return null;
            }
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


    }
}
