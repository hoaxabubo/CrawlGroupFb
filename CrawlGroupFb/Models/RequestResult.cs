using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGroupFb.Models
{
    public class RequestResult
    {
        public RequestResult(bool status, string message)
        {
            Status = status;
            Message = message;

        }
        public RequestResult(bool status, string message, RequestEnum @enum)
        {
            Status = status;
            Message = message;
            EnumResult = @enum;
        }
        public bool Status { get; set; }
        public string Message { get; set; }
        public RequestEnum EnumResult { get; set; }
    }
}
