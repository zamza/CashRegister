using CashRegister.Data.Store;
using CashRegister.Domain.Abstract;
using CashRegister.Domain.Model;
using CashRegister.Services.Abstract;

namespace CashRegister.Services.Manager
{
    public class CashRegisterManager : ICashRegisterManager
    {
        private readonly ICashRegister _cashRegister;
        private readonly CashRegisterRepository _cashRegisterRepository;

        public CashRegisterManager(ICashRegister cashRegister, CashRegisterRepository cashRegisterRepository)
        {
            _cashRegister = cashRegister;
            _cashRegisterRepository = cashRegisterRepository;
        }

        public CurrencyAmounts AddCashToCashRegister(CurrencyAmounts currencyAmounts)
        {
            return _cashRegister.AddCash(currencyAmounts);
        }

        public CurrencyAmounts GetAmountInCashRegister()
        {
            return _cashRegister.GetAmounts();
        }

        public CurrencyAmounts HandlePayment(Transaction transaction)
        {
            return _cashRegister.TakePayment(transaction);
        }
    }
}
