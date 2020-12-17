using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnePay.Api.Common;
using OnePay.Api.Models;
using OnePay.Api.Services.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace OnePay.Api.Controllers
{
    [Produces("application/json")]
    [Route("API/Ver02/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly ILogger<WalletController> _logger;
        private readonly IPaymentService _paymentService;
        private readonly IOnePayApiSettings _onePayApiSettings;

        public WalletController(IPaymentService paymentService, IOnePayApiSettings onePayApiSettings)
        {
            _paymentService = paymentService;
            _onePayApiSettings = onePayApiSettings;
        }


        /// <summary>
        /// Direct payment API is used to make payment for ecommerce with Onepay System from Merchant system.
        /// </summary>
        /// <param name="PaymentRequest"></param>
        /// <returns>PaymentResponse</returns>
        [Route("WalletDirectApiV2")]
        [HttpPost]
        [ProducesResponseType(typeof(PaymentResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaymentResponse>> Post([FromBody] PaymentRequest request)
        {
            try
            {
                return await _paymentService.MakePaymentAsync(request);
            }
            catch (Exception ex)
            {
                return new PaymentResponse { RespCode = OnePayResponseCode.SystemError };
            }
        }

        /// <summary>
        /// [FOR TESTING ONLY] To make a payment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Wallet/TEST_Wallet_DirectAPIV2
        ///     {
        ///        amount: 10000
        ///     }
        ///
        /// </remarks>
        /// <returns>A JSON object stating whether the action is successful carried out or not.</returns>
        [Route("TestWalletDirectApiV2")]
        [HttpPost]
        [ProducesResponseType(typeof(PaymentResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaymentResponse>> TestToMakePayment([FromBody] TestToMakePayment model)
        {
            try
            {
                var payment = new PaymentRequest
                {
                    Version = _onePayApiSettings.Version,
                    Channel = _onePayApiSettings.Channel,
                    MerchantUserId = _onePayApiSettings.MerchantUserId,
                    InvoiceNo = "INV001",
                    SequenceNo = "CEF11DB87838433EB9F33394AE98E3E7",
                    Amount = model.Amount,
                    Remark = "Testing",
                    WalletUserID = _onePayApiSettings.WalletUserID,
                    CallBackUrl = "uat.{merchantname}.com/AGD_ResponseDirectAPI",
                    ExpiredSeconds = 060
                };

                return await _paymentService.MakePaymentAsync(payment);
            }
            catch (Exception ex)
            {
                return new PaymentResponse { RespCode = OnePayResponseCode.SystemError };
            }
        }
    }

    public class TestToMakePayment
    {
        public string Amount { get; set; }
    }
}
