using _8100UI.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SummaryFieldsSelect.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows;

namespace Summary.ViewModels
{
    public class SummaryViewViewModel : BindableBase
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;

        private ObservableCollection<SqlLotSumFields> SelectedFieldColumn { get; set; }
        public ObservableCollection<SqlLotSumFields> SelectedClosedLotColumn { get; set; }
        public ObservableCollection<SqlLotSumFields> SelectedOpenLotColumn { get; set; }
        public ObservableCollection<SqlLotSumFields> PiorColumnList { get; set; }
        public ObservableCollection<SqlLotSumFields> ProdColumnList { get; set; }

        public ObservableCollection<SqlLotSumFields> PeriodColumnList { get; set; }
        public ObservableCollection<SqlLotSumFields> PeriodTallyColumnList { get; set; }

        private Sqlhandler SqlHandler;
        private Xmlhandler MyXml;
       // private AccessHandler AcHandler;

        public string CurDatatable { get; set; }

        public int MoistureType { get; set; }
        public int WeightUnit { get; set; }

        private DataTable _lvDatatable;
        public DataTable LvDatatable
        {
            get => _lvDatatable;
            set => SetProperty(ref _lvDatatable, value);
        }

        private int _baleSumDepth = 20;
        public int BaleSumDepth
        {
            get { return _baleSumDepth; }
            set => SetProperty(ref _baleSumDepth, value);
        }

        private ObservableCollection<SqlReportField> _xMLColumnList;
        public ObservableCollection<SqlReportField> XMLColumnList
        {
            get { return _xMLColumnList; }
            set { SetProperty(ref _xMLColumnList, value); }
        }


        private int _summaryTabIndex;
        public int SummaryTabIndex
        {
            get => _summaryTabIndex;
            set
            {
                SetProperty(ref _summaryTabIndex, value);
                DisplaySummaryof(value);
            }
        }

        private DataTable _openLotTable;
        public DataTable OpenLotTable
        {
            get => _openLotTable;
            set => SetProperty(ref _openLotTable, value);
        }

        List<string> OpenLotItems = new List<string>();

        private DataTable _closeLotTable;
        public DataTable CloseLotTable
        {
            get => _closeLotTable;
            set => SetProperty(ref _closeLotTable, value);
        }

        private DataTable _dayOpenTable;
        public DataTable DayOpenTable
        {
            get => _dayOpenTable;
            set => SetProperty(ref _dayOpenTable, value);
        }

        private DataTable _dayCloseTable;
        public DataTable DayCloseTable
        {
            get => _dayCloseTable;
            set => SetProperty(ref _dayCloseTable, value);
        }

        private DataTable _dayTallyTable;
        public DataTable DayTallyTable
        {
            get => _dayTallyTable;
            set => SetProperty(ref _dayTallyTable, value);
        }

        private DataTable _shiftOpenTable;
        public DataTable ShiftOpenTable
        {
            get => _shiftOpenTable;
            set => SetProperty(ref _shiftOpenTable, value);
        }

        private DataTable _shiftCloseTable;
        public DataTable ShiftCloseTable
        {
            get => _shiftCloseTable;
            set => SetProperty(ref _shiftCloseTable, value);
        }

        private DataTable _shiftTallyTable;
        public DataTable ShiftTallyTable
        {
            get => _shiftTallyTable;
            set => SetProperty(ref _shiftTallyTable, value);
        }

        private DataTable _dayPiorTable;
        public DataTable DayPiorTable
        {
            get => _dayPiorTable;
            set => SetProperty(ref _dayPiorTable, value);
        }

        private DataTable _shiftPiorTable;
        public DataTable ShiftPiorTable
        {
            get => _shiftPiorTable;
            set => SetProperty(ref _shiftPiorTable, value);
        }

        private DataTable _priodPiorTable;
        public DataTable PeriodPiorTable
        {
            get => _priodPiorTable;
            set => SetProperty(ref _priodPiorTable, value);
        }

        // Period Tab
        private DataTable _periodOpenTable;
        public DataTable PeriodOpenTable
        {
            get => _periodOpenTable;
            set => SetProperty(ref _periodOpenTable, value);
        }

        private DataTable _periodCloseTable;
        public DataTable PeriodCloseTable
        {
            get => _periodCloseTable;
            set => SetProperty(ref _periodCloseTable, value);
        }

        private DataTable _periodTallyTable;
        public DataTable PeriodTallyTable
        {
            get => _periodTallyTable;
            set => SetProperty(ref _periodTallyTable, value);
        }
        // End Period Tab



        private int _selecOpenLotIdx;
        public int SelecOpenLotIdx
        {
            get { return _selecOpenLotIdx; }
            set
            {
                SetProperty(ref _selecOpenLotIdx, value);
            }
        }
        private int _selectClosedLotIdx;
        public int SelectClosedLotIdx
        {
            get { return _selectClosedLotIdx; }
            set
            {
                SetProperty(ref _selectClosedLotIdx, value);
            }
        }

