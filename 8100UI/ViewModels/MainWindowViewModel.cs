using _8100UI.Services;
using MenuBarOne.ViewModels;
using MenuBarOne.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using Application = System.Windows.Forms.Application;

namespace _8100UI.ViewModels
{
    public class MainWindowViewModel : BindableBase, ICloseWindows
    {

        protected readonly Prism.Events.IEventAggregator _eventAggregator;
        private readonly ClsBaleLineEvents BaleEvents;

        private readonly ClsPipeServer PipeServer;
        private readonly ClsPipeClient PipeClient = ClsPipeClient.Instance;
        public Action Close { get; set; }

      
        private Visibility _menuVisible;
        public Visibility MenuVisible
        {
            get { return _menuVisible; }
            set { SetProperty(ref _menuVisible, value); }
        }



        public MenuBarOneViewModel MenuOne;
        public UserControl TopMenuOneBar
        {
            get { return new MenuBarOneView(MenuOne); }
        }


        private int screenWidth = (int)((int)SystemParameters.PrimaryScreenWidth * .80); // 1300;
        public int ScreenWidth
        {
            get { return screenWidth; }
            set { SetProperty(ref screenWidth, value); }
        }

        private int screenHeight = (int)((int)SystemParameters.PrimaryScreenHeight * .80); // 740;
        public int ScreenHeight
        {
            get { return screenHeight; }
            set { SetProperty(ref screenHeight, value); }
        }


        private string _title = "Forté 8100";
        public string WindowTitle
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _currentUser;
        public string CurrentUser
        {
            get { return _currentUser; }
            set
            {
                SetProperty(ref _currentUser, value);
                ClassCommon.CurrentUser = CurrentUser;
            }
        }
        private bool _userLogin;
        public bool UserLogin
        {
            get => _userLogin;
            set
            {
                SetProperty(ref _userLogin, value);
                ClassCommon.UserLogin = UserLogin;
                MenuVisible = value ? MenuVisible = Visibility.Visible : MenuVisible = Visibility.Hidden;
            }
        }
       
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(HandleRef hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr WindowHandle);
        public const int SW_RESTORE = 9;

        /// <summary>
        /// Shows Dash Board. (Only works Local)
        /// </summary>
        private DelegateCommand _showDBCommand;
        public DelegateCommand ShowDBCommand =>
       _showDBCommand ?? (_showDBCommand =
            new DelegateCommand(ShowDBCommandExecute));
        private void ShowDBCommandExecute()
        {
            Process[] objProcesses = System.Diagnostics.Process.GetProcessesByName("ForteService_Monitor");
            if (objProcesses.Length > 0)
            {
                IntPtr hWnd = IntPtr.Zero;
                hWnd = objProcesses[0].MainWindowHandle;
                ShowWindowAsync(new HandleRef(null, hWnd), SW_RESTORE);
                SetForegroundWindow(objProcesses[0].MainWindowHandle);
                ClsSerilog.LogMessage(ClsSerilog.Info, $"Show DashBoard");
            }
            else
                ClsSerilog.LogMessage(ClsSerilog.Info, $"DashBoard not running on this PC !");
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


        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;

            //Windows menu
            MenuOne = new MenuBarOneViewModel(_eventAggregator);

            BaleEvents = new ClsBaleLineEvents(_eventAggregator);
            BaleEvents.StartEventsTimer("BaleLine");

          
            CurrentUser = ClassCommon.CurrentUser;
            UserLogin = ClassCommon.UserLogin;
           
            PipeServer = new ClsPipeServer(_eventAggregator);
            PipeServer.StartListen();
          
           
            if (UserLogin)
                WindowTitle = $"Forté 8100 {ClassCommon.CustomerName}    [Login as {CurrentUser}]";
            else
                WindowTitle = $"Forté 8100 {ClassCommon.CustomerName}    [Not Login]";

            _eventAggregator.GetEvent<NamePipeCloseMessage>().Subscribe(ProcessNamePipeMsg);
            _eventAggregator.GetEvent<NamePipeUserLogin>().Subscribe(ProcessNamePipeLogonMsg);
        }

        private void RestartApp()
        {
            Views.MainWindow.AppWindows.Close();
            Thread.Sleep(100);
            Application.Restart();
        }


        private void ProcessNamePipeLogonMsg(string message)
        {
            message = message.TrimEnd(';');
            string[] words = message.Split(';');

            if (words[0] == "Logon")
            {
                UserLogin = true;
                CurrentUser = words[1];
                WindowTitle = $"Forté 8100 {ClassCommon.CustomerName}    [Login as {CurrentUser}]";
                ClassCommon.CurrentUser = CurrentUser;
            }
            else if (words[0] == "Logoff")
            {
                UserLogin = false;
                WindowTitle = $"Forté 8100 {ClassCommon.CustomerName}    [Not Login]";
            }

            _eventAggregator.GetEvent<UserLogin>().Publish(UserLogin);
        }

        private void ProcessNamePipeMsg(string message)
        {

            if (message != string.Empty)
            {
                if (message == "Close")
                {
                    CloseApplication();
                    CloseWindow();
                }
            }
        }

        private DelegateCommand _closedPageICommand;
        public DelegateCommand ClosedPageICommand =>
        _closedPageICommand ?? (_closedPageICommand =
            new DelegateCommand(ClosedPageICommandxecute));
        private void ClosedPageICommandxecute()
        {
            CloseApplication();
        }

        private void CloseApplication()
        {
            _eventAggregator.GetEvent<CloseMainWindow>().Publish(true);

            if (PipeServer != null) PipeServer.KillThread();
            if (PipeClient != null) PipeClient.ClosePipeClient();

            ServiceEventsTimer EventsTimer = ServiceEventsTimer.Instance;
            EventsTimer?.StopBaleEventsTimer();

            ClsSerilog.LogMessage(ClsSerilog.Info, "Closing Application");
            ClsSerilog.CloseLogger();

            System.Diagnostics.Process.GetCurrentProcess()?.Kill();
        }

        private void CloseWindow()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => { Close?.Invoke(); }));
        }
    }

    interface ICloseWindows
    {
        Action Close { get; set; }
    }
}
