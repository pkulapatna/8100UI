using _8100UI.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BaleStation.ViewModels
{
    public class BaleStationLineTwoViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;


        //CurMoisture
        private string _CurMoisture;
        public string CurMoistureA
        {
            get => _CurMoisture;
            set => SetProperty(ref _CurMoisture, value);
        }

        private int _stationType;
        public int StationType
        {
            get => _stationType;
            set => SetProperty(ref _stationType, value);
        }

        private string _unitType;
        public string UnitType
        {
            get => _unitType;
            set => SetProperty(ref _unitType, value);
        }

        private string _stationHdr;
        public string StationHdr
        {
            get => _stationHdr;
            set => SetProperty(ref _stationHdr, value);
        }

        // header color
        private Brush _hdrColor;
        public Brush HdrColor
        {
            get { return _hdrColor; }
            set { SetProperty(ref _hdrColor, value); }
        }
        // Value color
        private Brush _valColor;
        public Brush ValColor
        {
            get { return _valColor; }
            set { SetProperty(ref _valColor, value); }
        }
        //Unit color
        private Brush _unitColor;
        public Brush UnitColor
        {
            get { return _unitColor; }
            set { SetProperty(ref _unitColor, value); }
        }

        private Visibility _showTitle;
        public Visibility ShowTitle
        {
            get { return _showTitle; }
            set => SetProperty(ref _showTitle, value);
        }
        private Visibility _showButton;
        public Visibility ShowButton
        {
            get { return _showButton; }
            set
            {
                SetProperty(ref _showButton, value);

                ShowTitle = (value == Visibility.Visible) ? Visibility.Hidden : ShowTitle = Visibility.Visible;
            }
        }
        private Visibility _infoBoxVis;
        public Visibility InfoBoxVis
        {
            get { return _infoBoxVis; }
            set => SetProperty(ref _infoBoxVis, value);
        }
        private string _buttonContent;
        public string ButtonContent
        {
            get => _buttonContent;
            set => SetProperty(ref _buttonContent, value);
        }

        /// <summary>
        /// Station Line 2
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="stationType"></param>
        public BaleStationLineTwoViewModel(IEventAggregator eventAggregator, int stationType)
        {
            this._eventAggregator = eventAggregator;
      
            _eventAggregator.GetEvent<UpdateBaleMoveEventLineTwo>().Subscribe(BaleAtStationEvents);

            if (stationType == 99) //LayBoy
            {
                StationHdr = ClassCommon.BaleStation[ClassCommon.BaleStation.Count - 1].StationName;
            }
            else
                StationHdr = ClassCommon.StationGraphicName[stationType];

            if (stationType < ClassCommon.BaleStation.Count)
            {
                //StationHdr = ClassCommon.BaleStation[stationType].StationGraphicName;
                //StationHdr = ClassCommon.StationGraphicName[stationType];


                if (ClassCommon.BaleStation[stationType].StationName == "Weigh")
                {
                    UnitType = (ClassCommon.WeightUnit == 0) ? "kg." : "Lbs.";
                    this.StationType = 1;
                }
                else if (ClassCommon.BaleStation[stationType].StationName == "Marker")
                {
                    switch (ClassCommon.MoistureType)
                    {
                        case 0: // %MC
                            UnitType = "% MC";
                            break;
                        case 1: // %MR
                            UnitType = "% MR";
                            break;
                        case 2: // %AD
                            UnitType = "% AD";
                            break;
                        case 3: // %BD
                            UnitType = "% BD";
                            break;
                    }
                    this.StationType = 3;
                }

                else if (ClassCommon.BaleStation[stationType].StationName == "Press")
                {
                    UnitType = "Forte #";
                    this.StationType = 2;
                }

            }




            //  StationHdr = ClassCommon.Stations[stationType].Item2;

            //StationHdr = ClassCommon.BaleStation[0].StationGraphicName;

            /*

            switch (stationType)
            {
                case 0:
                    UnitType = "Drop #";
                   
                    
                    break;

                case 1:
                    UnitType = (ClassCommon.WeightUnit == 0) ? "kg." : "Lbs.";
                    break;

                case 2:
                    UnitType = "Forte #";
                    break;

                case 3:
                    switch (ClassCommon.MoistureType)
                    {
                        case 0: // %MC
                            UnitType = "% MC";
                            break;
                        case 1: // %MR
                            UnitType = "% MR";
                            break;
                        case 2: // %AD
                            UnitType = "% AD";
                            break;
                        case 3: // %BD
                            UnitType = "% BD";
                            break;
                    }
                    break;

                case 4:

                    break;

                case 5:
                    UnitType = "Drop#";
                    break;

                case 16:
                    UnitType = "Drop#";
                    StationHdr = ClassCommon.BaleStation[ClassCommon.StationOneLineOneID].StationGraphicName;
                    break;

            }
            */

            ShowButton = Visibility.Visible;
            ButtonContent = "Hello";
        }


        private void BaleAtStationEvents(int obj)
        {
            _ = UpdateBaleAtStationDataAsncy(obj);
        }

        private async Task UpdateBaleAtStationDataAsncy(int ConveyorId)
        {
            DataTable newdata = ClassCommon.NewDattable;
            
            await Task.Run(() =>
            {
                if (ConveyorId == ClassCommon.ApproachConveyorLineTwo)
                {
                    // if(StationType == ClassCommon.Approach)
                    //     UpdateValue($"Drop# {ClassCommon.NewDattable.Rows[ClassCommon.NewDatIndex]["DropNumber"].ToString()}");
                }
                else if (ConveyorId == ClassCommon.WeighConveyorLineTwo) //25
                {
                    if (StationType == ClassCommon.Scale)
                        UpdateValue(ClassCommon.GetWeightVal((float)newdata.Rows[ClassCommon.NewDatIndex]["Weight"]));
                }
                else if (ConveyorId == ClassCommon.PressConveyorLineTwo) //26
                {
                    if (StationType == ClassCommon.Press)
                        UpdateValue(newdata.Rows[ClassCommon.NewDatIndex]["Forte"].ToString());
                }
                else if ((ConveyorId == ClassCommon.MarkerConveyorLineTwo) & ((int)newdata.Rows[ClassCommon.NewDatIndex]["LineID"] == 2))
                {
                    if (StationType == ClassCommon.Marker)
                    {
                        UpdateValue(ClassCommon.GetMoistureVal((float)newdata.Rows[ClassCommon.NewDatIndex]["Moisture"]).ToString());
                    }
                }
            });
        }

        public void UpdateValue(string newvalue)
        {
            CurMoistureA = newvalue;
        }

        private DelegateCommand _btnClickCommand;
        public DelegateCommand BtnClickCommand =>
       _btnClickCommand ?? (_btnClickCommand =
            new DelegateCommand(BtnClickCommandExecute));
        private void BtnClickCommandExecute()
        {
            switch (StationType)
            {
                case 0: //Drop
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Drop Bale");

                    ClsPipeMessage.SendPipeMessage($"DB1;");
                    break;
                case 1: //Scale

                    break;
                case 2: //Press

                    break;
                case 3: //Marker
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Marker Pulse");

                    ClsPipeMessage.SendPipeMessage($"MK1Pulse;");
                    break;
                case 4: //Stacker

                    break;
                case 5: //Approach
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Approach Pulse");
                    ClsPipeMessage.SendPipeMessage($"APPulse;");
                    break;
                case 6: //Layboy1
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"LayBoy Drop");

                    ClsPipeMessage.SendPipeMessage($"Lb1Drop;");
                    break;
            }
        }

        public void ShowStateionButton(bool bShow, string bName)
        {
            ShowButton = bShow ? ShowButton = Visibility.Visible : ShowButton = Visibility.Hidden;
            ButtonContent = bName;
        }
    }
}
