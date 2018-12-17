using System.ComponentModel;
using System.Runtime.CompilerServices;
using VendingMachine.BL.Annotations;
using VendingMachine.BL.Wallet;

namespace VendingMachine.BL.VendingMachine
{
    public class VendingMachine : BaseVendingMachine, INotifyPropertyChanged
    {
        public VendingMachine(ICoinAcceptor coinAcceptor, IVendingMachineWallet wallet, 
            IBeverageRepository repository) 
            : base(coinAcceptor, wallet, repository)
        {
        }

        public override string BuyBeverage(string beverageName)
        {
            var beverage = BeverageRepository.GetBeverage(beverageName);

            if(beverage.Beverage.Coast > CoinAcceptor.Balance)
                return "Недостаточно средств";


            if (beverage.AvailableBeverageCount < 1)
                return "Данного напитка нет в налчии, выберите другой напиток";

            GetCoinsFromAcceptor(beverage.Beverage.Coast);

            beverage.GetBeverage();

            OnPropertyChanged(nameof(BeverageRepository));
            OnPropertyChanged(nameof(Wallet));
            OnPropertyChanged(nameof(CoinAcceptor));

            return "Спасибо!";
        }

        protected override void GetCoinsFromAcceptor(int coast)
        {
            Wallet.AddCoinsFromAcceptor(coast, CoinAcceptor.GetCoins());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
