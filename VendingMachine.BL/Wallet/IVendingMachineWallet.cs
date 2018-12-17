using System.Collections.Generic;

namespace VendingMachine.BL.Wallet
{
    public interface IVendingMachineWallet : IBaseWallet
    {
        int CashBack { get; }

        ICollection<ICoin> GetCashBack();

        void AddCoinsFromAcceptor(int beverageCoast, ICollection<ICoin> coins);

        bool CanGetCachBack { get; }
    }
}
