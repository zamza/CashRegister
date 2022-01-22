using CashRegister.Web.Models.DTO;

namespace CashRegister.Web.Models.Response
{
    public class GetAmountsResponse
    {
        public Dictionary<Currencies, int> Amounts { get; set; }
    }
}
