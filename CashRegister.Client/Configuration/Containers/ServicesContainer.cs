using CashRegister.Data.Store;
using CashRegister.Domain.Abstract;
using CashRegister.Domain.Model;
using CashRegister.Services.Abstract;
using CashRegister.Services.Manager;

namespace CashRegister.Client.Configuration.Containers
{
    internal static class ServicesContainer
    {
        internal static void ConfigureDependencies(IServiceCollection services)
        {
            var amounts = new Dictionary<Currencies, int>()
            {
                {Currencies.Penny, 2000},
                {Currencies.Nickel, 0},
                {Currencies.Dime, 0},
                {Currencies.Quarter, 0},
                {Currencies.HalfDollar, 0},
                {Currencies.Dollar, 0},
                {Currencies.TwoDollars, 0},
                {Currencies.FiveDollars, 0},
                {Currencies.TenDollars, 0},
                {Currencies.TwentyDollars, 2}
            };
            var denominations = new Dictionary<Currencies, decimal>()
            {
                {Currencies.Penny, .01M},
                {Currencies.Nickel, .05M},
                {Currencies.Dime, .1M},
                {Currencies.Quarter, .25M},
                {Currencies.HalfDollar, .5M},
                {Currencies.Dollar, 1M},
                {Currencies.TwoDollars, 2M},
                {Currencies.FiveDollars, 5M},
                {Currencies.TenDollars, 10M},
                {Currencies.TwentyDollars, 20M}
            };
            var cashRegister = new Domain.Model.CashRegister(new CurrencyAmounts() { Amounts = amounts }, denominations);

            services.AddScoped<ICashRegister, Domain.Model.CashRegister>(x => cashRegister);
            services.AddScoped<CashRegisterRepository, CashRegisterRepository>();
            services.AddScoped<ICashRegisterManager, CashRegisterManager>();
        }
    }
}
