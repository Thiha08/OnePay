using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnePay.Api.Common
{
    public class OnePayApiSettings : IOnePayApiSettings
    {
        public string Url { get ; set ; }

        public string MerchantUserId { get ; set ; }

        public string Token { get ; set ; }

        public string WalletUserID { get ; set ; }

        public string Channel { get ; set ; }

        public string Version { get; set; }
    }
}
