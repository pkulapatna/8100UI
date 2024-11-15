using _8100UI.Properties;
using _8100UI.Services;
using _8100UI.ViewModels;
using System;
using System.Threading;
using System.Windows;

namespace _8100UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindows;
        private readonly System.Threading.Mutex objMutex = null;

        public MainWindow()
        {
            InitializeComponent();
            AppWindows = this;
            Loaded += MainWindow_Loaded;

            objMutex = new System.Threading.Mutex(false, "Forte8100.exe");
            if (!objMutex.WaitOne(0, false))
            {
                objMutex.Close();
                objMutex = null;
                MessageBox.Show("Another instance is already running!");
                ClsSerilog.LogMessage(ClsSerilog.Info, $"Another instance is already running! -> {DateTime.Now}");
                AppWindows.Close();
                Thread.CurrentThread.Abort();
            }
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseWindows vm)
            {
                vm.Close += () => { this.Close(); };
            }
        }
    }
}
