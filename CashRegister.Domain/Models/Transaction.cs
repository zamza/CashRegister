namespace CashRegister.Domain.Models
{
    public class Transaction
    {
        public CurrencyAmounts AmountsPaid { get; set; }

        public decimal Cost { get; set; }
    }
}
