using System.Linq;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using VendingMachine.BL.VendingMachine;
using VendingMachine.BL.Wallet;
using VendingMachine.Wpf.Services;
using VendingMachineRef = VendingMachine.BL.VendingMachine.VendingMachine;

namespace VendingMachine.Wpf.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            var coinAceptor = new CoinAceptor();
            var vendingMachineValet = new VendingMachineWallet();
            var beverageRepository = new BeverageRepository();

            VendingMachine = new VendingMachineRef(coinAceptor, vendingMachineValet, beverageRepository);

            CustomerWallet = new Wallet();

            messageNotifyer = new MessageNotifyer();
        }

        private IMessageNotifyer messageNotifyer;

        public BaseVendingMachine VendingMachine { get; }

        public Wallet CustomerWallet { get; }

        #region Commands

        private ICommand depositCoin;
        public ICommand DepositCoinCommand
        {
            get
            {
                return depositCoin ?? (depositCoin = new RelayCommand<object>(DepositCoin));
            }
        }

        private ICommand buyBeverageCommand;
        public ICommand BuyBeverageCommand
        {
            get
            {
                return buyBeverageCommand ?? (buyBeverageCommand = new RelayCommand<object>(BuyBeverage));
            }
        }

        private ICommand getCashBackFromMachineCommand;
        public ICommand GetCashBackFromMachineCommand
        {
            get
            {
                return getCashBackFromMachineCommand ?? (getCashBackFromMachineCommand = new RelayCommand(GetCashBack));
            }
        }

        #endregion

        /// <summary>
        /// Купить напиток
        /// </summary>
        /// <param name="beverage"></param>
        private void BuyBeverage(object beverage)
        {
            var repositoryItem = beverage as RepositoryItem;

            if(repositoryItem != null)
                messageNotifyer.ShowInformationMessage(VendingMachine.BuyBeverage(repositoryItem.Beverage.Name));
        }

        /// <summary>
        /// Внести монету в монетоприемник
        /// </summary>
        /// <param name="coinObject"></param>
        private void DepositCoin(object coinObject)
        {
            if(coinObject == null) return;
            
            int coinValue = ((ICoin) coinObject).CoinValue;

            if (!CustomerWallet.Coins.Single(c => c.CoinValue == coinValue).CanGetCoin)
            {
                messageNotifyer.ShowErrorMessage("В вашем кошельке нет монет таким достоинством");
                return;
            }

            ICoin coin = CustomerWallet.GetCoin(coinValue);

            if(coin == null) return;
            
            VendingMachine.CoinAcceptor.AddCoin(coin);
        }

        /// <summary>
        /// Забрать сдачу
        /// </summary>
        private void GetCashBack()
        {
            var cashBack = VendingMachine.Wallet.GetCashBack();

            if(cashBack == null || cashBack.Count < 1)
            {
                messageNotifyer.ShowErrorMessage("Нет сдачи");
                return;
            }

            CustomerWallet.AddCahsBack(cashBack);
        }
    }
}