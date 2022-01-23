using AutoMapper;
using CashRegister.Client.Converters.Inbound;
using CashRegister.Web.Models.DTO;
using CurrenciesToCurrenciesConverter = CashRegister.Client.Converters.Outbound.CurrenciesToCurrenciesConverter;
using CurrencyAmountsToCurrencyAmountsConverter = CashRegister.Client.Converters.Outbound.CurrencyAmountsToCurrencyAmountsConverter;

namespace CashRegister.Client.Configuration
{
    public class WebMapperConfiguration : Profile
    {
        public WebMapperConfiguration()
        {
            //Inbound
            CreateMap<Transaction, Domain.Model.Transaction>().ConvertUsing<TransactionToTransactionConverter>();
            CreateMap<Currencies, Domain.Model.Currencies>().ConvertUsing<Converters.Inbound.CurrenciesToCurrenciesConverter>();
            CreateMap<CurrencyAmounts, Domain.Model.CurrencyAmounts>().ConvertUsing<Converters.Inbound.CurrencyAmountsToCurrencyAmountsConverter>();

            //Outbound
            CreateMap<Domain.Model.Currencies, Currencies>().ConvertUsing<CurrenciesToCurrenciesConverter>();
            CreateMap<Domain.Model.CurrencyAmounts, CurrencyAmounts>().ConvertUsing<CurrencyAmountsToCurrencyAmountsConverter >();
        }
    }
}
