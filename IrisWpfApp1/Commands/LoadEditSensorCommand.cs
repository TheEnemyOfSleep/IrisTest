using IrisWpfApp1.Models;
using IrisWpfApp1.Services;
using IrisWpfApp1.Stores;
using IrisWpfApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Commands
{
    public class LoadEditSensorCommand: CommandBase
    {
        private readonly SensorsStore _sensorsStore;
        private readonly EditSensorViewModel _viewModel;

        public override void Execute(object parameter)
        {
            IEnumerable<SensorState> sensorState = _sensorsStore.SensorStates;

            _viewModel.UpdateState(sensorState);
        }

        public LoadEditSensorCommand(SensorsStore sensorsStore, EditSensorViewModel viewModel)
        {
            _sensorsStore = sensorsStore;
            _viewModel = viewModel;
        }
    }
}
