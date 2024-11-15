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

namespace SheetCounter.ViewModels
{
    public class SheetCounterViewModel : BindableBase
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;
        private Sqlhandler SqlHandler;

        private string _targetWeight;
        public string TargetWeight
        {
            get => _targetWeight;
            set => SetProperty(ref _targetWeight, value);
        }
        private string _curSheetCount;
        public string CurSheetCount
        {
            get => _curSheetCount;
            set => SetProperty(ref _curSheetCount, value);
        }
        private string _avgWeight3D;
        public string AvgWeight3D
        {
            get => _avgWeight3D;
            set => SetProperty(ref _avgWeight3D, value);
        }
        private string _targetSheetCnt;
        public string TargetSheetCnt
        {
            get => _targetSheetCnt;
            set => SetProperty(ref _targetSheetCnt, value);
        }

        private bool _rbUseWeight;
        public bool RbUseWeight
        {
            get => _rbUseWeight;
            set
            {
                SetProperty(ref _rbUseWeight, value);
                if (value == true)
                {
                   // AsynchronousClient.StartClient($"UseWeight;");
                    ClsPipeMessage.SendPipeMessage($"UseWeight;");
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Sheetcount use Weight");
                }
            }
        }

        private bool _rbUseSheet;
        public bool RbUseSheet
        {
            get => _rbUseSheet;
            set
            {
                SetProperty(ref _rbUseSheet, value);
                if (value == true)
                {
                    //AsynchronousClient.StartClient($"UseSheet;");
                    ClsPipeMessage.SendPipeMessage($"UseSheet;");
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Sheetcount use Sheet");
                }
            }
        }

        public SheetCounterViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            SqlHandler = Sqlhandler.Instance;

            _eventAggregator.GetEvent<UpdateBaleMoveEvent>().Subscribe(UpdateBaleMoveEvents);
            _ = SetUpNewSheetCntAsync();
        }

        private DelegateCommand _sheetCounterCmd;
        public DelegateCommand SheetCounterCmd =>
       _sheetCounterCmd ?? (_sheetCounterCmd =
            new DelegateCommand(SheetCounterCmdExecute));
        private void SheetCounterCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"SheetCountSet;");
            ClsSerilog.LogMessage(ClsSerilog.Info, $"Sheetcount Set");
        }

        private void UpdateBaleMoveEvents(int obj)
        {
          //  DataTable newData = ClassCommon.NewDattable;

            if (obj == ClassCommon.ApproachConveyorLineOne)
            {
                _ = UpdateNewSheetCntAsync();
            }
        }

        private async Task UpdateNewSheetCntAsync()
        {
            CurSheetCount = string.Empty;
            AvgWeight3D = string.Empty;
            TargetSheetCnt = string.Empty;

            await Task.Run(() =>
            {
                using (DataTable sheetcountTable = SqlHandler.GetSheetCountTable())
                {
                    CurSheetCount = sheetcountTable.Rows[0]["CurShtCount"].ToString();
                    AvgWeight3D = String.Format("{0:0.00}", sheetcountTable.Rows[0]["LastAv"]);
                    TargetSheetCnt = sheetcountTable.Rows[0]["LastTargetSC"].ToString();
                }
                using (DataTable dropParamTable = SqlHandler.GetDropParamTable())
                {
                    TargetWeight = dropParamTable.Rows[0]["TargetWT"].ToString();
                }
            });
        }

        private async Task SetUpNewSheetCntAsync()
        {
            CurSheetCount = string.Empty;
            AvgWeight3D = string.Empty;
            TargetSheetCnt = string.Empty;
            object targettype = null;


            await Task.Run(() =>
            {
                using (DataTable sheetcountTable = SqlHandler.GetSheetCountTable())
                {
                    CurSheetCount = sheetcountTable.Rows[0]["CurShtCount"].ToString();
                    AvgWeight3D = String.Format("{0:0.00}", sheetcountTable.Rows[0]["LastAv"]);
                    TargetSheetCnt = sheetcountTable.Rows[0]["LastTargetSC"].ToString();

                    if (sheetcountTable.Rows[0]["UseSheetCount"].ToString() != string.Empty)
                    {
                        targettype = sheetcountTable.Rows[0]["UseSheetCount"];
                    }
                }

                using (DataTable dropParamTable = SqlHandler.GetDropParamTable())
                {
                    TargetWeight = dropParamTable.Rows[0]["TargetWT"].ToString();
                }

                if (targettype != null) RbUseSheet = true;
                else RbUseWeight = true;
            });
        }
    }
}
