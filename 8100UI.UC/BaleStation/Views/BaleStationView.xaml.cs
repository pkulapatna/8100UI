using BaleStation.ViewModels;
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

namespace BaleStation.Views
{
    /// <summary>
    /// Interaction logic for BaleStationView.xaml
    /// </summary>
    public partial class BaleStationView : UserControl
    {
        public BaleStationView(BaleStationViewModel baleStationOneVM)
        {
            InitializeComponent();
            this.DataContext = baleStationOneVM;
        }
    }
}
