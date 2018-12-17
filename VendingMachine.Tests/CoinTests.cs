using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.BL.Wallet;

namespace VendingMachine.Tests
{
    [TestClass]
    public class CoinTests
    {
        private int coinValue = 10;
        private int coinCount = 5;

        [TestMethod]
        public void GetCoinSuccess()
        {
            ICoin coin = new Coin(coinValue, coinCount);

            coin.GetCoin();

            int result = coin.CoinsCount;

            Assert.AreEqual(result, coinCount - 1);
        }

        [TestMethod]
        public void GetCoinError()
        {
            ICoin coin = new Coin(coinValue, 0);
            
            Action action = () => { coin.GetCoin(); };

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void CanGetCoinPropertyTrueSuccess()
        {
            ICoin coin = new Coin(coinValue, coinCount);

            coin.GetCoin();

            Assert.AreEqual(coin.CanGetCoin, true);
        }

        [TestMethod]
        public void CanGetCoinPropertyFalseSuccess()
        {
            ICoin coin = new Coin(coinValue, 1);

            coin.GetCoin();

            Assert.AreEqual(coin.CanGetCoin, false);
        }
    }
}
