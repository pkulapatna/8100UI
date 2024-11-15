using MidCmbDisplayBox.ViewModels;
using System;
using System.Windows.Controls;

namespace MidCmbDisplayBox.Views
{
    /// <summary>
    /// Interaction logic for MidCmbBoxView.xaml
    /// </summary>
    public partial class MidCmbBoxView : UserControl
    {
        private double IScreenWidth { get; set; }

        public MidCmbBoxView(MidCmbBoxViewModel midDisplayCmbBox)
        {
            InitializeComponent();
            this.DataContext = midDisplayCmbBox;
        }

        private void Cmb_SizeChange(object sender, System.Windows.SizeChangedEventArgs e)
        {
            IScreenWidth = e.NewSize.Width;
           
            cmbBoxOne.FontSize = IScreenWidth/11;
        }
    }
}
