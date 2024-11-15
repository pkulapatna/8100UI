
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MidCmbDisplayBox.Properties;
using _8100UI.Services;
using FieldModify.Views;
using System.Diagnostics;
using System.CodeDom;

namespace MidCmbDisplayBox.ViewModels
{
    public class MidCmbBoxViewModel : BindableBase
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;

        private Window ModWindow;
        private Sqlhandler _sqlHandler;
        private AccessHandler _accessHandler;

        private bool _userLogin;
        public bool UserLogin
        {
            get => _userLogin;
            set 
            { 
                SetProperty(ref _userLogin, value);    
            }
        }

        public string FieldType { get; set; } = "BoxX";
        public int BoxToChange { get; set; }

        private int _titleIndex = 0;
        public int TitleIndex
        {
            get => _titleIndex;
            set
            {
                SetProperty(ref _titleIndex, value);
                // if (value > -1) SetUpBoxes(value);
                ShowMenu = Visibility.Hidden;
                TextColor = NonEditFgColor;
                TextbackGndColor = NonEditBGColor;
            }
        }

        private List<int> boxindexLst = new List<int>();

        private int _boxId;
        public int BoxId
        {
            get => _boxId;
            set { SetProperty(ref _boxId, value); }
        }


        private List<int> _boxName = new List<int>();
        public List<int> BoxName
        {
            get => _boxName;
            set { SetProperty(ref _boxName, value); }
        }


        private List<string> _titleList;
        public List<string> TitleList
        {
            get => _titleList;
            set { SetProperty(ref _titleList, value); }
        }

        private string _boxValue;
        public string BoxValue
        {
            get => _boxValue;
            set { SetProperty(ref _boxValue, value); }
        }


        private int _lotUID;
        public int LotUID
        {
            get => _lotUID;
            set { SetProperty(ref _lotUID, value); }
        }

        private string _boxTitle;
        public string BoxTitle
        {
            get => _boxTitle;
            set { SetProperty(ref _boxTitle, value); }
        }

        //------------------------------------------------------------
        private Brush _textcolor;
        public Brush TextColor
        {
            get { return _textcolor; }
            set { SetProperty(ref _textcolor, value); }
        }
        private Brush editFgColor = Brushes.Blue;
        public Brush EditFgColor
        {
            get { return editFgColor; }
            set { SetProperty(ref editFgColor, value); }
        }
        private Brush nonEditFgColor = Brushes.Black;
        public Brush NonEditFgColor
        {
            get { return nonEditFgColor; }
            set { SetProperty(ref nonEditFgColor, value); }
        }
        //--------------------------------------------------------------
        private Brush _textbackGndColor = Brushes.White;
        public Brush TextbackGndColor
        {
            get { return _textbackGndColor; }
            set { SetProperty(ref _textbackGndColor, value); }
        }
        private Brush editBGColor = Brushes.AliceBlue;
        public Brush EditBGColor
        {
            get { return editBGColor; }
            set { SetProperty(ref editBGColor, value); }
        }
        private Brush nonEditBGColor = Brushes.AntiqueWhite;
        public Brush NonEditBGColor
        {
            get { return nonEditBGColor; }
            set { SetProperty(ref nonEditBGColor, value); }
        }
        //-----------------------------------------------------------------


        private Brush _titleColor;
        public Brush TitleColor
        {
            get { return _titleColor; }
            set { SetProperty(ref _titleColor, value); }
        }

        private Visibility _showMenu;
        public Visibility ShowMenu
        {
            get { return _showMenu; }
            set { SetProperty(ref _showMenu, value); }
        }

        public List<string> _cmbDropDownList = new List<string>();
        public List<string> CmbDropDownList
        {
            get => _cmbDropDownList;
            set => SetProperty(ref _cmbDropDownList, value);
        }


