using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnePay.Api.Common
{
    public interface IOnePayApiSettings
    {
        string Url { get; set; }

        string MerchantUserId { get; set; }

        string Token { get; set; }

        string WalletUserID { get; set; }

        string Channel { get; set; }

        string Version { get; set; }
    }
}
