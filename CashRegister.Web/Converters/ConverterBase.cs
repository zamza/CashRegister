using AutoMapper;

namespace CashRegister.Web.Converters
{
    public class ConverterBase
    {
        protected IMapper _mapper;

        public ConverterBase(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
