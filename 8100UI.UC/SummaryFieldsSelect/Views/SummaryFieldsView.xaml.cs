using Prism.Events;
using SummaryFieldsSelect.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SummaryFieldsSelect.Views
{
    /// <summary>
    /// Interaction logic for SummaryFieldsView.xaml
    /// </summary>
    public partial class SummaryFieldsView : UserControl
    {
        public static SummaryFieldsView _summaryFieldsView;
        protected readonly Prism.Events.IEventAggregator _eventAggregator;
        private readonly SummaryFieldsViewModel _summaryFieldsViewModel;

        public SummaryFieldsView(IEventAggregator eventAggregator, int ilottype)
        {
            InitializeComponent();
            this._eventAggregator = eventAggregator;
            _summaryFieldsViewModel = new SummaryFieldsViewModel(_eventAggregator, ilottype);
            this.DataContext = _summaryFieldsViewModel;
          
        }

        private void LeftClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedHdrList.SelectedIndex > 0)
                {
                    ObservableCollection<string> newlist = (ObservableCollection<string>)SelectedHdrList.ItemsSource;
                    int NewIndex = SelectedHdrList.SelectedIndex - 1;

                    if ((NewIndex > -1) || (NewIndex >= SelectedHdrList.Items.Count))
                    {
                        object selected = SelectedHdrList.SelectedItem;

                        // Removing removable element ItemsControl.ItemsSource
                        newlist.Remove(selected.ToString());
                        // Insert it in new position
                        newlist.Insert(NewIndex, selected.ToString());
                        // Restore selection
                        _summaryFieldsViewModel.SelectHdrItems = newlist;

                        //SelectHdrItems.SelectedItem = selected;
                        SelectedHdrList.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR in LeftClick " + ex.Message);
                //   MainWindow.AppWindows.LogObject.LogMessage(MsgTypes.WARNING, MsgSources.APPDROPPROFILE, "LeftClick " + ex.Message);
            }
        }

        private void RightClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((SelectedHdrList.SelectedIndex > -1) & (SelectedHdrList.SelectedIndex + 1 < SelectedHdrList.Items.Count))
                {
                    ObservableCollection<string> newlist = (ObservableCollection<string>)SelectedHdrList.ItemsSource;
                    int NewIndex = SelectedHdrList.SelectedIndex + 1;
                    object selected = SelectedHdrList.SelectedItem;

                    // Removing removable element ItemsControl.ItemsSource
                    newlist.Remove(selected.ToString());
                    // Insert it in new position
                    newlist.Insert(NewIndex, selected.ToString());

                    _summaryFieldsViewModel.SelectHdrItems = newlist;
                    SelectedHdrList.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR in RightClick " + ex.Message);
                //MainWindow.AppWindows.LogObject.LogMessage(MsgTypes.WARNING, MsgSources.APPBALEREALTIME, "RightClick" + ex.Message);
            }
        }


        private void ColumnHeader_Clicked(object sender, RoutedEventArgs e)
        {
            string ClickEvent = ((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString();

            _summaryFieldsViewModel.Hdr_ClickEvents(ClickEvent);

            //  _eventAggregator.GetEvent<SFVHeaderClickEvent>().Publish(((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString());
        }

        private void Save_Apply(object sender, RoutedEventArgs e)
        {
            var window = this.Parent as Window;
            window?.Close();
        }
    }
}
