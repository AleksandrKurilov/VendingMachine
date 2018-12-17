using System;
using VendingMachine.BL.Wallet; 

namespace VendingMachine.BL.VendingMachine
{
    public abstract class BaseVendingMachine
    {
        public BaseVendingMachine(ICoinAcceptor coinAcceptor, IVendingMachineWallet wallet, 
            IBeverageRepository beverageRepository)
        {
            if(coinAcceptor == null || wallet == null || beverageRepository == null)
                throw new ArgumentNullException();

            CoinAcceptor = coinAcceptor;
            Wallet = wallet;
            BeverageRepository = beverageRepository;
        }

        //Монетоприемник
        public ICoinAcceptor CoinAcceptor { get; }

        //Источник напитков
        public IBeverageRepository BeverageRepository { get; }

        //Кошелек машины
        public IVendingMachineWallet Wallet { get; }

        //Купить напиток
        public abstract string BuyBeverage(string beverageName);

        protected abstract void GetCoinsFromAcceptor(int coast);
    }
}
