using _8100UI.Services;
using BaleGraphic.Views;
using BaleStation.ViewModels;
using Prism.Events;
using PulpBale.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BaleGraphic.ViewModels
{
    public class OneLineGraphicViewModel : GraphicViewModelBase
    {
        protected new readonly IEventAggregator _eventAggregator;
        private ServiceEventsTimer ClearPressDelayTimer;

        private const int DropLineA = 0;
        private const int ScaleLineA = 1;
        private const int PressLineA = 2;
        private const int MarkerLineA = 3;

        private List<ClsConveyor> Conveys = new List<ClsConveyor>();

       
        public OneLineGraphicViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;

            UserLogin = ClassCommon.UserLogin;

            ClearPressDelayTimer = new ServiceEventsTimer(_eventAggregator);

            _eventAggregator.GetEvent<UpdateBaleMoveEvent>().Subscribe(UpdateBaleMoveEvents);
            _eventAggregator.GetEvent<UpdateMainTimerEvents>().Subscribe(MainTimerEvent);

            _eventAggregator.GetEvent<RemoveBaleEvent>().Subscribe(RemoveBaleFromConveyor);
            _eventAggregator.GetEvent<NamePipeMessage>().Subscribe(ProcessNamePipeMsg);
            _eventAggregator.GetEvent<UserLogin>().Subscribe(UserLogonEvent);

            //  _eventAggregator.GetEvent<DelayPressTimerStop>().Subscribe(DelayPressTimerStopEvent);
            //  _eventAggregator.GetEvent<BaleDataTableChnges>().Subscribe(BaleTableChecgesEvent);

            ScreenWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            ScreenHeight = (int)System.Windows.SystemParameters.PrimaryScreenHeight;

            SetUpStations();
            SetBaleOrdered();

            OneLineGraphicView.BaleGraphicWindow.SetUpStationConveyor();

            SetBalesConveyorOne();
            //Bale at Press-------------------------------------------------
            SetBalesConveyorTwo();
            //Default Bale at Scale
            SetBalesConveyorThree();
            //Bale at Marker-------------------------------------------------
            SetBaleConveyorFour();


        }

        private void SetBaleConveyorFour()
        {
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

            BaleSixMrk = new BaleViewModel(_eventAggregator, 6);
            BaleSixMrk.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleSevenMrk = new BaleViewModel(_eventAggregator, 7);
            BaleSevenMrk.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleEightMrk = new BaleViewModel(_eventAggregator, 8);
            BaleEightMrk.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleNineMrk = new BaleViewModel(_eventAggregator, 9);
            BaleNineMrk.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleTenMrk = new BaleViewModel(_eventAggregator, 10);
            BaleTenMrk.SetBaleColor(Brushes.Yellow, Brushes.Blue);

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

        private void SetBalesConveyorThree()
        {
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
        }

        private void SetBalesConveyorTwo()
        {
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

            BaleSixPress = new BaleViewModel(_eventAggregator, 6);
            BaleSixPress.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleSevenPress = new BaleViewModel(_eventAggregator, 7);
            BaleSevenPress.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleEightPress = new BaleViewModel(_eventAggregator, 8);
            BaleEightPress.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleNinePress = new BaleViewModel(_eventAggregator, 9);
            BaleNinePress.SetBaleColor(Brushes.Yellow, Brushes.Blue);

            BaleTenPress = new BaleViewModel(_eventAggregator, 10);
            BaleTenPress.SetBaleColor(Brushes.Yellow, Brushes.Blue);
        }

        private void SetBalesConveyorOne()
        {
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
        }

        /// <summary>
        /// 
        /// Update all data at last station! 
        /// Last station could be either Press or Marker
        /// Now only works moving bales from left to right only
        /// 
        /// Stations order by StationID  0 to  ClassCommon.BaleStation.Count
        /// 
        /// </summary>
        private void SetUpStations()
        {
            if(ClassCommon.MoveLeftToRight)
            {
                //
                for (int i = 0; i < ClassCommon.BaleStation.Count; i++)
                {
                    string stationName = ClassCommon.BaleStation[i].StationName;

                    switch (stationName)
                    {
                        case "Approach":

                            if(ClassCommon.BaleStation[i].StationId == 0 ) // Line 1
                            {
                                StationOneVM = new BaleStationViewModel(_eventAggregator, ClassCommon.BaleStation[i].StationId)
                                {
                                    UnitColor = Brushes.White
                                };
                                StationOneVM.ShowStateionButton(false, "");
                            }

                    
                            break;

                        case "Weigh":

                            if (ClassCommon.BaleStation[i].StationId == 1) // Line 1
                            {
                                StationTwoVM = new BaleStationViewModel(_eventAggregator, ClassCommon.BaleStation[i].StationId)
                                {
                                    UnitColor = Brushes.White
                                };
                                StationTwoVM.ShowStateionButton(false, "");
                            }
                          
                            break;

                        case "Press":

                            if (ClassCommon.BaleStation[i].StationId == 2) // Line 1
                            {
                                StationThreeVM = new BaleStationViewModel(_eventAggregator, ClassCommon.BaleStation[i].StationId)
                                {
                                    UnitColor = Brushes.White
                                };
                                StationThreeVM.ShowStateionButton(false, "");
                                //Set Last Station => update data here
                                if (ClassCommon.BaleStation.Count == 3)
                                {
                                    ClassCommon.UpdateDataAtStation = 2;
                                }
                            }

                            break;

                        case "Marker":

                            if (ClassCommon.BaleStation[i].StationId == 2) // Line 1
                            {
                                StationFourVM = new BaleStationViewModel(_eventAggregator, ClassCommon.BaleStation[i].StationId) 
                                {
                                    UnitColor = Brushes.White
                                };
                                StationFourVM.ShowStateionButton(false, "");
                                //Set Last Station => update data here
                                if (ClassCommon.BaleStation.Count == 4)
                                {
                                    ClassCommon.UpdateDataAtStation = 3;
                                }
                            }
                   
                            break;

                        case "Layboy1":
  
                            LayBoyStationVM = new BaleStationViewModel(_eventAggregator, ClassCommon.BaleStation[i].StationId)
                            {
                                UnitColor = Brushes.White
                            };
                            LayBoyStationVM.ShowStateionButton(false, "");
                           
                            break;

                        case "DropXfer":

                            int stationIdXf = ClassCommon.BaleStation[i].StationId;

                            break;

                    }
                }
            }
            else //need work here  need to get the station table.
            {
                StationOneVM = new BaleStationViewModel(_eventAggregator, ClassCommon.Marker)
                {
                    UnitColor = Brushes.White
                };
                //StationOneVM.ShowStateionButton(true, "Reset");
                //
                StationTwoVM = new BaleStationViewModel(_eventAggregator, ClassCommon.Press)
                {
                    UnitColor = Brushes.White
                };
                StationTwoVM.ShowStateionButton(false, "");
                //
                StationThreeVM = new BaleStationViewModel(_eventAggregator, ClassCommon.Scale)
                {
                    UnitColor = Brushes.White
                };
                StationThreeVM.ShowStateionButton(false, "");
                //
                StationFourVM = new BaleStationViewModel(_eventAggregator, ClassCommon.Drop) 
                {
                    UnitColor = Brushes.White
                };
                StationFourVM.ShowStateionButton(false, "");
            }
        }

        private void SetUpStations_old ()
        {
            if (ClassCommon.MoveLeftToRight)
            {
                StationOneVM = new BaleStationViewModel(_eventAggregator, ClassCommon.Drop) //0
                {
                    UnitColor = Brushes.White
                };
                StationOneVM.ShowStateionButton(false, "");

                StationTwoVM = new BaleStationViewModel(_eventAggregator, ClassCommon.Scale) //1
                {
                    UnitColor = Brushes.White
                };
                StationTwoVM.ShowStateionButton(false, "");

                StationThreeVM = new BaleStationViewModel(_eventAggregator, ClassCommon.Press) //2
                {
                    UnitColor = Brushes.White
                };
                StationThreeVM.ShowStateionButton(false, "");

                /*
                for (int i = 0; i < ClassCommon.BaleStations.Count; i++)
                {
                    if (ClassCommon.BaleStations[i].StationName == "Marker")
                    {
                        StationFourVM = new BaleStationViewModel(_eventAggregator, ClassCommon.Marker) //3
                        {
                            UnitColor = Brushes.White
                        };
                        StationFourVM.ShowStateionButton(false, "");
                        ShowStation4L1 = Visibility.Visible;
                        ClassCommon.UpdateDataAtStation = ClassCommon.Marker;
                    }
                    else
                    {
                        ShowStation4L1 = Visibility.Hidden;
                        ClassCommon.UpdateDataAtStation = ClassCommon.Press;
                    }
                }*/

            }
            else
            {
                StationOneVM = new BaleStationViewModel(_eventAggregator, ClassCommon.Marker)
                {
                    UnitColor = Brushes.White
                };
                //StationOneVM.ShowStateionButton(true, "Reset");
                //
                StationTwoVM = new BaleStationViewModel(_eventAggregator, ClassCommon.Press)
                {
                    UnitColor = Brushes.White
                };
                StationTwoVM.ShowStateionButton(false, "");
                //
                StationThreeVM = new BaleStationViewModel(_eventAggregator, ClassCommon.Scale)
                {
                    UnitColor = Brushes.White
                };
                StationThreeVM.ShowStateionButton(false, "");
                //
                StationFourVM = new BaleStationViewModel(_eventAggregator, ClassCommon.Drop)
                {
                    UnitColor = Brushes.White
                };
                StationFourVM.ShowStateionButton(false, "");
            }
        }


        private Queue<int> BaleOnScale = new Queue<int>();
        private Queue<int> BaleOnPress = new Queue<int>();
        private Queue<int> BaleOnmarker = new Queue<int>();
        private void UserLogonEvent(bool obj)
        {
            UserLogin = obj;
        }

        /// <summary>
        /// Put Bales on Conveyor
        /// </summary>
        /// <param name="obj"></param>
        private void MainTimerEvent(DateTime obj)
        {
            if (ClassCommon.MoveLeftToRight)
                Application.Current.Dispatcher.Invoke(new Action(() =>
                { _ = BaleOnConveyorAsyncLeftRight(); }));
            else
                Application.Current.Dispatcher.Invoke(new Action(() =>
                { _ = BaleOnConveyorAsyncRightLeft(); }));
        }

        /// <summary>
        /// First Station in on the left, bale move to the right =>
        /// </summary>
        /// <returns></returns>
        private async Task BaleOnConveyorAsyncLeftRight()
        {
            await Task.Run(() =>
            {
               
                //On Approach Conveyor before the scale
                List<int> BaleOnApproachConveyor = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.ApproachConveyorLineOne);
                BaleCountConveyorOne = BaleOnApproachConveyor.Count();
                if (this.BaleCountConveyorOne > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.ClearConveyorOne(); }));

                    //put Bale on scale conveyor in order from BaleCountConveyorOne list
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.PutBaleOnScaleConveyor(BaleOnApproachConveyor); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.ClearConveyorOne(); }));

                //On Weigh Conveyor before the Press
                List<int> BaleOnWeighConveyor = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.WeighConveyorLineOne);
                BaleConveyorTwo = BaleOnWeighConveyor.Count();
                if (BaleConveyorTwo > 0) // After the Scale
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.ClearConveyorTwo(); }));

                    //put Bale on Press conveyor
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.PutBaleOnPressConveyor(BaleOnWeighConveyor); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.ClearConveyorTwo(); }));

                //On Press Conveyor before Marker
                List<int> BaleOnPressConveyor = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.PressConveyorLineOne);
                BaleConveyorThree = BaleOnPressConveyor.Count();
                if (BaleConveyorThree > 0) // After the Press
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.ClearConveyorThree(); }));

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.PutBaleOnMarkerConveyor(BaleOnPressConveyor); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.ClearConveyorThree(); }));
            });
        }

        /// <summary>
        /// First Station is on the Right, bale moves to left <=
        /// </summary>
        /// <returns></returns>
        private async Task BaleOnConveyorAsyncRightLeft()
        {
            await Task.Run(() =>
            {
                //On Approach Conveyor before the scale
                List<int> BaleOnConveyorThree = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.ApproachConveyorLineOne);
                BaleConveyorThree = BaleOnConveyorThree.Count();
                if (this.BaleConveyorThree > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.ClearConveyorThree(); }));

                    //put Bale on scale conveyor in order from BaleCountConveyorOne list
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.PutBaleOnConveyorThree(BaleOnConveyorThree); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.ClearConveyorThree(); }));

                //On Weigh Conveyor before the Press
                List<int> BaleOnConveyorTwo = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.WeighConveyorLineOne);
                BaleConveyorTwo = BaleOnConveyorTwo.Count();
                if (BaleConveyorTwo > 0) // After the Scale
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.ClearConveyorTwo(); }));

                    //put Bale on Press conveyor
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.PutBaleOnConveyorTwo(BaleOnConveyorTwo); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.ClearConveyorTwo(); }));


                //On Press Conveyor before Marker
                List<int> BaleOnConveyorOne = _sqlHandler.GetCurrentBalesOnConveyor(ClassCommon.PressConveyorLineOne);
                BaleCountConveyorOne = BaleOnConveyorOne.Count();
                if (BaleCountConveyorOne > 0) // After the Press
                {
                 
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.PutBaleOnConveyorOne(BaleOnConveyorOne); }));
                }
                else
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { OneLineGraphicView.BaleGraphicWindow.ClearConveyorOne(); })); 
            });
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


        private void ProcessNamePipeMsg(string message)
        {
            message = message.TrimEnd(';');
            string[] words = message.Split(';');

            if (words[0] == "LogOn")
                UserLogin = true;
            else if (words[0] == "Logoff")
                UserLogin = false;
        }

        private void RemoveBaleFromConveyor(int obj)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { OneLineGraphicView.BaleGraphicWindow.RemoveBaleLineOne(obj); }));
        }

        private void MoveBaleLineA(int eventId, int baleId, int baleStatus)
        {
            //Console.WriteLine($"A_scrWidth = {ClassCommon.ScrWidth.ToString()} A_scrHeight = {ClassCommon.ScrHeight.ToString()}");

            int OnetoTwo = (int)(ClassCommon.ScrWidth * 0.28); // 0.30
            int TwoToThree = (int)(ClassCommon.ScrWidth * 0.53); //.55
            int ThreeToFour = (int)(ClassCommon.ScrWidth * 0.75); //.85
                                                                  //int FourToFive = (int)(ClassCommon.ScrWidth * 0.84);

            switch (eventId)
            {
                case DropLineA:

                    break;

                case ScaleLineA:
                    OneLineGraphicView.BaleGraphicWindow.MoveOneBaleA(eventId, baleId, 1, OnetoTwo, baleStatus);
                    //ClsSerilog.LogMessage(ClsSerilog.Info, $"Station= Scale  BaleId= {baleId} baleStatus= {baleStatus} ");
                    break;

                case PressLineA:
                    OneLineGraphicView.BaleGraphicWindow.MoveOneBaleA(eventId, baleId, OnetoTwo, TwoToThree, baleStatus);
                   // ClsSerilog.LogMessage(ClsSerilog.Info, $"Station= Press  BaleId= {baleId} baleStatus= {baleStatus} ");
                    break;

                case MarkerLineA:
                    OneLineGraphicView.BaleGraphicWindow.MoveOneBaleA(eventId, baleId, TwoToThree, ThreeToFour, baleStatus);
                   // ClsSerilog.LogMessage(ClsSerilog.Info, $"Station= Marker  BaleId= {baleId} baleStatus= {baleStatus} ");
                    break;
            }
        }

       
        //----------------------------------------------------------------------
        
    }
}
