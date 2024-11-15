using _8100UI.Services;
using BigCmbDisplayBox.ViewModels;
using JobParams.ViewModels;
using MenuBarOne.ViewModels;
using MidCmbDisplayBox.ViewModels;
using Prism.Events;
using ProductSelection.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;


namespace Console.ViewModels
{
    class ConsoleTwoLinesViewModel : ConsoleViewModelBase
    {
        protected new readonly IEventAggregator _eventAggregator;

        public ConsoleTwoLinesViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            UserLogin = ClassCommon.UserLogin;


            //Windows menu
            MenuOne = new MenuBarOneViewModel(_eventAggregator);

            _eventAggregator.GetEvent<UserLogin>().Subscribe(UserLogonEvent);

            //Big ComboBox line 1
            BigCmbBxOneLineOne = new BigCmbBoxViewModel(1, _eventAggregator)
            {
                TextColor = Brushes.Black,
                BackGdColor = Brushes.AliceBlue
            };

            BigCmbBxTwoLineOne = new BigCmbBoxViewModel(2, _eventAggregator)
            {
                TextColor = Brushes.Black,
                BackGdColor = Brushes.AliceBlue
            };

            //Big ComboBox line 2
            BigCmbBxOneLineTwo = new BigCmbBoxViewModel(3, _eventAggregator)
            {
                TextColor = Brushes.Black,
                BackGdColor = Brushes.AliceBlue
            };
            BigCmbBxTwoLineTwo = new BigCmbBoxViewModel(4, _eventAggregator)
            {
                TextColor = Brushes.Black,
                BackGdColor = Brushes.AliceBlue
            };

            LoadMidDisplayBoxLnOne();

            if (!ClassCommon.SingleLot)
            {
                LoadMidDisplayBoxLnTwo();
            }

            LoadProductGrade();

        }


        private void LoadProductGrade()
        {

            if (ClassCommon.StockActive)
            {
                StockSelectLineOne = new ProductViewModel(_eventAggregator, ClassCommon.CurStock);
                XStockSelectLineOne = new ProductViewModel(_eventAggregator, ClassCommon.NxtStock);

                StockLnOneVis = Visibility.Visible;
                NxtStockLnOneVis = Visibility.Visible;
            }

            if (ClassCommon.GradeActive)
            {
                GradesSelectLineOne = new ProductViewModel(_eventAggregator, ClassCommon.CurGrade);
                XGradeSelectLineOne = new ProductViewModel(_eventAggregator, ClassCommon.NxtGrade);

                GradeLnOneVis = Visibility.Visible;
                NxtGradeLnOneVis = Visibility.Visible;
            }

            if (ClassCommon.SingleLot == false)
            {
                StockSelectLineTwo = new ProductViewModel(_eventAggregator, 10);
                XStockSelectLineTwo = new ProductViewModel(_eventAggregator, 11);
                GradesSelectLineTwo = new ProductViewModel(_eventAggregator, 12);
                XGradeSelectLineTwo = new ProductViewModel(_eventAggregator, 13);

                GradeLnTwoVis = Visibility.Visible;
                NxtGradeLnTwoVis = Visibility.Visible;
                StockLnTwoVis = Visibility.Visible;
                NxtStockLnTwoVis = Visibility.Visible;
            }


            if (ClassCommon.JobParam)
            {
                jobViewModel = new JobParamsViewModel(_eventAggregator);
                ShowJobParamsUC = Visibility.Visible;
            }




        }



