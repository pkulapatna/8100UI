using _8100UI.Services;
using BaleStation.ViewModels;
using BaleStation.Views;
using Prism.Commands;
using Prism.Mvvm;
using PulpBale.ViewModels;
using PulpBale.Views;
using SheetCounter.ViewModels;
using SheetCounter.Views;
using System.Windows;
using System.Windows.Controls;
using MaxBalesOnCon.Views;


namespace BaleGraphicTwoLines.ViewModels
{
    public class TwoLineViewModelBase : BindableBase
    {

        protected readonly Prism.Events.IEventAggregator _eventAggregator;
      
        public readonly Sqlhandler _sqlHandler = Sqlhandler.Instance;

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
        public int ZIndexForteen
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

        //Station Line 1 -----------------------------------------------------------
        //
        public BaleStationViewModel StationOneLineOne;
        public UserControl StationOneLayBoy
        {
            get { return new BaleStationView(StationOneLineOne); }
        }

        public BaleStationViewModel StationTwoLineOne;
        public UserControl StationDropLineOne
        {
            get { return new BaleStationView(StationTwoLineOne); }
        }
        public BaleStationViewModel StationThreeLineOne;
        public UserControl StationScaleLineOne
        {
            get { return new BaleStationView(StationThreeLineOne); }
        }
        public BaleStationViewModel StationFourLineOne;
        public UserControl StationPressLineOne
        {
            get { return new BaleStationView(StationFourLineOne); }
        }
        public BaleStationViewModel StationFiveLineOne;
        public UserControl StationMarkerLineOne
        {
            get { return new BaleStationView(StationFiveLineOne); }
        }

        //Station Line 2 ------------------------------------------------------------

        public BaleStationLineTwoViewModel StationOneLineTwo;
        public UserControl StationTwoLayBoy
        {
            get { return new BaleStationLineTwoView(StationOneLineTwo); }
        }

        public BaleStationLineTwoViewModel StationTwoLineTwo;
        public UserControl StationTwoDrop
        {
            get { return new BaleStationLineTwoView(StationTwoLineTwo); }
        }
        public BaleStationLineTwoViewModel StationThreeLineTwo;
        public UserControl StationTwoScale
        {
            get { return new BaleStationLineTwoView(StationThreeLineTwo); }
        }
        public BaleStationLineTwoViewModel StationFourLineTwo;
        public UserControl StationTwoPress
        {
            get { return new BaleStationLineTwoView(StationFourLineTwo); }
        }
        public BaleStationLineTwoViewModel StationFiveLineTwo;
        public UserControl StationTwoMarker
        {
            get { return new BaleStationLineTwoView(StationFiveLineTwo); }
        }


        //Bales Line one at scale
        public BaleViewModel baleOneLineOne;
        public UserControl BaleOneLineOne
        {
            get { return new BaleView(baleOneLineOne); }
        }
        public BaleViewModel baleTwoLineOne;
        public UserControl BaleTwoLineOne
        {
            get { return new BaleView(baleTwoLineOne); }
        }
        public BaleViewModel baleThreeLineOne;
        public UserControl BaleThreeLineOne
        {
            get { return new BaleView(baleThreeLineOne); }
        }
        public BaleViewModel baleFourLineOne;
        public UserControl BaleFourLineOne
        {
            get { return new BaleView(baleFourLineOne); }
        }
        public BaleViewModel baleFiveLineOne;
        public UserControl BaleFiveLineOne
        {
            get { return new BaleView(baleFiveLineOne); }
        }
        public BaleViewModel baleSixLineOne;
        public UserControl BaleSixLineOne
        {
            get { return new BaleView(baleSixLineOne); }
        }
        public BaleViewModel baleSevenLineOne;
        public UserControl BaleSevenLineOne
        {
            get { return new BaleView(baleSevenLineOne); }
        }
        public BaleViewModel baleEightLineOne;
        public UserControl BaleEightLineOne
        {
            get { return new BaleView(baleEightLineOne); }
        }
        public BaleViewModel baleNineLineOne;
        public UserControl BaleNineLineOne
        {
            get { return new BaleView(baleNineLineOne); }
        }
        public BaleViewModel baleTenLineOne;
        public UserControl BaleTenLineOne
        {
            get { return new BaleView(baleTenLineOne); }
        }
        public BaleViewModel baleElevenLineOne;
        public UserControl BaleElevenLineOne
        {
            get { return new BaleView(baleElevenLineOne); }
        }
        public BaleViewModel baleTwelveLineOne;
        public UserControl BaleTwelveLineOne
        {
            get { return new BaleView(baleTwelveLineOne); }
        }
        public BaleViewModel baleThirteenLineOne;
        public UserControl BaleThirteenLineOne
        {
            get { return new BaleView(baleThirteenLineOne); }
        }
        public BaleViewModel baleFourteenLineOne;
        public UserControl BaleFourteenLineOne
        {
            get { return new BaleView(baleFourteenLineOne); }
        }
        public BaleViewModel baleFifteenLineOne;
        public UserControl BaleFifteenLineOne
        {
            get { return new BaleView(baleFifteenLineOne); }
        }

