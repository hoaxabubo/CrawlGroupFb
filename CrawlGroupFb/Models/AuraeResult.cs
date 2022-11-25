using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGroupFb.Models
{
    public class AuraeResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
     
        public AuraeResult()
        {

        }
        public AuraeResult(bool status, string message)
        {
            Status = status;
            Message = message;
            
        }
        
    }
}
