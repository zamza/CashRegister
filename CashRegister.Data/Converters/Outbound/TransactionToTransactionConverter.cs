using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CashRegister.Data.Model;

namespace CashRegister.Data.Converters.Outbound
{
    public class TransactionToTransactionConverter : ITypeConverter<Transaction, Domain.Model.Transaction>
    {
        public Domain.Model.Transaction Convert(Transaction source, Domain.Model.Transaction destination, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
