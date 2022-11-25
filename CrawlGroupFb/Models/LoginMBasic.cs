using AliceSeleniumHelper.AliceSeleniumHelper;
using AliceSeleniumHelper;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrawlGroupFb.Models;
using System.Threading;
using System.IO;
using OpenQA.Selenium;

using Aurae_Facebook_Care.Common;

namespace Aurae_Facebook_Care.BUS.Selenium
{

    internal class LoginMBasic
    {
        public static AuraeResult Login(ChromeDriver chrome, Account account)
        {
            try
            {
                AliceChrome.GotoUrl(chrome, "https://mbasic.facebook.com");
                AuraeResult checkLogin ;
                {   // send vao username
                    string usernameSend = account.U;
                    if (!string.IsNullOrEmpty(account.Email))
                    {
                        usernameSend = account.Email;
                    }

                    string xpath = "//*[@name='email']";
                    string sendValue = usernameSend;
                    var flag = AliceSendWait.BySelenium(chrome, xpath, sendValue);
                    if (!flag.Status)
                    {
                        return new AuraeResult(false, "Send vào thất bại -> " + xpath);
                    }
                }

                {   // send vao password
                    string xpath = "//*[@name='pass']";
                    string sendValue = account.P;
                    var flag = AliceSendWait.BySelenium(chrome, xpath, sendValue);
                    if (!flag.Status)
                    {
                        return new AuraeResult(false, "Send vào thất bại -> " + xpath);
                    }
                }

                {   // click vao nut login
                    string xpath = "//*[@name='login']";
                    var flag = AliceClickWait.BySelenium(chrome, xpath);
                    if (!flag.Status)
                    {
                        return new AuraeResult(false, "Click thất bại -> " + xpath);
                    }
                }
                Thread.Sleep(3000);
                checkLogin = CheckLoginMBasic(chrome, account, false);
                if (checkLogin.Status)
                {
                    return new AuraeResult(true, "Login success");
                }
                else
                {
                    return new AuraeResult(false, checkLogin.Message);
                }

            }
            catch (Exception ex)
            {

            }
            return new AuraeResult(false, "Login false catch");
        }
        public static AuraeResult CheckLoginMBasic(ChromeDriver chrome, Account account, bool isCheckLoginInput = true)
        {
            int timeStart = Environment.TickCount;
            while (true)
            {
                try
                {
                    #region check login
                    {
                        
                        { // 2FA
                            string xpath = "//*[@name='approvals_code']";
                            string sendValue = "";
                            AliceSeleniumHelperReturn checkFlag2FA = AliceSendFast.BySelenium(chrome, xpath, sendValue);
                            if (checkFlag2FA.Status)
                            {
                                Submit2FA(chrome, account);
                            }
                        }

                        {
                            AliceClickFast.BySelenium(chrome, "//*[contains(@id,'checkpointSubmitButton-actual')]");
                            AliceClickFast.BySelenium(chrome, "//*[@name='submit[This was me]' and @value]");
                            AliceClickFast.BySelenium(chrome, "//*[@name='submit[Continue]']");
                            // click vao nut luu dia diem
                            AliceClickFast.BySelenium(chrome, "//button[@value='OK']");
                            // getting started
                            AliceClickFast.BySelenium(chrome, "//*[@data-sigil='mChromeHeaderRight']");
                        }


                        try
                        {
                            var cookie = chrome.Manage().Cookies.GetCookieNamed("checkpoint");
                            if (cookie != null)
                            {
                                if (chrome.Url.Contains("disabled"))
                                {
                                    account.C = AliceCookie.Get(chrome);
                                    account.AccountStatus = "Checkpoint Disabled";
                       
                                    return new AuraeResult(false, "Checkpoint Disabled");
                                }
                                else if (AliceClickFast.BySelenium(chrome, "//*[@href='/']").Status)
                                {
                                    account.AccountStatus = "Acc lỗi dạng Something went wrong";
        
                                    return new AuraeResult(false, "Acc lỗi dạng Something went wrong");
                                }
                                else
                                {
                                    account.C = AliceCookie.Get(chrome);
                                    account.AccountStatus = "Checkpoint Normal";
 
                                    return new AuraeResult(false, "Checkpoint Normal");
                                }

                            }
                            System.Console.WriteLine(cookie);
                        }
                        catch { }

                        bool isHaveUID = AliceSource.ContainText(chrome, account.U);
                        bool isHaveDtsg = false;
                        {
                            //UID và fb_dtsg và urrl khoong checkpoint
                            bool isCheckpoint = chrome.Url.Contains("checkpoint");
                            string fb_dtsg = AliceSource.RegexText(chrome, "name=\"fb_dtsg\" value=\"(.*?)\"", 1);
                            if (fb_dtsg != null)
                            {
                                isHaveDtsg = true;
                            }
                            if (isHaveDtsg && isHaveUID && !isCheckpoint)
                            {
                                account.C = AliceCookie.Get(chrome);
                                account.AccountStatus = "Live";
                         
                                return new AuraeResult(true, "Login Success");
                            }
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {

                }


                Thread.Sleep(1000);
                #region Time out
                if (Environment.TickCount - timeStart > 30 * 1000)
                {

                    {
                       
                    }

                    AliceSource.GetChromeSource(chrome);
                    string errorPath = "Error";
                    if (!System.IO.Directory.Exists(errorPath))
                    {
                        System.IO.Directory.CreateDirectory(errorPath);
                    }
                    File.WriteAllText(errorPath + $"/{account.U}.html", chrome.PageSource);
                    return new AuraeResult(false, "Time out");
                }
                #endregion

                Thread.Sleep(100);
            }
            return new AuraeResult(true, "finished check login");
        }

        public static AuraeResult CheckLoginFast(ChromeDriver chrome, Account account)
        {
            try
            {
                if (!chrome.Url.Contains("mbasic"))
                {
                    AliceChrome.GotoUrl(chrome, "https://mbasic.facebook.com/me");
                }
                string dataJsPost = "dmFyIHJlcXVlc3RPcHRpb25zID0gewogIG1ldGhvZDogJ0dFVCcsCiAgcmVkaXJlY3Q6ICdmb2xsb3cnCn07CgoKdmFyIG9iaiA9IG5ldyBPYmplY3QoKTsKcmV0dXJuIGZldGNoKCJodHRwczovL21iYXNpYy5mYWNlYm9vay5jb20vc2V0dGluZ3MvYWNjb3VudC8/bmFtZSIsIHJlcXVlc3RPcHRpb25zKQoudGhlbihyZXNwb25zZSA9PiB7CglvYmoudXJsID0gcmVzcG9uc2UudXJsOwoJcmV0dXJuIHJlc3BvbnNlLnRleHQoKTsKfSkudGhlbihyZXN1bHQgPT4gewoJb2JqLnRleHQgPSByZXN1bHQ7CglyZXR1cm4gSlNPTi5zdHJpbmdpZnkob2JqKTsKfSk7";
                dataJsPost = Base64Helper.Base64Decode(dataJsPost);
                string html = chrome.ExecuteScript(dataJsPost).ToString();
                JsResult jsResult = JsonConvert.DeserializeObject<JsResult>(html);
                html = jsResult.Text;
                if (jsResult.Url.Contains("828281030927956") || jsResult.Url.Contains("1501092823525282") || jsResult.Url.Contains("checkpoint"))
                {
                    return new AuraeResult(false, "Checkpoint");
                }
                else
                {
                    return new AuraeResult(true, "Live");
                }
            }
            catch
            {

            }
            return new AuraeResult(false, "Catch");
        }

        public static AuraeResult Submit2FA(ChromeDriver chrome, Account account)
        {
            int timeStart = Environment.TickCount;
            while (true)
            {
                try
                {
                    string otp = TOTPHelper.Get2FACode(account.F.Replace(" ", ""));
                    string xpath = "//*[@name='approvals_code']";
                    string sendValue = otp;
                    var flag = AliceSendWait.BySelenium(chrome, xpath, sendValue, 1);
                    if (flag.Status)
                    {
                        string submitXpath = "//*[@name='submit[Submit Code]']";
                        var flagSubmit = AliceClickWait.BySelenium(chrome, submitXpath);
                        if (!flagSubmit.Status)
                        {
                            return new AuraeResult(false, "Click thất bại -> " + submitXpath);
                        }

                        string continueXpath = "//*[@name='submit[Continue]']";
                        var flagContinue = AliceClickWait.BySelenium(chrome, continueXpath);
                        if (!flagContinue.Status)
                        {
                            return new AuraeResult(false, "Click thất bại -> " + continueXpath);
                        }
                        else
                        {
                            return new AuraeResult(true, "Success");
                        }
                    }

                }
                catch (Exception ex)
                {

                }

                #region Time out
                if (Environment.TickCount - timeStart > 30 * 1000)
                {
                    AliceSource.GetChromeSource(chrome);
                    string errorPath = "Error";
                    if (!System.IO.Directory.Exists(errorPath))
                    {
                        System.IO.Directory.CreateDirectory(errorPath);
                    }
                    File.WriteAllText(errorPath + $"/{account.U}.html", chrome.PageSource);
                    return new AuraeResult(false, "Time out");
                }
                #endregion

                Thread.Sleep(100);
            }
            return new AuraeResult(false, "Catch");
        }

        public static AuraeResult BypassSpam(ChromeDriver chrome, Account account)
        {
            try
            {
                AliceChrome.GotoUrl(chrome, "https://mbasic.facebook.com");

                var checkloginfast = CheckLoginFast(chrome, account);
                if (!checkloginfast.Status)
                {
                    return new AuraeResult(false, "Checkpoint");
                }
                else
                {
                    AliceChrome.GotoUrl(chrome, "https://m.facebook.com");

                }



                int timeStart = Environment.TickCount;
                while (true)
                {
                    try
                    {
                        AliceClickFast.ByJScript(chrome, "//*[@data-nt=\"NT:BOX_3_CHILD\"]//*[@role=\"button\"]/../..");
                        AliceClickFast.ByJScript(chrome, "//*[@data-nt=\"NT:BOX_3_CHILD\"]//*[@data-nt=\"FB:BORDER\"]/../..");
                        AliceClickFast.ByJScript(chrome, "//*[@data-nt=\"NT:BOX_3_CHILD\"]//*[@data-nt=\"FB:BORDER\"]/../../../..");
                        AliceClickFast.ByJScript(chrome, "//*[@data-nt=\"NT:BOX_3_CHILD\"]//*[@data-nt=\"FB:TEXT4\" and contains(@style,\"FFFF\")]/../../../..");
                        AliceClickFast.ByJScript(chrome, "//*[@data-nt=\"NT:BOX_3_CHILD\"]//*[@role=\"button\"]/../..");

                        { // Done
                            string xpath = "//*[@data-nt='FB:FDS_TETRA_BUTTON']";

                            AliceSeleniumHelperReturn checkDone = AliceClickWait.BySelenium(chrome, xpath, 1);
                            if (checkDone.Status)
                            {
                                AliceChrome.GotoUrl(chrome, "https://m.facebook.com");
                                return new AuraeResult(true, "Success");
                            }
                        }

                        { // thoats neu khong co nut tiep tuc
                            bool isSpam = chrome.Url.Contains("actor_experience/actor_gateway");
                            var elementNextButton = AliceElement.FindElement(chrome, "//*[@data-nt='NT:BOX_3_CHILD']//*[@role='button']/../..", 1);
                            var elementBack = AliceElement.FindElement(chrome, "//span/div/img", 1);
                            if (elementNextButton == null && elementBack != null && isSpam)
                            {
                                AliceClickFast.ByJScript(chrome, "//span/div/img");
                                Thread.Sleep(4000);
                            }
                        }

                        {
                            var elementNextButton = AliceElement.FindElement(chrome, "//*[@data-nt='NT:BOX_3_CHILD']//*[@role='button']/../..", 1);
                            bool isPass = chrome.Url.Contains("ixt/msite/screen/next");
                            if (isPass && elementNextButton == null)
                            {
                                AliceChrome.GotoUrl(chrome, "https://m.facebook.com");
                                return new AuraeResult(true, "Success");
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                    }


                    Thread.Sleep(1000);
                    #region Time out
                    if (Environment.TickCount - timeStart > 30 * 1000)
                    {
                        AliceChrome.GotoUrl(chrome, "https://mbasic.facebook.com");
                        { // checkpoint 282
                            bool isCheckpoint = chrome.Url.Contains("1501092823525282");
                            if (isCheckpoint)
                            {
                                account.AccountStatus = "Checkpoint 282";
                      
                                return new AuraeResult(false, "Checkpoint 282");
                            }
                        }
                        { // checkpoint 956
                            bool isCheckpoint = chrome.Url.Contains("828281030927956");
                            if (isCheckpoint)
                            {
                                account.AccountStatus = "Checkpoint 956";
         
                                return new AuraeResult(false, "Checkpoint 956");
                            }
                        }


                        AliceSource.GetChromeSource(chrome);
                        string errorPath = "Error";
                        if (!System.IO.Directory.Exists(errorPath))
                        {
                            System.IO.Directory.CreateDirectory(errorPath);
                        }
                        File.WriteAllText(errorPath + $"/{account.U}.html", chrome.PageSource);
                        return new AuraeResult(false, "Time out");
                    }
                    #endregion

                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {

            }
            return new AuraeResult(false, "Catch");
        }

        public class JsResult
        {
            public string Text { get; set; }

            public string Url { get; set; }
        }

    }






}

