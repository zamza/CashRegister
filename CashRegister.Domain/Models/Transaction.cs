﻿namespace CashRegister.Domain.Models
{
    public class Transaction
    {
        public Dictionary<Currencies, int> AmountsPaid { get; set; }

        public decimal Cost { get; set; }
    }
}
