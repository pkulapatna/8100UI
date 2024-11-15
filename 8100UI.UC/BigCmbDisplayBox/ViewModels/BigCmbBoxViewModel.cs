using _8100UI.Services;
using BigCmbDisplayBox.Properties;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BigCmbDisplayBox.ViewModels
{
    public class BigCmbBoxViewModel : BindableBase
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;

        private Sqlhandler _sqlHandler;

        private int _titleIndex = 0;
        public int TitleIndex
        {
            get => _titleIndex;
            set {SetProperty(ref _titleIndex, value);}
        }

        private int _baleLine;
        public int BaleLine
        {
            get => _baleLine;
            set { SetProperty(ref _baleLine, value); }
        }

        private int _boxId;
        public int BoxId
        {
            get => _boxId;
            set { SetProperty(ref _boxId, value); }
        }

        private List<string> _titleList;
        public List<string> TitleList
        {
            get => _titleList;
            set { SetProperty(ref _titleList, value); }
        }
        private List<string> _titleListLineOne;
        public List<string> TitleListLineOne
        {
            get => _titleListLineOne;
            set { SetProperty(ref _titleListLineOne, value); }
        }
        private List<string> _titleListLineTwo;
        public List<string> TitleListLineTwo
        {
            get => _titleListLineTwo;
            set { SetProperty(ref _titleListLineTwo, value); }
        }


        private string _boxValue;
        public string BoxValue
        {
            get => _boxValue;
            set { SetProperty(ref _boxValue, value); }
        }
       
        public void UpdateBigBoxValue(DataTable newData)
        {
            float moisture = (float)newData.Rows[0]["Moisture"];
            float fMoisture;
            float Weight = (float)newData.Rows[0]["Weight"];

            if(BoxId == 1)
            {
                switch (TitleIndex)
                {
                    case 0:
                        fMoisture = moisture;
                        BoxValue = fMoisture.ToString("#0.00");
                        break;
                    case 1:
                        fMoisture = moisture / (1 - moisture / 100);
                        BoxValue = fMoisture.ToString("#0.00");
                        break;
                    case 2:
                        fMoisture = (float)((100 - moisture) / .9);
                        BoxValue = fMoisture.ToString("#0.00");
                        break;

                    case 3:
                        fMoisture = 100 - moisture;
                        BoxValue = fMoisture.ToString("#0.00");
                        break;

                    case 4:
                        BoxValue = Weight.ToString("#0.0");
                        break;

                    case 5:
                        BoxValue = (Weight * 2.20462).ToString("#0.0");
                        break;
                    
                    case 6:
                        BoxValue = newData.Rows[0]["UnitNumber"].ToString();  // UnitNumber
                        break;

                    case 7:
                        BoxValue = newData.Rows[0]["UnitBaleNumber"].ToString();  // UnitBaleNumber
                        break;
                }
            }
            else if (BoxId == 2)
            {
                switch (TitleIndex)
                {
                    case 0:
                        fMoisture = moisture;
                        BoxValue = fMoisture.ToString("#0.00");
                        break;
                    case 1:
                        fMoisture = moisture / (1 - moisture / 100);
                        BoxValue = fMoisture.ToString("#0.00");
                        break;
                    case 2:
                        fMoisture = (float)((100 - moisture) / .9);
                        BoxValue = fMoisture.ToString("#0.00");
                        break;

                    case 3:
                        fMoisture = 100 - moisture;
                        BoxValue = fMoisture.ToString("#0.00");
                        break;

                    case 4:
                        BoxValue = Weight.ToString("#0.0");
                        break;

                    case 5:
                        BoxValue = (Weight * 2.20462).ToString("#0.0");
                        break;

                    case 6:
                        BoxValue = newData.Rows[0]["UnitNumber"].ToString();  // UnitNumber
                        break;

                    case 7:
                        BoxValue = newData.Rows[0]["UnitBaleNumber"].ToString();  // UnitBaleNumber
                        break;
                }
            }
    
        }

        public void UpdateBigBoxValueLineTwo(DataTable newData)
        {
            float moisture = (float)newData.Rows[0]["Moisture"];
            float fMoisture;
            float Weight = (float)newData.Rows[0]["Weight"];

            if (BoxId == 3) //Line 2
            {
                switch (TitleIndex)
                {
                    case 0:
                        fMoisture = moisture;
                        BoxValue = fMoisture.ToString("#0.00");
                        break;
                    case 1:
                        fMoisture = moisture / (1 - moisture / 100);
                        BoxValue = fMoisture.ToString("#0.00");
                        break;
                    case 2:
                        fMoisture = (float)((100 - moisture) / .9);
                        BoxValue = fMoisture.ToString("#0.00");
                        break;

                    case 3:
                        fMoisture = 100 - moisture;
                        BoxValue = fMoisture.ToString("#0.00");
                        break;

                    case 4:
                        BoxValue = Weight.ToString("#0.0");
                        break;

                    case 5:
                        BoxValue = (Weight * 2.20462).ToString("#0.0");
                        break;
                }
            }
            else if (BoxId == 4) //Line 2
            {
                switch (TitleIndex)
                {
                    case 0:
                        fMoisture = moisture;
                        BoxValue = fMoisture.ToString("#0.00");
                        break;
                    case 1:
                        fMoisture = moisture / (1 - moisture / 100);
                        BoxValue = fMoisture.ToString("#0.00");
                        break;
                    case 2:
                        fMoisture = (float)((100 - moisture) / .9);
                        BoxValue = fMoisture.ToString("#0.00");
                        break;

                    case 3:
                        fMoisture = 100 - moisture;
                        BoxValue = fMoisture.ToString("#0.00");
                        break;

                    case 4:
                        BoxValue = Weight.ToString("#0.0");
                        break;

                    case 5:
                        BoxValue = (Weight * 2.20462).ToString("#0.0");
                        break;
                }
            }
        }

        private string _boxTitle;
        public string BoxTitle
        {
            get => _boxTitle;
            set { SetProperty(ref _boxTitle, value); }
        }

        private Brush _textcolor;
        public Brush TextColor
        {
            get { return _textcolor; }
            set { SetProperty(ref _textcolor, value); }
        }

        private Brush _backGdLowColor = Brushes.White;
        public Brush BackGdColor
        {
            get { return _backGdLowColor; }
            set { SetProperty(ref _backGdLowColor, value); }
        }


        private Brush _titleColor;
        public Brush TitleColor
        {
            get { return _titleColor; }
            set { SetProperty(ref _titleColor, value); }
        }

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
                    if (TitleIndex != Settings.Default.BoxOneLineTwoIdx)
                    {
                        Settings.Default.BoxOneLineTwoIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
                case 4:
                    if (TitleIndex != Settings.Default.BoxTwoLineTwoIdx)
                    {
                        Settings.Default.BoxTwoLineTwoIdx = TitleIndex;
                        Settings.Default.Save();
                    }
                    break;
            }
        }

        public BigCmbBoxViewModel(int boxNumber, IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            _sqlHandler = Sqlhandler.Instance;
            _eventAggregator.GetEvent<UpdateBaleDataEvent>().Subscribe(BaleDataReadyEvent);

            
            this.BoxId = boxNumber;

            TitleList = new List<string>
            {
                $"%MC",
                $"%MR",
                $"%AD",
                $"%BD",
                $"Weight kg.",
                $"Weight lbs.",
                $"Unit Number",
                $"UnitBaleNumber"
            };

            TitleListLineOne = new List<string>
            {
                $"L1_%MC",
                $"L1_%MR",
                $"L1_%AD",
                $"L1_%BD",
                $"L1_Weight kg.",
                $"L1_Weight lbs.",
                $"L1_UnitNumber",
                $"L1_UnitBaleNumber"
            };


            TitleListLineTwo = new List<string>
            {
                $"L2_%MC",
                $"L2_%MR",
                $"L2_%AD",
                $"L2_%BD",
                $"L2_Weight kg.",
                $"L2_Weight lbs.",
                $"L2_UnitNumber",
                $"L2_UnitBaleNumber"
            };



            switch (BoxId)
            {
                case 1:
                    TitleIndex = Settings.Default.BoxOneIdx;
                    TitleList = TitleListLineOne;
                    break;
                case 2:
                    TitleIndex = Settings.Default.BoxTwoIdx;
                    TitleList = TitleListLineOne;
                    break;
                case 3:
                    TitleIndex = Settings.Default.BoxOneLineTwoIdx;
                    TitleList = TitleListLineTwo;
                    break;
                case 4:
                    TitleIndex = Settings.Default.BoxTwoLineTwoIdx;
                    TitleList = TitleListLineTwo;
                    break;
            }
        }


        private void BaleDataReadyEvent(int obj)
        {
            _ = UpdatedataAtMarkerasync();
        }

        private async Task UpdatedataAtMarkerasync()
        {
            DataTable mytable = new DataTable();
            string CurDatatable =  await _sqlHandler.GetCurrentBaleTableNameAsync();
            string strquery = $"SELECT top  1 * from {CurDatatable} with (NOLOCK) WHERE LineID > 0 ORDER BY UID DESC SET STATISTICS TIME ON;";

            //  string strquery = $"SELECT top  1 * from {CurDatatable} with (NOLOCK) WHERE LineID = {lineNum} ORDER BY UID DESC SET STATISTICS TIME ON;";
            try
            {
                await Task.Run(async () =>
                {
                    mytable = await _sqlHandler.GetArchiveTableAsync(strquery);
                    if(mytable.Rows.Count > 0)
                    {
                        if(Convert.ToInt32(mytable.Rows[0]["LineID"]) == 1)
                                UpdateBigBoxValue(mytable);
                        else if (Convert.ToInt32(mytable.Rows[0]["LineID"]) == 2)
                            UpdateBigBoxValueLineTwo(mytable);
                    }    
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdatedataAtMarkerasync -> { ex.Message}");
                MessageBox.Show($"ERROR in UpdatedataAtMarkerasync {ex.Message}");
            }
        }




    }
}
