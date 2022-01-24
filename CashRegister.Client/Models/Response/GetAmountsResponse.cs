using CashRegister.Client.Models.DTO;

namespace CashRegister.Client.Models.Response
{
    public class GetAmountsResponse
    {
        public List<DenominationAmount> Amounts { get; set; }
    }
}
