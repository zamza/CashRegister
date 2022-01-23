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
            CreateMap<Transaction, Domain.Model.Transaction>().ConvertUsing<Inbound.TransactionToTransactionConverter>();
            CreateMap<Currencies, Domain.Model.Currencies>().ConvertUsing<Inbound.CurrenciesToCurrenciesConverter>();
            CreateMap<CurrencyAmounts, Domain.Model.CurrencyAmounts>().ConvertUsing<Inbound.CurrencyAmountsToCurrencyAmountsConverter>();

            //Outbound
            CreateMap<Domain.Model.Currencies, Currencies>().ConvertUsing<Outbound.CurrenciesToCurrenciesConverter>();
            CreateMap<Domain.Model.CurrencyAmounts, CurrencyAmounts>().ConvertUsing<Outbound.CurrencyAmountsToCurrencyAmountsConverter >();
        }
    }
}
