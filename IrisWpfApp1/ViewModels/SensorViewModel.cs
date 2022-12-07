using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.ViewModels
{
    public class SensorViewModel
    {
        private readonly Sensor _sensor;
        private readonly List<SensorState> _sensorStates;

        public string SensorNumber => _sensor.SensorNumber;
        public string Location => _sensor.Location;
        public string Charge => _sensor.Charge.ToString() + "%";
        public string SensorStateId => _sensorStates[_sensor.SensorStateId].State;

        public SensorViewModel(IEnumerable<SensorState> sensorState, Sensor sensor)
        {
            _sensorStates = sensorState.ToList();
            _sensor = sensor;
        }
    }
}
