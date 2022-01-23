using CashRegister.Domain.Abstract;

namespace CashRegister.Domain.Models
{
    public class CashRegister : ICashRegister
    {
        private Dictionary<Currencies, int> _amounts { get; set; }

        private Dictionary<Currencies, decimal> _denominations { get; set; }

        public CashRegister(Dictionary<Currencies, int> amounts, Dictionary<Currencies, decimal> denominations)
        {
            _denominations = denominations;
            _amounts = FillAmounts(amounts);
        }

        public Dictionary<Currencies, int> AddCash(Dictionary<Currencies, int> amountAdded)
        {
            CheckAmountPaidDenominationsValid(amountAdded);
            CheckNegativeAmounts(amountAdded);

            UpdateAmounts(amountAdded);
            return _amounts;
        }

        public Dictionary<Currencies, int> GetAmounts()
        {
            return _amounts;
        }

        public Dictionary<Currencies, int> TakePayment(Transaction transaction)
        {
            CheckAmountPaidDenominationsValid(transaction.AmountsPaid);
            CheckNegativeAmounts(transaction.AmountsPaid);
            CheckAmountsCoverCost(transaction);

            var updatedAmounts = GetUpdatedAmounts(transaction.AmountsPaid);
            var changeTotal = CalculateChange(transaction);
            var changeDenominations = MakeChange(changeTotal, updatedAmounts);

            CheckRegisterAmountsCoverAmountPaid(changeTotal, changeDenominations);

            _amounts = updatedAmounts;

            return changeDenominations;
        }
        private decimal CalculateChange(Transaction transaction)
        {
            var amountPaid = SumAmounts(transaction.AmountsPaid);
            return amountPaid - transaction.Cost;
        }

        private void CheckAmountsCoverCost(Transaction transaction)
        {
            if (SumAmounts(transaction.AmountsPaid) <= transaction.Cost)
            {
                throw new InvalidOperationException("The amount paid wasn't enough to cover the cost.");
            }
        }

        private void CheckAmountPaidDenominationsValid(Dictionary<Currencies, int> amounts)
        {
            if (!amounts.Keys.All(denomination => _denominations.ContainsKey(denomination)))
            {
                throw new InvalidOperationException("Cash register does not recognize one of the denominations.");
            }
        }

        private void CheckNegativeAmounts(Dictionary<Currencies, int> amountPaid)
        {
            if (amountPaid.Any(denomination => denomination.Value < 0))
            {
                throw new InvalidOperationException("One or more of the denominations was negative.");
            }
        }

        private void CheckRegisterAmountsCoverAmountPaid(decimal changeTotal, Dictionary<Currencies, int> amounts)
        {
            if (changeTotal != SumAmounts(amounts))
            {
                throw new InvalidOperationException("Cash Register does not hold enough cash to cover the transaction");
            }
        }

        private Dictionary<Currencies, int> FillAmounts(Dictionary<Currencies, int> amounts)
        {
            return _denominations.ToDictionary(
                denomination => denomination.Key,
                denomination => amounts.ContainsKey(denomination.Key) ? amounts[denomination.Key] : 0);
        }

        private Dictionary<Currencies, int> GetUpdatedAmounts(Dictionary<Currencies, int> amountAdded)
        {
            var updatedAmounts = _amounts.ToDictionary(amount => amount.Key, amount => amount.Value);

            foreach (var denomination in amountAdded.Where(denomination => denomination.Value > 0))
            {
                updatedAmounts[denomination.Key] += denomination.Value;
            }

            return updatedAmounts;
        }

        private Dictionary<Currencies, int> MakeChange(decimal amountToReturn, Dictionary<Currencies, int> amounts)
        {
            var amountReturned = new Dictionary<Currencies, int>();
            foreach (var denomination in _denominations.OrderByDescending(key => key.Value))
            {
                var amountUsed = Convert.ToInt32(Math.Min(Math.Floor(amountToReturn / denomination.Value), amounts[denomination.Key]));
                amountReturned.Add(denomination.Key, amountUsed);
                amountToReturn -= amountUsed * denomination.Value;
                amounts[denomination.Key] -= amountUsed;
            }
            return amountReturned;
        }

        private decimal SumAmounts(Dictionary<Currencies, int> amounts)
        {
            return amounts.Sum(amount => amount.Value * _denominations[amount.Key]);
        }

        private void UpdateAmounts(Dictionary<Currencies, int> amountAdded)
        {
            foreach (var denomination in amountAdded.Where(denomination => denomination.Value > 0))
            {
                _amounts[denomination.Key] += denomination.Value;
            }
        }
    }
}
