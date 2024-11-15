using BigCmbDisplayBox.ViewModels;
using BigCmbDisplayBox.Views;
using JobParams.ViewModels;
using JobParams.Views;
using MenuBarOne.ViewModels;
using MenuBarOne.Views;
using MidCmbDisplayBox.ViewModels;
using MidCmbDisplayBox.Views;
using Prism.Mvvm;
using ProductSelection.ViewModels;
using ProductSelection.Views;
using System.Windows.Controls;

namespace Console.ViewModels
{
    public class ConsoleViewModelBase : BindableBase
    {

        protected readonly Prism.Events.IEventAggregator _eventAggregator;


        private bool _userLogin;
        public bool UserLogin
        {
            get => _userLogin;
            set
            {
                SetProperty(ref _userLogin, value);
                //MenuVisible = value ? MenuVisible = Visibility.Visible : MenuVisible = Visibility.Hidden;
            }
        }

        public MenuBarOneViewModel MenuOne;
        public UserControl TopMenuOneBar => new MenuBarOneView(MenuOne);

        public ProductViewModel XStockList;
        public UserControl XProdList => new ProductView(XStockList);

        public ProductViewModel GradesList;
        public UserControl GradeList => new ProductView(GradesList);

        public ProductViewModel XGradesList;
        public UserControl XGradeList => new ProductView(XGradesList);

        public ProductViewModel StockList;
        public UserControl ProdList => new ProductView(StockList);


        public BigCmbBoxViewModel BigCmbBxOne;
        public UserControl BigCmbBoxOne => new BigCmbBoxView(BigCmbBxOne);

        public BigCmbBoxViewModel BigCmbBxTwo;
        public UserControl BigCmbBoxTwo => new BigCmbBoxView(BigCmbBxTwo);

        /// <summary>
        /// Line 1 ////////////////////////////////////////////////////
        /// </summary>
        public BigCmbBoxViewModel BigCmbBxOneLineOne;
        public UserControl BigCmbBoxOneLioneOne => new BigCmbBoxView(BigCmbBxOneLineOne);

        public BigCmbBoxViewModel BigCmbBxTwoLineOne;
        public UserControl BigCmbBoxTwoLineOne => new BigCmbBoxView(BigCmbBxTwoLineOne);
        //--------------------------------------------------------------

        /// <summary>
        /// Mid Combo Boxes Line One
        /// </summary>
        public MidCmbBoxViewModel MidDisplayBxOne;
        public UserControl MidDisplayOne => new MidCmbBoxView(MidDisplayBxOne);
        public MidCmbBoxViewModel MidDisplayBxTwo;
        public UserControl MidDisplayTwo => new MidCmbBoxView(MidDisplayBxTwo);

        public MidCmbBoxViewModel MidDisplayBxThree;
        public UserControl MidDisplayThree => new MidCmbBoxView(MidDisplayBxThree);

        public MidCmbBoxViewModel MidDisplayBxFour;
        public UserControl MidDisplayFour => new MidCmbBoxView(MidDisplayBxFour);

        public MidCmbBoxViewModel MidDisplayBxFive;
        public UserControl MidDisplayFive => new MidCmbBoxView(MidDisplayBxFive);

        public MidCmbBoxViewModel MidDisplayBxSix;
        public UserControl MidDisplaySix => new MidCmbBoxView(MidDisplayBxSix);


        /// <summary>
        /// Line One ///////////////////////////////////////////////////////////////
        /// </summary>
        //Prod line one
        public ProductViewModel StockSelectLineOne;
        public UserControl ProdSelectLineOne => new ProductView(StockSelectLineOne);

        //Next Prod line one
        public ProductViewModel XStockSelectLineOne;
        public UserControl XProdSelectLineOne => new ProductView(XStockSelectLineOne);

        //Grade Line One
        public ProductViewModel GradesSelectLineOne;
        public UserControl GradeSelectLineOne => new ProductView(GradesSelectLineOne);

        //Next Grade line one
        public ProductViewModel XGradeSelectLineOne;
        public UserControl XGradeSelctLineOne => new ProductView(XGradeSelectLineOne);


        /// <summary>
        /// Line 2 ///////////////////////////////////////////////////
        /// </summary>
        public BigCmbBoxViewModel BigCmbBxOneLineTwo;
        public UserControl BigCmbBoxOneLioneTwo => new BigCmbBoxView(BigCmbBxOneLineTwo);

        public BigCmbBoxViewModel BigCmbBxTwoLineTwo;
        public UserControl BigCmbBoxTwoLineTwo => new BigCmbBoxView(BigCmbBxTwoLineTwo);

        public JobParamsViewModel jobViewModel;
        public UserControl JobParamsUC => new JobParamsView(jobViewModel);

        /// Line 2 MidBox ////////////////////////////////////////////
        /// </summary>
        public MidCmbBoxLIneTwoViewModel MidDisplayBxOneLineTwo;
        public UserControl MidDisplayOneLineTwo => new MidCmbBoxLineTwoView(MidDisplayBxOneLineTwo);

        public MidCmbBoxLIneTwoViewModel MidDisplayBxTwoLineTwo;
        public UserControl MidDisplayTwoLineTwo => new MidCmbBoxLineTwoView(MidDisplayBxTwoLineTwo);
        public MidCmbBoxLIneTwoViewModel MidDisplayBxThreeLineTwo;
        
        public UserControl MidDisplayThreeLineTwo => new MidCmbBoxLineTwoView(MidDisplayBxThreeLineTwo);
        public MidCmbBoxLIneTwoViewModel MidDisplayBxFourLineTwo;
        public UserControl MidDisplayFourLineTwo => new MidCmbBoxLineTwoView(MidDisplayBxFourLineTwo);
        
        public MidCmbBoxLIneTwoViewModel MidDisplayBxFiveLineTwo;
        public UserControl MidDisplayFiveLineTwo => new MidCmbBoxLineTwoView(MidDisplayBxFiveLineTwo);
        
        public MidCmbBoxLIneTwoViewModel MidDisplayBxSixLineTwo;
        public UserControl MidDisplaySixLineTwo => new MidCmbBoxLineTwoView(MidDisplayBxSixLineTwo);

        /// <summary>
        /// Line 2 ///////////////////////////////////////////////////////////////
        /// </summary>
        /// Product Line 2
        public ProductViewModel StockSelectLineTwo;
        public UserControl ProdSelectLineTwo => new ProductView(StockSelectLineTwo);
        //Next Prod line Two
        public ProductViewModel XStockSelectLineTwo;
        public UserControl XProdSelectLineTwo => new ProductView(XStockSelectLineTwo);

        //Grade Line Two
        public ProductViewModel GradesSelectLineTwo;
        public UserControl GradeSelectLineTwo => new ProductView(GradesSelectLineTwo);

        //Next Grade line Two
        public ProductViewModel XGradeSelectLineTwo;
        public UserControl XGradeSelctLineTwo => new ProductView(XGradeSelectLineTwo);

    }
}
