using _8100UI.Services;
using FieldModify.Views;
using MidCmbDisplayBox.Properties;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MidCmbDisplayBox.ViewModels
{
    public class MidCmbBoxLIneTwoViewModel : BindableBase
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;
        private Window ModWindow;
        private Sqlhandler _sqlHandler;
      
        private bool _userLogin;
        public bool UserLogin
        {
            get => _userLogin;
            set  { SetProperty(ref _userLogin, value); }
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
                if (value > -1) SetUpBoxes(value);
            }
        }

        private void SetUpBoxes(int boxIndex)
        {
            switch (boxIndex)
            {
                case 0: // LotNumber
                    if (UserLogin)
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = Brushes.Black;
                    }
                    else
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = Brushes.DarkBlue;
                    }
                    break;

                case 1: // NextLotNumber //Changeable
                    if (UserLogin)
                    {
                        ShowMenu = Visibility.Visible;
                        TextColor = Brushes.Blue;
                    }
                    else
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = Brushes.DarkBlue;
                    }
                    break;

                case 2: // LotSize //Changeable
                    if (UserLogin)
                    {
                        ShowMenu = Visibility.Visible;
                        TextColor = Brushes.Blue;
                    }
                    else
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = Brushes.DarkBlue;
                    }
                    break;

                case 3: // BaleNumber
                    if (UserLogin)
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = Brushes.Black;
                    }
                    else
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = Brushes.DarkBlue;
                    }
                    break;

                case 4: // BaleCountDown
                    if (UserLogin)
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = Brushes.Black;
                    }
                    else
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = Brushes.DarkBlue;
                    }
                    break;

                case 5: // SerialNumber
                    if (UserLogin)
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = Brushes.Black;
                    }
                    else
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = Brushes.DarkBlue;
                    }
                    break;

                case 6: // Next SerialNumber //Changeable
                    if (UserLogin)
                    {
                        ShowMenu = Visibility.Visible;
                        TextColor = Brushes.Blue;
                    }
                    else
                    {
                        ShowMenu = Visibility.Hidden;
                        TextColor = Brushes.DarkBlue;
                    }
                    break;
            }
        }

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

        private System.Windows.Media.Brush _textcolor;
        public Brush TextColor
        {
            get { return _textcolor; }
            set { SetProperty(ref _textcolor, value); }
        }

        private Brush _textbackGndColor = Brushes.White;
        public Brush TextbackGndColor
        {
            get { return _textbackGndColor; }
            set { SetProperty(ref _textbackGndColor, value); }
        }

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

        public MidCmbBoxLIneTwoViewModel(int boxNumber, IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            this.BoxId = boxNumber;

            UserLogin = ClassCommon.UserLogin;

            _sqlHandler = Sqlhandler.Instance;

            _eventAggregator.GetEvent<CloseObjModWindow>().Subscribe(CloseFieldModDialog);
            _eventAggregator.GetEvent<ChangeMidBoxValue>().Subscribe(ChangeNewValue);
            _eventAggregator.GetEvent<UpdateBaleDataEvent>().Subscribe(BaleDataReadyEvent);
            _eventAggregator.GetEvent<CloseMainWindow>().Subscribe(CloseMainWindows);

            _eventAggregator.GetEvent<UserLogin>().Subscribe(UserLogonEvent);

            TitleList = new List<string>
            {
                $"Lot Number",
                $"Next LotNumber",
                $"Lot Size",
                $"Bale Count",
                $"Bale CountDown",
                $"Serial Number",
                $"Next Serial Number"
            };

            switch (BoxId)
            {
                case 1:
                    TitleIndex = Settings.Default.BoxElevenIdx;
                    break;
                case 2:
                    TitleIndex = Settings.Default.BoxTwelveIdx;
                    break;
                case 3:
                    TitleIndex = Settings.Default.BoxThirteenIdx;
                    break;
                case 4:
                    TitleIndex = Settings.Default.BoxFourteenIdx;
                    break;
                case 5:
                    TitleIndex = Settings.Default.BoxFifteenIdx;
                    break;
                case 6:
                    TitleIndex = Settings.Default.BoxSixteenIdx;
                    break;
            }
        }

        private void ChangeNewValue(string obj)
        {
            Console.WriteLine($"BoxToChange= {BoxToChange}  BoxId=  {BoxId}");

            switch (BoxToChange)
            {
                case 0: // LotNumber

                    break;
                case 1: // "NextLotNumber"
                    BoxValue = obj;

                    ClsPipeMessage.SendPipeMessage($"LN;{BoxValue};");
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Change Next Lot {BoxValue}");
                    break;
                case 2: // "LotSize"
                    BoxValue = obj;

                    ClsPipeMessage.SendPipeMessage($"LSZ;{BoxValue};{LotUID};");
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Change Lot Size {BoxValue}");
                    break;
                case 3:  //BaleNumber

                    break;
                case 4: //BaleCountDown

                    break;
                case 5: // SerialNumber
                    BoxValue = obj;

                    ClsPipeMessage.SendPipeMessage($"CSN;{BoxValue};");
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Change Serial Number {BoxValue}");

                    break;
                case 6: //Next SerialNumber
                    BoxValue = obj;

                    ClsPipeMessage.SendPipeMessage($"NSN;{BoxValue};");
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Change Next Serial Number {BoxValue}");
                    break;
                default:

                    break;
            }
            BoxToChange = 99;
        }

        private void UserLogonEvent(bool obj)
        {
            UserLogin = obj;
           // _ = UpdatedataAtMarkerasync(1);
        }

        private void CloseMainWindows(bool obj)
        {
            Settings.Default.Save();
        }

        /// <summary>
        /// Event at Marker id= obj
        /// </summary>
        /// <param name="obj"></param>
        private void BaleDataReadyEvent(int obj)
        {  
            _ = UpdatedataAtMarkerasync(obj);
        }
        private async Task UpdatedataAtMarkerasync(int stationId)
        {
            DataTable newBaleData = new DataTable();
            DataTable newLotData = new DataTable();
            int StationId = stationId;

            string CurBaletable =  await _sqlHandler.GetCurrentBaleTableNameAsync();
            string strbalequery = $"SELECT top  1 * from {CurBaletable} with (NOLOCK) WHERE LineID > 0 ORDER BY UID DESC SET STATISTICS TIME ON;";
            string strLotquery = $"SELECT TOP 2 * from RealTimeLotProc with (NOLOCK);";

            try
            {
                await Task.Run(async () =>
                {
                    newLotData = await _sqlHandler.GetArchiveTableAsync(strLotquery);
                    newBaleData = await _sqlHandler.GetForteDataTableAsync(strbalequery);
                    
                    if ((newLotData.Rows.Count > 0) && (newBaleData.Rows.Count > 0))
                    {  
                        if (ClassCommon.SingleLot == true)
                            Updateboxdata(newBaleData, newLotData);
                        else
                        {
                            if (Convert.ToInt32(newBaleData.Rows[0]["LineID"]) == 1)
                                Updateboxdata(newBaleData, newLotData);
                            else if (Convert.ToInt32(newBaleData.Rows[0]["LineID"]) == 2)
                                Updateboxdata(newBaleData, newLotData);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdatedataAtMarkerasync -> { ex.Message}");
                MessageBox.Show($"ERROR in UpdatedataAtMarkerasync {ex.Message}");
            }
        }

        private void Updateboxdata(DataTable newbaledata, DataTable newLotdata)
        {
            if (ClassCommon.BoxOneCheck)
                UpdateMidBoxValue(newbaledata, newLotdata);
            if (ClassCommon.BoxTwoCheck)
                UpdateMidBoxValue(newbaledata, newLotdata);
            if (ClassCommon.BoxThreeCheck)
                UpdateMidBoxValue(newbaledata, newLotdata);
            if (ClassCommon.BoxFourCheck)
                UpdateMidBoxValue(newbaledata, newLotdata);
            if (ClassCommon.BoxFiveCheck)
                UpdateMidBoxValue(newbaledata, newLotdata);
            if (ClassCommon.BoxSixCheck)
                UpdateMidBoxValue(newbaledata, newLotdata);
        }

        public void UpdateMidBoxValue(DataTable newData, DataTable newLotData)
        {
            try
            {
                switch (TitleIndex) //Data Type
                {
                    case 0: // Lot Number OK

                        if (UserLogin)
                        {
                            TextColor = Brushes.Black;
                        }
                        else
                        {   
                            TextColor = Brushes.DarkBlue;
                        }

                        ShowMenu = Visibility.Hidden;
                        if (newData != null)
                            BoxValue = newData.Rows[0]["LotNumber"].ToString();
                        break;

                    case 1: // Next Lot Number OK
                        if (UserLogin)
                        {
                            ShowMenu = Visibility.Visible;
                            TextColor = Brushes.Blue;
                        }
                        else
                        {
                            ShowMenu = Visibility.Hidden;
                            TextColor = Brushes.DarkBlue;
                        }
                        if (newLotData != null)
                        {
                            BoxValue = ((Single)newLotData.Rows[0]["NextLotNum"]).ToString();
                        }
                        break;

                    case 2: // Lot Size
                        if (UserLogin)
                        {
                            ShowMenu = Visibility.Visible;
                            TextColor = Brushes.Blue;
                        }
                        else
                        {
                            ShowMenu = Visibility.Hidden;
                            TextColor = Brushes.DarkBlue;
                        }
                        if (newLotData != null)
                            if (newLotData.Rows.Count > 0)
                            {
                                BoxValue = ((Single)newLotData.Rows[0]["Size"]).ToString();
                                LotUID = Convert.ToInt32(newLotData.Rows[0]["ID"]);
                            }
                            else BoxValue = "N/A";
                        break;

                    case 3:  //Bale Number OK
                        if (UserLogin)
                        {
                            TextColor = Brushes.Black;
                        }
                        else
                        {
                            TextColor = Brushes.DarkBlue;
                        }
                        ShowMenu = Visibility.Hidden;
                        if (newData != null)
                            BoxValue = newData.Rows[0]["LotBaleNumber"].ToString();
                        break;

                    case 4: //Bale Count Down

                        if (UserLogin)
                        {
                            TextColor = Brushes.Black;
                        }
                        else
                        {
                            TextColor = Brushes.DarkBlue;
                        }
                            ShowMenu = Visibility.Hidden;
                        if ((newLotData != null) & (newData != null))
                        {
                            Single LotSize = ((Single)newLotData.Rows[0]["Size"]);
                            Single BaleCount = Convert.ToSingle(newData.Rows[0]["LotBaleNumber"]);
                            BoxValue = (LotSize - BaleCount).ToString();
                        }
                        break;

                    case 5: //SerialNumber

                        if (UserLogin)
                        {
                            TextColor = Brushes.Black;
                        }
                        else
                        {
                            TextColor = Brushes.DarkBlue;
                        }

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
                            TextColor = Brushes.Blue;
                        }
                        else
                        {
                            ShowMenu = Visibility.Hidden;
                            TextColor = Brushes.DarkBlue;
                        }
                        // if(Convert.ToInt32(LineOne) > 0 )
                        BoxValue = "0"; // _sqlHandler.GetNextSerialNumber("0"); 
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
                    if (TitleIndex != Settings.Default.BoxElevenIdx)
                    {
                        Settings.Default.BoxElevenIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 2:
                    if (TitleIndex != Settings.Default.BoxTwelveIdx)
                    {
                        Settings.Default.BoxTwelveIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 3:
                    if (TitleIndex != Settings.Default.BoxThirteenIdx)
                    {
                        Settings.Default.BoxThirteenIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 4:
                    if (TitleIndex != Settings.Default.BoxFourteenIdx)
                    {
                        Settings.Default.BoxFourteenIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 5:
                    if (TitleIndex != Settings.Default.BoxFifteenIdx)
                    {
                        Settings.Default.BoxFifteenIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 6:
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
            BoxToChange = TitleIndex;

            ClsSerilog.LogMessage(ClsSerilog.Info, $"ChangeValueCommand -> { TitleList[TitleIndex]}");

            switch (TitleIndex)
            {
                case 0: // LotNumber //Can't Change
                    break;

                case 1: // NextLotNumber
                    ItemtoChange = "Change NextLot Number";
                    break;

                case 2: // LotSize
                    ItemtoChange = "Change Lot Size";
                    break;

                case 3: // BaleNumber //Can't Change
                    break;

                case 4: // BaleCountDown //Can't Change
                    break;

                case 5: // SerialNumber //Can't Change
                        //ItemtoChange = "Serial Number";
                    break;

                case 6: // Next SerialNumber
                    ItemtoChange = "Next Serial Number";
                    break;
            }
            ModWindow = new Window
            {
                Title = ItemtoChange,
                Width = 560,
                Height = 360,
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

        public static implicit operator MidCmbBoxLIneTwoViewModel(MidCmbBoxViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}
