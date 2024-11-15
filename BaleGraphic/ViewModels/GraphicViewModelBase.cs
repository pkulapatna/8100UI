using _8100UI.Services;
using BaleStation.ViewModels;
using BaleStation.Views;
using Control;
using Control.ViewModels;
using MaxBalesOnCon.Views;
using Prism.Commands;
using Prism.Mvvm;
using PulpBale.ViewModels;
using PulpBale.Views;
using SheetCounter.ViewModels;
using SheetCounter.Views;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BaleGraphic.ViewModels
{
    public class GraphicViewModelBase : BindableBase
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;
        public readonly Sqlhandler _sqlHandler = Sqlhandler.Instance;

        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        private bool _userLogin;
        public bool UserLogin
        {
            get => _userLogin;
            set
            {
                SetProperty(ref _userLogin, value);
                MenuVisible = value ? MenuVisible = Visibility.Visible : MenuVisible = Visibility.Hidden;
            }
        }

        private Visibility _menuVisible;
        public Visibility MenuVisible
        {
            get { return _menuVisible; }
            set { SetProperty(ref _menuVisible, value); }
        }

        private Visibility _showStation4;
        public Visibility ShowStation4L1
        {
            get { return _showStation4; }
            set { SetProperty(ref _showStation4, value); }
        }

    
        // Bale zindex order--------------------------------------------------------------

        private int _zIndexOne = 20;
        public int ZIndexOne
        {
            get => _zIndexOne;
            set => SetProperty(ref _zIndexOne, value);
        }
        private int _zIndexTwo = 19;
        public int ZIndexTwo
        {
            get => _zIndexTwo;
            set => SetProperty(ref _zIndexTwo, value);
        }

        private int _zIndexThree = 18;
        public int ZIndexThree
        {
            get => _zIndexThree;
            set => SetProperty(ref _zIndexThree, value);
        }
        private int _zIndexFour = 17;
        public int ZIndexFour
        {
            get => _zIndexFour;
            set => SetProperty(ref _zIndexFour, value);
        }
        private int _zIndexFive = 16;
        public int ZIndexFive
        {
            get => _zIndexFive;
            set => SetProperty(ref _zIndexFive, value);
        }
        private int _zIndexSix = 15;
        public int ZIndexSix
        {
            get => _zIndexSix;
            set => SetProperty(ref _zIndexSix, value);
        }
        private int _zIndexSeven = 14;
        public int ZIndexSeven
        {
            get => _zIndexSeven;
            set => SetProperty(ref _zIndexSeven, value);
        }
        private int _zIndexEight = 13;
        public int ZIndexEight
        {
            get => _zIndexEight;
            set => SetProperty(ref _zIndexEight, value);
        }
        private int _zIndexNine = 12;
        public int ZIndexNine
        {
            get => _zIndexNine;
            set => SetProperty(ref _zIndexNine, value);
        }
        private int _zIndexTen = 11;
        public int ZIndexTen
        {
            get => _zIndexTen;
            set => SetProperty(ref _zIndexTen, value);
        }
        private int _zIndexEleven = 10;
        public int ZIndexEleven
        {
            get => _zIndexEleven;
            set => SetProperty(ref _zIndexEleven, value);
        }
        private int _zIndexTwelve = 9;
        public int ZIndexTwelve
        {
            get => _zIndexTwelve;
            set => SetProperty(ref _zIndexTwelve, value);
        }
        private int _zIndexThirteen = 8;
        public int ZIndexThirteen
        {
            get => _zIndexThirteen;
            set => SetProperty(ref _zIndexThirteen, value);
        }
        private int _zIndexForteen = 7;
        public int ZIndexFourteen
        {
            get => _zIndexForteen;
            set => SetProperty(ref _zIndexForteen, value);
        }
        private int _zIndexFifteen = 6;
        public int ZIndexFifteen
        {
            get => _zIndexFifteen;
            set => SetProperty(ref _zIndexFifteen, value);
        }

        //Line 2
        //------------------------------------------------------------------------------------

        private int _zIndexOneL2 = 20;
        public int ZIndexOneL2
        {
            get => _zIndexOneL2;
            set => SetProperty(ref _zIndexOneL2, value);
        }
        private int _zIndexTwoL2 = 19;
        public int ZIndexTwoL2
        {
            get => _zIndexTwoL2;
            set => SetProperty(ref _zIndexTwoL2, value);
        }

        private int _zIndexThreeL2 = 18;
        public int ZIndexThreeL2
        {
            get => _zIndexThreeL2;
            set => SetProperty(ref _zIndexThreeL2, value);
        }
        private int _zIndexFourL2 = 17;
        public int ZIndexFourL2
        {
            get => _zIndexFourL2;
            set => SetProperty(ref _zIndexFourL2, value);
        }
        private int _zIndexFiveL2 = 16;
        public int ZIndexFiveL2
        {
            get => _zIndexFiveL2;
            set => SetProperty(ref _zIndexFiveL2, value);
        }
        private int _zIndexSixL2 = 15;
        public int ZIndexSixL2
        {
            get => _zIndexSixL2;
            set => SetProperty(ref _zIndexSixL2, value);
        }
        private int _zIndexSevenL2 = 14;
        public int ZIndexSevenL2
        {
            get => _zIndexSevenL2;
            set => SetProperty(ref _zIndexSevenL2, value);
        }
        private int _zIndexEightL2 = 13;
        public int ZIndexEightL2
        {
            get => _zIndexEightL2;
            set => SetProperty(ref _zIndexEightL2, value);
        }
        private int _zIndexNineL2 = 12;
        public int ZIndexNineL2
        {
            get => _zIndexNineL2;
            set => SetProperty(ref _zIndexNineL2, value);
        }
        private int _zIndexTenL2 = 11;
        public int ZIndexTenL2
        {
            get => _zIndexTenL2;
            set => SetProperty(ref _zIndexTenL2, value);
        }
        private int _zIndexElevenL2 = 10;
        public int ZIndexElevenL2
        {
            get => _zIndexElevenL2;
            set => SetProperty(ref _zIndexElevenL2, value);
        }
        private int _zIndexTwelveL2 = 9;
        public int ZIndexTwelveL2
        {
            get => _zIndexTwelveL2;
            set => SetProperty(ref _zIndexTwelveL2, value);
        }
        private int _zIndexThirteenL2 = 8;
        public int ZIndexThirteenL2
        {
            get => _zIndexThirteenL2;
            set => SetProperty(ref _zIndexThirteenL2, value);
        }
        private int _zIndexForteenL2 = 7;
        public int ZIndexForteenL2
        {
            get => _zIndexForteenL2;
            set => SetProperty(ref _zIndexForteenL2, value);
        }
        private int _zIndexFifteenL2 = 6;
        public int ZIndexFifteenL2
        {
            get => _zIndexFifteenL2;
            set => SetProperty(ref _zIndexFifteenL2, value);
        }

        public void SetBaleOrdered()
        {
            int bUpDn = 20;

            //Line 1
            if (ClassCommon.BaleOrderLoToHiLnOne)
            {
                ZIndexOne = bUpDn;
                ZIndexTwo = bUpDn - 1;
                ZIndexThree = bUpDn - 2;
                ZIndexFour = bUpDn - 3;
                ZIndexFive = bUpDn - 4;
                ZIndexSix = bUpDn - 5;
                ZIndexSeven = bUpDn - 6;
                ZIndexEight = bUpDn - 7;
                ZIndexNine = bUpDn - 8;
                ZIndexTen = bUpDn - 9;
                ZIndexEleven = bUpDn - 10;
                ZIndexTwelve = bUpDn - 11;
                ZIndexThirteen = bUpDn - 12;
                ZIndexFourteen = bUpDn - 13;
                ZIndexFifteen = bUpDn - 14;
            }
            else
            {
                ZIndexOne = bUpDn - 14;
                ZIndexTwo = bUpDn - 13;
                ZIndexThree = bUpDn - 12;
                ZIndexFour = bUpDn - 11;
                ZIndexFive = bUpDn - 10;
                ZIndexSix = bUpDn - 9;
                ZIndexSeven = bUpDn - 8;
                ZIndexEight = bUpDn - 7;
                ZIndexNine = bUpDn - 6;
                ZIndexTen = bUpDn - 5;
                ZIndexEleven = bUpDn - 4;
                ZIndexTwelve = bUpDn - 3;
                ZIndexThirteen = bUpDn - 2;
                ZIndexFourteen = bUpDn - 1;
                ZIndexFifteen = bUpDn;
            }

            //Line 2
            if (ClassCommon.BaleOrderLoToHiLnTwo)
            {
                ZIndexOneL2 = bUpDn;
                ZIndexTwoL2 = bUpDn - 1;
                ZIndexThreeL2 = bUpDn - 2;
                ZIndexFourL2 = bUpDn - 3;
                ZIndexFiveL2 = bUpDn - 4;
                ZIndexSixL2 = bUpDn - 5;
                ZIndexSevenL2 = bUpDn - 6;
                ZIndexEightL2 = bUpDn - 7;
                ZIndexNineL2 = bUpDn - 8;
                ZIndexTenL2 = bUpDn - 9;
                ZIndexElevenL2 = bUpDn - 10;
                ZIndexTwelveL2 = bUpDn - 11;
                ZIndexThirteenL2 = bUpDn - 12;
                ZIndexForteenL2 = bUpDn - 13;
                ZIndexFifteenL2 = bUpDn - 14;
            }
            else
            {
                ZIndexOneL2 = bUpDn - 14;
                ZIndexTwoL2 = bUpDn - 13;
                ZIndexThreeL2 = bUpDn - 12;
                ZIndexFourL2 = bUpDn - 11;
                ZIndexFiveL2 = bUpDn - 10;
                ZIndexSixL2 = bUpDn - 9;
                ZIndexSevenL2 = bUpDn - 8;
                ZIndexEightL2 = bUpDn - 7;
                ZIndexNineL2 = bUpDn - 6;
                ZIndexTenL2 = bUpDn - 5;
                ZIndexElevenL2 = bUpDn - 4;
                ZIndexTwelveL2 = bUpDn - 3;
                ZIndexThirteenL2 = bUpDn - 2;
                ZIndexForteenL2 = bUpDn - 1;
                ZIndexFifteenL2 = bUpDn;
            }

        }


        //SheetCounter ////////////////////////////////////////////////////////
        public SheetCounterViewModel SheetCounterWindow;
        public UserControl SheetCounter => new SheetCounterView(SheetCounterWindow);
        
        private Visibility _showSheetCount;
        public Visibility ShowSheetCount
        {
            get => _showSheetCount;
            set => SetProperty(ref _showSheetCount, value);
        }

        private bool _sheetCntPresent = false;
        public bool SheetCntPresent
        {
            get => _sheetCntPresent;
            set => SetProperty(ref _sheetCntPresent, value);
        }
        /// ////////////////////////////////////////////////////////////////////////

        private string _txtStatus;
        public string txtStatus
        {
            get => _txtStatus; 
            set => SetProperty(ref _txtStatus, value); 
        }
        private string _dropInConveyorOne;
        public string DropInConveyorOne
        {
            get => _dropInConveyorOne;
            set => SetProperty(ref _dropInConveyorOne, value);
        }

        private Visibility _showDropOne = Visibility.Hidden;
        public Visibility ShowDropOne
        {
            get => _showDropOne;
            set => SetProperty(ref _showDropOne, value);
        }

        private Visibility _showDropTwo = Visibility.Hidden;
        public Visibility ShowDropTwo
        {
            get => _showDropTwo;
            set => SetProperty(ref _showDropTwo, value);
        }

        private Visibility _showDropThree = Visibility.Hidden;
        public Visibility ShowDropThree
        {
            get => _showDropThree;
            set => SetProperty(ref _showDropThree, value);
        }

        private Visibility _showDropFour = Visibility.Hidden;
        public Visibility ShowDropFour
        {
            get => _showDropFour;
            set => SetProperty(ref _showDropFour, value);
        }

        private Visibility _showDropFive = Visibility.Hidden;
        public Visibility ShowDropFive
        {
            get => _showDropFive;
            set => SetProperty(ref _showDropFive, value);
        }

        private int _baleOnApproachConveyor;
        public int BaleCountConveyorOne
        {
            get => _baleOnApproachConveyor; 
            set => SetProperty(ref _baleOnApproachConveyor, value); 
        }


        private int _baleWeighConveyor;
        public int BaleConveyorTwo
        {
            get => _baleWeighConveyor; 
            set => SetProperty(ref _baleWeighConveyor, value); 
        }

        private int _balePressConveyor;
        public int BaleConveyorThree
        {
            get => _balePressConveyor; 
            set => SetProperty(ref _balePressConveyor, value); 
        }

        private int _baleConveyorMerker;
        public int BaleConveyorMarker
        {
            get => _baleConveyorMerker; 
            set => SetProperty(ref _baleConveyorMerker, value); 
        }

        private int _baleConveyorApproachLnOne;
        public int BaleConveyorApproachLnOne
        {
            get => _baleConveyorApproachLnOne; 
            set => SetProperty(ref _baleConveyorApproachLnOne, value); 
        }

        private int _baleConveyorPressLnOne;
        public int BaleConveyorPressLnOne
        {
            get => _baleConveyorPressLnOne; 
            set => SetProperty(ref _baleConveyorPressLnOne, value); 
        }

        private int _baleConveyorMarkerLnOne;
        public int BaleConveyorMarkerLnOne
        {
            get => _baleConveyorMarkerLnOne; 
            set => SetProperty(ref _baleConveyorMarkerLnOne, value); 
        }

        //Line 2
        private int _baleConveyorScaleLineTwo;
        public int BaleConveyorApproachLineTwo
        {
            get => _baleConveyorScaleLineTwo; 
            set => SetProperty(ref _baleConveyorScaleLineTwo, value); 
        }

        private int _baleConveyorPressLineTwo;
        public int BaleConveyorPressLineTwo
        {
            get => _baleConveyorPressLineTwo; 
            set => SetProperty(ref _baleConveyorPressLineTwo, value); 
        }

        private int _baleConveyorMerkerLineTwo;
        public int BaleConveyorMarkerLineTwo
        {
            get => _baleConveyorMerkerLineTwo; 
            set => SetProperty(ref _baleConveyorMerkerLineTwo, value); 
        }

        /// <summary>
        /// Drop
        /// </summary>
        private Visibility _showDropOneLnOne = Visibility.Hidden;
        public Visibility ShowDropOneLineOne
        {
            get => _showDropOneLnOne;
            set => SetProperty(ref _showDropOneLnOne, value);
        }

        private Visibility _showDropTwoLnOne = Visibility.Hidden;
        public Visibility ShowDropTwoLineOne
        {
            get => _showDropTwoLnOne;
            set => SetProperty(ref _showDropTwoLnOne, value);
        }

        private Visibility _showDropThreeLnOne = Visibility.Hidden;
        public Visibility ShowDropThreeLineOne
        {
            get => _showDropThreeLnOne;
            set => SetProperty(ref _showDropThreeLnOne, value);
        }

        private Visibility _showDropFourLnOne = Visibility.Hidden;
        public Visibility ShowDropFourLineOne
        {
            get => _showDropFourLnOne;
            set => SetProperty(ref _showDropFourLnOne, value);
        }

        private Visibility _showDropFiveLnOne = Visibility.Hidden;
        public Visibility ShowDropFiveLineOne
        {
            get => _showDropFiveLnOne;
            set => SetProperty(ref _showDropFiveLnOne, value);
        }


        private Visibility _twoLayBoys = Visibility.Hidden;
        public Visibility TwoLayBoys
        {
            get => _twoLayBoys;
            set => SetProperty(ref _twoLayBoys, value);
        }

        private Visibility _oneLayBoy = Visibility.Hidden;
        public Visibility OneLayBoy
        {
            get => _oneLayBoy;
            set => SetProperty(ref _oneLayBoy, value);
        }


        private Visibility _showDropOneLnTwo = Visibility.Hidden;
        public Visibility ShowDropOneLineTwo
        {
            get => _showDropOneLnTwo;
            set => SetProperty(ref _showDropOneLnTwo, value);
        }

        private Visibility _showDropTwoLnTwo = Visibility.Hidden;
        public Visibility ShowDropTwoLineTwo
        {
            get => _showDropTwoLnTwo;
            set => SetProperty(ref _showDropTwoLnTwo, value);
        }

        private Visibility _showDropThreeLnTwo = Visibility.Hidden;
        public Visibility ShowDropThreeLineTwo
        {
            get => _showDropThreeLnTwo;
            set => SetProperty(ref _showDropThreeLnTwo, value);
        }

        private Visibility _showDropFourLnTwo = Visibility.Hidden;
        public Visibility ShowDropFourLineTwo
        {
            get => _showDropFourLnTwo;
            set => SetProperty(ref _showDropFourLnTwo, value);
        }
        private Visibility _showDropFiveLnTwo = Visibility.Hidden;
        public Visibility ShowDropFiveLineTwo
        {
            get => _showDropFiveLnTwo;
            set => SetProperty(ref _showDropFiveLnTwo, value);
        }


        /// <summary>
        /// User Control------------------------------------------------------
        /// </summary>
        /// Stations Line 1

        //Station Line 1 -----------------------------------------------------------
        //
        public BaleStationViewModel StationOneLineOne;
        public UserControl StationOneLayBoy => new BaleStationView(StationOneLineOne);

        public BaleStationViewModel StationTwoLineOne;
        public UserControl StationDropLineOne => new BaleStationView(StationTwoLineOne);

        public BaleStationViewModel StationThreeLineOne;  
        public UserControl StationScaleLineOne => new BaleStationView(StationThreeLineOne);
      
        public BaleStationViewModel StationFourLineOne;
        public UserControl StationPressLineOne => new BaleStationView(StationFourLineOne);

        public BaleStationViewModel StationFiveLineOne;
        public UserControl StationMarkerLineOne => new BaleStationView(StationFiveLineOne);
        // End Stations Line One---------------------------------------------------------

        // Stations Line Two ------------------------------------------------------------
        public BaleStationLineTwoViewModel StationOneLineTwo;
        public UserControl StationTwoLayBoy => new BaleStationLineTwoView(StationOneLineTwo);

        public BaleStationLineTwoViewModel StationTwoLineTwo;
        public UserControl StationTwoDrop => new BaleStationLineTwoView(StationTwoLineTwo);
        
        public BaleStationLineTwoViewModel StationThreeLineTwo;
        public UserControl StationTwoScale => new BaleStationLineTwoView(StationThreeLineTwo);
        
        public BaleStationLineTwoViewModel StationFourLineTwo;
        public UserControl StationTwoPress => new BaleStationLineTwoView(StationFourLineTwo);

        public BaleStationLineTwoViewModel StationFiveLineTwo;
        public UserControl StationTwoMarker => new BaleStationLineTwoView(StationFiveLineTwo);

        //End Line Two---------------------------------------------------------




        /// <summary>
        /// Stations Line 1 ////////////////////////////////////////////////////////////////////////
        /// </summary>
        public BaleStationViewModel LayBoyStationVM;
        public UserControl StationLayBoy => new BaleStationView(LayBoyStationVM);

        public BaleStationViewModel ApproachStationVM;
        public UserControl StationApproach => new BaleStationView(ApproachStationVM);
   
        public BaleStationViewModel StationOneVM;
        public UserControl StationNumberOne => new BaleStationView(StationOneVM);

        public BaleStationViewModel StationTwoVM;
        public UserControl StationNumberTwo => new BaleStationView(StationTwoVM);

        public BaleStationViewModel StationThreeVM;
        public UserControl StationNumberThree => new BaleStationView(StationThreeVM);

        public BaleStationViewModel StationFourVM;
        public UserControl StationNumberFour => new BaleStationView(StationFourVM);

        //-/////////////////////////////////////////////////////////////////////////////////////////



        public BaleViewModel BaleOne;
        public UserControl BaleNumberOne => new BaleView(BaleOne);

        public BaleViewModel BaleTwo;
        public UserControl BaleNumberTwo => new BaleView(BaleTwo);

        public BaleViewModel BaleThree;
        public UserControl BaleNumberThree => new BaleView(BaleThree);

        public BaleViewModel BaleFour;
        public UserControl BaleNumberFour => new BaleView(BaleFour);

        public BaleViewModel BaleFive;
        public UserControl BaleNumberFive => new BaleView(BaleFive);

        public BaleViewModel BaleSix;
        public UserControl BaleNumberSix => new BaleView(BaleSix);

        public BaleViewModel BaleSeven;
        public UserControl BaleNumberSeven => new BaleView(BaleSeven);

        public BaleViewModel BaleEight;
        public UserControl BaleNumberEight => new BaleView(BaleEight);

        public BaleViewModel BaleNine;
        public UserControl BaleNumberNine => new BaleView(BaleNine);

        public BaleViewModel BaleTen;
        public UserControl BaleNumberTen => new BaleView(BaleTen);

        // Gost Bales--------------------------------------------------------
        public BaleViewModel BaleEleven;
        public UserControl BaleNumberEleven => new BaleView(BaleEleven);

        public BaleViewModel BaleTwelve;
        public UserControl BaleNumberTwelve => new BaleView(BaleTwelve);

        public BaleViewModel BaleThirteen;
        public UserControl BaleNumberThirteen => new BaleView(BaleThirteen);

        public BaleViewModel BaleFourteen;
        public UserControl BaleNumberFourteen => new BaleView(BaleThirteen);

        public BaleViewModel BaleFifteen;
        public UserControl BaleNumberFifteen => new BaleView(BaleFifteen);

        // Default Scale-------------------------------------------------------------------
        public BaleViewModel DefaultOneScale;
        public UserControl BaleDefaultOneScale => new BaleView(DefaultOneScale);
        public BaleViewModel DefaultTwoScale;
        public UserControl BaleDefaultTwoScale => new BaleView(DefaultTwoScale);
        public BaleViewModel DefaultThreeScale;
        public UserControl BaleDefaultThreeScale => new BaleView(DefaultThreeScale);
        public BaleViewModel DefaultFourScale;
        public UserControl BaleDefaultFourScale => new BaleView(DefaultFourScale);
        public BaleViewModel DefaultFiveScale;
        public UserControl BaleDefaultFiveScale => new BaleView(DefaultFiveScale);

        //Bales Line one at scale
        public BaleViewModel baleOneLineOne;
        public UserControl BaleOneLineOne => new BaleView(baleOneLineOne);
        public BaleViewModel baleTwoLineOne;
        public UserControl BaleTwoLineOne => new BaleView(baleTwoLineOne);
        public BaleViewModel baleThreeLineOne;
        public UserControl BaleThreeLineOne => new BaleView(baleThreeLineOne);
        public BaleViewModel baleFourLineOne;
        public UserControl BaleFourLineOne => new BaleView(baleFourLineOne);
        public BaleViewModel baleFiveLineOne;
        public UserControl BaleFiveLineOne => new BaleView(baleFiveLineOne);
        public BaleViewModel baleSixLineOne;
        public UserControl BaleSixLineOne => new BaleView(baleSixLineOne);
        public BaleViewModel baleSevenLineOne;
        public UserControl BaleSevenLineOne => new BaleView(baleSevenLineOne);
        public BaleViewModel baleEightLineOne;
        public UserControl BaleEightLineOne => new BaleView(baleEightLineOne);
        public BaleViewModel baleNineLineOne;
        public UserControl BaleNineLineOne => new BaleView(baleNineLineOne);
        public BaleViewModel baleTenLineOne;
        public UserControl BaleTenLineOne => new BaleView(baleTenLineOne);
        public BaleViewModel baleElevenLineOne;
        public UserControl BaleElevenLineOne => new BaleView(baleElevenLineOne);
        public BaleViewModel baleTwelveLineOne;
        public UserControl BaleTwelveLineOne => new BaleView(baleTwelveLineOne);
        public BaleViewModel baleThirteenLineOne;
        public UserControl BaleThirteenLineOne => new BaleView(baleThirteenLineOne);
        public BaleViewModel baleFourteenLineOne;
        public UserControl BaleFourteenLineOne => new BaleView(baleFourteenLineOne);
        public BaleViewModel baleFifteenLineOne;
        public UserControl BaleFifteenLineOne => new BaleView(baleFifteenLineOne);

        // Default Scale Line One ----------------------------------------------------
        public BaleViewModel DefaultScaleLineOneBaleOne;
        public UserControl BaleDefaultOneScaleLnOne => new BaleView(DefaultScaleLineOneBaleOne);
        public BaleViewModel DefaultScaleLineOneBaleTwo;
        public UserControl BaleDefaultTwoScaleLnOne => new BaleView(DefaultScaleLineOneBaleTwo);
        public BaleViewModel DefaultScaleLineOneBaleThree;
        public UserControl BaleDefaultThreeScaleLnOne => new BaleView(DefaultScaleLineOneBaleThree);
        public BaleViewModel DefaultScaleLineOneBaleFour;
        public UserControl BaleDefaultFourScaleLnOne => new BaleView(DefaultScaleLineOneBaleFour);
        public BaleViewModel DefaultFiveScaleLnFive;
        public UserControl BaleDefaultFiveScaleLnOne => new BaleView(DefaultFiveScaleLnFive);


        //-------------------------------------------------------------------------------
        // Bale on Press Conveyor line One
        //
        public BaleViewModel PressLineOneBaleOne;
        public UserControl BaleNumberOneLnOnePress => new BaleView(PressLineOneBaleOne);
        public BaleViewModel PressLineOneBaleTwo;
        public UserControl BaleNumberTwoLnOnePress => new BaleView(PressLineOneBaleTwo);
        public BaleViewModel PressLineOneBaleThree;
        public UserControl BaleNumberThreeLnOnePress => new BaleView(PressLineOneBaleThree);
        public BaleViewModel PressLineOneBaleFour;
        public UserControl BaleNumberFourLnOnePress => new BaleView(PressLineOneBaleFour);
        public BaleViewModel PressLineOneBaleFive;
        public UserControl BaleNumberFiveLnOnePress => new BaleView(PressLineOneBaleFive);
        public BaleViewModel PressLineOneBaleSix;
        public UserControl BaleNumberSixLnOnePress => new BaleView(PressLineOneBaleSix);
        public BaleViewModel PressLineOneBaleSeven;
        public UserControl BaleNumberSevenLnOnePress => new BaleView(PressLineOneBaleSeven);
        public BaleViewModel PressLineOneBaleEight;
        public UserControl BaleNumberEightLnOnePress => new BaleView(PressLineOneBaleEight);

        //--------------------------------------------------------------------
        // Default Bale on press conveyor Line One
        //
        public BaleViewModel DefaultOnePressLnOne;
        public UserControl BaleDefaultOnePressLnOne => new BaleView(DefaultOnePressLnOne);
        public BaleViewModel DefaultTwoPressLnOne;
        public UserControl BaleDefaultTwoPressLnOne => new BaleView(DefaultTwoPressLnOne);
        public BaleViewModel DefaultThreePressLnOne;
        public UserControl BaleDefaultThreePressLnOne => new BaleView(DefaultThreePressLnOne);
        public BaleViewModel DefaultFourPressLnOne;
        public UserControl BaleDefaultFourPressLnOne => new BaleView(DefaultFourPressLnOne);
        public BaleViewModel DefaultFivePressLnOne;
        public UserControl BaleDefaultFivePressLnOne => new BaleView(DefaultFivePressLnOne);

        //--------------------------------------------------------------------
        // At Press
        public BaleViewModel BaleOnePress;
        public UserControl BaleNumberOnePress => new BaleView(BaleOnePress);
        public BaleViewModel BaleTwoPress;
        public UserControl BaleNumberTwoPress => new BaleView(BaleTwoPress);

        public BaleViewModel BaleThreePress;
        public UserControl BaleNumberThreePress => new BaleView(BaleThreePress);

        public BaleViewModel BaleFourPress;
        public UserControl BaleNumberFourPress => new BaleView(BaleFourPress);

        public BaleViewModel BaleFivePress;
        public UserControl BaleNumberFivePress => new BaleView(BaleFivePress);

        public BaleViewModel BaleSixPress;
        public UserControl BaleNumberSixPress => new BaleView(BaleSixPress);

        public BaleViewModel BaleSevenPress;
        public UserControl BaleNumberSevenPress => new BaleView(BaleSevenPress);

        public BaleViewModel BaleEightPress;
        public UserControl BaleNumberEightPress => new BaleView(BaleEightPress);

        public BaleViewModel BaleNinePress;
        public UserControl BaleNumberNinePress => new BaleView(BaleNinePress);

        public BaleViewModel BaleTenPress;
        public UserControl BaleNumberTenPress => new BaleView(BaleTenPress);



        //--------------------------------------------------------------------
        // At Press Default
        public BaleViewModel DefaultOnePress;
        public UserControl BaleDefaultOnePress => new BaleView(DefaultOnePress);
        public BaleViewModel DefaultTwoPress;
        public UserControl BaleDefaultTwoPress => new BaleView(DefaultTwoPress);
        public BaleViewModel DefaultThreePress;
        public UserControl BaleDefaultThreePress => new BaleView(DefaultThreePress);
        public BaleViewModel DefaultFourPress;
        public UserControl BaleDefaultFourPress => new BaleView(DefaultFourPress);
        public BaleViewModel DefaultFivePress;
        public UserControl BaleDefaultFivePress => new BaleView(DefaultFivePress);

        //-----------------------------------------------------------------------------
        // Bales on Marker Line one.
        //
        public BaleViewModel MarkerLineOneBaleOne;
        public UserControl BaleNumberOneLnOneMrk => new BaleView(MarkerLineOneBaleOne);
        public BaleViewModel MarkerLineOneBaleTwo;
        public UserControl BaleNumberTwoLineOneMrk => new BaleView(MarkerLineOneBaleTwo);
        public BaleViewModel MarkerLineOneBaleThree;
        public UserControl BaleNumberThreeLnOneMrk => new BaleView(MarkerLineOneBaleThree);
        public BaleViewModel MarkerLineOneBaleFour;
        public UserControl BaleNumberFourLnOneMrk => new BaleView(MarkerLineOneBaleFour);
        public BaleViewModel MarkerLineOneBaleFive;
        public UserControl BaleNumberFiveLnOneMrk => new BaleView(MarkerLineOneBaleFive);
        public BaleViewModel MarkerLineOneBaleSix;
        public UserControl BaleNumberSixLnOneMrk => new BaleView(MarkerLineOneBaleSix);
        public BaleViewModel MarkerLineOneBaleSeven;
        public UserControl BaleNumberSevenLnOneMrk => new BaleView(MarkerLineOneBaleSeven);
        public BaleViewModel MarkerLineOneBaleEight;
        public UserControl BaleNumberEightLnOneMrk => new BaleView(MarkerLineOneBaleEight);
        //--------------------------------------------------------------------
        // Default Bale on Marker conveyor Line One
        //
        public BaleViewModel BaleDefaultOneMrkLnOne;
        public UserControl BaleDefaultOneMarkerLnOne => new BaleView(BaleDefaultOneMrkLnOne);
        public BaleViewModel BaleDefaultTwoMrkLnOne;
        public UserControl BaleDefaultTwoMarkerLnOne => new BaleView(BaleDefaultTwoMrkLnOne);
        public BaleViewModel BaleDefaultThreeMrkLnOne;
        public UserControl BaleDefaultThreeMarkerLnOne => new BaleView(BaleDefaultThreeMrkLnOne);
        public BaleViewModel BaleDefaultFourMrkLnOne;
        public UserControl BaleDefaultFourMarkerLnOne => new BaleView(BaleDefaultFourMrkLnOne);
        public BaleViewModel BaleDefaultFiveMrkLnOne;
        public UserControl BaleDefaultFiveMarkerLnOne => new BaleView(BaleDefaultFiveMrkLnOne);
        //-------------------------------------------------------------------------------

        /// <summary>
        /// Line 2 Bales Line Two on Scale conveyor ////////////////////// 
        /// </summary>
        public BaleViewModel baleOneLineTwo;
        public UserControl BaleOneLineTwo => new BaleView(baleOneLineTwo);
        public BaleViewModel baleTwoLineTwo;
        public UserControl BaleTwoLineTwo => new BaleView(baleTwoLineTwo);
        public BaleViewModel baleThreeLineTwo;
        public UserControl BaleThreeLineTwo => new BaleView(baleThreeLineTwo);
        public BaleViewModel baleFourLineTwo;
        public UserControl BaleFourLineTwo => new BaleView(baleFourLineTwo);
        public BaleViewModel baleFiveLineTwo;
        public UserControl BaleFiveLineTwo => new BaleView(baleFiveLineTwo);
        public BaleViewModel baleSixLineTwo;
        public UserControl BaleSixLineTwo => new BaleView(baleSixLineTwo);
        public BaleViewModel baleSevenLineTwo;
        public UserControl BaleSevenLineTwo => new BaleView(baleSevenLineTwo);
        public BaleViewModel baleEightLineTwo;
        public UserControl BaleEightLineTwo => new BaleView(baleEightLineTwo);
        public BaleViewModel baleNineLineTwo;
        public UserControl BaleNineLineTwo => new BaleView(baleNineLineTwo);
        public BaleViewModel baleTenLineTwo;
        public UserControl BaleTenLineTwo => new BaleView(baleTenLineTwo);
        public BaleViewModel baleElevenLineTwo;
        public UserControl BaleElevenLineTwo => new BaleView(baleElevenLineTwo);
        public BaleViewModel baleTwelveLineTwo;
        public UserControl BaleTwelveLineTwo => new BaleView(baleTwelveLineTwo);
        public BaleViewModel baleThirteenLineTwo;
        public UserControl BaleThirteenLineTwo => new BaleView(baleThirteenLineTwo);
        public BaleViewModel baleFourteenLineTwo;
        public UserControl BaleFourteenLineTwo => new BaleView(baleFourteenLineTwo);
        public BaleViewModel baleFifteenLineTwo;
        public UserControl BaleFifteenLineTwo => new BaleView(baleFifteenLineTwo);

        /// <summary>
        ///  Line 2 Default Bales on Scale conveyor ////////////////
        /// </summary>
        public BaleViewModel DefaultOneScaleLnTwo;
        public UserControl BaleDefaultOneScaleLnTwo => new BaleView(DefaultOneScaleLnTwo);
        public BaleViewModel DefaultTwoScaleLnTwo;
        public UserControl BaleDefaultTwoScaleLnTwo => new BaleView(DefaultTwoScaleLnTwo);
        public BaleViewModel DefaultThreeScaleLnTwo;
        public UserControl BaleDefaultThreeScaleLnTwo => new BaleView(DefaultThreeScaleLnTwo);
        public BaleViewModel DefaultFourScaleLnTwo;
        public UserControl BaleDefaultFourScaleLnTwo => new BaleView(DefaultFourScaleLnTwo);
        public BaleViewModel DefaultFiveScaleLnTwo;
        public UserControl BaleDefaultFiveScaleLnTwo => new BaleView(DefaultFiveScaleLnFive);


        /// <summary>
        /// Line 2 on Press Conveyor ///////////////////////////////
        /// </summary>

        public BaleViewModel PressLineTwoBaleOne;
        public UserControl BaleOneLineTwoPress => new BaleView(PressLineTwoBaleOne);
        public BaleViewModel PressLineTwoBaleTwo;
        public UserControl BaleTwoLineTwoPress => new BaleView(PressLineTwoBaleTwo);
        public BaleViewModel PressLineTwoBaleThree;
        public UserControl BaleEightLineTwoPress => new BaleView(PressLineTwoBaleThree);
        public BaleViewModel PressLineTwoBaleFour;
        public UserControl BaleThreeLineTwoPress => new BaleView(PressLineTwoBaleFour);
        public BaleViewModel baleFourLineTwoOnFive;
        public UserControl BaleFourLineTwoPress => new BaleView(baleFourLineTwoOnFive);
        public BaleViewModel PressLineTwoBaleSix;
        public UserControl BaleFiveLineTwoPress => new BaleView(PressLineTwoBaleSix);
        public BaleViewModel PressLineTwoBaleSeven;
        public UserControl BaleSixLineTwoPress => new BaleView(PressLineTwoBaleSeven);
        public BaleViewModel PressLineTwoBaleEight;
        public UserControl BaleSevenLineTwoPress => new BaleView(PressLineTwoBaleEight);

        /// <summary>
        ///Default Bale on press conveyor Line Two ///////////////////// 
        /// </summary>
        public BaleViewModel DefaultOnePressLnTwo;
        public UserControl BaleDefaultOnePressLnTwo => new BaleView(DefaultOnePressLnTwo);
        public BaleViewModel DefaultTwoPressLnTwo;
        public UserControl BaleDefaultTwoPressLnTwo => new BaleView(DefaultTwoPressLnTwo);
        public BaleViewModel DefaultThreePressLnTwo;
        public UserControl BaleDefaultThreePressLnTwo => new BaleView(DefaultThreePressLnTwo);
        public BaleViewModel DefaultFourPressLnTwo;
        public UserControl BaleDefaultFourPressLnTwo => new BaleView(DefaultFourPressLnTwo);
        public BaleViewModel DefaultFivePressLnTwo;
        public UserControl BaleDefaultFivePressLnTwo => new BaleView(DefaultFivePressLnTwo);

        //------------------
        //--------------------------------------------------
        // At Marker
        public BaleViewModel BaleOneMrk;
        public UserControl BaleNumberOneMrk => new BaleView(BaleOneMrk);
        public BaleViewModel BaleTwoMrk;
        public UserControl BaleNumberTwoMrk => new BaleView(BaleTwoMrk);

        public BaleViewModel BaleThreeMrk;
        public UserControl BaleNumberThreeMrk => new BaleView(BaleThreeMrk);

        public BaleViewModel BaleFourMrk;
        public UserControl BaleNumberFourMrk => new BaleView(BaleFourMrk);

        public BaleViewModel BaleFiveMrk;
        public UserControl BaleNumberFiveMrk => new BaleView(BaleFiveMrk);

        public BaleViewModel BaleSixMrk;
        public UserControl BaleNumberSixMrk => new BaleView(BaleSixMrk);

        public BaleViewModel BaleSevenMrk;
        public UserControl BaleNumberSevenMrk => new BaleView(BaleSevenMrk);

        public BaleViewModel BaleEightMrk;
        public UserControl BaleNumberEightMrk => new BaleView(BaleEightMrk);

        public BaleViewModel BaleNineMrk;
        public UserControl BaleNumberNineMrk => new BaleView(BaleNineMrk);

        public BaleViewModel BaleTenMrk;
        public UserControl BaleNumberTenMrk => new BaleView(BaleTenMrk);

        //---------------------------------------------------------------------
        public BaleViewModel BaleDefaultOneMrk;
        public UserControl BaleDefaultOneMarker => new BaleView(BaleDefaultOneMrk);

        public BaleViewModel BaleDefaultTwoMrk;
        public UserControl BaleDefaultTwoMarker => new BaleView(BaleDefaultTwoMrk);

        public BaleViewModel BaleDefaultThreeMrk;
        public UserControl BaleDefaultThreeMarker => new BaleView(BaleDefaultThreeMrk);

        public BaleViewModel BaleDefaultFourMrk;
        public UserControl BaleDefaultFourMarker => new BaleView(BaleDefaultFourMrk);

        public BaleViewModel BaleDefaultFiveMrk;
        public UserControl BaleDefaultFiveMarker => new BaleView(BaleDefaultFiveMrk);

        //--------------------------------------------------------------------
        // Line 2 on Marker Conveyor
        //
        public BaleViewModel MarkerLineTwoBaleOne;
        public UserControl BaleOneLineTwoMrk => new BaleView(MarkerLineTwoBaleOne);
        public BaleViewModel MarkerLineTwoBaleTwo;
        public UserControl BaleTwoLineTwoMrk => new BaleView(MarkerLineTwoBaleTwo);
        public BaleViewModel MarkerLineTwoBaleThree;
        public UserControl BaleThreeLineTwoMrk => new BaleView(MarkerLineTwoBaleThree);
        public BaleViewModel MarkerLineTwoBaleFour;
        public UserControl BaleFourLineTwoMrk => new BaleView(MarkerLineTwoBaleFour);
        public BaleViewModel MarkerLineTwoBaleFive;
        public UserControl BaleFiveLineTwoMrk => new BaleView(MarkerLineTwoBaleFive);
        public BaleViewModel MarkerLineTwoBaleSix;
        public UserControl BaleSixLineTwoMrk => new BaleView(MarkerLineTwoBaleSix);
        public BaleViewModel MarkerLineTwoBaleSeven;
        public UserControl BaleSevenLineTwoMrk => new BaleView(MarkerLineTwoBaleSeven);
        public BaleViewModel MarkerLineTwoBaleEight;
        public UserControl BaleEightLineTwoMrk => new BaleView(MarkerLineTwoBaleEight);

        //--------------------------------------------------------------------
        // Default Bale on Marker conveyor Line two
        //
        public BaleViewModel BaleDefaultOneMrkLnTwo;
        public UserControl BaleDefaultOneMarkerLnTwo => new BaleView(BaleDefaultOneMrkLnTwo);
        
        public BaleViewModel BaleDefaultTwoMrkLnTwo;
        public UserControl BaleDefaultTwoMarkerLnTwo => new BaleView(BaleDefaultTwoMrkLnTwo);
        
        public BaleViewModel BaleDefaultThreeMrkLnTwo;
        public UserControl BaleDefaultThreeMarkerLnTwo => new BaleView(BaleDefaultThreeMrkLnTwo);
        
        public BaleViewModel BaleDefaultFourMrkLnTwo;
        public UserControl BaleDefaultFourMarkerLnTwo => new BaleView(BaleDefaultFourMrkLnTwo);

        public BaleViewModel BaleDefaultFiveMrkLnTwo;
        public UserControl BaleDefaultFiveMarkerLnTwo => new BaleView(BaleDefaultFiveMrkLnOne);

        //---------------------------------------------------------------------

        public ConverorViewModel conveyorscale;
        public UserControl conveyorScale => new Conveyor(conveyorscale);
        
        public ConverorViewModel conveyorpress;
        public UserControl ConveyorPress => new Conveyor(conveyorpress);

        public ConverorViewModel conveyorMkr;
        public UserControl ConveyorMkr => new Conveyor(conveyorMkr);


        // End User Control
        //---------------------------------------------------------------------
        private DelegateCommand _incScaleConvCmd;
        public DelegateCommand IncScaleConvCmd =>
       _incScaleConvCmd ?? (_incScaleConvCmd =
            new DelegateCommand(IncScaleConvCmdExecute));
        private void IncScaleConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"IncScale;{ClassCommon.WeighConveyorLineOne};");
        }


        private DelegateCommand _decScaleConvCmd;
        public DelegateCommand DecScaleConvCmd =>
       _decScaleConvCmd ?? (_decScaleConvCmd =
            new DelegateCommand(DecScaleConvCmdExecute));
        private void DecScaleConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"DecScale;{ClassCommon.WeighConveyorLineOne};");
        }
        //----------------------------------------------------------------------

        private DelegateCommand _incPressConvCmd;
        public DelegateCommand IncPressConvCmd =>
       _incPressConvCmd ?? (_incPressConvCmd =
            new DelegateCommand(IncPressConvCmdExecute));
        private void IncPressConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"IncPress;{ClassCommon.PressConveyorLineOne};");
        }
        private DelegateCommand _decPressConvCmd;
        public DelegateCommand DecPressConvCmd =>
       _decPressConvCmd ?? (_decPressConvCmd =
            new DelegateCommand(DecPressConvCmdExecute));
        private void DecPressConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"DecPress;{ClassCommon.PressConveyorLineOne};");
        }
        //----------------------------------------------------------------------
        private DelegateCommand _incMkrConvCmd;
        public DelegateCommand IncMkrConvCmd =>
       _incMkrConvCmd ?? (_incMkrConvCmd =
            new DelegateCommand(IncMkrConvCmdExecute));
        private void IncMkrConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"IncMarker;{ClassCommon.MarkerConveyorLineOne};");
        }

        private DelegateCommand _decMkrConvCmd;
        public DelegateCommand DecMkrConvCmd =>
       _decMkrConvCmd ?? (_decMkrConvCmd =
            new DelegateCommand(DecMkrConvCmdExecute));
        private void DecMkrConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"DecMarker;{ClassCommon.MarkerConveyorLineOne};");
        }
        //------------------------------------------------------------------------

        private DelegateCommand _maxPressConvCmd;
        public DelegateCommand MaxPressConvCmd =>
       _maxPressConvCmd ?? (_maxPressConvCmd =
            new DelegateCommand(MaxPressConvCmdExecute).ObservesCanExecute(() => UserLogin));
        private void MaxPressConvCmdExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "Maximum Bale on Press Conveyor",
                Width = 400,
                Height = 200,
                ResizeMode = ResizeMode.NoResize,
                Content = new MaxBalesView(_eventAggregator, ClassCommon.PressConveyorLineOne)
            };
            _setupWindow?.ShowDialog();

        }
        //----------------------------------------------------------------------
        private DelegateCommand _clearScaleConvCmd;
        public DelegateCommand ClearScaleConvCmd =>
       _clearScaleConvCmd ?? (_clearScaleConvCmd =
            new DelegateCommand(ClearScaleConvCmdExecute));
        private void ClearScaleConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"ClrScaleConv;{ClassCommon.WeighConveyorLineOne};");
        }

        private DelegateCommand _clearPressConvCmd;
        public DelegateCommand ClearPressConvCmd =>
       _clearPressConvCmd ?? (_clearPressConvCmd =
            new DelegateCommand(ClearPressConvCmdExecute));
        private void ClearPressConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"ClrPressConv;{ClassCommon.PressConveyorLineOne};");
        }

        //-----------------------------------------------------------------------------------------
        private DelegateCommand _clearMkrConvCmd;
        public DelegateCommand ClearMkrConvCmd =>
       _clearMkrConvCmd ?? (_clearMkrConvCmd =
            new DelegateCommand(ClearMkrConvCmdExecute).ObservesCanExecute(() => UserLogin));
        private void ClearMkrConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"ClrMkrConv;{ClassCommon.MarkerConveyorLineOne};");
        }
        //-----------------------------------------------------------------------------------------

        private DelegateCommand _maxScaleConvCmd;
        public DelegateCommand MaxScaleConvCmd =>
       _maxScaleConvCmd ?? (_maxScaleConvCmd =
            new DelegateCommand(MaxScaleConvCmdExecute).ObservesCanExecute(() => UserLogin));
        private void MaxScaleConvCmdExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "Maximum Bale on Scale Conveyor",
                Width = 400,
                Height = 200,
                ResizeMode = ResizeMode.NoResize,
                Content = new MaxBalesView(_eventAggregator, ClassCommon.WeighConveyorLineOne)
            };
            _setupWindow?.ShowDialog();
        }

        private DelegateCommand _maxMkrConvCmd;
        public DelegateCommand MaxMkrConvCmd =>
       _maxMkrConvCmd ?? (_maxMkrConvCmd =
            new DelegateCommand(MaxMkrConvCmdExecute));
        private void MaxMkrConvCmdExecute()
        {
            SetUpWindow _setupWindow = new SetUpWindow
            {
                Title = "Maximum Bale on Marker Conveyor",
                Width = 400,
                Height = 200,
                ResizeMode = ResizeMode.NoResize,
                Content = new MaxBalesView(_eventAggregator, ClassCommon.MarkerConveyorLineOne)
            };
            _setupWindow?.ShowDialog();
        }
        //-----------------------------------------------------------------------------------------


    }

}
