using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CashRegister.Data.Model;

namespace CashRegister.Data.Converters.Outbound
{
    public class CurrenciesToCurrenciesConverter : ITypeConverter<Currencies, Domain.Model.Currencies>
    {
        public Domain.Model.Currencies Convert(Currencies source, Domain.Model.Currencies destination, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
