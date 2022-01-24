using AutoMapper;
using CashRegister.Client.Models.DTO;

namespace CashRegister.Client.Converters.Outbound
{
    public class CurrencyAmountsToCurrencyAmountsConverter : Client.Converters.ConverterBase, ITypeConverter<Domain.Model.CurrencyAmounts, CurrencyAmounts>
    {
        public CurrencyAmountsToCurrencyAmountsConverter(IMapper mapper) : base(mapper)
        {
        }

        public CurrencyAmounts Convert(Domain.Model.CurrencyAmounts source, CurrencyAmounts destination, ResolutionContext context)
        {
            CurrencyAmounts currencyAmounts = null;

            if (source != null)
            {
                currencyAmounts = new CurrencyAmounts()
                {
                    Amounts = _mapper.Map<List<DenominationAmount>>(source.Amounts)
                };
            }

            return currencyAmounts;
        }
    }
}