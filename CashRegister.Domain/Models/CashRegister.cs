using CashRegister.Domain.Abstract;

namespace CashRegister.Domain.Models
{
    public class CashRegister : ICashRegister
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

        public Dictionary<Currencies, int> AddCash(Dictionary<Currencies, int> amountAdded)
        {
            foreach (KeyValuePair<Currencies, int> denomination in amountAdded)
            {
                _amounts[denomination.Key] += denomination.Value;
            }

            return _amounts;
        }

        public bool CanAmountsCoverTransaction(Dictionary<Currencies, int> amountPayed)
        {
            return CanAmountsCoverTransaction(SumAmounts(amountPayed));
        }

        public bool CanAmountsCoverTransaction(decimal amountPayed)
        {
            return SumAmounts(_amounts) >= amountPayed;
        }

        public Dictionary<Currencies, int> GetAmounts()
        {
            return _amounts;
        }

        public bool IsTransactionDenominationsValid(Transaction transaction)
        {
            return transaction.AmountsPaid.Keys.All(denomination => _denominations.ContainsKey(denomination));
        }

        public Dictionary<Currencies, int> TakePayment(Transaction transaction)
        {
            if (CanAmountsCoverTransaction(transaction.Cost))
            {
                var change = GetChange(transaction);
                return MakeChange(change);
            }
            else
            {
                throw new InvalidOperationException("Cash Register does not hold enough cash to cover the transaction");
            }
        }

        private decimal GetChange(Transaction transaction)
        {
            var amountPaid = SumAmounts(transaction.AmountsPaid);
            return amountPaid - transaction.Cost;
        }

        private Dictionary<Currencies, int> MakeChange(decimal amountToReturn)
        {
            var amountReturned = new Dictionary<Currencies, int>();
            foreach (KeyValuePair<Currencies, decimal> denomination in _denominations.OrderByDescending(key => key.Value))
            {
                var amountUsed = Convert.ToInt32(Math.Min(Math.Floor(amountToReturn / denomination.Value), _amounts[denomination.Key]));
                amountReturned.Add(denomination.Key, amountUsed);
                amountToReturn -= amountUsed * denomination.Value;
                _amounts[denomination.Key] -= amountUsed;
            }
            return amountReturned;
        }
    }
}
