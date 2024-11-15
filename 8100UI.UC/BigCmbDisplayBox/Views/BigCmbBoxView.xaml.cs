using BigCmbDisplayBox.ViewModels;
using System.Windows.Controls;

namespace BigCmbDisplayBox.Views
{
    /// <summary>
    /// Interaction logic for BigCmbBoxView.xaml
    /// </summary>
    public partial class BigCmbBoxView : UserControl
    {
        public BigCmbBoxView(BigCmbBoxViewModel bigDisplayCmbBox)
        {
            InitializeComponent();
            this.DataContext = bigDisplayCmbBox;
        }
    }
}
