using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Domain.Model;

namespace CashRegister.Domain.Abstract
{
    public interface ICashRegister
    {
        CurrencyAmounts AddCash(CurrencyAmounts transaction);

        CurrencyAmounts GetAmounts();

        CurrencyAmounts TakePayment(Transaction transaction);
    }
}
