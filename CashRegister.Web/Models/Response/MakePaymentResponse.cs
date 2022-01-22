using CashRegister.Domain;

namespace CashRegister.Web.Models.Response
{
    public class MakePaymentResponse
    {
        public Dictionary<Currencies, int> Amounts { get; set; }
    }
}
