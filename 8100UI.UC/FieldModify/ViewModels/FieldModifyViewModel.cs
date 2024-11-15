using _8100UI.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace FieldModify.ViewModels
{
    public class FieldModifyViewModel : BindableBase
    {
        protected readonly IEventAggregator _eventAggregator;


        private bool _bModify = false;
        public bool BModify
        {
            get { return _bModify; }
            set
            {
                SetProperty(ref _bModify, value);
                BReadonly = !value;
            }
        }

        private bool _bReadonly = true;
        public bool BReadonly
        {
            get { return _bReadonly; }
            set { SetProperty(ref _bReadonly, value); }
        }

        private string _fdlValue;
        public string FdlValue
        {
            get { return _fdlValue; }
            set 
            { 
                SetProperty(ref _fdlValue, value);
                if(value == "0")
                {
                    SetProperty(ref _fdlValue, DBField);
                }
            }
        }

        private string _dBField;
        public string DBField
        {
            get { return _dBField; }
            set { SetProperty(ref _dBField, value); }
        }

        private string _fieldName;
        public string FieldName
        {
            get { return _fieldName; }
            set { SetProperty(ref _fieldName, value); }
        }

        private string _fieldType;
        public string FieldType
        {
            get => _fieldType; 
            set => SetProperty(ref _fieldType, value); 
        }


        private int _headerIndex;
        public int HeaderIndex
        {
            get => _headerIndex;
            set => SetProperty(ref _headerIndex, value);
        }

        public FieldModifyViewModel(IEventAggregator eventAggregator, string ObjectValue, string hdrName, string fieldType)
        {
            this._eventAggregator = eventAggregator;
        
            this.FieldName = hdrName;
            this.DBField = ObjectValue;
            this.FdlValue = ObjectValue;
            this.FieldType = fieldType;

        }

        private DelegateCommand _settingsCommand;
        public DelegateCommand SettingsCommand =>
        _settingsCommand ?? (_settingsCommand = new DelegateCommand(SettingsCommandExecute));
        private void SettingsCommandExecute()
        {
            BModify = true;
        }

        /// <summary>
        /// SendPipeMsg new value to main program
        /// </summary>
        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
        _saveCommand ?? (_saveCommand =
            new DelegateCommand(SaveCommandExecute));
        private void SaveCommandExecute()
        {
            switch (FieldName)
            {
                case "Next SerialNumber":
                   // ClsPipeMessage.SendPipeMessage($"NSN;{FdlValue};");
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Change Next Serial Number {FdlValue}");
                    BModify = false;
                    break;

                case "LotSize":
                   // ClsPipeMessage.SendPipeMessage($"LSZ;{FdlValue};");
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Change Lot Size {FdlValue}");
                    BModify = false;
                    break;

                case "Next LotNumber":
                  //  ClsPipeMessage.SendPipeMessage($"NSN;{FdlValue};");
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Change Next Serial Number {FdlValue}");
                    BModify = false;
                    break;

                default:
                 //  _eventAggregator.GetEvent<ChangeFieldValue>().Publish(FdlValue);
                    break;
            }
            BModify = false;   
        }
    }
}
