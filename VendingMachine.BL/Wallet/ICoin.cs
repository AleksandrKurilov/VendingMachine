namespace VendingMachine.BL.Wallet
{
    public interface ICoin
    {
        //Достоинство монеты
        int CoinValue { get; }

        //Количество монет
        int CoinsCount { get; }

        //Добавить монету
        void AddCoin();

        //Добавляет несколько монет
        void AddCoin(int coinsCount);

        //Возможно ли получить одну монету
        bool CanGetCoin { get; }

        //Уменьшает кол-во монет на одну
        void GetCoin();
    }
}
