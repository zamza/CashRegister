using AutoMapper;
using CashRegister.Data.Model;

namespace CashRegister.Data.Converters
{
    public class DataMapperConfiguration : Profile
    {
        public DataMapperConfiguration()
        {
            //Inbound
            CreateMap<Domain.Model.Transaction, Transaction>().ConvertUsing<Inbound.TransactionToTransactionConverter>();
            CreateMap<Domain.Model.Currencies, Currencies>().ConvertUsing<Inbound.CurrenciesToCurrenciesConverter>();
            CreateMap<Domain.Model.CurrencyAmounts, CurrencyAmounts>().ConvertUsing<Inbound.CurrencyAmountsToCurrencyAmountsConverter>();

            //Outbound
            CreateMap<Transaction, Domain.Model.Transaction>().ConvertUsing<Outbound.TransactionToTransactionConverter>();
            CreateMap<Currencies, Domain.Model.Currencies>().ConvertUsing<Outbound.CurrenciesToCurrenciesConverter>();
            CreateMap<CurrencyAmounts, Domain.Model.CurrencyAmounts>().ConvertUsing<Outbound.CurrencyAmountsToCurrencyAmountsConverter>();

        }
    }
}
