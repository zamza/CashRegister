using AutoMapper;
using CashRegister.Client.Converters.Inbound;
using CashRegister.Client.Converters.Outbound;
using CashRegister.Client.Models.DTO;
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
            CreateMap<DenominationAmount, KeyValuePair<Domain.Model.Currencies, int>>()
                .ConvertUsing<Converters.Inbound.DenominationAmountToKeyValuePairConverter>();

            //Outbound
            CreateMap<Domain.Model.Currencies, Currencies>().ConvertUsing<CurrenciesToCurrenciesConverter>();
            CreateMap<Domain.Model.CurrencyAmounts, CurrencyAmounts>().ConvertUsing<CurrencyAmountsToCurrencyAmountsConverter >();
            CreateMap<KeyValuePair<Domain.Model.Currencies, int>, DenominationAmount>().ConvertUsing<KeyValuePairToDenominationAmountConverter>();
            CreateMap<KeyValuePair<Domain.Model.Currencies, decimal>, DenominationValue>().ConvertUsing<KeyValuePairToDenominationValueConverter>();
        }
    }
}
