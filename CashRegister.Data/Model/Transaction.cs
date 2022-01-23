using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Data.Model
{
    public class Transaction
    {
        public CurrencyAmounts AmountsPaid { get; set; }

        public decimal Cost { get; set; }
    }
}
