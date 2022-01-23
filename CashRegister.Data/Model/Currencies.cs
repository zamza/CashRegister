using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Data.Model
{
    public enum Currencies
    {
        None = 0,
        Penny = 1 << 0,
        Nickel = 1 << 1,
        Dime = 1 << 2,
        Quarter = 1 << 3,
        HalfDollar = 1 << 4 ,
        Dollar = 1 << 5,
        TwoDollars = 1 << 6,
        FiveDollars = 1 << 7,
        TenDollars = 1 << 8,
        TwentyDollars = 1 << 9,
        GoldDoubloons = 1 << 10,
    }
}
