using CashRegister.Domain;

namespace CashRegister.Web.Models.Response
{
    public class AddCashResponse
    {
        public Dictionary<Currencies, int> Amounts { get; set; }
    }
}