        private void LoadMidDisplayBoxLnOne()
        {

            ///Mid Size Boxes///////////////////////////////////////////////////
            ///
            if (MidDisplayBxOne != null) MidDisplayBxOne = null;
            MidDisplayBxOne = new MidCmbBoxViewModel(1, _eventAggregator);

            if (MidDisplayBxTwo != null) MidDisplayBxTwo = null;
            MidDisplayBxTwo = new MidCmbBoxViewModel(2, _eventAggregator);

            if (MidDisplayBxThree != null) MidDisplayBxThree = null;
            MidDisplayBxThree = new MidCmbBoxViewModel(3, _eventAggregator);

            if (MidDisplayBxFour != null) MidDisplayBxFour = null;
            MidDisplayBxFour = new MidCmbBoxViewModel(4, _eventAggregator);

            if (MidDisplayBxFive != null) MidDisplayBxFive = null;
            MidDisplayBxFive = new MidCmbBoxViewModel(5, _eventAggregator);

            if (MidDisplayBxSix != null) MidDisplayBxSix = null;
            MidDisplayBxSix = new MidCmbBoxViewModel(6, _eventAggregator);

            ////////////////////////////////////////////////////////////////////
            ///
            ShowMidBoxOne = ((bool)ClassCommon.BoxOneCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxTwo = ((bool)ClassCommon.BoxTwoCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxThree = ((bool)ClassCommon.BoxThreeCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxFour = ((bool)ClassCommon.BoxFourCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxFive = ((bool)ClassCommon.BoxFiveCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxSix = ((bool)ClassCommon.BoxSixCheck == true) ? Visibility.Visible : Visibility.Hidden;

            ShowMidBoxOneLineTwo = Visibility.Hidden;
            ShowMidBoxTwoLineTwo = Visibility.Hidden;
            ShowMidBoxThreeLineTwo = Visibility.Hidden;
            ShowMidBoxFourLineTwo = Visibility.Hidden;
            ShowMidBoxFiveLineTwo = Visibility.Hidden;
            ShowMidBoxSixLineTwo = Visibility.Hidden;

        }

        private void LoadMidDisplayBoxLnTwo()
        {

            ///Mid Size Boxes///////////////////////////////////////////////////
            ///
            if (MidDisplayBxOneLineTwo != null) MidDisplayBxOneLineTwo = null;
            MidDisplayBxOneLineTwo = new MidCmbBoxLIneTwoViewModel(1, _eventAggregator);
            MidDisplayBxOneLineTwo.TextColor = Brushes.Red;

            if (MidDisplayBxTwoLineTwo != null) MidDisplayBxTwoLineTwo = null;
            MidDisplayBxTwoLineTwo = new MidCmbBoxLIneTwoViewModel(2, _eventAggregator);
            MidDisplayBxOneLineTwo.TextColor = Brushes.Red;

            if (MidDisplayBxTwoLineTwo != null) MidDisplayBxTwoLineTwo = null;
            MidDisplayBxTwoLineTwo = new MidCmbBoxLIneTwoViewModel(2, _eventAggregator);
            // MidDisplayBxTwo.TextColor = Brushes.Black;
            //MidDisplayBxTwo.TextbackGndColor = Brushes.LightGreen;

            if (MidDisplayBxThreeLineTwo != null) MidDisplayBxThreeLineTwo = null;
            MidDisplayBxThreeLineTwo = new MidCmbBoxLIneTwoViewModel(3, _eventAggregator);
            // MidDisplayBxThree.TextColor = Brushes.Black;
            // MidDisplayBxThree.TextbackGndColor = Brushes.LightGreen;

            if (MidDisplayBxFourLineTwo != null) MidDisplayBxFourLineTwo = null;
            MidDisplayBxFourLineTwo = new MidCmbBoxLIneTwoViewModel(4, _eventAggregator);
            // MidDisplayBxFour.TextColor = Brushes.Black;

            if (MidDisplayBxFiveLineTwo != null) MidDisplayBxFiveLineTwo = null;
            MidDisplayBxFiveLineTwo = new MidCmbBoxLIneTwoViewModel(5, _eventAggregator);
            // MidDisplayBxFive.TextColor = Brushes.Black;

            if (MidDisplayBxSixLineTwo != null) MidDisplayBxSixLineTwo = null;
            MidDisplayBxSixLineTwo = new MidCmbBoxLIneTwoViewModel(6, _eventAggregator);
            // MidDisplayBxSix.TextColor = Brushes.Black;

            ShowMidBoxOneLineTwo = ((bool)ClassCommon.BoxOneCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxTwoLineTwo = ((bool)ClassCommon.BoxTwoCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxThreeLineTwo = ((bool)ClassCommon.BoxThreeCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxFourLineTwo = ((bool)ClassCommon.BoxFourCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxFiveLineTwo = ((bool)ClassCommon.BoxFiveCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxSixLineTwo = ((bool)ClassCommon.BoxSixCheck == true) ? Visibility.Visible : Visibility.Hidden;

        }



        private void UserLogonEvent(bool obj)
        {
            UserLogin = obj;
        }

        //Line One------------------------------------------------------------------
        private Visibility _showMidBoxOne;
        public System.Windows.Visibility ShowMidBoxOne
        {
            get => _showMidBoxOne;
            set => SetProperty(ref _showMidBoxOne, value);
        }
        private Visibility _showMidBoxTwo;
        public Visibility ShowMidBoxTwo
        {
            get => _showMidBoxTwo;
            set => SetProperty(ref _showMidBoxTwo, value);
        }
        private Visibility _showMidBoxThree;
        public Visibility ShowMidBoxThree
        {
            get => _showMidBoxThree;
            set => SetProperty(ref _showMidBoxThree, value);
        }

        private Visibility _showMidBoxFour;
        public Visibility ShowMidBoxFour
        {
            get => _showMidBoxFour;
            set => SetProperty(ref _showMidBoxFour, value);
        }

        private Visibility _showMidBoxFive;
        public Visibility ShowMidBoxFive
        {
            get => _showMidBoxFive;
            set => SetProperty(ref _showMidBoxFive, value);
        }
        private Visibility _showMidBoxSix;
        public Visibility ShowMidBoxSix
        {
            get => _showMidBoxSix;
            set => SetProperty(ref _showMidBoxSix, value);
        }
        //-----------------------------------------------------------------

        //Line Two------------------------------------------------------------------
        private Visibility _showMidBoxOneLineTwo;
        public System.Windows.Visibility ShowMidBoxOneLineTwo
        {
            get => _showMidBoxOneLineTwo;
            set => SetProperty(ref _showMidBoxOneLineTwo, value);
        }
        private Visibility _showMidBoxTwoLineTwo;
        public Visibility ShowMidBoxTwoLineTwo
        {
            get => _showMidBoxTwoLineTwo;
            set => SetProperty(ref _showMidBoxTwoLineTwo, value);
        }
        private Visibility _showMidBoxThreeLineTwo;
        public Visibility ShowMidBoxThreeLineTwo
        {
            get => _showMidBoxThreeLineTwo;
            set => SetProperty(ref _showMidBoxThreeLineTwo, value);
        }
        private Visibility _showMidBoxFourLineTwo;
        public Visibility ShowMidBoxFourLineTwo
        {
            get => _showMidBoxFourLineTwo;
            set => SetProperty(ref _showMidBoxFourLineTwo, value);
        }
        private Visibility _showMidBoxFiveLineTwo;
        public Visibility ShowMidBoxFiveLineTwo
        {
            get => _showMidBoxFiveLineTwo;
            set => SetProperty(ref _showMidBoxFiveLineTwo, value);
        }
        private Visibility _showMidBoxSixLineTwo;
        public Visibility ShowMidBoxSixLineTwo
        {
            get => _showMidBoxSixLineTwo;
            set => SetProperty(ref _showMidBoxSixLineTwo, value);
        }
        //-----------------------------------------------------------------


        private Visibility _stockLnOneVis = Visibility.Hidden;
        public Visibility StockLnOneVis
        {
            get { return _stockLnOneVis; }
            set => SetProperty(ref _stockLnOneVis, value);
        }
     
        private Visibility _nxtStockLnOneVis = Visibility.Hidden;
        public Visibility NxtStockLnOneVis
        {
            get { return _nxtStockLnOneVis; }
            set => SetProperty(ref _nxtStockLnOneVis, value);
        }

        private Visibility _gradeLnOneVis = Visibility.Hidden;
        public Visibility GradeLnOneVis
        {
            get { return _gradeLnOneVis; }
            set => SetProperty(ref _gradeLnOneVis, value);
        }

        private Visibility nxtGradeLnOneVis = Visibility.Hidden;
        public Visibility NxtGradeLnOneVis
        {
            get { return nxtGradeLnOneVis; }
            set => SetProperty(ref nxtGradeLnOneVis, value);
        }

        private Visibility _stockLnTwoVis = Visibility.Hidden;
        public Visibility StockLnTwoVis
        {
            get { return _stockLnTwoVis; }
            set => SetProperty(ref _stockLnTwoVis, value);
        }

        private Visibility _nxtStockLnTwoVis = Visibility.Hidden;
        public Visibility NxtStockLnTwoVis
        {
            get { return _nxtStockLnTwoVis; }
            set => SetProperty(ref _nxtStockLnTwoVis, value);
        }

        private Visibility _gradeLnTwoVis = Visibility.Hidden;
        public Visibility GradeLnTwoVis
        {
            get { return _gradeLnTwoVis; }
            set => SetProperty(ref _gradeLnTwoVis, value);
        }

        private Visibility _nxtGradeLnTwoVis = Visibility.Hidden;
        public Visibility NxtGradeLnTwoVis
        {
            get { return _nxtGradeLnTwoVis; }
            set => SetProperty(ref _nxtGradeLnTwoVis, value);
        }

        private Visibility _showJobParamsUC = Visibility.Hidden;
        public Visibility ShowJobParamsUC
        {
            get => _showJobParamsUC;
            set => SetProperty(ref _showJobParamsUC, value);
        }




        private void LoadMidDisplayBoxesLineOne()
        {

            if (ClassCommon.SingleLot)
            {

                if (MidDisplayBxOne != null) MidDisplayBxOne = null;
                MidDisplayBxOne = new MidCmbBoxLIneTwoViewModel(1, _eventAggregator);


                if (MidDisplayBxTwo != null) MidDisplayBxTwo = null;
                MidDisplayBxTwo = new MidCmbBoxLIneTwoViewModel(2, _eventAggregator);
                // MidDisplayBxTwo.TextColor = Brushes.Black;
                //MidDisplayBxTwo.TextbackGndColor = Brushes.LightGreen;

                if (MidDisplayBxThree != null) MidDisplayBxThree = null;
                MidDisplayBxThree = new MidCmbBoxLIneTwoViewModel(3, _eventAggregator);
                // MidDisplayBxThree.TextColor = Brushes.Black;
                // MidDisplayBxThree.TextbackGndColor = Brushes.LightGreen;

                if (MidDisplayBxFour != null) MidDisplayBxFour = null;
                MidDisplayBxFour = new MidCmbBoxLIneTwoViewModel(4, _eventAggregator);
                // MidDisplayBxFour.TextColor = Brushes.Black;

                if (MidDisplayBxFive != null) MidDisplayBxFive = null;
                MidDisplayBxFive = new MidCmbBoxLIneTwoViewModel(5, _eventAggregator);
                // MidDisplayBxFive.TextColor = Brushes.Black;

                if (MidDisplayBxSix != null) MidDisplayBxSix = null;
                MidDisplayBxSix = new MidCmbBoxLIneTwoViewModel(6, _eventAggregator);
                // MidDisplayBxSix.TextColor = Brushes.Black;

                ShowMidBoxOne = ((bool)ClassCommon.BoxOneCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxTwo = ((bool)ClassCommon.BoxTwoCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxThree = ((bool)ClassCommon.BoxThreeCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxFour = ((bool)ClassCommon.BoxFourCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxFive = ((bool)ClassCommon.BoxFiveCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxSix = ((bool)ClassCommon.BoxSixCheck == true) ? Visibility.Visible : Visibility.Hidden;

                ShowMidBoxOneLineTwo = Visibility.Hidden;
                ShowMidBoxTwoLineTwo = Visibility.Hidden;
                ShowMidBoxThreeLineTwo = Visibility.Hidden;
                ShowMidBoxFourLineTwo = Visibility.Hidden;
                ShowMidBoxFiveLineTwo = Visibility.Hidden;
                ShowMidBoxSixLineTwo = Visibility.Hidden;

            }
            else
            {
                if (MidDisplayBxOne != null) MidDisplayBxOne = null;
                MidDisplayBxOne = new MidCmbBoxLIneTwoViewModel(1, _eventAggregator);

                if (MidDisplayBxTwo != null) MidDisplayBxTwo = null;
                MidDisplayBxTwo = new MidCmbBoxLIneTwoViewModel(2, _eventAggregator);
                // MidDisplayBxTwo.TextColor = Brushes.Black;
                //MidDisplayBxTwo.TextbackGndColor = Brushes.LightGreen;

                if (MidDisplayBxThree != null) MidDisplayBxThree = null;
                MidDisplayBxThree = new MidCmbBoxLIneTwoViewModel(3, _eventAggregator);
                // MidDisplayBxThree.TextColor = Brushes.Black;
                // MidDisplayBxThree.TextbackGndColor = Brushes.LightGreen;

                if (MidDisplayBxFour != null) MidDisplayBxFour = null;
                MidDisplayBxFour = new MidCmbBoxLIneTwoViewModel(4, _eventAggregator);
                // MidDisplayBxFour.TextColor = Brushes.Black;

                if (MidDisplayBxFive != null) MidDisplayBxFive = null;
                MidDisplayBxFive = new MidCmbBoxLIneTwoViewModel(5, _eventAggregator);
                // MidDisplayBxFive.TextColor = Brushes.Black;

                if (MidDisplayBxSix != null) MidDisplayBxSix = null;
                MidDisplayBxSix = new MidCmbBoxLIneTwoViewModel(6, _eventAggregator);
                // MidDisplayBxSix.TextColor = Brushes.Black;

                //Line 2
                if (MidDisplayBxOneLineTwo != null) MidDisplayBxOneLineTwo = null;
                MidDisplayBxOneLineTwo = new MidCmbBoxLIneTwoViewModel(1, _eventAggregator);
                MidDisplayBxOneLineTwo.TextColor = Brushes.Red;

                if (MidDisplayBxTwoLineTwo != null) MidDisplayBxTwoLineTwo = null;
                MidDisplayBxTwoLineTwo = new MidCmbBoxLIneTwoViewModel(2, _eventAggregator);
                // MidDisplayBxTwo.TextColor = Brushes.Black;
                //MidDisplayBxTwo.TextbackGndColor = Brushes.LightGreen;

                if (MidDisplayBxThreeLineTwo != null) MidDisplayBxThreeLineTwo = null;
                MidDisplayBxThreeLineTwo = new MidCmbBoxLIneTwoViewModel(3, _eventAggregator);
                // MidDisplayBxThree.TextColor = Brushes.Black;
                // MidDisplayBxThree.TextbackGndColor = Brushes.LightGreen;

                if (MidDisplayBxFourLineTwo != null) MidDisplayBxFourLineTwo = null;
                MidDisplayBxFourLineTwo = new MidCmbBoxLIneTwoViewModel(4, _eventAggregator);
                // MidDisplayBxFour.TextColor = Brushes.Black;

                if (MidDisplayBxFiveLineTwo != null) MidDisplayBxFiveLineTwo = null;
                MidDisplayBxFiveLineTwo = new MidCmbBoxLIneTwoViewModel(5, _eventAggregator);
                // MidDisplayBxFive.TextColor = Brushes.Black;

                if (MidDisplayBxSixLineTwo != null) MidDisplayBxSixLineTwo = null;
                MidDisplayBxSixLineTwo = new MidCmbBoxLIneTwoViewModel(6, _eventAggregator);
                // MidDisplayBxSix.TextColor = Brushes.Black;

                ShowMidBoxOne = ((bool)ClassCommon.BoxOneCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxTwo = ((bool)ClassCommon.BoxTwoCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxThree = ((bool)ClassCommon.BoxThreeCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxFour = ((bool)ClassCommon.BoxFourCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxFive = ((bool)ClassCommon.BoxFiveCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxSix = ((bool)ClassCommon.BoxSixCheck == true) ? Visibility.Visible : Visibility.Hidden;

                ShowMidBoxOneLineTwo = ((bool)ClassCommon.BoxOneCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxTwoLineTwo = ((bool)ClassCommon.BoxTwoCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxThreeLineTwo = ((bool)ClassCommon.BoxThreeCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxFourLineTwo = ((bool)ClassCommon.BoxFourCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxFiveLineTwo = ((bool)ClassCommon.BoxFiveCheck == true) ? Visibility.Visible : Visibility.Hidden;
                ShowMidBoxSixLineTwo = ((bool)ClassCommon.BoxSixCheck == true) ? Visibility.Visible : Visibility.Hidden;
            }
        }

    }
}
