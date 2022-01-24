using System.ComponentModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace CashRegister.Client.Models.DTO
{
    public class DenominationAmount
    {
        public Currencies Denomination { get; set; } 

        public int Amount { get; set; }
    }
}
