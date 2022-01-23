using System;
using System.Collections.Generic;
using System.Linq;
using CashRegister.Domain.Models;
using NUnit.Framework;

namespace CashRegister.Test
{
    [TestFixture]
    public class CashRegisterTests
    {
        private Domain.Models.CashRegister _cashRegister;
        private Dictionary<Currencies, decimal> _denominations;
        private Domain.Models.Transaction _transaction;

        [SetUp]
        public void Setup()
        {

            var amounts = new Dictionary<Currencies, int>()
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
            _cashRegister = new Domain.Models.CashRegister(amounts, _denominations);

            var transaction = new Transaction()
            {
                AmountsPaid = new Dictionary<Currencies, int>()
                {
                    {Currencies.TwentyDollars, 3},
                },
                Cost = 42.23M
            };
            _transaction = transaction;
        }

        [Test]
        public void AddCash_UpdateDenominationsUpdatesOnlyThoseDenominations()
        {
            // Get a copy of amounts
            var originalAmounts = _cashRegister.GetAmounts().ToDictionary(entry => entry.Key,
                entry => entry.Value);

            var deposit = new Dictionary<Currencies, int>()
            {
                {Currencies.Dollar, 500},
            };
            _cashRegister.AddCash(deposit);

            var amounts = _cashRegister.GetAmounts();

            var unchangedValuesRemainUnchanged = originalAmounts.Where(x => x.Key != Currencies.Dollar)
                .All(x => x.Value == amounts[x.Key]);

            Assert.IsTrue(unchangedValuesRemainUnchanged);
            Assert.IsTrue(amounts[Currencies.Dollar] == 1000);
        }

        [Test]
        public void AddCash_ThrowsExceptionsWhenUsingUnrecognizedCurrency()
        {
            var deposit = new Dictionary<Currencies, int>()
            {
                {Currencies.GoldDoubloons, 500},
            };
            Assert.Throws<InvalidOperationException>(() => _cashRegister.AddCash(deposit));
        }

        [Test]
        public void AddCash_NegativePaymentThrowsException()
        {
            var deposit = new Dictionary<Currencies, int>()
            {
                {Currencies.Dollar, -10}
            };
            Assert.Throws<InvalidOperationException>(() => _cashRegister.AddCash(deposit));
        }

        [Test]
        public void TakePayment_ReturnsExpectedAmounts()
        {
            var transaction = new Transaction()
            {
                AmountsPaid = new Dictionary<Currencies, int>()
                {
                    {Currencies.TwentyDollars, 3},
                },
                Cost = 42.23M
            };
            var amountReturned = _cashRegister.TakePayment(transaction);
            Assert.IsTrue(
                amountReturned[Currencies.TenDollars] == 1
                    && amountReturned[Currencies.FiveDollars] == 1
                    && amountReturned[Currencies.TwoDollars] == 1
                    && amountReturned[Currencies.HalfDollar] == 1
                    && amountReturned[Currencies.Quarter] == 1
                    && amountReturned[Currencies.Penny] == 2
                );
        }

        [Test]
        public void TakePayment_AddSpentAmountsToRegisterAmountTotals()
        {
            _cashRegister.TakePayment(_transaction);
            var amounts = _cashRegister.GetAmounts();

            Assert.IsTrue(
                amounts[Currencies.TwentyDollars] == 103
            );
        }

        [Test]
        public void TakePayment_SubtractChangeAmountsFromRegisterAmountTotals()
        {
            _cashRegister.TakePayment(_transaction);
            var amounts = _cashRegister.GetAmounts();

            Assert.IsTrue(
                amounts[Currencies.TenDollars] == 199
                && amounts[Currencies.FiveDollars] == 249
                && amounts[Currencies.TwoDollars] == 19
                && amounts[Currencies.HalfDollar] == 24
                && amounts[Currencies.Quarter] == 249
                && amounts[Currencies.Penny] == 498
            );
        }

        [Test]
        public void TakePayment_ThrowsExceptionsWhenUsingUnrecognizedCurrency()
        {
            var transaction = new Transaction()
            {
                AmountsPaid = new Dictionary<Currencies, int>()
                {
                    {Currencies.GoldDoubloons, 1}
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
                AmountsPaid = new Dictionary<Currencies, int>()
                {
                    {Currencies.Dollar, -10}
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
                AmountsPaid = new Dictionary<Currencies, int>()
                {
                    {Currencies.TwentyDollars, 1}
                },
                Cost = 30
            };

            Assert.Throws<InvalidOperationException>(() => _cashRegister.TakePayment(transaction));
        }

        [Test]
        public void TakePayment_ThrowsExceptionWhenCantMakeChange()
        {
            var amounts = new Dictionary<Currencies, int>()
            {
                {Currencies.Dollar, 10},
            };
            var cashRegister = new Domain.Models.CashRegister(amounts, _denominations);

            var transaction = new Transaction()
            {
                AmountsPaid = new Dictionary<Currencies, int>()
                {
                    {Currencies.TwentyDollars, 1}
                },
                Cost = 19.99M
            };

            Assert.Throws<InvalidOperationException>(() => cashRegister.TakePayment(transaction));
        }
    }
}