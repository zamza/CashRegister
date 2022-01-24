using CashRegister.Client.Models.DTO;

namespace CashRegister.Client.Models.Request
{
    public class MakePaymentRequest
    {
        public Transaction Transaction { get; set; }
    }
}