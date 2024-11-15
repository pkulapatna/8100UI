using Summary.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Summary
{
    public class SummaryModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion2", typeof(SummaryView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}