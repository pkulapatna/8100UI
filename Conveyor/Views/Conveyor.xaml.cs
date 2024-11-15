using Control.ViewModels;
using System.Windows.Controls;

namespace Control
{
    /// <summary>
    /// Interaction logic for ConveyorBlue.xaml
    /// </summary>
    public partial class Conveyor : UserControl
    {
        public Conveyor(ConverorViewModel conveyorViewModel)
        {
            InitializeComponent();
            this.DataContext = conveyorViewModel;
        }
    }
}
