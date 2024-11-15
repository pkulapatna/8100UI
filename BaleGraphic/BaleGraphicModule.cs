using _8100UI.Services;
using BaleGraphic.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace BaleGraphic
{
    public class BaleGraphicModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            //Graphic Module options
            //  1. One Line
            //      With Drop
            //      With out Drop
            //  2. Two Lines
            //      With Drop
            //      With out Drop

            if (ClassCommon.NumberOfLine == 1)
            {
                if (ClassCommon.DropChecked == true)
                    regionManager.RegisterViewWithRegion("ContentRegion1", typeof(OneLineGraphicDropView));
                else
                   regionManager.RegisterViewWithRegion("ContentRegion1", typeof(OneLineGraphicView));
            }
            else if (ClassCommon.NumberOfLine == 2)
            {
                if (ClassCommon.DropChecked == true)
                    regionManager.RegisterViewWithRegion("ContentRegion1", typeof(TwoLinesGraphicDropView));
                else
                    regionManager.RegisterViewWithRegion("ContentRegion1", typeof(TwoLinesGraphicView));
            }
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}