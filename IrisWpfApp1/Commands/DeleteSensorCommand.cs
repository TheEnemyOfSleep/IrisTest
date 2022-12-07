using IrisWpfApp1.Models;
using IrisWpfApp1.Stores;
using IrisWpfApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Commands
{
    public class DeleteSensorCommand : AsyncCommandBase
    {
        private SensorsStore _sensorsStore;
        private HistoryStore _historyStore;
        private SensorsListingViewModel _sensorsListingViewModel;

        public DeleteSensorCommand(SensorsStore sensorsStore, HistoryStore historyStore, SensorsListingViewModel sensorsListingViewModel)
        {
            _sensorsStore = sensorsStore;
            _historyStore = historyStore;
            _sensorsListingViewModel = sensorsListingViewModel;
            _sensorsListingViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _sensorsListingViewModel.SelectedSensor != null &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string sensorNumber = _sensorsListingViewModel.SelectedSensor.SensorNumber;

            await _sensorsStore.DeleteSensor(sensorNumber);
            await _historyStore.CreateHistory(new History(sensorNumber, ActionType.Delete, "unknown"));
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SensorsListingViewModel.SelectedSensor))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
