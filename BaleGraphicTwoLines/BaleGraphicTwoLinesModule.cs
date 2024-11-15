using _8100UI.Services;
using BaleGraphicTwoLines.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace BaleGraphicTwoLines
{
    public class BaleGraphicTwoLinesModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
          
            //Select Graphic Module (no Two Lines without drop for now!
            if (ClassCommon.DropChecked == true)
                regionManager.RegisterViewWithRegion("ContentRegion1", typeof(TwoLineGraphicDropView));
            else
                regionManager.RegisterViewWithRegion("ContentRegion1", typeof(TwoLineGraphicView));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}