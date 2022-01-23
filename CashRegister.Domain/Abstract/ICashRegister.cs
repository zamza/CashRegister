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

        Dictionary<Currencies, int> GetAmounts();

        Dictionary<Currencies, int> TakePayment(Transaction transaction);
    }
}
