using _8100UI.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;

namespace MaxBalesOnCon.ViewModels
{
    public class MaxBalesViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private Sqlhandler _sqlHandler = Sqlhandler.Instance;

        private int _conveyorId;
        public int ConveyorId
        {
            get { return _conveyorId; }
            set { SetProperty(ref _conveyorId, value); }
        }

        private List<int> _baleMaxList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        public List<int> BaleMaxList
        {
            get { return _baleMaxList; }
            set { SetProperty(ref _baleMaxList, value); }
        }

        private int _selectIdxVal;
        public int SelectIdxVal
        {
            get { return _selectIdxVal; }
            set { SetProperty(ref _selectIdxVal, value);}
        }

        public MaxBalesViewModel(IEventAggregator eventAggregator, int conveyorId)
        {
            this._eventAggregator = eventAggregator;
            this.ConveyorId = conveyorId;
         
        }

        private DelegateCommand _loadedPageICommand;
        public DelegateCommand LoadedPageICommand =>
        _loadedPageICommand ?? (_loadedPageICommand = new DelegateCommand(LoadPageExecute));
        private void LoadPageExecute()
        {
            for(int i = 0; i < ClassCommon.BaleStation.Count; i++)
            {
                if (ConveyorId == ClassCommon.BaleStation[i].OutPutCnv)
                {
                    SelectIdxVal = _sqlHandler.GetBalesOnConveyor(ClassCommon.BaleStation[i].StationId);
                }
            }
        }

        private DelegateCommand _closedPageICommand;
        public DelegateCommand ClosedPageICommand =>
        _closedPageICommand ?? (_closedPageICommand =
            new DelegateCommand(ClosedPageICommandxecute));

        private void ClosedPageICommandxecute()
        {
            
        }
        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
        _saveCommand ?? (_saveCommand =
            new DelegateCommand(SaveCommandExecute));
        private void SaveCommandExecute()
        {
            ClsPipeMessage.SendPipeMessage($"BOC;{ConveyorId};{SelectIdxVal};");
        }
    }
}
