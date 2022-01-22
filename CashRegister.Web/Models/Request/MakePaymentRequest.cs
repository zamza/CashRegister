using CashRegister.Domain;

namespace CashRegister.Web.Models.Request
{
    public class MakePaymentRequest
    {
        public Dictionary<Currencies, int> AmountsPaid { get; set; }

        public decimal Cost { get; set; }
    }
}