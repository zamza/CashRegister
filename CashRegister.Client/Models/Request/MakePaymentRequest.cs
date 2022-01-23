using CashRegister.Web.Models.DTO;

namespace CashRegister.Web.Models.Request
{
    public class MakePaymentRequest
    {
        public Transaction Transaction { get; set; }
    }
}