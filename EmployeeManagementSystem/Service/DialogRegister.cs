using EmployeeManagementSystem.View;

namespace EmployeeManagementSystem.Service
{
    public class DialogRegister
    {
        public Action InputShowAction { get; set; }
        public Action InputCloseAction { get; set; }
        public Action LoadingCloseAction { get; set; }
        public Action LoadingShowAction { get; set; }

        public DialogRegister(IDialogWindowService<InputWindow> inputDialogService, IDialogWindowService<LoadingScreen> loadingDialogService) 
        {
            InputShowAction = inputDialogService.ShowDialog;
            InputCloseAction = inputDialogService.Close;

            LoadingShowAction = loadingDialogService.ShowDialog;
            LoadingShowAction = loadingDialogService.Close;
        }

    }
}
