using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8100UI.Services
{
    class ClassApplicationService
    {
        private ClassApplicationService() { }

        private static readonly ClassApplicationService _instance = new ClassApplicationService();
        public static ClassApplicationService Instance { get { return _instance; } }

        private Prism.Events.IEventAggregator _eventAggregator;

        public Prism.Events.IEventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null)
                    _eventAggregator = new Prism.Events.EventAggregator();
                return _eventAggregator;
            }
        }

    }
    public class BaleTimerEventMessage : PubSubEvent<DateTime> { }
    public class UpdateBaleMoveEvent : PubSubEvent<int> { }
    public class UpdateBaleDataEvent : PubSubEvent<int> { }
    public class UpdateBaleMoveEventLineTwo : PubSubEvent<int> { }
    public class UpdateMidBoxData : PubSubEvent<int> { }

    public class NamePipeMessage : PubSubEvent<string> { }
    public class ChangeFieldValue : PubSubEvent<string> { }
    public class BaleDataTableChnges : PubSubEvent<bool> { }
    public class SaveModFieldEvent : PubSubEvent<int> { }
    public class CloseObjModWindow : PubSubEvent<bool> { }
    public class ChangeMidBoxValue : PubSubEvent<string> { }
    public class CloseMainWindow : PubSubEvent<bool> { }
    public class ClosingLogWindow : PubSubEvent<bool> { }
    public class MoveLogWindow : PubSubEvent<bool> { }
    public class UpdateMainTimerEvents : PubSubEvent<DateTime> { }
    public class RemoveBaleEvent : PubSubEvent<int> { }
    public class DelayPressTimerStop : PubSubEvent<DateTime> { }
    public class DelayScaleTimerStop : PubSubEvent<DateTime> { }

    public class Reporttextmessage : PubSubEvent<string> { }
    public class DebugTextmessage : PubSubEvent<string> { }
    public class BaleAtScaleMoveEvent : PubSubEvent<int> { }
    public class BaleAtPressMoveEvent : PubSubEvent<int> { }
    public class NamePipeCloseMessage : PubSubEvent<string> { }
    public class NamePipeUserLogin : PubSubEvent<string> { }
    public class NamePipeCloseLot : PubSubEvent<string> { }
    public class UpdateNewLotNumA : PubSubEvent<string> { }
    public class UpdateNewLotNumB : PubSubEvent<string> { }

    public class ChangeLotSizeA : PubSubEvent<string> { }
    public class ChangeLotSizeB : PubSubEvent<string> { }

    public class UserLogin : PubSubEvent<bool> { }
}
