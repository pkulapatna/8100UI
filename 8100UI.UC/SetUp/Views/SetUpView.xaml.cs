using Prism.Events;
using SetUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SetUp.Views
{
    /// <summary>
    /// Interaction logic for SetUpView.xaml
    /// </summary>
    public partial class SetUpView : UserControl
    {
        private IEventAggregator _eventAggregator;

        public SetUpView(IEventAggregator EventAggregator)
        {
            InitializeComponent();
            this._eventAggregator = EventAggregator;
            this.DataContext = new SetUpViewModel(_eventAggregator);
        }

        private void Apply_Close(object sender, RoutedEventArgs e)
        {
            var window = this.Parent as Window;
            window?.Close();
        }
    }
}