        public MidCmbBoxViewModel(int boxNumber, IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            this.BoxId = boxNumber;

            UserLogin = ClassCommon.UserLogin;

              _accessHandler = new AccessHandler();
            _sqlHandler = Sqlhandler.Instance;

            _eventAggregator.GetEvent<CloseObjModWindow>().Subscribe(CloseFieldModDialog);
            _eventAggregator.GetEvent<CloseMainWindow>().Subscribe(CloseMainWindows);
            _eventAggregator.GetEvent<UpdateMidBoxData>().Subscribe(UpdateMidBoxesData);
            _eventAggregator.GetEvent<UserLogin>().Subscribe(UserLogonEvent);

            TitleList = _sqlHandler.GetBigBoxHdrList();

            switch (BoxId)
            {
                case 1:
                    TitleIndex = Settings.Default.BoxOneIdx;
                 
                    break;
                case 2:
                    TitleIndex = Settings.Default.BoxTwoIdx;
                   
                    break;
                case 3:
                    TitleIndex = Settings.Default.BoxThreeIdx;
                   
                    break;
                case 4:
                    TitleIndex =Settings.Default.BoxFourIdx;
                    
                    break;
                case 5:
                    TitleIndex = Settings.Default.BoxFiveIdx;
                   
                    break;
                case 6:
                    TitleIndex = Settings.Default.BoxSixIdx;
                    
                    break;
                case 7:
                    TitleIndex = Settings.Default.BoxSevenIdx;
                   
                    break;
                case 8:
                    TitleIndex = Settings.Default.BoxEightIdx;
                   
                    break;
            }
        }

        private void UpdateMidBoxesData(int obj)
        {
            _ = UpdatedataAtMarkerasync(obj);
        }

        private void UserLogonEvent(bool obj)
        {
            UserLogin = obj;
           _ = UpdatedataAtMarkerasync(1);
        }

        private void CloseMainWindows(bool obj)
        {
            Settings.Default.Save();
        }

