using AutoMapper;
using CashRegister.Web.Models.DTO;

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

            if (source != null)
            {
                transaction = new Domain.Model.Transaction
                {
                    AmountsPaid = _mapper.Map<Domain.Model.CurrencyAmounts>(source.AmountsPaid),
                    Cost = source.Cost
                };
            }

            return transaction;
        }
    }
}
