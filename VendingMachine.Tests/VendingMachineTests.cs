using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.BL.VendingMachine;
using VendingMachine.BL.Wallet;

namespace VendingMachine.Tests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void CoinAceptorBalance()
        {
            CoinAceptor aceptor = new CoinAceptor();

            int coinsCount = 10;
            int coinsValue = 5;

            aceptor.AddCoin(new Coin(coinsValue, coinsCount));

            Assert.AreEqual(aceptor.Balance, coinsCount * coinsValue);
        }

        [TestMethod]
        public void GetNotExistsBeverage()
        {
            List<IRepositoryItem> items = new List<IRepositoryItem>();

            items.Add(new RepositoryItem(new Coffee(), 10));
            items.Add(new RepositoryItem(new CoffeeWithMilk(), 10));
            items.Add(new RepositoryItem(new Juice(), 10));
            items.Add(new RepositoryItem(new Tea(), 10));

            IBeverageRepository repositiry = new BeverageRepository(items);

            Assert.ThrowsException<Exception>(() => repositiry.GetBeverage("Молоко"));
        }

        [TestMethod]
        public void GetBeverageSuccess()
        {
            List<IRepositoryItem> items = new List<IRepositoryItem>();

            var repository1 = new RepositoryItem(new Coffee(), 10);
            var repository2 = new RepositoryItem(new CoffeeWithMilk(), 10);
            var repository3 = new RepositoryItem(new Juice(), 10);
            var repository4 = new RepositoryItem(new Tea(), 10);

            items.Add(repository1);
            items.Add(repository2);
            items.Add(repository3);
            items.Add(repository4);

            IBeverageRepository repositiry = new BeverageRepository(items);

            Assert.AreEqual(repositiry.GetBeverage("Кофе"), repository1);
            Assert.AreEqual(repositiry.GetBeverage("Кофе с молоком"), repository2);
            Assert.AreEqual(repositiry.GetBeverage("Сок"), repository3);
            Assert.AreEqual(repositiry.GetBeverage("Чай"), repository4);
        }

        [TestMethod]
        public void VendingMachineWalletCashBackSuccess_2_2_2()
        {
            List<ICoin> coins = new List<ICoin>();

            coins.Add(new Coin(10, 10));
            coins.Add(new Coin(5, 10));
            coins.Add(new Coin(2, 10));

            IVendingMachineWallet wallet = new VendingMachineWallet(coins);

            wallet.AddCoinsFromAcceptor(4, new List<ICoin>() {new Coin(10, 1)});

            var expectedCollection = wallet.GetCashBack().ToList();

            expectedCollection = expectedCollection.OrderBy(c => c.CoinValue).ToList();

            List<ICoin> actual = new List<ICoin>();

            actual.Add(new Coin(2, 1));
            actual.Add(new Coin(2, 1));
            actual.Add(new Coin(2, 1));

            actual = actual.OrderBy(c => c.CoinValue).ToList();

            Assert.IsTrue(expectedCollection.SequenceEqual(actual, new CoinsComparer()));
        }

        [TestMethod]
        public void VendingMachineWalletCashBackSuccess_5_2_2_2()
        {
            List<ICoin> coins = new List<ICoin>();

            coins.Add(new Coin(10, 10));
            coins.Add(new Coin(5, 10));
            coins.Add(new Coin(2, 10));

            IVendingMachineWallet wallet = new VendingMachineWallet(coins);

            wallet.AddCoinsFromAcceptor(9, new List<ICoin>() { new Coin(10, 1), new Coin(10, 1) });

            var expectedCollection = wallet.GetCashBack().ToList();

            expectedCollection = expectedCollection.OrderBy(c => c.CoinValue).ToList();

            List<ICoin> actual = new List<ICoin>();

            actual.Add(new Coin(5, 1));
            actual.Add(new Coin(2, 1));
            actual.Add(new Coin(2, 1));
            actual.Add(new Coin(2, 1));

            actual = actual.OrderBy(c => c.CoinValue).ToList();

            Assert.IsTrue(expectedCollection.SequenceEqual(actual, new CoinsComparer()));
        }

        [TestMethod]
        public void VendingMachineWalletCashBackSuccess_10_10_5_5_1_1()
        {
            List<ICoin> coins = new List<ICoin>();

            coins.Add(new Coin(10, 2));
            coins.Add(new Coin(5, 10));
            coins.Add(new Coin(1, 10));

            IVendingMachineWallet wallet = new VendingMachineWallet(coins);

            wallet.AddCoinsFromAcceptor(8, new List<ICoin>()
            {
                new Coin(5, 1),
                new Coin(5, 1),
                new Coin(5, 1),
                new Coin(5, 1),
                new Coin(5, 1),
                new Coin(5, 1),
                new Coin(5, 1),
                new Coin(5, 1)
            });

            var expectedCollection = wallet.GetCashBack().ToList();

            expectedCollection = expectedCollection.OrderBy(c => c.CoinValue).ToList();

            List<ICoin> actual = new List<ICoin>();

            actual.Add(new Coin(10, 1));
            actual.Add(new Coin(10, 1));
            actual.Add(new Coin(5, 1));
            actual.Add(new Coin(5, 1));
            actual.Add(new Coin(1, 1));
            actual.Add(new Coin(1, 1));

            actual = actual.OrderBy(c => c.CoinValue).ToList();

            Assert.IsTrue(expectedCollection.SequenceEqual(actual, new CoinsComparer()));
        }

        [TestMethod]
        public void VendingMachineWalletCashBackError()
        {
            List<ICoin> coins = new List<ICoin>();

            coins.Add(new Coin(10, 10));
            coins.Add(new Coin(2, 10));

            IVendingMachineWallet wallet = new VendingMachineWallet(coins);

            wallet.AddCoinsFromAcceptor(9, new List<ICoin>()
            {
                new Coin(2, 1),
            });

            var expectedCollection = wallet.GetCashBack();

            Assert.IsTrue(expectedCollection == null);
        }
    }
}