        /// <summary>
        /// Line 2 Bales Line Two on Scale conveyor ////////////////////// 
        /// </summary>
        public BaleViewModel baleOneLineTwo;
        public UserControl BaleOneLineTwo
        {
            get { return new BaleView(baleOneLineTwo); }
        }
        public BaleViewModel baleTwoLineTwo;
        public UserControl BaleTwoLineTwo
        {
            get { return new BaleView(baleTwoLineTwo); }
        }
        public BaleViewModel baleThreeLineTwo;
        public UserControl BaleThreeLineTwo
        {
            get { return new BaleView(baleThreeLineTwo); }
        }
        public BaleViewModel baleFourLineTwo;
        public UserControl BaleFourLineTwo
        {
            get { return new BaleView(baleFourLineTwo); }
        }
        public BaleViewModel baleFiveLineTwo;
        public UserControl BaleFiveLineTwo
        {
            get { return new BaleView(baleFiveLineTwo); }
        }
        public BaleViewModel baleSixLineTwo;
        public UserControl BaleSixLineTwo
        {
            get { return new BaleView(baleSixLineTwo); }
        }
        public BaleViewModel baleSevenLineTwo;
        public UserControl BaleSevenLineTwo
        {
            get { return new BaleView(baleSevenLineTwo); }
        }
        public BaleViewModel baleEightLineTwo;
        public UserControl BaleEightLineTwo
        {
            get { return new BaleView(baleEightLineTwo); }
        }
        public BaleViewModel baleNineLineTwo;
        public UserControl BaleNineLineTwo
        {
            get { return new BaleView(baleNineLineTwo); }
        }
        public BaleViewModel baleTenLineTwo;
        public UserControl BaleTenLineTwo
        {
            get { return new BaleView(baleTenLineTwo); }
        }
        public BaleViewModel baleElevenLineTwo;
        public UserControl BaleElevenLineTwo
        {
            get { return new BaleView(baleElevenLineTwo); }
        }
        public BaleViewModel baleTwelveLineTwo;
        public UserControl BaleTwelveLineTwo
        {
            get { return new BaleView(baleTwelveLineTwo); }
        }
        public BaleViewModel baleThirteenLineTwo;
        public UserControl BaleThirteenLineTwo
        {
            get { return new BaleView(baleThirteenLineTwo); }
        }
        public BaleViewModel baleFourteenLineTwo;
        public UserControl BaleFourteenLineTwo
        {
            get { return new BaleView(baleFourteenLineTwo); }
        }
        public BaleViewModel baleFifteenLineTwo;
        public UserControl BaleFifteenLineTwo
        {
            get { return new BaleView(baleFifteenLineTwo); }
        }

        // Default Scale Line One ----------------------------------------------------
        public BaleViewModel DefaultScaleLineOneBaleOne;
        public UserControl BaleDefaultOneScaleLnOne
        {
            get { return new BaleView(DefaultScaleLineOneBaleOne); }
        }
        public BaleViewModel DefaultScaleLineOneBaleTwo;
        public UserControl BaleDefaultTwoScaleLnOne
        {
            get { return new BaleView(DefaultScaleLineOneBaleTwo); }
        }
        public BaleViewModel DefaultScaleLineOneBaleThree;
        public UserControl BaleDefaultThreeScaleLnOne
        {
            get { return new BaleView(DefaultScaleLineOneBaleThree); }
        }
        public BaleViewModel DefaultScaleLineOneBaleFour;
        public UserControl BaleDefaultFourScaleLnOne
        {
            get { return new BaleView(DefaultScaleLineOneBaleFour); }
        }
        public BaleViewModel DefaultFiveScaleLnFive;
        public UserControl BaleDefaultFiveScaleLnOne
        {
            get { return new BaleView(DefaultFiveScaleLnFive); }
        }

