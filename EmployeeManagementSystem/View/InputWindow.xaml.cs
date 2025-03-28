using EmployeeManagementSystem.ViewModel;
using System.Windows;

namespace EmployeeManagementSystem.View
{
    /// <summary>
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public InputWindow(EmployeeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;            
        }
    }
}
