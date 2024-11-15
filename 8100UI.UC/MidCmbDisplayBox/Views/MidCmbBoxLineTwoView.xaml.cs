using MidCmbDisplayBox.ViewModels;
using System.Windows.Controls;

namespace MidCmbDisplayBox.Views
{
    /// <summary>
    /// Interaction logic for MidCmbBoxLineTwoView.xaml
    /// </summary>
    public partial class MidCmbBoxLineTwoView : UserControl
    {
        public MidCmbBoxLineTwoView(MidCmbBoxLIneTwoViewModel midDisplayCmbBox)
        {
            InitializeComponent();
            this.DataContext = midDisplayCmbBox;
        }
    }
}
