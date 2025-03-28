using EmployeeManagementSystem.Common;

namespace EmployeeManagementSystem.Model
{
    public class LoadingScreenUIInfo : BaseViewModel
    {
        private string _processText;
        private double _processState;
        private string _processCurrent;

        public string ProcessText
        {
            get { return _processText; }
            set { _processText = value; }
        }

        public double ProcessState
        {
            get { return _processState; }
            set { _processState = value; }
        }

        public string ProcessCurrent
        {
            get { return _processCurrent; }
            set { _processCurrent = value; }
        }

        public bool IsCanceled { get; set; } = false;

        public CancellationTokenSource CancelTokenSource { get; set; } = new CancellationTokenSource();

        public LoadingScreenUIInfo()
        {
            ProcessText = string.Empty;
            ProcessState = 0.0;
            ProcessCurrent = string.Empty;
        }
    }
}
