using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Domain.Models;

namespace CashRegister.Domain.Abstract
{
    public interface ICashRegister
    {
        Dictionary<Currencies, int> AddCash(Dictionary<Currencies, int> transaction);

        bool CanAmountsCoverTransaction(Dictionary<Currencies, int> amountPayed);

        bool CanAmountsCoverTransaction(decimal amountPayed);

        Dictionary<Currencies, int> GetAmounts();

        bool IsTransactionDenominationsValid(Transaction transaction);

        Dictionary<Currencies, int> TakePayment(Transaction transaction);
    }
}
