using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using VendingMachine.BL.Annotations;
using VendingMachine.BL.Wallet;

namespace VendingMachine.BL.VendingMachine
{
    public class VendingMachineWallet : IVendingMachineWallet, INotifyPropertyChanged
    {
        public VendingMachineWallet(List<ICoin> coins)
        {
            if(coins == null || coins.Count < 1)
                throw new ArgumentNullException();

            this.coins = coins.OrderByDescending(c => c.CoinValue).ToList();
        }

        public VendingMachineWallet()
        {
            coins = new List<ICoin>();
            coins.Add(new Coin(1, 100));
            coins.Add(new Coin(2, 100));
            coins.Add(new Coin(5, 100));
            coins.Add(new Coin(10, 100));

            coins = coins.OrderByDescending(c => c.CoinValue).ToList();
        }

        private List<ICoin> coins;

        public int CashBack { get; private set; }

        public ICollection<ICoin> Coins => coins.AsReadOnly();

        public int Balance
        {
            get
            {
                if (coins == null || coins.Count < 1) return 0;

                return coins.Sum(c => c.CoinValue * c.CoinsCount);
            }
        }

        public bool CanGetCachBack
        {
            get { return CashBack > 0; }
        }

        public ICollection<ICoin> GetCashBack()
        {
            var result = new List<ICoin>();
            if(GetRecursiveCoins(CashBack, 0, 0, result))
            {
                CashBack -= result.Sum(c => c.CoinValue);
                UpdateProperties();
                return result;
            }

            return null;
        }

        private bool GetRecursiveCoins(int currentSumm, int currentIndex, int usedCurrentCoin, List<ICoin> currentSummCoins)
        {
            if (currentSumm == coins[currentIndex].CoinValue && coins[currentIndex].CoinsCount > usedCurrentCoin)
            {
                coins[currentIndex].GetCoin();
                currentSummCoins.Add(new Coin(coins[currentIndex].CoinValue, 1));
                return true;
            }

            if (coins[currentIndex].CoinValue < currentSumm && coins[currentIndex].CoinsCount > usedCurrentCoin)
                if (GetRecursiveCoins(currentSumm - coins[currentIndex].CoinValue,
                    currentIndex, usedCurrentCoin + 1, currentSummCoins))
                {
                    currentSummCoins.Add(new Coin(coins[currentIndex].CoinValue, 1));
                    coins[currentIndex].GetCoin();
                    return true;
                }

            while (currentIndex + 1 < coins.Count)
            {
                currentIndex++;

                if (GetRecursiveCoins(currentSumm, currentIndex, 0, currentSummCoins))
                    return true;
            }

            return false;
        }

        public void AddCoinsFromAcceptor(int beverageCoast, ICollection<ICoin> addedCoins)
        {
            CashBack += addedCoins.Sum(c => c.CoinValue * c.CoinsCount) - beverageCoast;

            foreach(var coin in addedCoins)
            {
                if(coin.CoinsCount < 1) continue;
                
                coins.Single(c => c.CoinValue == coin.CoinValue).AddCoin(coin.CoinsCount);
            }

            UpdateProperties();
        }

        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(coins));
            OnPropertyChanged(nameof(Balance));
            OnPropertyChanged(nameof(CashBack));
            OnPropertyChanged(nameof(CanGetCachBack));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
