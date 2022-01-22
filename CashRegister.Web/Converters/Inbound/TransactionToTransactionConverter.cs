using AutoMapper;
using CashRegister.Web.Models.DTO;

namespace CashRegister.Web.Converters.Inbound
{
    public class TransactionToTransactionConverter : ITypeConverter<Transaction, Domain.Models.Transaction>
    {
        private readonly IMapper _mapper;

        public TransactionToTransactionConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Domain.Models.Transaction Convert(Transaction source, Domain.Models.Transaction destination, ResolutionContext context)
        {
            Domain.Models.Transaction transaction = null;

            if (source != null)
            {
                transaction = new Domain.Models.Transaction
                {
                    AmountsPaid = _mapper.Map<Dictionary<Domain.Models.Currencies, int>>(source.AmountsPaid),
                    Cost = source.Cost
                };
            }

            return transaction;
        }
    }
}
