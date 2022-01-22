using AutoMapper;
using Outbound = CashRegister.Web.Converters.Outbound;
using Inbound = CashRegister.Web.Converters.Inbound;
using CashRegister.Web.Models.DTO;

namespace CashRegister.Web.Configuration
{
    public class WebMapperConfiguration : Profile
    {
        public WebMapperConfiguration()
        {
            //Inbound
            CreateMap<Transaction, Domain.Models.Transaction>().ConvertUsing<Inbound.TransactionToTransactionConverter>();
            CreateMap<Currencies, Domain.Models.Currencies>().ConvertUsing<Inbound.CurrenciesToCurrenciesConverter>();

            //Outbound
            CreateMap<Domain.Models.Currencies, Currencies>().ConvertUsing<Outbound.CurrenciesToCurrenciesConverter>();
        }
    }
}
