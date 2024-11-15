using Prism.Events;
using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Windows;

namespace _8100UI.Services
{
    public class ClsPipeServer
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;
        private static ManualResetEvent _pauseEvent = new ManualResetEvent(true);

        private static ClsPipeServer instance = null;
        private static readonly object padlock = new object();

        NamedPipeServerStream pipeServer;
        public Thread listenerThread;
        private static string strDevice = "Server1";
        private static string strPipeName = "testpipe";

        public static ClsPipeServer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ClsPipeServer(ClassApplicationService.Instance.EventAggregator);
                    }
                    return instance;
                }
            }
        }

        public ClsPipeServer(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            listenerThread = new Thread(this.PipeThread);
        }


        private void PipeThread()
        {
            pipeServer = new NamedPipeServerStream(strPipeName, PipeDirection.InOut, 4);
            // ShowStatus("Created new Thread and wait for Message");
            Listen(strDevice);
            //ReceiveByteAndRespond();
        }

        public void StartListen()
        {
            ClsSerilog.LogMessage(ClsSerilog.Info, $"Start PipeServer -> {DateTime.Now}");
            listenerThread.Start();
        }


        /// <summary>
        /// Listen to the Sdev
        /// </summary>
        /// <param name="Sdev"></param>
        private void Listen(string Sdev)
        {
            StreamReader sr = new StreamReader(pipeServer);
            StreamWriter sw = new StreamWriter(pipeServer);

            ClsSerilog.LogMessage(ClsSerilog.Info, $"Pipe Start Listening to => {Sdev}");

            do
            {
                try
                {
                    //the server will wait until a client process connects
                    pipeServer.WaitForConnection();
                    string test;

                    sw.WriteLine(Sdev);
                    sw.Flush();

                    pipeServer?.WaitForPipeDrain();
                    test = sr?.ReadLine();

                    if (test != null)
                    {
                        ProsIncommingPipeMsg(test);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"ERROR in Listen {ex.Message}");
                    ClsSerilog.LogMessage(ClsSerilog.Error, $"ERROR in Listen {ex.Message}");
                }

                finally
                {
                    if (pipeServer.IsConnected)
                    {
                        pipeServer.WaitForPipeDrain();
                        pipeServer.Disconnect();
                    }
                    else
                    {
                        //ShowStatus(" In Listen -> Thread has Stopped");
                    }
                }

            } while (listenerThread.IsAlive);

        }

        private static void ReceiveByteAndRespond()
        {
            int test;
            using (NamedPipeClientStream namedPipeClient = new NamedPipeClientStream(strPipeName))
            {
                namedPipeClient.Connect();
                test = namedPipeClient.ReadByte();
                Console.WriteLine(test);
                namedPipeClient.WriteByte(2);
            }
        }


        public void ProsIncommingPipeMsg(string strMsg)
        {
            string MsgRec = strMsg;

            try
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                { ClsSerilog.LogMessage(ClsSerilog.Info, $"Pipe Message Received -> {strMsg} at {DateTime.Now}"); }));

                strMsg = strMsg.TrimEnd(';');

                string firstWord = string.Empty;
                string[] words = strMsg.Split(';');

                if (words.Count() == 1)
                    firstWord = words[0];
                if (words.Count() == 2)
                    firstWord = words[0];

                switch (firstWord)
                {
                    case "Close":
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        { _eventAggregator.GetEvent<NamePipeCloseMessage>().Publish("Close"); }));
                        break;

                    case "Logon":
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        { _eventAggregator.GetEvent<NamePipeUserLogin>().Publish(MsgRec); }));
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        { ClsSerilog.LogMessage(ClsSerilog.Info, $"User LogOn -> {MsgRec} at {DateTime.Now}"); }));
                        break;

                    case "Logoff":
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        { _eventAggregator.GetEvent<NamePipeUserLogin>().Publish(MsgRec); }));
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        { ClsSerilog.LogMessage(ClsSerilog.Info, $"User LogOff -> {MsgRec} at {DateTime.Now}"); }));
                        break;

                    case "CloseLot": //Need to refresh lot summary table. "CloseLot" = "0001" or "ALL"
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        { _eventAggregator.GetEvent<NamePipeCloseLot>().Publish("CloseLot"); }));
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in ProsIncommingPipeMsg {ex.Message}");
                ClsSerilog.LogMessage(ClsSerilog.Error, $"ERROR in ProsIncommingPipeMsg {ex.Message}");
            }
        }

        public void ShowStatus(string strMsg)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            { ClsSerilog.LogMessage(ClsSerilog.Info, $"ShowStatus -> {strMsg} "); }));
        }

        private void Pause()
        {
            _pauseEvent.Reset();
            Console.WriteLine("Thread paused");
        }

        private void Resume()
        {
            _pauseEvent.Set();
            Console.WriteLine("Thread resuming ");
        }

        [Obsolete]
        public void SuspendThread()
        {
            if (listenerThread.IsAlive)
            {
                // MainWindow.AppWindow.txtStatus.Text = "Listener Thread has Suspended";
                Pause();
                listenerThread.Suspend();
            }
        }

        [Obsolete]
        public void ResumeThread()
        {
            if (listenerThread.IsAlive)
            {
                // MainWindow.AppWindow.txtStatus.Text = "Listener Thread Resumed";
                Resume();
                listenerThread.Resume();
            }
        }

        public void KillThread()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            { ClsSerilog.LogMessage(ClsSerilog.Info, $"Stop PipeServer Thread -> {DateTime.Now}"); }));
            listenerThread?.Abort();
            listenerThread = null;
            // System.Diagnostics.Process.GetCurrentProcess().Kill();


        }
    }
}
