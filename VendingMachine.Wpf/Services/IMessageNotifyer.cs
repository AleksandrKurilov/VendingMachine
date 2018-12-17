namespace VendingMachine.Wpf.Services
{
    public interface IMessageNotifyer
    {
        void ShowErrorMessage(string message);
        void ShowInformationMessage(string message);
    }
}
