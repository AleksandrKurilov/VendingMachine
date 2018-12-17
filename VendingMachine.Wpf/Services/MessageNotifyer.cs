using System.Windows;

namespace VendingMachine.Wpf.Services
{
    public class MessageNotifyer : IMessageNotifyer
    {
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowInformationMessage(string message)
        {
            MessageBox.Show(message, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
