using AutoMapper;
using CashRegister.Client.Models.DTO;

namespace CashRegister.Client.Converters.Outbound
{
    public class KeyValuePairToDenominationAmountConverter : ConverterBase, ITypeConverter<KeyValuePair<Domain.Model.Currencies, int>, DenominationAmount>
    {
        public KeyValuePairToDenominationAmountConverter(IMapper mapper) : base(mapper)
        {
        }

        public DenominationAmount Convert(KeyValuePair<Domain.Model.Currencies, int> source, DenominationAmount destination, ResolutionContext context)
        {
            return new DenominationAmount()
            {
                Denomination = _mapper.Map<Currencies>(source.Key),
                Amount = source.Value,
            };
        }
    }
}
