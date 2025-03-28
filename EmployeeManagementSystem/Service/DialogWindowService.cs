using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EmployeeManagementSystem.Service
{
    public class DialogWindowService<T> : IDialogWindowService<T> where T : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public DialogWindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Show()
        {
            _serviceProvider.GetRequiredService<T>().Show();
        }

        public void ShowDialog()
        {
            _serviceProvider.GetRequiredService<T>().ShowDialog();
        }

        public void Close()
        {
            _serviceProvider.GetRequiredService<T>().Close();
        }
    }
}
