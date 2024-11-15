using _8100UI.Services;
using FieldModify.ViewModels;
using Prism.Events;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FieldModify.Views
{
    /// <summary>
    /// Interaction logic for FieldModifyView.xaml
    /// </summary>
    public partial class FieldModifyView : UserControl
    {
        private readonly IEventAggregator _eventAggregator;
     
        public FieldModifyView(IEventAggregator eventAggregator, string hdrName, string ObjectValue, string fieldType)
        {
            InitializeComponent();
            this._eventAggregator = eventAggregator;

            this.DataContext = new FieldModifyViewModel(_eventAggregator, ObjectValue, hdrName, fieldType);

            _eventAggregator.GetEvent<CloseObjModWindow>().Subscribe(CloseFieldModify);

            FdlValue.Focus();
        }

        private void CloseFieldModify(bool obj)
        {
            Window.GetWindow(this).Close();
        }

        private void Filteronlynumber(object sender, TextCompositionEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text)) return;
            e.Handled = IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string str)
        {
            Regex reg = new Regex("[^0-9.]+");
            return reg.IsMatch(str);
        }

        private void Apply_Close(object sender, RoutedEventArgs e)
        {
            var window = this.Parent as Window;
            window?.Close();
        }
    }
}