        //-------------------------------------------------------------------------------
        // Bale on Press Conveyor line One
        //
        public BaleViewModel PressLineOneBaleOne;
        public UserControl BaleNumberOneLnOnePress
        {
            get { return new BaleView(PressLineOneBaleOne); }
        }
        public BaleViewModel PressLineOneBaleTwo;
        public UserControl BaleNumberTwoLnOnePress
        {
            get { return new BaleView(PressLineOneBaleTwo); }
        }
        public BaleViewModel PressLineOneBaleThree;
        public UserControl BaleNumberThreeLnOnePress
        {
            get { return new BaleView(PressLineOneBaleThree); }
        }
        public BaleViewModel PressLineOneBaleFour;
        public UserControl BaleNumberFourLnOnePress
        {
            get { return new BaleView(PressLineOneBaleFour); }
        }
        public BaleViewModel PressLineOneBaleFive;
        public UserControl BaleNumberFiveLnOnePress
        {
            get { return new BaleView(PressLineOneBaleFive); }
        }
        public BaleViewModel PressLineOneBaleSix;
        public UserControl BaleNumberSixLnOnePress
        {
            get { return new BaleView(PressLineOneBaleSix); }
        }
        public BaleViewModel PressLineOneBaleSeven;
        public UserControl BaleNumberSevenLnOnePress
        {
            get { return new BaleView(PressLineOneBaleSeven); }
        }
        public BaleViewModel PressLineOneBaleEight;
        public UserControl BaleNumberEightLnOnePress
        {
            get { return new BaleView(PressLineOneBaleEight); }
        }

        //--------------------------------------------------------------------
        // Default Bale on press conveyor Line One
        //
        public BaleViewModel DefaultOnePressLnOne;
        public UserControl BaleDefaultOnePressLnOne
        {
            get { return new BaleView(DefaultOnePressLnOne); }
        }
        public BaleViewModel DefaultTwoPressLnOne;
        public UserControl BaleDefaultTwoPressLnOne
        {
            get { return new BaleView(DefaultTwoPressLnOne); }
        }
        public BaleViewModel DefaultThreePressLnOne;
        public UserControl BaleDefaultThreePressLnOne
        {
            get { return new BaleView(DefaultThreePressLnOne); }
        }
        public BaleViewModel DefaultFourPressLnOne;
        public UserControl BaleDefaultFourPressLnOne
        {
            get { return new BaleView(DefaultFourPressLnOne); }
        }
        public BaleViewModel DefaultFivePressLnOne;
        public UserControl BaleDefaultFivePressLnOne
        {
            get { return new BaleView(DefaultFivePressLnOne); }
        }

        //-----------------------------------------------------------------------------
        // Bales on Marker Line one.
        //
        public BaleViewModel MarkerLineOneBaleOne;
        public UserControl BaleNumberOneLnOneMrk
        {
            get { return new BaleView(MarkerLineOneBaleOne); }
        }
        public BaleViewModel MarkerLineOneBaleTwo;
        public UserControl BaleNumberTwoLineOneMrk
        {
            get { return new BaleView(MarkerLineOneBaleTwo); }
        }
        public BaleViewModel MarkerLineOneBaleThree;
        public UserControl BaleNumberThreeLnOneMrk
        {
            get { return new BaleView(MarkerLineOneBaleThree); }
        }
        public BaleViewModel MarkerLineOneBaleFour;
        public UserControl BaleNumberFourLnOneMrk
        {
            get { return new BaleView(MarkerLineOneBaleFour); }
        }
        public BaleViewModel MarkerLineOneBaleFive;
        public UserControl BaleNumberFiveLnOneMrk
        {
            get { return new BaleView(MarkerLineOneBaleFive); }
        }
        public BaleViewModel MarkerLineOneBaleSix;
        public UserControl BaleNumberSixLnOneMrk
        {
            get { return new BaleView(MarkerLineOneBaleSix); }
        }
        public BaleViewModel MarkerLineOneBaleSeven;
        public UserControl BaleNumberSevenLnOneMrk
        {
            get { return new BaleView(MarkerLineOneBaleSeven); }
        }
        public BaleViewModel MarkerLineOneBaleEight;
        public UserControl BaleNumberEightLnOneMrk
        {
            get { return new BaleView(MarkerLineOneBaleEight); }
        }
        //--------------------------------------------------------------------
        // Default Bale on Marker conveyor Line One
        //
        public BaleViewModel BaleDefaultOneMrkLnOne;
        public UserControl BaleDefaultOneMarkerLnOne
        {
            get { return new BaleView(BaleDefaultOneMrkLnOne); }
        }
        public BaleViewModel BaleDefaultTwoMrkLnOne;
        public UserControl BaleDefaultTwoMarkerLnOne
        {
            get { return new BaleView(BaleDefaultTwoMrkLnOne); }
        }
        public BaleViewModel BaleDefaultThreeMrkLnOne;
        public UserControl BaleDefaultThreeMarkerLnOne
        {
            get { return new BaleView(BaleDefaultThreeMrkLnOne); }
        }
        public BaleViewModel BaleDefaultFourMrkLnOne;
        public UserControl BaleDefaultFourMarkerLnOne
        {
            get { return new BaleView(BaleDefaultFourMrkLnOne); }
        }
        public BaleViewModel BaleDefaultFiveMrkLnOne;
        public UserControl BaleDefaultFiveMarkerLnOne
        {
            get { return new BaleView(BaleDefaultFiveMrkLnOne); }
        }
        //-------------------------------------------------------------------------------

