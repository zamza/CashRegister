using AutoMapper;
using CashRegister.Web.Models.DTO;

namespace CashRegister.Web.Converters.Outbound
{
    public class CurrenciesToCurrenciesConverter : ITypeConverter<Domain.Models.Currencies, Currencies>
    {
        public Currencies Convert(Domain.Models.Currencies source, Currencies destination, ResolutionContext context)
        {
            Currencies currency;

            switch (source)
            {
                case (Domain.Models.Currencies.Dime):
                    currency = Currencies.Dime;
                    break;
                case (Domain.Models.Currencies.Dollar):
                    currency = Currencies.Dollar;
                    break;
                case (Domain.Models.Currencies.FiveDollars):
                    currency = Currencies.FiveDollars;
                    break;
                case (Domain.Models.Currencies.HalfDollar):
                    currency = Currencies.HalfDollar;
                    break;
                case (Domain.Models.Currencies.Nickel):
                    currency = Currencies.Nickel;
                    break;
                case (Domain.Models.Currencies.Penny):
                    currency = Currencies.Penny;
                    break;
                case (Domain.Models.Currencies.Quarter):
                    currency = Currencies.Quarter;
                    break;
                case (Domain.Models.Currencies.TenDollars):
                    currency = Currencies.TenDollars;
                    break;
                case (Domain.Models.Currencies.TwentyDollars):
                    currency = Currencies.TwentyDollars;
                    break;
                case (Domain.Models.Currencies.TwoDollars):
                    currency = Currencies.TwoDollars;
                    break;
                default: throw new ArgumentException("Unrecognized Currency");
            }

            return currency;
        }
    }
}