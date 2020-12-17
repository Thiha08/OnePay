using Newtonsoft.Json;
using OnePay.Api.Common;
using OnePay.Api.Models;
using OnePay.Api.Services.Interfaces;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnePay.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private IOnePayApiSettings _onePayApiSettings;

        public PaymentService(IOnePayApiSettings onePayApiSettings)
        {
            _onePayApiSettings = onePayApiSettings;
        }

        public async Task<PaymentResponse> MakePaymentAsync(PaymentRequest payment)
        {
            var signatureString = GetSignatureString(payment);
            payment.HashValue = Hashing.GetHMAC(signatureString, _onePayApiSettings.Token);

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(_onePayApiSettings.Url, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PaymentResponse>(apiResponse);
                }
            }
        }

        private string GetSignatureString(PaymentRequest payment)
        {
            return payment.Version + payment.Channel + payment.MerchantUserId + payment.InvoiceNo + payment.Amount + payment.Remark + payment.SequenceNo;
        }
    }
}