        /// <summary>
        ///  Line 2 Default Bales on Scale conveyor ////////////////
        /// </summary>
        public BaleViewModel DefaultOneScaleLnTwo;
        public UserControl BaleDefaultOneScaleLnTwo
        {
            get { return new BaleView(DefaultOneScaleLnTwo); }
        }
        public BaleViewModel DefaultTwoScaleLnTwo;
        public UserControl BaleDefaultTwoScaleLnTwo
        {
            get { return new BaleView(DefaultTwoScaleLnTwo); }
        }
        public BaleViewModel DefaultThreeScaleLnTwo;
        public UserControl BaleDefaultThreeScaleLnTwo
        {
            get { return new BaleView(DefaultThreeScaleLnTwo); }
        }
        public BaleViewModel DefaultFourScaleLnTwo;
        public UserControl BaleDefaultFourScaleLnTwo
        {
            get { return new BaleView(DefaultFourScaleLnTwo); }
        }
        public BaleViewModel DefaultFiveScaleLnTwo;
        public UserControl BaleDefaultFiveScaleLnTwo
        {
            get { return new BaleView(DefaultFiveScaleLnFive); }
        }

        /// <summary>
        /// Line 2 on Press Conveyor ///////////////////////////////
        /// </summary>

        public BaleViewModel PressLineTwoBaleOne;
        public UserControl BaleOneLineTwoPress
        {
            get { return new BaleView(PressLineTwoBaleOne); }
        }
        public BaleViewModel PressLineTwoBaleTwo;
        public UserControl BaleTwoLineTwoPress
        {
            get { return new BaleView(PressLineTwoBaleTwo); }
        }
        public BaleViewModel PressLineTwoBaleThree;
        public UserControl BaleEightLineTwoPress
        {
            get { return new BaleView(PressLineTwoBaleThree); }
        }
        public BaleViewModel PressLineTwoBaleFour;
        public UserControl BaleThreeLineTwoPress
        {
            get { return new BaleView(PressLineTwoBaleFour); }
        }
        public BaleViewModel baleFourLineTwoOnFive;
        public UserControl BaleFourLineTwoPress
        {
            get { return new BaleView(baleFourLineTwoOnFive); }
        }
        public BaleViewModel PressLineTwoBaleSix;
        public UserControl BaleFiveLineTwoPress
        {
            get { return new BaleView(PressLineTwoBaleSix); }
        }
        public BaleViewModel PressLineTwoBaleSeven;
        public UserControl BaleSixLineTwoPress
        {
            get { return new BaleView(PressLineTwoBaleSeven); }
        }
        public BaleViewModel PressLineTwoBaleEight;
        public UserControl BaleSevenLineTwoPress
        {
            get { return new BaleView(PressLineTwoBaleEight); }
        }

