using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.BL.Wallet;

namespace VendingMachine.Tests
{
    [TestClass]
    public class WalletTests
    {
        [TestMethod]
        public void WalletAddAndGetNotExistsCoinExeption()
        {
            List<ICoin> coins = new List<ICoin>();

            coins.Add(new Coin(1));
            coins.Add(new Coin(2));
            coins.Add(new Coin(5));
            coins.Add(new Coin(10));

            Wallet wallet = new Wallet(coins);

            Action notExistsCoinGet = () => wallet.AddCoin(13);
            Action notExistsCoinValueAdd = () => wallet.GetCoin(13);

            Assert.ThrowsException<Exception>(notExistsCoinGet);
            Assert.ThrowsException<Exception>(notExistsCoinValueAdd);
        }

        [TestMethod]
        public void WalletGetTooManyCoinsError()
        {
            List<ICoin> coins = new List<ICoin>();

            coins.Add(new Coin(1, 10));
            coins.Add(new Coin(2, 10));
            coins.Add(new Coin(5, 0));
            coins.Add(new Coin(10, 10));

            Wallet wallet = new Wallet(coins);


            Action emptyCoinsCountGet = () => wallet.GetCoin(5);

            Assert.ThrowsException<Exception>(emptyCoinsCountGet);
        }

        [TestMethod]
        public void WalletAddCoinSuccess()
        {
            Wallet wallet = new Wallet();

            int currentBalance = wallet.Balance;

            wallet.AddCoin(5);

            Assert.AreEqual(currentBalance + 5, wallet.Balance);
        }

        [TestMethod]
        public void WalletAddCashBackSuccess()
        {
            Wallet wallet = new Wallet();

            int currentBalance = wallet.Balance;

            var cashback = new List<ICoin>();
            cashback.Add(new Coin(1,2));
            cashback.Add(new Coin(2,1));
            cashback.Add(new Coin(5,1));
            cashback.Add(new Coin(10,1));

            wallet.AddCahsBack(cashback);

            Assert.AreEqual(currentBalance + cashback.Sum(c => c.CoinValue * c.CoinsCount), wallet.Balance);
        }

        [TestMethod]
        public void WalletAddCashBackError()
        {
            Wallet wallet = new Wallet();

            Action action = () => { wallet.AddCahsBack(null); };

            Assert.ThrowsException<Exception>(action);
        }
    }
}