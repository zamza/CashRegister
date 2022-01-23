using CashRegister.Domain.Abstract;

namespace CashRegister.Domain.Model
{
    public class CurrencyAmounts : ICurrencyAmounts
    {
        public Dictionary<Currencies, int> Amounts { get; set; }

        public decimal SumAmounts(Dictionary<Currencies, decimal> denominations)
        {
            return Amounts.Sum(amount => amount.Value * denominations[amount.Key]);
        }
    }
}
