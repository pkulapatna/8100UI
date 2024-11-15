using _8100UI.Services;
using _8100UI.Views;
using BaleGraphic;
using Console;
using Prism.Ioc;
using Prism.Modularity;
using Summary;
using System;
using System.Collections.Generic;
using System.Windows;

namespace _8100UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private ClsSerilog LogMessage;
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            LogMessage = new ClsSerilog();
            ClsSerilog.LogMessage(ClsSerilog.Info, $"-------------------------------------------------");
            ClsSerilog.LogMessage(ClsSerilog.Info, $"Start Application -> {DateTime.Now}");
        }

        /// <summary>
        /// Setup UI Automation or manual here!
        /// 1. Console for one Line or 2 lines  ConsoleModule
        /// 2. Graphic for one line or 2 lines  BaleGraphicModule
        /// </summary>
        /// <param name="moduleCatalog"></param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            ClassCommon.SavedItemList = SetUpApplcation();
            if(ClassCommon.SavedItemList.Count > 0)
            {
                ClassCommon.CustomerName = Convert.ToString(ClassCommon.SavedItemList[2].Item2);
                ClassCommon.MoistureType = Convert.ToInt32(ClassCommon.SavedItemList[3].Item2);
                ClassCommon.WeightUnit = Convert.ToInt32(ClassCommon.SavedItemList[4].Item2);
                ClassCommon.ScanPeriod = Convert.ToInt32(ClassCommon.SavedItemList[5].Item2);
                
                ClassCommon.DropChecked = Convert.ToBoolean(ClassCommon.SavedItemList[6].Item2);
                ClassCommon.SheetChecked = Convert.ToBoolean(ClassCommon.SavedItemList[7].Item2);
               
                ClassCommon.NumberOfLine = Convert.ToInt32(ClassCommon.SavedItemList[8].Item2);
                ClassCommon.SingleLot = Convert.ToBoolean(ClassCommon.SavedItemList[9].Item2);
                ClassCommon.StockActive = Convert.ToBoolean(ClassCommon.SavedItemList[10].Item2);
                ClassCommon.GradeActive = Convert.ToBoolean(ClassCommon.SavedItemList[11].Item2);
                
                ClassCommon.BaleOrderLoToHiLnOne = Convert.ToBoolean(ClassCommon.SavedItemList[12].Item2);
                ClassCommon.LayBoyCount = Convert.ToInt32(ClassCommon.SavedItemList[13].Item2);
                
                ClassCommon.JobParam = Convert.ToBoolean(ClassCommon.SavedItemList[14].Item2);
                ClassCommon.BaleOrderLoToHiLnTwo = Convert.ToBoolean(ClassCommon.SavedItemList[15].Item2);
                ClassCommon.LangaugeIdx = Convert.ToInt32(ClassCommon.SavedItemList[16].Item2);
                ClassCommon.MoveLeftToRight = Convert.ToBoolean(ClassCommon.SavedItemList[17].Item2);
            }
            //else go back to their default values

            // Big number on the top of the screen
            moduleCatalog.AddModule<ConsoleModule>();
            //Schematic display 
            moduleCatalog.AddModule<BaleGraphicModule>();
            //Summary Module is the same for all settings
            moduleCatalog.AddModule<SummaryModule>();

        }

        private List<Tuple<string, string>> SetUpApplcation()
        {
            string xmlfilepath = @"C:\ForteXml\8100UISetup.xml";
            string StartElement = "UI8100Config";
            Xmlhandler myxml = Xmlhandler.Instance;
            myxml.CheckCreateSettingsXMLFiles(xmlfilepath, StartElement);
            return myxml.Read8100SetUpXmlFile();
        }

        private void SetLanguageDictionary()
        {

            ResourceDictionary dict = new ResourceDictionary();
            switch (ClassCommon.LangaugeIdx) //Thread.CurrentThread.CurrentCulture.ToString())
            {
                case 0: // "en-US": English (United States)
                    dict.Source = new Uri("WpfLanguageResource;component/StringResources.xaml", UriKind.Relative);
                    break;
                case 1: //"es-ES": Spanish (Spain)
                    dict.Source = new Uri("WpfLanguageResource;component/StringResources.Sp.xaml", UriKind.Relative);
                    break;
                case 2: //"fr-FR": French (France)
                    dict.Source = new Uri("WpfLanguageResource;component/StringResources.Fr.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("WpfLanguageResource;component/StringResources.xaml", UriKind.Relative);
                    break;
            }

            this.Resources.MergedDictionaries.Add(dict);

        }
    }
}
