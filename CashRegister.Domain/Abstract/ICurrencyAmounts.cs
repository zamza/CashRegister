using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Domain.Models;

namespace CashRegister.Domain.Abstract
{
    internal interface ICurrencyAmounts
    {
        Dictionary<Currencies, int> Amounts { get; set; }

        decimal SumAmounts(Dictionary<Currencies, decimal> denominations);
    }
}
