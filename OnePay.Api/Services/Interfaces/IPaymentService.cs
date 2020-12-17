using OnePay.Api.Models;
using System.Threading.Tasks;

namespace OnePay.Api.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponse> MakePaymentAsync(PaymentRequest request);
    }
}
