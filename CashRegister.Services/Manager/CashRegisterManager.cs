using CashRegister.Domain.Abstract;
using CashRegister.Domain.Model;
using CashRegister.Services.Abstract;

namespace CashRegister.Services.Manager
{
    public class CashRegisterManager : ICashRegisterManager
    {
        private readonly ICashRegister _cashRegister;

        public CashRegisterManager(ICashRegister cashRegister)
        {
            _cashRegister = cashRegister;
        }

        public CurrencyAmounts AddCashToCashRegister(CurrencyAmounts currencyAmounts)
        {
            return _cashRegister.AddCash(currencyAmounts);
        }

        public CurrencyAmounts GetAmountInCashRegister()
        {
            return _cashRegister.GetAmounts();
        }

        public Dictionary<Currencies, decimal> GetDenominations()
        {
            return _cashRegister.GetDenominations();
        }

        public CurrencyAmounts HandlePayment(Transaction transaction)
        {
            return _cashRegister.TakePayment(transaction);
        }
    }
}
