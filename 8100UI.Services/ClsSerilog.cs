﻿using Serilog;
using Serilog.Events;
using System;
using System.Windows;

namespace _8100UI.Services
{
    public class ClsSerilog
    {
        public static int Info = 0;
        public static int Warning = 1;
        public static int Error = 2;
        public static int Fatal = 3;
        public static int Debug = 4;

        public ClsSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                         .MinimumLevel.Debug()
                         .Enrich.FromLogContext()
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                         .Enrich.FromLogContext()
                         .WriteTo.File($"C:\\ForteLog\\ASCIILog\\8100UI_Log_.Log", rollingInterval: RollingInterval.Month)
                         .CreateLogger();
        }

        public static void LogMessage(int logidx, string strMessage)
        {
            try
            {
                switch (logidx)
                {
                    case 0:
                        Log.Information(strMessage);
                        break;
                    case 1:
                        Log.Warning(strMessage);
                        break;
                    case 2:
                        Log.Error(strMessage);
                        break;
                    case 3:
                        Log.Fatal(strMessage);
                        break;
                    case 4:
                        Log.Debug(strMessage);
                        break;
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show($"ERROR in  LogMessage {ex.Message}");
               Log.Fatal(ex, "LOG ERROR");
            }
        }
        public static void CloseLogger()
        {
            Log.Information("Close and Flush Log");
            Log.CloseAndFlush();
        }
    }
}
