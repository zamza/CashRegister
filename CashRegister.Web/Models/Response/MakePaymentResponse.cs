using CashRegister.Web.Models.DTO;

namespace CashRegister.Web.Models.Response
{
    public class MakePaymentResponse
    {
        public Dictionary<Currencies, int> Amounts { get; set; }
    }
}
