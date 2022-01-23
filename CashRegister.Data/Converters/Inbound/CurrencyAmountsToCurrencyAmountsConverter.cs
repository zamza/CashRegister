using AutoMapper;
using CashRegister.Data.Model;

namespace CashRegister.Data.Converters.Inbound
{
    public class CurrencyAmountsToCurrencyAmountsConverter : ITypeConverter<Domain.Model.CurrencyAmounts, CurrencyAmounts>
    {
        public CurrencyAmounts Convert(Domain.Model.CurrencyAmounts source, CurrencyAmounts destination, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
