using AutoMapper;
using CashRegister.Web.Models.DTO;

namespace CashRegister.Web.Converters.Inbound
{
    public class CurrenciesToCurrenciesConverter : ITypeConverter<Currencies, Domain.Models.Currencies>
    {
        public Domain.Models.Currencies Convert(Currencies source, Domain.Models.Currencies destination, ResolutionContext context)
        {
            Domain.Models.Currencies currency;

            switch (source)
            {
                case (Currencies.Dime):
                    currency = Domain.Models.Currencies.Dime;
                    break;
                case (Currencies.Dollar):
                    currency = Domain.Models.Currencies.Dollar;
                    break;
                case (Currencies.FiveDollars):
                    currency = Domain.Models.Currencies.FiveDollars;
                    break;
                case (Currencies.HalfDollar):
                    currency = Domain.Models.Currencies.HalfDollar;
                    break;
                case (Currencies.Nickel):
                    currency = Domain.Models.Currencies.Nickel;
                    break;
                case (Currencies.Penny):
                    currency = Domain.Models.Currencies.Penny;
                    break;
                case (Currencies.Quarter):
                    currency = Domain.Models.Currencies.Quarter;
                    break;
                case (Currencies.TenDollars):
                    currency = Domain.Models.Currencies.TenDollars;
                    break;
                case (Currencies.TwentyDollars):
                    currency = Domain.Models.Currencies.TwentyDollars;
                    break;
                case (Currencies.TwoDollars):
                    currency = Domain.Models.Currencies.TwoDollars;
                    break;
                default: throw new ArgumentException("Unrecognized Currency");
            }

            return currency;
        }
    }
}
