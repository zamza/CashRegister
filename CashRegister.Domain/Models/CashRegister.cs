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

        public Dictionary<Currencies, int> TakePayment(decimal amountPayed)
        {
            var transaction = _amounts.ToDictionary(entry => entry.Key, entry => entry.Value);
            var amountReturned = new Dictionary<Currencies, int>();
            foreach (KeyValuePair<Currencies, decimal> denomination in _denominations.OrderByDescending(key => key.Value))
            {
                var amountUsed = Convert.ToInt32(Math.Min(Math.Floor(amountPayed / denomination.Value), transaction[denomination.Key]));
                amountReturned.Add(denomination.Key, amountUsed);
                amountPayed -= amountUsed * denomination.Value;
                transaction[denomination.Key] -= amountUsed;
            }

            if (amountPayed > 0)
            {
                throw new InvalidOperationException();
            }
            else
            {
                _amounts = transaction;
                return amountReturned;
            }

        }
    }
}
