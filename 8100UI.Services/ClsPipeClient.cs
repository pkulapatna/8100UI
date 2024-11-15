using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _8100UI.Services
{
    public class ClsPipeClient
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;
        private static readonly string strDevice = "Server1";
        private static string strPipeName = "testpipe2";
        private bool bPipeCon = false;

        private static ClsPipeClient instance = null;
        private static readonly object padlock = new object();

        NamedPipeClientStream pipeClient = null;

        public static ClsPipeClient Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ClsPipeClient(ClassApplicationService.Instance.EventAggregator);
                    }
                    return instance;
                }
            }
        }

        public ClsPipeClient(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
        }

        public void SendPipeMsg(string strtext)
        {
            string temp;

            if (!bPipeCon) ConnectPipe();

            StreamReader sr = new StreamReader(pipeClient);
            StreamWriter sw = new StreamWriter(pipeClient);

            temp = sr.ReadLine();

            if ((temp != null) && (temp == strDevice))
            {
                try
                {
                    sw.WriteLine(strtext);
                    sw.Flush();
                    pipeClient.Close();
                    bPipeCon = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in SendPipeMsg from 8100UI " + ex.Message);
                    ClsSerilog.LogMessage(ClsSerilog.Error, "Error in SendPipeMsg from 8100UI " + ex.Message);
                }
            }
        }

        private void ConnectPipe()
        {
            try
            {
                pipeClient = new NamedPipeClientStream(".",
                strPipeName, PipeDirection.InOut, PipeOptions.Asynchronous);

                if (pipeClient.IsConnected != true)
                {
                    pipeClient.Connect(5000); //set timeout for 5 secounds.
                    bPipeCon = true;
                }
                else
                    bPipeCon = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in ConnectPipe " + ex.Message);
                ClsSerilog.LogMessage(ClsSerilog.Error, "Error in ConnectPipe " + ex.Message);
            }
        }

        public void ClosePipeClient()
        {
            if (pipeClient != null)
            {
                pipeClient.Close();
                pipeClient = null;
            }
            ClsSerilog.LogMessage(ClsSerilog.Info, $"Close PipeClient {DateTime.Now}");
        }
    }
}
