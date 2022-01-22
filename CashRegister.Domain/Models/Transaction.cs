using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Models
{
    public class Transaction
    {
        public Dictionary<Currencies, int> AmountsPaid { get; private set; }

        public decimal Cost { get; private set; }

        public Transaction(Dictionary<Currencies, int> amountsPaid, decimal cost)
        {
            this.AmountsPaid = amountsPaid;
            this.Cost = cost;
        }
    }
}
