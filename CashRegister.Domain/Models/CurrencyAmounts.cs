using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Domain.Abstract;

namespace CashRegister.Domain.Models
{
    public class CurrencyAmounts : ICurrencyAmounts
    {
        public Dictionary<Currencies, int> Amounts { get; set; }

        public decimal SumAmounts(Dictionary<Currencies, decimal> denominations)
        {
            return Amounts.Sum(amount => amount.Value * denominations[amount.Key]);
        }
    }
}
