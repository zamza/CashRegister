using AutoMapper;
using CashRegister.Web.Models.DTO;

namespace CashRegister.Client.Converters.Outbound
{
    public class CurrenciesToCurrenciesConverter : ITypeConverter<Domain.Model.Currencies, Currencies>
    {
        public Currencies Convert(Domain.Model.Currencies source, Currencies destination, ResolutionContext context)
        {
            Currencies currency;

            switch (source)
            {
                case (Domain.Model.Currencies.Dime):
                    currency = Currencies.Dime;
                    break;
                case (Domain.Model.Currencies.Dollar):
                    currency = Currencies.Dollar;
                    break;
                case (Domain.Model.Currencies.FiveDollars):
                    currency = Currencies.FiveDollars;
                    break;
                case (Domain.Model.Currencies.HalfDollar):
                    currency = Currencies.HalfDollar;
                    break;
                case (Domain.Model.Currencies.Nickel):
                    currency = Currencies.Nickel;
                    break;
                case (Domain.Model.Currencies.Penny):
                    currency = Currencies.Penny;
                    break;
                case (Domain.Model.Currencies.Quarter):
                    currency = Currencies.Quarter;
                    break;
                case (Domain.Model.Currencies.TenDollars):
                    currency = Currencies.TenDollars;
                    break;
                case (Domain.Model.Currencies.TwentyDollars):
                    currency = Currencies.TwentyDollars;
                    break;
                case (Domain.Model.Currencies.TwoDollars):
                    currency = Currencies.TwoDollars;
                    break;
                default: throw new ArgumentException("Unrecognized Currency");
            }

            return currency;
        }
    }
}