using _8100UI.Services;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;

namespace SummaryFieldsSelect.Model
{
    public class SummaryFieldsModel
    {
        private readonly Sqlhandler sqlHandler;
        private readonly Xmlhandler myXml;
        private readonly AccessHandler AcHandler;
        
        public DataTable BaleRecDateCodes { get; set; } //Time
        public DataTable BaleRecTimeCodes { get; set; } //Time
        public DataTable BaleRecIdentCodes { get; set; } //Record structure
        public DataTable BaleRecWtMCodes { get; set; } //Weight and Moisture
        public DataTable DateFormat { get; set; } //Time and Date Format.
        public List<Tuple<string, string, string, string>> AllItemsList { get; set; }

        public SummaryFieldsModel(int ilottype)
        {
            sqlHandler = Sqlhandler.Instance;
            myXml = Xmlhandler.Instance;
            AcHandler = new AccessHandler();
        }

        internal List<Tuple<string, string, string, string>> GetBaleRecWtMCodes()
        {
            List<Tuple<string, string, string, string>> ListRecWtMCodes = new List<Tuple<string, string, string, string>>();
            BaleRecWtMCodes = AcHandler.GetBaleRecWtMCodes("P");

            try
            {
                foreach (DataRow item in BaleRecWtMCodes.Rows)
                {
                    ListRecWtMCodes.Add(new Tuple<string, string, string, string>(item[3].ToString(), item[1].ToString(), item[2].ToString(), item[4].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in GetBaleRecWtMCodes {ex.Message}");
            }
            return ListRecWtMCodes;
        }

        internal List<Tuple<string, string, string, string>> GetAllColumnList(int coltype)
        {
            AllItemsList = new List<Tuple<string, string, string, string>>();

            switch (coltype)
            {
                case 0: //Open Lot
                    AllItemsList.Add(new Tuple<string, string, string, string>("Lot Number", "LotNum", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Product", "StockName", "@", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Time Open", "OpenTD", "h:mm:ss", "DateTime"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Date Open", "OpenTD", "MM/dd/yy", "DateTime"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Grade", "GroupText", "@", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Lot Identity", "LotIdent", "@", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Custom Lot Number", "FC_IdentString", "@", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Status", "LotStatus", "@", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Order String", "OrderStr", "@", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %AD", "(TotBW / TotNW) * 100 / .9", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AD Inv Wt kg", "(TotBW / .9) + TotTW", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BD  Inv Wt Lbs", "(TotBW +TotTW) * 2.20462", "0000", "int"));

                    DataTable HdrAccTable = AcHandler.GetAvailableLotItemList(coltype);
                    foreach (DataRow item in HdrAccTable.Rows)
                    {
                        AllItemsList.Add(new Tuple<string, string, string, string>(item[1].ToString(), item[0].ToString(), item[2].ToString(), "int"));
                    }
                    AllItemsList.Add(new Tuple<string, string, string, string>("ASCII Field1", "AsciiFld1", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("ASCII Field2", "AsciiFld2", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("ASCII Field3", "AsciiFld3", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("ASCII Field4", "AsciiFld4", "@", "string"));
                    break;

                case 1: //Closed Lot
                    AllItemsList.Add(new Tuple<string, string, string, string>("Time Open", "OpenTD", "h:mm:ss", "DateTime"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Date Open", "OpenTD", "MM/dd/yy", "DateTime"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Time Close", "CloseTD", "h:mm:ss", "DateTime"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Date Close", "CloseTD", "MM/dd/yy", "DateTime"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Lot Number", "LotNum", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Lot Identity", "LotIdent", "@", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Custom Lot Number", "FC_IdentString", "@", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Status", "LotStatus", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Order String", "OrderStr", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Ship kg", "TotNW + TotTW", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Ship Lbs", "(TotNW + TotTW ) * 2.20462", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %AD", "(TotBW / TotNW) * 100 / .9", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Min AD", "(100 - MaxMC)/ .9", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Max AD", "(100 - MinMC)/ .9", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Avg bale kg", "TotNW  / BaleCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Avg bale Lbs", "TotNW * 2.20462 / BaleCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AD Inv Wt kg", "(TotBW / .9) + TotTW", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BD  Inv Wt Lbs", "(TotBW +TotTW) * 2.20462", "0000", "int"));


                    DataTable HdrAccTable2 = AcHandler.GetAvailableLotItemList(coltype);
                    foreach (DataRow item in HdrAccTable2.Rows)
                    {
                        AllItemsList.Add(new Tuple<string, string, string, string>(item[1].ToString(), item[0].ToString(), item[2].ToString(), "int"));
                    }
                    AllItemsList.Add(new Tuple<string, string, string, string>("ASCII Field1", "AsciiFld1", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("ASCII Field2", "AsciiFld2", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("ASCII Field3", "AsciiFld3", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("ASCII Field4", "AsciiFld4", "@", "string"));
                    break;

                case 2: //Bale Summary
                    DataTable HdrAccTable3 = sqlHandler.GetSqlScema();
                    foreach (DataRow item in HdrAccTable3.Rows)
                    {
                        AllItemsList.Add(new Tuple<string, string, string, string>(item[1].ToString(), item[1].ToString(), Getformat(item[2].ToString()), item[2].ToString()));
                    }
                    break;

                case 3: //Day Summary //Shifts Summary

                    //BaleCount
                    AllItemsList.Add(new Tuple<string, string, string, string>("BaleCount", "BaleCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("TotNW", "TotNW", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Lot Number", "LotNum", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Product", "StockName", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Time Open", "OpenTD", "h:mm:ss", "DateTime"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Date Open", "OpenTD", "MM/dd/yy", "DateTime"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Grade", "GroupText", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Lot Identity", "LotIdent", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Custom Lot Number", "FC_IdentString", "@", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Status", "LotStatus", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Order String", "OrderStr", "@", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %AD", "(TotBW / TotNW) * 100 / .9", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AD Inv Wt kg", "(TotBW / .9) + TotTW", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BD  Inv Wt Lbs", "(TotBW +TotTW) * 2.20462", "0000", "int"));

                    DataTable HdrAccTable4 = sqlHandler.GetSqlScema();
                    foreach (DataRow item in HdrAccTable4.Rows)
                    {
                        AllItemsList.Add(new Tuple<string, string, string, string>(item[1].ToString(), item[1].ToString(), Getformat(item[2].ToString()), item[2].ToString()));
                    }
                    break;

                case 4: //Shifts Summary
                    DataTable HdrAccTable5 = sqlHandler.GetSqlScema();
                    foreach (DataRow item in HdrAccTable5.Rows)
                    {
                        AllItemsList.Add(new Tuple<string, string, string, string>(item[1].ToString(), item[1].ToString(), Getformat(item[2].ToString()), item[2].ToString()));
                    }
                    break;

                case 5: //Day Tally
                    AllItemsList.Add(new Tuple<string, string, string, string>("BaleCount", "BaleCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("UnitCount", "UnitCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Ship Wt.Kg", "TotBW +TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Ship Tonnes", "( TotNW + TotTW )/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("TotalInv Wt", "( TotBW + TotTW )/9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("A IW Kg", "(TotBW / .9) + TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("A IW Tn", "((TotBW / .9) + TotTW)  / 1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("B IW Kg", "TotBW +TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("B IW Tn", "TotBW +TotTW)/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AW Kg", "TotBW/.9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AW Tn", "TotBW  / 1000 /.9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BW Kg", "TotBW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BW Tn", "TotBW / 1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Wet Wt Tn", "(TotBW /1000/.9)/((TotBW/TotNW)/.9)", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("NW Kg", "TotNW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("NW Tn", "TotNW/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Avg NW Kg", "TotNW /BaleCount", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %AD", "(TotBW / TotNW) * 100 / .9", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %BD", "(TotBW / TotNW)*100", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %MC", "100-((TotB /TotNW)*100 )", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("TW Kg", "TotNW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av SpareSngFld1", "SpareSngFld1 / BaleCount", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av SpareSngFld2", "SpareSngFld2 / BaleCount", "@", "string"));
                    break;

                case 6:
                    AllItemsList.Add(new Tuple<string, string, string, string>("BaleCount", "BaleCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("TotNW", "TotNW", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Lot Number", "LotNum", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Product", "StockName", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Time Open", "OpenTD", "h:mm:ss", "DateTime"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Date Open", "OpenTD", "MM/dd/yy", "DateTime"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Grade", "GroupText", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Lot Identity", "LotIdent", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Custom Lot Number", "FC_IdentString", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Status", "LotStatus", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Order String", "OrderStr", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %AD", "(TotBW / TotNW) * 100 / .9", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AD Inv Wt kg", "(TotBW / .9) + TotTW", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BD  Inv Wt Lbs", "(TotBW +TotTW) * 2.20462", "0000", "int"));

                    DataTable HdrAccTabl5 = sqlHandler.GetSqlScema();
                    foreach (DataRow item in HdrAccTabl5.Rows)
                    {
                        AllItemsList.Add(new Tuple<string, string, string, string>(item[1].ToString(), item[1].ToString(), Getformat(item[2].ToString()), item[2].ToString()));
                    }
                    break;
                case 8: //Day Tally
                    AllItemsList.Add(new Tuple<string, string, string, string>("BaleCount", "BaleCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("UnitCount", "UnitCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Ship Wt.Kg", "TotBW +TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Ship Tonnes", "( TotNW + TotTW )/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("TotalInv Wt", "( TotBW + TotTW )/9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("A IW Kg", "(TotBW / .9) + TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("A IW Tn", "((TotBW / .9) + TotTW)  / 1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("B IW Kg", "TotBW +TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("B IW Tn", "TotBW +TotTW)/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AW Kg", "TotBW/.9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AW Tn", "TotBW  / 1000 /.9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BW Kg", "TotBW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BW Tn", "TotBW / 1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Wet Wt Tn", "(TotBW /1000/.9)/((TotBW/TotNW)/.9)", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("NW Kg", "TotNW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("NW Tn", "TotNW/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Avg NW Kg", "TotNW /BaleCount", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %AD", "(TotBW / TotNW) * 100 / .9", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %BD", "(TotBW / TotNW)*100", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %MC", "100-((TotB /TotNW)*100 )", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("TW kg", "TotNW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av SpareSngFld1", "SpareSngFld1 / BaleCount", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av SpareSngFld2", "SpareSngFld2 / BaleCount", "@", "string"));
                    break;

                case 9: //Day Tally
                    AllItemsList.Add(new Tuple<string, string, string, string>("BaleCount", "BaleCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("UnitCount", "UnitCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Ship Wt.kg", "TotBW +TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Ship Tonnes", "( TotNW + TotTW )/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("TotalInv Wt", "( TotBW + TotTW )/9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("A IW kg", "(TotBW / .9) + TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("A IW Tn", "((TotBW / .9) + TotTW)  / 1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("B IW kg", "TotBW +TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("B IW Tn", "TotBW +TotTW)/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AW kg", "TotBW/.9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AW Tn", "TotBW  / 1000 /.9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BW kg", "TotBW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BW Tn", "TotBW / 1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Wet Wt Tn", "(TotBW /1000/.9)/((TotBW/TotNW)/.9)", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("NW kg", "TotNW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("NW Tn", "TotNW/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Avg NW kg", "TotNW /BaleCount", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %AD", "(TotBW / TotNW) * 100 / .9", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %BD", "(TotBW / TotNW)*100", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %MC", "100-((TotB /TotNW)*100 )", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("TW kg", "TotNW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av SpareSngFld1", "SpareSngFld1 / BaleCount", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av SpareSngFld2", "SpareSngFld2 / BaleCount", "@", "string"));
                    break;


                case 10: //period 

                    AllItemsList.Add(new Tuple<string, string, string, string>("BaleCount", "BaleCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("TotNW", "TotNW", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Lot Number", "LotNum", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Product", "StockName", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Time Open", "OpenTD", "h:mm:ss", "DateTime"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Date Open", "OpenTD", "MM/dd/yy", "DateTime"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Grade", "GroupText", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Lot Identity", "LotIdent", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Custom Lot Number", "FC_IdentString", "@", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Status", "LotStatus", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Order String", "OrderStr", "@", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %AD", "(TotBW / TotNW) * 100 / .9", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AD Inv Wt kg", "(TotBW / .9) + TotTW", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BD  Inv Wt Lbs", "(TotBW +TotTW) * 2.20462", "0000", "int"));

                    DataTable HdrAccTable10 = sqlHandler.GetSqlScema();
                    foreach (DataRow item in HdrAccTable10.Rows)
                    {
                        AllItemsList.Add(new Tuple<string, string, string, string>(item[1].ToString(), item[1].ToString(), Getformat(item[2].ToString()), item[2].ToString()));
                    }
                    break;

                case 11: //period Tally 
                    AllItemsList.Add(new Tuple<string, string, string, string>("BaleCount", "BaleCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("UnitCount", "UnitCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Ship Wt.Kg", "TotBW +TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Ship Tonnes", "( TotNW + TotTW )/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("TotalInv Wt", "( TotBW + TotTW )/9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("A IW Kg", "(TotBW / .9) + TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("A IW Tn", "((TotBW / .9) + TotTW)  / 1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("B IW Kg", "TotBW +TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("B IW Tn", "TotBW +TotTW)/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AW Kg", "TotBW/.9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AW Tn", "TotBW  / 1000 /.9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BW Kg", "TotBW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BW Tn", "TotBW / 1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Wet Wt Tn", "(TotBW /1000/.9)/((TotBW/TotNW)/.9)", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("NW Kg", "TotNW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("NW Tn", "TotNW/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Avg NW Kg", "TotNW /BaleCount", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %AD", "(TotBW / TotNW) * 100 / .9", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %BD", "(TotBW / TotNW)*100", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %MC", "100-((TotB /TotNW)*100 )", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("TW Kg", "TotNW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av SpareSngFld1", "SpareSngFld1 / BaleCount", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av SpareSngFld2", "SpareSngFld2 / BaleCount", "@", "string"));
                    break;

                case 12: //period Tally 
                    AllItemsList.Add(new Tuple<string, string, string, string>("BaleCount", "BaleCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("UnitCount", "UnitCount", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Ship Wt.Kg", "TotBW +TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Ship Tonnes", "( TotNW + TotTW )/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("TotalInv Wt", "( TotBW + TotTW )/9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("A IW Kg", "(TotBW / .9) + TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("A IW Tn", "((TotBW / .9) + TotTW)  / 1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("B IW Kg", "TotBW +TotTW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("B IW Tn", "TotBW +TotTW)/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AW Kg", "TotBW/.9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("AW Tn", "TotBW  / 1000 /.9", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BW Kg", "TotBW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("BW Tn", "TotBW / 1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Wet Wt Tn", "(TotBW /1000/.9)/((TotBW/TotNW)/.9)", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("NW Kg", "TotNW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("NW Tn", "TotNW/1000", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Avg NW Kg", "TotNW /BaleCount", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %AD", "(TotBW / TotNW) * 100 / .9", "0000", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %BD", "(TotBW / TotNW)*100", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av %MC", "100-((TotB /TotNW)*100 )", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("TW Kg", "TotNW", "#0.00", "int"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av SpareSngFld1", "SpareSngFld1 / BaleCount", "@", "string"));
                    AllItemsList.Add(new Tuple<string, string, string, string>("Av SpareSngFld2", "SpareSngFld2 / BaleCount", "@", "string"));
                    break;

            }
            return AllItemsList;
        }
        private string Getformat(string itemType)
        {
            string myformat = string.Empty;

            switch (itemType)
            {
                case "int":
                    myformat = "000";
                    break;

                case "nvarchar":
                    myformat = "@";
                    break;

                case "smallint":
                    myformat = "000";
                    break;

                case "real":
                    myformat = "#0.0";
                    break;

                case "datetime":
                    myformat = "MM/dd/yy";
                    break;

                case "tinyint":
                    myformat = "000";
                    break;
            }
            return myformat;
        }

        internal ObservableCollection<SqlLotSumFields> GetXMLReportList(int ilottype)
        {
            ObservableCollection<SqlLotSumFields> returnList = new ObservableCollection<SqlLotSumFields>();

            switch (ilottype)
            {
                case 0: //Open Lot

                    returnList = myXml.ReadOpenLotXml(ClassCommon.OpenLotSummaryXmlFilePath);
                    break;

                case 1: //Closed Lot
                    returnList = myXml.ReadOpenLotXml(ClassCommon.ClosedLotSummaryXmlFilePath);
                    break;

                case 2: //Bale Summary
                    returnList = myXml.ReadOpenLotXml(ClassCommon.BaleSummaryXmlFilePath);
                    break;

                case 3: //Day Summary
                    returnList = myXml.ReadOpenLotXml(ClassCommon.DaySummaryXmlFilePath);
                    break;

                case 5: //Tally Summary
                    returnList = myXml.ReadOpenLotXml(ClassCommon.DayTallyXmlFilePath);
                    break;

                case 6: //Shift Summary
                    returnList = myXml.ReadOpenLotXml(ClassCommon.ShiftSummaryXmlFilePath);
                    break;

                case 8: //Shift Summary
                    returnList = myXml.ReadOpenLotXml(ClassCommon.ShiftTallyXmlFilePath);
                    break;

                case 9: //Pior Summary
                    returnList = myXml.ReadOpenLotXml(ClassCommon.PiorXmlFilePath);
                    break;

                case 10: //Pior Summary
                    returnList = myXml.ReadOpenLotXml(ClassCommon.PeriodXmlFilePath);
                    break;

                case 11: //Pior Summary
                    returnList = myXml.ReadOpenLotXml(ClassCommon.PeriodXmlFilePath);
                    break;

                case 12: //Period Summary
                    returnList = myXml.ReadOpenLotXml(ClassCommon.PeriodTallyXmlFilePath);
                    break;
            }
            return returnList;
        }

        internal void SaveFieldColumns(ObservableCollection<SqlLotSumFields> columnFields, int ilottype)
        {
            myXml.UpdateSummaryXML(columnFields, ilottype);
        }
    }
}
