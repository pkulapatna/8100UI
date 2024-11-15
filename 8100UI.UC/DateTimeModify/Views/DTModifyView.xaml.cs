using DateTimeModify.ViewModels;
using Prism.Events;
using System.Windows;
using System.Windows.Controls;

namespace DateTimeModify.Views
{
    /// <summary>
    /// Interaction logic for DTModifyView.xaml
    /// </summary>
    public partial class DTModifyView : UserControl
    {
        private readonly IEventAggregator _eventAggregator;

        public DTModifyView(IEventAggregator eventAggregator, string hdrName, string ObjectValue)
        {
            InitializeComponent();
            this._eventAggregator = eventAggregator;
            this.DataContext = new DTModifyViewModel(_eventAggregator, hdrName, ObjectValue);   
        }
        private void Apply_Close(object sender, RoutedEventArgs e)
        {
            var window = this.Parent as Window;
            window?.Close();
        }
    }
}
