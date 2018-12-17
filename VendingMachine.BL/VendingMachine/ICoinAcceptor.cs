using System.Collections.Generic;
using VendingMachine.BL.Wallet;

namespace VendingMachine.BL.VendingMachine
{
    public interface ICoinAcceptor
    {
        //Баланс (Сумма монет в монетоприемнике)
        int Balance { get; }

        //Добавить монету в монетоприемник
        void AddCoin(ICoin coin);

        //Забирает все монеты из монетоприемника и 
        //очищает колекцию монет
        ICollection<ICoin> GetCoins();
    }
}
