using _8100UI.Services;
using BaleGraphic.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace BaleGraphic.Views
{
    /// <summary>
    /// Interaction logic for OneLineGraphicView.xaml
    /// </summary>
    public partial class OneLineGraphicView : UserControl
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;

        private readonly Storyboard myStoryboard = new Storyboard();
        public static OneLineGraphicView BaleGraphicWindow;        
        private double wdCoef = 0.0;

        public int movebalespeed { get; set; } = 2;
        public int balestationA { get; set; }

        private bool reverseorder = true;

        public bool GostOneMoved { get; set; }

        //   Queue<int> gostBale = new Queue<int>();

        //Small Window @ Scale reverse order
        int BaleOneScale;
        int BaleTwoScale;
        int BaleThreeScale;
        int BaleFourScale;
        int BaleFiveScale;
        int BaleSixScale;
        int BaleSevenScale;
        int BaleEightScale;
        int BaleNineScale;
        int BaleTenScale;

        //Small Window @ Scale
        int DefaultBaleOneScale;     
        int DefaultBaleTwoScale;     
        int DefaultBaleThreeScale;   
        int DefaultBaleFourScale;    
        int DefaultBaleFiveScale;    

        //Small Window @ press
        int BaleOnePress;
        int BaleTwoPress;
        int BaleThreePress;
        int BaleFourPress;
        int BaleFivePress;
        int BaleSixPress;
        int BaleSevenPress;
        int BaleEightPress;
        int BaleNinePress;
        int BaleTenPress;

        //small Window @ Press
        int DefaultBaleOnePress;   
        int DefaultBaleTwoPress;   
        int DefaultBaleThreePress;    
        int DefaultpressFourPress;      
        int DefaultpressFivePress;    


        //full Window @ Marker
        int BaleOneMkr;   
        int BaleTwoMkr;   
        int BaleThreeMkr;   
        int BaleFourMkr;      
        int BaleFiveMkr;    
        int BaleSixMkr;     
        int BaleSevenMkr;   
        int BaleEightMkr;   
        int BaleNineMkr;    
        int BaleTenMkr;     

        //small Window @ Marker
        int DefaultBaleOneMkr; 
        int DefaultBaleTwoMkr; 
        int DefaultBaleThreeMkr;
        int DefaultpressFourMkr;  
        int DefaultpressFiveMkr; 


        public OneLineGraphicView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            BaleGraphicWindow = this;
            this._eventAggregator = eventAggregator;
            this.DataContext = new OneLineGraphicViewModel(_eventAggregator);

            ClearAllBales();

        
        }


        /// <summary>
        /// Setup Stations and Conveyors
        /// </summary>
        public void SetUpStationConveyor()
        {
            FrontEnd.Width = new GridLength(100);

            //No Drop
            stationOne.Width = new GridLength(0);
            ConvOne.Width = new GridLength(0);

            //No Mrker
            ConvFour.Width = new GridLength(0);
            stationFive.Width = new GridLength(0);

            BackEnd.Width = new GridLength(100);

        }


       
        private void BaleGraphic_sizeChanged(object sender, SizeChangedEventArgs e)
        {
            double iScreenWidth = e.NewSize.Width;
            wdCoef = e.NewSize.Width * 0.35;

            //Approach to Scale
            if (reverseorder)
            {
                BaleOneScale = (int)wdCoef - 460;
                BaleTwoScale = (int)wdCoef - 440;
                BaleThreeScale = (int)wdCoef - 420;
                BaleFourScale = (int)wdCoef - 400;
                BaleFiveScale = (int)wdCoef - 380;
                BaleSixScale = (int)wdCoef - 360;
                BaleSevenScale = (int)wdCoef - 340;
                BaleEightScale = (int)wdCoef - 320;
                BaleNineScale = (int)wdCoef - 300;
                BaleTenScale = (int)wdCoef - 280;
            }
            else
            {
                BaleOneScale = (int)wdCoef - 280;
                BaleTwoScale = (int)wdCoef - 300;
                BaleThreeScale = (int)wdCoef - 320;
                BaleFourScale = (int)wdCoef - 340;
                BaleFiveScale = (int)wdCoef - 360;
                BaleSixScale = (int)wdCoef - 380;
                BaleSevenScale = (int)wdCoef - 400;
                BaleEightScale = (int)wdCoef - 420;
                BaleNineScale = (int)wdCoef - 440;
                BaleTenScale = (int)wdCoef - 460;
            }

            //Scale to Press
            if (reverseorder)
            {
                BaleOnePress = (int)wdCoef - 460;
                BaleTwoPress = (int)wdCoef - 440;
                BaleThreePress = (int)wdCoef - 420;
                BaleFourPress = (int)wdCoef - 400;
                BaleFivePress = (int)wdCoef - 380;
                BaleSixPress = (int)wdCoef - 360;
                BaleSevenPress = (int)wdCoef - 340;
                BaleEightPress = (int)wdCoef - 320;
                BaleNinePress = (int)wdCoef - 300;
                BaleTenPress = (int)wdCoef  - 280;

                DefaultBaleOnePress = (int)wdCoef - 260;
                DefaultBaleTwoPress = (int)wdCoef - 240;
                DefaultBaleThreePress = (int)wdCoef - 220;
                DefaultpressFourPress = (int)wdCoef - 200;
                DefaultpressFivePress = (int)wdCoef - 180;
            }
            else
            {
                BaleOnePress = (int)wdCoef - 280;
                BaleTwoPress = (int)wdCoef - 300;
                BaleThreePress = (int)wdCoef - 320;
                BaleFourPress = (int)wdCoef - 340;
                BaleFivePress = (int)wdCoef - 360;
                BaleSixPress = (int)wdCoef - 380;
                BaleSevenPress = (int)wdCoef - 400;
                BaleEightPress = (int)wdCoef - 420;
                BaleNinePress = (int)wdCoef - 440;
                BaleTenPress = (int)wdCoef - 460;

                DefaultBaleOnePress = (int)wdCoef - 480;
                DefaultBaleTwoPress = (int)wdCoef - 500;
                DefaultBaleThreePress = (int)wdCoef - 520;
                DefaultpressFourPress = (int)wdCoef - 540;
                DefaultpressFivePress = (int)wdCoef - 560;
            }

            //Press to Marker
            if (reverseorder)
            {
                BaleOneMkr = (int)wdCoef - 460;
                BaleTwoMkr = (int)wdCoef - 440;
                BaleThreeMkr = (int)wdCoef - 420;
                BaleFourMkr = (int)wdCoef - 400;
                BaleFiveMkr = (int)wdCoef - 380;
                BaleSixMkr = (int)wdCoef - 360;
                BaleSevenMkr = (int)wdCoef - 340;
                BaleEightMkr = (int)wdCoef - 320;
                BaleNineMkr = (int)wdCoef - 300;
                BaleTenMkr = (int)wdCoef - 280;

                DefaultBaleOneMkr = (int)wdCoef - 260;
                DefaultBaleTwoMkr = (int)wdCoef - 240;
                DefaultBaleThreeMkr = (int)wdCoef - 220;
                DefaultpressFourMkr = (int)wdCoef - 200;
                DefaultpressFiveMkr = (int)wdCoef - 180;
            }
            else
            {
                BaleOneMkr = (int)wdCoef - 280;
                BaleTwoMkr = (int)wdCoef - 300;
                BaleThreeMkr = (int)wdCoef - 320;
                BaleFourMkr = (int)wdCoef - 340;
                BaleFiveMkr = (int)wdCoef - 360;
                BaleSixMkr = (int)wdCoef - 380;
                BaleSevenMkr = (int)wdCoef - 400;
                BaleEightMkr = (int)wdCoef - 420;
                BaleNineMkr = (int)wdCoef - 440;
                BaleTenMkr = (int)wdCoef - 460;

                DefaultBaleOneMkr = (int)wdCoef - 480;
                DefaultBaleTwoMkr = (int)wdCoef - 500;
                DefaultBaleThreeMkr = (int)wdCoef - 520;
                DefaultpressFourMkr = (int)wdCoef - 540;
                DefaultpressFiveMkr = (int)wdCoef - 560;
            }
        }

       

        private void PutDefaultBaleOnScale(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        if (baleList[i] == 0)
                        {
                            defaultbaleOneScale.Margin = new Thickness(DefaultBaleOneScale, 0, 0, 0);
                            defaultbaleOneScale.Visibility = Visibility.Visible;
                        }
                        break;

                    case 1:
                        if (baleList[i] == 0)
                        {
                            defaultbaleTwoScale.Margin = new Thickness(DefaultBaleTwoScale, 0, 0, 0);
                            defaultbaleTwoScale.Visibility = Visibility.Visible;
                        }
                        break;

                    case 2:
                        if (baleList[i] == 0)
                        {
                            defaultbaleThreeScale.Margin = new Thickness(DefaultBaleThreeScale, 0, 0, 0);
                            defaultbaleThreeScale.Visibility = Visibility.Visible;
                        }
                        break;

                    case 3:
                        if (baleList[i] == 0)
                        {
                            defaultbaleFourScale.Margin = new Thickness(DefaultBaleFourScale, 0, 0, 0);
                            defaultbaleFourScale.Visibility = Visibility.Visible;
                        }
                        break;

                    case 4:
                        {
                            defaultbaleFiveScale.Margin = new Thickness(DefaultBaleFiveScale, 0, 0, 0);
                            defaultbaleFiveScale.Visibility = Visibility.Visible;
                        }
                        break;
                }
            }
        }
        private void PutDefaultBaleOnPressConveyor(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        if (baleList[i] == 0)
                        {
                            defaultbaleOnePress.Margin = new Thickness(DefaultBaleOnePress, 0, 0, 0);
                            defaultbaleOnePress.Visibility = Visibility.Visible;
                        }
                        break;

                    case 1:
                        if (baleList[i] == 0)
                        {
                            defaultbaleTwoPress.Margin = new Thickness(DefaultBaleTwoPress, 0, 0, 0);
                            defaultbaleTwoPress.Visibility = Visibility.Visible;
                        }
                        break;

                    case 2:
                        if (baleList[i] == 0)
                        {
                            defaultbaleThreePress.Margin = new Thickness(DefaultBaleThreePress, 0, 0, 0);
                            defaultbaleThreePress.Visibility = Visibility.Visible;
                        }
                        break;

                    case 3:
                        if (baleList[i] == 0)
                        {
                            defaultbaleFourPress.Margin = new Thickness(DefaultpressFourPress, 0, 0, 0);
                            defaultbaleFourPress.Visibility = Visibility.Visible;
                        }
                        break;

                    case 4:
                        {
                            defaultbaleFivePress.Margin = new Thickness(DefaultpressFivePress, 0, 0, 0);
                            defaultbaleFivePress.Visibility = Visibility.Visible;
                        }
                        break;
                }
            }
        }
        internal void PutDefaultBaleOnMrkConveyor(List<int> baleList)
        {
            
            for (int i = 0; i < baleList.Count; i++ )
            {
                switch (i)
                {
                    case 0:
                        if(baleList[i] == 0)
                        {
                            defaultbaleOneMrk.Margin = new Thickness(DefaultBaleOneMkr, 0, 0, 0);
                            defaultbaleOneMrk.Visibility = Visibility.Visible;
                        }
                        break;

                    case 1:
                        if (baleList[i] == 0)
                        {
                            defaultbaleTwoMrk.Margin = new Thickness(DefaultBaleTwoMkr, 0, 0, 0);
                            defaultbaleTwoMrk.Visibility = Visibility.Visible;
                        }
                        break;

                    case 2:
                        if (baleList[i] == 0)
                        {
                            defaultbaleThreeMrk.Margin = new Thickness(DefaultBaleThreeMkr, 0, 0, 0);
                            defaultbaleThreeMrk.Visibility = Visibility.Visible;
                        }
                        break;

                    case 3:
                        if (baleList[i] == 0)
                        {
                            defaultbaleFourMrk.Margin = new Thickness(DefaultpressFourMkr, 0, 0, 0);
                            defaultbaleFourMrk.Visibility = Visibility.Visible;
                        }
                        break;

                    case 4:
                        {
                            defaultbaleFiveMrk.Margin = new Thickness(DefaultpressFiveMkr, 0, 0, 0);
                            defaultbaleFiveMrk.Visibility = Visibility.Visible;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Conveyor before the Weight scale
        /// </summary>
        /// <param name="baleList"></param>
        internal void PutBaleOnScaleConveyor(List<int> baleList)
        {
           
            for (int i = 0; i < baleList.Count; i++)
            {
               // Console.WriteLine($"----- i = {i} and baleList[i] = {baleList[i]}");

                switch (baleList[i])
                {
                    case 0:
                        PutDefaultBaleOnScale(baleList);
                        break;
                    case 1:
                        baleOneLineOne.Margin = new Thickness(BaleOneScale, 0, 0, 0);
                        baleOneLineOne.Visibility = Visibility.Visible;
                        break;

                    case 2:
                        baleTwoLineOne.Margin = new Thickness(BaleTwoScale, 0, 0, 0);
                        baleTwoLineOne.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        baleThreeLineOne.Margin = new Thickness(BaleThreeScale, 0, 0, 0);
                        baleThreeLineOne.Visibility = Visibility.Visible;
                        break;

                    case 4:
                        baleFourLineOne.Margin = new Thickness(BaleFourScale, 0, 0, 0);
                        baleFourLineOne.Visibility = Visibility.Visible;
                        break;

                    case 5:
                        baleFiveLineOne.Margin = new Thickness(BaleFiveScale, 0, 0, 0);
                        baleFiveLineOne.Visibility = Visibility.Visible;
                        break;

                    case 6:
                        baleSixLineOne.Margin = new Thickness(BaleSixScale, 0, 0, 0);
                        baleSixLineOne.Visibility = Visibility.Visible;
                        break;

                    case 7:
                        baleSevenLineOne.Margin = new Thickness(BaleSevenScale, 0, 0, 0);
                        baleSevenLineOne.Visibility = Visibility.Visible;
                        break;

                    case 8:
                        baleEightLineOne.Margin = new Thickness(BaleEightScale, 0, 0, 0);
                        baleEightLineOne.Visibility = Visibility.Visible;
                        break;
                    case 9:
                        baleNineLineOne.Margin = new Thickness(BaleNineScale, 0, 0, 0);
                        baleNineLineOne.Visibility = Visibility.Visible;
                        break;

                    case 10:
                        baleTenLineOne.Margin = new Thickness(BaleTenScale, 0, 0, 0);
                        baleTenLineOne.Visibility = Visibility.Visible;
                        break;
                }
            }

        }
        internal void PutBaleOnPressConveyor(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                switch (baleList[i])
                {
                    case 0:
                        PutDefaultBaleOnPressConveyor(baleList);
                        break;

                    case 1:
                        baleOnePress.Margin = new Thickness(BaleOnePress, 0, 0, 0);
                        baleOnePress.Visibility = Visibility.Visible;
                        break;

                    case 2:
                        baleTwoPress.Margin = new Thickness(BaleTwoPress, 0, 0, 0);
                        baleTwoPress.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        baleThreePress.Margin = new Thickness(BaleThreePress, 0, 0, 0);
                        baleThreePress.Visibility = Visibility.Visible;
                        break;

                    case 4:
                        baleFourPress.Margin = new Thickness(BaleFourPress, 0, 0, 0);
                        baleFourPress.Visibility = Visibility.Visible;
                        break;

                    case 5:
                        baleFivePress.Margin = new Thickness(BaleFivePress, 0, 0, 0);
                        baleFivePress.Visibility = Visibility.Visible;
                        break;

                    case 6:
                        baleSixPress.Margin = new Thickness(BaleSixPress, 0, 0, 0);
                        baleSixPress.Visibility = Visibility.Visible;
                        break;

                    case 7:
                        baleSevenPress.Margin = new Thickness(BaleSevenPress, 0, 0, 0);
                        baleSevenPress.Visibility = Visibility.Visible;
                        break;

                    case 8:
                        baleEightPress.Margin = new Thickness(BaleEightPress, 0, 0, 0);
                        baleEightPress.Visibility = Visibility.Visible;
                        break;
                    case 9:
                        baleNinePress.Margin = new Thickness(BaleNinePress, 0, 0, 0);
                        baleNinePress.Visibility = Visibility.Visible;
                        break;

                    case 10:
                        baleTenPress.Margin = new Thickness(BaleTenPress, 0, 0, 0);
                        baleTenPress.Visibility = Visibility.Visible;
                        break;
                }
            }
        }
        internal void PutBaleOnMarkerConveyor(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                switch (baleList[i])
                {
                    case 0:
                        PutDefaultBaleOnMrkConveyor(baleList);
                        break;

                    case 1:
                        baleOneMrk.Margin = new Thickness(BaleOneMkr, 0, 0, 0);
                        baleOneMrk.Visibility = Visibility.Visible;
                        break;

                    case 2:
                        baleTwoMrk.Margin = new Thickness(BaleTwoMkr, 0, 0, 0);
                        baleTwoMrk.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        baleThreeMrk.Margin = new Thickness(BaleThreeMkr, 0, 0, 0);
                        baleThreeMrk.Visibility = Visibility.Visible;
                        break;

                    case 4:
                        baleFourMrk.Margin = new Thickness(BaleFourMkr, 0, 0, 0);
                        baleFourMrk.Visibility = Visibility.Visible;
                        break;

                    case 5:
                        baleFiveMrk.Margin = new Thickness(BaleFiveMkr, 0, 0, 0);
                        baleFiveMrk.Visibility = Visibility.Visible;
                        break;

                    case 6:
                        baleSixMrk.Margin = new Thickness(BaleSixMkr, 0, 0, 0);
                        baleSixMrk.Visibility = Visibility.Visible;
                        break;

                    case 7:
                        baleSevenMrk.Margin = new Thickness(BaleSevenMkr, 0, 0, 0);
                        baleSevenMrk.Visibility = Visibility.Visible;
                        break;

                    case 8:
                        baleEightMrk.Margin = new Thickness(BaleEightMkr, 0, 0, 0);
                        baleEightMrk.Visibility = Visibility.Visible;
                        break;
                    case 9:
                        baleNineLineOne.Margin = new Thickness(BaleNineMkr, 0, 0, 0);
                        baleNineLineOne.Visibility = Visibility.Visible;
                        break;

                    case 10:
                        baleTenLineOne.Margin = new Thickness(BaleTenMkr, 0, 0, 0);
                        baleTenLineOne.Visibility = Visibility.Visible;
                        break;
                }
            }
        }


        internal void PutBaleOnConveyorOne(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                switch (baleList[i])
                {
                    case 0:
                        PutDefaultBaleOnScale(baleList);
                        break;
                    case 1:
                        baleOneLineOne.Margin = new Thickness(10, 0, 0, 0);
                        baleOneLineOne.Visibility = Visibility.Visible;
                        break;

                    case 2:
                        baleTwoLineOne.Margin = new Thickness(15, 0, 0, 0);
                        baleTwoLineOne.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        baleThreeLineOne.Margin = new Thickness(20, 0, 0, 0);
                        baleThreeLineOne.Visibility = Visibility.Visible;
                        break;

                    case 4:
                        baleFourLineOne.Margin = new Thickness(25, 0, 0, 0);
                        baleFourLineOne.Visibility = Visibility.Visible;
                        break;

                    case 5:
                        baleFiveLineOne.Margin = new Thickness(30, 0, 0, 0);
                        baleFiveLineOne.Visibility = Visibility.Visible;
                        break;

                    case 6:
                        baleSixLineOne.Margin = new Thickness(35, 0, 0, 0);
                        baleSixLineOne.Visibility = Visibility.Visible;
                        break;

                    case 7:
                        baleSevenLineOne.Margin = new Thickness(40, 0, 0, 0);
                        baleSevenLineOne.Visibility = Visibility.Visible;
                        break;

                    case 8:
                        baleEightLineOne.Margin = new Thickness(45, 0, 0, 0);
                        baleEightLineOne.Visibility = Visibility.Visible;
                        break;
                    case 9:
                        baleNineLineOne.Margin = new Thickness(50, 0, 0, 0);
                        baleNineLineOne.Visibility = Visibility.Visible;
                        break;

                    case 10:
                        baleTenLineOne.Margin = new Thickness(55, 0, 0, 0);
                        baleTenLineOne.Visibility = Visibility.Visible;
                        break;
                }
            }

        }
        internal void PutBaleOnConveyorTwo(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                switch (baleList[i])
                {
                    case 0:
                        PutDefaultBaleOnPressConveyor(baleList);
                        break;

                    case 1:
                        baleOnePress.Margin = new Thickness(BaleTenPress, 0, 0, 0);
                        baleOnePress.Visibility = Visibility.Visible;
                        break;

                    case 2:
                        baleTwoPress.Margin = new Thickness(BaleNinePress, 0, 0, 0);
                        baleTwoPress.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        baleThreePress.Margin = new Thickness(BaleEightPress, 0, 0, 0);
                        baleThreePress.Visibility = Visibility.Visible;
                        break;

                    case 4:
                        baleFourPress.Margin = new Thickness(BaleSevenPress, 0, 0, 0);
                        baleFourPress.Visibility = Visibility.Visible;
                        break;

                    case 5:
                        baleFivePress.Margin = new Thickness(BaleSixPress, 0, 0, 0);
                        baleFivePress.Visibility = Visibility.Visible;
                        break;

                    case 6:
                        baleSixPress.Margin = new Thickness(BaleFivePress, 0, 0, 0);
                        baleSixPress.Visibility = Visibility.Visible;
                        break;

                    case 7:
                        baleSevenPress.Margin = new Thickness(BaleFourPress, 0, 0, 0);
                        baleSevenPress.Visibility = Visibility.Visible;
                        break;

                    case 8:
                        baleEightPress.Margin = new Thickness(BaleThreePress, 0, 0, 0);
                        baleEightPress.Visibility = Visibility.Visible;
                        break;
                    case 9:
                        baleNinePress.Margin = new Thickness(BaleTwoPress, 0, 0, 0);
                        baleNinePress.Visibility = Visibility.Visible;
                        break;

                    case 10:
                        baleTenPress.Margin = new Thickness(BaleOnePress, 0, 0, 0);
                        baleTenPress.Visibility = Visibility.Visible;
                        break;
                }
            }
        }
        internal void PutBaleOnConveyorThree(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                switch (baleList[i])
                {
                    case 0:
                        PutDefaultBaleOnMrkConveyor(baleList);
                        break;

                    case 1:
                        baleOneMrk.Margin = new Thickness(BaleTenMkr, 0, 0, 0);
                        baleOneMrk.Visibility = Visibility.Visible;
                        break;

                    case 2:
                        baleTwoMrk.Margin = new Thickness(BaleNineMkr, 0, 0, 0);
                        baleTwoMrk.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        baleThreeMrk.Margin = new Thickness(BaleEightMkr, 0, 0, 0);
                        baleThreeMrk.Visibility = Visibility.Visible;
                        break;

                    case 4:
                        baleFourMrk.Margin = new Thickness(BaleSevenMkr, 0, 0, 0);
                        baleFourMrk.Visibility = Visibility.Visible;
                        break;

                    case 5:
                        baleFiveMrk.Margin = new Thickness(BaleSixMkr, 0, 0, 0);
                        baleFiveMrk.Visibility = Visibility.Visible;
                        break;

                    case 6:
                        baleSixMrk.Margin = new Thickness(BaleFiveMkr, 0, 0, 0);
                        baleSixMrk.Visibility = Visibility.Visible;
                        break;

                    case 7:
                        baleSevenMrk.Margin = new Thickness(BaleFourMkr, 0, 0, 0);
                        baleSevenMrk.Visibility = Visibility.Visible;
                        break;

                    case 8:
                        baleEightMrk.Margin = new Thickness(BaleThreeMkr, 0, 0, 0);
                        baleEightMrk.Visibility = Visibility.Visible;
                        break;
                    case 9:
                        baleNineLineOne.Margin = new Thickness(BaleTwoMkr, 0, 0, 0);
                        baleNineLineOne.Visibility = Visibility.Visible;
                        break;

                    case 10:
                        baleTenLineOne.Margin = new Thickness(BaleOneMkr, 0, 0, 0);
                        baleTenLineOne.Visibility = Visibility.Visible;
                        break;
                }
            }
        }



        internal void DropBaleLineOne(int nBale)
        {
           
           // ClsSerilog.LogMessage(ClsSerilog.Info, $"DropBaleLineOne -> { nBale}");

            switch (nBale)
            {
                case 1:
                    baleOneLineOne.Visibility = Visibility.Visible;
                    break;

                case 2:
                    baleOneLineOne.Visibility = Visibility.Visible;
                    baleTwoLineOne.Visibility = Visibility.Visible;
                    break;
                case 3:
                    baleOneLineOne.Visibility = Visibility.Visible;
                    baleTwoLineOne.Visibility = Visibility.Visible;
                    baleThreeLineOne.Visibility = Visibility.Visible;
                    break;

                case 4:
                    baleOneLineOne.Visibility = Visibility.Visible;
                    baleTwoLineOne.Visibility = Visibility.Visible;
                    baleThreeLineOne.Visibility = Visibility.Visible;
                    baleFourLineOne.Visibility = Visibility.Visible;
                    break;

                case 5:
                    baleOneLineOne.Visibility = Visibility.Visible;
                    baleTwoLineOne.Visibility = Visibility.Visible;
                    baleThreeLineOne.Visibility = Visibility.Visible;
                    baleFourLineOne.Visibility = Visibility.Visible;
                    baleFiveLineOne.Visibility = Visibility.Visible;
                    break;

                case 6:
                    baleOneLineOne.Visibility = Visibility.Visible;
                    baleTwoLineOne.Visibility = Visibility.Visible;
                    baleThreeLineOne.Visibility = Visibility.Visible;
                    baleFourLineOne.Visibility = Visibility.Visible;
                    baleFiveLineOne.Visibility = Visibility.Visible;
                    baleSixLineOne.Visibility = Visibility.Visible;
                    break;

                case 7:
                    baleOneLineOne.Visibility = Visibility.Visible;
                    baleTwoLineOne.Visibility = Visibility.Visible;
                    baleThreeLineOne.Visibility = Visibility.Visible;
                    baleFourLineOne.Visibility = Visibility.Visible;
                    baleFiveLineOne.Visibility = Visibility.Visible;
                    baleSixLineOne.Visibility = Visibility.Visible;
                    baleSevenLineOne.Visibility = Visibility.Visible;
                    break;

                case 8:
                    baleOneLineOne.Visibility = Visibility.Visible;
                    baleTwoLineOne.Visibility = Visibility.Visible;
                    baleThreeLineOne.Visibility = Visibility.Visible;
                    baleFourLineOne.Visibility = Visibility.Visible;
                    baleFiveLineOne.Visibility = Visibility.Visible;
                    baleSixLineOne.Visibility = Visibility.Visible;
                    baleSevenLineOne.Visibility = Visibility.Visible;
                    baleEightLineOne.Visibility = Visibility.Visible;
                    break;
                case 9:
                    baleOneLineOne.Visibility = Visibility.Visible;
                    baleTwoLineOne.Visibility = Visibility.Visible;
                    baleThreeLineOne.Visibility = Visibility.Visible;
                    baleFourLineOne.Visibility = Visibility.Visible;
                    baleFiveLineOne.Visibility = Visibility.Visible;
                    baleSixLineOne.Visibility = Visibility.Visible;
                    baleSevenLineOne.Visibility = Visibility.Visible;
                    baleEightLineOne.Visibility = Visibility.Visible;
                    baleNineLineOne.Visibility = Visibility.Visible;
                    break;

                case 10:
                    baleOneLineOne.Visibility = Visibility.Visible;
                    baleTwoLineOne.Visibility = Visibility.Visible;
                    baleThreeLineOne.Visibility = Visibility.Visible;
                    baleFourLineOne.Visibility = Visibility.Visible;
                    baleFiveLineOne.Visibility = Visibility.Visible;
                    baleSixLineOne.Visibility = Visibility.Visible;
                    baleSevenLineOne.Visibility = Visibility.Visible;
                    baleEightLineOne.Visibility = Visibility.Visible;
                    baleNineLineOne.Visibility = Visibility.Visible;
                    baleTenLineOne.Visibility = Visibility.Visible;
                    break;

            }
        }


        internal void MoveBalePassScale(int iBale)
        {
          
            DoubleAnimation myDoubleAnimation = new DoubleAnimation();

            myDoubleAnimation.From = 1;
            myDoubleAnimation.To = DefaultBaleOnePress = (int)wdCoef - 120;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(movebalespeed));
            myDoubleAnimation.AutoReverse = false;

            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Canvas.LeftProperty));
            myStoryboard.Children.Add(myDoubleAnimation);

            switch (iBale)
            {
                case 0:
                    
                    break;
                case 1:
                //    b1c0.Visibility = Visibility.Visible;
                    baleOneLineOne.Visibility = Visibility.Hidden;
                    Storyboard.SetTargetName(myDoubleAnimation, "b1c0");
                  //  myStoryboard.Begin(b1c0);
                    break;
                case 2:
                  //  b1c1.Visibility = Visibility.Visible;
                    baleTwoLineOne.Visibility = Visibility.Hidden;
                    Storyboard.SetTargetName(myDoubleAnimation, "b1c1");
                  //  myStoryboard.Begin(b1c1);
                    break;
                case 3:
                   // b1c2.Visibility = Visibility.Visible;
                    baleThreeLineOne.Visibility = Visibility.Hidden;
                    Storyboard.SetTargetName(myDoubleAnimation, "b1c2");
                   // myStoryboard.Begin(b1c2);
                    break;
                case 4:
                   // b1c3.Visibility = Visibility.Visible;
                    baleFourLineOne.Visibility = Visibility.Hidden;
                    Storyboard.SetTargetName(myDoubleAnimation, "b1c3");
                  //  myStoryboard.Begin(b1c3);
                    break;
                case 5:
                  //  b1c4.Visibility = Visibility.Visible;
                    baleFiveLineOne.Visibility = Visibility.Hidden;
                    Storyboard.SetTargetName(myDoubleAnimation, "b1c4");
                   // myStoryboard.Begin(b1c4);
                    break;
                case 6:
                   // b1c5.Visibility = Visibility.Visible;
                    baleSixLineOne.Visibility = Visibility.Hidden;
                    Storyboard.SetTargetName(myDoubleAnimation, "b1c5");
                   // myStoryboard.Begin(b1c5);
                    break;
                case 7:
                   // b1c6.Visibility = Visibility.Visible;
                    baleSevenLineOne.Visibility = Visibility.Hidden;
                    Storyboard.SetTargetName(myDoubleAnimation, "b1c6");
                   // myStoryboard.Begin(b1c6);
                    break;
                case 8:
                   // b1c7.Visibility = Visibility.Visible;
                    baleEightLineOne.Visibility = Visibility.Hidden;
                    Storyboard.SetTargetName(myDoubleAnimation, "b1c7");
                   // myStoryboard.Begin(b1c7);
                    break;
                case 9:
                   // b1c8.Visibility = Visibility.Visible;
                    baleNineLineOne.Visibility = Visibility.Hidden;
                    Storyboard.SetTargetName(myDoubleAnimation, "b1c8");
                   // myStoryboard.Begin(b1c8);
                    break;
                case 10:
                   // b1c9.Visibility = Visibility.Visible;
                    baleTenLineOne.Visibility = Visibility.Hidden;
                    Storyboard.SetTargetName(myDoubleAnimation, "b1c9");
                  //  myStoryboard.Begin(b1c9);
                    break;
            }


        }

        internal void MoveOneBaleA(int station, int iBale, int start, int stop, int baleStatus)
        {
            DoubleAnimation myDoubleAnimation = new DoubleAnimation();

            balestationA = station;

            myDoubleAnimation.From = start;
            myDoubleAnimation.To = stop;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(movebalespeed));
            myDoubleAnimation.AutoReverse = false;

            myDoubleAnimation.Name = "BaleOne";

            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Canvas.LeftProperty));
            myStoryboard.Children.Add(myDoubleAnimation);

            //ClsSerilog.LogMessage(ClsSerilog.Info, $"1   MoveBaleLineA -----station = {station}, iBale = {iBale}, baleStatus= {baleStatus}");

            try
            {

                switch (iBale)
                {
                    case 0:
                       
                        break;
                    case 1:
                       // b1c0.Visibility = Visibility.Visible;
                        baleOneLineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c0");
                       // myStoryboard.Begin(b1c0);
                        break;
                    case 2:
                       // b1c1.Visibility = Visibility.Visible;
                        baleTwoLineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c1");
                       // myStoryboard.Begin(b1c1);
                        break;
                    case 3:
                        //b1c2.Visibility = Visibility.Visible;
                        baleThreeLineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c2");
                        //myStoryboard.Begin(b1c2);
                        break;
                    case 4:
                       // b1c3.Visibility = Visibility.Visible;
                        baleFourLineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c3");
                      //  myStoryboard.Begin(b1c3);
                        break;
                    case 5:
                       // b1c4.Visibility = Visibility.Visible;
                        baleFiveLineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c4");
                       // myStoryboard.Begin(b1c4);
                        break;
                    case 6:
                       // b1c5.Visibility = Visibility.Visible;
                        baleSixLineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c5");
                      //  myStoryboard.Begin(b1c5);
                        break;
                    case 7:
                       // b1c6.Visibility = Visibility.Visible;
                        baleSevenLineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c6");
                       // myStoryboard.Begin(b1c6);
                        break;
                    case 8:
                       // b1c7.Visibility = Visibility.Visible;
                        baleEightLineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c7");
                       // myStoryboard.Begin(b1c7);
                        break;
                    case 9:
                       // b1c8.Visibility = Visibility.Visible;
                        baleNineLineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c8");
                      //  myStoryboard.Begin(b1c8);
                        break;
                    case 10:
                       // b1c9.Visibility = Visibility.Visible;
                        baleTenLineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c9");
                       // myStoryboard.Begin(b1c9);
                        break;


                    case 11:
                       // b1c10.Visibility = Visibility.Visible;
                        bale11LineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c10");
                       // myStoryboard.Begin(b1c10);
                        ClsSerilog.LogMessage(ClsSerilog.Info, $"1  Scale -----  MoveBaleLineA -----station = {station} iBale-> {iBale}");
                        break;

                    case 12:
                       // b1c11.Visibility = Visibility.Visible;
                        bale12LineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c11");
                       // myStoryboard.Begin(b1c11);
                        ClsSerilog.LogMessage(ClsSerilog.Info, $"1  Scale -----  MoveBaleLineA -----station = {station} iBale-> {iBale}");
                        break;

                    case 13:
                       // b1c12.Visibility = Visibility.Visible;
                        bale13LineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c12");
                       // myStoryboard.Begin(b1c12);
                        ClsSerilog.LogMessage(ClsSerilog.Info, $"1  Scale -----  MoveBaleLineA -----station = {station} iBale-> {iBale}");
                        break;

                    case 14:
                       // b1c13.Visibility = Visibility.Visible;
                        bale14LineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c13");
                       // myStoryboard.Begin(b1c13);
                        ClsSerilog.LogMessage(ClsSerilog.Info, $"1  Scale -----  MoveBaleLineA -----station = {station} iBale-> {iBale}");
                        break;
                    
                    case 15:
                       // b1c14.Visibility = Visibility.Visible;
                        bale15LineOne.Visibility = Visibility.Hidden;
                        Storyboard.SetTargetName(myDoubleAnimation, "b1c14");
                       // myStoryboard.Begin(b1c14);
                        ClsSerilog.LogMessage(ClsSerilog.Info, $"1  Scale -----  MoveBaleLineA -----station = {station} iBale-> {iBale}");
                        break;

                    default:
                     // //  Storyboard.SetTargetName(myDoubleAnimation, "b1c0");
                     //   b1c0.Visibility = Visibility;

                      //  myStoryboard.Begin(b1c0);
                        break;
                }
                myStoryboard.Children.Clear();
                myDoubleAnimation = null;
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in MoveOneBaleA -> { ex.Message}");
                MessageBox.Show($"ERROR in MoveOneBaleA {ex.Message}");
            }
        }

      
        private void MoveGostBaleOne(int station, int iBale, int baleStatus, DoubleAnimation myDoubleAnimation)
        {
            ClsSerilog.LogMessage(ClsSerilog.Info, $" MoveGostBaleOne -----station = {station} iBale-> {iBale} baleStatus= {baleStatus}");

           // b1c10.Visibility = Visibility.Visible;
            bale11LineOne.Visibility = Visibility.Hidden;
            Storyboard.SetTargetName(myDoubleAnimation, "b1c10");
           // myStoryboard.Begin(b1c10);   
        }

        private void MoveGostBaleTwo(int station, int iBale, int baleStatus, DoubleAnimation myDoubleAnimation)
        {
            ClsSerilog.LogMessage(ClsSerilog.Info, $" MoveGostBaleTwo -----station = {station} iBale-> {iBale} baleStatus= {baleStatus}");

            //b1c11.Visibility = Visibility.Visible;
            bale12LineOne.Visibility = Visibility.Hidden;
            Storyboard.SetTargetName(myDoubleAnimation, "b1c11");
           // myStoryboard.Begin(b1c11);
        }


        internal void RemoveBaleLineOne(int baleNumber)
        {
            /*
            switch (baleNumber)
            {
                case 1:
                    b1c0.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    b1c1.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    b1c2.Visibility = Visibility.Hidden;
                    break;
                case 4:
                    b1c3.Visibility = Visibility.Hidden;
                    break;
                case 5:
                    b1c4.Visibility = Visibility.Hidden;
                    break;
                case 6:
                    b1c5.Visibility = Visibility.Hidden;
                    break;
                case 7:
                    b1c6.Visibility = Visibility.Hidden;
                    break;
                case 8:
                    b1c7.Visibility = Visibility.Hidden;
                    break;
                case 9:
                    b1c8.Visibility = Visibility.Hidden;
                    break;
                case 10:
                    b1c9.Visibility = Visibility.Hidden;
                    break;

                case 11:
                    b1c10.Visibility = Visibility.Hidden;
                    break;
                case 12:
                    b1c11.Visibility = Visibility.Hidden;
                    break;
                case 13:
                    b1c12.Visibility = Visibility.Hidden;
                    break;
                case 14:
                    b1c13.Visibility = Visibility.Hidden;
                    break;
                case 15:
                    b1c14.Visibility = Visibility.Hidden;
                    break;

            } 
            */
        }

        public void ClearConveyorOne()
        {
            baleOneLineOne.Visibility = Visibility.Hidden;
            baleTwoLineOne.Visibility = Visibility.Hidden;
            baleThreeLineOne.Visibility = Visibility.Hidden;
            baleFourLineOne.Visibility = Visibility.Hidden;
            baleFiveLineOne.Visibility = Visibility.Hidden;

            baleSixLineOne.Visibility = Visibility.Hidden;
            baleSevenLineOne.Visibility = Visibility.Hidden;
            baleEightLineOne.Visibility = Visibility.Hidden;
            baleNineLineOne.Visibility = Visibility.Hidden;
            baleTenLineOne.Visibility = Visibility.Hidden;

            defaultbaleOneScale.Visibility = Visibility.Hidden;
            defaultbaleTwoScale.Visibility = Visibility.Hidden;
            defaultbaleThreeScale.Visibility = Visibility.Hidden;
            defaultbaleFourScale.Visibility = Visibility.Hidden;
            defaultbaleFiveScale.Visibility = Visibility.Hidden;
        }

        public void ClearBaleFromApproachConveyor(List<int> baleOnScale)
        {
            baleOneLineOne.Visibility = Visibility.Hidden;
            baleTwoLineOne.Visibility = Visibility.Hidden;
            baleThreeLineOne.Visibility = Visibility.Hidden;
            baleFourLineOne.Visibility = Visibility.Hidden;
            baleFiveLineOne.Visibility = Visibility.Hidden;

            baleSixLineOne.Visibility = Visibility.Hidden;
            baleSevenLineOne.Visibility = Visibility.Hidden;
            baleEightLineOne.Visibility = Visibility.Hidden;
            baleNineLineOne.Visibility = Visibility.Hidden;
            baleTenLineOne.Visibility = Visibility.Hidden;

            defaultbaleOneScale.Visibility = Visibility.Hidden;
            defaultbaleTwoScale.Visibility = Visibility.Hidden;
            defaultbaleThreeScale.Visibility = Visibility.Hidden;
            defaultbaleFourScale.Visibility = Visibility.Hidden;
            defaultbaleFiveScale.Visibility = Visibility.Hidden; 
        }


        public void ClearConveyorTwo()
        {
            baleOnePress.Visibility = Visibility.Hidden;
            baleTwoPress.Visibility = Visibility.Hidden;
            baleThreePress.Visibility = Visibility.Hidden;
            baleFourPress.Visibility = Visibility.Hidden;
            baleFivePress.Visibility = Visibility.Hidden;

            baleSixPress.Visibility = Visibility.Hidden;
            baleSevenPress.Visibility = Visibility.Hidden;
            baleEightPress.Visibility = Visibility.Hidden;
            baleNinePress.Visibility = Visibility.Hidden;
            baleTenPress.Visibility = Visibility.Hidden;


            defaultbaleOnePress.Visibility = Visibility.Hidden;
            defaultbaleTwoPress.Visibility = Visibility.Hidden;
            defaultbaleThreePress.Visibility = Visibility.Hidden;
            defaultbaleFourPress.Visibility = Visibility.Hidden;
            defaultbaleFivePress.Visibility = Visibility.Hidden;

          //  b1c0.Visibility = Visibility.Hidden;
          //  b1c1.Visibility = Visibility.Hidden;
          //  b1c2.Visibility = Visibility.Hidden;
         //   b1c3.Visibility = Visibility.Hidden;
         //   b1c4.Visibility = Visibility.Hidden;
         //   b1c5.Visibility = Visibility.Hidden;
         //   b1c6.Visibility = Visibility.Hidden;
         //   b1c7.Visibility = Visibility.Hidden;
         //   b1c8.Visibility = Visibility.Hidden;
         //   b1c9.Visibility = Visibility.Hidden;
        //    b1c10.Visibility = Visibility.Hidden;
         //   b1c11.Visibility = Visibility.Hidden;
         //   b1c12.Visibility = Visibility.Hidden;
         //   b1c13.Visibility = Visibility.Hidden;
         //   b1c14.Visibility = Visibility.Hidden;
        }
        public void ClearConveyorThree()
        {
            baleOneMrk.Visibility = Visibility.Hidden;
            baleTwoMrk.Visibility = Visibility.Hidden;
            baleThreeMrk.Visibility = Visibility.Hidden;
            baleFourMrk.Visibility = Visibility.Hidden;
            baleFiveMrk.Visibility = Visibility.Hidden;
            baleSixMrk.Visibility = Visibility.Hidden;
            baleSevenMrk.Visibility = Visibility.Hidden;
            baleEightMrk.Visibility = Visibility.Hidden;
            baleNineMrk.Visibility = Visibility.Hidden;
            baleTenMrk.Visibility = Visibility.Hidden;

            defaultbaleOneMrk.Visibility = Visibility.Hidden;
            defaultbaleTwoMrk.Visibility = Visibility.Hidden;
            defaultbaleThreeMrk.Visibility = Visibility.Hidden;
            defaultbaleFourMrk.Visibility = Visibility.Hidden;
            defaultbaleFiveMrk.Visibility = Visibility.Hidden;
        }


        public void ClearAllBales()
        {
            /*
            b1c0.Visibility = Visibility.Hidden;
            b1c1.Visibility = Visibility.Hidden;
            b1c2.Visibility = Visibility.Hidden;
            b1c3.Visibility = Visibility.Hidden;
            b1c4.Visibility = Visibility.Hidden;
            b1c5.Visibility = Visibility.Hidden;
            b1c6.Visibility = Visibility.Hidden;
            b1c7.Visibility = Visibility.Hidden;
            b1c8.Visibility = Visibility.Hidden;
            b1c9.Visibility = Visibility.Hidden;
            b1c10.Visibility = Visibility.Hidden;
            b1c11.Visibility = Visibility.Hidden;
            b1c12.Visibility = Visibility.Hidden;
            b1c13.Visibility = Visibility.Hidden;
            b1c14.Visibility = Visibility.Hidden;
            */
            baleOneLineOne.Visibility = Visibility.Hidden;
            baleTwoLineOne.Visibility = Visibility.Hidden;
            baleThreeLineOne.Visibility = Visibility.Hidden;
            baleFourLineOne.Visibility = Visibility.Hidden;
            baleFiveLineOne.Visibility = Visibility.Hidden;
            baleSixLineOne.Visibility = Visibility.Hidden;
            baleSevenLineOne.Visibility = Visibility.Hidden;
            baleEightLineOne.Visibility = Visibility.Hidden;
            baleNineLineOne.Visibility = Visibility.Hidden;
            baleTenLineOne.Visibility = Visibility.Hidden;
            bale11LineOne.Visibility = Visibility.Hidden;
            bale12LineOne.Visibility = Visibility.Hidden;
            bale13LineOne.Visibility = Visibility.Hidden;
            bale14LineOne.Visibility = Visibility.Hidden;
            bale15LineOne.Visibility = Visibility.Hidden;

            baleOnePress.Visibility = Visibility.Hidden;
            baleTwoPress.Visibility = Visibility.Hidden;
            baleThreePress.Visibility = Visibility.Hidden;
            baleFourPress.Visibility = Visibility.Hidden;
            baleFivePress.Visibility = Visibility.Hidden;

            baleSixPress.Visibility = Visibility.Hidden;
            baleSevenPress.Visibility = Visibility.Hidden;
            baleEightPress.Visibility = Visibility.Hidden;
            baleNinePress.Visibility = Visibility.Hidden;
            baleTenPress.Visibility = Visibility.Hidden;

            baleOneMrk.Visibility = Visibility.Hidden;
            baleTwoMrk.Visibility = Visibility.Hidden;
            baleThreeMrk.Visibility = Visibility.Hidden;
            baleFourMrk.Visibility = Visibility.Hidden;
            baleFiveMrk.Visibility = Visibility.Hidden;
            baleSixMrk.Visibility = Visibility.Hidden;
            baleSevenMrk.Visibility = Visibility.Hidden;
            baleEightMrk.Visibility = Visibility.Hidden;
            baleNineMrk.Visibility = Visibility.Hidden;
            baleTenMrk.Visibility = Visibility.Hidden;



            defaultbaleOneScale.Visibility = Visibility.Hidden;
            defaultbaleTwoScale.Visibility = Visibility.Hidden;
            defaultbaleThreeScale.Visibility = Visibility.Hidden;
            defaultbaleFourScale.Visibility = Visibility.Hidden;
            defaultbaleFiveScale.Visibility = Visibility.Hidden;

            defaultbaleOnePress.Visibility = Visibility.Hidden;
            defaultbaleTwoPress.Visibility = Visibility.Hidden;
            defaultbaleThreePress.Visibility = Visibility.Hidden;
            defaultbaleFourPress.Visibility = Visibility.Hidden;
            defaultbaleFivePress.Visibility = Visibility.Hidden;

            defaultbaleOneMrk.Visibility = Visibility.Hidden;
            defaultbaleTwoMrk.Visibility = Visibility.Hidden;
            defaultbaleThreeMrk.Visibility = Visibility.Hidden;
            defaultbaleFourMrk.Visibility = Visibility.Hidden;
            defaultbaleFiveMrk.Visibility = Visibility.Hidden;
        }

        
    }
}