        private async Task UpdatedataAtMarkerasync(int stationId)
        {
            DataTable newBaleData = new DataTable();
            DataTable newLotData = new DataTable();
            int StationId = stationId;
            int lineId = 1;

            //From Forte_Syatem Db.
            string LotProcessquery = $"SELECT * from RealTimeLotProc where ID = 0;";
           //From ForteData Db.
            string strLotquery = $"SELECT  Top 1 NextLotNum, Size from [RealTimeLotProc] with (NOLOCK) where ID = 0;";

            string CurBaletable = await _sqlHandler.GetCurrentBaleTableNameAsync();
            string strbalequery = $"SELECT top  1 * from {CurBaletable} with (NOLOCK) WHERE LineID = {lineId} ORDER BY UID DESC SET STATISTICS TIME ON;";
          
            try
            {
                await Task.Run(async () =>
                {
                    DataColumn column;

                    newBaleData = await _sqlHandler.GetArchiveTableAsync(strbalequery);
                    newLotData = await _sqlHandler.GeForteSystemTableAsync(strLotquery);

                    // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                    column = new DataColumn
                    {
                        DataType = System.Type.GetType("System.Int32"),
                        ColumnName = "Next LotNumber"
                    };
                    newBaleData.Columns.Add(column);
   
                    column = new DataColumn
                    {
                        DataType = System.Type.GetType("System.Int32"),
                        ColumnName = "LotSize"
                    };
                    newBaleData.Columns.Add(column);

                    column = new DataColumn
                    {
                        DataType = System.Type.GetType("System.Int32"),
                        ColumnName = "Next SerialNumber"
                    };
                    newBaleData.Columns.Add(column);


                    if ((newLotData.Rows.Count > 0) && (newBaleData.Rows.Count > 0))
                    {
                            if (Convert.ToInt32(newBaleData.Rows[0]["LineID"]) == 1)
                                Updateboxdata(newBaleData, newLotData, BoxId);
                    }
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdatedataAtMarkerasync -> { ex.Message}");
                MessageBox.Show($"ERROR in UpdatedataAtMarkerasync {ex.Message}");
            }
        }


        private string UpdateBoxValues(DataTable newbaledata, DataTable newLotdata)
        {
            string Boxvalue = string.Empty;

            switch (TitleList[TitleIndex].ToString())
            {
                case "Next LotNumber":
                    if (newLotdata != null)
                        BoxValue = newLotdata.Rows[0]["NextLotNum"].ToString();

                    if(UserLogin)
                    {
                        ShowMenu = Visibility.Visible;
                        TextColor = EditFgColor;
                        TextbackGndColor = EditBGColor;
                    }
                    else
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = NonEditFgColor;
                        TextbackGndColor = NonEditBGColor;
                    }

                    break;

                case "LotSize":
                    if (newLotdata != null)
                        BoxValue = (string)newLotdata.Rows[0]["Size"].ToString();

                    if (UserLogin)
                    {
                        ShowMenu = Visibility.Visible;
                        TextColor = EditFgColor;
                        TextbackGndColor = EditBGColor;
                    }
                    else
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = NonEditFgColor;
                        TextbackGndColor = NonEditBGColor;
                    }
                    break;

                case "Next SerialNumber":
                    if (newbaledata != null)
                        BoxValue = ((int)newbaledata.Rows[0]["SerialNumber"] + 1).ToString();

                    if (UserLogin)
                    {
                        ShowMenu = Visibility.Visible;
                        TextColor = EditFgColor;
                        TextbackGndColor = EditBGColor;
                    }
                    else
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = NonEditFgColor;
                        TextbackGndColor = NonEditBGColor;
                    }
                    break;

                default:
                    if (newbaledata != null)
                        BoxValue = newbaledata.Rows[0][TitleList[TitleIndex]].ToString();
                    break;
            }
            return Boxvalue;
  
        }

        private void Updateboxdata(DataTable newbaledata, DataTable newLotdata, int boxId)
        {
            switch (boxId)
            {
                case 1:
                    if (ClassCommon.BoxOneCheck)
                        UpdateBoxValues(newbaledata, newLotdata);
                    break;

                case 2:
                    if (ClassCommon.BoxTwoCheck)
                        UpdateBoxValues(newbaledata, newLotdata);
                    break;

                case 3:
                    if (ClassCommon.BoxThreeCheck)
                        UpdateBoxValues(newbaledata, newLotdata);
                    break;

                case 4:
                    if (ClassCommon.BoxFourCheck)
                        UpdateBoxValues(newbaledata, newLotdata);
                    break;

                case 5:
                    if (ClassCommon.BoxFiveCheck)
                        UpdateBoxValues(newbaledata, newLotdata);
                    break;

                case 6:
                    if (ClassCommon.BoxSixCheck)
                        UpdateBoxValues(newbaledata, newLotdata);
                    break;
            }

        }

   
        public void UpdateMidBoxValue(DataTable newData, DataTable newLotData, int boxNumber)
        {
            Console.WriteLine($" xxxxxxxxxxxxxxxxxxx boxNumber=  {boxNumber}   TitleIndex=  {TitleIndex}");


            Console.WriteLine($" xxxxxxxxxxxxxxxxxxx boxNumber=  {boxNumber}   TitleIndex=  {TitleList[TitleIndex]}");

            try
            {
                switch (TitleIndex) //Data Type
                {
                    case 0: // LotNumber No Change!

                        TextColor = NonEditFgColor;
                        TextbackGndColor = NonEditBGColor;
                        ShowMenu = Visibility.Hidden;

                      //  if (newData != null)
                      //      BoxValue = newData.Rows[0]["LotNumber"].ToString();
                        break;

                    case 1: // Next LotNumber OK
                        if (UserLogin)
                        {
                            ShowMenu = Visibility.Visible;
                            TextColor = EditFgColor;
                            TextbackGndColor = EditBGColor;
                        }
                        else
                        {
                            ShowMenu = Visibility.Hidden;
                            TextColor = NonEditFgColor;
                            TextbackGndColor = NonEditBGColor;
                        }  
                        if (newData != null)
                            BoxValue = ((int)newData.Rows[0]["LotNumber"] + 1).ToString();
                        break;

                    case 2: // LotSize
                        if (UserLogin)
                        {
                            ShowMenu = Visibility.Visible;
                            TextColor = EditFgColor;
                            TextbackGndColor = EditBGColor;
                        }
                        else
                        {
                            ShowMenu = Visibility.Hidden;
                            TextColor = NonEditFgColor;
                            TextbackGndColor = NonEditBGColor;
                        }
                        if (newLotData != null)
                            if (newLotData.Rows.Count > 0)
                            {
                                BoxValue = newLotData.Rows[0]["Size"].ToString();
                                LotUID = Convert.ToInt32(newLotData.Rows[0]["ID"].ToString());
                            }
                            else BoxValue = "N/A";
                        break;

                    case 3:  //LotBaleNumber  
                        TextColor = NonEditFgColor;
                        TextbackGndColor = NonEditBGColor;
                        ShowMenu = Visibility.Hidden;

                        if (newData != null)
                            BoxValue = newData.Rows[0]["LotBaleNumber"].ToString();
                        break;


                    case 4: //Bale Count Down
                        
                        TextColor = NonEditFgColor;
                        TextbackGndColor = NonEditBGColor;
                        ShowMenu = Visibility.Hidden;
                        if (newLotData != null)
                            if (newLotData.Rows.Count > 0)
                                BoxValue = (Convert.ToInt32(newLotData.Rows[0]["Size"]) - Convert.ToInt32(newData.Rows[0]["LotBaleNumber"])).ToString();
                            else BoxValue = "N/A";
                        break;

                    case 5: //SerialNumber
                       
                        TextColor = NonEditFgColor;
                        TextbackGndColor = NonEditBGColor;
                        ShowMenu = Visibility.Hidden;
                        if (newLotData != null)
                        {
                            BoxValue = newData.Rows[0]["SerialNumber"].ToString();
                        }
                        else BoxValue = "N/A";
                        break;

                    case 6: //Next SerialNumber
                        if (UserLogin)
                        {
                            ShowMenu = Visibility.Visible;
                            TextColor = EditFgColor;
                            TextbackGndColor = EditBGColor;
                        }
                        else
                        {
                            ShowMenu = Visibility.Hidden;
                            TextColor = NonEditFgColor;
                            TextbackGndColor = NonEditBGColor;
                        }
                        // if(Convert.ToInt32(LineOne) > 0 )
                        BoxValue = "0";// _sqlHandler.GetNextSerialNumber("0"); //_accessHandler.GetNextSerialNumber(LineOne);
                        break;

                    case 7: // UnitNumber
                        TextColor = NonEditFgColor;
                        TextbackGndColor = NonEditBGColor;
                        ShowMenu = Visibility.Hidden;
                        if (newData != null)
                            BoxValue = ((int)newData.Rows[0]["UnitNumber"]).ToString();
                        else BoxValue = "N/A";
                        break;

                    case 8: //Next UnitNumber
                        if (UserLogin)
                        {
                            ShowMenu = Visibility.Visible;
                            TextColor = EditFgColor;
                            TextbackGndColor = EditBGColor;
                        }
                        else
                        {
                            ShowMenu = Visibility.Hidden;
                            TextColor = NonEditFgColor;
                            TextbackGndColor = NonEditBGColor;
                        }
                        break;

                    case 9: //UnitBaleNumber
                        if (newLotData != null)
                        {
                            BoxValue = newData.Rows[0]["UnitBaleNumber"].ToString();
                        }
                        else BoxValue = "N/A";

                        ShowMenu = Visibility.Hidden;
                        TextColor = NonEditFgColor;
                        TextbackGndColor = NonEditBGColor;

                        break;

                    default:
                        ShowMenu = Visibility.Hidden;
                        TextColor = Brushes.Black;
                        BoxValue = "N/A";
                        break;
                }
                
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateMidBoxValue -> { ex.Message}");
                MessageBox.Show($"ERROR in UpdateMidBoxValue {ex.Message}");
            }
        }

        /// <summary>
        /// DropDown Closed
        /// </summary>
        private DelegateCommand _boxComboCommand;
        public DelegateCommand BoxComboCommand =>
        _boxComboCommand ?? (_boxComboCommand = new DelegateCommand(boxComboCommandExecute));
        private void boxComboCommandExecute()
        {
            
            switch (BoxId)
            {
                case 1:
                    if(TitleIndex != Settings.Default.BoxOneIdx)
                    {
                        Settings.Default.BoxOneIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 2:
                    if(TitleIndex != Settings.Default.BoxTwoIdx)
                    {
                        Settings.Default.BoxTwoIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 3:
                    if (TitleIndex != Settings.Default.BoxThreeIdx)
                    {
                        Settings.Default.BoxThreeIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 4:
                    if (TitleIndex != Settings.Default.BoxFourIdx)
                    {
                        Settings.Default.BoxFourIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 5:
                    if (TitleIndex != Settings.Default.BoxFiveIdx)
                    {
                        Settings.Default.BoxFiveIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 6:
                    if (TitleIndex != Settings.Default.BoxSixIdx)
                    {
                        Settings.Default.BoxSixIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 7:
                    if (TitleIndex != Settings.Default.BoxSevenIdx)
                    {
                        Settings.Default.BoxSevenIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 8:
                    if (TitleIndex != Settings.Default.BoxEightIdx)
                    {
                        Settings.Default.BoxEightIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 11:
                    if (TitleIndex != Settings.Default.BoxElevenIdx)
                    {
                        Settings.Default.BoxElevenIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 12:
                    if (TitleIndex != Settings.Default.BoxTwelveIdx)
                    {
                        Settings.Default.BoxTwelveIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 13:
                    if (TitleIndex != Settings.Default.BoxThirteenIdx)
                    {
                        Settings.Default.BoxThirteenIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 14:
                    if (TitleIndex != Settings.Default.BoxFourteenIdx)
                    {
                        Settings.Default.BoxFourteenIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 15:
                    if (TitleIndex != Settings.Default.BoxFifteenIdx)
                    {
                        Settings.Default.BoxFifteenIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 16:
                    if (TitleIndex != Settings.Default.BoxSixteenIdx)
                    {
                        Settings.Default.BoxSixteenIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
            }
        }

        private DelegateCommand _changeValueCommand;
        public DelegateCommand ChangeValueCommand =>
        _changeValueCommand ?? (_changeValueCommand = new DelegateCommand(ChangeValueCommandExecute));
        private void ChangeValueCommandExecute()
        {
            string ItemtoChange = TitleIndex.ToString();
                if (ModWindow != null) ModWindow = null;

            BoxToChange =   TitleIndex;
            ItemtoChange = TitleList[TitleIndex].ToString();

            ClsSerilog.LogMessage(ClsSerilog.Info, $"ChangeValueCommand -> { TitleList[TitleIndex]}");
          
            ModWindow = new Window
            {
                Title = ItemtoChange,
                Width = 300,
                Height = 300,
                Content = new FieldModifyView(_eventAggregator, ItemtoChange, BoxValue, FieldType)
            };
            ModWindow.ResizeMode = ResizeMode.NoResize;
            ModWindow.ShowDialog();
            
        }

        private void CloseFieldModDialog(bool obj)
        {
            if (obj)
            {
                if (ModWindow != null)
                {
                    ModWindow.Close();
                    ModWindow = null;
                }
            }
        }

        public static implicit operator MidCmbBoxViewModel(MidCmbBoxLIneTwoViewModel v)
        {
            throw new NotImplementedException();
        }
    }

}
