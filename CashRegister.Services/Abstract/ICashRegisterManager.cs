using CashRegister.Domain.Model;

namespace CashRegister.Services.Abstract
{
    public interface ICashRegisterManager
    {
        CurrencyAmounts AddCashToCashRegister(CurrencyAmounts currencyAmounts);

        CurrencyAmounts GetAmountInCashRegister();

        CurrencyAmounts HandlePayment(Transaction transaction);
    }
}
