using AutoMapper;
using CashRegister.Client.Models.DTO;

namespace CashRegister.Client.Converters.Outbound
{
    public class KeyValuePairToDenominationValueConverter : ConverterBase, ITypeConverter<KeyValuePair<Domain.Model.Currencies, decimal>, DenominationValue>
    {
        public KeyValuePairToDenominationValueConverter(IMapper mapper) : base(mapper)
        {
        }

        public DenominationValue Convert(KeyValuePair<Domain.Model.Currencies, decimal> source, DenominationValue destination, ResolutionContext context)
        {
            return new DenominationValue()
            {
                Name = _mapper.Map<Currencies>(source.Key),
                Value = source.Value,
            };
        }
    }
}