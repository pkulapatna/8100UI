using System.Windows.Controls;
using BaleStation.ViewModels;

namespace BaleStation.Views
{
    /// <summary>
    /// Interaction logic for BaleStationLineTwoView.xaml
    /// </summary>
    public partial class BaleStationLineTwoView : UserControl
    {
        public BaleStationLineTwoView(BaleStationLineTwoViewModel baleStationLineTwo)
        {
            InitializeComponent();
            this.DataContext = baleStationLineTwo;
        }
    }
}
