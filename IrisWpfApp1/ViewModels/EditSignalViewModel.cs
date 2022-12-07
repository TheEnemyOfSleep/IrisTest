using IrisWpfApp1.Commands;
using IrisWpfApp1.Models;
using IrisWpfApp1.Services;
using IrisWpfApp1.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IrisWpfApp1.ViewModels
{
    public class EditSignalViewModel: ViewModelBase
    {
        private int _id;
        private string _sensorNumber;
        private SignalType _signalType;
        private DateTime _date;
        private SignalsStore _signalsStore;
        private const string _additionalInformation = "";

        public string AdditionalInformation
        {
            get
            {
                return _additionalInformation;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string SensorNumber
        {
            get
            {
                return _sensorNumber;
            }
            set
            {
                _sensorNumber = value;
                OnPropertyChanged(nameof(SensorNumber));
            }
        }

        public IEnumerable<SignalType> SignalTypes
        {
            get
            {
                return Enum.GetValues(typeof(SignalType)).Cast<SignalType>();
            }
        }

        public SignalType SignalType
        {
            get
            {
                return _signalType;
            }
            set
            {
                _signalType = value;
                OnPropertyChanged(nameof(SignalType));
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public EditSignalViewModel(SignalsStore signalsStore, NavigationService signalViewNavigationService)
        {
            _signalsStore = signalsStore;
            _date = DateTime.Now;

            SubmitCommand = new EditSignalCommand(_signalsStore, this, signalViewNavigationService);
            CancelCommand = new NavigateCommand(signalViewNavigationService);
        }
    }
}
