using CashRegister.Web.Models.DTO;

namespace CashRegister.Web.Models.DTO
{
    public class Transaction
    {
        public CurrencyAmounts AmountsPaid { get; set; }

        public decimal Cost { get; set; }
    }
}
