using System.Runtime.Serialization;

namespace CashRegister.Client.Models.DTO
{
    public enum Currencies
    {
        [EnumMember(Value = "Pennies")]
        Penny,
        [EnumMember(Value = "Nickels")]
        Nickel,
        [EnumMember(Value = "Dimes")]
        Dime,
        [EnumMember(Value = "Quarters")]
        Quarter,
        [EnumMember(Value = "Half Dollars")]
        HalfDollar,
        [EnumMember(Value = "Dollar Bills")]
        Dollar,
        [EnumMember(Value = "Two Dollar Bills")]
        TwoDollars,
        [EnumMember(Value = "Five Dollar Bills")]
        FiveDollars,
        [EnumMember(Value = "Ten Dollar Bills")]
        TenDollars,
        [EnumMember(Value = "Twenty Dollar Bills")]
        TwentyDollars,
        [EnumMember(Value = "Gold Doubloons")]
        GoldDoubloons
    }
}