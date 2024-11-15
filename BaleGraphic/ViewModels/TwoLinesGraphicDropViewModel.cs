﻿using _8100UI.Services;
using BaleGraphic.Views;
using BaleStation.ViewModels;
using Prism.Events;
using PulpBale.ViewModels;
using SheetCounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BaleGraphic.ViewModels
{
    class TwoLinesGraphicDropViewModel : GraphicViewModelBase
    {
        protected new readonly IEventAggregator _eventAggregator;

        private string _txtStatus;
        public string TxtStatus
        {
            get { return _txtStatus; }
            set { SetProperty(ref _txtStatus, value); }
        }


        public TwoLinesGraphicDropViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;

            UserLogin = ClassCommon.UserLogin;

            if (ClassCommon.SheetChecked == true)
            {
                SheetCounterWindow = new SheetCounterViewModel(_eventAggregator);
                ShowSheetCount = Visibility.Visible;
            }
            else
                ShowSheetCount = Visibility.Hidden;

            if (ClassCommon.LayBoyCount == 2)
                TwoLayBoys = Visibility.Visible;

            if (ClassCommon.LayBoyCount == 1)
                OneLayBoy = Visibility.Visible;


            SetBaleOrdered();


            _eventAggregator.GetEvent<UpdateBaleMoveEvent>().Subscribe(UpdateBaleMoveEvents);
            _eventAggregator.GetEvent<UpdateMainTimerEvents>().Subscribe(MainTimerEvent);

            SetUpStations();
            SetUpBaleLineOne();
            SetUpBaleLineTwo();
        }

        private void SetUpStations()
        {
            StationOneLineOne = new BaleStationViewModel(_eventAggregator, 0);
            StationOneLineOne.UnitColor = Brushes.White;
            StationOneLineOne.ShowStateionButton(false, "");

            StationTwoLineOne = new BaleStationViewModel(_eventAggregator, 1)
            {
                UnitColor = Brushes.White
            };
            StationTwoLineOne.ShowStateionButton(false, "");

            StationThreeLineOne = new BaleStationViewModel(_eventAggregator, 2)
            {
                UnitColor = Brushes.White
            };
            StationThreeLineOne.ShowStateionButton(false, "");

            StationFourLineOne = new BaleStationViewModel(_eventAggregator, 3)
            {
                UnitColor = Brushes.White
            };
            StationFourLineOne.ShowStateionButton(false, "");

            StationFiveLineOne = new BaleStationViewModel(_eventAggregator, 4);
            StationFiveLineOne.UnitColor = Brushes.White;
            StationFiveLineOne.ShowStateionButton(false, "");


            //Line 2 -------------------------
            StationOneLineTwo = new BaleStationLineTwoViewModel(_eventAggregator, 0)
            {
                UnitColor = Brushes.White
            };
            StationOneLineTwo.ShowStateionButton(false, "");

            StationTwoLineTwo = new BaleStationLineTwoViewModel(_eventAggregator, 1)
            {
                UnitColor = Brushes.White
            };
            StationTwoLineTwo.ShowStateionButton(false, "");

            StationThreeLineTwo = new BaleStationLineTwoViewModel(_eventAggregator, 2)
            {
                UnitColor = Brushes.White
            };
            StationThreeLineTwo.ShowStateionButton(false, "");

            //StationFourLineTwo = new BaleStationLineTwoViewModel(_eventAggregator, ClassCommon.Press)
            StationFourLineTwo = new BaleStationLineTwoViewModel(_eventAggregator, 3)
            {
                UnitColor = Brushes.White
            };
            StationFourLineTwo.ShowStateionButton(false, "");

            //StationFiveLineTwo = new BaleStationLineTwoViewModel(_eventAggregator, ClassCommon.Marker);
            StationFiveLineTwo = new BaleStationLineTwoViewModel(_eventAggregator, 4);
            StationFiveLineTwo.UnitColor = Brushes.White;
            StationFiveLineTwo.ShowStateionButton(false, "");

        }
        private void SetUpBaleLineOne()
        {
            // Line 1-------------------------------------------------------------------------------
            // On Scale conveyor
            //
            baleOneLineOne = new BaleViewModel(_eventAggregator, 1);
            baleOneLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleTwoLineOne = new BaleViewModel(_eventAggregator, 2);
            baleTwoLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleThreeLineOne = new BaleViewModel(_eventAggregator, 3);
            baleThreeLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleFourLineOne = new BaleViewModel(_eventAggregator, 4);
            baleFourLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleFiveLineOne = new BaleViewModel(_eventAggregator, 5);
            baleFiveLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleSixLineOne = new BaleViewModel(_eventAggregator, 6);
            baleSixLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleSevenLineOne = new BaleViewModel(_eventAggregator, 7);
            baleSevenLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleEightLineOne = new BaleViewModel(_eventAggregator, 8);
            baleEightLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleNineLineOne = new BaleViewModel(_eventAggregator, 9);
            baleNineLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleTenLineOne = new BaleViewModel(_eventAggregator, 10);
            baleTenLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleElevenLineOne = new BaleViewModel(_eventAggregator, 11);
            baleElevenLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleTwelveLineOne = new BaleViewModel(_eventAggregator, 12);
            baleTwelveLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleThirteenLineOne = new BaleViewModel(_eventAggregator, 13);
            baleThirteenLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleFourteenLineOne = new BaleViewModel(_eventAggregator, 14);
            baleFourteenLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            baleFifteenLineOne = new BaleViewModel(_eventAggregator, 15);
            baleFifteenLineOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            //Default Bales on Scale conveyor
            DefaultScaleLineOneBaleOne = new BaleViewModel(_eventAggregator, 0);
            DefaultScaleLineOneBaleOne.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultScaleLineOneBaleTwo = new BaleViewModel(_eventAggregator, 0);
            DefaultScaleLineOneBaleTwo.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultScaleLineOneBaleThree = new BaleViewModel(_eventAggregator, 0);
            DefaultScaleLineOneBaleThree.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultScaleLineOneBaleFour = new BaleViewModel(_eventAggregator, 0);
            DefaultScaleLineOneBaleFour.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultFiveScaleLnFive = new BaleViewModel(_eventAggregator, 0);
            DefaultFiveScaleLnFive.SetBaleColor(Brushes.White, Brushes.Blue);

            // Line 1 on press Conveyor -----------------------------------------------------------
            //
            PressLineOneBaleOne = new BaleViewModel(_eventAggregator, 1);
            PressLineOneBaleOne.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            PressLineOneBaleTwo = new BaleViewModel(_eventAggregator, 2);
            PressLineOneBaleTwo.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            PressLineOneBaleThree = new BaleViewModel(_eventAggregator, 3);
            PressLineOneBaleThree.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            PressLineOneBaleFour = new BaleViewModel(_eventAggregator, 4);
            PressLineOneBaleFour.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            PressLineOneBaleFive = new BaleViewModel(_eventAggregator, 5);
            PressLineOneBaleFive.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            PressLineOneBaleSix = new BaleViewModel(_eventAggregator, 6);
            PressLineOneBaleSix.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            PressLineOneBaleSeven = new BaleViewModel(_eventAggregator, 7);
            PressLineOneBaleSeven.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            PressLineOneBaleEight = new BaleViewModel(_eventAggregator, 8);
            PressLineOneBaleEight.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            //Ddefault Bale on Press conveyor Line One
            DefaultOnePressLnOne = new BaleViewModel(_eventAggregator, 0);
            DefaultOnePressLnOne.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultTwoPressLnOne = new BaleViewModel(_eventAggregator, 0);
            DefaultTwoPressLnOne.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultThreePressLnOne = new BaleViewModel(_eventAggregator, 0);
            DefaultThreePressLnOne.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultFourPressLnOne = new BaleViewModel(_eventAggregator, 0);
            DefaultFourPressLnOne.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultFivePressLnOne = new BaleViewModel(_eventAggregator, 0);
            DefaultFivePressLnOne.SetBaleColor(Brushes.White, Brushes.Blue);

            ////////////////////////////////////////////////////////////////////////////////////
            // Marker
            //
            MarkerLineOneBaleOne = new BaleViewModel(_eventAggregator, 1);
            MarkerLineOneBaleOne.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            MarkerLineOneBaleTwo = new BaleViewModel(_eventAggregator, 2);
            MarkerLineOneBaleTwo.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            MarkerLineOneBaleThree = new BaleViewModel(_eventAggregator, 3);
            MarkerLineOneBaleThree.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            MarkerLineOneBaleFour = new BaleViewModel(_eventAggregator, 4);
            MarkerLineOneBaleFour.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            MarkerLineOneBaleFive = new BaleViewModel(_eventAggregator, 5);
            MarkerLineOneBaleFive.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            MarkerLineOneBaleSix = new BaleViewModel(_eventAggregator, 6);
            MarkerLineOneBaleSix.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            MarkerLineOneBaleSeven = new BaleViewModel(_eventAggregator, 7);
            MarkerLineOneBaleSeven.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            MarkerLineOneBaleEight = new BaleViewModel(_eventAggregator, 8);
            MarkerLineOneBaleEight.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            //Default Bales on Marker conveyor Line one------------------------------------------------------
            //
            BaleDefaultOneMrkLnOne = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultOneMrkLnOne.SetBaleColor(Brushes.White, Brushes.Black);

            BaleDefaultTwoMrkLnOne = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultTwoMrkLnOne.SetBaleColor(Brushes.White, Brushes.Black);

            BaleDefaultThreeMrkLnOne = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultThreeMrkLnOne.SetBaleColor(Brushes.White, Brushes.Black);

            BaleDefaultFourMrkLnOne = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultFourMrkLnOne.SetBaleColor(Brushes.White, Brushes.Black);

            BaleDefaultFiveMrkLnOne = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultFiveMrkLnOne.SetBaleColor(Brushes.White, Brushes.Black);

        }
        private void SetUpBaleLineTwo()
        {
            // Line 2-------------------------------------------------------------------------------
            // On Scale Conveyor
            //
            baleOneLineTwo = new BaleViewModel(_eventAggregator, 1);
            baleOneLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleTwoLineTwo = new BaleViewModel(_eventAggregator, 2);
            baleTwoLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleThreeLineTwo = new BaleViewModel(_eventAggregator, 3);
            baleThreeLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleFourLineTwo = new BaleViewModel(_eventAggregator, 4);
            baleFourLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleFiveLineTwo = new BaleViewModel(_eventAggregator, 5);
            baleFiveLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleSixLineTwo = new BaleViewModel(_eventAggregator, 6);
            baleSixLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleSevenLineTwo = new BaleViewModel(_eventAggregator, 7);
            baleSevenLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleEightLineTwo = new BaleViewModel(_eventAggregator, 8);
            baleEightLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleNineLineTwo = new BaleViewModel(_eventAggregator, 9);
            baleNineLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleTenLineTwo = new BaleViewModel(_eventAggregator, 10);
            baleTenLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleElevenLineTwo = new BaleViewModel(_eventAggregator, 11);
            baleElevenLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleTwelveLineTwo = new BaleViewModel(_eventAggregator, 12);
            baleTwelveLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleThirteenLineTwo = new BaleViewModel(_eventAggregator, 13);
            baleThirteenLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleFourteenLineTwo = new BaleViewModel(_eventAggregator, 14);
            baleFourteenLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleFifteenLineTwo = new BaleViewModel(_eventAggregator, 15);
            baleFifteenLineTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            //Default Bales on Scale conveyor Line 2
            //
            DefaultOneScaleLnTwo = new BaleViewModel(_eventAggregator, 0);
            DefaultOneScaleLnTwo.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultTwoScaleLnTwo = new BaleViewModel(_eventAggregator, 0);
            DefaultTwoScaleLnTwo.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultThreeScaleLnTwo = new BaleViewModel(_eventAggregator, 0);
            DefaultThreeScaleLnTwo.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultFourScaleLnTwo = new BaleViewModel(_eventAggregator, 0);
            DefaultFourScaleLnTwo.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultFiveScaleLnTwo = new BaleViewModel(_eventAggregator, 0);
            DefaultFiveScaleLnTwo.SetBaleColor(Brushes.White, Brushes.Blue);


            //Line 2 Bales on Press Conveyor//////////////////////////////////////////////
            //
            PressLineTwoBaleOne = new BaleViewModel(_eventAggregator, 1);
            PressLineTwoBaleOne.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            PressLineTwoBaleTwo = new BaleViewModel(_eventAggregator, 2);
            PressLineTwoBaleTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            PressLineTwoBaleFour = new BaleViewModel(_eventAggregator, 3);
            PressLineTwoBaleFour.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            baleFourLineTwoOnFive = new BaleViewModel(_eventAggregator, 4);
            baleFourLineTwoOnFive.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            PressLineTwoBaleSix = new BaleViewModel(_eventAggregator, 5);
            PressLineTwoBaleSix.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            PressLineTwoBaleSeven = new BaleViewModel(_eventAggregator, 6);
            PressLineTwoBaleSeven.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            PressLineTwoBaleEight = new BaleViewModel(_eventAggregator, 7);
            PressLineTwoBaleEight.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            PressLineTwoBaleThree = new BaleViewModel(_eventAggregator, 8);
            PressLineTwoBaleThree.SetBaleColor(Brushes.GreenYellow, Brushes.Black);


            //Ddefault Bale on Press conveyor Line Two
            DefaultOnePressLnTwo = new BaleViewModel(_eventAggregator, 0);
            DefaultOnePressLnTwo.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultTwoPressLnTwo = new BaleViewModel(_eventAggregator, 0);
            DefaultTwoPressLnTwo.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultThreePressLnTwo = new BaleViewModel(_eventAggregator, 0);
            DefaultThreePressLnTwo.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultFourPressLnTwo = new BaleViewModel(_eventAggregator, 0);
            DefaultFourPressLnTwo.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultFivePressLnTwo = new BaleViewModel(_eventAggregator, 0);
            DefaultFivePressLnTwo.SetBaleColor(Brushes.White, Brushes.Blue);


            //Line 2 Bales on Marker Conveyor////////////////////////////////////////////////
            //
            MarkerLineTwoBaleOne = new BaleViewModel(_eventAggregator, 1);
            MarkerLineTwoBaleOne.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            MarkerLineTwoBaleTwo = new BaleViewModel(_eventAggregator, 2);
            MarkerLineTwoBaleTwo.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            MarkerLineTwoBaleThree = new BaleViewModel(_eventAggregator, 3);
            MarkerLineTwoBaleThree.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            MarkerLineTwoBaleFour = new BaleViewModel(_eventAggregator, 4);
            MarkerLineTwoBaleFour.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            MarkerLineTwoBaleFive = new BaleViewModel(_eventAggregator, 5);
            MarkerLineTwoBaleFive.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            MarkerLineTwoBaleSix = new BaleViewModel(_eventAggregator, 6);
            MarkerLineTwoBaleSix.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            MarkerLineTwoBaleSeven = new BaleViewModel(_eventAggregator, 7);
            MarkerLineTwoBaleSeven.SetBaleColor(Brushes.GreenYellow, Brushes.Black);

            MarkerLineTwoBaleEight = new BaleViewModel(_eventAggregator, 8);
            MarkerLineTwoBaleEight.SetBaleColor(Brushes.GreenYellow, Brushes.Black);


            //Default Bales on Marker conveyor Line Two-----------------------------------
            //
            BaleDefaultOneMrkLnTwo = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultOneMrkLnTwo.SetBaleColor(Brushes.White, Brushes.Black);

            BaleDefaultTwoMrkLnTwo = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultTwoMrkLnTwo.SetBaleColor(Brushes.White, Brushes.Black);

            BaleDefaultThreeMrkLnTwo = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultThreeMrkLnTwo.SetBaleColor(Brushes.White, Brushes.Black);

            BaleDefaultFourMrkLnTwo = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultFourMrkLnTwo.SetBaleColor(Brushes.White, Brushes.Black);

            BaleDefaultFiveMrkLnTwo = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultFiveMrkLnTwo.SetBaleColor(Brushes.White, Brushes.Black);

            //-------------------------------------------------------------------------------
        }

        private void UpdateBaleMoveEvents(int obj)
        {
            try
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////
                if (obj == ClassCommon.WeighConveyorLineOne)
                {
                    if (ClassCommon.NewDattable != null)
                    {
                        // Application.Current.Dispatcher.Invoke(new Action(() =>
                        // { BaleGraphicView.BaleGraphicWindow.MoveBalePassScale(Convert.ToInt32(ClassCommon.NewDattable.Rows[ClassCommon.NewDatIndex]["Position"])); }));

                        /*
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                           MoveBaleLineA(ScaleLineA, Convert.ToInt32(ClassCommon.NewDattable.Rows[ClassCommon.NewDatIndex]["Position"]),
                                Convert.ToInt32(ClassCommon.NewDattable.Rows[ClassCommon.NewDatIndex]["Status"]));
                        }));
                       */
                        txtStatus = $" Bale {ClassCommon.NewDattable.Rows[ClassCommon.NewDatIndex]["Position"]} passed Scale";
                        _eventAggregator.GetEvent<Reporttextmessage>().Publish(txtStatus);
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////
                if (obj == ClassCommon.PressConveyorLineOne)
                {
                    if (ClassCommon.NewDattable != null)
                    {


                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            //  MoveBaleLineA(PressLineA, Convert.ToInt32(ClassCommon.NewDattable.Rows[ClassCommon.NewDatIndex]["Position"]),
                            //      Convert.ToInt32(ClassCommon.NewDattable.Rows[ClassCommon.NewDatIndex]["Status"]));
                        }));

                        txtStatus = $" Bale {ClassCommon.NewDattable.Rows[ClassCommon.NewDatIndex]["Position"]} passed Press";
                        _eventAggregator.GetEvent<Reporttextmessage>().Publish(txtStatus);

                    }
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////
                if (obj == ClassCommon.MarkerConveyorLineOne)
                {
                    if (ClassCommon.NewDattable != null)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            // MoveBaleLineA(MarkerLineA, Convert.ToInt32(ClassCommon.NewDattable.Rows[ClassCommon.NewDatIndex]["Position"]),
                            //    Convert.ToInt32(ClassCommon.NewDattable.Rows[ClassCommon.NewDatIndex]["Status"]));
                        }));

                        txtStatus = $" Bale {ClassCommon.NewDattable.Rows[ClassCommon.NewDatIndex]["Position"]} passed Marker";
                        _eventAggregator.GetEvent<Reporttextmessage>().Publish(txtStatus);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in UpdateBaleMoveEvents {ex.Message}");
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateBaleMoveEvents -> {ex.Message}");
            }
        }


        private void MainTimerEvent(DateTime obj)
        {
            _ = BaleTimerEventsAsync();

            if (ClassCommon.SheetChecked == true)
                _ = UpdateShowBaleEvents();

        }

        private async Task BaleTimerEventsAsync()
        {

            await Task.Run(() =>
            {

                //ClsSerilog.LogMessage(ClsSerilog.Debug, $"xxxxxx------ClassCommon.ConveyorTwoLnOne = {ClassCommon.ConveyorTwoLnOne}  ClassCommon.ApproachConveyorLineOne =  { ClassCommon.ApproachConveyorLineOne}");

                //For Line 1 ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //On Approach Conveyor before the scale
                List<int> BaleOnApproachConveyor = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.ApproachConveyorLineOne);
                BaleConveyorApproachLnOne = BaleOnApproachConveyor.Count();
                if (BaleConveyorApproachLnOne > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.ClearBalesOnScaleConveyorLineOne(); }));

                    //put Bale on scale conveyor in order from BaleOnApproachConveyor list
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.PutBaleOnScaleConveyorLineOne(BaleOnApproachConveyor); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.ClearBalesOnScaleConveyorLineOne(); }));

                //On Weigh Conveyor before the Press
                List<int> BaleOnWeighConveyor = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.WeighConveyorLineOne);
                BaleConveyorPressLnOne = BaleOnWeighConveyor.Count();
                if (BaleConveyorPressLnOne > 0) // After the Scale
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.ClearAllBalePressConveyorLineOne(); }));

                    //put Bale on Press conveyor
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.PutBaleOnPressConveyorLineOne(BaleOnWeighConveyor); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.ClearAllBalePressConveyorLineOne(); }));


                //On Press Conveyor before Marker
                List<int> BaleOnPressConveyor = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.PressConveyorLineOne);
                BaleConveyorMarkerLnOne = BaleOnPressConveyor.Count();
                if (BaleConveyorMarkerLnOne > 0) // After the Press
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.ClearAllBaleMrkConveyorLineOne(); }));

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.PutBaleOnMarkerConveyorLineOne(BaleOnPressConveyor); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.ClearAllBaleMrkConveyorLineOne(); }));


                //For Line 2 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //On Approach Conveyor before the scale
                List<int> BaleOnApproachConveyorLn2 = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.ApproachConveyorLineTwo);
                BaleConveyorApproachLineTwo = BaleOnApproachConveyorLn2.Count();
                if (BaleConveyorApproachLineTwo > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.ClearAllBalesOnScaleConveyorLineTwo(); }));

                    //put Bale on scale conveyor in order from BaleOnApproachConveyor list
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.PutBaleOnScaleConveyorLineTwo(BaleOnApproachConveyorLn2); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.ClearAllBalesOnScaleConveyorLineTwo(); }));

                //On Weigh Conveyor before the Press
                List<int> BaleOnWeighConveyorLn2 = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.WeighConveyorLineTwo);
                BaleConveyorPressLineTwo = BaleOnWeighConveyorLn2.Count();

                if (BaleConveyorPressLineTwo > 0) // After the Scale
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.ClearAllBalePressConveyorLineTwo(); }));

                    //put Bale on Press conveyor
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.PutBaleOnPressConveyorLineTwo(BaleOnWeighConveyorLn2); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.ClearAllBalePressConveyorLineTwo(); }));


                //On Press Conveyor before Marker
                List<int> BaleOnMarkerConveyorLineTwo = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.PressConveyorLineTwo);
                BaleConveyorMarkerLineTwo = BaleOnMarkerConveyorLineTwo.Count();
                if (BaleConveyorMarkerLineTwo > 0) // After the Press
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.ClearAllBalesMrkConveyorLineTwo(); }));

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.PutBaleOnMarkerConveyorLineTwo(BaleOnMarkerConveyorLineTwo); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { TwoLinesGraphicDropView.TwoLinesGraphicDropWindow.ClearAllBalesMrkConveyorLineTwo(); }));

                //LayBoy
                List<int> LbDropCount = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.LayBoy1ConveyorId);
                ShowDroponLayboyConveyor(LbDropCount.Count());

            });
        }


        private async Task UpdateShowBaleEvents()
        {
            try
            {
                await Task.Run(() =>
                {
                    //ShowDroponLayboyConveyor(_sqlHandler.GetBaleDatatable());
                    ShowDropOnshuttleConveyor(_sqlHandler.GetBaleDatatable());
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateShowBaleEvents -> { ex.Message}");
                MessageBox.Show($"Error in UpdateShowBaleEvents -> { ex.Message}");
            }
        }

        private void ShowDropOnshuttleConveyor(DataTable newData)
        {

            ShowDropOneLineOne = Visibility.Hidden;
            ShowDropTwoLineOne = Visibility.Hidden;
            ShowDropThreeLineOne = Visibility.Hidden;
            ShowDropFourLineOne = Visibility.Hidden;
            ShowDropFiveLineOne = Visibility.Hidden;
            //DropInConveyorOne = string.Empty;

            ShowDropOneLineTwo = Visibility.Hidden;
            ShowDropTwoLineTwo = Visibility.Hidden;
            ShowDropThreeLineTwo = Visibility.Hidden;
            ShowDropFourLineTwo = Visibility.Hidden;
            ShowDropFiveLineTwo = Visibility.Hidden;
            //DropInConveyorTwo = string.Empty;

            if (newData.Rows.Count > 0)
            {
                for (int i = 0; i < newData.Rows.Count; i++)
                {
                    if (Convert.ToInt32(newData.Rows[i]["ConveyorID"]) == 0)
                    {
                        int DropNumLineOne = Convert.ToInt32(newData.Rows[i]["NumbOnConv"]);
                        PutDropOnLineOne(DropNumLineOne);
                    }

                    if (Convert.ToInt32(newData.Rows[i]["ConveyorID"]) == 5)
                    {
                        int DropNumLineTwo = Convert.ToInt32(newData.Rows[i]["NumbOnConv"]);
                        PutDropOnLineTwo(DropNumLineTwo);
                    }
                }
            }
        }

        private void PutDropOnLineOne(int dropNum)
        {
            // ClsSerilog.LogMessage(ClsSerilog.Debug, $"PutDropOnLineOne -> { dropNum}");

            switch (dropNum)
            {
                case 1:
                    ShowDropOneLineOne = Visibility.Visible;
                    ShowDropTwoLineOne = Visibility.Hidden;
                    ShowDropThreeLineOne = Visibility.Hidden;
                    ShowDropFourLineOne = Visibility.Hidden;
                    ShowDropFiveLineOne = Visibility.Hidden;
                    break;
                case 2:
                    ShowDropOneLineOne = Visibility.Visible;
                    ShowDropTwoLineOne = Visibility.Visible;
                    ShowDropThreeLineOne = Visibility.Hidden;
                    ShowDropFourLineOne = Visibility.Hidden;
                    ShowDropFiveLineOne = Visibility.Hidden;
                    break;
                case 3:
                    ShowDropOneLineOne = Visibility.Visible;
                    ShowDropTwoLineOne = Visibility.Visible;
                    ShowDropThreeLineOne = Visibility.Visible;
                    ShowDropFourLineOne = Visibility.Hidden;
                    ShowDropFiveLineOne = Visibility.Hidden;
                    break;
                case 4:
                    ShowDropOneLineOne = Visibility.Visible;
                    ShowDropTwoLineOne = Visibility.Visible;
                    ShowDropThreeLineOne = Visibility.Visible;
                    ShowDropFourLineOne = Visibility.Visible;
                    ShowDropFiveLineOne = Visibility.Hidden;
                    break;
                case 5:
                    ShowDropOneLineOne = Visibility.Visible;
                    ShowDropTwoLineOne = Visibility.Visible;
                    ShowDropThreeLineOne = Visibility.Visible;
                    ShowDropFourLineOne = Visibility.Visible;
                    ShowDropFiveLineOne = Visibility.Visible;
                    break;
            }
        }

        private void PutDropOnLineTwo(int dropNum)
        {
            switch (dropNum)
            {
                case 1:
                    ShowDropOneLineTwo = Visibility.Visible;
                    ShowDropTwoLineTwo = Visibility.Hidden;
                    ShowDropThreeLineTwo = Visibility.Hidden;
                    ShowDropFourLineTwo = Visibility.Hidden;
                    ShowDropFiveLineTwo = Visibility.Hidden;
                    break;
                case 2:
                    ShowDropOneLineTwo = Visibility.Visible;
                    ShowDropTwoLineTwo = Visibility.Visible;
                    ShowDropThreeLineTwo = Visibility.Hidden;
                    ShowDropFourLineTwo = Visibility.Hidden;
                    ShowDropFiveLineTwo = Visibility.Hidden;
                    break;
                case 3:
                    ShowDropOneLineTwo = Visibility.Visible;
                    ShowDropTwoLineTwo = Visibility.Visible;
                    ShowDropThreeLineTwo = Visibility.Visible;
                    ShowDropFourLineTwo = Visibility.Hidden;
                    ShowDropFiveLineTwo = Visibility.Hidden;
                    break;
                case 4:
                    ShowDropOneLineTwo = Visibility.Visible;
                    ShowDropTwoLineTwo = Visibility.Visible;
                    ShowDropThreeLineTwo = Visibility.Visible;
                    ShowDropFourLineTwo = Visibility.Visible;
                    ShowDropFiveLineTwo = Visibility.Hidden;
                    break;
                case 5:
                    ShowDropOneLineTwo = Visibility.Visible;
                    ShowDropTwoLineTwo = Visibility.Visible;
                    ShowDropThreeLineTwo = Visibility.Visible;
                    ShowDropFourLineTwo = Visibility.Visible;
                    ShowDropFiveLineTwo = Visibility.Visible;
                    break;
            }
        }

        private void ShowDroponLayboyConveyor(int v)
        {
          
        }
    }
}