using CashRegister.Domain.Abstract;

namespace CashRegister.Domain.Models
{
    public class CashRegister : ICashRegister
    {
        private CurrencyAmounts _amounts { get; set; }

        private Dictionary<Currencies, decimal> _denominations { get; set; }

        public CashRegister(CurrencyAmounts amounts, Dictionary<Currencies, decimal> denominations)
        {
            _denominations = denominations;
            _amounts = FillAmounts(amounts);
        }

        public CurrencyAmounts AddCash(CurrencyAmounts amountAdded)
        {
            CheckAmountPaidDenominationsValid(amountAdded);
            CheckNegativeAmounts(amountAdded);

            UpdateAmounts(amountAdded);
            return _amounts;
        }

        public CurrencyAmounts GetAmounts()
        {
            return _amounts;
        }

        public CurrencyAmounts TakePayment(Transaction transaction)
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
            var amountPaid = transaction.AmountsPaid.SumAmounts(_denominations);
            return amountPaid - transaction.Cost;
        }

        private void CheckAmountsCoverCost(Transaction transaction)
        {
            if (transaction.AmountsPaid.SumAmounts(_denominations) <= transaction.Cost)
            {
                throw new InvalidOperationException("The amount paid wasn't enough to cover the cost.");
            }
        }

        private void CheckAmountPaidDenominationsValid(CurrencyAmounts amounts)
        {
            if (!amounts.Amounts.Keys.All(denomination => _denominations.ContainsKey(denomination)))
            {
                throw new InvalidOperationException("Cash register does not recognize one of the denominations.");
            }
        }

        private void CheckNegativeAmounts(CurrencyAmounts amountPaid)
        {
            if (amountPaid.Amounts.Any(denomination => denomination.Value < 0))
            {
                throw new InvalidOperationException("One or more of the denominations was negative.");
            }
        }

        private void CheckRegisterAmountsCoverAmountPaid(decimal changeTotal, CurrencyAmounts amounts)
        {
            if (changeTotal != amounts.SumAmounts(_denominations))
            {
                throw new InvalidOperationException("Cash Register does not hold enough cash to cover the transaction");
            }
        }

        private CurrencyAmounts FillAmounts(CurrencyAmounts amounts)
        {
            return new CurrencyAmounts()
            {
                Amounts = _denominations.ToDictionary(
                    denomination => denomination.Key,
                    denomination =>
                        amounts.Amounts.ContainsKey(denomination.Key) ? amounts.Amounts[denomination.Key] : 0)
            };
        }

        private CurrencyAmounts GetUpdatedAmounts(CurrencyAmounts amountAdded)
        {
            var updatedAmounts = _amounts.Amounts.ToDictionary(amount => amount.Key, amount => amount.Value);

            foreach (var denomination in amountAdded.Amounts.Where(denomination => denomination.Value > 0))
            {
                updatedAmounts[denomination.Key] += denomination.Value;
            }

            return new CurrencyAmounts()
            {
                Amounts = updatedAmounts
            };
        }

        private CurrencyAmounts MakeChange(decimal amountToReturn, CurrencyAmounts amounts)
        {
            var amountReturned = new Dictionary<Currencies, int>();
            foreach (var denomination in _denominations.OrderByDescending(key => key.Value))
            {
                var amountUsed = Convert.ToInt32(Math.Min(Math.Floor(amountToReturn / denomination.Value), amounts.Amounts[denomination.Key]));
                amountReturned.Add(denomination.Key, amountUsed);
                amountToReturn -= amountUsed * denomination.Value;
                amounts.Amounts[denomination.Key] -= amountUsed;
            }

            return new CurrencyAmounts()
            {
                Amounts = amountReturned
            };
        }

        private void UpdateAmounts(CurrencyAmounts amountAdded)
        {
            foreach (var denomination in amountAdded.Amounts.Where(denomination => denomination.Value > 0))
            {
                _amounts.Amounts[denomination.Key] += denomination.Value;
            }
        }
    }
}
