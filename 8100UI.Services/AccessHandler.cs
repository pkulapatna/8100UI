using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _8100UI.Services
{
    public class AccessHandler
    {
        private readonly bool bDebug = false;

        private readonly string dbProvider = "PROVIDER=Microsoft.Jet.OLEDB.4.0;";
        //private readonly string dbProvider = "PROVIDER=Microsoft.ACE.OLEDB.12.0;";

        //private string DB_SUPJ4 = @"Data Source=C:\\ForteSystem\Reports\Rep_SupJ4.mdb;Persist Security Info=True;";
        private readonly string DB_7760 = @"Data Source=C:\\ForteSystem\RealTime\7760.mdb;Persist Security Info=True;";
        private readonly string DB_RPJ4 = @"Data Source=C:\\ForteSystem\Reports\ReportsJ4.mdb;Persist Security Info=True;";
        private readonly string DB_CFG7760 = @"Data Source=C:\\ForteSystem\RealTime\Cfg7760.mdb;Persist Security Info=True;";
        private readonly string DB_STOCK = @"Data Source=C:\\ForteSystem\Grades\PulpGrade.mdb;Persist Security Info=True;";
        private readonly string DB_RPSUBJ4 = @"Data Source=C:\\ForteSystem\Reports\Rep_SupJ4.mdb;Persist Security Info=True;";

        public AccessHandler()
        {

        }
        public DataTable GetBaleRecWtMCodes(string type)
        {
            DataTable mytable = new DataTable();
            string strQuery = $"SELECT [Type],[FieldExpr],[Format],[BRecFld1],[FieldType] FROM [BaleRecWtMCodes] WHERE Type = '{type}'";
            string connectionString = dbProvider + DB_RPSUBJ4;
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand comm = new OleDbCommand(strQuery, connection))
                    {
                        using (OleDbDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                mytable.Load(reader);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetBaleRecWtMCodes -> { ex.Message}");
                MessageBox.Show($"ERROR in GetBaleRecWtMCodes {ex.Message}");
            }
            return mytable;
        }

        public async Task<DataTable> GeBaleDataTableAsync()
        {
            DataTable mydat = new DataTable();
            string connectionString = dbProvider + DB_7760;

            int rowsel = ClassCommon.DropSize * 2;

            // ClsSerilog.LogMessage(ClassCommon.Info, $"GeBaleDataTableAsync ClassCommon.DropSize -> { ClassCommon.DropSize}");
            // ClsSerilog.LogMessage(ClassCommon.Info, $"GeBaleDataTableAsync ClassCommon.NumDropsMax -> { ClassCommon.NumDropsMax}");

            string strQuery = $"SELECT TOP {rowsel} * FROM [Bales] ORDER by ID";

            try
            {
                await Task.Run(() =>
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();
                        using (OleDbCommand comm = new OleDbCommand(strQuery, connection))
                        {
                            using (OleDbDataReader reader = comm.ExecuteReader())
                            {
                                if (reader.HasRows)
                                    mydat.Load(reader);
                            }
                        }
                        connection.Close();
                    }
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GeBaleDataTableAsync -> { ex.Message}");
                ClsSerilog.LogMessage(ClsSerilog.Error, $"strQuery -> { strQuery}");
                throw new Exception("ERROR in GeBaleDataTableAsync.", ex);
                //MessageBox.Show("Error in GeBaleDataTableAsync -> " + ex.Message);
            }
            if (bDebug) ClsSerilog.LogMessage(ClsSerilog.Info, $"GeBaleDataTableAsync mydatRows = {mydat.Rows.Count}");
            return mydat;
        }


        public DataTable GeBaleDrop()
        {
            DataTable mydat = new DataTable();
            string connectionString = dbProvider + DB_7760;
            string strQuery = $"SELECT TOP 1 * FROM [Drops] ORDER by ID";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand comm = new OleDbCommand(strQuery, connection))
                    {
                        using (OleDbDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                mydat.Load(reader);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GeBaleDrop -> { ex.Message}");
                MessageBox.Show($"Error in GeBaleDrop -> { ex.Message}");
            }
            if (bDebug) ClsSerilog.LogMessage(ClsSerilog.Info, $"GeBaleDrop mydatRows = {mydat.Rows.Count}");
            return mydat;
        }

        public DataTable GetRealTimeLotProc(string querystr)
        {
            DataTable mytable = new DataTable();
            string connectionString = dbProvider + DB_7760;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand comm = new OleDbCommand(querystr, connection))
                    {
                        using (OleDbDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                mytable.Load(reader);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetRealTimeLotProc -> { ex.Message}");
                MessageBox.Show("Error in GetRealTimeLotProc -> " + ex.Message);
            }
            return mytable;
        }

        public DataTable GetAvailableLotItemList(int lotType)
        {
            DataTable mydat = new DataTable();
            string connectionString = dbProvider + DB_RPSUBJ4;
            string strquery = string.Empty;

            if (lotType == 0)
                strquery = $"SELECT [FieldExpr],[Brief Desc],[Format] FROM [SummaryRecWtMCodes] WHERE Type = '*' AND IniEnable = True";
            else if (lotType == 1)
                strquery = $"SELECT [FieldExpr],[Brief Desc],[Format] FROM [SummaryRecWtMCodes] WHERE Type = '*' AND IniEnable = True";
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand comm = new OleDbCommand(strquery, connection))
                    {
                        using (OleDbDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                mydat.Load(reader);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetAvailableLotItemList -> { ex.Message}");
                MessageBox.Show("Error in GetAvailableLotItemList -> " + ex.Message);
            }
            if (bDebug) ClsSerilog.LogMessage(ClsSerilog.Info, $"GetAvailableLotItemList mydatRows = {mydat.Rows.Count}");
            return mydat;
        }


        public async Task<int> GeBaleinDropLineOneAsync(int ConvID)
        {
            int nBale = 0;
            DataTable mydat = new DataTable();
            string connectionString = dbProvider + DB_7760;
            string strQuery = $"SELECT * FROM [Bales] WHERE ConveyorID = {ConvID} AND NumbOnConv > 0";

            try
            {
                await Task.Run(() =>
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {

                        connection.Open();
                        using (OleDbCommand comm = new OleDbCommand(strQuery, connection))
                        {
                            using (OleDbDataReader reader = comm.ExecuteReader())
                            {
                                if (reader.HasRows)
                                    mydat.Load(reader);
                            }
                        }
                        connection.Close();
                    }
                    if (mydat.Rows.Count > 0) nBale = mydat.Rows.Count;
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GeBaleinDropLineOneAsync -> { ex.Message}");
                MessageBox.Show("Error in GeBaleinDropLineOneAsync -> " + ex.Message);
            }
            if (bDebug) ClsSerilog.LogMessage(ClsSerilog.Info, $"GeBaleinDropLineOneAsync return {nBale}");
            return nBale;
        }

        public int GeBaleinDrop(int ConvID)
        {
            int nBale = 0;
            DataTable mydat = new DataTable();
            string connectionString = dbProvider + DB_7760;
            string strQuery = $"SELECT * FROM [Bales] WHERE ConveyorID = {ConvID} AND NumbOnConv > 0";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand comm = new OleDbCommand(strQuery, connection))
                    {
                        using (OleDbDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                mydat.Load(reader);
                        }
                    }
                    connection.Close();
                }
                if (mydat.Rows.Count > 0) nBale = mydat.Rows.Count;
                if (ClassCommon.LayBoy1ConveyorId == 0) nBale += 1; //No drop reset
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GeBaleinDrop -> { ex.Message}");
                MessageBox.Show($"Error in GeBaleinDrop -> {ex.Message}");
            }
            ClsSerilog.LogMessage(ClsSerilog.Info, $"GeBaleinDrop return {nBale}");
            return nBale;
        }

        public string GetNextSerialNumber(string iD)
        {
            string serialNum = string.Empty;
            string connectionString = dbProvider + DB_7760;
            string strQuery = $"SELECT [NextSerNum] FROM [RealTimeItemSerial] WHERE ID = {iD}";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection?.Open();
                    using (OleDbCommand comm = new OleDbCommand(strQuery, connection))
                    {
                        using (OleDbDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) > -1)
                                        serialNum = reader.GetInt32(0).ToString();
                                }
                        }
                    }
                    connection?.Close();
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Access Error in GetNextSerialNumber -> { ex.Message}");
                MessageBox.Show($"Access Error in GetNextSerialNumber -> {ex.Message}");
            }
            return serialNum;
        }

        //-----------------------------------------------------------------------------------------------------------


        public DataTable GetBaleRecIdentCodes(string type)
        {
            DataTable mytable = new DataTable();
            string strQuery = $"SELECT [Type],[FieldExpr],[Format],[BRecFld],[FieldType] FROM [BaleRecIdentCodes] WHERE Type = '{type}'";
            string connectionString = dbProvider + DB_RPSUBJ4;
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, connection))
                    {
                        adapter.Fill(mytable);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetBaleRecIdentCodes -> { ex.Message}");
                MessageBox.Show($"ERROR in GetBaleRecIdentCodes {ex.Message}");
            }
            return mytable;
        }



        public DataTable GetBaleRecDTCodesTable(string type)
        {
            DataTable mytable = new DataTable();
            string strQuery = $"SELECT [Type],[FieldExpr],[Format],[BRecFld] FROM [BaleRecDtTmCodes] WHERE Type = '{type}'";
            string connectionString = dbProvider + DB_RPSUBJ4;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, connection))
                    {
                        adapter.Fill(mytable);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetBaleRecDTCodesTable -> { ex.Message}");
                MessageBox.Show($"ERROR in GetBaleRecDTCodesTable {ex.Message}");
            }
            return mytable;
        }

        public DataTable GetBaleDataTable()
        {
            DataTable mydat = new DataTable();
            string connectionString = dbProvider + DB_7760;

            string strQuery = $"SELECT TOP 25 * FROM [Bales] WHERE NumbOnConv > 0 ORDER by ID";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, connection))
                    {
                        adapter.Fill(mydat);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetBaleDataTable -> { ex.Message}");
                MessageBox.Show($"ERROR in GetBaleDataTable {ex.Message}");
            }
            return mydat;
        }

        public async Task<DataTable> GeBaleDropAsync()
        {
            DataTable mydat = new DataTable();
            string connectionString = dbProvider + DB_7760;
            string strQuery = $"SELECT TOP 1 * FROM [Drops] ORDER by ID";

            try
            {
                await Task.Run(() =>
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, connection))
                        {
                            adapter.Fill(mydat);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GeBaleDropAsync -> { ex.Message}");
                MessageBox.Show($"Error in GeBaleDropAsync -> { ex.Message}");
            }
            return mydat;
        }


        public async Task<DataTable> GetLotTableAsync(List<string> lotitemlist, int lotType)
        {
            DataTable mydat = new DataTable();
            string connectionString = dbProvider + DB_RPJ4;
            string strItems = string.Empty;
            char charsToTrim = ',';
            string strquery = string.Empty;

            foreach (var Item in lotitemlist)
            {
                strItems += Item + ",";
            }

            if (lotType == 0)
                strquery = $"SELECT {strItems.TrimEnd(charsToTrim)} FROM [OpenLots] WHERE LotStatus = 'Open'";
            else if (lotType == 1)
                strquery = $"SELECT {strItems.TrimEnd(charsToTrim)} FROM [LotTransaction] WHERE LotStatus = 'Closed'";
            try
            {
                await Task.Run(() =>
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(strquery, connection))
                        {
                            adapter.Fill(mydat);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetLotTableAsync -> { ex.Message}");
                MessageBox.Show("Error in GetLotTableAsync -> " + ex.Message);
            }
            return mydat;
        }

        public DataTable GetLotTable(List<string> lotitemlist, int lotType)
        {
            DataTable myDat = new DataTable();
            string connectionString = dbProvider + DB_RPJ4;

            string strItems = string.Empty;
            char charsToTrim = ',';
            string strquery = string.Empty;

            foreach (var Item in lotitemlist)
            {
                strItems += Item + ",";
            }
            if (lotType == 0)
                strquery = $"SELECT {strItems.TrimEnd(charsToTrim)} FROM [OpenLots] WHERE LotStatus = 'Open'";
            else if (lotType == 1)
                strquery = $"SELECT {strItems.TrimEnd(charsToTrim)} FROM [LotTransaction] WHERE LotStatus = 'Closed'";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(strquery, connection))
                    {
                        adapter.Fill(myDat);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetLotTable -> { ex.Message}");
                MessageBox.Show("Error in GetLotTable -> " + ex.Message);
            }
            if (bDebug) ClsSerilog.LogMessage(ClsSerilog.Info, $"GetLotTable mydatRows = {myDat.Rows.Count}");
            return myDat;
        }


        public int GetDropSize()
        {
            int dSize = 0;
            DataTable mydat = new DataTable();
            string connectionString = dbProvider + DB_7760;
            string strQuery = $"SELECT * FROM [Conveyors] WHERE Type = 'Approach'";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, connection))
                    {
                        adapter.Fill(mydat);
                    }
                }
                if (mydat.Rows.Count > 0)
                {
                    dSize = Convert.ToInt32(mydat.Rows[0]["Length"]);
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetDropSize -> { ex.Message}");
                MessageBox.Show($"ERROR in GetDropSize {ex.Message}");
            }
            if (bDebug) ClsSerilog.LogMessage(ClsSerilog.Info, $"GetDropSize return {dSize} mydat.Rows.Count= {mydat.Rows.Count} strQuery= {strQuery}");
            return dSize;
        }

        public DataTable GetDropFromConv(string queryOne)
        {
            DataTable MyDropTable = new DataTable();
            string connectionString = dbProvider + DB_CFG7760;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(queryOne, connection))
                    {
                        adapter.Fill(MyDropTable);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetBaleStationsTableList -> { ex.Message}");
                MessageBox.Show($"ERROR in GetBaleStationsTableList {ex.Message}");
            }
            if (bDebug) ClsSerilog.LogMessage(ClsSerilog.Info, $"GetBaleStationsTableList return MyDropTable.Rows.Count ={MyDropTable.Rows.Count}");
            return MyDropTable;
        }

        public DataTable GetAccessTable(string tableName)
        {
            DataTable StockTable = new DataTable();
            string connectionString = dbProvider + DB_STOCK;
            string strQuery = $"SELECT * FROM [{tableName}]";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, connection))
                {
                    adapter.Fill(StockTable);
                }
            }
            return StockTable;
        }

        public string GetBalesInStack(int line, int cId)
        {
            string bales = string.Empty;

            DataTable mydat = new DataTable();
            string connectionString = dbProvider + DB_7760;

            string strQuery = $"SELECT top 20 * FROM [Bales] WHERE ConveyorID = {cId} AND LineID = {line} ORDER by ID";
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, connection))
                    {
                        adapter.Fill(mydat);
                    }
                }

                if (mydat.Rows.Count > 0) bales = mydat.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetBalesInStack -> { ex.Message}");
                MessageBox.Show($"ERROR in GetBalesInStack {ex.Message}");
            }
            return bales;
        }

    }
}
