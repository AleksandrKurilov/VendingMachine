using System.Collections.Generic;

namespace VendingMachine.BL.Wallet
{
    public interface IWallet : IBaseWallet
    {
        //Получить монету
        ICoin GetCoin(int coinValue);

        //Добавить монету
        void AddCoin(int coinValue);

        //Добавляет сдачу в кошелек
        void AddCahsBack(ICollection<ICoin> cashBack);
    }
}
