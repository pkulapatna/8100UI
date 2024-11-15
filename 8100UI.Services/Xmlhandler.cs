using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml;

namespace _8100UI.Services
{
    public class Xmlhandler
    {
        private static Xmlhandler instance = null;
        private static readonly object padlock = new object();
        //string xmlSqlfilepath = @"C:\Program Files(x86)\Forte Technology\8100UISetup.xml";
        string xmlSqlfilepath = @"C:\ForteXml\8100UISetup.xml";

        public static Xmlhandler Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Xmlhandler();
                    }
                    return instance;
                }
            }
        }

        public Xmlhandler() { }
        
        public string GetSettingsDirectory()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory;
        }

        public string SettingsGdvFile
        {
            get { return Path.Combine(GetSettingsDirectory(), "GridviewItems.xml"); }
        }

        public void CheckandCreateXMLFiles(string fileLocationPath, string startElement)
        {
            if (!System.IO.File.Exists(fileLocationPath))
            {
                // ClassCommon.SystemLogFile.LogMessage(MsgTypes.INFO, MsgSources.APPSTART, "Create xml file " + FileLocationPath);
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true
                };

                using (XmlWriter writer = XmlWriter.Create(fileLocationPath, settings))
                {
                    //Begin write
                    writer.WriteStartDocument();
                    //Node
                    writer.WriteStartElement(startElement);

                    writer.WriteEndDocument();
                    writer.Close();
                }
            }
        }


        public void CheckCreateSettingsXMLFiles(string fileLocationPath, string startElement)
        {
            List<Tuple<string, string>> myList = new List<Tuple<string, string>>();

            if (File.Exists(fileLocationPath) != true)
            {
                // ClassCommon.SystemLogFile.LogMessage(MsgTypes.INFO, MsgSources.APPSTART, "Create xml file " + FileLocationPath);
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true
                };

                using (XmlWriter writer = XmlWriter.Create(fileLocationPath, settings))
                {
                    //Begin write
                    writer.WriteStartDocument();
                    //Node
                    writer.WriteStartElement(startElement);

                    writer.WriteEndDocument();
                    writer.Close();
                }

                myList.Add(new Tuple<string, string>($"{"Host"}", $"127.0.0.1"));
                myList.Add(new Tuple<string, string>("Instant", "SQLEXPRESS"));
                myList.Add(new Tuple<string, string>("CustomerName", "Forte"));
                myList.Add(new Tuple<string, string>("MoistureType", "0"));
                myList.Add(new Tuple<string, string>("WeightUnit", "0"));
                myList.Add(new Tuple<string, string>("ScanPeriod", "2"));
                myList.Add(new Tuple<string, string>("DropEnable", "false"));
                myList.Add(new Tuple<string, string>("SheetChecked", "false"));
                myList.Add(new Tuple<string, string>("ProcessLine", "1"));
                myList.Add(new Tuple<string, string>("SingleLot", "true"));
                myList.Add(new Tuple<string, string>("StockEnable", "false"));
                myList.Add(new Tuple<string, string>("GradeEnable", "false"));
                myList.Add(new Tuple<string, string>("OrderLowtoHigh", "true"));

                myList.Add(new Tuple<string, string>("Layboycount", "1"));
                myList.Add(new Tuple<string, string>("JobParamChecked", "false"));
                myList.Add(new Tuple<string, string>("OrderLowtoHighLn2", "false"));
                myList.Add(new Tuple<string, string>("Language", "0"));
                myList.Add(new Tuple<string, string>("RightToLeft", "false"));

                WriteXmlFile(myList);
            }
        }


        public List<string> ReadXmlGridView(string FileLocation)
        {
            List<string> XmlGridView = new List<string>();
            XmlGridView.Clear();
            XmlDocument doc = new XmlDocument();

            try
            {
                if (File.Exists(FileLocation))
                {
                    doc.Load(FileLocation);
                    XmlNodeList xnl = doc.SelectNodes("CustomGridView/Field/Name");

                    if ((xnl != null) && (xnl.Count > 0))
                    {
                        foreach (XmlNode xn in xnl)
                        {
                            if (File.Exists(FileLocation))
                                XmlGridView.Add(xn.InnerText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in ReadXmlGridView -> { ex.Message}");
                MessageBox.Show("Error in ReadXmlGridView - " + ex.Message);

            }
            return XmlGridView;
        }

        

        public string GetBaseDirPath()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory;
        }

        public void UpdateSummaryXML(ObservableCollection<SqlLotSumFields> columnFields, int ilottype)
        {
            string xmlfilepath = string.Empty;
            char[] charsToTrim = { ',' };
            ClassCommon.ReportFields = string.Empty;

            switch (ilottype)
            {
                case 0:
                    xmlfilepath = ClassCommon.OpenLotSummaryXmlFilePath;
                    if (columnFields.Count > 0)
                    {
                        ClassCommon.ReportFields = string.Empty;
                        for (int i = 0; i < columnFields.Count; i++)
                        {
                            ClassCommon.ReportFields += columnFields[i].FieldExp + ",";
                        }
                        ClassCommon.ReportFields = ClassCommon.ReportFields.TrimEnd(charsToTrim);
                    }
                    break;

                case 1:
                    xmlfilepath = ClassCommon.ClosedLotSummaryXmlFilePath;
                    break;
                case 2:
                    xmlfilepath = ClassCommon.BaleSummaryXmlFilePath;
                    break;
                case 3:
                    xmlfilepath = ClassCommon.DaySummaryXmlFilePath;
                    break;

                case 5:
                    xmlfilepath = ClassCommon.DayTallyXmlFilePath;
                    break;

                case 6:
                    xmlfilepath = ClassCommon.ShiftSummaryXmlFilePath;
                    break;

                case 8:
                    xmlfilepath = ClassCommon.ShiftTallyXmlFilePath;
                    break;

                case 9:
                    xmlfilepath = ClassCommon.PiorXmlFilePath;
                    break;

                case 10:
                    xmlfilepath = ClassCommon.PeriodXmlFilePath;
                    break;

                case 12:
                    xmlfilepath = ClassCommon.PeriodTallyXmlFilePath;
                    break;
            }

            try
            {
                if (File.Exists(xmlfilepath))
                {
                    File.SetAttributes(xmlfilepath, FileAttributes.Normal);
                    XmlWriterSettings settings = new XmlWriterSettings
                    {
                        Indent = true
                    };

                    using (XmlWriter writer = XmlWriter.Create(xmlfilepath, settings))
                    {
                        //Begin write
                        writer.WriteStartDocument();
                        //Node
                        writer.WriteStartElement("SummaryColumn");

                        foreach (var item in columnFields)
                        {
                            writer.WriteStartElement("Field");
                            writer.WriteElementString("Name", item.FieldName);
                            writer.WriteElementString("FieldExp", item.FieldExp);
                            writer.WriteElementString("Format", item.Format);
                            writer.WriteElementString("FieldType", item.FieldType);
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateSummaryXML -> { ex.Message}");
                MessageBox.Show("Error in Update UpdateSummaryXML " + ex);
            }
        }

        public void WriteXmlFile(List<Tuple<string, string>> myList)
        {
            string xmlfilepath = @"C:\ForteXml\8100UISetup.xml";
            string subPath = @"C:\ForteXml";
            string StartElement = "UI8100Config";
            try
            {
                bool exists = System.IO.Directory.Exists(subPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(subPath);

                if (!File.Exists(xmlfilepath))
                {
                    XmlWriterSettings newsettings = new XmlWriterSettings
                    {
                        Indent = true
                    };
                    using (XmlWriter writer = XmlWriter.Create(xmlfilepath, newsettings))
                    {
                        //Begin write
                        writer.WriteStartDocument();
                        //Node
                        writer.WriteStartElement(StartElement);

                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    newsettings = null;
                }

                File.SetAttributes(xmlfilepath, FileAttributes.Normal);
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true
                };

                using (XmlWriter writer = XmlWriter.Create(xmlfilepath, settings))
                {
                    //Begin write
                    writer.WriteStartDocument();
                    //Node
                    writer.WriteStartElement("ParamSetting");

                    foreach (var item in myList)
                    {
                        writer.WriteStartElement("Field");
                        writer.WriteElementString("Name", item.Item1);
                        writer.WriteElementString("Value", item.Item2);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Close();
                }
                settings = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in WriteXmlFile {ex.Message}");
            }
        }

        public ObservableCollection<SqlLotSumFields> ReadOpenLotXml(string openLotSummaryXmlFilePath)
        {
            ObservableCollection<SqlLotSumFields> Mylist = new ObservableCollection<SqlLotSumFields>();
            Mylist.Clear();
            XmlDocument doc = new XmlDocument();
            char[] charsToTrim = { ',' };

            try
            {
                if (File.Exists(openLotSummaryXmlFilePath))
                {
                    doc.Load(openLotSummaryXmlFilePath);

                    XmlNodeList xmlnode = doc.GetElementsByTagName("SummaryColumn");
                    XmlNodeList ANode = doc.SelectNodes("SummaryColumn/Field");
                    ClassCommon.ReportFields = string.Empty;

                    if ((ANode != null) && (ANode.Count > 0))
                    {
                        for (int i = 0; i <= ANode.Count - 1; i++)
                        {
                            var Name = xmlnode[0].ChildNodes.Item(i).ChildNodes.Item(0).InnerText.Trim();
                            var FieldExp = xmlnode[0].ChildNodes.Item(i).ChildNodes.Item(1).InnerText.Trim();
                            var Format = xmlnode[0].ChildNodes.Item(i).ChildNodes.Item(2).InnerText.Trim();
                            var ItemType = xmlnode[0].ChildNodes.Item(i).ChildNodes.Item(3).InnerText.Trim();

                            Mylist.Add(new SqlLotSumFields(Name, FieldExp, Format, ItemType));
                            ClassCommon.ReportFields += $" [{Name}] ";
                        }
                    }
                    ClassCommon.ReportFields = ClassCommon.ReportFields.TrimEnd(charsToTrim);
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in ReadOpenLotXml -> { ex.Message}");
                MessageBox.Show("ERROR in  ReadOpenLotXml " + ex.Message);
            }
            return Mylist;
        }

        public ObservableCollection<SqlReportField> ReadXMlRepColumn(string summaryXmlFilePath)
        {
            ObservableCollection<SqlReportField> RepColList = new ObservableCollection<SqlReportField>();
            RepColList.Clear();
            XmlDocument doc = new XmlDocument();
            char[] charsToTrim = { ',' };

            try
            {
                if (File.Exists(summaryXmlFilePath))
                {
                    doc.Load(summaryXmlFilePath);

                    XmlNodeList xmlnode = doc.GetElementsByTagName("SummaryColumn");
                    XmlNodeList ANode = doc.SelectNodes("SummaryColumn/Field");
                    ClassCommon.ReportFields = string.Empty;

                    if ((ANode != null) && (ANode.Count > 0))
                    {
                        for (int i = 0; i <= ANode.Count - 1; i++)
                        {
                            var DbField = xmlnode[0].ChildNodes.Item(i).ChildNodes.Item(0).InnerText.Trim();
                            var FieldExp = xmlnode[0].ChildNodes.Item(i).ChildNodes.Item(1).InnerText.Trim();
                            var Name = xmlnode[0].ChildNodes.Item(i).ChildNodes.Item(2).InnerText.Trim();
                            var Format = xmlnode[0].ChildNodes.Item(i).ChildNodes.Item(3).InnerText.Trim();

                            RepColList?.Add(new SqlReportField(DbField, FieldExp, Name, Format));
                            ClassCommon.ReportFields += FieldExp + ",";
                        }
                    }
                    ClassCommon.ReportFields = ClassCommon.ReportFields.TrimEnd(charsToTrim);
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in ReadXMlRepColumn -> { ex.Message}");
                MessageBox.Show("ERROR in  ReadXMlRepColumn " + ex.Message);
            }
            return RepColList;
        }


        public void UpdateXMlcolumnList(ObservableCollection<string> selectedHdrList, string settingsGdvFile)
        {
            try
            {
                if (File.Exists(settingsGdvFile)) File.Delete(settingsGdvFile);

                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true
                };
                using (XmlWriter writer = XmlWriter.Create(settingsGdvFile, settings))
                {
                    //Begin write
                    writer.WriteStartDocument();
                    //Node
                    writer.WriteStartElement("CustomGridView");

                    foreach (var item in selectedHdrList)
                    {
                        writer.WriteStartElement("Field");
                        writer.WriteElementString("Name", item);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Close();
                }
            }

            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in UpdateXMlcolumnList -> { ex.Message}");
                MessageBox.Show("Error in UpdateXMlcolumnList " + ex);
            }
        }


        public List<Tuple<string, string>> Read8100SetUpXmlFile()
        {
            List<Tuple<string, string>> Items = new List<Tuple<string, string>>();
            XmlDocument doc = new XmlDocument();

            try
            {
                if (File.Exists(xmlSqlfilepath))
                {
                    doc.Load(xmlSqlfilepath);

                    XmlNodeList xmlnode = doc.GetElementsByTagName("ParamSetting");
                    XmlNodeList ANode = doc.SelectNodes("ParamSetting/Field");

                    if ((ANode != null) && (ANode.Count > 0))
                    {
                        for (int i = 0; i <= ANode.Count - 1; i++)
                        {
                            var Name = xmlnode[0].ChildNodes.Item(i).ChildNodes.Item(0).InnerText.Trim();
                            var Value = xmlnode[0].ChildNodes.Item(i).ChildNodes.Item(1).InnerText.Trim();
                            Items.Add(new Tuple<string, string>($"{Name}", $"{Value}"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in Read8100SetUpXmlFile {ex.Message}");
            }
            return Items;
        }

    }
}
