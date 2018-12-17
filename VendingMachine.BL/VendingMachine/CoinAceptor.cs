using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using VendingMachine.BL.Annotations;
using VendingMachine.BL.Wallet;

namespace VendingMachine.BL.VendingMachine
{
    public class CoinAceptor : ICoinAcceptor, INotifyPropertyChanged
    {
        public CoinAceptor()
        {
            ClearCoins();
        }

        private List<ICoin> coins;

        public int Balance
        {
            get
            {
                if(coins == null || coins.Count < 1) return 0;

                return coins
                    .Aggregate(0, (total, coin) => total + coin.CoinValue * coin.CoinsCount);
            }
        }

        public void AddCoin(ICoin coin)
        {
            if(!coins.Any(c => c.CoinValue == coin.CoinValue))
                throw  new Exception("Нет монеты данного достоинства");

            coins.Single(c => c.CoinValue == coin.CoinValue).AddCoin(coin.CoinsCount);

            OnPropertyChanged(nameof(Balance));
            OnPropertyChanged(nameof(coins));
        }

        public ICollection<ICoin> GetCoins()
        {
            ICollection<ICoin> tmpCoins = coins;
            ClearCoins();
            OnPropertyChanged(nameof(Balance));
            OnPropertyChanged(nameof(coins));
            return tmpCoins;
        }

        private void ClearCoins()
        {
            coins = new List<ICoin>();
            coins.Add(new Coin(1));
            coins.Add(new Coin(2));
            coins.Add(new Coin(5));
            coins.Add(new Coin(10));

            coins = coins.OrderByDescending(c => c.CoinValue).ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