        /// <summary>
        ///Default Bale on press conveyor Line Two ///////////////////// 
        /// </summary>
        public BaleViewModel DefaultOnePressLnTwo;
        public UserControl BaleDefaultOnePressLnTwo
        {
            get { return new BaleView(DefaultOnePressLnTwo); }
        }
        public BaleViewModel DefaultTwoPressLnTwo;
        public UserControl BaleDefaultTwoPressLnTwo
        {
            get { return new BaleView(DefaultTwoPressLnTwo); }
        }
        public BaleViewModel DefaultThreePressLnTwo;
        public UserControl BaleDefaultThreePressLnTwo
        {
            get { return new BaleView(DefaultThreePressLnTwo); }
        }
        public BaleViewModel DefaultFourPressLnTwo;
        public UserControl BaleDefaultFourPressLnTwo
        {
            get { return new BaleView(DefaultFourPressLnTwo); }
        }
        public BaleViewModel DefaultFivePressLnTwo;
        public UserControl BaleDefaultFivePressLnTwo
        {
            get { return new BaleView(DefaultFivePressLnTwo); }
        }

        //--------------------------------------------------------------------
        // Line 2 on Marker Conveyor
        //
        public BaleViewModel MarkerLineTwoBaleOne;
        public UserControl BaleOneLineTwoMrk
        {
            get { return new BaleView(MarkerLineTwoBaleOne); }
        }
        public BaleViewModel MarkerLineTwoBaleTwo;
        public UserControl BaleTwoLineTwoMrk
        {
            get { return new BaleView(MarkerLineTwoBaleTwo); }
        }
        public BaleViewModel MarkerLineTwoBaleThree;
        public UserControl BaleThreeLineTwoMrk
        {
            get { return new BaleView(MarkerLineTwoBaleThree); }
        }
        public BaleViewModel MarkerLineTwoBaleFour;
        public UserControl BaleFourLineTwoMrk
        {
            get { return new BaleView(MarkerLineTwoBaleFour); }
        }
        public BaleViewModel MarkerLineTwoBaleFive;
        public UserControl BaleFiveLineTwoMrk
        {
            get { return new BaleView(MarkerLineTwoBaleFive); }
        }
        public BaleViewModel MarkerLineTwoBaleSix;
        public UserControl BaleSixLineTwoMrk
        {
            get { return new BaleView(MarkerLineTwoBaleSix); }
        }
        public BaleViewModel MarkerLineTwoBaleSeven;
        public UserControl BaleSevenLineTwoMrk
        {
            get { return new BaleView(MarkerLineTwoBaleSeven); }
        }
        public BaleViewModel MarkerLineTwoBaleEight;
        public UserControl BaleEightLineTwoMrk
        {
            get { return new BaleView(MarkerLineTwoBaleEight); }
        }

        //--------------------------------------------------------------------
        // Default Bale on Marker conveyor Line Two
        //
        public BaleViewModel BaleDefaultOneMrkLnTwo;
        public UserControl BaleDefaultOneMarkerLnTwo
        {
            get { return new BaleView(BaleDefaultOneMrkLnTwo); }
        }
        public BaleViewModel BaleDefaultTwoMrkLnTwo;
        public UserControl BaleDefaultTwoMarkerLnTwo
        {
            get { return new BaleView(BaleDefaultTwoMrkLnTwo); }
        }
        public BaleViewModel BaleDefaultThreeMrkLnTwo;
        public UserControl BaleDefaultThreeMarkerLnTwo
        {
            get { return new BaleView(BaleDefaultThreeMrkLnTwo); }
        }
        public BaleViewModel BaleDefaultFourMrkLnTwo;
        public UserControl BaleDefaultFourMarkerLnTwo
        {
            get { return new BaleView(BaleDefaultFourMrkLnTwo); }
        }
        public BaleViewModel BaleDefaultFiveMrkLnTwo;
        public UserControl BaleDefaultFiveMarkerLnTwo
        {
            get { return new BaleView(BaleDefaultFiveMrkLnOne); }
        }

        private int _baleConveyorApproachLnOne;
        public int BaleConveyorApproachLnOne
        {
            get { return _baleConveyorApproachLnOne; }
            set { SetProperty(ref _baleConveyorApproachLnOne, value); }
        }

        private int _baleConveyorPressLnOne;
        public int BaleConveyorPressLnOne
        {
            get { return _baleConveyorPressLnOne; }
            set { SetProperty(ref _baleConveyorPressLnOne, value); }
        }

        private int _baleConveyorMarkerLnOne;
        public int BaleConveyorMarkerLnOne
        {
            get { return _baleConveyorMarkerLnOne; }
            set { SetProperty(ref _baleConveyorMarkerLnOne, value); }
        }


