using _8100UI.Services;
using Prism.Events;
using Summary.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Summary.Views
{
    /// <summary>
    /// Interaction logic for SummaryView
    /// </summary>
    public partial class SummaryView : UserControl
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;

        public ObservableCollection<SqlLotSumFields> BaleColumnList { get; set; }
        public ObservableCollection<SqlLotSumFields> ShiftColumnList { get; set; }
        public ObservableCollection<SqlLotSumFields> TallyDayList { get; set; }
        public ObservableCollection<SqlLotSumFields> OpenLotColumnList { get; set; }
        public ObservableCollection<SqlLotSumFields> CloseLotColumnList { get; set; }
        public ObservableCollection<SqlLotSumFields> OpenDaySummaryList { get; set; }
        public ObservableCollection<SqlLotSumFields> CloseDaySummaryList { get; set; }

        public ObservableCollection<SqlLotSumFields> DayTallyList { get; set; }

        public ObservableCollection<SqlLotSumFields> OpenShiftColumnList { get; set; }
        public ObservableCollection<SqlLotSumFields> CloseShiftColumnList { get; set; }
        public ObservableCollection<SqlLotSumFields> TallyShiftColumnList { get; set; }

        public ObservableCollection<SqlLotSumFields> OpenPeriodColumnList { get; set; }
        public ObservableCollection<SqlLotSumFields> ClosePeriodColumnList { get; set; }
        public ObservableCollection<SqlLotSumFields> TallyPeriodColumnList { get; set; }

        public ObservableCollection<SqlLotSumFields> TotalDayColumnList { get; set; }
        public ObservableCollection<SqlLotSumFields> TotalShiftColumnList { get; set; }
        public ObservableCollection<SqlLotSumFields> TotalPeriodColumnList { get; set; }


        private readonly Xmlhandler MyXml;

        public SummaryView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this._eventAggregator = eventAggregator;
            this.DataContext = new SummaryViewViewModel(_eventAggregator);
            MyXml = Xmlhandler.Instance;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            BaleColumnList = MyXml.ReadOpenLotXml(ClassCommon.BaleSummaryXmlFilePath);

            DataGridControl.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            DataGridControl.SelectedIndex = 0;
            DataGridControl.Focus();

            if (BaleColumnList.Count > 0)
            {
                for (int i = 0; i < BaleColumnList.Count; i++)
                {
                    if (e.PropertyName.Contains(BaleColumnList[i].FieldName))
                    {

                        if (BaleColumnList[i].FieldType == "datetime")
                        {
                            e.Column.ClipboardContentBinding.StringFormat = BaleColumnList[i].Format;
                        }
                        else
                            e.Column.ClipboardContentBinding.StringFormat = BaleColumnList[i].Format;
                    }
                }
            }

            if (e.PropertyName.StartsWith("Moisture"))
            {
                switch (ClassCommon.MoistureType)
                {
                    case 0: // %MC
                        e.Column.Header = "% MC";
                        break;

                    case 1: // %MR
                        e.Column.Header = "% MR";
                        break;

                    case 2: // %AD
                        e.Column.Header = "% AD";
                        break;

                    case 3: // %BD
                        e.Column.Header = "% BD";
                        break;
                }
            }

            if (e.PropertyName.StartsWith("Weight"))
            {
                switch (ClassCommon.WeightUnit)
                {
                    case 0: // Metric
                        e.Column.Header = "Weight kg.";
                        break;

                    case 1: // English
                        e.Column.Header = "Weight Lbs.";
                        break;
                }
            }

            DataGridControl.SelectedIndex = 0;
            DataGridControl.Focus();
        }

        private void OnAutoGeneratingColumnCloseLot(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridControl2.Columns[0].Visibility = System.Windows.Visibility.Collapsed;

            DataGridClosedUnit.Columns[0].Visibility = System.Windows.Visibility.Collapsed;

            CloseLotColumnList = MyXml.ReadOpenLotXml(ClassCommon.ClosedLotSummaryXmlFilePath);
            //Close Lot
            DataGridControl2.SelectedIndex = 0;
            if (CloseLotColumnList.Count > 0)
            {
                for (int i = 0; i < CloseLotColumnList.Count; i++)
                {
                    if (e.PropertyName.Contains(CloseLotColumnList[i].FieldName))
                    {
                        e.Column.ClipboardContentBinding.StringFormat = CloseLotColumnList[i].Format;
                    }
                }
            }
        }

        private void OnAutoGeneratingColumnOpenLot(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridControl1.Columns[0].Visibility = System.Windows.Visibility.Collapsed;


            DataGridOpenUnit.Columns[0].Visibility = System.Windows.Visibility.Collapsed;

            OpenLotColumnList = MyXml.ReadOpenLotXml(ClassCommon.OpenLotSummaryXmlFilePath);
          

            DataGridControl1.SelectedIndex = 0;
            if (OpenLotColumnList.Count > 0)
            {
                for (int i = 0; i < OpenLotColumnList.Count; i++)
                {
                    if (e.PropertyName.Contains(OpenLotColumnList[i].FieldName))
                    {
                        e.Column.ClipboardContentBinding.StringFormat = OpenLotColumnList[i].Format;
                    }
                }
            }
            
        }

        

        private void OnAutoGeneratingColumnDayTally(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void DataGridControl4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnAutoGeneratingColumnShiftOpen(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            ShiftColumnList = MyXml.ReadOpenLotXml(ClassCommon.ShiftSummaryXmlFilePath);
            TallyDayList = MyXml.ReadOpenLotXml(ClassCommon.ShiftTallyXmlFilePath);

            DataGridControl1.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            DataGridControl1.SelectedIndex = 0;
            if (ShiftColumnList.Count > 0)
            {
                for (int i = 0; i < ShiftColumnList.Count; i++)
                {
                    if (e.PropertyName.Contains(ShiftColumnList[i].FieldName))
                    {
                        e.Column.ClipboardContentBinding.StringFormat = ShiftColumnList[i].Format;
                    }
                }
            }
            DataGridControl2.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            DataGridControl2.SelectedIndex = 0;
            if (ShiftColumnList.Count > 0)
            {
                for (int i = 0; i < ShiftColumnList.Count; i++)
                {
                    if (e.PropertyName.Contains(ShiftColumnList[i].FieldName))
                    {
                        e.Column.ClipboardContentBinding.StringFormat = ShiftColumnList[i].Format;
                    }
                }
            }


        }


        private void OnAutoGeneratingColumnDaySummary(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridControl3.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            OpenDaySummaryList = MyXml.ReadOpenLotXml(ClassCommon.DaySummaryXmlFilePath);
            if (OpenDaySummaryList.Count > 0)
            {
                for (int i = 0; i < OpenDaySummaryList.Count; i++)
                {
                    if (e.PropertyName.Contains(OpenDaySummaryList[i].FieldName))
                    {
                        e.Column.ClipboardContentBinding.StringFormat = OpenDaySummaryList[i].Format;
                    }
                }
            }

            DataGridControl4.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            CloseDaySummaryList = MyXml.ReadOpenLotXml(ClassCommon.DaySummaryXmlFilePath);
            if (CloseDaySummaryList.Count > 0)
            {
                for (int i = 0; i < CloseDaySummaryList.Count; i++)
                {
                    if (e.PropertyName.Contains(CloseDaySummaryList[i].FieldName))
                    {
                        e.Column.ClipboardContentBinding.StringFormat = CloseDaySummaryList[i].Format;
                    }
                }
            }

            DataGridControl5.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            DayTallyList = MyXml.ReadOpenLotXml(ClassCommon.DayTallyXmlFilePath);
            if (DayTallyList.Count > 0)
            {
                for (int i = 0; i < DayTallyList.Count; i++)
                {
                    if (e.PropertyName.Contains(DayTallyList[i].FieldName))
                    {
                        e.Column.ClipboardContentBinding.StringFormat = DayTallyList[i].Format;
                    }
                }
            }
        }

        private void OnAutoGeneratingColumnShiftSummary(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridControl6.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            OpenShiftColumnList = MyXml.ReadOpenLotXml(ClassCommon.ShiftSummaryXmlFilePath);
            if (OpenShiftColumnList.Count > 0)
            {
                for (int i = 0; i < OpenShiftColumnList.Count; i++)
                {
                    if (e.PropertyName.Contains(OpenShiftColumnList[i].FieldName))
                    {
                        e.Column.ClipboardContentBinding.StringFormat = OpenShiftColumnList[i].Format;
                    }
                }
            }

            DataGridControl7.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            CloseShiftColumnList = MyXml.ReadOpenLotXml(ClassCommon.DayTallyXmlFilePath);
            if (CloseShiftColumnList.Count > 0)
            {
                for (int i = 0; i < CloseShiftColumnList.Count; i++)
                {
                    if (e.PropertyName.Contains(CloseShiftColumnList[i].FieldName))
                    {
                        e.Column.ClipboardContentBinding.StringFormat = CloseShiftColumnList[i].Format;
                    }
                }
            }

            DataGridControl8.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            TallyShiftColumnList = MyXml.ReadOpenLotXml(ClassCommon.ShiftTallyXmlFilePath);
            if (TallyShiftColumnList.Count > 0)
            {
                for (int i = 0; i < TallyShiftColumnList.Count; i++)
                {
                    if (e.PropertyName.Contains(TallyShiftColumnList[i].FieldName))
                    {
                        e.Column.ClipboardContentBinding.StringFormat = TallyShiftColumnList[i].Format;
                    }
                }
            }
        }

        private void Window_SizeChenged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            ClassCommon.ScrWidth = e.NewSize.Width;
            ClassCommon.ScrHeight = e.NewSize.Height;

            double dColHdrHeight = e.NewSize.Width * 0.025;
            double iGVFontsize = e.NewSize.Width * 0.009;

            DataGridControl.FontSize = iGVFontsize;
            DataGridControl.ColumnHeaderHeight = dColHdrHeight;
            DataGridControl.Width = e.NewSize.Width;// - e.NewSize.Width;
        }

        private void OnAutoGeneratingColumnPeriodSummary(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridControl16.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            OpenPeriodColumnList = MyXml.ReadOpenLotXml(ClassCommon.PeriodXmlFilePath);
            if (OpenPeriodColumnList.Count > 0)
            {
                for (int i = 0; i < OpenPeriodColumnList.Count; i++)
                {
                    if (e.PropertyName.Contains(OpenPeriodColumnList[i].FieldName))
                    {
                        e.Column.ClipboardContentBinding.StringFormat = OpenPeriodColumnList[i].Format;
                    }
                }
            }

            DataGridControl17.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            ClosePeriodColumnList = MyXml.ReadOpenLotXml(ClassCommon.PeriodXmlFilePath);
            if (ClosePeriodColumnList.Count > 0)
            {
                for (int i = 0; i < ClosePeriodColumnList.Count; i++)
                {
                    if (e.PropertyName.Contains(ClosePeriodColumnList[i].FieldName))
                    {
                        e.Column.ClipboardContentBinding.StringFormat = ClosePeriodColumnList[i].Format;
                    }
                }
            }

            DataGridControl18.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            TallyPeriodColumnList = MyXml.ReadOpenLotXml(ClassCommon.PeriodTallyXmlFilePath);
            if (TallyPeriodColumnList.Count > 0)
            {
                for (int i = 0; i < TallyPeriodColumnList.Count; i++)
                {
                    if (e.PropertyName.Contains(TallyPeriodColumnList[i].FieldName))
                    {
                        e.Column.ClipboardContentBinding.StringFormat = TallyPeriodColumnList[i].Format;
                    }
                }
            }
        }



        private void OnAutoGeneratingColumnPriorDay(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridControl10.Columns[0].Visibility = System.Windows.Visibility.Collapsed;

            TotalDayColumnList = MyXml.ReadOpenLotXml(ClassCommon.PeriodTallyXmlFilePath);


        }

        private void OnAutoGeneratingColumnPriorShift(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridControl11.Columns[0].Visibility = System.Windows.Visibility.Collapsed;


        }

        private void OnAutoGeneratingColumnPriorPeriod(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridControl12.Columns[0].Visibility = System.Windows.Visibility.Collapsed;

        }

    }
}
