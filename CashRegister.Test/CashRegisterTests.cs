using System;
using System.Collections.Generic;
using System.Linq;
using CashRegister.Domain.Model;
using NUnit.Framework;

namespace CashRegister.Test
{
    [TestFixture]
    public class CashRegisterTests
    {
        private Domain.Model.CashRegister _cashRegister;
        private Dictionary<Currencies, decimal> _denominations;
        private Transaction _transaction;

        [SetUp]
        public void Setup()
        {
            var currencyAmounts = new CurrencyAmounts()
            {
                Amounts = new Dictionary<Currencies, int>()
                {
                    {Currencies.Penny, 500},
                    {Currencies.Nickel, 200},
                    {Currencies.Dime, 500},
                    {Currencies.Quarter, 250},
                    {Currencies.HalfDollar, 25},
                    {Currencies.Dollar, 500},
                    {Currencies.TwoDollars, 20},
                    {Currencies.FiveDollars, 250},
                    {Currencies.TenDollars, 200},
                    {Currencies.TwentyDollars, 100}
                }
            };

            _denominations = new Dictionary<Currencies, decimal>()
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
            _cashRegister = new Domain.Model.CashRegister(currencyAmounts, _denominations);

            var transaction = new Transaction()
            {
                AmountsPaid = new CurrencyAmounts() {
                    Amounts = new Dictionary<Currencies, int>()
                    {
                        {Currencies.TwentyDollars, 3},
                    }
                },
                Cost = 42.23M
            };
            _transaction = transaction;
        }

        [Test]
        public void AddCash_UpdateDenominationsUpdatesOnlyThoseDenominations()
        {
            // Get a copy of amounts
            var originalAmounts = _cashRegister.GetAmounts().Amounts.ToDictionary(entry => entry.Key,
                entry => entry.Value);

            var deposit = new CurrencyAmounts()
            {
                Amounts = new Dictionary<Currencies, int>()
                {
                    {Currencies.Dollar, 500},
                }
            };
            _cashRegister.AddCash(deposit);

            var amounts = _cashRegister.GetAmounts();

            var unchangedValuesRemainUnchanged = originalAmounts.Where(x => x.Key != Currencies.Dollar)
                .All(x => x.Value == amounts.Amounts[x.Key]);

            Assert.IsTrue(unchangedValuesRemainUnchanged);
            Assert.IsTrue(amounts.Amounts[Currencies.Dollar] == 1000);
        }

        [Test]
        public void AddCash_ThrowsExceptionsWhenUsingUnrecognizedCurrency()
        {
            var deposit = new CurrencyAmounts()
            {
                Amounts = new Dictionary<Currencies, int>()
                {
                    {Currencies.GoldDoubloons, 500},
                }
            };
            Assert.Throws<InvalidOperationException>(() => _cashRegister.AddCash(deposit));
        }

        [Test]
        public void AddCash_NegativePaymentThrowsException()
        {
            var deposit = new CurrencyAmounts()
            {
                Amounts = new Dictionary<Currencies, int>()
                {
                    {Currencies.Dollar, -10}
                }
            };
            Assert.Throws<InvalidOperationException>(() => _cashRegister.AddCash(deposit));
        }

        [Test]
        public void TakePayment_ReturnsExpectedAmounts()
        {
            var transaction = new Transaction()
            {
                
                AmountsPaid = new CurrencyAmounts()
                {
                    Amounts = new Dictionary<Currencies, int>()
                    {
                        {Currencies.TwentyDollars, 3},
                    },
                },
                Cost = 42.23M
            };
            var amountReturned = _cashRegister.TakePayment(transaction);
            Assert.IsTrue(
                amountReturned.Amounts[Currencies.TenDollars] == 1
                    && amountReturned.Amounts[Currencies.FiveDollars] == 1
                    && amountReturned.Amounts[Currencies.TwoDollars] == 1
                    && amountReturned.Amounts[Currencies.HalfDollar] == 1
                    && amountReturned.Amounts[Currencies.Quarter] == 1
                    && amountReturned.Amounts[Currencies.Penny] == 2
                );
        }

        [Test]
        public void TakePayment_AddSpentAmountsToRegisterAmountTotals()
        {
            _cashRegister.TakePayment(_transaction);
            var amounts = _cashRegister.GetAmounts();

            Assert.IsTrue(
                amounts.Amounts[Currencies.TwentyDollars] == 103
            );
        }

        [Test]
        public void TakePayment_SubtractChangeAmountsFromRegisterAmountTotals()
        {
            _cashRegister.TakePayment(_transaction);
            var amounts = _cashRegister.GetAmounts();

            Assert.IsTrue(
                amounts.Amounts[Currencies.TenDollars] == 199
                && amounts.Amounts[Currencies.FiveDollars] == 249
                && amounts.Amounts[Currencies.TwoDollars] == 19
                && amounts.Amounts[Currencies.HalfDollar] == 24
                && amounts.Amounts[Currencies.Quarter] == 249
                && amounts.Amounts[Currencies.Penny] == 498
            );
        }

        [Test]
        public void TakePayment_ThrowsExceptionsWhenUsingUnrecognizedCurrency()
        {
            var transaction = new Transaction()
            {
                AmountsPaid = new CurrencyAmounts()
                {
                    Amounts = new Dictionary<Currencies, int>()
                        {
                            {Currencies.GoldDoubloons, 1}
                        },
                    },
                Cost = 5
            };

            Assert.Throws<InvalidOperationException>(() => _cashRegister.TakePayment(transaction));
        }

        [Test]
        public void TakePayment_NegativePaymentThrowsException()
        {
            var transaction = new Transaction()
            {
                AmountsPaid = new CurrencyAmounts()
                {
                    Amounts = new Dictionary<Currencies, int>()
                    {
                        {Currencies.Dollar, -10}
                    },
                },
                Cost = 5
            };

            Assert.Throws<InvalidOperationException>(() => _cashRegister.TakePayment(transaction));
        }

        [Test]
        public void TakePayment_ThrowsExceptionWhenAmountsDontCoverCost()
        {
            var transaction = new Transaction()
            {
                AmountsPaid = new CurrencyAmounts()
                {
                    Amounts = new Dictionary<Currencies, int>()
                    {
                        {Currencies.TwentyDollars, 1}
                    }
                },
                Cost = 30
            };

            Assert.Throws<InvalidOperationException>(() => _cashRegister.TakePayment(transaction));
        }

        [Test]
        public void TakePayment_ThrowsExceptionWhenCantMakeChange()
        {
            var amounts = new CurrencyAmounts()
            {
                Amounts = new Dictionary<Currencies, int>()
                {
                    {Currencies.Dollar, 10},
                }
            };
            var cashRegister = new Domain.Model.CashRegister(amounts, _denominations);

            var transaction = new Transaction()
            {
                AmountsPaid = new CurrencyAmounts()
                {
                    Amounts = new Dictionary<Currencies, int>()
                    {
                        {Currencies.TwentyDollars, 1}
                    },
                },
                Cost = 19.99M
            };

            Assert.Throws<InvalidOperationException>(() => cashRegister.TakePayment(transaction));
        }
    }
}