        //Line 2
        private int _baleConveyorScaleLineTwo;
        public int BaleConveyorApproachLineTwo
        {
            get { return _baleConveyorScaleLineTwo; }
            set { SetProperty(ref _baleConveyorScaleLineTwo, value); }
        }

        private int _baleConveyorPressLineTwo;
        public int BaleConveyorPressLineTwo
        {
            get { return _baleConveyorPressLineTwo; }
            set { SetProperty(ref _baleConveyorPressLineTwo, value); }
        }

        private int _baleConveyorMerkerLineTwo;
        public int BaleConveyorMarkerLineTwo
        {
            get { return _baleConveyorMerkerLineTwo; }
            set { SetProperty(ref _baleConveyorMerkerLineTwo, value); }
        }

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

        private string _dropInConveyorOne;
        public string DropInConveyorOne
        {
            get => _dropInConveyorOne;
            set => SetProperty(ref _dropInConveyorOne, value);
        }

        private string _dropInConveyorTwo;
        public string DropInConveyorTwo
        {
            get => _dropInConveyorTwo;
            set => SetProperty(ref _dropInConveyorTwo, value);
        }

        public SheetCounterViewModel SheetCounterWindow;
        public UserControl SheetCounter
        {
            get => new SheetCounterView(SheetCounterWindow);
        }

        private Visibility _showSheetCount;
        public Visibility ShowSheetCount
        {
            get => _showSheetCount;
            set => SetProperty(ref _showSheetCount, value);
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


        private DelegateCommand _maxScaleConvCmd;
        public DelegateCommand MaxScaleConvCmd =>
       _maxScaleConvCmd ?? (_maxScaleConvCmd =
            new DelegateCommand(MaxScaleConvCmdExecute));
        private void MaxScaleConvCmdExecute()
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

        /// <summary>
        /// Press Line One //////////////////////////////////////////////////////////////////////
        /// </summary>
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

        private DelegateCommand _clearPressConvCmd;
        public DelegateCommand ClearPressConvCmd =>
       _clearPressConvCmd ?? (_clearPressConvCmd =
            new DelegateCommand(ClearPressConvCmdExecute));
        private void ClearPressConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"ClrPressConv;{ClassCommon.PressConveyorLineOne};");
        }

        private DelegateCommand _maxPressConvCmd;
        public DelegateCommand MaxPressConvCmd =>
       _maxPressConvCmd ?? (_maxPressConvCmd =
            new DelegateCommand(MaxPressConvCmdExecute));
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



        /// <summary>
        /// Marker Line One ///////////////////////////////////////////////////////////////////////////
        /// </summary>

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
        
