using AutoMapper;
using CashRegister.Client.Models.DTO;

namespace CashRegister.Client.Converters.Inbound
{
    public class TransactionToTransactionConverter : Client.Converters.ConverterBase, ITypeConverter<Transaction, Domain.Model.Transaction>
    {
        public TransactionToTransactionConverter(IMapper mapper) : base(mapper)
        {
        }

        public Domain.Model.Transaction Convert(Transaction source, Domain.Model.Transaction destination, ResolutionContext context)
        {
            Domain.Model.Transaction transaction = null;

            var amountsPaid = new Domain.Model.CurrencyAmounts()
            {
                Amounts = _mapper.Map<Dictionary<Domain.Model.Currencies, int>>(source.AmountsPaid)
            };

            if (source != null)
            {
                transaction = new Domain.Model.Transaction
                {
                    AmountsPaid = amountsPaid,
                    Cost = source.Cost
                };
            }

            return transaction;
        }
    }
}
