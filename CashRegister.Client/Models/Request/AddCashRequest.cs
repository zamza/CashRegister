using CashRegister.Client.Models.DTO;

namespace CashRegister.Web.Models.Request
{
    public class AddCashRequest
    {
        public CurrencyAmounts CurrencyAmounts { get; set; }
    }
}