        private DelegateCommand _clearMkrConvCmd;
        public DelegateCommand ClearMkrConvCmd =>
       _clearMkrConvCmd ?? (_clearMkrConvCmd =
            new DelegateCommand(ClearMkrConvCmdExecute));
        private void ClearMkrConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"ClrMkrConv;{ClassCommon.MarkerConveyorLineOne};");
        }

        private DelegateCommand _maxMkrConvLineOneCmd;
        public DelegateCommand MaxMkrConvLineOneCmd =>
       _maxMkrConvLineOneCmd ?? (_maxMkrConvLineOneCmd =
            new DelegateCommand(MaxMkrConvLineOneCmdExecute));
        private void MaxMkrConvLineOneCmdExecute()
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

        

        //SCALE Line 2 ///////////////////////////////////////////////////////////////////////////
        private DelegateCommand _incScaleLineTwoConvCmd;
        public DelegateCommand IncScaleLineTwoConvCmd =>
       _incScaleLineTwoConvCmd ?? (_incScaleLineTwoConvCmd =
            new DelegateCommand(IncScaleLineTwoConvCmdExecute));
        private void IncScaleLineTwoConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"IncScale;{ClassCommon.WeighConveyorLineTwo};");
        }
        private DelegateCommand _decScaleLineTwoConvCmd;
        public DelegateCommand DecScaleLineTwoConvCmd =>
       _decScaleLineTwoConvCmd ?? (_decScaleLineTwoConvCmd =
            new DelegateCommand(DecScaleLineTwoConvCmdExecute));
        private void DecScaleLineTwoConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"DecScale;{ClassCommon.WeighConveyorLineTwo};");
        }
        private DelegateCommand _clearScaleLineTwoConvCmd;
        public DelegateCommand ClearScaleLineTwoConvCmd =>
       _clearScaleLineTwoConvCmd ?? (_clearScaleLineTwoConvCmd =
            new DelegateCommand(ClearScaleLineTwoConvCmdExecute));
        private void ClearScaleLineTwoConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"ClrScaleConv;{ClassCommon.WeighConveyorLineTwo};");
        }
        private DelegateCommand _maxScaleConvLineTwoCmd;
        public DelegateCommand MaxScaleLineTwoConvCmd =>
       _maxScaleConvLineTwoCmd ?? (_maxScaleConvLineTwoCmd =
            new DelegateCommand(MaxScaleConvLineTwoCmdExecute));
        private void MaxScaleConvLineTwoCmdExecute()
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
        //Press Conveyor ////////////////////////////////////////////////////////////////////////////r
        private DelegateCommand _clearPressLineTwoConvCmd;
        public DelegateCommand ClearPressLineTwoConvCmd =>
       _clearPressLineTwoConvCmd ?? (_clearPressLineTwoConvCmd =
            new DelegateCommand(ClearPressLineTwoConvCmdExecute));
        private void ClearPressLineTwoConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"ClrPressConv;{ClassCommon.PressConveyorLineTwo};");
        }

        private DelegateCommand _incPressLineTwoConvCmd;
        public DelegateCommand IncPressLineTwoConvCmd =>
       _incPressLineTwoConvCmd ?? (_incPressLineTwoConvCmd =
            new DelegateCommand(IncPressLineTwoConvCmdExecute));
        private void IncPressLineTwoConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"IncPress;{ClassCommon.PressConveyorLineTwo};");
        }
        private DelegateCommand _decPressLineTwoConvCmd;
        public DelegateCommand DecPressLineTwoConvCmd =>
       _decPressLineTwoConvCmd ?? (_decPressLineTwoConvCmd =
            new DelegateCommand(DecPressLineTwoConvCmdExecute));
        private void DecPressLineTwoConvCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"DecPress;{ClassCommon.PressConveyorLineTwo};");
        }
        private DelegateCommand _maxPressConvLineTwoCmd;
        public DelegateCommand MaxPressLineTwoConvCmd =>
       _maxPressConvLineTwoCmd ?? (_maxPressConvLineTwoCmd =
            new DelegateCommand(MaxPressConvLineTwoCmdExecute));
        private void MaxPressConvLineTwoCmdExecute()
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
        //Marker Conveyor Line 2 ////////////////////////////////////////////////////////////////
        private DelegateCommand _incMkrConvLineTwoCmd;
        public DelegateCommand IncMkrConvLineTwoCmd =>
       _incMkrConvLineTwoCmd ?? (_incMkrConvLineTwoCmd =
            new DelegateCommand(IncMkrConvLineTwoCmdExecute));
        private void IncMkrConvLineTwoCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"IncMarker;{ClassCommon.MarkerConveyorLineTwo};");
        }

        private DelegateCommand _decMkrConvLineTwoCmd;
        public DelegateCommand DecMkrConvLineTwoCmd =>
       _decMkrConvLineTwoCmd ?? (_decMkrConvLineTwoCmd =
            new DelegateCommand(DecMkrConvLineTwoCmdExecute));
        private void DecMkrConvLineTwoCmdExecute()
        {
            ClsPipeMessage.SendPipeMessage($"DecMarker;{ClassCommon.MarkerConveyorLineTwo};");
        }

        private DelegateCommand _clearMkrConvCmdL2;
        public DelegateCommand ClearMkrConvLineTwoCmd =>
       _clearMkrConvCmdL2 ?? (_clearMkrConvCmdL2 =
            new DelegateCommand(ClearMkrConvCmdExecuteL2));
        private void ClearMkrConvCmdExecuteL2()
        {
            ClsPipeMessage.SendPipeMessage($"ClrMkrConv;{ClassCommon.MarkerConveyorLineTwo};");
        }

        private DelegateCommand _maxMkrConvLineTwoCmd;
        public DelegateCommand MaxMkrConvLineTwoCmd =>
       _maxMkrConvLineTwoCmd ?? (_maxMkrConvLineTwoCmd =
            new DelegateCommand(MaxMkrConvLineTwoCmdExecute));
        private void MaxMkrConvLineTwoCmdExecute()
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
                ZIndexForteen = bUpDn - 13;
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
                ZIndexForteen = bUpDn - 1;
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

    }
}
