using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _8100UI.Services
{
    public class ClsPipeMessage
    {
        private static ClsPipeClient PipeClient;

        public ClsPipeMessage()
        {
            PipeClient = ClsPipeClient.Instance;
        }

        public static void SendPipeMessage(string strMessage)
        {
            ClsSerilog.LogMessage(ClsSerilog.Info, $"SendPipeMessage -> {strMessage}");
            try
            {
                if (PipeClient == null) PipeClient = ClsPipeClient.Instance;
                PipeClient.SendPipeMsg(strMessage);
                PipeClient = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in SendPipeMessage -> {ex.Message}");
                ClsSerilog.LogMessage(ClsSerilog.Info, $"ERROR in SendPipeMessage -> {ex.Message}");
            }
        }

    }
}
