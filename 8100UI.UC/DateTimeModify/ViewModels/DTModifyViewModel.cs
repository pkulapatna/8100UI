using _8100UI.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Data;

namespace DateTimeModify.ViewModels
{
    public class DTModifyViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

       // private readonly AccessHandler AcHandler;

        public DataTable BaleRecDateCodes { get; set; } //Time
        public DataTable BaleRecTimeCodes { get; set; } //Time

        private string _fdlValue;
        public string FdlValue
        {
            get { return _fdlValue; }
            set{SetProperty(ref _fdlValue, value);}
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

        private string _dateFormatVis;
        public string DateFormatVis
        {
            get { return _dateFormatVis; }
            set { SetProperty(ref _dateFormatVis, value); }
        }

        private List<string> _dateList;
        public List<string> DateList
        {
            get => _dateList; 
            set => SetProperty(ref _dateList, value); 
        }
        private string _selectDateVal;
        public string SelectDateVal
        {
            get => _selectDateVal;
            set 
            { 
                SetProperty(ref _selectDateVal, value);
                FdlValue = value;
            } 
        }
       

        private List<string> _timeList;
        public List<string> TimeList
        {
            get => _timeList;
            set => SetProperty(ref _timeList, value);
        }
        private string _selectTimeVal;
        public string SelectTimeVal
        {
            get => _selectTimeVal;
            set 
            {     
                SetProperty(ref _selectTimeVal, value);
                FdlValue = value;
            }
        }
        
        public DTModifyViewModel(IEventAggregator eventAggregator, string hdrName, string ObjectValue)
        {
            this._eventAggregator = eventAggregator;
            FieldName = hdrName;
            DBField = ObjectValue;
            FdlValue = ObjectValue;

            //AcHandler = new AccessHandler();

            DateList = new List<string>
            {
                "MM/dd/yy",
                "MM.dd.yy",
                "MM-dd-yy",
                "dd/MM/yy",
                "MM/dd/yyyy",
                "dd/MM/yyyy",
                "MM.dd.yy",
                "MM-dd-yy"
            };

            TimeList = new List<string>
            {
                "hh:mm:ss"
            };

            /*
            BaleRecDateCodes = AcHandler.GetBaleRecDTCodesTable("D");
            BaleRecTimeCodes = AcHandler.GetBaleRecDTCodesTable("T");


            if(BaleRecDateCodes.Rows.Count > 0)
            {
                

                for (int i = 1; i < BaleRecDateCodes.Rows.Count; i++)
                {
                    DateList.Add(BaleRecDateCodes.Rows[i]["Format"].ToString());
                }
            }
            */

            if (hdrName == "Format")
                DateFormatVis = "1";
            else
                DateFormatVis = "0";
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
        _saveCommand ?? (_saveCommand =
            new DelegateCommand(SaveCommandExecute));

        private void SaveCommandExecute()
        {
            _eventAggregator.GetEvent<ChangeFieldValue>().Publish(FdlValue);
            
        }


    }
}
