using _8100UI.Services.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace _8100UI.Services
{
    public static class ClassCommon
    {

        public static string MainWindowTitle { get; set; } = Settings.Default.CustomerName;
        public static  List<Tuple<string, string>> SavedItemList = new List<Tuple<string, string>>();
        public static List<string> StationGraphicName = new List<string>();

        public static string DefaultFields
        {
            get => "TimeComplete, CalibrationName,GradeName, LotNumber, SerialNumber, Position," +
                "Weight, Moisture, Forte, LineName, (BDWeight/.9)+TareWeight, WeightStatus, MoistMes,LineId";
        }

        public static List<string> MoistureTypeLst = new List<string>()
        {
            "Moisture Content %",
            "Moisture Regain %",
            "Air Dry %",
            "Bone Dry %"
        };



       // public static List<ClsBaleStation> BaleStations;

        public static List<ClsStation> BaleStation;
        public static List<ClassStationGraphics> BaleStationGraphic;
        public static List<ClsConveyor> Conveyors;

        public static int CurStock = 0;
        public static int NxtStock = 1;
        public static int CurGrade = 2;
        public static int NxtGrade = 3;

        public static int DataReady = 99;
        public static int UpdateDataAtStation;
       

        public static bool UserLogin
        {
            get => Settings.Default.UserLogin;
            set
            {
                Settings.Default.UserLogin = value;
                Settings.Default.Save();
            }
        }

        public static string CurrentUser
        {
            get => Settings.Default.CurrentUser;
            set
            {
                Settings.Default.CurrentUser = value;
                Settings.Default.Save();
            }
        }

        public static int ScanIndex
        {
            get => Settings.Default.ScanIndex;
            set
            {
                Settings.Default.ScanIndex = value;
                Settings.Default.Save();
            }
        }

        public static bool MoveLeftToRight 
        { 
            get => Settings.Default.MoveLeftToRight; 
            set
            {
                Settings.Default.MoveLeftToRight = value;
                Settings.Default.Save();
            }
        }

        public static bool SingleLot
        {
            get => Settings.Default.SingleLot;
            set
            {
                Settings.Default.SingleLot = value;
                Settings.Default.Save();
            }
        }


        public static int DropSize
        {
            get => Settings.Default.DropSize;
            set
            {
                Settings.Default.DropSize = value;
                Settings.Default.Save();
            }
        }
        public static int NumDropsMax
        {
            get => Settings.Default.NumDropsMax;
            set
            {
                Settings.Default.NumDropsMax = value;
                Settings.Default.Save();
            }
        }

        public static bool DropChecked
        {
            get => Settings.Default.DropChecked;
            set
            {
                Settings.Default.DropChecked = value;
                Settings.Default.Save();
            }
        }

        public static bool SheetChecked
        {
            get => Settings.Default.SheetChecked;
            set
            {
                Settings.Default.SheetChecked = value;
                Settings.Default.Save();
            }
        }

        public static int NumberOfLine
        {
            get => Settings.Default.ProcessingLine;
            set
            {
                Settings.Default.ProcessingLine = value;
                Settings.Default.Save();
            }
        }

        public static bool StockActive
        {
            get => Settings.Default.StockActive;
            set
            {
                Settings.Default.StockActive = value;
                Settings.Default.Save();
            }
        }

        public static bool GradeActive
        {
            get => Settings.Default.GradeActive;
            set
            {
                Settings.Default.GradeActive = value;
                Settings.Default.Save();
            }
        }


        /// <summary>
        /// LineOne-------------------------------------------------------
        /// </summary>
        //ApproachConveyorLineOne Conveyor
        public static int ApproachConveyorLineOne
        {
            get => Settings.Default.ApproachLineOne;
            set
            {
                Settings.Default.ApproachLineOne = value;
                Settings.Default.Save();
            }
        }
        public static int WeighConveyorLineOne
        {
            get => Settings.Default.WeighLineOne;
            set
            {
                Settings.Default.WeighLineOne = value;
                Settings.Default.Save();
            }
        }
        public static int PressConveyorLineOne
        {
            get => Settings.Default.PressLineOne;
            set
            {
                Settings.Default.PressLineOne = value;
                Settings.Default.Save();
            }
        }
        public static int MarkerConveyorLineOne
        {
            get => Settings.Default.MarkerOneLineOne;
            set
            {
                Settings.Default.MarkerOneLineOne = value;
                Settings.Default.Save();
            }
        }



        /// <summary>
        /// LineOne-------------------------------------------------------
        /// </summary>
        //ApproachConveyorLineOne
        public static int ApproachConveyorLineTwo
        {
            get => Settings.Default.ApproachLineTwo;
            set
            {
                Settings.Default.ApproachLineTwo = value;
                Settings.Default.Save();
            }
        }
        public static int WeighConveyorLineTwo
        {
            get => Settings.Default.WeighLineTwo;
            set
            {
                Settings.Default.WeighLineTwo = value;
                Settings.Default.Save();
            }
        }
        public static int PressConveyorLineTwo
        {
            get => Settings.Default.PressLineTwo;
            set
            {
                Settings.Default.PressLineTwo = value;
                Settings.Default.Save();
            }
        }
        public static int MarkerConveyorLineTwo
        {
            get => Settings.Default.MarkerOneLineTwo;
            set
            {
                Settings.Default.MarkerOneLineTwo = value;
                Settings.Default.Save();
            }
        }
        //-------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public static int LayBoy1ConveyorId
        {
            get => Settings.Default.LayBoy1ConveyorId;
            set
            {
                Settings.Default.LayBoy1ConveyorId = value;
                Settings.Default.Save();
            }
        }

        //-----------------------------------------------------------------
      

        public static DataTable NewDattable;
        public static int NewDatIndex;

        public static string CnnVal()
        {
            return Settings.Default.ForteDbSqlCon;
        }

        //---------------------------------------------------------------
        public static string CustomerName
        {
            get => Settings.Default.CustomerName;
            set
            {
                Settings.Default.CustomerName = value;
                Settings.Default.Save();
            }
        }
        public static int MoistureType
        {
            get => Settings.Default.MoistureType;
            set
            {
                Settings.Default.MoistureType = value;
                Settings.Default.Save();
            }
        }
        public static int WeightUnit
        {
            get => Settings.Default.WeightUnit;
            set
            {
                Settings.Default.WeightUnit = value;
                Settings.Default.Save();
            }
        }
        public static int ScanPeriod
        {
            get => Settings.Default.periodScan;
            set
            {
                Settings.Default.periodScan = value;
                Settings.Default.Save();
            }
        }

        //-------------------------------------------------------------------

        public static string CustomerTitle
        {
            get => Settings.Default.CustomerTitle;
            set
            {
                Settings.Default.CustomerTitle = value;
                Settings.Default.Save();
            }
        }


        /// <summary>
        /// SQL Host-----------------------------------------------------------------
        /// </summary>
        public static string SQLHost
        {
            get => Settings.Default.Host;
            set
            {
                Settings.Default.Host = value;
                Settings.Default.Save();
            }
        }
        public static string SQLInstant
        {
            get => Settings.Default.Instant;
            set
            {
                Settings.Default.Instant = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Socket --------------------------------------------------------------------
        /// </summary>
        public static int MessageId
        {
            get => Settings.Default.MsgID;
            set
            {
                Settings.Default.MsgID = value;
                Settings.Default.Save();
            }
        }


        /// <summary>
        /// true = Run porgram from Local
        /// false = Run program from remote or ini file not found.
        /// </summary>
        public static bool RunFromLocal
        {
            get => Settings.Default.RunFromLocal;
            set
            {
                Settings.Default.RunFromLocal = value;
                Settings.Default.Save();
            }
        }
        public static string RemoteHost
        {
            get => Settings.Default.RemoteHost;
            set
            {
                Settings.Default.RemoteHost = value;
                Settings.Default.Save();
            }
        }
        public static int RemotePort
        {
            get => Settings.Default.RemotePort;
            set
            {
                Settings.Default.RemotePort = value;
                Settings.Default.Save();
            }
        }

        //------------------------------------------------------------------------------
        public static bool SheetCntCheck
        {
            get => Settings.Default.SheetCntCheck;
            set
            {
                Settings.Default.SheetCntCheck = value;
                Settings.Default.Save();
            }
        }

        public static bool BoxOneCheck
        {
            get => Settings.Default.BoxOneCheck;
            set
            {
                Settings.Default.BoxOneCheck = value;
                Settings.Default.Save();
            }
        }
        public static bool BoxTwoCheck
        {
            get => Settings.Default.BoxTwoCheck;
            set
            {
                Settings.Default.BoxTwoCheck = value;
                Settings.Default.Save();
            }
        }
        public static bool BoxThreeCheck
        {
            get => Settings.Default.BoxThreeCheck;
            set
            {
                Settings.Default.BoxThreeCheck = value;
                Settings.Default.Save();
            }
        }
        public static bool BoxFourCheck
        {
            get => Settings.Default.BoxFourCheck;
            set
            {
                Settings.Default.BoxFourCheck = value;
                Settings.Default.Save();
            }
        }
        public static bool BoxFiveCheck
        {
            get => Settings.Default.BoxFiveCheck;
            set
            {
                Settings.Default.BoxFiveCheck = value;
                Settings.Default.Save();
            }
        }
        public static bool BoxSixCheck
        {
            get => Settings.Default.BoxSixCheck;
            set
            {
                Settings.Default.BoxSixCheck = value;
                Settings.Default.Save();
            }
        }


       
        public static int LangaugeIdx
        {
            get => Settings.Default.LangaugeIdx;
            set
            {
                Settings.Default.LangaugeIdx = value;
                Settings.Default.Save();
            }
        }



        //------------------------------------------------------------------------------
        private static double _scrWidth;
        public static double ScrWidth
        {
            get => _scrWidth;
            set => _scrWidth = value;
        }

        private static double _scrHeight;
        public static double ScrHeight
        {
            get => _scrHeight;
            set => _scrHeight = value;
        }


        private static string _moistureADat;
        public static string MoistureADat
        {
            get => _moistureADat;
            set => _moistureADat = value;
        }

        private static string _weightADat;
        public static string WeightADat
        {
            get => _weightADat;
            set => _weightADat = value;
        }

        public static int LayBoyCount
        {
            get => Settings.Default.LayBoyCount;
            set
            {
                Settings.Default.LayBoyCount = value;
                Settings.Default.Save();
            }
        }

        public static bool BaleOrderLoToHiLnOne
        {
            get => Settings.Default.BaleOrderLoToHiLnOne;
            set
            {
                Settings.Default.BaleOrderLoToHiLnOne = value;
                Settings.Default.Save();
            }
        }

        public static bool BaleOrderLoToHiLnTwo
        {
            get => Settings.Default.BaleOrderLoToHiLnTwo;
            set
            {
                Settings.Default.BaleOrderLoToHiLnTwo = value;
                Settings.Default.Save();
            }
        }


        public static bool JobParam
        {
            get => Settings.Default.JobParam;
            set
            {
                Settings.Default.JobParam = value;
                Settings.Default.Save();
            }
        }

   
        //from ini file-----------------------------------------
        public static string IniCustomerName
        {
            get => Settings.Default.IniCustomerName;
            set
            {
                Settings.Default.IniCustomerName = value;
                Settings.Default.Save();
            }
        }
        public static string IniMoistureType
        {
            get => Settings.Default.IniMoistureType;
            set
            {
                Settings.Default.IniMoistureType = value;
                Settings.Default.Save();
            }
        }
        public static string IniWeightUnit
        {
            get => Settings.Default.IniWeightUnit;
            set
            {
                Settings.Default.IniWeightUnit = value;
                Settings.Default.Save();
            }
        }
        public static int IniLotSummaryDepth
        {
            get => Settings.Default.IniLotSummaryDepth;
            set
            {
                Settings.Default.IniLotSummaryDepth = value;
                Settings.Default.Save();
            }
        }
        public static int IniBaleSummaryDepth
        {
            get => Settings.Default.IniBaleSummaryDepth;
            set
            {
                Settings.Default.IniBaleSummaryDepth = value;
                Settings.Default.Save();
            }
        }

        

        //-------------------------------------------------------

        public static string ReportFields
        {
            get => Settings.Default.ReportFields;
            set
            {
                Settings.Default.ReportFields = value;
                Settings.Default.Save();
            }
        }

        public static string OpenLotSummaryXmlFilePath
        {
            get => System.AppDomain.CurrentDomain.BaseDirectory + "OpenLotSummaryItems.xml";
        }
        public static string ClosedLotSummaryXmlFilePath
        {
            get => System.AppDomain.CurrentDomain.BaseDirectory + "ClosedLotSummaryItems.xml";
        }
        public static string BaleSummaryXmlFilePath
        {
            get => System.AppDomain.CurrentDomain.BaseDirectory + "BaleSummaryItems.xml";
        }
        public static string DaySummaryXmlFilePath
        {
            get => System.AppDomain.CurrentDomain.BaseDirectory + "DaySummaryItems.xml";
        }
        public static string DayTallyXmlFilePath
        {
            get => System.AppDomain.CurrentDomain.BaseDirectory + "DayTallyItems.xml";
        }
        public static string ShiftSummaryXmlFilePath
        {
            get => System.AppDomain.CurrentDomain.BaseDirectory + "ShiftSummaryItems.xml";
        }
        public static string ShiftTallyXmlFilePath
        {
            get => System.AppDomain.CurrentDomain.BaseDirectory + "ShiftTallyItems.xml";
        }
        public static string PiorXmlFilePath
        {
            get => System.AppDomain.CurrentDomain.BaseDirectory + "PiorSumaryItems.xml";
        }

        public static string PeriodXmlFilePath
        {
            get => System.AppDomain.CurrentDomain.BaseDirectory + "PeriodSummaryItems.xml";
        }

        public static string PeriodTallyXmlFilePath
        {
            get => System.AppDomain.CurrentDomain.BaseDirectory + "PeriodTallyItems.xml";
        }

  
        //Line A
        public static int DropLineA = 0;
        public static int ScaleLineA = 1;
        public static int PressLineA = 2;
        public static int MarkerLineA = 3;
        public static int StackLineA = 4;

        //Line B
        public static int DropLineB = 10;
        public static int ScaleLineB = 11;
        public static int PressLineB = 12;
        public static int MarkerLineB = 13;
        public static int StackLineB = 14;
        public static int OpenLot = 0;
        public static int CloseLot = 1;
        public static int BaleTable = 2;
        public static int OpenDayTable = 3;
        public static int CloseDayTable = 4;
        public static int DayTallyTable = 5;
        public static int OpenShiftTable = 6;
        public static int CloseShiftTable = 7;
        public static int ShiftTallyTable = 8;
        public static int PiorTable = 9;

        public static int OpenPriodTable = 10;
        public static int ClosePriodTable = 11;
        public static int PriodTallyTable = 12;

        public static ObservableCollection<SqlReportField> ReportGridView { get; set; }
        public static int ScanRate
        {
            get => Settings.Default.ScanRate;
            set
            {
                Settings.Default.ScanRate = value;
                Settings.Default.Save();
            }
        }

        public static object AlpacModule { get; private set; }

        public static List<Tuple<string, char>> Asciilist = new List<Tuple<string, char>>() {
            new Tuple<string, char>("<NULL>", '\x0'),
            new Tuple<string, char>("<SOH>", '\x1'),
            new Tuple<string, char>("<STX>", '\x2'),
            new Tuple<string, char>("<ETX>", '\x3'),
            new Tuple<string, char>("<EOT>", '\x4'),
            new Tuple<string, char>("<ENQ>", '\x5'),
            new Tuple<string, char>("<ACK>", '\x6'),
            new Tuple<string, char>("<TAB>", '\x9'),
            new Tuple<string, char>("<LF>", '\xA'),
            new Tuple<string, char>("<VT>", '\xB'),
            new Tuple<string, char>("<FF>", '\xC'),
            new Tuple<string, char>("<CR>", '\xD'),
            new Tuple<string, char>("<SO>", '\xF'),
            new Tuple<string, char>("<ETB>", '\x17'),
            new Tuple<string, char>("<ESC>", '\x1B'),
            new Tuple<string, char>("<Space>", '\x20'),
            new Tuple<string, char>(",", '\x2C'),
            new Tuple<string, char>("-", '\x2D'),
            new Tuple<string, char>(".", '\x2E'),
            new Tuple<string, char>("#", '\x23'),
            new Tuple<string, char>(":", '\x2A'),
            new Tuple<string, char>(";", '\x2B'),
            new Tuple<string, char>("[", '\x5B'),
            new Tuple<string, char>("]", '\x5D'),
            new Tuple<string, char>("_", '\x5F')};



        public static List<Tuple<int, string>> Stations = new List<Tuple<int, string>>()
        {
            new Tuple<int, string>(0,"Drop"),
            new Tuple<int, string>(1,"Scale"),
            new Tuple<int, string>(2,"Press"),
            new Tuple<int, string>(3,"Marker"),
            new Tuple<int, string>(4,"Stacker"),
            new Tuple<int, string>(5,"Approach"),
            new Tuple<int, string>(6,"Layboy1")
        };

        public static int Drop = 0;
        public static int Scale = 1;
        public static int Press = 2;
        public static int Marker = 3;
        public static int Stacker = 4;
        public static int Approach = 5;
        public static int Layboy1 = 6;


        public static int StationOneLnOne = 0;
        public static int StationTwoLnOne = 1;
        public static int StationThreeLnOne = 2;
        public static int StationFourLnOne = 3;
        public static int StationFiveLnOne = 4;
        public static int StationFSixLnOne = 5;



        public static int StationOneLineOneID { get; set; }
        public static int StationTwoLineOneID { get; set; }
        public static int StationThreeLineOneID { get; set; }
        public static int StationFourLineOneID { get; set; }
        public static int StationFiveLineOneID { get; set; }

        /// <summary>
        /// Used for FieldSelect Windows
        /// </summary>
        public enum InstanceType : int
        {
            OpenLot = 0,
            CloseLot = 1,
            Summary = 2
        }

        public enum LotEvent : int
        {
            OpenLot = 0,
            CloseLot = 1,
            Reset = 2
        };


        public static string GetMoistureVal(float moisture)
        {
            float fMoisture;

            switch (MoistureType)
            {
                case 0:
                    fMoisture = moisture;
                    // Moisture Content % = Moisture
                    break;

                case 1:
                    fMoisture = moisture / (1 - moisture / 100);
                    // Moisture Regain % = Moisture / ( 1- Moisture / 100)
                    break;

                case 2:
                    fMoisture = (float)((100 - moisture) / .9);
                    // Air Dry % =  (100 - Moisture) / .9
                    break;

                case 3:
                    fMoisture = 100 - moisture;
                    // Bone Dry % = 100 - Moisture
                    break;
                default:
                    fMoisture = moisture;
                    break;
            }
            return fMoisture.ToString("#0.00");  // moisture.ToString("#0.00");
        }

        public static string GetMoistureValue(float moisture, int moistureType)
        {
            float fMoisture;

            switch (moistureType)
            {
                case 0:
                    fMoisture = moisture;
                    // Moisture Content % = Moisture
                    break;

                case 1:
                    fMoisture = moisture / (1 - moisture / 100);
                    // Moisture Regain % = Moisture / ( 1- Moisture / 100)
                    break;

                case 2:
                    fMoisture = (float)((100 - moisture) / .9);
                    // Air Dry % =  (100 - Moisture) / .9
                    break;

                case 3:
                    fMoisture = 100 - moisture;
                    // Bone Dry % = 100 - Moisture
                    break;
                default:
                    fMoisture = moisture;
                    break;
            }
            return fMoisture.ToString("#0.00");  // moisture.ToString("#0.00");
        }

        public static string GetWeightVal(float weight)
        {
            string strWeight = string.Empty;

            if (ClassCommon.WeightUnit == 0) strWeight = weight.ToString("#0.00");
            else if (ClassCommon.WeightUnit == 1) strWeight = (weight * 2.20462).ToString("#0.00");
            return strWeight;
        }




    }
}
