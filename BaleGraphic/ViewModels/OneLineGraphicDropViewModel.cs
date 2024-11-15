using _8100UI.Services;
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
    public class OneLineGraphicDropViewModel : GraphicViewModelBase
    {
        protected new readonly IEventAggregator _eventAggregator;

        public OneLineGraphicDropViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;

            UserLogin = ClassCommon.UserLogin;

            _eventAggregator.GetEvent<RemoveBaleEvent>().Subscribe(RemoveBaleFromConveyor);
            _eventAggregator.GetEvent<NamePipeMessage>().Subscribe(ProcessNamePipeMsg);
            _eventAggregator.GetEvent<UserLogin>().Subscribe(UserLogonEvent);
            _eventAggregator.GetEvent<UpdateBaleMoveEvent>().Subscribe(UpdateBaleMoveEvents);
            _eventAggregator.GetEvent<UpdateMainTimerEvents>().Subscribe(MainTimerEvent);

            ScreenWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            ScreenHeight = (int)System.Windows.SystemParameters.PrimaryScreenHeight;

            SheetCntPresent = false;// ClassCommon.SheetCntCheck;

            if (ClassCommon.SheetChecked == true)
            {
                SheetCounterWindow = new SheetCounterViewModel(_eventAggregator);
                ShowSheetCount = Visibility.Visible;
            }
            else
                ShowSheetCount = Visibility.Hidden;


            //ClassCommon.Layboy1
            LayBoyStationVM = new BaleStationViewModel(_eventAggregator, 99) 
            {
                UnitColor = Brushes.White
            };
            LayBoyStationVM.ShowStateionButton(false, "");

            //ClassCommon.Approach
            StationOneVM = new BaleStationViewModel(_eventAggregator, 1)
            {
                UnitColor = Brushes.White
            }; 
            StationOneVM.ShowStateionButton(false, "");

            //ClassCommon.Scale
            StationTwoVM = new BaleStationViewModel(_eventAggregator, 2)
            {
                UnitColor = Brushes.White
            }; 
            StationTwoVM.ShowStateionButton(false, "");

            //ClassCommon.Press
            StationThreeVM = new BaleStationViewModel(_eventAggregator, 3)
            {
                UnitColor = Brushes.White
            };
            StationThreeVM.ShowStateionButton(false, "");

            // ClassCommon.Marker
            StationFourVM = new BaleStationViewModel(_eventAggregator, 4)
            {
                UnitColor = Brushes.White
            };
            StationFourVM.ShowStateionButton(false, "");



            BaleOne = new BaleViewModel(_eventAggregator, 1);
            BaleOne.SetBaleColor(Brushes.Yellow, Brushes.Black);

            BaleTwo = new BaleViewModel(_eventAggregator, 2);
            BaleTwo.SetBaleColor(Brushes.Yellow, Brushes.Black);

            BaleThree = new BaleViewModel(_eventAggregator, 3);
            BaleThree.SetBaleColor(Brushes.Yellow, Brushes.Black);

            BaleFour = new BaleViewModel(_eventAggregator, 4);
            BaleFour.SetBaleColor(Brushes.Yellow, Brushes.Black);

            BaleFive = new BaleViewModel(_eventAggregator, 5);
            BaleFive.SetBaleColor(Brushes.Yellow, Brushes.Black);

            BaleSix = new BaleViewModel(_eventAggregator, 6);
            BaleSix.SetBaleColor(Brushes.Yellow, Brushes.Black);

            BaleSeven = new BaleViewModel(_eventAggregator, 7);
            BaleSeven.SetBaleColor(Brushes.Yellow, Brushes.Black);

            BaleEight = new BaleViewModel(_eventAggregator, 8);
            BaleEight.SetBaleColor(Brushes.Yellow, Brushes.Black);

            BaleNine = new BaleViewModel(_eventAggregator, 9);
            BaleNine.SetBaleColor(Brushes.Yellow, Brushes.Black);

            BaleTen = new BaleViewModel(_eventAggregator, 10);
            BaleTen.SetBaleColor(Brushes.Yellow, Brushes.Black);

            BaleEleven = new BaleViewModel(_eventAggregator, 11);
            BaleEleven.SetBaleColor(Brushes.White, Brushes.Blue);

            BaleTwelve = new BaleViewModel(_eventAggregator, 12);
            BaleTwelve.SetBaleColor(Brushes.White, Brushes.Blue);

            BaleThirteen = new BaleViewModel(_eventAggregator, 13);
            BaleThirteen.SetBaleColor(Brushes.White, Brushes.Blue);

            BaleFourteen = new BaleViewModel(_eventAggregator, 14);
            BaleFourteen.SetBaleColor(Brushes.White, Brushes.Blue);

            BaleFifteen = new BaleViewModel(_eventAggregator, 15);
            BaleFifteen.SetBaleColor(Brushes.White, Brushes.Blue);

            //Bale at Press-------------------------------------------------
            BaleOnePress = new BaleViewModel(_eventAggregator, 1);
            BaleOnePress.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleTwoPress = new BaleViewModel(_eventAggregator, 2);
            BaleTwoPress.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleThreePress = new BaleViewModel(_eventAggregator, 3);
            BaleThreePress.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleFourPress = new BaleViewModel(_eventAggregator, 4);
            BaleFourPress.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleFivePress = new BaleViewModel(_eventAggregator, 5);
            BaleFivePress.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            //Default Bale at Scale
            DefaultOneScale = new BaleViewModel(_eventAggregator, 0);
            DefaultOneScale.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultTwoScale = new BaleViewModel(_eventAggregator, 0);
            DefaultTwoScale.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultThreeScale = new BaleViewModel(_eventAggregator, 0);
            DefaultThreeScale.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultFourScale = new BaleViewModel(_eventAggregator, 0);
            DefaultFourScale.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultFiveScale = new BaleViewModel(_eventAggregator, 0);
            DefaultFiveScale.SetBaleColor(Brushes.White, Brushes.Blue);

            //Default Bales
            DefaultOnePress = new BaleViewModel(_eventAggregator, 0);
            DefaultOnePress.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultTwoPress = new BaleViewModel(_eventAggregator, 0);
            DefaultTwoPress.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultThreePress = new BaleViewModel(_eventAggregator, 0);
            DefaultThreePress.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultFourPress = new BaleViewModel(_eventAggregator, 0);
            DefaultFourPress.SetBaleColor(Brushes.White, Brushes.Blue);

            DefaultFivePress = new BaleViewModel(_eventAggregator, 0);
            DefaultFivePress.SetBaleColor(Brushes.White, Brushes.Blue);

            //Bale at Marker-------------------------------------------------
            BaleOneMrk = new BaleViewModel(_eventAggregator, 1);
            BaleOneMrk.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleTwoMrk = new BaleViewModel(_eventAggregator, 2);
            BaleTwoMrk.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleThreeMrk = new BaleViewModel(_eventAggregator, 3);
            BaleThreeMrk.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleFourMrk = new BaleViewModel(_eventAggregator, 4);
            BaleFourMrk.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleFiveMrk = new BaleViewModel(_eventAggregator, 5);
            BaleFiveMrk.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            //Default Bales
            BaleDefaultOneMrk = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultOneMrk.SetBaleColor(Brushes.White, Brushes.Blue);

            BaleDefaultTwoMrk = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultTwoMrk.SetBaleColor(Brushes.White, Brushes.Blue);

            BaleDefaultThreeMrk = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultThreeMrk.SetBaleColor(Brushes.White, Brushes.Blue);

            BaleDefaultFourMrk = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultFourMrk.SetBaleColor(Brushes.White, Brushes.Blue);

            BaleDefaultFiveMrk = new BaleViewModel(_eventAggregator, 0);
            BaleDefaultFiveMrk.SetBaleColor(Brushes.White, Brushes.Blue);
        }

        private void RemoveBaleFromConveyor(int obj)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { OneLineGraphicDropView.BaleGraphicDropWindow.RemoveBaleLineOne(obj); }));
        }

        private void ProcessNamePipeMsg(string message)
        {
            message = message.TrimEnd(';');
            string[] words = message.Split(';');

            if (words[0] == "LogOn")
                UserLogin = true;
            else if (words[0] == "Logoff")
                UserLogin = false;
        }

        private void UserLogonEvent(bool obj)
        {
            UserLogin = obj;
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
                        // { OneLineGraphicView.BaleGraphicWindow.MoveBalePassScale(Convert.ToInt32(ClassCommon.NewDattable.Rows[ClassCommon.NewDatIndex]["Position"])); }));

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
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateBaleMoveEvents -> { ex.Message}");
            }
        }

        /// <summary>
        /// Put Bales on Conveyor
        /// </summary>
        /// <param name="obj"></param>
        private void MainTimerEvent(DateTime obj)
        {

            Application.Current.Dispatcher.Invoke(new Action(() =>
            { _ = BaleAtScaleConveyorAsync(); }));

            //_ = BaleAtScaleConveyorAsync();

            if (SheetCntPresent == true)
                _ = UpdateShowBaleEvents();
        }

        private async Task BaleAtScaleConveyorAsync()
        {
            await Task.Run(() =>
            {

                //On Weigh Conveyor before the Press
                List<int> BaleOnScaleConveyor = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.WeighConveyorLineOne);
                BaleConveyorTwo = BaleOnScaleConveyor.Count();
                if (BaleConveyorTwo > 0) // After the Approach
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicDropView.BaleGraphicDropWindow.ClearBaleFromScaleConveyor(); }));

                    //put Bale on scale conveyor in order from BaleOnScaleConveyor list
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicDropView.BaleGraphicDropWindow.PutBaleOnScaleConveyor(BaleOnScaleConveyor); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicDropView.BaleGraphicDropWindow.ClearScaleConveyor(); }));

                //2 on Press Conveyor
                List<int> BaleOnPressConveyor = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.PressConveyorLineOne);
                BaleConveyorThree = BaleOnPressConveyor.Count();
                if (BaleConveyorThree > 0) // After the Scale
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicDropView.BaleGraphicDropWindow.ClearPressConveyor(); }));

                    //put Bale on Press conveyor
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicDropView.BaleGraphicDropWindow.PutBaleOnPressConveyor(BaleOnPressConveyor); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicDropView.BaleGraphicDropWindow.ClearPressConveyor(); }));


                //3 on Marker Conveyor
                List<int> BaleOnMarkerConveyor = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.MarkerConveyorLineOne);
                BaleConveyorMarker = BaleOnMarkerConveyor.Count();
                if (BaleConveyorMarker > 0) // After the Press
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicDropView.BaleGraphicDropWindow.ClearMrkConveyor(); }));

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicDropView.BaleGraphicDropWindow.PutBaleOnMarkerConveyor(BaleOnMarkerConveyor); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicDropView.BaleGraphicDropWindow.ClearMrkConveyor(); }));
            });
        }

        private async Task UpdateShowBaleEvents()
        {
            try
            {
                await Task.Run(() =>
                {
                    //Access 
                    //  ShowBalesInADrop(AcHandler.GeBaleDrop());
                    //SQL
                    ShowBalesInADrop(_sqlHandler.GetBaleDatatable());
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateShowBaleEvents -> { ex.Message}");
                MessageBox.Show($"Error in UpdateShowBaleEvents -> { ex.Message}");
            }
        }


        private void ShowBalesInADrop(DataTable newData)
        {
            try
            {
                if (newData.Rows.Count > 0)
                {
                    int ConID = Convert.ToInt32(newData.Rows[0]["ConveyorID"]);
                    int DropNum = Convert.ToInt32(newData.Rows[0]["NumbOnConv"]);

                    if (ConID == ClassCommon.LayBoy1ConveyorId) //8
                    {
                        DropInConveyorOne = DropNum.ToString();
                        txtStatus = $"Drop= {ConID}, DropNum= {DropNum}";
                        _eventAggregator.GetEvent<Reporttextmessage>().Publish(txtStatus);

                        switch (DropNum)
                        {
                            case 1:
                                ShowDropOne = Visibility.Visible;
                                ShowDropTwo = Visibility.Hidden;
                                ShowDropThree = Visibility.Hidden;
                                ShowDropFour = Visibility.Hidden;
                                ShowDropFive = Visibility.Hidden;
                                break;
                            case 2:
                                ShowDropOne = Visibility.Visible;
                                ShowDropTwo = Visibility.Visible;
                                ShowDropThree = Visibility.Hidden;
                                ShowDropFour = Visibility.Hidden;
                                ShowDropFive = Visibility.Hidden;
                                break;
                            case 3:
                                ShowDropOne = Visibility.Visible;
                                ShowDropTwo = Visibility.Visible;
                                ShowDropThree = Visibility.Visible;
                                ShowDropFour = Visibility.Hidden;
                                ShowDropFive = Visibility.Hidden;
                                break;
                            case 4:
                                ShowDropOne = Visibility.Visible;
                                ShowDropTwo = Visibility.Visible;
                                ShowDropThree = Visibility.Visible;
                                ShowDropFour = Visibility.Visible;
                                ShowDropFive = Visibility.Hidden;
                                break;
                            case 5:
                                ShowDropOne = Visibility.Visible;
                                ShowDropTwo = Visibility.Visible;
                                ShowDropThree = Visibility.Visible;
                                ShowDropFour = Visibility.Visible;
                                ShowDropFive = Visibility.Visible;
                                break;
                        }
                    }
                    else
                    {
                        ShowDropOne = Visibility.Hidden;
                        ShowDropTwo = Visibility.Hidden;
                        ShowDropThree = Visibility.Hidden;
                        ShowDropFour = Visibility.Hidden;
                        ShowDropFive = Visibility.Hidden;
                        DropInConveyorOne = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in ShowBalesInADrop -> { ex.Message}");
                MessageBox.Show($"Error in ShowBalesInADrop -> { ex.Message}");
            }
        }
    }
}
