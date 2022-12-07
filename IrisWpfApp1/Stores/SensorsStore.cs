using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Stores
{
    public class SensorsStore
    {
        private readonly List<Sensor> _sensors;
        private readonly List<SensorState> _sensorStates;
        private readonly Sensors _sensorsModel;
        private readonly Lazy<Task> _initLazy;

        public IEnumerable<Sensor> Sensors => _sensors;
        public IEnumerable<SensorState> SensorStates => _sensorStates;

        public SensorsStore(Sensors sensors)
        {
            _sensorsModel = sensors;
            _initLazy = new Lazy<Task>(() => Initialize());

            _sensors = new List<Sensor>();
            _sensorStates = new List<SensorState>();
        }

        public event Action<Sensor> SensorAdded;
        public event Action<Sensor> SensorEdited;
        public event Action<Sensor> SensorDeleted;

        public async Task Load()
        {
            await _initLazy.Value;
        }

        private async Task Initialize()
        {
            IEnumerable<Sensor> sensors = await _sensorsModel.GetAllSensors();
            IEnumerable<SensorState> sensorsStates = await _sensorsModel.GetAsyncSensorStates();

            _sensors.Clear();
            _sensors.AddRange(sensors);

            _sensorStates.Clear();
            _sensorStates.AddRange(sensorsStates);
        }

        public async Task AddSensor(Sensor sensor)
        {
            await _sensorsModel.AddSensor(sensor);
            _sensors.Add(sensor);

            OnSensorAdded(sensor);
        }

        private void OnSensorAdded(Sensor sensor)
        {
            SensorAdded?.Invoke(sensor);
        }

        public async Task EditSensor(Sensor sensor)
        {
            await _sensorsModel.EditSensor(sensor);
            int i = _sensors.FindIndex(v => v.SensorNumber == sensor.SensorNumber);
            _sensors[i] = sensor;

            OnSensorEdited(sensor);
        }

        private void OnSensorEdited(Sensor sensor)
        {
            SensorEdited?.Invoke(sensor);
        }


        // Delete commands
        public async Task DeleteSensor(Sensor sensor)
        {
            await _sensorsModel.DeleteSensor(sensor);
            _sensors.Remove(sensor);

            OnSensorDeleted(sensor);
        }

        public async Task DeleteSensor(string sensorNumber)
        {
            await _sensorsModel.DeleteSensor(sensorNumber);
            Sensor removalItem = _sensors.FirstOrDefault(v => v.SensorNumber == sensorNumber);
            _sensors.Remove(removalItem);

            OnSensorDeleted(removalItem);
        }

        private void OnSensorDeleted(Sensor sensor)
        {
            SensorDeleted?.Invoke(sensor);
        }
    }
}
