using CashRegister.Domain;

namespace CashRegister.Web.Models.Request
{
    public class AddCashRequest
    {
        public Dictionary<Currencies, int> Amounts { get; set; }
    }
}
