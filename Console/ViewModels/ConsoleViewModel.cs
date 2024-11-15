using _8100UI.Services;
using BigCmbDisplayBox.ViewModels;
using MenuBarOne.ViewModels;
using MidCmbDisplayBox.ViewModels;
using Prism.Events;
using ProductSelection.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Console.ViewModels
{
    public class ConsoleViewModel : ConsoleViewModelBase
    {
        protected new readonly IEventAggregator _eventAggregator;



        private Visibility _stockLnOneVis = Visibility.Hidden;
        public Visibility StockLnOneVis
        {
            get => _stockLnOneVis; 
            set => SetProperty(ref _stockLnOneVis, value);
        }

        
        private Visibility _gradeLnOneVis = Visibility.Hidden;
        public Visibility GradeLnOneVis
        {
            get => _gradeLnOneVis; 
            set => SetProperty(ref _gradeLnOneVis, value);
        }
        private Visibility nxtGradeLnOneVis = Visibility.Hidden;
        public Visibility NxtGradeLnOneVis
        {
            get => nxtGradeLnOneVis; 
            set => SetProperty(ref nxtGradeLnOneVis, value);
        }

        private Visibility _nxtStockLnOneVis = Visibility.Hidden;
        public Visibility NxtStockLnOneVis
        {
            get => _nxtStockLnOneVis; 
            set => SetProperty(ref _nxtStockLnOneVis, value);
        }

      
        //MidBox Visib
        private Visibility _showMidBoxOne;
        public Visibility ShowMidBoxOne
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

       

        public ConsoleViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            UserLogin = ClassCommon.UserLogin;

          

            _eventAggregator.GetEvent<UserLogin>().Subscribe(UserLogonEvent);

            _eventAggregator.GetEvent<UpdateBaleDataEvent>().Subscribe(BaleDataReadyEvent);

            ////////////////////////////////////////////////////////////////////
            // Usercontrols for product and Grade ///////////////////

            if (ClassCommon.StockActive)
            {
                StockList = new ProductViewModel(_eventAggregator, ClassCommon.CurStock);
                XStockList = new ProductViewModel(_eventAggregator, ClassCommon.NxtStock);

                StockLnOneVis = Visibility.Visible;
                NxtStockLnOneVis = Visibility.Visible;
            }

            if (ClassCommon.GradeActive)
            {
                GradesList = new ProductViewModel(_eventAggregator, ClassCommon.CurGrade);
                XGradesList = new ProductViewModel(_eventAggregator, ClassCommon.NxtGrade);

                GradeLnOneVis = Visibility.Visible;
                NxtGradeLnOneVis = Visibility.Visible;
            }
           
            
            ////////////////////////////////////////////////////////////////////
            ///
            ShowMidBoxOne = ((bool)ClassCommon.BoxOneCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxTwo = ((bool)ClassCommon.BoxTwoCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxThree = ((bool)ClassCommon.BoxThreeCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxFour = ((bool)ClassCommon.BoxFourCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxFive = ((bool)ClassCommon.BoxFiveCheck == true) ? Visibility.Visible : Visibility.Hidden;
            ShowMidBoxSix = ((bool)ClassCommon.BoxSixCheck == true) ? Visibility.Visible : Visibility.Hidden;

            //Big ComboBox
            BigCmbBxOne = new BigCmbBoxViewModel(1, _eventAggregator)
            {
                TextColor = Brushes.Black,
                BackGdColor = Brushes.White
            };
            BigCmbBxTwo = new BigCmbBoxViewModel(2, _eventAggregator)
            {
                TextColor = Brushes.Black,
                BackGdColor = Brushes.White
            };

            LoadMidDisplayBoxes();
        }

        private void BaleDataReadyEvent(int obj)
        {
           // Debug.Print($" ********************* BaleDataReadyEvent");
            _eventAggregator.GetEvent<UpdateMidBoxData>().Publish(1);
        }

        private void UserLogonEvent(bool obj)
        {
            UserLogin = obj;   
        }

       


        private void LoadMidDisplayBoxes()
        {
            ///Mid Size Boxes///////////////////////////////////////////////////
            ///
            if (MidDisplayBxOne != null) MidDisplayBxOne = null;
                MidDisplayBxOne = new MidCmbBoxViewModel(1, _eventAggregator);
           
            if (MidDisplayBxTwo != null) MidDisplayBxTwo = null;
            MidDisplayBxTwo = new MidCmbBoxViewModel(2, _eventAggregator);
            // MidDisplayBxTwo.TextColor = Brushes.Black;
            //MidDisplayBxTwo.TextbackGndColor = Brushes.LightGreen;

            if (MidDisplayBxThree != null) MidDisplayBxThree = null;
            MidDisplayBxThree = new MidCmbBoxViewModel(3, _eventAggregator);
            // MidDisplayBxThree.TextColor = Brushes.Black;
            // MidDisplayBxThree.TextbackGndColor = Brushes.LightGreen;

            if (MidDisplayBxFour != null) MidDisplayBxFour = null;
            MidDisplayBxFour = new MidCmbBoxViewModel(4, _eventAggregator);
            // MidDisplayBxFour.TextColor = Brushes.Black;

            if (MidDisplayBxFive != null) MidDisplayBxFive = null;
            MidDisplayBxFive = new MidCmbBoxViewModel(5, _eventAggregator);
            // MidDisplayBxFive.TextColor = Brushes.Black;

            if (MidDisplayBxSix != null) MidDisplayBxSix = null;
            MidDisplayBxSix = new MidCmbBoxViewModel(6, _eventAggregator);
            // MidDisplayBxSix.TextColor = Brushes.Black;
        }
    }
}
