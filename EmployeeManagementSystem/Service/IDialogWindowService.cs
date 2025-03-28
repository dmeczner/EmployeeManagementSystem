namespace EmployeeManagementSystem.Service
{
    public interface IDialogWindowService<T>
    {
        void Show();
        void ShowDialog();
        void Close();
    }
}
