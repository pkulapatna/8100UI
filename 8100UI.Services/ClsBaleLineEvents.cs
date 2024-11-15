using Prism.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace _8100UI.Services
{
    public class ClsBaleLineEvents
    {
        public bool debug { get; set; } = false;
        protected readonly IEventAggregator _eventAggregator;
        private ServiceEventsTimer BaleEventsTimer;
        private readonly Sqlhandler _sqlHandler;


        readonly int[] preAId = new int[20];
        readonly int[] curAId = new int[20];
        public int DropSize { get; set; }
        public DataTable NewdataTable { get; set; }
        public int TableIndex { get; set; }

        public DataTable CurDataTable { get; set; }

        private DataTable _newBaleData;
        public DataTable NewBaleData
        {
            get { return _newBaleData; }
            set { _newBaleData = value; }
        }

        public ClsBaleLineEvents(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            _sqlHandler = Sqlhandler.Instance;

            CurDataTable = new DataTable();

            GetBaleStationsfromDB();
          
            _eventAggregator.GetEvent<UpdateMainTimerEvents>().Subscribe(UpdateBaleMoveEventAsync); //Timer

        }

        public void GetBaleStationsfromDB()
        {
            ClassCommon.BaleStationGraphic = GetStationsGraphicTableList();
            ClassCommon.Conveyors = _sqlHandler.GetConveyorsTableList();
            ClassCommon.BaleStation = SetBaleStations();

            ClassCommon.DropSize = _sqlHandler.GetDropSize();
            ClassCommon.NumDropsMax = _sqlHandler.GetNumDropsMax();

          //ClassCommon.BaleStations = GetBaleStationsTable();
        }

        private List<ClsBaleStation> GetBaleStationsTable()
        {
            List<ClsBaleStation> stationsList = new List<ClsBaleStation>();
            string queryOne = $"SELECT * FROM [Stations] with(NOLOCK) WHERE ProcessingLine = 1 ORDER By [ID] ASC";

            try
            {
                using (DataTable Station = _sqlHandler.GetBaleStationsTable(queryOne))
                {
                    if (Station.Rows.Count > 0)
                    {
                        for (int i = 0; i < Station.Rows.Count; i++)
                        {
                            stationsList.Add(new ClsBaleStation(Convert.ToInt32(Station.Rows[i]["ID"].ToString()),
                             Station.Rows[i]["Name"].ToString().Remove(0, 4),
                            Convert.ToInt32(Station.Rows[i]["InternalConveyor"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["ProcessingLine"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["OutputConveyor"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["InputConveyor"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["Device1ID"].ToString())
                            ));
                        }
                        //ClassCommon.LayBoy1ConveyorId = StationsList[StationsList.Count - 1].OutPutCnv;
                        // ClsSerilog.LogMessage(ClsSerilog.Info, $"0. ClassCommon.LayBoy1ConveyorId -> {ClassCommon.LayBoy1ConveyorId}");
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Access Error in GetBaleStationsTable -> {ex.Message}");
                MessageBox.Show($"Access Error in GetBaleStationsTable -> {ex.Message}");
            }
            return stationsList;
        }

        public void StartEventsTimer(string eventName)
        {
            if (BaleEventsTimer == null)
            {
                BaleEventsTimer = new ServiceEventsTimer(_eventAggregator);
                BaleEventsTimer.InitStartEventsTimer(eventName);
            }
        }

        private List<ClassStationGraphics> GetStationsGraphicTableList()
        {
            List<ClassStationGraphics> StationsGraphicTable = new List<ClassStationGraphics>();

            try
            {
                using (DataTable Station = _sqlHandler.GetStationGraphicsTableList())
                {
                    for (int i = 0; i < Station.Rows.Count; i++)
                    {
                        StationsGraphicTable.Add(new ClassStationGraphics(
                            Convert.ToInt32(Station.Rows[i]["UniqueID"]),
                            Station.Rows[i]["Name"].ToString(),
                            Convert.ToBoolean(Station.Rows[i]["Btn1Visible"]),
                            Station.Rows[i]["CapEnglish1"].ToString(),
                            Convert.ToBoolean(Station.Rows[i]["Btn2Visible"]),
                            Station.Rows[i]["CapEnglish2"].ToString(),
                            Convert.ToBoolean(Station.Rows[i]["Btn3Visible"].ToString()),
                            Station.Rows[i]["CapEnglish3"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetStationsGraphicTableList -> { ex.Message}");
                MessageBox.Show($"ERROR in GetStationsGraphicTableList {ex.Message}");
            }
            return StationsGraphicTable;
        }

        private List<ClsStation> SetBaleStations_new()
        {
            ClsSerilog.LogMessage(ClsSerilog.Info, $"Set BaleStations");
            List<ClsStation> StationsList = new List<ClsStation>();
            string queryOne = $"SELECT * FROM [Stations] with(NOLOCK) WHERE ProcessingLine = 1 ORDER By [ID] ASC";
            string queryTwo = $"SELECT * FROM [Stations]  with(NOLOCK) WHERE ProcessingLine = 2 ORDER By [ID] ASC";
            string queryLayBoy = $"SELECT * FROM [Stations]  with(NOLOCK) WHERE ProcessingLine = 0 ORDER By [ID] ASC";

            List<int> station = _sqlHandler.GetStationsInfo();

            for(int i = 0; i < station.Count; i++)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"station {i} -> { station[i]}");
            }

            try
            {
                for(int x = 0; x < station.Count; x++)
                {
                   
                    if (station[x] == 1) ///Line 1 ////////////////////////////////////////////////////////////////////
                    {
                        using (DataTable Station = _sqlHandler.GetBaleStationsTable(queryOne))
                        {
                            if (Station.Rows.Count > 0)
                            {
                                for (int i = 0; i < Station.Rows.Count; i++)
                                {
                                    StationsList.Add(new ClsStation(Convert.ToInt32(Station.Rows[i]["ID"].ToString()),
                                     Station.Rows[i]["Name"].ToString().Remove(0, 4),
                                    Convert.ToInt32(Station.Rows[i]["InternalConveyor"].ToString()),
                                    Convert.ToInt32(Station.Rows[i]["ProcessingLine"].ToString()),
                                    Convert.ToInt32(Station.Rows[i]["OutputConveyor"].ToString()),
                                    Convert.ToInt32(Station.Rows[i]["InputConveyor"].ToString())
                                    ));
                                }
                            }
                        }

                        for (int i = 0; i < StationsList.Count; i++)
                        {
                            if (StationsList[i].StationName == "Weigh")
                            {
                                ClassCommon.WeighConveyorLineOne = StationsList[i].OutPutCnv;
                                if (i - 1 > -1)
                                {
                                    ClassCommon.ApproachConveyorLineOne = StationsList[i - 1].OutPutCnv;
                                }
                            }
                            else if (StationsList[i].StationName == "Press")
                            {
                                ClassCommon.PressConveyorLineOne = StationsList[i].OutPutCnv;
                            }
                            else if (StationsList[i].StationName == "Marker")
                            {
                                ClassCommon.MarkerConveyorLineOne = StationsList[i].OutPutCnv;
                            }
                        }
                    }
                        
                    if (station[x] == 2)///Line 2 ////////////////////////////////////////////////////////////////////
                    {
                        using (DataTable StationTwo = _sqlHandler.GetBaleStationsTable(queryTwo))
                        {
                            List<ClsStation> StationsListTwo = new List<ClsStation>();

                            if (StationTwo.Rows.Count > 0)
                            {
                                //  ClsSerilog.LogMessage(ClsSerilog.Info, $"First ID -> {Convert.ToInt32(StationTwo.Rows[0]["ID"])},  Rows Count => {StationTwo.Rows.Count}");

                                for (int i = 0; i < StationTwo.Rows.Count; i++)
                                {
                                    StationsList.Add(new ClsStation(Convert.ToInt32(StationTwo.Rows[i]["ID"].ToString()),
                                     StationTwo.Rows[i]["Name"].ToString().Remove(0, 4),
                                    Convert.ToInt32(StationTwo.Rows[i]["InternalConveyor"].ToString()),
                                    Convert.ToInt32(StationTwo.Rows[i]["ProcessingLine"].ToString()),
                                    Convert.ToInt32(StationTwo.Rows[i]["OutputConveyor"].ToString()),
                                    Convert.ToInt32(StationTwo.Rows[i]["InputConveyor"].ToString())
                                    ));

                                    StationsListTwo.Add(new ClsStation(Convert.ToInt32(StationTwo.Rows[i]["ID"].ToString()),
                                    StationTwo.Rows[i]["Name"].ToString().Remove(0, 4),
                                   Convert.ToInt32(StationTwo.Rows[i]["InternalConveyor"].ToString()),
                                   Convert.ToInt32(StationTwo.Rows[i]["ProcessingLine"].ToString()),
                                   Convert.ToInt32(StationTwo.Rows[i]["OutputConveyor"].ToString()),
                                   Convert.ToInt32(StationTwo.Rows[i]["InputConveyor"].ToString())
                                   ));
                                }

                                for (int i = 0; i < StationsListTwo.Count; i++)
                                {
                                    if (StationsListTwo[i].StationName == "xfer")
                                    {
                                        ClassCommon.ApproachConveyorLineTwo = StationsListTwo[i].OutPutCnv;
                                    }
                                    else if (StationsListTwo[i].StationName == "Weigh")
                                    {
                                        ClassCommon.WeighConveyorLineTwo = StationsListTwo[i].OutPutCnv;
                                    }
                                    else if (StationsListTwo[i].StationName == "Press")
                                    {
                                        ClassCommon.PressConveyorLineTwo = StationsListTwo[i].OutPutCnv;
                                    }
                                    else if (StationsListTwo[i].StationName == "Marker")
                                    {
                                        ClassCommon.MarkerConveyorLineTwo = StationsListTwo[i].OutPutCnv;
                                    }
                                }
                            }
                        }
                    }

                    if (station[x] == 0) //LayBoy
                    {
                        ClsSerilog.LogMessage(ClsSerilog.Info, $"LayBoy station found = -> {station[x]}");
                        //LayBoy at the End of the list
                        using (DataTable StationAll = _sqlHandler.GetBaleStationsTable(queryLayBoy))
                        {
                            if (StationAll.Rows.Count > 0)
                            {
                                for (int i = 0; i < StationAll.Rows.Count; i++)
                                {
                                    StationsList.Add(new ClsStation(Convert.ToInt32(StationAll.Rows[i]["ID"].ToString()),
                                     StationAll.Rows[i]["Name"].ToString().Remove(0, 4),
                                    Convert.ToInt32(StationAll.Rows[i]["InternalConveyor"].ToString()),
                                    Convert.ToInt32(StationAll.Rows[i]["ProcessingLine"].ToString()),
                                    Convert.ToInt32(StationAll.Rows[i]["OutputConveyor"].ToString()),
                                    Convert.ToInt32(StationAll.Rows[i]["InputConveyor"].ToString())
                                    ));
                                }
                                ClassCommon.LayBoy1ConveyorId = StationsList[StationsList.Count - 1].OutPutCnv;
                            }
                        }
                    }
                }

                //For Debug!
                for (int i = 0; i < StationsList.Count; i++)
                {
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Station Name =  {StationsList[i].StationName}," +
                        $" StationId =  {StationsList[i].StationId}," +
                        $" InputConveyor =  {StationsList[i].InputCnv}," +
                        $" OutputConveyor =  {StationsList[i].OutPutCnv}," +
                        $" ProcessLine =  {StationsList[i].Line}");

                    ClassCommon.StationOneLineOneID = i;
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in SetBaleStations -> { ex.Message}");
                MessageBox.Show($"ERROR in SetBaleStations {ex.Message}");
            }
            return StationsList;
        }

        private List<ClsStation> SetBaleStations_old()
        {
            ClsSerilog.LogMessage(ClsSerilog.Info, $"Set BaleStations");
            List<ClsStation> StationsList = new List<ClsStation>();
            string queryOne = $"SELECT * FROM [Stations] with(NOLOCK) WHERE ProcessingLine = 1 ORDER By [ID] ASC";
            string queryTwo = $"SELECT * FROM [Stations]  with(NOLOCK) WHERE ProcessingLine = 2 ORDER By [ID] ASC";
            string queryAll = $"SELECT * FROM [Stations]  with(NOLOCK) WHERE ProcessingLine = 0 ORDER By [ID] ASC";

            try
            {
                using (DataTable Station = _sqlHandler.GetBaleStationsTable(queryOne))
                {
                    if (Station.Rows.Count > 0)
                    {
                        for (int i = 0; i < Station.Rows.Count; i++)
                        {
                            StationsList.Add(new ClsStation(Convert.ToInt32(Station.Rows[i]["ID"].ToString()),
                             Station.Rows[i]["Name"].ToString().Remove(0, 4),
                            Convert.ToInt32(Station.Rows[i]["InternalConveyor"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["ProcessingLine"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["OutputConveyor"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["InputConveyor"].ToString())
                            ));
                        }
                    }
                }

                for (int i = 0; i < StationsList.Count; i++)
                {
                    if (StationsList[i].StationName == "Weigh")
                    {
                        ClassCommon.WeighConveyorLineOne = StationsList[i].OutPutCnv;
                        if (i - 1 > -1)
                        {
                            ClassCommon.ApproachConveyorLineOne = StationsList[i - 1].OutPutCnv;
                        }
                    }
                    else if (StationsList[i].StationName == "Press")
                    {
                        ClassCommon.PressConveyorLineOne = StationsList[i].OutPutCnv;
                    }
                    else if (StationsList[i].StationName == "Marker")
                    {
                        ClassCommon.MarkerConveyorLineOne = StationsList[i].OutPutCnv;
                    }
                }

                ///Line 2 ///////////////////////////////////////////////////////////////////////////////////////////////
                ///
                using (DataTable StationTwo = _sqlHandler.GetBaleStationsTable(queryTwo))
                {
                    List<ClsStation> StationsListTwo = new List<ClsStation>();

                    if (StationTwo.Rows.Count > 0)
                    {
                        //  ClsSerilog.LogMessage(ClsSerilog.Info, $"First ID -> {Convert.ToInt32(StationTwo.Rows[0]["ID"])},  Rows Count => {StationTwo.Rows.Count}");

                        for (int i = 0; i < StationTwo.Rows.Count; i++)
                        {
                            StationsList.Add(new ClsStation(Convert.ToInt32(StationTwo.Rows[i]["ID"].ToString()),
                             StationTwo.Rows[i]["Name"].ToString().Remove(0, 4),
                            Convert.ToInt32(StationTwo.Rows[i]["InternalConveyor"].ToString()),
                            Convert.ToInt32(StationTwo.Rows[i]["ProcessingLine"].ToString()),
                            Convert.ToInt32(StationTwo.Rows[i]["OutputConveyor"].ToString()),
                            Convert.ToInt32(StationTwo.Rows[i]["InputConveyor"].ToString())
                            ));

                            StationsListTwo.Add(new ClsStation(Convert.ToInt32(StationTwo.Rows[i]["ID"].ToString()),
                            StationTwo.Rows[i]["Name"].ToString().Remove(0, 4),
                           Convert.ToInt32(StationTwo.Rows[i]["InternalConveyor"].ToString()),
                           Convert.ToInt32(StationTwo.Rows[i]["ProcessingLine"].ToString()),
                           Convert.ToInt32(StationTwo.Rows[i]["OutputConveyor"].ToString()),
                           Convert.ToInt32(StationTwo.Rows[i]["InputConveyor"].ToString())
                           ));
                        }

                        for (int i = 0; i < StationsListTwo.Count; i++)
                        {
                            if (StationsListTwo[i].StationName == "xfer")
                            {
                                ClassCommon.ApproachConveyorLineTwo = StationsListTwo[i].OutPutCnv;
                            }
                            else if (StationsListTwo[i].StationName == "Weigh")
                            {
                                ClassCommon.WeighConveyorLineTwo = StationsListTwo[i].OutPutCnv;
                            }
                            else if (StationsListTwo[i].StationName == "Press")
                            {
                                ClassCommon.PressConveyorLineTwo = StationsListTwo[i].OutPutCnv;
                            }
                            else if (StationsListTwo[i].StationName == "Marker")
                            {
                                ClassCommon.MarkerConveyorLineTwo = StationsListTwo[i].OutPutCnv;
                            }
                        }
                    }
                }
                //LayBoy at the End of the list
                using (DataTable StationAll = _sqlHandler.GetBaleStationsTable(queryAll))
                {
                    if (StationAll.Rows.Count > 0)
                    {
                        for (int i = 0; i < StationAll.Rows.Count; i++)
                        {
                            StationsList.Add(new ClsStation(Convert.ToInt32(StationAll.Rows[i]["ID"].ToString()),
                             StationAll.Rows[i]["Name"].ToString().Remove(0, 4),
                            Convert.ToInt32(StationAll.Rows[i]["InternalConveyor"].ToString()),
                            Convert.ToInt32(StationAll.Rows[i]["ProcessingLine"].ToString()),
                            Convert.ToInt32(StationAll.Rows[i]["OutputConveyor"].ToString()),
                            Convert.ToInt32(StationAll.Rows[i]["InputConveyor"].ToString())
                            ));
                        }
                        ClassCommon.LayBoy1ConveyorId = StationsList[StationsList.Count - 1].OutPutCnv;
                        ClsSerilog.LogMessage(ClsSerilog.Info, $"0. ClassCommon.LayBoy1ConveyorId -> {ClassCommon.LayBoy1ConveyorId}");
                    }
                }
                for (int i = 0; i < StationsList.Count; i++)
                {
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Station Name =  {StationsList[i].StationName}," +
                        $" StationId =  {StationsList[i].StationId}," +
                        $" InputConveyor =  {StationsList[i].InputCnv}," +
                        $" OutputConveyor =  {StationsList[i].OutPutCnv}," +
                        $" ProcessLine =  {StationsList[i].Line}");

                    ClassCommon.StationOneLineOneID = i;
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in SetBaleStations -> { ex.Message}");
                MessageBox.Show($"ERROR in SetBaleStations {ex.Message}");
            }
            return StationsList;
        }



        private List<ClsBaleStation> GetBaleStationsTableList()
        {
            List<ClsBaleStation> stationsList = new List<ClsBaleStation>();
            string queryOne = $"SELECT * FROM [Stations] with(NOLOCK) WHERE ProcessingLine = 1 ORDER By [ID] ASC";

            try
            {
                using (DataTable Station = _sqlHandler.GetBaleStationsTable(queryOne))
                {
                    if (Station.Rows.Count > 0)
                    {
                        for (int i = 0; i < Station.Rows.Count; i++)
                        {
                            stationsList.Add(new ClsBaleStation(Convert.ToInt32(Station.Rows[i]["ID"].ToString()),
                             Station.Rows[i]["Name"].ToString().Remove(0, 4),
                            Convert.ToInt32(Station.Rows[i]["InternalConveyor"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["ProcessingLine"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["OutputConveyor"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["InputConveyor"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["Device1ID"].ToString())
                            ));
                        }
                        //ClassCommon.LayBoy1ConveyorId = StationsList[StationsList.Count - 1].OutPutCnv;
                       // ClsSerilog.LogMessage(ClsSerilog.Info, $"0. ClassCommon.LayBoy1ConveyorId -> {ClassCommon.LayBoy1ConveyorId}");
                    }
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Access Error in GetBaleStationsTableList -> {ex.Message}");
                MessageBox.Show($"Access Error in GetBaleStationsTableList -> {ex.Message}");
            }
            return stationsList;
        }



        private List<ClsStation> SetBaleStations()
        {
            ClsSerilog.LogMessage(ClsSerilog.Info, $"Set BaleStations");
            List<ClsStation> StationsList = new List<ClsStation>();
            string queryOne = $"SELECT * FROM [Stations] with(NOLOCK) WHERE ProcessingLine = 1 ORDER By [ID] ASC";
            string queryTwo = $"SELECT * FROM [Stations]  with(NOLOCK) WHERE ProcessingLine = 2 ORDER By [ID] ASC";
            string queryAll = $"SELECT * FROM [Stations]  with(NOLOCK) WHERE ProcessingLine = 0 ORDER By [ID] ASC";

            try
            {
                GetStationsGraphicName();

                using (DataTable Station = _sqlHandler.GetBaleStationsTable(queryOne))
                {
                    if (Station.Rows.Count > 0)
                    {
                        for (int i = 0; i < Station.Rows.Count; i++)
                        {
                            StationsList.Add(new ClsStation(Convert.ToInt32(Station.Rows[i]["ID"].ToString()),
                             Station.Rows[i]["Name"].ToString().Remove(0, 4),
                            Convert.ToInt32(Station.Rows[i]["InternalConveyor"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["ProcessingLine"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["OutputConveyor"].ToString()),
                            Convert.ToInt32(Station.Rows[i]["InputConveyor"].ToString())
                            ));
                        }
                    }
                }

                for (int i = 0; i < StationsList.Count; i++)
                {
                    if (StationsList[i].StationName == "Weigh")
                    {
                        ClassCommon.WeighConveyorLineOne = StationsList[i].OutPutCnv;
                        if (i - 1 > -1)
                        {
                            ClassCommon.ApproachConveyorLineOne = StationsList[i - 1].OutPutCnv;
                        }
                    }
                    else if (StationsList[i].StationName == "Press")
                    {
                        ClassCommon.PressConveyorLineOne = StationsList[i].OutPutCnv;
                    }
                    else if (StationsList[i].StationName == "Marker")
                    {
                        ClassCommon.MarkerConveyorLineOne = StationsList[i].OutPutCnv;
                    }
                }

                ///Line 2 ///////////////////////////////////////////////////////////////////////////////////////////////
                ///
                using (DataTable StationTwo = _sqlHandler.GetBaleStationsTable(queryTwo))
                {
                    List<ClsStation> StationsListTwo = new List<ClsStation>();

                    if (StationTwo.Rows.Count > 0)
                    {
                        //  ClsSerilog.LogMessage(ClsSerilog.Info, $"First ID -> {Convert.ToInt32(StationTwo.Rows[0]["ID"])},  Rows Count => {StationTwo.Rows.Count}");

                        for (int i = 0; i < StationTwo.Rows.Count; i++)
                        {
                            StationsList.Add(new ClsStation(Convert.ToInt32(StationTwo.Rows[i]["ID"].ToString()),
                             StationTwo.Rows[i]["Name"].ToString().Remove(0, 4),
                            Convert.ToInt32(StationTwo.Rows[i]["InternalConveyor"].ToString()),
                            Convert.ToInt32(StationTwo.Rows[i]["ProcessingLine"].ToString()),
                            Convert.ToInt32(StationTwo.Rows[i]["OutputConveyor"].ToString()),
                            Convert.ToInt32(StationTwo.Rows[i]["InputConveyor"].ToString())
                            ));

                            StationsListTwo.Add(new ClsStation(Convert.ToInt32(StationTwo.Rows[i]["ID"].ToString()),
                            StationTwo.Rows[i]["Name"].ToString().Remove(0, 4),
                           Convert.ToInt32(StationTwo.Rows[i]["InternalConveyor"].ToString()),
                           Convert.ToInt32(StationTwo.Rows[i]["ProcessingLine"].ToString()),
                           Convert.ToInt32(StationTwo.Rows[i]["OutputConveyor"].ToString()),
                           Convert.ToInt32(StationTwo.Rows[i]["InputConveyor"].ToString())
                           ));
                        }

                        for (int i = 0; i < StationsListTwo.Count; i++)
                        {
                            if (StationsListTwo[i].StationName == "xfer")
                            {
                                ClassCommon.ApproachConveyorLineTwo = StationsListTwo[i].OutPutCnv;
                            }
                            else if (StationsListTwo[i].StationName == "Weigh")
                            {
                                ClassCommon.WeighConveyorLineTwo = StationsListTwo[i].OutPutCnv;
                            }
                            else if (StationsListTwo[i].StationName == "Press")
                            {
                                ClassCommon.PressConveyorLineTwo = StationsListTwo[i].OutPutCnv;
                            }
                            else if (StationsListTwo[i].StationName == "Marker")
                            {
                                ClassCommon.MarkerConveyorLineTwo = StationsListTwo[i].OutPutCnv;
                            }
                        }
                    }
                }
                //LayBoy at the End of the list
                using (DataTable StationAll = _sqlHandler.GetBaleStationsTable(queryAll))
                {
                    if (StationAll.Rows.Count > 0)
                    {
                        for (int i = 0; i < StationAll.Rows.Count; i++)
                        {
                            StationsList.Add(new ClsStation(Convert.ToInt32(StationAll.Rows[i]["ID"].ToString()),
                             StationAll.Rows[i]["Name"].ToString().Remove(0, 4),
                            Convert.ToInt32(StationAll.Rows[i]["InternalConveyor"].ToString()),
                            Convert.ToInt32(StationAll.Rows[i]["ProcessingLine"].ToString()),
                            Convert.ToInt32(StationAll.Rows[i]["OutputConveyor"].ToString()),
                            Convert.ToInt32(StationAll.Rows[i]["InputConveyor"].ToString())
                            ));
                        }
                        ClassCommon.LayBoy1ConveyorId = StationsList[StationsList.Count - 1].OutPutCnv;
                        ClsSerilog.LogMessage(ClsSerilog.Info, $"0. ClassCommon.LayBoy1ConveyorId -> {ClassCommon.LayBoy1ConveyorId}");
                    }
                }
                for (int i = 0; i < StationsList.Count; i++)
                {
                    ClsSerilog.LogMessage(ClsSerilog.Info, $"Station Name =  {StationsList[i].StationName}," +
                        $" StationId =  {StationsList[i].StationId}," +
                        $" InputConveyor =  {StationsList[i].InputCnv}," +
                        $" OutputConveyor =  {StationsList[i].OutPutCnv}," +
                        $" ProcessLine =  {StationsList[i].Line}");

                    ClassCommon.StationOneLineOneID = i;
                }
            }
            catch (Exception ex)
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in SetBaleStations -> { ex.Message}");
                MessageBox.Show($"ERROR in SetBaleStations {ex.Message}");
            }
            return StationsList;
        }

        private void GetStationsGraphicName()
        {
            string queryGrap = $"SELECT * FROM [StationGraphics]  with(NOLOCK) ORDER By [UniqueID] ASC";

            using (DataTable Stationx = _sqlHandler.GetBaleStationsTable(queryGrap))
            {
                if (Stationx.Rows.Count > 0)
                {
                    for (int i = 0; i < Stationx.Rows.Count; i++)
                    {
                        ClassCommon.StationGraphicName.Add(Stationx.Rows[i]["Name"].ToString());

                      //  ClsSerilog.LogMessage(ClsSerilog.Info, $" StationGraphics Name =  {Stationx.Rows[i]["Name"]}," +
                      //        $"StationID =  { Stationx.Rows[i]["StationID"]}");
                    }
                }
            }
        }

        private void UpdateBaleMoveEventAsync(DateTime obj)
        {
            //SQL
            Application.Current.Dispatcher.Invoke(new Action(() => 
            { _ = _sqlHandler.GeBaleDataTableAsync().ContinueWith(t => ProcessDataAsync(t.Result)); }));

         //   _ = _sqlHandler.GeBaleDataTableAsync().ContinueWith(t => ProcessDataAsync(t.Result));

            //Access 
            //  _ = AcHandler.GeBaleDataTableAsync().ContinueWith(t => ProcessData(t.Result));
        }

        private async Task ProcessDataAsync(DataTable newData)
        {
            int DatRows = newData.Rows.Count;

            await Task.Run(() =>
            {
                // if (CurDataTable != newData)
                //{
                NewdataTable = newData;
                ClassCommon.NewDattable = newData;
                if (newData.Rows.Count > 0)
                {
                    for (int i = 0; i < newData.Rows.Count; i++)
                    {
                        for (int x = 0; x < DatRows - 1; x++)
                        {
                            if (Convert.ToInt32(newData.Rows[i]["ID"]) == x)
                            {
                                curAId[x] = (int)newData.Rows[i]["ConveyorID"];

                                //only new data
                                if (curAId[x] != preAId[x])
                                {
                                   
                                    if ((int)newData.Rows[i]["ConveyorID"] == ClassCommon.ApproachConveyorLineOne) 
                                    {
                                        if (newData.Rows[i]["NumbOnConv"].ToString() == "1")
                                        {      
                                            ClassCommon.NewDatIndex = i;
                                            _eventAggregator.GetEvent<UpdateBaleMoveEvent>().Publish(ClassCommon.ApproachConveyorLineOne);
                                        }
                                    }
                                    else if ((int)newData.Rows[i]["ConveyorID"] == ClassCommon.WeighConveyorLineOne) //Scale Weigh
                                    {
                                        ClassCommon.NewDatIndex = i;
                                        _eventAggregator.GetEvent<UpdateBaleMoveEvent>().Publish(ClassCommon.WeighConveyorLineOne);
                                    }
                                    else if ((int)newData.Rows[i]["ConveyorID"] == ClassCommon.PressConveyorLineOne) //Bale Press
                                    {
                                        ClassCommon.NewDatIndex = i;
                                        _eventAggregator.GetEvent<UpdateBaleMoveEvent>().Publish(ClassCommon.PressConveyorLineOne);

                                        // Tell the world to update data if this is the last station
                                        if(ClassCommon.UpdateDataAtStation == ClassCommon.Press)
                                            _eventAggregator.GetEvent<UpdateBaleDataEvent>().Publish(ClassCommon.DataReady);
                                        
                                    }
                                    else if ((int)newData.Rows[i]["ConveyorID"] == ClassCommon.MarkerConveyorLineOne) //Marker
                                    {
                                        ClassCommon.NewDatIndex = i;
                                       
                                        //
                                        _eventAggregator.GetEvent<UpdateBaleMoveEventLineTwo>().Publish(ClassCommon.MarkerConveyorLineTwo);
                                        //
                                        _eventAggregator.GetEvent<UpdateBaleMoveEvent>().Publish(ClassCommon.MarkerConveyorLineOne);

                                        // Tell the world to update data, the data processed after the Marker,
                                        if (ClassCommon.UpdateDataAtStation == ClassCommon.Marker)
                                            _eventAggregator.GetEvent<UpdateBaleDataEvent>().Publish(ClassCommon.DataReady);
                                    }


                                    //Line 2
                                    else if ((int)newData.Rows[i]["ConveyorID"] == ClassCommon.ApproachConveyorLineTwo)
                                    {
                                        
                                        if (newData.Rows[i]["NumbOnConv"].ToString() == "1")
                                        {
                                            ClassCommon.NewDatIndex = i;
                                            _eventAggregator.GetEvent<UpdateBaleMoveEventLineTwo>().Publish(ClassCommon.ApproachConveyorLineTwo);
                                        }
                                    }
                                    else if ((int)newData.Rows[i]["ConveyorID"] == ClassCommon.WeighConveyorLineTwo)
                                    {
                                        ClassCommon.NewDatIndex = i;
                                        _eventAggregator.GetEvent<UpdateBaleMoveEventLineTwo>().Publish(ClassCommon.WeighConveyorLineTwo);
                                    }
                                    else if ((int)newData.Rows[i]["ConveyorID"] == ClassCommon.PressConveyorLineTwo)
                                    {
                                        ClassCommon.NewDatIndex = i;
                                        _eventAggregator.GetEvent<UpdateBaleMoveEventLineTwo>().Publish(ClassCommon.PressConveyorLineTwo);
                                    }
                                    else if ((int)newData.Rows[i]["ConveyorID"] == ClassCommon.MarkerConveyorLineTwo)
                                    {
                                        ClassCommon.NewDatIndex = i;
                                        // Tell the world to update data, the data processed after the Marker,
                                        _eventAggregator.GetEvent<UpdateBaleDataEvent>().Publish(ClassCommon.MarkerConveyorLineTwo);
                                        //
                                        _eventAggregator.GetEvent<UpdateBaleMoveEventLineTwo>().Publish(ClassCommon.MarkerConveyorLineTwo);
                                    }
                                    
                                    //LayBoy
                                    else if ((int)newData.Rows[i]["ConveyorID"] == ClassCommon.LayBoy1ConveyorId)
                                    {
                                        ClsSerilog.LogMessage(ClsSerilog.Info, $"Bale at LayBoy1ConveyorId");
                                        _eventAggregator.GetEvent<UpdateBaleMoveEventLineTwo>().Publish(ClassCommon.LayBoy1ConveyorId);
                                    }
                                }
                                preAId[x] = curAId[x];
                            }
                        }
                    }
                }

            });
        }
    }
}
