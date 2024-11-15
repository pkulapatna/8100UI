using _8100UI.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SetUp;
using SetUp.Views;
using System.Windows;
using SetUpWindow = SetUp.SetUpWindow;

namespace MenuBarOne.ViewModels
{
    public class MenuBarOneViewModel : BindableBase
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;

        private Window LogWindow;     
        
        public MenuBarOneViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
          
            _eventAggregator.GetEvent<ClosingLogWindow>().Subscribe(CloseLogWindows);
            _eventAggregator.GetEvent<MoveLogWindow>().Subscribe(MoveLogWindows);
        }

        private void MoveLogWindows(bool obj)
        {
            LogWindow.DragMove();
        }

        private void CloseLogWindows(bool obj)
        {
            if (LogWindow != null)
            {
                LogWindow.Close();
                LogWindow = null;
            }
        }

        private DelegateCommand _clearAllCommand;
        public DelegateCommand ClearAllCommand =>
       _clearAllCommand ?? (_clearAllCommand =
            new DelegateCommand(ClearAllCommandExecute));
        private void ClearAllCommandExecute()
        {
            ClsSerilog.LogMessage(ClsSerilog.Info, $"Clear All Bales");
            ClsPipeMessage.SendPipeMessage($"ClearAll;");
        }

        private DelegateCommand _systemResetCommand;
        public DelegateCommand SystemResetCommand =>
       _systemResetCommand ?? (_systemResetCommand =
            new DelegateCommand(SystemResetCommandExecute));
        private void SystemResetCommandExecute()
        {
            ClsSerilog.LogMessage(ClsSerilog.Info, $"System Reset");
            ClsPipeMessage.SendPipeMessage($"SysReset;");
        }

        private DelegateCommand _setupCommand;
        public DelegateCommand SetupCommand =>
       _setupCommand ?? (_setupCommand =
            new DelegateCommand(SetupCommandExecute));
        private void SetupCommandExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "Display Options",
                Width = 600,
                Height = 500,
                ResizeMode = ResizeMode.NoResize,
                Content = new SetUpView(_eventAggregator)
            };
            _setupWindow?.ShowDialog();
        }

        private DelegateCommand _showLogCommand;
        public DelegateCommand ShowLogCommand =>
       _showLogCommand ?? (_showLogCommand =
            new DelegateCommand(ShowLogCommandExecute));
        private void ShowLogCommandExecute()
        {
            ClsPipeMessage.SendPipeMessage($"ShowLog;");
            ClsSerilog.LogMessage(ClsSerilog.Info, $"Show Log");
        }

        private DelegateCommand _showGraphCommand;
        public DelegateCommand ShowGraphCommand =>
       _showGraphCommand ?? (_showGraphCommand =
            new DelegateCommand(ShowGraphCommandExecute));
        private void ShowGraphCommandExecute()
        {
            ClsPipeMessage.SendPipeMessage($"Graph;");
            ClsSerilog.LogMessage(ClsSerilog.Info, $"Show Graph");
        }

    }
}
