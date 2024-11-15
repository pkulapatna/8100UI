using _8100UI.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Media;

namespace PulpBale.ViewModels
{
    public class BaleViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private Brush _baleColor;
        public Brush BaleColor
        {
            get { return _baleColor; }
            set { SetProperty(ref _baleColor, value); }
        }

        private Brush _baletxtColor;
        public Brush BaletxtColor
        {
            get { return _baletxtColor; }
            set { SetProperty(ref _baletxtColor, value); }
        }

        private int _baleNumber;
        public int BaleNumber
        {
            get { return _baleNumber; }
            set { SetProperty(ref _baleNumber, value); }
        }

        public BaleViewModel(IEventAggregator eventAggregator, int baleOne)
        {
            this._eventAggregator = eventAggregator;
            this.BaleNumber = baleOne;
        }

        private DelegateCommand _removeBaleCommand;
        public DelegateCommand RemoveBaleCommand =>
        _removeBaleCommand ?? (_removeBaleCommand =
            new DelegateCommand(RemoveBaleCommandExecute));
        private void RemoveBaleCommandExecute()
        {
            _eventAggregator.GetEvent<RemoveBaleEvent>().Publish(BaleNumber);
           
            ClsPipeMessage.SendPipeMessage($"RemoveBale;{BaleNumber};");

            ClsSerilog.LogMessage(ClsSerilog.Info, $"Remove bale {BaleNumber}");
        }

        public void SetBaleColor(Brush balecolor, Brush baletxtColor)
        {
            this.BaleColor = balecolor;
            this.BaletxtColor = baletxtColor;
        }
    }
}
