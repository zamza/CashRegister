using AutoMapper;
using CashRegister.Web.Models.DTO;

namespace CashRegister.Web.Converters.Inbound
{
    public class CurrencyAmountsToCurrencyAmountsConverter : ConverterBase, ITypeConverter<CurrencyAmounts, Domain.Models.CurrencyAmounts>
    {
        public CurrencyAmountsToCurrencyAmountsConverter(IMapper mapper) : base(mapper)
        {
        }

        public Domain.Models.CurrencyAmounts Convert(CurrencyAmounts source, Domain.Models.CurrencyAmounts destination, ResolutionContext context)
        {
            Domain.Models.CurrencyAmounts currencyAmounts = null;

            if (source != null)
            {
                currencyAmounts = new Domain.Models.CurrencyAmounts()
                {
                    Amounts = _mapper.Map<Dictionary<Domain.Models.Currencies, int>>(source.Amounts)
                };
            }

            return currencyAmounts;
        }
    }
}
