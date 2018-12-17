using System.ComponentModel;
using System.Runtime.CompilerServices;
using VendingMachine.BL.Annotations;

namespace VendingMachine.BL.VendingMachine
{
    public class RepositoryItem : IRepositoryItem, INotifyPropertyChanged
    {
        public RepositoryItem(BeverageBase beverage, int availableBeverageCount)
        {
            AvailableBeverageCount = availableBeverageCount;
            Beverage = beverage;
        }

        public BeverageBase Beverage { get; }
        public int AvailableBeverageCount { get; private set; }

        public bool GetBeverage()
        {
            if(AvailableBeverageCount < 0)
                return false;

            AvailableBeverageCount--;
            OnPropertyChanged(nameof(AvailableBeverageCount));
            OnPropertyChanged(nameof(CanBuyBeverage));
            return true;
        }

        public bool CanBuyBeverage
        {
            get { return AvailableBeverageCount > 0; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
