using JobParams.ViewModels;
using System.Windows.Controls;

namespace JobParams.Views
{
    /// <summary>
    /// Interaction logic for JobParamsView.xaml
    /// </summary>
    public partial class JobParamsView : UserControl
    {
        public JobParamsView(JobParamsViewModel _jobParamsViewModel)
        {
            InitializeComponent();
            this.DataContext = _jobParamsViewModel;
        }
    }
}
