using MaxBalesOnCon.ViewModels;
using Prism.Events;
using System.Windows;
using System.Windows.Controls;

namespace MaxBalesOnCon.Views
{
    /// <summary>
    /// Interaction logic for MaxBalesView.xaml
    /// </summary>
    public partial class MaxBalesView : UserControl
    {
        private IEventAggregator _eventAggregator;

        public int ConveyorId { get; set; }

        public MaxBalesView(IEventAggregator eventAggregator, int conveyorId)
        {
            InitializeComponent();
            this._eventAggregator = eventAggregator;
            this.ConveyorId = conveyorId;
            this.DataContext = new MaxBalesViewModel(_eventAggregator, conveyorId);
        }

        private void Apply_Close(object sender, RoutedEventArgs e)
        {
            var window = this.Parent as Window;
            window?.Close();
        }
    }
}
