using _8100UI.Services;
using Console.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Console
{
    public class ConsoleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //Console Module options
            //  1. One Line
            //  2. Two Lines
           
            var regionManager = containerProvider.Resolve<IRegionManager>();
            if (ClassCommon.NumberOfLine == 1)
                regionManager.RegisterViewWithRegion("ContentRegion", typeof(ConsoleView));
            else if (ClassCommon.NumberOfLine == 2)
                regionManager.RegisterViewWithRegion("ContentRegion", typeof(ConsoleTwoLinesView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}