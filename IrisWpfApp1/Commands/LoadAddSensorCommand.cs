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
    public class LoadAddSensorCommand : CommandBase
    {
        private readonly SensorsStore _sensorsStore;
        private readonly AddSensorViewModel _viewModel;

        public override void Execute(object parameter)
        {
            IEnumerable<SensorState> sensorStates = _sensorsStore.SensorStates;

            _viewModel.UpdateState(sensorStates);
        }

        public LoadAddSensorCommand(SensorsStore sensorsStore, AddSensorViewModel viewModel)
        {
            _sensorsStore = sensorsStore;
            _viewModel = viewModel;
        }
    }
}
