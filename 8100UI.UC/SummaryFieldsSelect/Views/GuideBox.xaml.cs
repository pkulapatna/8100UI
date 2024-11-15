using System.Windows;

namespace SummaryFieldsSelect.Views
{
    /// <summary>
    /// Interaction logic for GuideBox.xaml
    /// </summary>
    public partial class GuideBox : Window
    {
        public GuideBox()
        {
            InitializeComponent();
            textInfo.Text = System.IO.File.ReadAllText(@"C:\Forté Technology, Inc\info.txt");
        }
    }
}
