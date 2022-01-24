using CashRegister.Client.Models.DTO;

namespace CashRegister.Client.Models.Response
{
    public class GetDenominationsResponse
    {
        public List<DenominationValue> Denominations { get; set; }

    }
}
