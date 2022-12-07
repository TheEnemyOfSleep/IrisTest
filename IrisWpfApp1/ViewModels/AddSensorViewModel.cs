using IrisWpfApp1.Commands;
using IrisWpfApp1.Models;
using IrisWpfApp1.Services;
using IrisWpfApp1.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace IrisWpfApp1.ViewModels
{
    public class AddSensorViewModel: ViewModelBase
    {
        private string _sensorNumber;
        private string _location;
        private int _charge;
        private string _sensorState;
        private string _username;
        private List<string> _sensorStates;
        private readonly SensorsStore _sensorsStore;
        private readonly HistoryStore _historyStore;

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

        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public int Charge
        {
            get
            {
                return _charge;
            }
            set
            {
                _charge = value;
                OnPropertyChanged(nameof(Charge));
            }
        }

        public List<string> SensorStates
        {
            get
            {
                return _sensorStates;
            }
        }


        public string SensorState
        {
            get
            {
                return _sensorState;
            }
            set
            {
                
                if (_sensorState == value) return;
                _sensorState = value;
                OnPropertyChanged(nameof(SensorState));
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {

                if (_username == value) return;
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public ICommand LoadAddSensorCommand { get; }

        public ICommand SubmitCommand { get; }

        public ICommand CancelCommand { get; }

        public AddSensorViewModel(SensorsStore sensorsStore, HistoryStore historyStore, NavigationService sensorViewNavigationService)
        {
            _sensorsStore = sensorsStore;
            _historyStore = historyStore;
            _sensorStates = sensorsStore.SensorStates.Select(v => v.State).ToList();

            LoadAddSensorCommand = new LoadAddSensorCommand(_sensorsStore, this);
            SubmitCommand = new AddSensorCommand(_sensorsStore, _historyStore, this, sensorViewNavigationService);
            CancelCommand = new NavigateCommand(sensorViewNavigationService);
        }

        public static AddSensorViewModel LoadViewModel(SensorsStore sensorsStore, HistoryStore historyStore, NavigationService sensorViewNavigationService)
        {
            AddSensorViewModel viewModel = new AddSensorViewModel(sensorsStore, historyStore, sensorViewNavigationService);

            viewModel.LoadAddSensorCommand.Execute(null);

            return viewModel;
        }

        public void UpdateState(IEnumerable<SensorState> sensorStates)
        {
            _sensorStates = sensorStates.Select(v => v.State).ToList();
        }
    }
}
