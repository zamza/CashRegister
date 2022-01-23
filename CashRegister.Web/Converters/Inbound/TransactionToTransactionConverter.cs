using AutoMapper;
using CashRegister.Web.Models.DTO;

namespace CashRegister.Web.Converters.Inbound
{
    public class TransactionToTransactionConverter : ConverterBase, ITypeConverter<Transaction, Domain.Models.Transaction>
    {
        public TransactionToTransactionConverter(IMapper mapper) : base(mapper)
        {
        }

        public Domain.Models.Transaction Convert(Transaction source, Domain.Models.Transaction destination, ResolutionContext context)
        {
            Domain.Models.Transaction transaction = null;

            if (source != null)
            {
                transaction = new Domain.Models.Transaction
                {
                    AmountsPaid = _mapper.Map<Domain.Models.CurrencyAmounts>(source.AmountsPaid),
                    Cost = source.Cost
                };
            }

            return transaction;
        }
    }
}
