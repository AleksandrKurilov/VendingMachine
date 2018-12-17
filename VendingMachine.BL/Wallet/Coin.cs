using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VendingMachine.BL.Annotations;

namespace VendingMachine.BL.Wallet
{
    public class Coin : ICoin, INotifyPropertyChanged
    {
        private readonly int coinValue;
        private int coinsCount;

        public Coin(int coinValue, int coinsCount)
        {
            this.coinValue = coinValue;
            this.coinsCount = coinsCount;
        }

        public Coin(int coinValue)
        {
            this.coinValue = coinValue;
            coinsCount = 0;
        }

        public int CoinValue => coinValue;
        public int CoinsCount => coinsCount;

        public void GetCoin()
        {
            if(coinsCount < 1)
                throw new Exception("Количество монет не может быть меньше нуля");
            
            coinsCount--;
        }

        public void AddCoin()
        {
            coinsCount++;
            OnPropertyChanged(nameof(CoinsCount));
        }

        public void AddCoin(int addCoinsCount)
        {
            if(addCoinsCount < 1)
                throw new Exception("Количество добавляемых монет должно быть больше нуля");

            coinsCount += addCoinsCount;
            OnPropertyChanged(nameof(CoinsCount));
        }

        public bool CanGetCoin
        {
            get { return CoinsCount > 0; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
