using IrisWpfApp1.Models;
using IrisWpfApp1.Stores;
using IrisWpfApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Commands
{
    public class LoadSensorsCommand : AsyncCommandBase
    {
        private readonly SensorsStore _sensorsStore;
        private readonly SensorsListingViewModel _viewModel;

        public override async Task ExecuteAsync(object parameter)
        {
             await _sensorsStore.Load();

            _viewModel.UpdateSensors(_sensorsStore.SensorStates, _sensorsStore.Sensors);
        }

        public LoadSensorsCommand(SensorsStore sensorsStore, SensorsListingViewModel viewModel)
        {
            _sensorsStore = sensorsStore;
            _viewModel = viewModel;
        }
    }
}
