namespace VendingMachine.BL.VendingMachine
{
    public interface IRepositoryItem
    {
        BeverageBase Beverage { get; }
        int AvailableBeverageCount { get; }
        bool CanBuyBeverage { get; }
        bool GetBeverage();
    }
}
