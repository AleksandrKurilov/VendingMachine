using System.Collections.Generic;

namespace VendingMachine.BL.Wallet
{
    public interface IBaseWallet
    {
        //Монеты в кашельке
        ICollection<ICoin> Coins { get; }

        //Баланс в кошельке
        int Balance { get; }
    }
}
