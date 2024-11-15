using _8100UI.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;

namespace SetUp.ViewModels
{
    public class SetUpViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
      
        public SetUpViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            setLastModDate();
        }

        private string _txtAppVer;
        public string TxtAppVer
        {
            get { return _txtAppVer; }
            set { SetProperty(ref _txtAppVer, value); }
        }


        private bool _bModify = false;
        public bool BModify
        {
            get { return _bModify; }
            set { SetProperty(ref _bModify, value); }
        }
        public int MoistureTypeSelect { get; set; }

       
        //-- Med Boxes--------------------------------------------------------------

        private bool _boxOneChecked = ClassCommon.BoxOneCheck;
        public bool BoxOneChecked
        {
            get { return _boxOneChecked; }
            set { SetProperty(ref _boxOneChecked, value); }
        }

        private bool _boxTwoChecked = ClassCommon.BoxTwoCheck;
        public bool BoxTwoChecked
        {
            get { return _boxTwoChecked; }
            set { SetProperty(ref _boxTwoChecked, value); }
        }

        private bool _boxThreeChecked = ClassCommon.BoxThreeCheck;
        public bool BoxThreeChecked
        {
            get { return _boxThreeChecked; }
            set { SetProperty(ref _boxThreeChecked, value); }
        }

        private bool _boxFourChecked = ClassCommon.BoxFourCheck;
        public bool BoxFourChecked
        {
            get { return _boxFourChecked; }
            set { SetProperty(ref _boxFourChecked, value); }
        }

        private bool _boxFiveChecked = ClassCommon.BoxFiveCheck;
        public bool BoxFiveChecked
        {
            get { return _boxFiveChecked; }
            set { SetProperty(ref _boxFiveChecked, value); }
        }

        private bool _boxSixChecked = ClassCommon.BoxSixCheck;
        public bool BoxSixChecked
        {
            get { return _boxSixChecked; }
            set { SetProperty(ref _boxSixChecked, value); }
        }

        //-----------------------------------------------------------------
     
      
        private string _weightUnit;
        public string WeightUnit
        {
            get { return _weightUnit; }
            set { SetProperty(ref _weightUnit, value); }
        }

        private string _moistureType;
        public string MoistureType
        {
            get { return _moistureType; }
            set { SetProperty(ref _moistureType, value); }
        }

        private void setLastModDate()
        {

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(assembly.Location);
            DateTime lastModified = fileInfo.LastWriteTime;

            TxtAppVer = lastModified.ToString();
        }

        public DelegateCommand _loadedPageICommand;
        public DelegateCommand LoadedPageICommand =>
        _loadedPageICommand ?? (_loadedPageICommand =
          new DelegateCommand(LoadedPageICommandExecute));
        private void LoadedPageICommandExecute()
        {
            BModify = false;

            switch (ClassCommon.MoistureType)
            {
                case 0:
                    MoistureType = "% Moisture Content";
                    break;
                case 1:
                    MoistureType = "% Moisture Regain";
                    break;
                case 2:
                    MoistureType = "% Air Dry";
                    break;
                case 3:
                    MoistureType = "% Bone Dry";
                    break;
                default:
                    MoistureType = "% Moisture";
                    break;
            }

            WeightUnit = (ClassCommon.WeightUnit == 0) ? "Kilogram (kg)": "Pound (lbs.)";
        }


        public DelegateCommand _closedPageICommand;
        public DelegateCommand ClosedPageICommand =>
        _closedPageICommand ?? (_closedPageICommand =
          new DelegateCommand(ClosedPageICommandExecute));
        private void ClosedPageICommandExecute()
        {
            
        }

        public DelegateCommand _settingsCommand;
        public DelegateCommand SettingsCommand =>
        _settingsCommand ?? (_settingsCommand =
          new DelegateCommand(SettingsCommandExecute));
        private void SettingsCommandExecute()
        {
            BModify = true;
        }

        public DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
        _saveCommand ?? (_saveCommand =
          new DelegateCommand(SaveCommandExecute).ObservesCanExecute(() => BModify));
        private void SaveCommandExecute()
        {
            BModify = false;
       
            ClassCommon.BoxOneCheck = BoxOneChecked;
            ClassCommon.BoxTwoCheck = BoxTwoChecked;
            ClassCommon.BoxThreeCheck = BoxThreeChecked;
            ClassCommon.BoxFourCheck = BoxFourChecked;
            ClassCommon.BoxFiveCheck = BoxFiveChecked;
            ClassCommon.BoxSixCheck = BoxSixChecked;  
        }
    }
}
