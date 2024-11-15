using _8100UI.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JobParams.ViewModels
{
    public class JobParamsViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private Sqlhandler _sqlHandler;

        private string _jobNumber;
        public string JobNumber
        {
            get => _jobNumber;
            set => SetProperty(ref _jobNumber, value);
        }
        private string _balesToEOL;
        public string BalesToEOL
        {
            get => _balesToEOL;
            set => SetProperty(ref _balesToEOL, value);
        }
        private string _tare;
        public string Tare
        {
            get => _tare;
            set => SetProperty(ref _tare, value);
        }

        private List<string> _flagList;
        public List<string> FlagList
        {
            get => _flagList;
            set => SetProperty(ref _flagList, value);
        }

        private int _selectIndex;
        public int SelectIndex
        {
            get => _selectIndex;
            set => SetProperty(ref _selectIndex, value);
        }

        private bool _buttonEnable;
        public bool ButtonEnable
        {
            get { return _buttonEnable; }
            set 
            { 
                SetProperty(ref _buttonEnable, value);
                ButtonOpc = (value) ? "1.0" : "0.4";
            }
        }

        private string _buttonOpc;
        public string ButtonOpc
        {
            get => _buttonOpc;
            set => SetProperty(ref _buttonOpc, value);
        }

       
        public JobParamsViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<UpdateBaleMoveEvent>().Subscribe(UpdateBaleMoveEvents);
            _sqlHandler = Sqlhandler.Instance;

            ButtonEnable = false;
            FlagList = new List<string>() { "AU", "ST","GO"};
            SelectIndex = 0;
            _ = UpdateJobParamsAsync();
        }

        private void UpdateBaleMoveEvents(int obj)
        {
            _ = UpdateJobParamsAsync();
        }

        /// <summary>
        /// Update Box JobParams Box. UpdateBaleMoveEvent
        /// </summary>
        /// <returns></returns>
        private async Task UpdateJobParamsAsync()
        {
            string curLot = string.Empty;
            Single curBaleInLot = 0;

            string CurBaletable = await _sqlHandler.GetCurrentBaleTableNameAsync();
            string strbalequery = $"SELECT top  1 * from {CurBaletable} with (NOLOCK) WHERE LineID > 0 ORDER BY UID DESC SET STATISTICS TIME ON;";
            string strLotquery = $"SELECT TOP 2 * from RealTimeLotProc with (NOLOCK);";
        
            string strJobquery = $"SELECT TOP 1 * from OpenLots with (NOLOCK);"; //Not sure where to look for JobNum?

           

            try
            {
                await Task.Run(async () =>
                {
                    DataTable newBaleData = await _sqlHandler.GetForteDataTableAsync(strbalequery);
                    if (newBaleData.Rows.Count > 0)
                    {
                        curBaleInLot = Convert.ToSingle(newBaleData.Rows[0]["LotBaleNumber"]);
                        Tare = Convert.ToSingle(newBaleData.Rows[0]["TareWeight"]).ToString();

                        /* DBNULL
                        JobNumber = Convert.ToInt32(newBaleData.Rows[0]["JobNum"]).ToString();
                        ClsSerilog.LogMessage(ClsSerilog.Error, $"JobNumber -> { JobNumber}");
                        */

                        DataTable newLotData = _sqlHandler.GetRealTimeLotProc(strLotquery);
                        if (newLotData.Rows.Count > 0)
                        {
                            Single LotSize = Convert.ToSingle(newLotData.Rows[0]["Size"]);
                            if ((LotSize > 0) & (curBaleInLot > -1))
                                BalesToEOL = (LotSize - curBaleInLot).ToString();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateJobParamsAsync -> { ex.Message}");
                MessageBox.Show($"Error in UpdateJobParamsAsync -> { ex.Message}");
            }
        }


        private DelegateCommand _applyCommand;
        public DelegateCommand ApplyCommand =>
       _applyCommand ?? (_applyCommand =
            new DelegateCommand(ApplyCommandExecute).ObservesCanExecute(() => ButtonEnable));
        private void ApplyCommandExecute()
        {
           
        }

        private DelegateCommand _startCommand;
        public DelegateCommand StartCommand =>
       _startCommand ?? (_startCommand =
            new DelegateCommand(StartCommandExecute).ObservesCanExecute(() => ButtonEnable));
        private void StartCommandExecute()
        {
           
        }

    }
}
