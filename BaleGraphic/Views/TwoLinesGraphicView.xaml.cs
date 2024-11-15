using _8100UI.Services;
using BaleGraphic.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BaleGraphic.Views
{
    /// <summary>
    /// Interaction logic for TwoLinesGraphicView.xaml
    /// </summary>
    public partial class TwoLinesGraphicView : UserControl
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;

        public static TwoLinesGraphicView GraphicTwoLinesWindow;

        private double wdCoef = 0.0;
        private double ScaleCoef = 0.0;

        private double mkCoef = 0.0;


        //Scale Conveyor Line One
        int BaleOneScaleL1;
        int BaleTwoScaleL1;
        int BaleThreeScaleL1;
        int BaleFourScaleL1;
        int BaleFiveScaleL1;
        int BaleSixScaleL1;
        int BaleSevenScaleL1;
        int BaleEightScaleL1;
        int BaleNineScaleL1;
        int BaleTenScaleL1;
        int BaleElevenScaleL1;
        int BaleTwelveScaleL1;
        int BaleThirteenScaleL1;
        int BaleFourteenScaleL1;
        int BaleFifteenScaleL1;


        //Scale Conveyor Line Two
        int BaleOneScaleL2;
        int BaleTwoScaleL2;
        int BaleThreeScaleL2;
        int BaleFourScaleL2;
        int BaleFiveScaleL2;
        int BaleSixScaleL2;
        int BaleSevenScaleL2;
        int BaleEightScaleL2;
        int BaleNineScaleL2;
        int BaleTenScaleL2;
        int BaleElevenScaleL2;
        int BaleTwelveScaleL2;
        int BaleThirteenScaleL2;
        int BaleFourteenScaleL2;
        int BaleFifteenScaleL2;

        //Default  Scale
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


        //small Window @ Marker
        int DefaultBaleOneMkr;
        int DefaultBaleTwoMkr;
        int DefaultBaleThreeMkr;
        int DefaultpressFourMkr;
        int DefaultpressFiveMkr;


        public TwoLinesGraphicView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this._eventAggregator = eventAggregator;
            GraphicTwoLinesWindow = this;
            this.DataContext = new TwoLinesGraphicViewModel(_eventAggregator);

            //Line 1
            ClearAllBalesLineOne();
            //Line 2
            ClearAllBalesLineTwo();
           
        }

        private void ClearAllBalesLineOne()
        {
            ClearBalesOnScaleConveyorLineOne();
            ClearAllBalePressConveyorLineOne();
            ClearAllBaleMrkConveyorLineOne();
        }

        private void ClearAllBalesLineTwo()
        {
            ClearAllBalesOnScaleConveyorLineTwo();
            ClearAllBalePressConveyorLineTwo();
            ClearAllBalesMrkConveyorLineTwo();
        }

       

        public void ClearBalesOnScaleConveyorLineOne()
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
            baleElevenLineOne.Visibility = Visibility.Hidden;
            baleTwelveLineOne.Visibility = Visibility.Hidden;
            baleThirteenLineOne.Visibility = Visibility.Hidden;
            baleFourteenLineOne.Visibility = Visibility.Hidden;
            baleFifteenLineOne.Visibility = Visibility.Hidden;

            defaultbaleOneScaleLnOne.Visibility = Visibility.Hidden;
            defaultbaleTwoScaleLnOne.Visibility = Visibility.Hidden;
            defaultbaleThreeScaleLnOne.Visibility = Visibility.Hidden;
            defaultbaleFourScaleLnOne.Visibility = Visibility.Hidden;
            defaultbaleFiveScaleLnOne.Visibility = Visibility.Hidden;
        }

        public void ClearAllBalePressConveyorLineOne()
        {
            baleOneLnOnePress.Visibility = Visibility.Hidden;
            baleTwoLnOnePress.Visibility = Visibility.Hidden;
            baleThreeLnOnePress.Visibility = Visibility.Hidden;
            baleFourLnOnePress.Visibility = Visibility.Hidden;
            baleFiveLnOnePress.Visibility = Visibility.Hidden;
            baleSixLnOnePress.Visibility = Visibility.Hidden;
            baleSevenLnOnePress.Visibility = Visibility.Hidden;
            baleEightLnOnePress.Visibility = Visibility.Hidden;

            defaultbaleOnePressLnOne.Visibility = Visibility.Hidden;
            defaultbaleTwoPressLnOne.Visibility = Visibility.Hidden;
            defaultbaleThreePressLnOne.Visibility = Visibility.Hidden;
            defaultbaleFourPressLnOne.Visibility = Visibility.Hidden;
            defaultbaleFivePressLnOne.Visibility = Visibility.Hidden;
        }

        internal void ClearAllBaleMrkConveyorLineOne()
        {
            baleOneLnOneMrk.Visibility = Visibility.Hidden;
            baleTwoLnOneMrk.Visibility = Visibility.Hidden;
            baleThreeLnOneMrk.Visibility = Visibility.Hidden;
            baleFourLnOneMrk.Visibility = Visibility.Hidden;
            baleFiveLnOneMrk.Visibility = Visibility.Hidden;
            baleSixLnOneMrk.Visibility = Visibility.Hidden;
            baleSevenLnOneMrk.Visibility = Visibility.Hidden;
            baleEightLnOneMrk.Visibility = Visibility.Hidden;

            defaultbaleOneMrkLnOne.Visibility = Visibility.Hidden;
            defaultbaleTwoMrkLnOne.Visibility = Visibility.Hidden;
            defaultbaleThreeMrkLnOne.Visibility = Visibility.Hidden;
            defaultbaleFourMrkLnOne.Visibility = Visibility.Hidden;
            defaultbaleFiveMrkLnOne.Visibility = Visibility.Hidden;
        }

        internal void ClearAllBalesOnScaleConveyorLineTwo()
        {
            baleOneLineTwo.Visibility = Visibility.Hidden;
            baleTwoLineTwo.Visibility = Visibility.Hidden;
            baleThreeLineTwo.Visibility = Visibility.Hidden;
            baleFourLineTwo.Visibility = Visibility.Hidden;
            baleFiveLineTwo.Visibility = Visibility.Hidden;
            baleSixLineTwo.Visibility = Visibility.Hidden;
            baleSevenLineTwo.Visibility = Visibility.Hidden;
            baleEightLineTwo.Visibility = Visibility.Hidden;
            baleNineLineTwo.Visibility = Visibility.Hidden;
            baleTenLineTwo.Visibility = Visibility.Hidden;
            baleElevenLineTwo.Visibility = Visibility.Hidden;
            baleTwelveLineTwo.Visibility = Visibility.Hidden;
            baleThirteenLineTwo.Visibility = Visibility.Hidden;
            baleFourteenLineTwo.Visibility = Visibility.Hidden;
            baleFifteenLineTwo.Visibility = Visibility.Hidden;

            defaultbaleOneScaleLnTwo.Visibility = Visibility.Hidden;
            defaultbaleTwoScaleLnTwo.Visibility = Visibility.Hidden;
            defaultbaleThreeScaleLnTwo.Visibility = Visibility.Hidden;
            defaultbaleFourScaleLnTwo.Visibility = Visibility.Hidden;
            defaultbaleFiveScaleLnTwo.Visibility = Visibility.Hidden;
        }

        internal void ClearAllBalePressConveyorLineTwo()
        {
            pressbaleOneLineTwo.Visibility = Visibility.Hidden;
            pressbaleTwoLineTwo.Visibility = Visibility.Hidden;
            pressbaleThreeLineTwo.Visibility = Visibility.Hidden;
            pressbaleFourLineTwo.Visibility = Visibility.Hidden;
            pressbaleFiveLineTwo.Visibility = Visibility.Hidden;
            pressbaleSixLineTwo.Visibility = Visibility.Hidden;
            pressbaleSevenLineTwo.Visibility = Visibility.Hidden;
            pressbaleEightLineTwo.Visibility = Visibility.Hidden;

            defaultbaleOnePressLnTwo.Visibility = Visibility.Hidden;
            defaultbaleTwoPressLnTwo.Visibility = Visibility.Hidden;
            defaultbaleThreePressLnTwo.Visibility = Visibility.Hidden;
            defaultbaleFourPressLnTwo.Visibility = Visibility.Hidden;
            defaultbaleFivePressLnTwo.Visibility = Visibility.Hidden;
        }

        internal void ClearAllBalesMrkConveyorLineTwo()
        {
            mrkbaleOneLineTwo.Visibility = Visibility.Hidden;
            mrkbaleTwoLineTwo.Visibility = Visibility.Hidden;
            mrkbaleThreeLineTwo.Visibility = Visibility.Hidden;
            mrkbaleFourLineTwo.Visibility = Visibility.Hidden;
            mrkbaleFiveLineTwo.Visibility = Visibility.Hidden;
            mrkbaleSixLineTwo.Visibility = Visibility.Hidden;
            mrkbaleSevenLineTwo.Visibility = Visibility.Hidden;
            mrkbaleEightLineTwo.Visibility = Visibility.Hidden;

            defaultbaleOneMrkLnTwo.Visibility = Visibility.Hidden;
            defaultbaleTwoMrkLnTwo.Visibility = Visibility.Hidden;
            defaultbaleThreeMrkLnTwo.Visibility = Visibility.Hidden;
            defaultbaleFourMrkLnTwo.Visibility = Visibility.Hidden;
            defaultbaleFiveMrkLnTwo.Visibility = Visibility.Hidden;

        }

        public void PutBaleOnScaleConveyorLineOne(List<int> baleList)
        {
            // ClsSerilog.LogMessage(ClsSerilog.Info, $"PutBaleOnScale ConveyorLineOne");

            if (baleList.Count > 0)
            {
                for (int i = 0; i < baleList.Count; i++)
                {

                    switch (baleList[i])
                    {
                        case 0:
                            PutDefaultBaleOnScaleLineOne(baleList);
                            break;
                        case 1:
                            baleOneLineOne.Margin = new Thickness(BaleOneScaleL1, 0, 0, 0);
                            baleOneLineOne.Visibility = Visibility.Visible;
                            break;

                        case 2:
                            baleTwoLineOne.Margin = new Thickness(BaleTwoScaleL1, 0, 0, 0);
                            baleTwoLineOne.Visibility = Visibility.Visible;
                            break;
                        case 3:
                            baleThreeLineOne.Margin = new Thickness(BaleThreeScaleL1, 0, 0, 0);
                            baleThreeLineOne.Visibility = Visibility.Visible;
                            break;

                        case 4:
                            baleFourLineOne.Margin = new Thickness(BaleFourScaleL1, 0, 0, 0);
                            baleFourLineOne.Visibility = Visibility.Visible;
                            break;

                        case 5:
                            baleFiveLineOne.Margin = new Thickness(BaleFiveScaleL1, 0, 0, 0);
                            baleFiveLineOne.Visibility = Visibility.Visible;
                            break;

                        case 6:
                            baleSixLineOne.Margin = new Thickness(BaleSixScaleL1, 0, 0, 0);
                            baleSixLineOne.Visibility = Visibility.Visible;
                            break;

                        case 7:
                            baleSevenLineOne.Margin = new Thickness(BaleSevenScaleL1, 0, 0, 0);
                            baleSevenLineOne.Visibility = Visibility.Visible;
                            break;

                        case 8:
                            baleEightLineOne.Margin = new Thickness(BaleEightScaleL1, 0, 0, 0);
                            baleEightLineOne.Visibility = Visibility.Visible;
                            break;
                        case 9:
                            baleNineLineOne.Margin = new Thickness(BaleNineScaleL1, 0, 0, 0);
                            baleNineLineOne.Visibility = Visibility.Visible;
                            break;

                        case 10:
                            baleTenLineOne.Margin = new Thickness(BaleTenScaleL1, 0, 0, 0);
                            baleTenLineOne.Visibility = Visibility.Visible;
                            break;
                        case 11:
                            baleElevenLineOne.Margin = new Thickness(BaleElevenScaleL1, 0, 0, 0);
                            baleElevenLineOne.Visibility = Visibility.Visible;
                            break;
                        case 12:
                            baleTwelveLineOne.Margin = new Thickness(BaleTwelveScaleL1, 0, 0, 0);
                            baleTwelveLineOne.Visibility = Visibility.Visible;
                            break;
                        case 13:
                            baleThirteenLineOne.Margin = new Thickness(BaleThirteenScaleL1, 0, 0, 0);
                            baleThirteenLineOne.Visibility = Visibility.Visible;
                            break;
                        case 14:
                            baleFourteenLineOne.Margin = new Thickness(BaleFourteenScaleL1, 0, 0, 0);
                            baleFourteenLineOne.Visibility = Visibility.Visible;
                            break;
                        case 15:
                            baleFifteenLineOne.Margin = new Thickness(BaleFifteenScaleL1, 0, 0, 0);
                            baleFifteenLineOne.Visibility = Visibility.Visible;
                            break;
                    }
                }
            }
        }

        private void PutDefaultBaleOnScaleLineOne(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        if (baleList[i] == 0)
                        {
                            defaultbaleOneScaleLnOne.Margin = new Thickness(DefaultBaleOneScale, 0, 0, 0);
                            defaultbaleOneScaleLnOne.Visibility = Visibility.Visible;
                        }
                        break;

                    case 1:
                        if (baleList[i] == 0)
                        {
                            defaultbaleTwoScaleLnOne.Margin = new Thickness(DefaultBaleTwoScale, 0, 0, 0);
                            defaultbaleTwoScaleLnOne.Visibility = Visibility.Visible;
                        }
                        break;

                    case 2:
                        if (baleList[i] == 0)
                        {
                            defaultbaleThreeScaleLnOne.Margin = new Thickness(DefaultBaleThreeScale, 0, 0, 0);
                            defaultbaleThreeScaleLnOne.Visibility = Visibility.Visible;
                        }
                        break;

                    case 3:
                        if (baleList[i] == 0)
                        {
                            defaultbaleFourScaleLnOne.Margin = new Thickness(DefaultBaleFourScale, 0, 0, 0);
                            defaultbaleFourScaleLnOne.Visibility = Visibility.Visible;
                        }
                        break;

                    case 4:
                        {
                            defaultbaleFiveScaleLnOne.Margin = new Thickness(DefaultBaleFiveScale, 0, 0, 0);
                            defaultbaleFiveScaleLnOne.Visibility = Visibility.Visible;
                        }
                        break;
                }
            }
        }

        internal void PutBaleOnPressConveyorLineOne(List<int> baleList)
        {
            //ClsSerilog.LogMessage(ClsSerilog.Info, $"PutBaleOnPress ConveyorLineOne");

            for (int i = 0; i < baleList.Count; i++)
            {
                switch (baleList[i])
                {
                    case 0:
                        PutDefaultBaleOnPressConveyorLineOne(baleList);
                        break;

                    case 1:
                        baleOneLnOnePress.Margin = new Thickness(BaleOnePress, 0, 0, 0);
                        baleTwoLnOnePress.Visibility = Visibility.Visible;
                        break;

                    case 2:
                        baleTwoLnOnePress.Margin = new Thickness(BaleTwoPress, 0, 0, 0);
                        baleTwoLnOnePress.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        baleThreeLnOnePress.Margin = new Thickness(BaleThreePress, 0, 0, 0);
                        baleThreeLnOnePress.Visibility = Visibility.Visible;
                        break;

                    case 4:
                        baleFourLnOnePress.Margin = new Thickness(BaleFourPress, 0, 0, 0);
                        baleFourLnOnePress.Visibility = Visibility.Visible;
                        break;

                    case 5:
                        baleFiveLnOnePress.Margin = new Thickness(BaleFivePress, 0, 0, 0);
                        baleFiveLnOnePress.Visibility = Visibility.Visible;
                        break;

                    case 6:
                        baleSixLnOnePress.Margin = new Thickness(BaleSixPress, 0, 0, 0);
                        baleSixLnOnePress.Visibility = Visibility.Visible;
                        break;

                    case 7:
                        baleSevenLnOnePress.Margin = new Thickness(BaleSevenPress, 0, 0, 0);
                        baleSevenLnOnePress.Visibility = Visibility.Visible;
                        break;

                    case 8:
                        baleEightLnOnePress.Margin = new Thickness(BaleEightPress, 0, 0, 0);
                        baleEightLnOnePress.Visibility = Visibility.Visible;
                        break;

                }
            }
        }

        private void PutDefaultBaleOnPressConveyorLineOne(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        if (baleList[i] == 0)
                        {
                            defaultbaleOnePressLnOne.Margin = new Thickness(DefaultBaleOnePress, 0, 0, 0);
                            defaultbaleOnePressLnOne.Visibility = Visibility.Visible;
                        }
                        break;

                    case 1:
                        if (baleList[i] == 0)
                        {
                            defaultbaleTwoPressLnOne.Margin = new Thickness(DefaultBaleTwoPress, 0, 0, 0);
                            defaultbaleTwoPressLnOne.Visibility = Visibility.Visible;
                        }
                        break;

                    case 2:
                        if (baleList[i] == 0)
                        {
                            defaultbaleThreePressLnOne.Margin = new Thickness(DefaultBaleThreePress, 0, 0, 0);
                            defaultbaleThreePressLnOne.Visibility = Visibility.Visible;
                        }
                        break;

                    case 3:
                        if (baleList[i] == 0)
                        {
                            defaultbaleFourPressLnOne.Margin = new Thickness(DefaultpressFourPress, 0, 0, 0);
                            defaultbaleFourPressLnOne.Visibility = Visibility.Visible;
                        }
                        break;

                    case 4:
                        {
                            defaultbaleFivePressLnOne.Margin = new Thickness(DefaultpressFivePress, 0, 0, 0);
                            defaultbaleFivePressLnOne.Visibility = Visibility.Visible;
                        }
                        break;
                }
            }
        }

        internal void PutBaleOnMarkerConveyorLineOne(List<int> baleList)
        {
            //ClsSerilog.LogMessage(ClsSerilog.Info, $"PutBaleOnMarker ConveyorLineOne");

            for (int i = 0; i < baleList.Count; i++)
            {
                switch (baleList[i])
                {
                    case 0:
                        PutDefaultBaleOnMrkConveyorLineOne(baleList);
                        break;

                    case 1:
                        baleOneLnOneMrk.Margin = new Thickness(BaleOneMkr, 0, 0, 0);
                        baleOneLnOneMrk.Visibility = Visibility.Visible;
                        break;

                    case 2:
                        baleTwoLnOneMrk.Margin = new Thickness(BaleTwoMkr, 0, 0, 0);
                        baleTwoLnOneMrk.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        baleThreeLnOneMrk.Margin = new Thickness(BaleThreeMkr, 0, 0, 0);
                        baleThreeLnOneMrk.Visibility = Visibility.Visible;
                        break;

                    case 4:
                        baleFourLnOneMrk.Margin = new Thickness(BaleFourMkr, 0, 0, 0);
                        baleFourLnOneMrk.Visibility = Visibility.Visible;
                        break;

                    case 5:
                        baleFiveLnOneMrk.Margin = new Thickness(BaleFiveMkr, 0, 0, 0);
                        baleFiveLnOneMrk.Visibility = Visibility.Visible;
                        break;

                    case 6:
                        baleSixLnOneMrk.Margin = new Thickness(BaleSixMkr, 0, 0, 0);
                        baleSixLnOneMrk.Visibility = Visibility.Visible;
                        break;

                    case 7:
                        baleSevenLnOneMrk.Margin = new Thickness(BaleSevenMkr, 0, 0, 0);
                        baleSevenLnOneMrk.Visibility = Visibility.Visible;
                        break;

                    case 8:
                        baleEightLnOneMrk.Margin = new Thickness(BaleEightMkr, 0, 0, 0);
                        baleEightLnOneMrk.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void PutDefaultBaleOnMrkConveyorLineOne(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        if (baleList[i] == 0)
                        {
                            defaultbaleOneMrkLnOne.Margin = new Thickness(DefaultBaleOneMkr, 0, 0, 0);
                            defaultbaleOneMrkLnOne.Visibility = Visibility.Visible;
                        }
                        break;

                    case 1:
                        if (baleList[i] == 0)
                        {
                            defaultbaleTwoMrkLnOne.Margin = new Thickness(DefaultBaleTwoMkr, 0, 0, 0);
                            defaultbaleTwoMrkLnOne.Visibility = Visibility.Visible;
                        }
                        break;

                    case 2:
                        if (baleList[i] == 0)
                        {
                            defaultbaleThreeMrkLnOne.Margin = new Thickness(DefaultBaleThreeMkr, 0, 0, 0);
                            defaultbaleThreeMrkLnOne.Visibility = Visibility.Visible;
                        }
                        break;

                    case 3:
                        if (baleList[i] == 0)
                        {
                            defaultbaleFourMrkLnOne.Margin = new Thickness(DefaultpressFourMkr, 0, 0, 0);
                            defaultbaleFourMrkLnOne.Visibility = Visibility.Visible;
                        }
                        break;

                    case 4:
                        {
                            defaultbaleFiveMrkLnOne.Margin = new Thickness(DefaultpressFiveMkr, 0, 0, 0);
                            defaultbaleFiveMrkLnOne.Visibility = Visibility.Visible;
                        }
                        break;
                }
            }
        }


        public void PutBaleOnScaleConveyorLineTwo(List<int> baleList)
        {
            // ClsSerilog.LogMessage(ClsSerilog.Info, $"PutBaleOnScale ConveyorLineOne");

            if (baleList.Count > 0)
            {
                for (int i = 0; i < baleList.Count; i++)
                {

                    switch (baleList[i])
                    {
                        case 0:
                            PutDefaultBaleOnScaleLineTwo(baleList);
                            break;
                        case 1:
                            baleOneLineTwo.Margin = new Thickness(BaleOneScaleL2, 0, 0, 0);
                            baleOneLineTwo.Visibility = Visibility.Visible;
                            break;

                        case 2:
                            baleTwoLineTwo.Margin = new Thickness(BaleTwoScaleL2, 0, 0, 0);
                            baleTwoLineTwo.Visibility = Visibility.Visible;
                            break;
                        case 3:
                            baleThreeLineTwo.Margin = new Thickness(BaleThreeScaleL2, 0, 0, 0);
                            baleThreeLineTwo.Visibility = Visibility.Visible;
                            break;

                        case 4:
                            baleFourLineTwo.Margin = new Thickness(BaleFourScaleL2, 0, 0, 0);
                            baleFourLineTwo.Visibility = Visibility.Visible;
                            break;

                        case 5:
                            baleFiveLineTwo.Margin = new Thickness(BaleFiveScaleL2, 0, 0, 0);
                            baleFiveLineTwo.Visibility = Visibility.Visible;
                            break;

                        case 6:
                            baleSixLineTwo.Margin = new Thickness(BaleSixScaleL2, 0, 0, 0);
                            baleSixLineTwo.Visibility = Visibility.Visible;
                            break;

                        case 7:
                            baleSevenLineTwo.Margin = new Thickness(BaleSevenScaleL2, 0, 0, 0);
                            baleSevenLineTwo.Visibility = Visibility.Visible;
                            break;

                        case 8:
                            baleEightLineTwo.Margin = new Thickness(BaleEightScaleL2, 0, 0, 0);
                            baleEightLineTwo.Visibility = Visibility.Visible;
                            break;
                        case 9:
                            baleNineLineTwo.Margin = new Thickness(BaleNineScaleL2, 0, 0, 0);
                            baleNineLineTwo.Visibility = Visibility.Visible;
                            break;

                        case 10:
                            baleTenLineTwo.Margin = new Thickness(BaleTenScaleL2, 0, 0, 0);
                            baleTenLineTwo.Visibility = Visibility.Visible;
                            break;
                        case 11:
                            baleElevenLineTwo.Margin = new Thickness(BaleElevenScaleL2, 0, 0, 0);
                            baleElevenLineTwo.Visibility = Visibility.Visible;
                            break;
                        case 12:
                            baleTwelveLineTwo.Margin = new Thickness(BaleTwelveScaleL2, 0, 0, 0);
                            baleTwelveLineTwo.Visibility = Visibility.Visible;
                            break;
                        case 13:
                            baleThirteenLineTwo.Margin = new Thickness(BaleThirteenScaleL2, 0, 0, 0);
                            baleThirteenLineTwo.Visibility = Visibility.Visible;
                            break;
                        case 14:
                            baleFourteenLineTwo.Margin = new Thickness(BaleFourteenScaleL2, 0, 0, 0);
                            baleFourteenLineTwo.Visibility = Visibility.Visible;
                            break;
                        case 15:
                            baleFifteenLineTwo.Margin = new Thickness(BaleFifteenScaleL2, 0, 0, 0);
                            baleFifteenLineTwo.Visibility = Visibility.Visible;
                            break;
                    }
                }
            }
        }
        private void PutDefaultBaleOnScaleLineTwo(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        if (baleList[i] == 0)
                        {
                            defaultbaleOneScaleLnTwo.Margin = new Thickness(DefaultBaleOneScale, 0, 0, 0);
                            defaultbaleOneScaleLnTwo.Visibility = Visibility.Visible;
                        }
                        break;

                    case 1:
                        if (baleList[i] == 0)
                        {
                            defaultbaleTwoScaleLnTwo.Margin = new Thickness(DefaultBaleTwoScale, 0, 0, 0);
                            defaultbaleTwoScaleLnTwo.Visibility = Visibility.Visible;
                        }
                        break;

                    case 2:
                        if (baleList[i] == 0)
                        {
                            defaultbaleThreeScaleLnTwo.Margin = new Thickness(DefaultBaleThreeScale, 0, 0, 0);
                            defaultbaleThreeScaleLnTwo.Visibility = Visibility.Visible;
                        }
                        break;

                    case 3:
                        if (baleList[i] == 0)
                        {
                            defaultbaleFourScaleLnTwo.Margin = new Thickness(DefaultBaleFourScale, 0, 0, 0);
                            defaultbaleFourScaleLnTwo.Visibility = Visibility.Visible;
                        }
                        break;

                    case 4:
                        {
                            defaultbaleFiveScaleLnTwo.Margin = new Thickness(DefaultBaleFiveScale, 0, 0, 0);
                            defaultbaleFiveScaleLnTwo.Visibility = Visibility.Visible;
                        }
                        break;
                }
            }
        }

        internal void PutBaleOnPressConveyorLineTwo(List<int> baleList)
        {

            for (int i = 0; i < baleList.Count; i++)
            {
                //  ClsSerilog.LogMessage(ClsSerilog.Info, $"PutBaleOnPress PutBaleOnPressConveyorLineTwo  Bale => {baleList[i]}");

                switch (baleList[i])
                {
                    case 0:
                        PutDefaultBaleOnPressConveyorLineTwo(baleList);
                        break;

                    case 1:
                        pressbaleOneLineTwo.Margin = new Thickness(BaleOnePress, 0, 0, 0);
                        pressbaleOneLineTwo.Visibility = Visibility.Visible;
                        break;

                    case 2:
                        pressbaleTwoLineTwo.Margin = new Thickness(BaleTwoPress, 0, 0, 0);
                        pressbaleTwoLineTwo.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        pressbaleThreeLineTwo.Margin = new Thickness(BaleThreePress, 0, 0, 0);
                        pressbaleThreeLineTwo.Visibility = Visibility.Visible;
                        break;

                    case 4:
                        pressbaleFourLineTwo.Margin = new Thickness(BaleFourPress, 0, 0, 0);
                        pressbaleFourLineTwo.Visibility = Visibility.Visible;
                        break;

                    case 5:
                        pressbaleFiveLineTwo.Margin = new Thickness(BaleFivePress, 0, 0, 0);
                        pressbaleFiveLineTwo.Visibility = Visibility.Visible;
                        break;

                    case 6:
                        pressbaleSixLineTwo.Margin = new Thickness(BaleSixPress, 0, 0, 0);
                        pressbaleSixLineTwo.Visibility = Visibility.Visible;
                        break;

                    case 7:
                        pressbaleSevenLineTwo.Margin = new Thickness(BaleSevenPress, 0, 0, 0);
                        pressbaleSevenLineTwo.Visibility = Visibility.Visible;
                        break;

                    case 8:
                        pressbaleEightLineTwo.Margin = new Thickness(BaleEightPress, 0, 0, 0);
                        pressbaleEightLineTwo.Visibility = Visibility.Visible;
                        break;

                }
            }
        }
        private void PutDefaultBaleOnPressConveyorLineTwo(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        if (baleList[i] == 0)
                        {
                            defaultbaleOnePressLnTwo.Margin = new Thickness(DefaultBaleOnePress, 0, 0, 0);
                            defaultbaleOnePressLnTwo.Visibility = Visibility.Visible;
                        }
                        break;

                    case 1:
                        if (baleList[i] == 0)
                        {
                            defaultbaleTwoPressLnTwo.Margin = new Thickness(DefaultBaleTwoPress, 0, 0, 0);
                            defaultbaleTwoPressLnTwo.Visibility = Visibility.Visible;
                        }
                        break;

                    case 2:
                        if (baleList[i] == 0)
                        {
                            defaultbaleThreePressLnTwo.Margin = new Thickness(DefaultBaleThreePress, 0, 0, 0);
                            defaultbaleThreePressLnTwo.Visibility = Visibility.Visible;
                        }
                        break;

                    case 3:
                        if (baleList[i] == 0)
                        {
                            defaultbaleFourPressLnTwo.Margin = new Thickness(DefaultpressFourPress, 0, 0, 0);
                            defaultbaleFourPressLnTwo.Visibility = Visibility.Visible;
                        }
                        break;

                    case 4:
                        {
                            defaultbaleFivePressLnTwo.Margin = new Thickness(DefaultpressFivePress, 0, 0, 0);
                            defaultbaleFivePressLnTwo.Visibility = Visibility.Visible;
                        }
                        break;
                }
            }
        }

        internal void PutBaleOnMarkerConveyorLineTwo(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                //ClsSerilog.LogMessage(ClsSerilog.Info, $"PutBaleOnMarkerConveyorLineTwo  baleList[i] =   {baleList[i]}");

                switch (baleList[i])
                {
                    case 0:
                        PutDefaultBaleOnMrkConveyorLineTwo(baleList);
                        break;

                    case 1:
                        mrkbaleOneLineTwo.Margin = new Thickness(BaleOneMkr, 0, 0, 0);
                        mrkbaleOneLineTwo.Visibility = Visibility.Visible;
                        break;

                    case 2:
                        mrkbaleTwoLineTwo.Margin = new Thickness(BaleTwoMkr, 0, 0, 0);
                        mrkbaleTwoLineTwo.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        mrkbaleThreeLineTwo.Margin = new Thickness(BaleThreeMkr, 0, 0, 0);
                        mrkbaleThreeLineTwo.Visibility = Visibility.Visible;
                        break;

                    case 4:
                        mrkbaleFourLineTwo.Margin = new Thickness(BaleFourMkr, 0, 0, 0);
                        mrkbaleFourLineTwo.Visibility = Visibility.Visible;
                        break;

                    case 5:
                        mrkbaleFiveLineTwo.Margin = new Thickness(BaleFiveMkr, 0, 0, 0);
                        mrkbaleFiveLineTwo.Visibility = Visibility.Visible;
                        break;

                    case 6:
                        mrkbaleSixLineTwo.Margin = new Thickness(BaleSixMkr, 0, 0, 0);
                        mrkbaleSixLineTwo.Visibility = Visibility.Visible;
                        break;

                    case 7:
                        mrkbaleSevenLineTwo.Margin = new Thickness(BaleSevenMkr, 0, 0, 0);
                        mrkbaleSevenLineTwo.Visibility = Visibility.Visible;
                        break;

                    case 8:
                        mrkbaleEightLineTwo.Margin = new Thickness(BaleEightMkr, 0, 0, 0);
                        mrkbaleEightLineTwo.Visibility = Visibility.Visible;
                        break;
                }
            }
        }
        private void PutDefaultBaleOnMrkConveyorLineTwo(List<int> baleList)
        {
            for (int i = 0; i < baleList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        if (baleList[i] == 0)
                        {
                            defaultbaleOneMrkLnTwo.Margin = new Thickness(DefaultBaleOneMkr, 0, 0, 0);
                            defaultbaleOneMrkLnTwo.Visibility = Visibility.Visible;
                        }
                        break;

                    case 1:
                        if (baleList[i] == 0)
                        {
                            defaultbaleTwoMrkLnTwo.Margin = new Thickness(DefaultBaleTwoMkr, 0, 0, 0);
                            defaultbaleTwoMrkLnTwo.Visibility = Visibility.Visible;
                        }
                        break;

                    case 2:
                        if (baleList[i] == 0)
                        {
                            defaultbaleThreeMrkLnTwo.Margin = new Thickness(DefaultBaleThreeMkr, 0, 0, 0);
                            defaultbaleThreeMrkLnTwo.Visibility = Visibility.Visible;
                        }
                        break;

                    case 3:
                        if (baleList[i] == 0)
                        {
                            defaultbaleFourMrkLnTwo.Margin = new Thickness(DefaultpressFourMkr, 0, 0, 0);
                            defaultbaleFourMrkLnTwo.Visibility = Visibility.Visible;
                        }
                        break;

                    case 4:
                        {
                            defaultbaleFiveMrkLnTwo.Margin = new Thickness(DefaultpressFiveMkr, 0, 0, 0);
                            defaultbaleFiveMrkLnTwo.Visibility = Visibility.Visible;
                        }
                        break;
                }
            }
        }

        private void BaleGraphic_sizeChanged(object sender, SizeChangedEventArgs e)
        {
           // double IScreenWidth = e.NewSize.Width;

            ScaleCoef = e.NewSize.Width * .14; //.36
            wdCoef = e.NewSize.Width * .34; //.54
            mkCoef = e.NewSize.Width * .65; //.775

            //Line One
            if (ClassCommon.BaleOrderLoToHiLnOne)
            {
                BaleOneScaleL1 = (int)ScaleCoef;
                BaleTwoScaleL1 = (int)ScaleCoef - 10;
                BaleThreeScaleL1 = (int)ScaleCoef - 20;
                BaleFourScaleL1 = (int)ScaleCoef - 30;
                BaleFiveScaleL1 = (int)ScaleCoef - 40;
                BaleSixScaleL1 = (int)ScaleCoef - 50;
                BaleSevenScaleL1 = (int)ScaleCoef - 60;
                BaleEightScaleL1 = (int)ScaleCoef - 70;
                BaleNineScaleL1 = (int)ScaleCoef - 80;
                BaleTenScaleL1 = (int)ScaleCoef - 90;
                BaleElevenScaleL1 = (int)ScaleCoef - 100;
                BaleTwelveScaleL1 = (int)ScaleCoef - 110;
                BaleThirteenScaleL1 = (int)ScaleCoef - 120;
                BaleFourteenScaleL1 = (int)ScaleCoef - 130;
                BaleFifteenScaleL1 = (int)ScaleCoef - 140;
            }
            else
            {
                BaleOneScaleL1 = (int)ScaleCoef - 140;
                BaleTwoScaleL1 = (int)ScaleCoef - 130;
                BaleThreeScaleL1 = (int)ScaleCoef - 120;
                BaleFourScaleL1 = (int)ScaleCoef - 110;
                BaleFiveScaleL1 = (int)ScaleCoef - 100;
                BaleSixScaleL1 = (int)ScaleCoef - 90;
                BaleSevenScaleL1 = (int)ScaleCoef - 80;
                BaleEightScaleL1 = (int)ScaleCoef - 70;
                BaleNineScaleL1 = (int)ScaleCoef - 60;
                BaleTenScaleL1 = (int)ScaleCoef - 50;
                BaleElevenScaleL1 = (int)ScaleCoef - 40;
                BaleTwelveScaleL1 = (int)ScaleCoef - 30;
                BaleThirteenScaleL1 = (int)ScaleCoef - 20;
                BaleFourteenScaleL1 = (int)ScaleCoef - 10;
                BaleFifteenScaleL1 = (int)ScaleCoef - 0;
            }

            //Line Two
            if (ClassCommon.BaleOrderLoToHiLnTwo)
            {
                BaleOneScaleL2 = (int)ScaleCoef;
                BaleTwoScaleL2 = (int)ScaleCoef - 10;
                BaleThreeScaleL2 = (int)ScaleCoef - 20;
                BaleFourScaleL2 = (int)ScaleCoef - 30;
                BaleFiveScaleL2 = (int)ScaleCoef - 40;
                BaleSixScaleL2 = (int)ScaleCoef - 50;
                BaleSevenScaleL2 = (int)ScaleCoef - 60;
                BaleEightScaleL2 = (int)ScaleCoef - 70;
                BaleNineScaleL2 = (int)ScaleCoef - 80;
                BaleTenScaleL2 = (int)ScaleCoef - 90;
                BaleElevenScaleL2 = (int)ScaleCoef - 100;
                BaleTwelveScaleL2 = (int)ScaleCoef - 110;
                BaleThirteenScaleL2 = (int)ScaleCoef - 120;
                BaleFourteenScaleL2 = (int)ScaleCoef - 130;
                BaleFifteenScaleL2 = (int)ScaleCoef - 140;
            }
            else
            {
                BaleOneScaleL2 = (int)ScaleCoef - 140;
                BaleTwoScaleL2 = (int)ScaleCoef - 130;
                BaleThreeScaleL2 = (int)ScaleCoef - 120;
                BaleFourScaleL2 = (int)ScaleCoef - 110;
                BaleFiveScaleL2 = (int)ScaleCoef - 100;
                BaleSixScaleL2 = (int)ScaleCoef - 90;
                BaleSevenScaleL2 = (int)ScaleCoef - 80;
                BaleEightScaleL2 = (int)ScaleCoef - 70;
                BaleNineScaleL2 = (int)ScaleCoef - 60;
                BaleTenScaleL2 = (int)ScaleCoef - 50;
                BaleElevenScaleL2 = (int)ScaleCoef - 40;
                BaleTwelveScaleL2 = (int)ScaleCoef - 30;
                BaleThirteenScaleL2 = (int)ScaleCoef - 20;
                BaleFourteenScaleL2 = (int)ScaleCoef - 10;
                BaleFifteenScaleL2 = (int)ScaleCoef - 0;
            }

            //Bale at Press////////////////////////////////////////
            BaleOnePress = (int)wdCoef;
            BaleTwoPress = (int)wdCoef - 10;
            BaleThreePress = (int)wdCoef - 20;
            BaleFourPress = (int)wdCoef - 30;
            BaleFivePress = (int)wdCoef - 40;
            BaleSixPress = (int)wdCoef - 50;
            BaleSevenPress = (int)wdCoef - 60;
            BaleEightPress = (int)wdCoef - 70;

            DefaultBaleOnePress = (int)wdCoef - 80;
            DefaultBaleTwoPress = (int)wdCoef - 90;
            DefaultBaleThreePress = (int)wdCoef - 100;
            DefaultpressFourPress = (int)wdCoef - 110;
            DefaultpressFivePress = (int)wdCoef - 120;
            /////////////////////////////////////////////////////////
            ///
            //Marker/////////////////////////////////////////////////
            BaleOneMkr = (int)mkCoef;
            BaleTwoMkr = (int)mkCoef - 10;
            BaleThreeMkr = (int)mkCoef - 20;
            BaleFourMkr = (int)mkCoef - 30;
            BaleFiveMkr = (int)mkCoef - 40;
            BaleSixMkr = (int)mkCoef - 50;
            BaleSevenMkr = (int)mkCoef - 60;
            BaleEightMkr = (int)mkCoef - 70;

            DefaultBaleOneMkr = (int)mkCoef - 80;
            DefaultBaleTwoMkr = (int)mkCoef - 90;
            DefaultBaleThreeMkr = (int)mkCoef - 100;
            DefaultpressFourMkr = (int)mkCoef - 110;
            DefaultpressFiveMkr = (int)mkCoef - 120;
        }
    }
}
