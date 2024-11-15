using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8100UI.Services
{
    public class ServiceEventsTimer
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;
  
        private System.Windows.Threading.DispatcherTimer EventsTimer;
        private System.Windows.Threading.DispatcherTimer ScaleDelayTimer;
        private System.Windows.Threading.DispatcherTimer PressDelayTimer;

        private static readonly object padlock = new object();
        private static ServiceEventsTimer instance = null;

        public static ServiceEventsTimer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceEventsTimer(ClassApplicationService.Instance.EventAggregator);
                    }
                    return instance;
                }
            }
        }

        public ServiceEventsTimer(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
        }


        #region EventsTimer
        public void InitStartEventsTimer(string eventTag)
        {
            InitializeStartEventsTimer(eventTag);
        }
        public void StopEventsTimer()
        {
            EventsTimer?.Stop();
        }
        private void InitializeStartEventsTimer(string eventTag)
        {
            if (EventsTimer != null) EventsTimer = null;
            EventsTimer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds((int)ClassCommon.ScanPeriod)
            };
            EventsTimer.Tick += new EventHandler(EventsTimer_Tick);
            EventsTimer.Tag = eventTag;
            EventsTimer?.Start();

            ClsSerilog.LogMessage(ClsSerilog.Info, $"Start EventsTimer");
        }
        private void EventsTimer_Tick(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => { SetBaleEventsTimerActions(); }));
        }

        private void SetBaleEventsTimerActions()
        {
            _eventAggregator.GetEvent<UpdateMainTimerEvents>().Publish(DateTime.Now);
        }
        public void StopBaleEventsTimer()
        {
            ClsSerilog.LogMessage(ClsSerilog.Info, $"Stop EventsTimer");
            if (EventsTimer != null)
            {
                EventsTimer.Stop();
                EventsTimer = null;
            }
        }
        #endregion EventsTimer 

        #region ScaleDelayTimer
        public void InitStartScaleEventsDelayTimer(string eventTag)
        {
            InitializeStartScaleDelayTimer(eventTag);
        }
        private void InitializeStartScaleDelayTimer(string eventTag)
        {
            if (ScaleDelayTimer != null) ScaleDelayTimer = null;
            ScaleDelayTimer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(2000)
            };
            ScaleDelayTimer.Tick += new EventHandler(EventsScaleDelayTimer_Tick);
            ScaleDelayTimer.Tag = eventTag;
            ScaleDelayTimer?.Start();
        }
        private void EventsScaleDelayTimer_Tick(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => { FinishScaleTimerActions(); }));
        }
        private void FinishScaleTimerActions()
        {
            _eventAggregator.GetEvent<DelayScaleTimerStop>().Publish(DateTime.Now);
            ScaleDelayTimer?.Stop();

        }
        #endregion ScaleDelayTimer 



        #region PressDelayTimer
        public void InitStartPressEventsDelayTimer(string eventTag)
        {
            InitializeStartEventsDelayTimer(eventTag);
        }
        private void InitializeStartEventsDelayTimer(string eventTag)
        {
            if (PressDelayTimer != null) PressDelayTimer = null;
            PressDelayTimer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            PressDelayTimer.Tick += new EventHandler(EventsDelayTimer_Tick);
            PressDelayTimer.Tag = eventTag;
            PressDelayTimer?.Start();
        }
        private void EventsDelayTimer_Tick(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => { FinishPressTimerActions(); }));  
        }
        private void FinishPressTimerActions()
        {
           
            _eventAggregator.GetEvent<DelayPressTimerStop>().Publish(DateTime.Now);
            PressDelayTimer?.Stop();
            
        }
        #endregion PressDelayTimer 


    }
}
