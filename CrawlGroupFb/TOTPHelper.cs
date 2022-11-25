using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurae_Facebook_Care.Common
{
    public class TOTPHelper
    {
        public static string Get2FACode(string F)
        {
            string Re = "";
            try
            {
         
                HttpRequest xRequest = new HttpRequest();
                xRequest.KeepAlive = false;

                try
                {
                    string FCode = xRequest.Get($"http://thanhson.name.vn/2fa.php?F={F}").ToString();
                    return FCode.Trim();


                }
                catch
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
    }
}
