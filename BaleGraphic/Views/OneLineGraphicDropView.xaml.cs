using _8100UI.Services;
using BaleGraphic.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BaleGraphic.Views
{
    /// <summary>
    /// Interaction logic for OneLineGraphicDropView.xaml
    /// </summary>
    public partial class OneLineGraphicDropView : UserControl
    {

        protected readonly Prism.Events.IEventAggregator _eventAggregator;
        public static OneLineGraphicDropView BaleGraphicDropWindow;
        private double iScreenWidth { get; set; }
        private double wdCoef = 0.0;

        //Small Window @ Scale
        int DefaultBaleOneScale = 10;     //850
        int DefaultBaleTwoScale = 0;     //830
        int DefaultBaleThreeScale = -10;   //810
        int DefaultBaleFourScale = -20;    //790
        int DefaultBaleFiveScale = -30;    //770


        //Small Window @ press
        int BaleOnePress = 450;     //850
        int BaleTwoPress = 430;     //830
        int BaleThreePress = 410;   //810
        int BaleFourPress = 390;    //790
        int BaleFivePress = 370;    //770
        int BaleSixPress = 350;     //750
        int BaleSevenPress = 330;   //730
        int BaleEightPress = 310;   //710
        int BaleNinePress = 290;    //690
        int BaleTenPress = 270;     //670

        //small Window @ Press
        int DefaultBaleOnePress = 440;   //1400
        int DefaultBaleTwoPress = 430;   //1380
        int DefaultBaleThreePress = 420;    //1360
        int DefaultpressFourPress = 410;    //1340  
        int DefaultpressFivePress = 0;    //1320


        //full Window @ Marker
        int BaleOneMkr = 760;   //1400
        int BaleTwoMkr = 740;   //1380
        int BaleThreeMkr = 720;    //1360
        int BaleFourMkr = 700;    //1340  
        int BaleFiveMkr = 680;    //1320
        int BaleSixMkr = 660;     //1300
        int BaleSevenMkr = 640;   //1280
        int BaleEightMkr = 620;   //1260
        int BaleNineMkr = 600;    //1240
        int BaleTenMkr = 580;     //1220

        //small Window @ Marker
        int DefaultBaleOneMkr; //= 710;   //1400
        int DefaultBaleTwoMkr; //= 700;   //1380
        int DefaultBaleThreeMkr;// = 690;    //1360
        int DefaultpressFourMkr;// = 680;    //1340  
        int DefaultpressFiveMkr; //= 670;    //1320



        public OneLineGraphicDropView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            BaleGraphicDropWindow = this;
            this._eventAggregator = eventAggregator;
            this.DataContext = new OneLineGraphicDropViewModel(_eventAggregator);

            ClearAllBales();
        }

        public void ClearAllBales()
        {
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

            baleOneMrk.Visibility = Visibility.Hidden;
            baleTwoMrk.Visibility = Visibility.Hidden;
            baleThreeMrk.Visibility = Visibility.Hidden;
            baleFourMrk.Visibility = Visibility.Hidden;
            baleFiveMrk.Visibility = Visibility.Hidden;

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


        internal void RemoveBaleLineOne(int baleNumber)
        {
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
        }

        internal void ClearBaleFromScaleConveyor()
        {

            baleOneLineOne.Visibility = Visibility.Hidden;
            baleTwoLineOne.Visibility = Visibility.Hidden;
            baleThreeLineOne.Visibility = Visibility.Hidden;
            baleFourLineOne.Visibility = Visibility.Hidden;
            baleFiveLineOne.Visibility = Visibility.Hidden;

            defaultbaleOneScale.Visibility = Visibility.Hidden;
            defaultbaleTwoScale.Visibility = Visibility.Hidden;
            defaultbaleThreeScale.Visibility = Visibility.Hidden;
            defaultbaleFourScale.Visibility = Visibility.Hidden;
            defaultbaleFiveScale.Visibility = Visibility.Hidden;
        }


        internal void PutBaleOnScaleConveyor(List<int> baleList)
        {

            if (baleList.Count > 0)
            {
                for (int i = 0; i < baleList.Count; i++)
                {
                    switch (baleList[i])
                    {
                        case 0:
                            PutDefaultBaleOnScale(baleList);
                            break;
                        case 1:
                            baleOneLineOne.Visibility = Visibility.Visible;
                            break;

                        case 2:
                            baleTwoLineOne.Visibility = Visibility.Visible;
                            break;
                        case 3:
                            baleThreeLineOne.Visibility = Visibility.Visible;
                            break;

                        case 4:
                            baleFourLineOne.Visibility = Visibility.Visible;
                            break;

                        case 5:
                            baleFiveLineOne.Visibility = Visibility.Visible;
                            break;

                        case 6:
                            baleSixLineOne.Visibility = Visibility.Visible;
                            break;

                        case 7:
                            baleSevenLineOne.Visibility = Visibility.Visible;
                            break;

                        case 8:
                            baleEightLineOne.Visibility = Visibility.Visible;
                            break;
                        case 9:
                            baleNineLineOne.Visibility = Visibility.Visible;
                            break;

                        case 10:
                            baleTenLineOne.Visibility = Visibility.Visible;
                            break;
                    }
                }
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


        private void BaleGraphic_sizeChanged(object sender, SizeChangedEventArgs e)
        {
            iScreenWidth = e.NewSize.Width;
            wdCoef = e.NewSize.Width * .30;// 0.45; //35

            //Bale at Press////////////////////////////////////////
            BaleOnePress = (int)wdCoef;
            BaleTwoPress = (int)wdCoef - 15;
            BaleThreePress = (int)wdCoef - 30;
            BaleFourPress = (int)wdCoef - 45;
            BaleFivePress = (int)wdCoef - 60;
            BaleSixPress = (int)wdCoef - 75;
            BaleSevenPress = (int)wdCoef - 90;
            BaleEightPress = (int)wdCoef - 105;
            BaleNinePress = (int)wdCoef - 120;
            BaleTenPress = (int)wdCoef - 135;

            DefaultBaleOnePress = (int)wdCoef - 120;
            DefaultBaleTwoPress = (int)wdCoef - 135;
            DefaultBaleThreePress = (int)wdCoef - 150;
            DefaultpressFourPress = (int)wdCoef - 165;
            DefaultpressFivePress = (int)wdCoef - 180;
            /////////////////////////////////////////////////////////
            ///
            //Marker/////////////////////////////////////////////////
            BaleOneMkr = (int)(iScreenWidth - wdCoef - 300);
            BaleTwoMkr = (int)(iScreenWidth - wdCoef - 310);
            BaleThreeMkr = (int)(iScreenWidth - wdCoef - 320);
            BaleFourMkr = (int)(iScreenWidth - wdCoef - 330);
            BaleFiveMkr = (int)(iScreenWidth - wdCoef - 310);
            BaleSixMkr = (int)(iScreenWidth - wdCoef - 340);
            BaleSevenMkr = (int)(iScreenWidth - wdCoef - 350);
            BaleEightMkr = (int)(iScreenWidth - wdCoef - 360);
            BaleNineMkr = (int)(iScreenWidth - wdCoef - 370);
            BaleTenMkr = (int)(iScreenWidth - wdCoef - 380);

            DefaultBaleOneMkr = (int)(iScreenWidth - wdCoef - 390);
            DefaultBaleTwoMkr = (int)(iScreenWidth - wdCoef - 400);
            DefaultBaleThreeMkr = (int)(iScreenWidth - wdCoef - 410);
            DefaultpressFourMkr = (int)(iScreenWidth - wdCoef - 420);
            DefaultpressFiveMkr = (int)(iScreenWidth - wdCoef - 430);
            /////////////////////////////////////////////////////////
        }

        internal void ClearScaleConveyor()
        {
            baleOneLineOne.Visibility = Visibility.Hidden;
            baleTwoLineOne.Visibility = Visibility.Hidden;
            baleThreeLineOne.Visibility = Visibility.Hidden;
            baleFourLineOne.Visibility = Visibility.Hidden;
            baleFiveLineOne.Visibility = Visibility.Hidden;

            defaultbaleOneScale.Visibility = Visibility.Hidden;
            defaultbaleTwoScale.Visibility = Visibility.Hidden;
            defaultbaleThreeScale.Visibility = Visibility.Hidden;
            defaultbaleFourScale.Visibility = Visibility.Hidden;
            defaultbaleFiveScale.Visibility = Visibility.Hidden;
        }

        internal void ClearPressConveyor()
        {
            baleOnePress.Visibility = Visibility.Hidden;
            baleTwoPress.Visibility = Visibility.Hidden;
            baleThreePress.Visibility = Visibility.Hidden;
            baleFourPress.Visibility = Visibility.Hidden;
            baleFivePress.Visibility = Visibility.Hidden;

            defaultbaleOnePress.Visibility = Visibility.Hidden;
            defaultbaleTwoPress.Visibility = Visibility.Hidden;
            defaultbaleThreePress.Visibility = Visibility.Hidden;
            defaultbaleFourPress.Visibility = Visibility.Hidden;
            defaultbaleFivePress.Visibility = Visibility.Hidden;


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
                        baleSixLineOne.Margin = new Thickness(BaleSixPress, 0, 0, 0);
                        baleSixLineOne.Visibility = Visibility.Visible;
                        break;

                    case 7:
                        baleSevenLineOne.Margin = new Thickness(BaleSevenPress, 0, 0, 0);
                        baleSevenLineOne.Visibility = Visibility.Visible;
                        break;

                    case 8:
                        baleEightLineOne.Margin = new Thickness(BaleEightPress, 0, 0, 0);
                        baleEightLineOne.Visibility = Visibility.Visible;
                        break;
                    case 9:
                        baleNineLineOne.Margin = new Thickness(BaleNinePress, 0, 0, 0);
                        baleNineLineOne.Visibility = Visibility.Visible;
                        break;

                    case 10:
                        baleTenLineOne.Margin = new Thickness(BaleTenPress, 0, 0, 0);
                        baleTenLineOne.Visibility = Visibility.Visible;
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

        internal void ClearMrkConveyor()
        {
            baleOneMrk.Visibility = Visibility.Hidden;
            baleTwoMrk.Visibility = Visibility.Hidden;
            baleThreeMrk.Visibility = Visibility.Hidden;
            baleFourMrk.Visibility = Visibility.Hidden;
            baleFiveMrk.Visibility = Visibility.Hidden;

            defaultbaleOneMrk.Visibility = Visibility.Hidden;
            defaultbaleTwoMrk.Visibility = Visibility.Hidden;
            defaultbaleThreeMrk.Visibility = Visibility.Hidden;
            defaultbaleFourMrk.Visibility = Visibility.Hidden;
            defaultbaleFiveMrk.Visibility = Visibility.Hidden;
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
                        baleSixLineOne.Margin = new Thickness(BaleSixMkr, 0, 0, 0);
                        baleSixLineOne.Visibility = Visibility.Visible;
                        break;

                    case 7:
                        baleSevenLineOne.Margin = new Thickness(BaleSevenMkr, 0, 0, 0);
                        baleSevenLineOne.Visibility = Visibility.Visible;
                        break;

                    case 8:
                        baleEightLineOne.Margin = new Thickness(BaleEightMkr, 0, 0, 0);
                        baleEightLineOne.Visibility = Visibility.Visible;
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

        private void PutDefaultBaleOnMrkConveyor(List<int> baleList)
        {

            for (int i = 0; i < baleList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        if (baleList[i] == 0)
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


    }
}
