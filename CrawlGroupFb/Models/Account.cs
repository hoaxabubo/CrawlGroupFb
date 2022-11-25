using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGroupFb.Models
{
    public partial class Account
    {
        public string U { get; set; }

        public string P { get; set; }

        public string C { get; set; }

        public string F { get; set; }

        public string Email { get; set; }

        public string EmailPassword { get; set; }

        public string RecoveryEmail { get; set; }

        public string RecoveryEmailPassword { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string Birthday { get; set; }

        public long? Friends { get; set; }

        public long? RequestSent { get; set; }

        public long? RequestReceive { get; set; }

        public long? Suggestion { get; set; }

        public long? Groups { get; set; }

        public long? Pages { get; set; }

        public long? AdsAccountCount { get; set; }

        public long? BmCount { get; set; }

        public string CreatedTime { get; set; }

        public int? Avatar { get; set; }

        public int? Cover { get; set; }

        public string Token { get; set; }

        public string Language { get; set; }

        public string Gender { get; set; }

        public string Notes { get; set; }

        public string AccountStatus { get; set; }

        public string ModuleStatus { get; set; }

        public string AccountQuality { get; set; }

        public string Location { get; set; }

        public string LastUpdate { get; set; }

        public string GroupCode { get; set; }

        public string Proxy { get; set; }

        public string UserAgent { get; set; }

    }
}
