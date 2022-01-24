using AutoMapper;
using CashRegister.Client.Models.DTO;

namespace CashRegister.Client.Converters.Inbound
{
    public class CurrencyAmountsToCurrencyAmountsConverter : Client.Converters.ConverterBase, ITypeConverter<CurrencyAmounts, Domain.Model.CurrencyAmounts>
    {
        public CurrencyAmountsToCurrencyAmountsConverter(IMapper mapper) : base(mapper)
        {
        }

        public Domain.Model.CurrencyAmounts Convert(CurrencyAmounts source, Domain.Model.CurrencyAmounts destination, ResolutionContext context)
        {
            Domain.Model.CurrencyAmounts currencyAmounts = null;

            if (source != null)
            {
                currencyAmounts = new Domain.Model.CurrencyAmounts()
                {
                    Amounts = _mapper.Map<Dictionary<Domain.Model.Currencies, int>>(source.Amounts)
                };
            }

            return currencyAmounts;
        }
    }
}
