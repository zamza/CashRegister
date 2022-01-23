using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CashRegister.Data.Model;

namespace CashRegister.Data.Converters.Outbound
{
    public class CurrencyAmountsToCurrencyAmountsConverter : ITypeConverter<CurrencyAmounts, Domain.Model.CurrencyAmounts>
    {
        public Domain.Model.CurrencyAmounts Convert(CurrencyAmounts source, Domain.Model.CurrencyAmounts destination, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
