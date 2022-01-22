using CashRegister.Domain.Abstract;

namespace CashRegister.Domain.Models
{
    public class CashRegister
    {
        private Dictionary<Currencies, int> _amounts { get; set; }

        private Dictionary<Currencies, decimal> _denominations { get; set; }

        public CashRegister(Dictionary<Currencies, int> amounts, Dictionary<Currencies, decimal> denominations)
        {
            _amounts = amounts;
            _denominations = denominations;
        }

        public decimal SumAmounts(Dictionary<Currencies, int> amounts)
        {
            return amounts.Sum(amount => amount.Value * _denominations[amount.Key]);
        }

        public bool CanAmountsCoverTransaction(Dictionary<Currencies, int> amountPayed)
        {
            return CanAmountsCoverTransaction(SumAmounts(amountPayed));
        }

        public bool CanAmountsCoverTransaction(decimal amountPayed)
        {
            return SumAmounts(_amounts) >= amountPayed;
        }

        public Dictionary<Currencies, int> TakePayment(Dictionary<Currencies, int> amountPayed)
        {
            return TakePayment(SumAmounts(amountPayed));
        }

        public Dictionary<Currencies, int> TakePayment(decimal amountPayed)
        {
            if (CanAmountsCoverTransaction(amountPayed))
            {
                var amountReturned = new Dictionary<Currencies, int>();
                foreach (KeyValuePair<Currencies, decimal> denomination in _denominations.OrderByDescending(key => key.Value))
                {
                    var amountUsed = Convert.ToInt32(Math.Min(Math.Floor(amountPayed / denomination.Value), _amounts[denomination.Key]));
                    amountReturned.Add(denomination.Key, amountUsed);
                    amountPayed -= amountUsed * denomination.Value;
                    _amounts[denomination.Key] -= amountUsed;
                }
                return amountReturned;
            }
            else
            {
                throw new InvalidOperationException("Cash Register does not hold enough cash to cover the transaction");
            }
        }
    }
}
