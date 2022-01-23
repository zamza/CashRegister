using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CashRegister.Data.Model;

namespace CashRegister.Data.Converters.Inbound
{
    public class TransactionToTransactionConverter : ITypeConverter<Domain.Model.Transaction, Transaction>
    {
        public Transaction Convert(Domain.Model.Transaction source, Transaction destination, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
