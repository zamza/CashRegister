using AutoMapper;
using CashRegister.Web.Models.DTO;

namespace CashRegister.Client.Converters.Inbound
{
    public class CurrenciesToCurrenciesConverter : ITypeConverter<Currencies, Domain.Model.Currencies>
    {
        public Domain.Model.Currencies Convert(Currencies source, Domain.Model.Currencies destination, ResolutionContext context)
        {
            Domain.Model.Currencies currency;

            switch (source)
            {
                case (Currencies.Dime):
                    currency = Domain.Model.Currencies.Dime;
                    break;
                case (Currencies.Dollar):
                    currency = Domain.Model.Currencies.Dollar;
                    break;
                case (Currencies.FiveDollars):
                    currency = Domain.Model.Currencies.FiveDollars;
                    break;
                case (Currencies.HalfDollar):
                    currency = Domain.Model.Currencies.HalfDollar;
                    break;
                case (Currencies.Nickel):
                    currency = Domain.Model.Currencies.Nickel;
                    break;
                case (Currencies.Penny):
                    currency = Domain.Model.Currencies.Penny;
                    break;
                case (Currencies.Quarter):
                    currency = Domain.Model.Currencies.Quarter;
                    break;
                case (Currencies.TenDollars):
                    currency = Domain.Model.Currencies.TenDollars;
                    break;
                case (Currencies.TwentyDollars):
                    currency = Domain.Model.Currencies.TwentyDollars;
                    break;
                case (Currencies.TwoDollars):
                    currency = Domain.Model.Currencies.TwoDollars;
                    break;
                default: throw new ArgumentException("Unrecognized Currency");
            }

            return currency;
        }
    }
}
