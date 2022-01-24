using AutoMapper;
using CashRegister.Client.Models.DTO;
using Currencies = CashRegister.Domain.Model.Currencies;

namespace CashRegister.Client.Converters.Inbound
{
    public class DenominationAmountToKeyValuePairConverter : ConverterBase, ITypeConverter<DenominationAmount, KeyValuePair<Domain.Model.Currencies, int>>
    {
        public DenominationAmountToKeyValuePairConverter(IMapper mapper) : base(mapper)
        {
        }

        public KeyValuePair<Currencies, int> Convert(DenominationAmount source, KeyValuePair<Currencies, int> destination, ResolutionContext context)
        {
            return new KeyValuePair<Currencies, int>(_mapper.Map<Domain.Model.Currencies>(source.Denomination), source.Amount);
        }
    }
}
