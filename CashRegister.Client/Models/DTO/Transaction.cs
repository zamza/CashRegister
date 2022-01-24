namespace CashRegister.Client.Models.DTO
{
    public class Transaction
    {
        public List<DenominationAmount> AmountsPaid { get; set; }

        public decimal Cost { get; set; }
    }
}
