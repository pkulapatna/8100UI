using _8100UI.Services.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _8100UI.Services
{
    public class Sqlhandler
    {
        private const int BALE_ARCHIVE = 0;

        private const int LOT_ARCHIVE = 1;

        public string currentAchivesTable = "BaleArchive" + DateTime.Now.ToString("MMMyy");
        public string previousArchivesTable = "BaleArchive" + DateTime.Now.AddMonths(-1).ToString("MMMyy");

        public string currentLotTable = "LotArchive" + DateTime.Now.ToString("MMMyy");
        public string previousLotTable = "LotArchive" + DateTime.Now.AddMonths(-1).ToString("MMMyy");

        public string SqlAuRtCon { get; set; }
        public string SqlAuProdCon { get; set; }
        public string SqlAuMMSCon { get; set; }
        public string SqlAuForteSysCon { get; set; }

        public string StrUserName { get; set; }
        public string StrPassWrd { get; set; }
        public string StrHostID { get; set; }
        public string StrInstance { get; set; }
        public string WorkStationID { get; set; }
        public string TargetWorkStationID { get; set; }
        public int RawsDept { get; set; } = 20;

        //Sql Database
        public const string MASTER_DB = "Master";
        public const string REALTIME_DB = "Fortedata";
        public const string PULPGRADE_DB = "PulpGrade";
        public const string PROCESSTRAN_DB = "ForteTrans";

        public const string FORTEMMS_DB = "ForteMMS";
        public const string FORTESYSTEM_DB = "Forte_System";

        //SQL script to create table
        public const string SQLCALTABLE = "CalTable";
        public const string SQLLOTARCHSTABLE = "LotArchive";
        public const string SQLOPENLOTSTABLE = "LotOpen";
        public const string SQLCLOSELOTSTABLE = "LotClose";

        //Tables in  ForteMMS DB
        public const string BALEARCHIVETABLE = "BaleArchive";
        public const string BALEPOOLTABLE = "BalePool";

        public const string LINESTABLE = "Lines";
        public const string RTITEMSSERIALTABLE = "RealTimeItemSerial";
        public const string RTLOTPROCESSTABLE = "RealTimeLotProc";
        public const string SCHEMATICTABLE = "Schematic";
        public const string SERIALDEVICESTABLE = "SerialDevices";
        public const string SOURCETABLE = "Sources";

        //Table's Name in  Forte_System DB
        public const string FS_CONVEYORTABLE = "Conveyors";
        public const string FS_STATIONTABLE = "Stations";
        public const string FS_GRADETABLE = "GradeTable";
        public const string FS_STOCKTABLE = "stockTable";
        public const string FS_OPENLOTTABLE = "OpenLots";
        public const string FS_BALETABLE = "Bales";
        public const string FS_LOTTRANTABLE = "Lottransaction";

        public List<string> RemoveFieldsList = null;
        //  private string createTableQuery = string.Empty;
        private static Sqlhandler instance = null;
        private static readonly object padlock = new object();

        // public const string BALESTABLE = "Bales";


        private string MasterConStr;
        private string WinAuProdConStr;
        private string WinAuRealtimeConStr;
       // private string WinAuTransConStr;

        private string SqlAuRealtimeConStr;



        public List<int> GetStationsInfo()
        {
            List<int> Stations = new List<int>();

            string strQuery = "SELECT DISTINCT ProcessingLine From [Stations] with(NOLOCK) Where ProcessingLine > 0 ;";

            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (var command = new SqlCommand(strQuery, sqlConnection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    if (reader != null)
                                        Stations.Add(Convert.ToInt32(reader[0]));
                                }
                        }
                    }
                    sqlConnection?.Close();
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in SQL Handler GetStationsInfo -> { ex.Message}");
                throw new Exception("Error in SQL Handler GetStationsInfo ->", ex);  
            }
            return Stations;
        }



        public DataTable GetDayTable(List<string> dayitemlist, int dayType)
        {
            DataTable mytable = new DataTable();
            string strItems = string.Empty;
            char charsToTrim = ',';
            string strquery = string.Empty;
            string DayTable = "DayProd";

            foreach (var Item in dayitemlist)
            {
                strItems += Item + ",";
            }
            if (dayType == 3)
                strquery = $"SELECT {strItems.TrimEnd(charsToTrim)} FROM [{DayTable}] with(NOLOCK) WHERE LotStatus = 'Open'";
            else if (dayType == 4)
                strquery = $"SELECT TOP 20  {strItems.TrimEnd(charsToTrim)} FROM [{DayTable}] with(NOLOCK) WHERE LotStatus = 'Closed' ORDER BY [index] DESC";
            else if (dayType == 5)
                strquery = $"SELECT  Top 1 {strItems.TrimEnd(charsToTrim)} FROM [DayTotalsRevA] with(NOLOCK)";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                mytable.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                }
                
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetDayTable -> { ex.Message}");
                throw new Exception("ERROR in GetDayTable.", ex);
                //MessageBox.Show($"Error in GetDayTable -> {ex.Message}");
            }
            return mytable;  
        }

        public string GetCurrentUser()
        {
            string username = string.Empty;
            string strquery = $"SELECT [User] FROM [CurrentUser];";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    username = (reader.GetString(0));
                                }
                        }
                    }
                    sqlConnection?.Close();
                }
                return username;
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetCurrentUser -> { ex.Message}");
                throw new Exception("ERROR in GetCurrentUser.", ex);
                //MessageBox.Show($"Error in GetCurrentUser -> {ex.Message}");
            }
        }

        public List<int> GetCurrentBalesOnConveyor(int conveyorId)
        {
            List<int> NumBale = new List<int>();
            string strquery = $"SELECT Position FROM [Bales]  with(NOLOCK) WHERE ConveyorID = {conveyorId} AND NumbOnConv > 0";
            DataTable MyTable = new DataTable();

            //ClsSerilog.LogMessage(ClsSerilog.Info, $"GetCurrentBalesOnConveyor -> { strquery}");

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                MyTable.Load(reader);
                            if (MyTable.Rows.Count > 0)
                            {
                                for(int i = 0; i < MyTable.Rows.Count; i++ )
                                {
                                    if(MyTable.Rows[i][0] != null)
                                        NumBale.Add(Convert.ToInt32(MyTable.Rows[i][0]));
                                }
                            }
                        }
                    }
                    sqlConnection?.Close();
                    /*
                    if( conveyorId == ClassCommon.ApproachConveyorLineOne)
                    {
                        for(int i = 0; i < NumBale.Count; i++ )
                        {
                            ClsSerilog.LogMessage(ClsSerilog.Debug, $"ConvoyorID = {conveyorId} NumBale -> { NumBale[i].ToString()}");
                        }
                    }*/
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetCurrentBalesOnConveyor -> { ex.Message}");
                throw new Exception("ERROR in GetCurrentBalesOnConveyor.", ex);
                //MessageBox.Show($"Error in GetCurrentBalesOnConveyor -> {ex.Message}");
               
            }
            return NumBale;
        }

        public List<ClsConveyor> GetConveyorsTableList()
        {
            List<ClsConveyor> Conveyors = new List<ClsConveyor>();
          //  int cnt = 0;
          //  int DeviceCnt;
         //   int iLayboy = FindLayboy(1);
            //int iArr;

            DataTable ConTable = new DataTable();

            string strquery = $"SELECT * FROM [Conveyors] with(NOLOCK) ORDER bY ID ASC;";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                ConTable.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                    
                    if(ConTable.Rows.Count > 0)
                    {
                        for(int i = 0; i < ConTable.Rows.Count; i++ )
                        {
                            Conveyors.Add(new ClsConveyor(Convert.ToInt32(ConTable.Rows[i]["ID"].ToString()),
                                   ConTable.Rows[i]["Name"].ToString(),
                                    Convert.ToInt32(ConTable.Rows[i]["Length"].ToString()),
                                    Convert.ToInt32(ConTable.Rows[i]["Counter"].ToString()),
                                    Convert.ToInt32(ConTable.Rows[i]["FirstBale"].ToString()),
                                    Convert.ToInt32(ConTable.Rows[i]["LastBale"].ToString()),
                                    Convert.ToInt32(ConTable.Rows[i]["ProcessingLine"].ToString()),
                                    ConTable.Rows[i]["Type"].ToString()
                                    ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"SQL Error in GetConveyorsTableList -> { ex.Message}");
                throw new Exception("SQL ERROR in GetConveyorsTableList.", ex);
            }
            return Conveyors;
        }

        private int FindLayboy(int iLine)
        {
            int Iret = 0;
            string strquery = $"SELECT * FROM [Stations] WHERE StationTypeID = '90' AND ProcessingLine ='{iLine}' ORDER BY [ID] ASC";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    Iret = Convert.ToInt32(reader.GetValue(0));
                                }
                        }
                    }
                    sqlConnection?.Close();
                }
            }
            catch (Exception ex )
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"SQL Error in GetConveyorsTableList -> { ex.Message}");
                throw new Exception("SQL ERROR in GetConveyorsTableList.", ex);
            }
            return Iret;
        }

        public DataTable GetRealTimeLotProc(string strLotquery)
        {
            DataTable Lottable = new DataTable();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strLotquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                Lottable.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                }
            }
            catch (Exception ex )
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetRealTimeLotProc -> { ex.Message}");
                throw new Exception("ERROR in GetRealTimeLotProc.", ex);
            }
            return Lottable;
        }

        public int GetBalesOnConveyor(int stationId)
        {
            int BaleCount = 0;
            string strquery = $"SELECT Length FROM [Conveyors] with(NOLOCK) WHERE Station2ID= '{stationId}';";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    BaleCount = Convert.ToInt32(reader.GetValue(0));
                                }
                        }
                    }
                    sqlConnection?.Close();
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetBalesOnConveyor -> { ex.Message}");
                throw new Exception("ERROR in GetBalesOnConveyor.", ex);
                //MessageBox.Show($"Error in GetBalesOnConveyor -> {ex.Message}");
            }
            return BaleCount;
        }

        public async Task<DataTable> GetDayTableAsync(List<string> dayitemlist, int dayType)
        {
            DataTable mytable = new DataTable();
            string strItems = string.Empty;
            char charsToTrim = ',';
            string strquery = string.Empty;
            string DayTable = "DayProd";

            foreach (var Item in dayitemlist)
            {
                strItems += Item + ",";
            }
            if (dayType == 3)
                strquery = $"SELECT {strItems.TrimEnd(charsToTrim)} FROM [{DayTable}] with(NOLOCK) WHERE LotStatus = 'Open'";
            else if (dayType == 4)
                strquery = $"SELECT TOP 20  {strItems.TrimEnd(charsToTrim)} FROM [{DayTable}] with(NOLOCK) WHERE LotStatus = 'Closed' ORDER BY [index] DESC";

            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                        {
                            adapter.Fill(mytable);
                        }
                    }
                });
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetDayTableAsync -> { ex.Message}");
                MessageBox.Show($"Error in GetDayTableAsync -> {ex.Message}");
            }
            return mytable;
        }

        public DataTable GetStationGraphicsTableList()
        {
            DataTable MyTable = new DataTable();
            string strquery = $"SELECT * FROM [StationGraphics] with(NOLOCK) ORDER By [UniqueID] ASC";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                MyTable.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                }
                return MyTable;
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetStationGraphicsTableList -> { ex.Message}");
                ClsSerilog.LogMessage(ClsSerilog.Error, $"strQuery -> { strquery}");
                throw new Exception("ERROR in GetStationGraphicsTableList.", ex);
            }
            finally {
            }
        }


        internal DataTable GetBaleStationsTable(string strquery)
        {
            DataTable MyStationTable = new DataTable();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                MyStationTable.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                }
                return MyStationTable;
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GeBaleDataTableAsync -> { ex.Message}");
                ClsSerilog.LogMessage(ClsSerilog.Error, $"strQuery -> { strquery}");
                throw new Exception("ERROR in GetBaleStationsTableList.", ex);
            } 
        }

        public string GetProductName(string strName)
        {
            string StrName = string.Empty;
            string strquery = string.Empty;
            DataTable mytable = new DataTable();

            if (strName == "Stock")
                strquery = $"SELECT top  1 * from Sources with (NOLOCK);";
            else if (strName == "Grade")
                strquery = $"SELECT top  1 * from Lines with (NOLOCK);";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                mytable.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                }

                if (mytable.Rows.Count > 0)
                {
                    if (strName == "Stock")
                        StrName = mytable.Rows[0].Field<string>("StockName").ToString();

                    else if (strName == "Grade")
                        StrName = mytable.Rows[0].Field<string>("Name").ToString();
                }
                return StrName;
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in SQL GetProductName -> { ex.Message}");
                ClsSerilog.LogMessage(ClsSerilog.Error, $"strQuery -> { strquery}");
                throw new Exception("ERROR in SQL GetProductName.", ex);
            } 
        }

        public async Task<DataTable> GeBaleDataTableAsync()
        {
            DataTable mytable = new DataTable();
            string strquery = $"SELECT TOP 20 * FROM [Bales] with(NOLOCK) ORDER by ID";

            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                        {
                            adapter.Fill(mytable);
                        }
                    }
                });  
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in SQL GeBaleDataTableAsync -> { ex.Message}");
                throw;
               //  MessageBox.Show($"Error in SQL GeBaleDataTableAsync -> {ex.Message}");

                //ClsSerilog.LogMessage(ClsSerilog.Error, $"strQuery -> { strquery}");
                //throw new Exception("ERROR in SQL GeBaleDataTableAsync.", ex);
            }
            return mytable;
        }

        public DataTable GetBaleDatatable()
        {
            DataTable mytable = new DataTable();
            string strquery = $"SELECT TOP 20 * FROM [Bales] with(NOLOCK) ORDER by ID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                    {
                        adapter.Fill(mytable);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in SQL GetBaleDatatable -> { ex.Message}");
                ClsSerilog.LogMessage(ClsSerilog.Error, $"strQuery -> { strquery}");
                throw new Exception("ERROR in SQL GetBaleDatatable.", ex);
                //MessageBox.Show($"Error in SQL GetBaleDatatable -> {ex.Message}");
            }
            return mytable;
        }


        public DataTable GetBaleDatatable_old()
        {
            
            DataTable mytable = new DataTable();
            string strquery = $"SELECT TOP 10 * FROM [Bales] with(NOLOCK) ORDER by ID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                mytable.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in SQL GetBaleDatatable -> { ex.Message}");
                ClsSerilog.LogMessage(ClsSerilog.Error, $"strQuery -> { strquery}");
                throw new Exception("ERROR in SQL GetBaleDatatable.", ex);
                //MessageBox.Show($"Error in SQL GetBaleDatatable -> {ex.Message}");
            }
            return mytable;
        }


        public DataTable GetStockSourcesTable(string strTable)
        {
            DataTable myTable = new DataTable();
            string strQuery = $"SELECT * FROM  [{strTable}] with(NOLOCK)";

            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strQuery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                myTable.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"ERROR in GetStockSourcesTable {ex.Message}");
            }
            return myTable;
        }

        public DataTable GetDayPior(List<string> dayitemlist, int dayType)
        {
            DataTable mytable = new DataTable();

            string strItems = string.Empty;
            char charsToTrim = ',';
            string strquery = string.Empty;

            string DayTable = "DayProd";

            foreach (var Item in dayitemlist)
            {
                strItems += Item + ",";
            }
            if (dayType == 3)
                strquery = $"SELECT {strItems.TrimEnd(charsToTrim)} FROM [{DayTable}] with(NOLOCK) WHERE LotStatus = 'Open'";
            else if (dayType == 4)
                strquery = $"SELECT TOP 20  {strItems.TrimEnd(charsToTrim)} FROM [{DayTable}] with(NOLOCK) WHERE LotStatus = 'Closed' ORDER BY [index] DESC";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {

                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                mytable.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetDayPior -> { ex.Message}");
                throw new Exception("ERROR in GetDayPior.", ex);
                //MessageBox.Show($"Error in GetDayPior -> {ex.Message}");
            }
            return mytable;
        }


        public async Task<DataTable> GetDayPiorAsync(List<string> dayitemlist, int dayType)
        {
            DataTable mytable = new DataTable();
            string strItems = string.Empty;
            char charsToTrim = ',';
            string strquery = string.Empty;

            string DayTable = "DayProd";

            foreach (var Item in dayitemlist)
            {
                strItems += Item + ",";
            }
            if (dayType == 3)
                strquery = $"SELECT {strItems.TrimEnd(charsToTrim)} FROM [{DayTable}] with(NOLOCK) WHERE LotStatus = 'Open'";
            else if (dayType == 4)
                strquery = $"SELECT TOP 20  {strItems.TrimEnd(charsToTrim)} FROM [{DayTable}] with(NOLOCK) WHERE LotStatus = 'Closed' ORDER BY [index] DESC";

            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                    {

                        sqlConnection?.Open();
                        using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                        {
                            using (SqlDataReader reader = comm.ExecuteReader())
                            {
                                if (reader.HasRows)
                                    mytable.Load(reader);
                            }
                        }
                        sqlConnection?.Close();
                    }
                });
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetDayPiorAsync -> { ex.Message}");
                throw new Exception("ERROR in GetDayPiorAsync.", ex);
                //MessageBox.Show($"Error in GetDayPiorAsync -> {ex.Message}");
            }
            return mytable;
        }

        public async Task<DataTable> GetShiftTableAsync(List<string> fielditems, int openDayTable)
        {
            DataTable mytable = new DataTable();
            string strItems = string.Empty;
            char charsToTrim = ',';
            string strquery = string.Empty;
            string ShiftTable = "ShiftProd";

            foreach (var Item in fielditems)
            {
                strItems += Item + ",";
            }

            if (openDayTable == 6)
                strquery = $"SELECT {strItems.TrimEnd(charsToTrim)} FROM [{ShiftTable}] with(NOLOCK) WHERE LotStatus = 'Open'";
            else if (openDayTable == 7)
                strquery = $"SELECT TOP 20  {strItems.TrimEnd(charsToTrim)} FROM [{ShiftTable}] with(NOLOCK) WHERE LotStatus = 'Closed' ORDER BY [index] DESC";

            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                        {
                            adapter.Fill(mytable);
                        }
                    }
                });
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetShiftTableAsync -> { ex.Message}");
                throw new Exception("ERROR in GetShiftTableAsync.", ex);
               // MessageBox.Show($"Error in GetShiftTableAsync -> {ex.Message}");
            }
            return mytable;
        }


        public DataTable GetShiftTable(List<string> fielditems, int openDayTable)
        {
            DataTable MyTable = new DataTable();
            string strItems = string.Empty;
            char charsToTrim = ',';
            string strquery = string.Empty;
            string ShiftTable = "ShiftProd";

            foreach (var Item in fielditems)
            {
                strItems += Item + ",";
            }
            if (openDayTable == 6)
                strquery = $"SELECT {strItems.TrimEnd(charsToTrim)} FROM [{ShiftTable}] with(NOLOCK) WHERE LotStatus = 'Open'";
            else if (openDayTable == 7)
                strquery = $"SELECT TOP 20  {strItems.TrimEnd(charsToTrim)} FROM [{ShiftTable}] with(NOLOCK) WHERE LotStatus = 'Closed' ORDER BY [index] DESC";
            else if (openDayTable == 8)
                strquery = $"SELECT TOP 20  {strItems.TrimEnd(charsToTrim)} FROM [ShiftTotalsRevA] with(NOLOCK)";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                MyTable.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetShiftTable -> { ex.Message}");
                throw new Exception("ERROR in GetShiftTable.", ex);
                //MessageBox.Show($"Error in GetShiftTable -> {ex.Message}");
            }
            return MyTable;
        }

        public DataTable GetShiftPior(List<string> fielditems, int openDayTable)
        {
            DataTable mytable = new DataTable();
            string strItems = string.Empty;
            char charsToTrim = ',';
            string strquery = string.Empty;

            string ShiftTable = "ShiftProd";

            foreach (var Item in fielditems)
            {
                strItems += Item + ",";
            }

            if (openDayTable == 6)
                strquery = $"SELECT {strItems.TrimEnd(charsToTrim)} FROM [{ShiftTable}] with(NOLOCK) WHERE LotStatus = 'Open'";
            else if (openDayTable == 7)
                strquery = $"SELECT TOP 20  {strItems.TrimEnd(charsToTrim)} FROM [{ShiftTable}] with(NOLOCK) WHERE LotStatus = 'Closed' ORDER BY [index] DESC";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                mytable.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in ReadXmlGridView -> { ex.Message}");
                throw new Exception("ERROR in GetShiftPiorAsync.", ex);
                //MessageBox.Show($"Error in GetShiftPiorAsync -> {ex.Message}");
            }
            return mytable;
        }

        public async Task<DataTable> GetShiftPiorAsync(List<string> fielditems, int openDayTable)
        {
            DataTable mytable = new DataTable();
            string strItems = string.Empty;
            char charsToTrim = ',';
            string strquery = string.Empty;
            string ShiftTable = "ShiftProd";

            foreach (var Item in fielditems)
            {
                strItems += Item + ",";
            }

            if (openDayTable == 6)
                strquery = $"SELECT {strItems.TrimEnd(charsToTrim)} FROM [{ShiftTable}] with(NOLOCK) WHERE LotStatus = 'Open'";
            else if (openDayTable == 7)
                strquery = $"SELECT TOP 20  {strItems.TrimEnd(charsToTrim)} FROM [{ShiftTable}] with(NOLOCK) WHERE LotStatus = 'Closed' ORDER BY [index] DESC";

            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                    {
                        sqlConnection?.Open();
                        using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                        {
                            using (SqlDataReader reader = comm.ExecuteReader())
                            {
                                if (reader.HasRows)
                                    mytable.Load(reader);
                            }
                        }
                        sqlConnection?.Close();
                    }
                });
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetShiftPiorAsync -> { ex.Message}");
                throw new Exception("ERROR in GetShiftPiorAsync.", ex);
                //MessageBox.Show($"Error in GetShiftPiorAsync -> {ex.Message}");
            }
            return mytable;
        }

        public DataTable GetOpenLotTable(List<string> lotitemlist, int lotType)
        {
            DataTable openLottable = new DataTable();
            string strItems = string.Empty;
            char charsToTrim = ',';
            string strquery;
            int lotdount = (ClassCommon.IniLotSummaryDepth > 0) ? ClassCommon.IniLotSummaryDepth : 21;

            if (lotitemlist.Count > 0)
            {

                foreach (var Item in lotitemlist)
                {
                    strItems += Item + ",";
                }
                if (lotType == 0)
                    strquery = $"SELECT Top {lotdount} {strItems.TrimEnd(charsToTrim)} FROM [OpenLots] with(NOLOCK) WHERE LotStatus = 'Open'  ORDER BY UID DESC SET STATISTICS TIME ON;";
                else
                    strquery = $"SELECT Top {lotdount} {strItems.TrimEnd(charsToTrim)} FROM [OpenLots] with(NOLOCK) WHERE LotStatus = 'Closed' ORDER BY UID DESC SET STATISTICS TIME ON;";
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                    {
                        sqlConnection?.Open();
                        using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                        {
                            using (SqlDataReader reader = comm.ExecuteReader())
                            {
                                if (reader.HasRows)
                                    openLottable.Load(reader);
                            }
                        }
                        sqlConnection?.Close();
                    }
                    /*
                    using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                        {
                            adapter.Fill(openLottable);
                        }
                    }
                    */
                }
                catch (SqlException ex)
                {
                    ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetSQLOpenLotTable -> { ex.Message}");
                    ClsSerilog.LogMessage(ClsSerilog.Error, $"-----------------------------------");
                    ClsSerilog.LogMessage(ClsSerilog.Error, $"String Query=->  { strquery}");
                    ClsSerilog.LogMessage(ClsSerilog.Error, $"-----------------------------------");
                    throw new SyntaxErrorException("ERROR in GetOpenLotTable.", ex);
                    //MessageBox.Show($"Error in GetSQLOpenLotTable -> {ex.Message}");
                }
            }
            return openLottable;
        }



        public DataTable GetPriodTable(List<string> fielditems, int tableType)
        {
            DataTable PeriodTable = new DataTable();
            string strquery = string.Empty;
            string strItems = string.Empty;
            char charsToTrim = ',';

            if (fielditems.Count > 0)
            {
                foreach (var Item in fielditems)
                {
                    strItems += Item + ",";
                }
            }
                if (tableType == 10)
                strquery = $"SELECT  {strItems.TrimEnd(charsToTrim)} FROM [PeriodProd] with(NOLOCK) WHERE LotStatus = 'Open'  SET STATISTICS TIME ON;";
            else if (tableType == 11)
                strquery = $"SELECT  {strItems.TrimEnd(charsToTrim)} FROM [PeriodProd] with(NOLOCK) WHERE LotStatus = 'Close'  SET STATISTICS TIME ON;";
            else if (tableType == 12)
                strquery = $"SELECT  {strItems.TrimEnd(charsToTrim)} FROM [PeriodTotalsRevA] with(NOLOCK) SET STATISTICS TIME ON;";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                PeriodTable.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetPriodTable -> { ex.Message}");
                MessageBox.Show("Error in GetPriodTable -> " + ex.Message);
            }
            return PeriodTable;
        }



        public List<Tuple<string, string>> GetTableSchema()
        {
            string ArchiveTable = Sqlhandler.FS_BALETABLE + DateTime.Now.ToString("MMMyy");

            DataTable dx = new DataTable();
            List<Tuple<string, string>> mylist = new List<Tuple<string, string>>();
            string strQuery = "SELECT ORDINAL_POSITION,COLUMN_NAME,DATA_TYPE FROM ForteData.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" + ArchiveTable + "'";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(WinAuRealtimeConStr))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(strQuery, sqlConnection))
                    {
                        adapter.Fill(dx);
                    }
                }
                foreach (var item in this.RemoveFieldsList)
                {
                    RemoveHrdItem(dx, item);
                }

                foreach (DataRow item in dx.Rows)
                {
                    mylist.Add(new Tuple<string, string>(item[1].ToString(), item[2].ToString()));
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetTableSchema -> { ex.Message}");
                MessageBox.Show("Error in GetTableSchema -> " + ex.Message);
            }
            return mylist;
        }

     

        public DataTable GetLotTable(List<string> lotitemlist, int lotType)
        {
            DataTable mytable = new DataTable();
            string strItems = string.Empty;
            char charsToTrim = ',';
            string strquery = string.Empty;
            int lotdount = (ClassCommon.IniLotSummaryDepth > 0) ? ClassCommon.IniLotSummaryDepth : 21;
            string lottable = "LotArchive" + DateTime.Now.ToString("MMMyy");

            foreach (var Item in lotitemlist)
            {
                strItems += Item + ",";
            }

            if (lotType == 0)
                strquery = $"SELECT Top {lotdount} {strItems.TrimEnd(charsToTrim)} FROM [{lottable}] with(NOLOCK) WHERE LotStatus = 'Open' ORDER BY UID DESC";
            else if (lotType == 1)
                strquery = $"SELECT Top {lotdount} {strItems.TrimEnd(charsToTrim)} FROM [{lottable}] with(NOLOCK) WHERE LotStatus = 'Closed' ORDER BY UID DESC";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuRtCon))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                    {
                        adapter.Fill(mytable);
                    }
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetLotTable -> { ex.Message}");
                //throw new SyntaxErrorException("ERROR in GetLotTable.", ex);
                MessageBox.Show($"Error in GetLotTable -> {ex.Message}");
            }
            return mytable;
        }


        public async Task<DataTable> GetLotTableAsync(List<string> lotitemlist, int lotType)
        {
            DataTable mytable = new DataTable();
            string strItems = string.Empty;
            char charsToTrim = ',';
            string strquery = string.Empty;
            int lotdount = (ClassCommon.IniLotSummaryDepth > 0) ? ClassCommon.IniLotSummaryDepth : 21;
            string lottable = "LotArchive" + DateTime.Now.ToString("MMMyy");

            foreach (var Item in lotitemlist)
            {
                strItems += Item + ",";
            }

            if (lotType == 0)
                strquery = $"SELECT Top {lotdount} {strItems.TrimEnd(charsToTrim)} FROM [{lottable}] with(NOLOCK) WHERE LotStatus = 'Open' ORDER BY UID DESC";
            else if (lotType == 1)
                strquery = $"SELECT Top {lotdount} {strItems.TrimEnd(charsToTrim)} FROM [{lottable}] with(NOLOCK) WHERE LotStatus = 'Closed' ORDER BY UID DESC";

            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection sqlConnection = new SqlConnection(SqlAuRtCon))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                        {
                            adapter.Fill(mytable);
                        }
                    }
                });
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in ReadXmlGridView -> { ex.Message}");
                MessageBox.Show($"Error in GetLotTableAsync -> {ex.Message}");
            }
            return mytable;
        }

        public void SendDataToMain(string newvalue)
        {
            ClassCommon.MessageId += 1;
            if (ClassCommon.MessageId > 9999) ClassCommon.MessageId = 1;

            string strInsert = $"INSERT INTO ToForteService ([ID],[Msg],[AppName]) VALUES ({ClassCommon.MessageId},'{newvalue}','Forte8100UI;')";
            ExecuteCommand(strInsert, SqlAuForteSysCon);
        }


        public DataTable GetSqlLotScema()
        {
            string LotTable = Sqlhandler.SQLLOTARCHSTABLE + DateTime.Now.ToString("MMMyy");
            DataTable dx = new DataTable();
            string strQuery = "SELECT ORDINAL_POSITION,COLUMN_NAME,DATA_TYPE FROM ForteData.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" + LotTable + "'";

            try
            {
                using (var sqlConnection = new SqlConnection(WinAuRealtimeConStr))
                {
                    using (var adapter = new SqlDataAdapter(strQuery, sqlConnection))
                    {
                        adapter.Fill(dx);
                    }
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetSqlLotScema -> { ex.Message}");
                MessageBox.Show("Error in GetSqlLotScema -> " + ex.Message);
            }
            return dx;
        }


        public async Task<DataTable> GetArchiveTableAsync(string strquery)
        {
            DataTable mytable = new DataTable();
            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection sqlConnection = new SqlConnection(SqlAuRealtimeConStr))
                    {
                        sqlConnection?.Open();
                        using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                        {
                            using (SqlDataReader reader = comm.ExecuteReader())
                            {
                                if (reader.HasRows)
                                    mytable.Load(reader);
                            }
                        }
                        sqlConnection?.Close();
                    }
                });
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetArchiveTableAsync -> {ex.Message}");
                throw new Exception("ERROR in GetArchiveTableAsync.", ex);
                //MessageBox.Show("Error in GetForteDataTable -> " + ex.Message);
            }
            return mytable;
        }




        public async Task<DataTable> GeForteSystemTableAsync(string strquery)
        {
            DataTable mytable = new DataTable();
            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                    {
                        sqlConnection?.Open();
                        using (SqlCommand comm = new SqlCommand(strquery, sqlConnection))
                        {
                            using (SqlDataReader reader = comm.ExecuteReader())
                            {
                                if (reader.HasRows)
                                    mytable.Load(reader);
                            }
                        }
                        sqlConnection?.Close();
                    }
                });
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GeForteSystemTableAsync -> {ex.Message}");
                throw new Exception("ERROR in GeForteSystemTableAsync.", ex);
                //MessageBox.Show("Error in GetForteDataTable -> " + ex.Message);
            }
            return mytable;
        }



        public List<string> GetServerList()
        {
            List<string> ServerList = new List<string>();
            ServerList.Clear();

            try
            {
                System.Data.Sql.SqlDataSourceEnumerator instance = System.Data.Sql.SqlDataSourceEnumerator.Instance;
                System.Data.DataTable table = instance.GetDataSources();


                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        if (row[1].ToString() == "SQLEXPRESS")
                            ServerList.Add(row[0].ToString() + @"\" + row[1].ToString());
                    }
                }

            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetServerList -> { ex.Message}");
                throw new Exception("ERROR in GetServerList.", ex);
                //MessageBox.Show($"Error in GetServerList -> {ex.Message}");
            }
            return ServerList;
        }

        public async Task<DataTable> GetForteDataTableAsync(string strquery)
        {
            DataTable mytable = new DataTable();
            try
            {
                await Task.Run(() =>
                {

                    using (SqlConnection sqlConnection = new SqlConnection(WinAuRealtimeConStr))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                        {
                            adapter.Fill(mytable);
                        }
                    }
                });
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in ReadXmlGridView -> { ex.Message}");
                MessageBox.Show("Error in GetArchiveTableAsync -> " + ex.Message);
            }
            return mytable;
        }

        public bool TestSqlConnection(string host, string instant, string database, string userid, string password)
        {
            string Source = host + @"\" + instant;
            string constring = "Data Source = '" + Source + "'; Database = " + database + "; user id = '" + userid +
                                "'; Password = '" + password + "'; connection timeout=30;Persist Security Info=True;";
            bool bConnected = false;

            try
            {
                using (var sqlConnection = new SqlConnection(constring))
                {
                    sqlConnection?.Open();
                    bConnected = true;
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in TestSqlConnection -> { ex.Message}");
                throw new Exception("ERROR in TestSqlConnection.", ex);
                //MessageBox.Show("Error in TestSqlConnection " + ex.Message);
            }
            return bConnected;

        }

        public async Task<DataTable> GeConveyorTableAsync(string dattable)
        {
            string strquery = $"SELECT * fROM {dattable} ORDER by ID ASC";
            DataTable mytable = new DataTable();
            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                        {
                            adapter.Fill(mytable);
                        }
                    }
                });
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GeConveyorTableAsync -> { ex.Message}");
                MessageBox.Show("Error in GeConveyorTableAsync -> " + ex.Message);
            }
            return mytable;
        }


        public async Task<DataTable> GeBaleTableAsync(string dattable)
        {
            //string strquery = $"SELECT * fROM {dattable}  WHERE NumbOnConv > 0 AND PreviousID > 0 AND NextID > 0";

            string strquery = $"SELECT Top 20 * fROM {dattable} with(NOLOCK) WHERE NumbOnConv > 0 ORDER by ID";

            DataTable mytable = new DataTable();
            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                        {
                            adapter.Fill(mytable);
                        }
                    }
                });
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GeBaleTableAsync -> { ex.Message}");
                MessageBox.Show("Error in GeBaleTableAsync -> " + ex.Message);
            }
            return mytable;
        }

        public async Task<DataTable> GetSheetCountTableAsync()
        {
            DataTable mytable = new DataTable();
            string strquery = $"SELECT Top 1 * fROM SheetCountParams with(NOLOCK)";

            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                        {
                            adapter.Fill(mytable);
                        }
                    }
                });

            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetSheetCountTableAsync -> { ex.Message}");
                MessageBox.Show($"Error in GetSheetCountTableAsync -> {ex.Message}");
            }
            return mytable;
        }

        public DataTable GetSheetCountTable()
        {
            DataTable mytable = new DataTable();
            string strquery = $"SELECT Top 1 * fROM SheetCountParams with(NOLOCK)";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                    {
                        adapter.Fill(mytable);
                    }
                }

            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetSheetCountTable -> { ex.Message}");
                throw new Exception("ERROR in GetSheetCountTable.", ex);
                //MessageBox.Show($"Error in GetSheetCountTable -> {ex.Message}");
            }
            return mytable;
        }


        public async Task<DataTable> GetDropParamTableAsync()
        {
            DataTable mytable = new DataTable();
            string strquery = $"SELECT Top 1 * fROM DropParams with(NOLOCK)";

            await Task.Run(() =>
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                    {
                        adapter.Fill(mytable);
                    }
                }
            });
            return mytable;
        }

        public DataTable GetDropParamTable()
        {
            DataTable mytable = new DataTable();
            string strquery = $"SELECT Top 1 * fROM DropParams with(NOLOCK)";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                    {
                        adapter.Fill(mytable);
                    }
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetDropParamTable -> { ex.Message}");
                throw new Exception("ERROR in GetDropParamTable.", ex);
                //MessageBox.Show($"Error in GetDropParamTable -> {ex.Message}");
            }
            return mytable;
        }


        public static Sqlhandler Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Sqlhandler();
                    }
                    return instance;
                }
            }
        }

        public Sqlhandler()
        {
            InitialSetupSqlDataBase();
            SetRemoveFields();
        }



        public DataTable GetSqlScema()
        {
            DataTable dx = new DataTable();

            string strQuery = "SELECT ORDINAL_POSITION,COLUMN_NAME,DATA_TYPE " +
                $" FROM ForteData.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'{GetCurrentBaleTableName()}'";
            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuRtCon))
                {
                    sqlConnection?.Open();
                    using (SqlCommand comm = new SqlCommand(strQuery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dx.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                }

            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in ReadXmlGridView -> { ex.Message}");
                throw new Exception("ERROR in GetSqlScema.", ex);
                MessageBox.Show("Error in GetSqlScema -> " + ex.Message);
            }
            return dx;
        }

        public string GetCurrentBaleTableName()
        {
            List<string> tablelist = new List<string>();
            string CurrentBaleTable = string.Empty;
            string strquery = "USE ForteData SELECT [Name],create_date FROM sys.Tables with(NOLOCK) " +
                "WHERE [name] LIKE'%BaleArchive%'ORDER BY create_date DESC";
            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuRtCon))
                {
                    sqlConnection?.Open();
                    using (var command = new SqlCommand(strquery, sqlConnection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    if (reader.GetString(0) != null)
                                        tablelist.Add(reader.GetString(0));
                                }
                        }
                    }
                    sqlConnection?.Close();
                }
                if (tablelist.Count > 0)
                    CurrentBaleTable = tablelist[0].ToString();
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetCurrentBaleTableName -> { ex.Message}");
                throw new Exception("ERROR in ClsSQLhandler GetCurrentBaleTableName.", ex);
                //MessageBox.Show($"ERROR in ClsSQLhandler GetCurrentBaleTableName {ex.Message}");
            }
            return CurrentBaleTable;
        }

        public async Task<string> GetCurrentBaleTableNameAsync()
        {
            List<string> tablelist = new List<string>();
            string CurrentBaleTable = string.Empty;
            string strquery = "USE ForteData SELECT [Name],create_date FROM sys.Tables with(NOLOCK) " +
                "WHERE [name] LIKE'%BaleArchive%'ORDER BY create_date DESC";
            try
            {
                await Task.Run(() =>
                {
                    using (var sqlConnection = new SqlConnection(SqlAuRtCon))
                    {
                        sqlConnection?.Open();
                        using (var command = new SqlCommand(strquery, sqlConnection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                    while (reader.Read())
                                    {
                                        if (reader.GetString(0) != null)
                                            tablelist.Add(reader.GetString(0));
                                    }
                            }
                        }
                        sqlConnection?.Close();
                    }
                    if (tablelist.Count > 0)
                        CurrentBaleTable = tablelist[0].ToString();
                });
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetCurrentBaleTableNameAsync -> {ex.Message}");
                throw new Exception("ERROR in ClsSQLhandler GetCurrentBaleTableNameAsync.", ex);
                //MessageBox.Show($"ERROR in ClsSQLhandler GetCurrentBaleTableName {ex.Message}");
            }
            return CurrentBaleTable;
        }




        public string GetCurrentLotTable()
        {
            List<string> tablelist = new List<string>();
            string CurrentLotTable = string.Empty;
            string strquery = "USE ForteData SELECT [Name],create_date FROM sys.Tables with(NOLOCK) " +
                "WHERE [name] LIKE'%LotArchive%'ORDER BY create_date DESC";

            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuRtCon))
                {
                    sqlConnection?.Open();
                    using (var command = new SqlCommand(strquery, sqlConnection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    if (reader.GetString(0) != null)
                                        tablelist.Add(reader.GetString(0));
                                }
                        }
                    }
                    sqlConnection?.Close();
                }
                if (tablelist.Count > 0)
                    CurrentLotTable = tablelist[0].ToString();
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetCurrentLotTable -> { ex.Message}");
                throw new Exception("ERROR in GetCurrentLotTable.", ex);
                //MessageBox.Show($"ERROR in ClsSQLhandler GetCurrentLotTable {ex.Message}");
            }
            return CurrentLotTable;
        }


        public void InitialSetupSqlDataBase()
        {
            ClsSerilog.LogMessage(ClsSerilog.Info, $"InitialSetup SqlDataBase");

            SetUpSqlParams();
            SetConnectionString();

            //Bale Archives
            // createTableQuery = ArchiveTablescript(m_CurrentAchivesTable);
            //  CheckCreateSqlTable(m_CurrentAchivesTable, m_CreateTableQuery);
            //  m_ArchiveFieldsList = SetArchiveDataStructure(m_CreateTableQuery, m_CurrentAchivesTable);

        }

        private void SetConnectionString()
        {
            MasterConStr = SetWinAuConnString(MASTER_DB);
            WinAuProdConStr = SetWinAuConnString(PULPGRADE_DB);
            WinAuRealtimeConStr = SetWinAuConnString(REALTIME_DB);
            //WinAuTransConStr = SetWinAuConnString(PROCESSTRAN_DB);

            SqlAuRealtimeConStr = SetSqlAuConnString(REALTIME_DB);

            SqlAuRtCon = SetSqlAuConnString(REALTIME_DB);
            SqlAuProdCon = SetSqlAuConnString(PULPGRADE_DB);
            SqlAuMMSCon = SetSqlAuConnString(FORTEMMS_DB);

            SqlAuForteSysCon = SetSqlAuConnString(FORTESYSTEM_DB);
            //ClsSerilog.LogMessage(ClsSerilog.Info, $"SqlAuForteSysCon = {SqlAuForteSysCon}");


        }

        private string SetWinAuConnString(string SqlDatabase)
        {
            string strDataSource = WorkStationID + @"\" + StrInstance;
            return "workstation id=" + WorkStationID +
                    ";packet size=4096;integrated security=SSPI;data source='" + strDataSource +
                    "';persist security info=False;initial catalog= " + SqlDatabase;
        }
        private string SetSqlAuConnString(string SqlDatabase)
        {
            string strDataSource = WorkStationID + @"\" + StrInstance;
            return "Data Source ='" + strDataSource + "'; Database = "
                + SqlDatabase + "; User id= '" + StrUserName + "'; Password = '"
                + StrPassWrd + "'; connection timeout=30;Persist Security Info=True;";

        }


        private void SetUpSqlParams()
        {
            if(ClassCommon.SavedItemList[0].Item2 != null)
            {
                ClassCommon.SQLHost = ClassCommon.SavedItemList[0].Item2;
            }
            else
                ClassCommon.SQLHost = Environment.MachineName;

            if (ClassCommon.SavedItemList[1].Item2 != null)
            {
                ClassCommon.SQLInstant = ClassCommon.SavedItemList[1].Item2;
            }
            else
                ClassCommon.SQLInstant = "SQLEXPRESS";


            WorkStationID = ClassCommon.SQLHost;
            StrInstance = ClassCommon.SQLInstant;
            StrUserName = "forte";
            StrPassWrd = "etrof";

            //ClsSerilog.LogMessage(ClsSerilog.Info, $"WorkStationID = {WorkStationID}");
            //ClsSerilog.LogMessage(ClsSerilog.Info, $"StrInstance = {StrInstance}");
            //ClsSerilog.LogMessage(ClsSerilog.Info, $"StrUserName = {StrUserName}");
            //ClsSerilog.LogMessage(ClsSerilog.Info, $"StrPassWrd = {StrPassWrd}");
        }

   

        public long GetNewUID(string curDatatable)
        {
            long idx = 0;


            string strIdxQry = $"SELECT TOP 5 UID FROM dbo.[{curDatatable}] ORDER BY [TimeComplete]  DESC";
            List<int> itemList = new List<int>();

            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuRtCon))
                {
                   sqlConnection?.Open();
                    using (var command = new SqlCommand(strIdxQry, sqlConnection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    if (reader != null)
                                        itemList.Add(Convert.ToInt32(reader[0].ToString()));
                                }
                            }
                        }
                    }
                    sqlConnection?.Close();
                }
                if (itemList.Count > 0) idx = itemList[0]; //Newest index in the table.
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetNewUID -> { ex.Message}");
                MessageBox.Show("ERROR in GetNewUID " + ex.Message);

            }
            return idx;
        }



        public bool CheckSqlTable(string StrTablename, string constr)
        {
            bool bfound = false;
            string xstrQuery = "SELECT [name],create_date FROM sys.tables WHERE [name] LIKE '%" + StrTablename + "%' ORDER BY create_date DESC";

            try
            {
                using (var sqlConnection = new SqlConnection(constr))
                {
                    sqlConnection?.Open();
                    using (var command = new SqlCommand(xstrQuery, sqlConnection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                bfound = true;
                        }
                    }
                    sqlConnection?.Close();
                }

                //  if (!bfound)
                //      bfound = ExecuteCommand(GetCreateScript(StrTablename), constr);

            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in CheckSqlTable -> { ex.Message}");
                MessageBox.Show("ERROR in CheckSqlTable" + ex);
            }
            return bfound;
        }

        public DateTime GetProductDayEnd()
        {
            var ProdEnd = DateTime.Now;
            DataTable BaleTable = new DataTable();

            string strQuery = $"SELECT Top 1 ProdDayEnd from  {FS_BALETABLE}";

            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    using (var adapter = new SqlDataAdapter(strQuery, sqlConnection))
                    {
                        adapter.Fill(BaleTable);
                    }
                    if (BaleTable.Rows.Count > 0)
                        ProdEnd = Convert.ToDateTime(BaleTable.Rows[0]["ProdDayEnd"]);
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetProductDayEnd -> { ex.Message}");
                MessageBox.Show($"ERROR in GetProductDayEnd {ex.Message}");
            }
            return ProdEnd;
        }

        public DataTable GetRtLotProc()
        {
            DataTable RtLotProc = new DataTable();
            string strQuery = $"SELECT Top 2 * FROM {RTLOTPROCESSTABLE}";

            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuMMSCon))
                {
                    using (var adapter = new SqlDataAdapter(strQuery, sqlConnection))
                    {
                        adapter.Fill(RtLotProc);
                    }
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetRtLotProc -> { ex.Message}");
                MessageBox.Show($"ERROR in GetRtLotProc {ex.Message}");

            }
            return RtLotProc;
        }

        //---------------------------------------------------------------------------------------------------------------

        public DataTable GetConveyorTable()
        {
            DataTable ConvoyerTable = new DataTable();
            string strQuery = $"SELECT * FROM {FS_CONVEYORTABLE} WHERE ProcessingLine > 0";

            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    using (var adapter = new SqlDataAdapter(strQuery, sqlConnection))
                    {
                        adapter.Fill(ConvoyerTable);
                    }
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetConveyorTable -> { ex.Message}");
                MessageBox.Show($"ERROR in GetConveyorTable {ex.Message}");

            }
            return ConvoyerTable;
        }

        //---------------------------------------------------------------------------------------------------------------
        public DataTable GetSystemTable(string tableName)
        {
            DataTable myTable = new DataTable();
            string strQuery = $"SELECT * FROM {tableName} ";

            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    using (var adapter = new SqlDataAdapter(strQuery, sqlConnection))
                    {
                        adapter.Fill(myTable);
                    }
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetSystemTable -> { ex.Message}");
                //throw new Exception("ERROR in GetSystemTable", ex);
                MessageBox.Show($"ERROR in GetSystemTable {ex.Message}");
            }
            return myTable;
        }

        public int GetDropSize()
        {
            int dSize = 0;

            string strQuery = $"SELECT DropSize FROM [Sources] WHERE ID = '1'";

            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (var command = new SqlCommand(strQuery, sqlConnection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    if (reader != null)
                                        dSize = Convert.ToInt32(reader[0].ToString());
                                }
                        }
                    }
                    sqlConnection?.Close();
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetDropSize -> { ex.Message}");
                MessageBox.Show($"ERROR in GetDropSize {ex.Message}");
            }
            return dSize;
        }

        public int GetNumDropsMax()
        {
            int dSize = 0;
            string strQuery = $"SELECT NumDropsMax FROM [DropParams] WHERE SourceID = '1'";

            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    sqlConnection?.Open();
                    using (var command = new SqlCommand(strQuery, sqlConnection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    if (reader != null)
                                        dSize = Convert.ToInt32(reader[0].ToString());
                                }
                        }
                    }
                    sqlConnection?.Close();
                }

            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetDropSize -> { ex.Message}");
                MessageBox.Show($"ERROR in GetDropSize {ex.Message}");
            }
            return dSize;
        }

        public DataTable GetOpenLotTable(string strquery)
        {
            DataTable mytable = new DataTable();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(strquery, sqlConnection))
                    {
                        adapter.Fill(mytable);
                    }
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetOpenLotTable -> { ex.Message}");
                MessageBox.Show("Error in GetOpenLotTable " + ex.Message);
            }

            return mytable;
        }

        public DataTable GetLotClosSize(string strQuery)
        {
            DataTable mytable = new DataTable();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(strQuery, sqlConnection))
                    {
                        adapter.Fill(mytable);
                    }
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetLotClosSize -> { ex.Message}");
                MessageBox.Show("Error in GetLotClosSize " + ex.Message);
            }
            return mytable;
        }


        //---------------------------------------------------------------------------------------------------------------

        public DataTable GetCurrBaleTable(int intLine)
        {
            DataTable myTable = new DataTable();
            string strQuery = $"SELECT TOP 20 * FROM {FS_BALETABLE} WHERE LineId= {intLine} ";

            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuForteSysCon))
                {
                    using (var adapter = new SqlDataAdapter(strQuery, sqlConnection))
                    {
                        adapter.Fill(myTable);
                    }
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetCurrBaleTable -> { ex.Message}");
                MessageBox.Show($"ERROR in GetCurrBaleTable {ex.Message}");
            }
            return myTable;
        }


        public string GetBaleInStack(int linenum)
        {
            int Station1ID = (linenum == 1) ? 4 : 9;
            string baleinstack = string.Empty;
            string strQuery = $"SELECT Counter FROM {FS_CONVEYORTABLE} WHERE Station1ID= {Station1ID} AND Type = 'Internal'";

            try
            {
                using (DataTable myTable = new DataTable())
                {
                    using (var sqlConnection = new SqlConnection(SqlAuForteSysCon))
                    {
                        using (var adapter = new SqlDataAdapter(strQuery, sqlConnection))
                        {
                            adapter.Fill(myTable);
                        }
                    }
                    if (myTable.Rows.Count > 0)
                        baleinstack = myTable.Rows[0]["Counter"].ToString();
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetBalesInStack -> { ex.Message}");
                MessageBox.Show($"ERROR in GetBalesInStack {ex.Message}");
            }
            return baleinstack;
        }


        //---------------------------------------------------------------------------------------------------------------


        private bool ExecuteCommand(string strquery, string m_conn)
        {
            bool bDone = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(m_conn))
                {
                    if (sqlConnection != null) sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(strquery, sqlConnection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                bDone = true;
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in executeCommand -> { ex.Message}");
                //throw new Exception("ERROR in executeCommand", ex);
                MessageBox.Show($"SQL ERROR in executeCommand {ex.Message}");
            }
            return bDone;
        }

        public bool CheckSqlDatabase(string Strdb)
        {
            bool bFoundTable = false;
            string strQuery = "SELECT * FROM sys.databases d WHERE d.database_id>4";
            string strCreateDb = "CREATE DATABASE " + Strdb;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(WinAuRealtimeConStr))
                {
                    sqlConnection?.Open();
                    using (SqlCommand command = new SqlCommand(strQuery, sqlConnection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    if (reader != null)
                                    {
                                        // Console.WriteLine("reader = " + reader[0].ToString());
                                        if (reader[0].ToString() == Strdb)
                                            bFoundTable = true;
                                    }
                                }
                            }
                        }
                    }
                    if (!bFoundTable)
                        bFoundTable = ExecuteCommand(strCreateDb, WinAuRealtimeConStr);
                    sqlConnection?.Close();
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in CheckSqlDatabase -> { ex.Message}");
                MessageBox.Show("ERROR in SQL CheckSqlDatabase " + ex.Message);

            }
            return bFoundTable;
        }

        public DataTable GetSqlDatatable(string strTable)
        {
            DataTable mytable = new DataTable();
            string strquery = "SELECT * FROM " + strTable + " ORDER BY ID ASC";

            try
            {
                using (var sqlConnection = new SqlConnection(WinAuProdConStr))
                {
                    using (var adapter = new SqlDataAdapter(strquery, sqlConnection))
                    {
                        adapter.Fill(mytable);
                    }
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetSqlDatatable -> { ex.Message}");
                MessageBox.Show("ERROR in GetSqlDatatable " + ex.Message);
            }
            return mytable;
        }

        private void GetTop2SqlArchivetable()
        {
            List<string> tablelist = new List<string>();
            string strquery = "SELECT top 2 [name],create_date FROM sys.tables WHERE [name] LIKE '%BaleArchive%' ORDER BY create_date DESC";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuRealtimeConStr))
                {
                    sqlConnection?.Open();
                    using (var command = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    if (reader.GetString(0) != null)
                                        tablelist.Add(reader.GetString(0));
                                }
                        }
                    }
                    sqlConnection?.Close();
                }
                currentAchivesTable = tablelist[0].ToString();
                // previousArchivesTable = tablelist.Count > 1 ? tablelist[1].ToString() : tablelist[0].ToString();
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetTop2SqlArchivetable -> { ex.Message}");
                throw new Exception("ERROR in GetTop2SqlArchivetable", ex);
                //MessageBox.Show("ERROR in GetTop2SqlArchivetable " + ex.Message);
            }
        }


        public DataTable GetSqlArchivetable(string strClause)
        {
            DataTable mytable = new DataTable();

            try
            {
                using (var sqlConnection = new SqlConnection(SqlAuRealtimeConStr))
                {
                    using (var adapter = new SqlDataAdapter(strClause, sqlConnection))
                    {
                        adapter.Fill(mytable);
                    }
                }

                DataColumnCollection columns = mytable.Columns;
                if (columns.Contains("Moisture"))
                {
                    DataRow[] rows = mytable.Select();
                    for (int i = 0; i < rows.Length; i++)
                    {
                        switch (Settings.Default.MoistureType)
                        {
                            case 0: // %MC == moisture from Sql database
                                if ((columns.Contains("Moisture")) & (rows[i]["Moisture"] != null))
                                    rows[i]["Moisture"] = rows[i]["Moisture"];
                                break;

                            case 1: // %MR  = Moisture / ( 1- Moisture / 100)
                                if ((columns.Contains("Moisture")) & (rows[i]["Moisture"] != null))
                                    rows[i]["Moisture"] = rows[i].Field<Single>("Moisture") / (1 - rows[i].Field<Single>("Moisture") / 100);
                                break;

                            case 2: // %AD = (100 - moisture) / 0.9
                                if ((columns.Contains("Moisture")) & (rows[i]["Moisture"] != null))
                                    rows[i]["Moisture"] = (100 - rows[i].Field<Single>("Moisture")) / 0.9;
                                break;

                            case 3: // %BD  = 100 - moisture
                                if ((columns.Contains("Moisture")) & (rows[i]["Moisture"] != null))
                                    rows[i]["Moisture"] = 100 - rows[i].Field<Single>("Moisture");
                                break;
                        }

                        if (Settings.Default.WeightUnit == 1) //English Unit lb
                        {
                            if ((columns.Contains("Weight")) & (rows[i]["Weight"] != null))
                                rows[i]["Weight"] = (rows[i].Field<Single>("Weight") * 2.20462); //Lb.
                            if ((columns.Contains("BDWeight")) & (rows[i]["BDWeight"] != null))
                                rows[i]["BDWeight"] = (rows[i].Field<Single>("BDWeight") * 2.20462); //Lb.
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetSqlArchivetable -> { ex.Message}");
                MessageBox.Show("ERROR in GetSqlArchivetable " + ex.Message);
            }
            return mytable;
        }

        public DataTable GetSqlTableHdr()
        {
            DataTable dx = new DataTable();
            string strQuery = "SELECT ORDINAL_POSITION,COLUMN_NAME,DATA_TYPE FROM ForteData.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" + GetCurrentArchivesTable() + "'";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuRealtimeConStr))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(strQuery, sqlConnection))
                    {
                        adapter.Fill(dx);
                    }
                }
                foreach (var item in this.RemoveFieldsList)
                {
                    RemoveHrdItem(dx, item);
                }
            }

            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetSqlTableHdr -> { ex.Message}");
                MessageBox.Show("Error in GetSqlTableHdr -> " + ex.Message);

            }
            return dx;
        }

        public string GetCurrentArchivesTable()
        {
            GetTop2SqlArchivetable();
            return currentAchivesTable;
        }

        public List<string> GetTableItemLists(string strTableName)
        {
            List<string> itemlist = new List<string>();
            string strquery = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + strTableName + "';";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuRealtimeConStr))
                {
                    sqlConnection?.Open();
                    using (var command = new SqlCommand(strquery, sqlConnection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    if (reader.GetString(3) != null)
                                        itemlist.Add(reader.GetString(3));
                                }
                        }
                    }
                    sqlConnection?.Close();
                }
            }
            catch (SqlException ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetTableItemLists -> { ex.Message}");
                MessageBox.Show("ERROR in GetTableItemLists" + ex.Message);
            }
            return itemlist;
        }

        /// <summary>
        /// Need tyo find out where the Next Serial come from!!!
        /// </summary>
        /// <param name="iD"></param>
        /// <returns></returns>
        public string GetNextSerialNumber(string iD)
        {
            string serialNum = string.Empty;
            string strQuery = $"SELECT Top 1 NextSerNum FROM [RealTimeItemSerial] with(NOLOCK)";
           
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlAuMMSCon))
                {
                    sqlConnection?.Open();
                    using (var command = new SqlCommand(strQuery, sqlConnection))
                    {
                        using (SqlDataReader  reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    if (reader != null)
                                        serialNum = Convert.ToInt32(reader[0]).ToString();
                                }
                        }
                    }
                    sqlConnection?.Close();
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Access Error in GetNextSerialNumber -> { ex.Message}");
                MessageBox.Show($"Access Error in GetNextSerialNumber -> {ex.Message}");
            }
            return serialNum;
        }

        public List<string> GetBigBoxHdrList()
        {
            List<string> hdrList = new List<string>();

            try
            {
                DataTable tempTable = GetSqlScema2(currentAchivesTable);

                foreach (DataRow item in tempTable.Rows)
                {
                    hdrList.Add(item[1].ToString());
                }

                if (hdrList.Contains("SpareSngFld1"))
                    hdrList.Remove("SpareSngFld1");

                if (hdrList.Contains("SpareSngFld2"))
                    hdrList.Remove("SpareSngFld2");


                if (hdrList.Contains("AsciiFld1"))
                    hdrList.Remove("AsciiFld1");

                if (hdrList.Contains("AsciiFld2"))
                    hdrList.Remove("AsciiFld2");


                if (hdrList.Contains("Forte1"))
                    hdrList.Remove("Forte1");

                if (hdrList.Contains("Forte2"))
                    hdrList.Remove("Forte2");

                if (hdrList.Contains("WtMes"))
                    hdrList.Remove("WtMes");

                if (hdrList.Contains("MoistMes"))
                    hdrList.Remove("MoistMes");

                if (hdrList.Contains("LotNumberStatus"))
                    hdrList.Remove("LotNumberStatus");

                if (hdrList.Contains("Moisture"))
                    hdrList.Remove("Moisture");

                if (hdrList.Contains("Weight"))
                    hdrList.Remove("Weight");

                if (hdrList.Contains("JobNum"))
                    hdrList.Remove("JobNum");

                for (int i = 0; i < hdrList.Count; i++)
                {
                    if (hdrList[i].Contains("Finish"))
                        hdrList[i] = "Viscosity";

                    if (hdrList[i].Contains("FC_LotIdentString"))
                        hdrList[i] = "CusLotNumber";

                    if (hdrList[i].Contains("SpareSngFld3"))
                        hdrList[i] = "%CV";
                }               
               
                hdrList.Add("UnitNumber");
                hdrList.Add("UnitBaleNumber");

                // add from another table that should be editable
                hdrList.Add("LotSize");
                hdrList.Add("Next LotNumber");
                hdrList.Add("Next SerialNumber");


            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Access Error in GetBigBoxHdrList -> {ex.Message}");
                MessageBox.Show($"Access Error in GetBigBoxHdrList -> {ex.Message}");
            }
            return hdrList;
        }



        public DataTable GetSqlScema2(string curtable)
        {
            DataTable dx = new DataTable();

            string strQuery = $"SELECT ORDINAL_POSITION,COLUMN_NAME,DATA_TYPE FROM ForteData.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'{curtable}'";

            string ConString = "Data Source ='" + WorkStationID + @"\" + StrInstance + "'; Database = fortedata; User id= 'Forte'; Password = 'etrof'; connection timeout=30;Persist Security Info=True;";

            try
            {
                using (var sqlConnection = new SqlConnection(ConString))
                {
                    sqlConnection.Open();
                    using (SqlCommand comm = new SqlCommand(strQuery, sqlConnection))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dx.Load(reader);
                        }
                    }
                    sqlConnection?.Close();
                }
             //   SetRemoveFields();

                foreach (var item in RemoveFieldsList)
                {
                    RemoveHrdItem(dx, item);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR in GetSqlScema " + ex.Message);
                ClsSerilog.LogMessage(ClsSerilog.Error, $"ERROR in GetSqlScema < {ex.Message}");
            }
            return dx;
        }





        private void RemoveHrdItem(DataTable Ttable, string strItem)
        {
            foreach (DataRow item in Ttable.Rows)
            {
                if (item[1].ToString() == strItem)
                {
                    item.Delete();
                }
            }
            Ttable.AcceptChanges();
        }

        private void SetRemoveFields()
        {
            if (RemoveFieldsList == null)
            {
                RemoveFieldsList = new List<string>
                {
                    "Index",
                    "Empty",
                    "QualityUID",
                    "AsciiFld1",
                    "AsciiFld2",
                    "OrderStr",
                    "QualityName",
                    "GradeLabel1",
                    "StockLabel1",
                    "StockLabel2",
                    "StockLabel3",
                    "StockLabel4",
                    //"JobNum",
                    //RemoveFieldsList.Add("Forte1");
                    //RemoveFieldsList.Add("Forte2");
                    "ForteAveraging",
                    //ItemRemoveLst.Add("UpCount")
                    //ItemRemoveLst.Add("DownCount")
                    "DownCount2",
                    "Brightness",
                    "BaleHeight",
                    "SourceID",
                    "Finish",
                    "SheetArea",
                    "SheetCount",
                    "CalibrationID",
                    "PkgMoistMethod",
                    //RemoveFieldsList.Add("SpareSngFld1");
                    //RemoveFieldsList.Add("SpareSngFld2");
                    "SpareSngFld3",
                    "LastInGroup",
                    //RemoveFieldsList.Add("MoistMes");
                    "ProdDayStart",
                    "ProdDayEnd",
                    "LineID",
                    "StockID",
                    "GradeID",
                    //RemoveFieldsList.Add("WtMes");
                    "AsciiFld3",
                    "AsciiFld4",
                    "SR",
                    "UID",
                    "Package",
                    "ResultDesc",
                    "GradeLabel2",
                    "WLAlarm",
                    "WLAStatus",
                    //
                    "Status",
                    "WeightStatus",
                    "TemperatureStatus",
                    "OrigWeightStatus",
                    "ForteStatus",
                    "Forte1Status",
                    "Forte2Status",
                    "UpCountStatus",
                    "DownCountStatus",
                    "DownCount2Status",
                    "BrightnessStatus",
                    "TimeStartStatus",
                    "BaleHeightStatus",
                    "TimeCompleteStatus",
                    "SourceIDStatus",
                    "StockIDStatus",
                    "GradeIDStatus",
                    "TareWeightStatus",
                    "AllowanceStatus",
                    "SheetCountStatus",
                    "MoistureStatus",
                    "NetWeightStatus",
                    "CalibrationIDStatus",
                    "SeriAlNumberStatus",
                    //RemoveFieldsList.Add("LotNumberStatus");
                    "TemperatureStatus",
                    "UnitNumberStatus",
                    "UnitIdent",
                    "UnitBaleNumber",
                    "UnitNumber",
                    "Temperature",
                    "FC_IdentString",
                    "FC_LotIdentString",
                    "Dirt",
                    "DropTime",
                    "Position",
                    "DropNumber",
                    "GradeName",
                    //Last removed
                    "SourceName",
                    "LineName",
                    "OrigWeight",
                    // RemoveFieldsList.Add("LotNumber");
                    // RemoveFieldsList.Add("LotBaleNumber");
                    "LotIdent",
                    "LotIdent",
                    "BasisWeight",
                    "BDWeight",
                    "MonthCode"
                };
            }
        }

        private string ArchiveTablescript(string strTableName)
        {
            return (@"CREATE TABLE[dbo].[" + strTableName + "]" +
              "([Index][int] NULL," +
              "[Empty][bit] NULL," +
              "[SourceName][nvarchar](255) NULL," +
              "[LineName] [nvarchar] (255) NULL," +
              "[StockName] [nvarchar] (255) NULL," +
              "[CalibrationName] [nvarchar] (255) NULL," +
              "[GradeName] [nvarchar] (255) NULL," +
              "[Status] [smallint] NULL," +
              "[Weight] [real] NULL," +
              "[WeightStatus] [smallint] NULL," +
              "[OrigWeight] [real] NULL," +
              "[OrigWeightStatus] [smallint] NULL," +
              "[Forte] [int] NULL," +
              "[ForteStatus] [smallint] NULL," +
              "[ForteAveraging] [bit] NULL," +
              "[Forte1] [int] NULL," +
              "[Forte1Status] [smallint] NULL," +
              "[Forte2] [int] NULL," +
              "[Forte2Status] [smallint] NULL," +
              "[UpCount] [int] NULL," +
              "[UpCountStatus] [smallint] NULL," +
              "[DownCount] [int] NULL," +
              "[DownCountStatus] [smallint] NULL," +
              "[DownCount2] [int] NULL," +
              "[DownCount2Status] [smallint] NULL," +
              "[Brightness] [real] NULL," +
              "[BrightnessStatus] [smallint] NULL," +
              "[BaleHeight] [real] NULL," +
              "[BaleHeightStatus] [smallint] NULL," +
              "[TimeStart] [datetime] NULL," +
              "[TimeStartStatus] [smallint] NULL," +
              "[TimeComplete] [datetime] NULL," +
              "[TimeCompleteStatus] [smallint] NULL," +
              "[SourceID] [int] NULL," +
              "[SourceIDStatus] [smallint] NULL," +
              "[LineID] [int] NULL," +
              "[StockID] [int] NULL," +
              "[StockIDStatus] [smallint] NULL," +
              "[GradeID] [int] NULL," +
              "[GradeIDStatus] [smallint] NULL," +
              "[TareWeight] [real] NULL," +
              "[TareWeightStatus] [smallint] NULL," +
              "[Finish] [real] NULL," +
              "[AllowanceStatus] [smallint] NULL," +
              "[DropNumber] [int] NULL," +
              "[DropTime] [datetime] NULL," +
              "[Position] [tinyint] NULL," +
              "[SheetCount] [smallint] NULL," +
              "[SheetCountStatus] [smallint] NULL," +
              "[SheetArea] [real] NULL," +
              "[Moisture] [real] NULL," +
              "[MoistureStatus] [smallint] NULL," +
              "[NetWeight] [real] NULL," +
              "[NetWeightStatus] [smallint] NULL," +
              "[BDWeight] [real] NULL," +
              "[CalibrationID] [int] NULL," +
              "[CalibrationIDStatus] [smallint] NULL," +
              "[SerialNumber] [int] NULL," +
              "[SeriAlNumberStatus] [smallint] NULL," +
              "[LotNumber] [int] NULL," +
              "[LotNumberStatus] [smallint] NULL," +
              "[LotBaleNumber] [int] NULL," +
              "[PkgMoistMethod] [int] NULL," +
              "[SpareSngFld1] [real] NULL," +
              "[SpareSngFld2] [real] NULL," +
              "[QualityUID] [int] NULL," +
              "[LastInGroup] [bit] NULL," +
              "[MoistMes] [nvarchar] (10) NULL," +
              "[WtMes] [nvarchar] (10) NULL," +
              "[ProdDayStart] [datetime] NULL," +
              "[ProdDayEnd] [datetime] NULL," +
              "[ShiftName] [nvarchar] (10) NULL," +
              "[MonthCode] [nvarchar] (1) NULL," +
              "[AsciiFld1] [nvarchar] (25) NULL," +
              "[AsciiFld2] [nvarchar] (25) NULL," +
              "[AsciiFld3] [nvarchar] (25) NULL," +
              "[AsciiFld4] [nvarchar] (25) NULL," +
              "[SR] [real] NULL," +
              "[UID] [int] NULL," +
              "[OrderStr] [nvarchar] (25) NULL," +
              "[Package] [nvarchar] (12) NULL," +
              "[ResultDesc] [nvarchar] (4) NULL," +
              "[Temperature] [real] NULL," +
              "[TemperatureStatus] [smallint] NULL," +
              "[LotIdent] [nvarchar] (16) NULL," +
              "[Dirt] [real] NULL," +
              "[BasisWeight] [real] NULL," +
              "[QualityName] [nvarchar] (50) NULL," +
              "[FC_IdentString] [nvarchar] (50) NULL," +
              "[StockLabel1] [nvarchar] (50) NULL," +
              "[GradeLabel1] [nvarchar] (50) NULL," +
              "[StockLabel2] [nvarchar] (50) NULL," +
              "[GradeLabel2] [nvarchar] (50) NULL," +
              "[FC_LotIdentString] [nvarchar] (25) NULL," +
              "[UnitBaleNumber] [smallint] NULL," +
              "[UnitNumber] [int] NULL," +
              "[UnitNumberStatus] [smallint] NULL," +
              "[UnitIdent] [nvarchar] (20) NULL," +
              "[WLAlarm] [smallint] NULL," +
              "[WLAStatus] [smallint] NULL," +
              //   "[JobNum] [int] NULL," +
              "[SpareSngFld3] [real] NULL," +
              "[StockLabel3] [nvarchar] (50) NULL," +
              "[StockLabel4] [nvarchar] (50) NULL)" +
              " ON[PRIMARY];");
        }


        private string LotTableScript(string strTableName)
        {
            return (@"CREATE TABLE[dbo].[" + strTableName + "]" +
            "([Index] [int] NULL," +
            "[LotNum] [int] NULL," +
            "[LotStatus] [nvarchar](255) NULL," +
            "[Empty][bit] NULL," +
            "[PriGrp] [smallint] NULL," +
            "[SecGrp] [smallint] NULL," +
            "[GroupingID] [smallint] NULL, " +
            "[GroupText] [nvarchar](255) NULL, " +
            "[OnHold] [bit] NULL," +
            "[JobNum] [bit] NULL," +
            "[OpenTD] [datetime] NULL," +
            "[CloseTD] [datetime] NULL," +
            "[CloseBySize] [bit] NULL," +
            "[CloseByTime] [bit] NULL," +
            "[BaleCount] [smallint] NULL," +
            "[TotNW] [float] NULL," +
            "[MinNW] [real] NULL," +
            "[MinNWBale] [int] NULL," +
            "[MaxNW] [real] NULL," +
            "[MaxNWBale] [int] NULL," +
            "[NW2N] [real] NULL," +
            "[MeanNW] [real] NULL," +
            "[RangeNW] [real] NULL," +
            "[StdDevNW] [real] NULL," +
            "[TotTW] [real] NULL," +
            "[TotBW] [real] NULL," +
            "[TotMC] [real] NULL," +
            "[MinMC] [real] NULL," +
            "[MinMCBale] [int] NULL," +
            "[MaxMC] [real] NULL," +
            "[MaxMCBale] [int] NULL," +
            "[MC2M] [real] NULL," +
            "[RangeMC] [real] NULL," +
            "[StdDevMC] [real] NULL," +
            "[NextBaleNumber] [real] NULL," +
            "[BalesAssigned] [real] NULL," +
            "[ClosingSize] [real] NULL," +
            "[Action] [nvarchar](16) NULL," +
            "[SR] [real] NULL," +
            "[Finish] [real] NULL," +
            "[OrderStr] [nvarchar](25) NULL," +
            "[UID] [int] NULL," +
            "[AsciiFld1] [nvarchar](20) NULL," +
            "[AsciiFld2] [nvarchar](20) NULL," +
            "[AsciiFld3] [nvarchar](20) NULL," +
            "[AsciiFld4] [nvarchar](20) NULL," +
            "[SpareSngFld1] [real] NULL," +
            "[SpareSngFld2] [real] NULL," +
            "[LotIdent] [nvarchar](16) NULL," +
            "[QualityUID] [int] NULL," +
            "[StockName] [nvarchar](20) NULL," +
            "[FC_IdentString] [nvarchar](25) NULL," +
            "[UnitCount] [smallint] NULL," +
            "[SpareSngFld3] [real] NULL," +
            "[MonthCode] [nvarchar](20) NULL)" +
            "ON[PRIMARY];");
        }

        private string StockTableScript()
        {
            return (@"CREATE TABLE[dbo].[" + FS_STOCKTABLE + "]" +
                "([ID] [smallint] NULL," +
                "[Name] [nvarchar] (20) NULL," +
                "[CalIDLn1] [smallint] NULL," +
                "[CalIDLn2] [smallint] NULL," +
                "[CalIDLn3] [smallint] NULL," +
                "[TareWeight] [real] NULL," +
                "[WrapperStockID] [smallint] NULL," +
                "[WrapperStyleID] [smallint] NULL," +
                "[DefaultWeight] [real] NULL," +
                "[DefaultNetWeight] [real] NULL," +
                "[DefaultForte] [smallint] NULL," +
                "[DefaultMoisture] [real] NULL," +
                "[MoistConv] [smallint] NULL," +
                "[DefaultGradeID] [smallint] NULL," +
                "[DefaultCriteriaID] [smallint] NULL," +
                "[DefaultBrightness] [real] NULL," +
                "[DefaultTemp] [real] NULL," +
                "[DefaultSheetCount] [smallint] NULL," +
                "[DefaultSheetArea] [real] NULL," +
                "[DefaultBasisWt] [real] NULL," +
                "[NextBaleNumber] [int] NULL," +
                "[NextLotNumber] [int] NULL," +
                "[CurrentBaleNumber] [int] NULL," +
                "[TargetCount] [int] NULL," +
                "[TargetBaleWt] [real] NULL," +
                "[TargetBaleWtTol] [real] NULL," +
                "[TargetBaleWtType] [smallint] NULL," +
                "[SampleCount] [smallint] NULL," +
                "[Finish] [real] NULL," +
                "[MinWeight] [real] NULL," +
                "[MaxWeight] [real] NULL," +
                "[MinMoisture] [real] NULL," +
                "[MaxMoisture] [real] NULL," +
                "[WTUseCaution] [bit] NULL," +
                "[MUseCaution] [bit] NULL," +
                "[TareWeight2] [real] NULL," +
                "[TareWeight3] [real] NULL," +
                "[CautionWTLow] [real] NULL," +
                "[CautionWTHigh] [real] NULL," +
                "[CautionMoistLow] [real] NULL," +
                "[CautionMoistHigh] [real] NULL," +
                "[StdRegain] [real] NULL," +
                "[CalRange1Low] [real] NULL," +
                "[CalRange1High] [real] NULL," +
                "[CalRange2Low] [real] NULL," +
                "[CalRange2High] [real] NULL," +
                "[CalRange3Low] [real] NULL," +
                "[CalRange3High] [real] NULL," +
                "[CrossoverMoist] [real] NULL," +
                "[CrossoverCal] [real] NULL," +
                "[HtConst] [float] NULL," +
                "[NomHt] [float] NULL," +
                "[WetLapHtConst] [float] NULL," +
                "[WetLapNomHt] [float] NULL," +
                "[WetLapWtFctr] [float] NULL," +
                "[SheetArea] [real] NULL," +
                "[SRCRestriction] [tinyint] NULL," +
                "[Label1] [nvarchar] (50) NULL," +
                "[Label2] [nvarchar] (50) NULL," +
                "[CalIDLn4] [smallint] NULL," +
                "[CalIDLn5] [smallint] NULL," +
                "[Label3] [nvarchar] (50) NULL," +
                "[Label4] [nvarchar] (50) NULL," +
                "[IsReject] [bit] NULL," +
                "[RejInc] [bit] NULL)" +
                "ON[PRIMARY];");
        }

        private string GradesTableScript()
        {
            return (@"CREATE TABLE[dbo].[" + FS_GRADETABLE + "]" +
            "([ID] [smallint] NULL," +
            "[Name] [nvarchar] (20) NULL," +
            "[Label1] [nvarchar] (50) NULL," +
            "[Label2][nvarchar](50) NULL," +
            "[LnReset] [nvarchar] (25) NULL," +
            "[Size] [smallint] NULL, " +
            "[RailCarCap] [smallint] NULL," +
            "[NextLotNumber] [smallint] NULL," +
            "[DateCreated] [datetime] NULL," +
            "[DateModified] [datetime] NULL," +
            "[StockID] [smallint] NULL," +
            "[BarcodeStencil] [bit] NULL," +
            "[WrapperStockID] [smallint] NULL," +
            "[WrapperStyleID] [smallint] NULL," +
            "[StackSize] [smallint] NULL," +
            "[UnitSize] [smallint] NULL," +
            "[MinMoisture] [real] NULL," +
            "[MaxMoisture] [real] NULL," +
            "[MinWeight] [real] NULL," +
            "[MaxWeight] [real] NULL," +
            "[CautionWTLow] [real] NULL," +
            "[CautionWTHigh] [real] NULL," +
            "[CautionMoistLow] [real] NULL," +
            "[CautionMoistHigh] [real] NULL," +
            "[WTUseCaution] [bit] NULL," +
            "[MUseCaution] [bit] NULL," +
            "[IsReject] [bit] NULL," +
            "[RejInc] [bit] NULL)" +
            "ON[PRIMARY];");
        }

        private string CalibrationTableScript()
        {
            return (@"CREATE TABLE[dbo].[" + SQLCALTABLE + "]" +
                "([ID] [smallint] NOT NULL DEFAULT 0," +
                "[Name] [nvarchar] (20) NULL," +
                "[A] [float] DEFAULT 0," +
                "[B] [float] DEFAULT 0," +
                "[C] [float] DEFAULT 0," +
                "[D] [float] DEFAULT 0," +
                "[Method] [nvarchar] (17) NULL," +
                "[LabBasis] [smallint] DEFAULT 1," +
                "[DateCreated] [datetime]," +
                "[DateModified] [datetime]," +
                "[WtUnits] [nvarchar] (1) NULL," +
                "[Function] [nvarchar] (16) NULL," +
                "[FunctionID] [smallint] DEFAULT 1," +
                "[Status] [smallint] DEFAULT 1," +
                "[DelDate] [datetime]," +
                "[FC_E] [real] DEFAULT 1," +
                "[FC_M] [real] DEFAULT 1," +
                "[Equation] [nvarchar] (MAX) NULL)" +
                "ON[PRIMARY];");

        }
        private string LotTransactionScript()
        {
            return (@"CREATE TABLE[" + FS_LOTTRANTABLE + "]" +
                "([Index][int]," +
                "[LotNum] [int]," +
                "[LotStatus] [nvarchar](255)," +
                "[Empty] [bit] NOT NULL," +
                "[PriGrp] [smallint]," +
                "[SecGrp] [smallint]," +
                "[GroupingID] [smallint]," +
                "[GroupText] [nvarchar] (255)," +
                "[OnHold] [bit] NOT NULL," +
                "[JobNum] [int]," +
                "[OpenTD] [datetime]," +
                "[CloseTD] [datetime]," +
                "[CloseBySize] [bit] NOT NULL," +
                "[CloseByTime] [bit] NOT NULL," +
                "[BaleCount] [smallint]," +
                "[TotNW] [float]," +
                "[MinNW] [real]," +
                "[MinNWBale] [int]," +
                "[MaxNW] [real]," +
                "[MaxNWBale] [int]," +
                "[NW*NW] [real]," +
                "[MeanNW] [real]," +
                "[RangeNW] [real]," +
                "[StdDevNW] [real]," +
                "[TotTW] [real]," +
                "[TotBW] [real]," +
                "[TotMC] [real]," +
                "[MinMC] [real]," +
                "[MinMCBale] [int]," +
                "[MaxMC] [real]," +
                "[MaxMCBale] [int]," +
                "[MC*MC] [real]," +
                "[RangeMC] [real]," +
                "[StdDevMC] [real]," +
                "[NextBaleNumber] [real]," +
                "[BalesAssigned] [real]," +
                "[ClosingSize] [real]," +
                "[Action] [nvarchar](16)," +
                "[SR] [real] DEFAULT 0," +
                "[Finish] [real] DEFAULT 0," +
                "[OrderStr] [nvarchar](25)," +
                "[UID] [int]," +
                "[SpareSngFld1] [real]," +
                "[SpareSngFld2] [real]," +
                "[AsciiFld1] [nvarchar](20)," +
                "[AsciiFld2] [nvarchar](20)," +
                "[AsciiFld3] [nvarchar](20)," +
                "[AsciiFld4] [nvarchar](20)," +
                "[LotIdent] [nvarchar](16)," +
                "[QualityUID] [int]," +
                "[StockName] [nvarchar](20)," +
                "[FC_IdentString] [nvarchar](25)," +
                "[UnitCount] [smallint]," +
                "[MonthCode] [nvarchar](20)," +
                "[SpareSngFld3] [real])" +
                "ON[PRIMARY];");
        }

        private string OpenLotsScript()
        {
            return (@"CREATE TABLE[" + SQLOPENLOTSTABLE + "]" +
                "([Index] [int] DEFAULT 0," +
                "[LotNum] [int]," +
                "[LotStatus] [nvarchar](15)," +
                "[Empty] [bit] NOT NULL," +
                "[PriGrp] [smallint]," +
                "[SecGrp] [smallint]," +
                "[GroupingID] [smallint]," +
                "[GroupText] [nvarchar](20)," +
                "[OnHold] [bit] NOT NULL," +
                "[JobNum] [int]," +
                "[OpenTD] [datetime]," +
                "[CloseTD] [datetime]," +
                "[CloseBySize] [bit] NOT NULL," +
                "[CloseByTime] [bit] NOT NULL," +
                "[BaleCount] [smallint]," +
                "[TotNW]  [float]," +
                "[MinNW] [real]," +
                "[MinNWBale] [int]," +
                "[MaxNW] [real]," +
                "[MaxNWBale] [int]," +
                "[NW*NW] [real]," +
                "[MeanNW] [real]," +
                "[RangeNW] [real]," +
                "[StdDevNW] [real]," +
                "[TotTW] [real]," +
                "[TotBW] [real]," +
                "[TotMC] [real] DEFAULT 0," +
                "[MinMC] [real]," +
                "[MinMCBale] [int]," +
                "[MaxMC] [real]," +
                "[MaxMCBale] [int]," +
                "[MC*MC] [real]," +
                "[RangeMC] [real]," +
                "[StdDevMC] [real]," +
                "[NextBaleNumber] [real] DEFAULT 0," +
                "[BalesAssigned] [real] DEFAULT 0," +
                "[ClosingSize] [real] DEFAULT 0," +
                "[Action] [nvarchar](16)," +
                "[SR] [real] DEFAULT 0," +
                "[Finish] [real] DEFAULT 0," +
                "[OrderStr] [nvarchar](25)," +
                "[UID] [int]," +
                "[SpareSngFld1] [real]," +
                "[SpareSngFld2] [real]," +
                "[AsciiFld1] [nvarchar](20)," +
                "[AsciiFld2] [nvarchar](20)," +
                "[AsciiFld3] [nvarchar](20)," +
                "[AsciiFld4] [nvarchar](20)," +
                "[LotIdent] [nvarchar](16)," +
                "[QualityUID] [int]," +
                "[StockName] [nvarchar](20)," +
                "[FC_IdentString] [nvarchar](25)," +
                "[UnitCount] [smallint]," +
                "[MonthCode] [nvarchar](20)," +
                "[SpareSngFld3] [real])" +
                "ON[PRIMARY];");
        }

       
    }
}
