using IrisWpfApp1.Commands;
using IrisWpfApp1.Models;
using IrisWpfApp1.Services;
using IrisWpfApp1.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IrisWpfApp1.ViewModels
{
    public class SensorsListingViewModel: ViewModelBase
    {
        private readonly ObservableCollection<SensorViewModel> _sensors;
        public IEnumerable<SensorViewModel> Sensors => _sensors;
        private SensorsStore _sensorsStore;
        private SignalsStore _signalsStore;
        private HistoryStore _historyStore;
        private SensorViewModel _sensor;
        private List<string> _sensorStates;


        public SensorViewModel SelectedSensor {
            get
            {
                return _sensor;
            }
            set
            {

                if (_sensor == value) return;
                _sensor = value;
                OnPropertyChanged(nameof(SelectedSensor));
            }
        }

        public ICommand LoadSensorsCommand { get; }

        public ICommand AddSensorCommand { get; }

        public ICommand EditSensorCommand { get; }

        public ICommand DeleteSensorCommand { get; }

        public ICommand SendAlarmSignalCommand { get; }

        public ICommand SendServiceSignalCommand { get; }

        public SensorsListingViewModel(SensorsStore sensorsStore,
                                       SignalsStore signalsStore,
                                       HistoryStore historyStore,
                                       NavigationService addSensorViewNavigationService,
                                       NavigationService editSensorViewNavigationService)
        {
            _sensorsStore = sensorsStore;
            _signalsStore = signalsStore;
            _historyStore = historyStore;
            _sensors = new ObservableCollection<SensorViewModel>();

            LoadSensorsCommand = new LoadSensorsCommand(_sensorsStore, this);
            AddSensorCommand = new NavigateCommand(addSensorViewNavigationService);
            EditSensorCommand = new NavigateCommand(editSensorViewNavigationService);
            DeleteSensorCommand = new DeleteSensorCommand(_sensorsStore, _historyStore, this);

            SendAlarmSignalCommand = new SendSignalCommand(_sensorsStore, _signalsStore, SignalType.Alarm, this);
            SendServiceSignalCommand = new SendSignalCommand(_sensorsStore, _signalsStore, SignalType.ServiseSignal, this);

            _sensorsStore.SensorAdded += OnSensorAdded;
            _sensorsStore.SensorEdited += SensorEdited;
            _sensorsStore.SensorDeleted += OnSensorDeleted;
        }

        private void SensorEdited(Sensor sensor)
        {
            SensorViewModel sensorViewModel = _sensors.FirstOrDefault(v => v.SensorNumber == sensor.SensorNumber);

            int i = _sensors.IndexOf(sensorViewModel);
            _sensors[i] = new SensorViewModel(_sensorsStore.SensorStates, sensor);

        }

        private void OnSensorAdded(Sensor sensor)
        {
            SensorViewModel sensorViewModel = new SensorViewModel(_sensorsStore.SensorStates, sensor);

            _sensors.Add(sensorViewModel);
        }


        private void OnSensorDeleted(Sensor sensor)
        {
            SensorViewModel sensorViewModel = _sensors.FirstOrDefault(v => v.SensorNumber == sensor.SensorNumber);

            _sensors.Remove(sensorViewModel);
        }

        public override void Dispose()
        {
            _sensorsStore.SensorDeleted -= OnSensorDeleted;
            base.Dispose();
        }

        public static SensorsListingViewModel LoadViewModel(SensorsStore sensorsStore,
                                                            SignalsStore signalsStore,
                                                            HistoryStore historyStore,
                                                            NavigationService addSensorViewNavigationService,
                                                            NavigationService editSensorViewNavigationService)
        {
            SensorsListingViewModel viewModel = new SensorsListingViewModel(sensorsStore, signalsStore, historyStore, addSensorViewNavigationService, editSensorViewNavigationService);

            viewModel.LoadSensorsCommand.Execute(null);

            return viewModel;
        }

        public void UpdateSensors(IEnumerable<SensorState> sensorState, IEnumerable<Sensor> sensors)
        {
            _sensors.Clear();
            _sensorStates = sensorState.Select(v => v.State).ToList();

            foreach (Sensor sensor in sensors)
            {
                SensorViewModel sensorViewModel = new SensorViewModel(sensorState, sensor);
                _sensors.Add(sensorViewModel);
            }
        }
    }
}
