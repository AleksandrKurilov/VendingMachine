using System.Collections.Generic;

namespace VendingMachine.BL.VendingMachine
{
    public interface IBeverageRepository
    {
        IRepositoryItem GetBeverage(string beverageName);

        ICollection<IRepositoryItem> Items { get; }
    }
}
