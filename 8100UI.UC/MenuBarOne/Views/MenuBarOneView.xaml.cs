using MenuBarOne.ViewModels;
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

namespace MenuBarOne.Views
{
    /// <summary>
    /// Interaction logic for MenuBarOneView.xaml
    /// </summary>
    public partial class MenuBarOneView : UserControl
    {
      
        public MenuBarOneView(MenuBarOneViewModel menuOneVM)
        {
            InitializeComponent();
            this.DataContext = menuOneVM;
        }
    }
}
