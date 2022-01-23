using AutoMapper;
using CashRegister.Web.Models.DTO;

namespace CashRegister.Web.Converters.Outbound
{
    public class CurrencyAmountsToCurrencyAmountsConverter : ConverterBase, ITypeConverter<Domain.Models.CurrencyAmounts, CurrencyAmounts>
    {
        public CurrencyAmountsToCurrencyAmountsConverter(IMapper mapper) : base(mapper)
        {
        }

        public CurrencyAmounts Convert(Domain.Models.CurrencyAmounts source, CurrencyAmounts destination, ResolutionContext context)
        {
            CurrencyAmounts currencyAmounts = null;

            if (source != null)
            {
                currencyAmounts = new CurrencyAmounts()
                {
                    Amounts = _mapper.Map<Dictionary<Currencies, int>>(source.Amounts)
                };
            }

            return currencyAmounts;
        }
    }
}