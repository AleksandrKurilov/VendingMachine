using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using VendingMachine.BL.Annotations;

namespace VendingMachine.BL.Wallet
{
    public class Wallet : IWallet, INotifyPropertyChanged
    {
        public Wallet(List<ICoin>  coins)
        {
            if(coins == null || coins.Count < 1)
                throw new ArgumentNullException();

            this.coins = coins;
        }

        public Wallet()
        {
            coins = new List<ICoin>()
            {
                new Coin(1, 10),
                new Coin(2, 30),
                new Coin(5, 20),
                new Coin(10, 15),
            };
        }

        private readonly List<ICoin> coins;

        public ICollection<ICoin> Coins => coins.AsReadOnly();

        public int Balance
        {
            get
            {
                
                if (coins == null || coins.Count < 1) return 0;

                return coins.Sum(c => c.CoinValue * c.CoinsCount);
            }
        }

        public ICoin GetCoin(int coinValue)
        {
            if(!coins.Any(c => c.CoinValue == coinValue))
                throw new Exception("В кошельке нет монет такого достоинства");

            if(coins.Single(c => c.CoinValue == coinValue).CoinsCount < 1)
                throw new Exception("В кошельке монет такого достоинства 0");

            coins.Single(c => c.CoinValue == coinValue).GetCoin();

            OnPropertyChanged(nameof(Balance));
            OnPropertyChanged(nameof(coins));

            return new Coin(coinValue, 1);
        }

        public void AddCoin(int coinValue)
        {
            if (!coins.Any(c => c.CoinValue == coinValue))
                throw new Exception("В кошельке нет монет такого достоинства");

            coins.Single(c => c.CoinValue == coinValue).AddCoin();

            OnPropertyChanged(nameof(Balance));
            OnPropertyChanged(nameof(coins));
        }

        public void AddCahsBack(ICollection<ICoin> cashBack)
        {
            if(cashBack == null || cashBack.Count < 1)
                                throw new Exception("Нет монет для добавления");
            try
            {
                foreach(var coin in cashBack)
                {
                    coins.Single(c => c.CoinValue == coin.CoinValue).AddCoin(coin.CoinsCount);
                }
                OnPropertyChanged(nameof(coins));
                OnPropertyChanged(nameof(Balance));
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
