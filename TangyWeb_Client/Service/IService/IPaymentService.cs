using Tangy_Models.DTO;

namespace TangyWeb_Client.Service.IService
{
    public interface IPaymentService
    {
        Task<SuccessModelDTO> CheckOut(StripePaymentDTO stripePaymentDTO);
    }
}