        private string _textMsg;
        public string TextMsg
        {
            get => _textMsg;
            set => SetProperty(ref _textMsg, value);
        }
        private string _textDebug;
        public string TextDebug
        {
            get => _textDebug;
            set => SetProperty(ref _textDebug, value);
        }

        public SummaryViewViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            SummaryTabIndex = 0;
            SqlHandler = Sqlhandler.Instance;
            MyXml = Xmlhandler.Instance;
            checkCreateXMLfiles();

            ClsSerilog.LogMessage(ClsSerilog.Info, $"Load SummaryViewViewModel -> {DateTime.Now}");

            _eventAggregator.GetEvent<UpdateBaleDataEvent>().Subscribe(BaleDataReadyEvent);

            _eventAggregator.GetEvent<SaveModFieldEvent>().Subscribe(UpdateSummaryListView);
            _eventAggregator.GetEvent<Reporttextmessage>().Subscribe(UpdateMessage);

            _eventAggregator.GetEvent<DebugTextmessage>().Subscribe(UpdateDebugMessage);
        }

        private void UpdateDebugMessage(string obj)
        {
            TextDebug = obj;
        }

        private void UpdateMessage(string obj)
        {
            TextMsg = obj;
        }

        private void UpdateSummaryListView(int obj)
        {
            DisplaySummaryof(SummaryTabIndex);
        }

        private void checkCreateXMLfiles()
        {
            MyXml.CheckandCreateXMLFiles(ClassCommon.BaleSummaryXmlFilePath, "BaleSummary");
            MyXml.CheckandCreateXMLFiles(ClassCommon.OpenLotSummaryXmlFilePath, "LotSummary");
            MyXml.CheckandCreateXMLFiles(ClassCommon.ClosedLotSummaryXmlFilePath, "LotSummary");
            MyXml.CheckandCreateXMLFiles(ClassCommon.DaySummaryXmlFilePath, "DaySummary");
            MyXml.CheckandCreateXMLFiles(ClassCommon.DayTallyXmlFilePath, "DayTally");
            MyXml.CheckandCreateXMLFiles(ClassCommon.PiorXmlFilePath, "PiorSummary");
            MyXml.CheckandCreateXMLFiles(ClassCommon.ShiftSummaryXmlFilePath, "ShiftSummary");
            MyXml.CheckandCreateXMLFiles(ClassCommon.ShiftTallyXmlFilePath, "ShiftTally");

            MyXml.CheckandCreateXMLFiles(ClassCommon.PeriodXmlFilePath, "PriodSummary");
            MyXml.CheckandCreateXMLFiles(ClassCommon.PeriodTallyXmlFilePath, "PriodTally");

        }

        private void BaleDataReadyEvent(int obj)
        {
            DisplaySummaryof(SummaryTabIndex);
        }

        private void DisplaySummaryof(int value)
        {
            switch (value)
            {
                case 0:
                    _ = UpdateBaleSummaryAsync();
                    break;

                case 1:
                    UpdateLotSummary();
                    break;

                case 2:
                    UpdateDaySummary();
                    break;

                case 3:
                    UpdateShiftSummary();
                    break;

                case 4:
                    UpdateProidSummary();
                    break;

                case 5:
                    UpdatePriorSummary();
                    break;

            }
        }

        private void UpdateProidSummary()
        {
            try
            {
                _ = GetPeriodTableAsync();

            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateProidSummary -> { ex.Message}");
                MessageBox.Show($"ERROR in UpdateProidSummary {ex.Message}");
            }
        }

       

        private void UpdatePriorSummary()
        {
            try
            {
                PiorColumnList = MyXml.ReadOpenLotXml(ClassCommon.PiorXmlFilePath);
                if (PiorColumnList.Count > 0)
                {
                    List<string> Fielditems = new List<string>();
                    for (int i = 0; i < PiorColumnList.Count; i++)
                    {
                        Fielditems.Add(PiorColumnList[i].FieldExp);
                    }
                    _ = SqlHandler.GetDayPiorAsync(Fielditems, ClassCommon.CloseDayTable).ContinueWith(t => UpdateDayPiorTable(t.Result));

                    _ = SqlHandler.GetShiftPiorAsync(Fielditems, ClassCommon.CloseShiftTable).ContinueWith(t => UpdateShiftPiorTable(t.Result));

                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdatePriorSummary -> { ex.Message}");
                MessageBox.Show($"ERROR in UpdatePriorSummary {ex.Message}");
            }
        }

        private void UpdateShiftSummary()
        {
            SqlHandler = Sqlhandler.Instance;
            MyXml = Xmlhandler.Instance;

            _ = GetOpenShiftTableAsync(ClassCommon.ShiftSummaryXmlFilePath);
            _ = GetCloseShiftTableAsync(ClassCommon.ShiftSummaryXmlFilePath);
            _ = GetShiftTallyTableAsyn(ClassCommon.ShiftTallyXmlFilePath);

        }

        private void UpdateDaySummary()
        {
            //SqlHandler = Sqlhandler.Instance;
            //MyXml = Xmlhandler.Instance;
            //AcHandler = new AccessHandler();

            _ = GetDayTableAsync(ClassCommon.DaySummaryXmlFilePath);
            _ = GetTallyTableAsync(ClassCommon.DayTallyXmlFilePath);
        }

        private async Task UpdateBaleSummaryAsync()
        {
           // ClsSerilog.LogMessage(ClsSerilog.Error, $"UpdateBaleSummary -> { DateTime.Now}");

            SqlHandler = Sqlhandler.Instance;
            MyXml = Xmlhandler.Instance;
            string CurBaleTable = await SqlHandler.GetCurrentBaleTableNameAsync();
            _ = UpdateSummaryDataGridAsync(BaleSumDepth, CurBaleTable);
        }

        private void UpdateLotSummary()
        {
            SqlHandler = Sqlhandler.Instance;
            MyXml = Xmlhandler.Instance;

            var tasks = new List<Task>
            {
                Task.Run(() => _ = GetLotsTableAsync(ClassCommon.OpenLot)),
                Task.Run(() => _ = GetLotsTableAsync(ClassCommon.CloseLot))
            };
            Task t = Task.WhenAll(tasks);
            try
            {
                t.Wait();
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateLotSummary -> { ex.Message}");
                MessageBox.Show("ERROR in UpdateLotSummary " + ex.Message);
            }

        }


        private async Task UpdateSummaryDataGridAsync(int baleSumDepth, string curDatatable)
        {
            DataTable Mytable = new DataTable();
            List<int> blanklist = new List<int>();
            string strItems = string.Empty;
            char[] charsToTrim = { ',' };
            string strquery = string.Empty;
            string strItem = string.Empty;

            MoistureType = ClassCommon.MoistureType;
            WeightUnit = ClassCommon.WeightUnit;

          
            try
            {
                await Task.Run(async () =>
                {
                    SelectedFieldColumn = MyXml.ReadOpenLotXml(ClassCommon.BaleSummaryXmlFilePath);
                    if (SelectedFieldColumn.Count > 0)
                    {
                        List<string> Fielditems = new List<string>();
                        for (int i = 0; i < SelectedFieldColumn.Count; i++)
                        {
                            strItem += SelectedFieldColumn[i].FieldExp + ",";
                        }

                        strItem = strItem.TrimEnd(charsToTrim);
                        strquery = $"SELECT top  {baleSumDepth} {strItem} from [ForteData].[dbo].[{curDatatable}] with (NOLOCK)  ORDER BY UID DESC SET STATISTICS TIME ON;";

                        //ClsSerilog.LogMessage(ClsSerilog.Info, $"UpdateSummaryDataGridAsync  -> {strquery} ");

                        Mytable = await SqlHandler.GetForteDataTableAsync(strquery);
                        UpdateScreenLstView(Mytable);
                    }
                });

            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateSummaryDataGridAsync -> { ex.Message}");
                MessageBox.Show($"ERROR in UpdateSummaryDataGridAsync= {ex.Message}");
            }
        }


        private void UpdateScreenLstView(DataTable result)
        {
//            ClsSerilog.LogMessage(ClsSerilog.Info, $"UpdateScreenLstView  -> { result.Rows[0][0] } { result.Rows[0][1] } { result.Rows[0][2]} { result.Rows[0][3] }");

            DataRow[] rows = result.Select();

            try
            {
                for (int i = 0; i < result.Columns.Count; i++)
                {
                    result.Columns[i].ColumnName = SelectedFieldColumn[i].FieldName;
                    result.AcceptChanges();
                }

                foreach (DataColumn column in result.Columns)
                {
                    if (column.ColumnName == "Column1")
                        column.ColumnName = "IW @CCKg";
                }

                for (int i = 0; i < rows.Length; i++)
                {
                    string Weight = ClassCommon.GetWeightVal((float)result.Rows[i]["Weight"]);
                    string strMoisture = ClassCommon.GetMoistureVal((float)result.Rows[i]["Moisture"]);

                    result.Rows[i]["Weight"] = Weight; // String.Format("{0:0.00}", Mytable.Rows[i]["Weight"]);
                    result.Rows[i]["Moisture"] = strMoisture; //  String.Format("{0:0.00}", Mytable.Rows[i]["Moisture"]);
                    result.AcceptChanges();
                }
                LvDatatable = result;
               

            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateScreenLstView -> { ex.Message}");
                MessageBox.Show($"ERROR in UpdateScreenLstView= {ex.Message}");
            }
        }

        public void UpDateTable()
        {
            _ = UpdateSummaryDataGridAsync(BaleSumDepth, CurDatatable);
        }

        public async Task GetLotsTableAsync(int Lotid)
        {
            OpenLotItems.Clear();

            try
            {
                await Task.Run(() =>
                {
                    if (Lotid == 0) // Open Lot
                    {
                        SelectedOpenLotColumn = MyXml.ReadOpenLotXml(ClassCommon.OpenLotSummaryXmlFilePath);

                        if (SelectedOpenLotColumn.Count > 0)
                        {
                            List<string> Fielditems = new List<string>();
                            for (int i = 0; i < SelectedOpenLotColumn.Count; i++)
                            {
                                Fielditems.Add(SelectedOpenLotColumn[i].FieldExp);
                            }

                            OpenLotItems = Fielditems;

                            //DataTable oLptTable = AcHandler.GetLotTable(Fielditems, Lotid);

                            DataTable oLptTable = SqlHandler.GetOpenLotTable(Fielditems, Lotid);

                            if (oLptTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < oLptTable.Columns.Count; i++)
                                {
                                    oLptTable.Columns[i].ColumnName = SelectedOpenLotColumn[i].FieldName;
                                    oLptTable.AcceptChanges();
                                }
                                OpenLotTable = oLptTable;
                            }
                        }
                    }
                    else if (Lotid == 1) //Closed Lot
                    {
                        SelectedClosedLotColumn = MyXml.ReadOpenLotXml(ClassCommon.ClosedLotSummaryXmlFilePath);
                        if (SelectedClosedLotColumn.Count > 0)
                        {
                            List<string> Fielditems = new List<string>();
                            for (int i = 0; i < SelectedClosedLotColumn.Count; i++)
                            {
                                Fielditems.Add(SelectedClosedLotColumn[i].FieldExp);
                            }

                            //DataTable cLptTable = AcHandler.GetLotTable(Fielditems, Lotid);
                            //_ = AcHandler.GetLotTableAsync(lotitems, Lotid).ContinueWith(t => GeClosedLotTable(t.Result));

                            DataTable cLptTable = SqlHandler.GetOpenLotTable(Fielditems, Lotid);
                            for (int i = 0; i < cLptTable.Columns.Count; i++)
                            {
                                cLptTable.Columns[i].ColumnName = SelectedClosedLotColumn[i].FieldName;
                                cLptTable.AcceptChanges();
                            }
                            CloseLotTable = cLptTable;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetLotsTableAsync -> { ex.Message}");
                MessageBox.Show("ERROR in GetLotsTableAsync " + ex.Message);
            }
        }

        private async Task GetDayTableAsync(string filePatch)
        {
            DataTable dayOTable = new DataTable();
            DataTable dayCTable = new DataTable();
         
            try
            {
                await Task.Run(() =>
                {
                    SelectedFieldColumn = MyXml.ReadOpenLotXml(filePatch);
                    if (SelectedFieldColumn.Count > 0)
                    {
                        List<string> Fielditems = new List<string>();
                        for (int i = 0; i < SelectedFieldColumn.Count; i++)
                        {
                            Fielditems.Add(SelectedFieldColumn[i].FieldExp);
                        }

                        using (dayOTable = SqlHandler.GetDayTable(Fielditems, ClassCommon.OpenDayTable))
                        {
                            if (dayOTable.Rows.Count > 0)
                            {
                                SelectedFieldColumn = MyXml.ReadOpenLotXml(ClassCommon.DaySummaryXmlFilePath);
                                if (SelectedFieldColumn.Count > 0)
                                {
                                    for (int i = 0; i < dayOTable.Columns.Count; i++)
                                    {
                                        dayOTable.Columns[i].ColumnName = SelectedFieldColumn[i].FieldName;
                                        dayOTable.AcceptChanges();
                                    }
                                }
                                DayOpenTable = dayOTable;
                            }
                        }

                        using (dayCTable = SqlHandler.GetDayTable(Fielditems, ClassCommon.CloseDayTable))
                        {
                            if (dayCTable.Rows.Count > 0)
                            {
                                SelectedFieldColumn = MyXml.ReadOpenLotXml(ClassCommon.DaySummaryXmlFilePath);
                                if (SelectedFieldColumn.Count > 0)
                                {
                                    for (int i = 0; i < dayCTable.Columns.Count; i++)
                                    {
                                        dayCTable.Columns[i].ColumnName = SelectedFieldColumn[i].FieldName;
                                        dayCTable.AcceptChanges();
                                    }
                                }
                                DayCloseTable = dayCTable;
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetDayTable -> { ex.Message}");
                MessageBox.Show($"ERROR in GetDayTable {ex.Message}");
            }
        }

        public async Task GetTallyTableAsync(string filePath)
        {
            DataTable mytable = new DataTable();
            try
            {
                await Task.Run(() =>
                {
                    SelectedFieldColumn = MyXml.ReadOpenLotXml(filePath);
                    if (SelectedFieldColumn.Count > 0)
                    {
                        List<string> Fielditems = new List<string>();
                        for (int i = 0; i < SelectedFieldColumn.Count; i++)
                        {
                            Fielditems.Add(SelectedFieldColumn[i].FieldExp);
                        }
                        mytable = SqlHandler.GetDayTable(Fielditems, ClassCommon.DayTallyTable);

                        SelectedFieldColumn = MyXml.ReadOpenLotXml(ClassCommon.DayTallyXmlFilePath);
                        if (SelectedFieldColumn.Count > 0)
                        {
                            for (int i = 0; i < mytable.Columns.Count; i++)
                            {
                                mytable.Columns[i].ColumnName = SelectedFieldColumn[i].FieldName;
                                mytable.AcceptChanges();
                            }
                        }
                    }
                    DayTallyTable = mytable;
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetTallyTable -> { ex.Message}");
                MessageBox.Show($"ERROR in GetTallyTable {ex.Message}");
            }
        }

        private async Task GetOpenShiftTableAsync(string filePath)
        {
            DataTable OpenShiftTable = new DataTable();

            try
            {
                await Task.Run(() =>
                {
                    SelectedFieldColumn = MyXml.ReadOpenLotXml(filePath);
                    if (SelectedFieldColumn.Count > 0)
                    {
                        List<string> Fielditems = new List<string>();
                        for (int i = 0; i < SelectedFieldColumn.Count; i++)
                        {
                            Fielditems.Add(SelectedFieldColumn[i].FieldExp);
                        }
                        OpenShiftTable = SqlHandler.GetShiftTable(Fielditems, ClassCommon.OpenShiftTable);
                        if (OpenShiftTable.Rows.Count > 0)
                        {
                            SelectedFieldColumn = MyXml.ReadOpenLotXml(ClassCommon.ShiftSummaryXmlFilePath);
                            {
                                for (int i = 0; i < OpenShiftTable.Columns.Count; i++)
                                {
                                    OpenShiftTable.Columns[i].ColumnName = SelectedFieldColumn[i].FieldName;
                                    OpenShiftTable.AcceptChanges();
                                }
                            }
                        }
                    }
                    ShiftOpenTable = OpenShiftTable;
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetShiftTable -> { ex.Message}");
                MessageBox.Show($"ERROR in GetShiftTable {ex.Message}");
            }
        }
        private async Task GetCloseShiftTableAsync(string filePath)
        {
            DataTable CloseShiftTable = new DataTable();

            try
            {
                await Task.Run(() =>
                {
                    SelectedFieldColumn = MyXml.ReadOpenLotXml(filePath);
                    if (SelectedFieldColumn.Count > 0)
                    {
                        List<string> Fielditems = new List<string>();
                        for (int i = 0; i < SelectedFieldColumn.Count; i++)
                        {
                            Fielditems.Add(SelectedFieldColumn[i].FieldExp);
                        }
                        CloseShiftTable = SqlHandler.GetShiftTable(Fielditems, ClassCommon.CloseShiftTable);
                        if (CloseShiftTable.Rows.Count > 0)
                        {
                            SelectedFieldColumn = MyXml.ReadOpenLotXml(ClassCommon.ShiftSummaryXmlFilePath);
                            {
                                for (int i = 0; i < CloseShiftTable.Columns.Count; i++)
                                {
                                    CloseShiftTable.Columns[i].ColumnName = SelectedFieldColumn[i].FieldName;
                                    CloseShiftTable.AcceptChanges();
                                }
                            }
                        }
                    }
                    ShiftCloseTable = CloseShiftTable;
                });

            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetCloseShiftTableAsync -> { ex.Message}");
                MessageBox.Show($"ERROR in GetCloseShiftTableAsync {ex.Message}");
            }
        }

        private async Task GetShiftTallyTableAsyn(string filePatch)
        {
            DataTable TallyTable = new DataTable();

            try
            {
                await Task.Run(() =>
                {
                    SelectedFieldColumn = MyXml.ReadOpenLotXml(filePatch);
                    if (SelectedFieldColumn.Count > 0)
                    {
                        List<string> Fielditems = new List<string>();
                        for (int i = 0; i < SelectedFieldColumn.Count; i++)
                        {
                            Fielditems.Add(SelectedFieldColumn[i].FieldExp);
                        }
                        TallyTable = SqlHandler.GetShiftTable(Fielditems, ClassCommon.ShiftTallyTable);

                        SelectedFieldColumn = MyXml.ReadOpenLotXml(ClassCommon.ShiftTallyXmlFilePath);
                        if (SelectedFieldColumn.Count > 0)
                        {
                            for (int i = 0; i < TallyTable.Columns.Count; i++)
                            {
                                TallyTable.Columns[i].ColumnName = SelectedFieldColumn[i].FieldName;
                                TallyTable.AcceptChanges();
                            }
                        }
                    }
                    ShiftTallyTable = TallyTable;
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetTallyTable -> { ex.Message}");
                MessageBox.Show($"ERROR in GetTallyTable {ex.Message}");
            }
        }

        private async Task GetPiorTableAsync()
        {
            DataTable dayP = new DataTable();
            DataTable shiftP = new DataTable();

            try
            {
                await Task.Run(() =>
                {
                    PiorColumnList = MyXml.ReadOpenLotXml(ClassCommon.PiorXmlFilePath);
                    if (PiorColumnList.Count > 0)
                    {
                        List<string> Fielditems = new List<string>();
                        for (int i = 0; i < PiorColumnList.Count; i++)
                        {
                            Fielditems.Add(PiorColumnList[i].FieldExp);
                        }

                        using (dayP = SqlHandler.GetDayPior(Fielditems, ClassCommon.CloseDayTable))
                        {
                            if (dayP.Rows.Count > 0)
                            {
                                PiorColumnList = MyXml.ReadOpenLotXml(ClassCommon.PiorXmlFilePath);
                                if (PiorColumnList.Count > 0)
                                {
                                    for (int i = 0; i < dayP.Columns.Count; i++)
                                    {
                                        dayP.Columns[i].ColumnName = PiorColumnList[i].FieldName;
                                        dayP.AcceptChanges();
                                    }
                                }
                                DayPiorTable = dayP;
                            }
                        }

                        using (shiftP = SqlHandler.GetShiftPior(Fielditems, ClassCommon.CloseShiftTable))
                        {
                            if (shiftP.Rows.Count > 0)
                            {
                                PiorColumnList = MyXml.ReadOpenLotXml(ClassCommon.PiorXmlFilePath);
                                if (PiorColumnList.Count > 0)
                                {
                                    for (int i = 0; i < shiftP.Columns.Count; i++)
                                    {
                                        shiftP.Columns[i].ColumnName = PiorColumnList[i].FieldName;
                                        shiftP.AcceptChanges();
                                    }
                                }
                                ShiftPiorTable = shiftP;
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetPiorTable -> { ex.Message}");
                MessageBox.Show($"ERROR in GetPiorTable {ex.Message}");
            }
        }

        private async Task GetPeriodTableAsync()
        {
            DataTable OpenPeriodTable = new DataTable();
            DataTable ClosePeriodTable = new DataTable();
            DataTable TallyPeriodTable = new DataTable();

            try
            {
                await Task.Run(() =>
                {
                    PeriodColumnList = MyXml.ReadOpenLotXml(ClassCommon.PeriodXmlFilePath); //PriodTable
                    if (PeriodColumnList.Count >0)
                    {
                        List<string> Fielditems = new List<string>();
                        for (int i = 0; i < PeriodColumnList.Count; i++)
                        {
                            Fielditems.Add(PeriodColumnList[i].FieldExp);
                        }
                        using (OpenPeriodTable = SqlHandler.GetPriodTable(Fielditems, ClassCommon.OpenPriodTable))
                        {
                            if (OpenPeriodTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < OpenPeriodTable.Columns.Count; i++)
                                {
                                    OpenPeriodTable.Columns[i].ColumnName = PeriodColumnList[i].FieldName;
                                    OpenPeriodTable.AcceptChanges();
                                }
                            }
                            PeriodOpenTable = OpenPeriodTable;
                        }

                        using (OpenPeriodTable = SqlHandler.GetPriodTable(Fielditems, ClassCommon.ClosePriodTable))
                        {
                            if (OpenPeriodTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < ClosePeriodTable.Columns.Count; i++)
                                {
                                    ClosePeriodTable.Columns[i].ColumnName = PeriodColumnList[i].FieldName;
                                    ClosePeriodTable.AcceptChanges();
                                }
                            }
                            PeriodCloseTable = ClosePeriodTable;
                        }
         
               
                        PeriodTallyColumnList = MyXml.ReadOpenLotXml(ClassCommon.PeriodTallyXmlFilePath);
                        List<string> Totalitems = new List<string>();
                        for (int i = 0; i < PeriodTallyColumnList.Count; i++)
                        {
                            Totalitems.Add(PeriodTallyColumnList[i].FieldExp);
                        }

                        using (TallyPeriodTable = SqlHandler.GetPriodTable(Totalitems, ClassCommon.PriodTallyTable))
                        {
                            if (TallyPeriodTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < TallyPeriodTable.Columns.Count; i++)
                                {
                                    TallyPeriodTable.Columns[i].ColumnName = PeriodTallyColumnList[i].FieldName;
                                    TallyPeriodTable.AcceptChanges();
                                }
                            }
                        }
                        PeriodTallyTable = TallyPeriodTable;
                        
                    }
                });

            }
            catch (Exception ex )
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetPiorTable -> { ex.Message}");
                MessageBox.Show($"ERROR in GetPiorTable {ex.Message}");
                
            }

        }


        private void UpdateDayPiorTable(DataTable result)
        {
            try
            {
                PiorColumnList = MyXml.ReadOpenLotXml(ClassCommon.PiorXmlFilePath);
                if (PiorColumnList.Count > 0)
                {
                    for (int i = 0; i < result.Columns.Count; i++)
                    {
                        result.Columns[i].ColumnName = PiorColumnList[i].FieldName;
                        result.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateDayPiorTable -> { ex.Message}");
                MessageBox.Show($"ERROR in UpdateDayPiorTable {ex.Message}");
            }
            DayPiorTable = result;
        }

        private void UpdateShiftPiorTable(DataTable result)
        {
            try
            {
                PiorColumnList = MyXml.ReadOpenLotXml(ClassCommon.PiorXmlFilePath);
                if (PiorColumnList.Count > 0)
                {
                    for (int i = 0; i < result.Columns.Count; i++)
                    {
                        result.Columns[i].ColumnName = PiorColumnList[i].FieldName;
                        result.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateShiftPiorTable -> { ex.Message}");
                MessageBox.Show($"ERROR in UpdateShiftPiorTable {ex.Message}");
            }
            ShiftPiorTable = result;
        }

        //BaleSummary Dispalay options
        private DelegateCommand _columnConfigCommand;
        public DelegateCommand ColumnConfigCommand =>
       _columnConfigCommand ?? (_columnConfigCommand =
            new DelegateCommand(ColumnConfigCommandExecute));
        private void ColumnConfigCommandExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "Bale Summary Fields Selection",
                Width = 1120,
                Height = 520,
                ResizeMode = ResizeMode.NoResize,
                Content = new SummaryFieldsView(_eventAggregator, ClassCommon.BaleTable)
            };
            _setupWindow.ShowDialog();
        }

        //Open Lot Summary
        private DelegateCommand _openLotFieldsCommand;
        public DelegateCommand OpenLotFieldsCommand =>
            _openLotFieldsCommand ?? (_openLotFieldsCommand =
                new DelegateCommand(OpenLotFieldsCommandExecute));
        private void OpenLotFieldsCommandExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "OpenLot Summary Fields Selection",
                Width = 1120,
                Height = 520,
                ResizeMode = ResizeMode.NoResize,
                Content = new SummaryFieldsView(_eventAggregator, ClassCommon.OpenLot)

            };
            _setupWindow.ShowDialog();
        }

        //Close Lot Summary
        private DelegateCommand _closeLotFieldsCommand;
        public DelegateCommand CloseLotFieldsCommand =>
            _closeLotFieldsCommand ?? (_closeLotFieldsCommand =
                new DelegateCommand(CloseLotFieldsCommandExecute));
        private void CloseLotFieldsCommandExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "CloseLot Summary Fields Selection",
                Width = 1120,
                Height = 520,
                ResizeMode = ResizeMode.NoResize,
                Content = new SummaryFieldsView(_eventAggregator, ClassCommon.CloseLot)
            };
            _setupWindow.ShowDialog();

        }

        //Close open Lot button
        private DelegateCommand _closedSelLotCommand;
        public DelegateCommand ClosedSelLotCommand =>
            _closedSelLotCommand ?? (_closedSelLotCommand =
                new DelegateCommand(ClosedSelLotCommandExecute));
        private void ClosedSelLotCommandExecute()
        {
            // DataTable RawOpenLotTable = AcHandler.GetLotTable(OpenLotItems, 0);
            DataTable RawOpenLotTable = SqlHandler.GetOpenLotTable(OpenLotItems, 0);

            if ((RawOpenLotTable.Rows.Count > 0) & (SelecOpenLotIdx > -1))
            {
                int LotNumber = (int)RawOpenLotTable.Rows[SelecOpenLotIdx]["LotNum"];

                if (System.Windows.MessageBox.Show($"Are you Sure, you want to Close {LotNumber} ?", "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Clossing Open Lot -> {LotNumber}");

                    ClsPipeMessage.SendPipeMessage($"ClosedLotUID; {LotNumber};");
                    //Force lot to close, Tell the world the lot is closed!
                    _eventAggregator.GetEvent<UpdateBaleDataEvent>().Publish(0);
                }
            }
            else
            {
                MessageBox.Show("No Open Lot!");
                ClsSerilog.LogMessage(ClsSerilog.Info, $"No Open Lot Data");
            }
        }

        private DelegateCommand _daySummaryCommand;
        public DelegateCommand DaySummaryCommand =>
            _daySummaryCommand ?? (_daySummaryCommand =
                new DelegateCommand(DaySummaryCommandExecute));
        private void DaySummaryCommandExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "Day Summary Fields Selection",
                Width = 1120,
                Height = 520,
                ResizeMode = ResizeMode.NoResize,
                Content = new SummaryFieldsView(_eventAggregator, ClassCommon.OpenDayTable)
            };
            _setupWindow.ShowDialog();
        }

        private DelegateCommand _dayTallyCommand;
        public DelegateCommand DayTallyCommand =>
            _dayTallyCommand ?? (_dayTallyCommand =
                new DelegateCommand(DayTallyCommandExecute));

        private void DayTallyCommandExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "Day Tally Fields Selection",
                Width = 1120,
                Height = 520,
                ResizeMode = ResizeMode.NoResize,
                Content = new SummaryFieldsView(_eventAggregator, ClassCommon.DayTallyTable)

            };
            _setupWindow.ShowDialog();

        }

        private DelegateCommand _shiftSummaryCommand;
        public DelegateCommand ShiftSummaryCommand =>
            _shiftSummaryCommand ?? (_shiftSummaryCommand =
                new DelegateCommand(ShiftSummaryCommandExecute));

        private void ShiftSummaryCommandExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "Shift Summary Fields Selection",
                Width = 1120,
                Height = 520,
                ResizeMode = ResizeMode.NoResize,
                Content = new SummaryFieldsView(_eventAggregator, ClassCommon.OpenShiftTable)
            };
            _setupWindow.ShowDialog();
        }


        private DelegateCommand _shiftTallyCommand;
        public DelegateCommand ShiftTallyCommand =>
            _shiftTallyCommand ?? (_shiftTallyCommand =
                new DelegateCommand(ShiftTallyCommandExecute));

        private void ShiftTallyCommandExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "Shift Tally Fields Selection",
                Width = 1120,
                Height = 520,
                ResizeMode = ResizeMode.NoResize,
                Content = new SummaryFieldsView(_eventAggregator, ClassCommon.ShiftTallyTable)

            };
            _setupWindow.ShowDialog();
        }




        private DelegateCommand _periodSummaryCommand;
        public DelegateCommand PeriodSummaryCommand =>
            _periodSummaryCommand ?? (_periodSummaryCommand =
                new DelegateCommand(PeriodSummaryCommandExecute));
        private void PeriodSummaryCommandExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "Period Fields Selection",
                Width = 1120,
                Height = 520,
                ResizeMode = ResizeMode.NoResize,
                Content = new SummaryFieldsView(_eventAggregator, ClassCommon.OpenPriodTable)

            };
            _setupWindow.ShowDialog();
        }

        private DelegateCommand _periodTallyCommand;
        public DelegateCommand PeriodTallyCommand =>
            _periodTallyCommand ?? (_periodTallyCommand =
                new DelegateCommand(PeriodTallyCommandExecute));
        private void PeriodTallyCommandExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "Period Tally Fields Selection",
                Width = 1120,
                Height = 520,
                ResizeMode = ResizeMode.NoResize,
                Content = new SummaryFieldsView(_eventAggregator, ClassCommon.PriodTallyTable)

            };
            _setupWindow.ShowDialog();
        }



        private DelegateCommand _piorSummaryCommand;
        public DelegateCommand PiorSummaryCommand =>
            _piorSummaryCommand ?? (_piorSummaryCommand =
                new DelegateCommand(PiorSummaryCommandExecute));
        private void PiorSummaryCommandExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "Prior Summary Fields Selection",
                Width = 1120,
                Height = 520,
                ResizeMode = ResizeMode.NoResize,
                Content = new SummaryFieldsView(_eventAggregator, ClassCommon.PiorTable)
            };
            _setupWindow.ShowDialog();
        }

        private DelegateCommand _openUnitFieldsCommand;
        public DelegateCommand OpenUnitFieldsCommand =>
            _openUnitFieldsCommand ?? (_openUnitFieldsCommand =
                new DelegateCommand(OpenUnitFieldsCommandExecute));
        private void OpenUnitFieldsCommandExecute()
        {
            
        }

        private DelegateCommand _closeUnitFieldsCommand;
        public DelegateCommand CloseUnitFieldsCommand =>
            _closeUnitFieldsCommand ?? (_closeUnitFieldsCommand =
                new DelegateCommand(CloseUnitFieldsCommandExecute));
        private void CloseUnitFieldsCommandExecute()
        {
           

        }

        private DelegateCommand _closedSelUnitCommand;
        public DelegateCommand ClosedSelUnitCommand =>
            _closedSelUnitCommand ?? (_closedSelUnitCommand =
                new DelegateCommand(ClosedSelUnitCommandExecute));
        private void ClosedSelUnitCommandExecute()
        {
           
        }

       
    }
}
