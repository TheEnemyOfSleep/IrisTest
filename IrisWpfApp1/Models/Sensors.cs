using IrisWpfApp1.Exceptions;
using IrisWpfApp1.Services.HistoryCreator;
using IrisWpfApp1.Services.SensorConflictValidators;
using IrisWpfApp1.Services.SensorCreators;
using IrisWpfApp1.Services.SensorEditor;
using IrisWpfApp1.Services.SensorProviders;
using IrisWpfApp1.Services.SensorRemover;
using IrisWpfApp1.Services.StateProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Models
{
    public class Sensors
    {
        private readonly ISensorProvider _sensorProvider;
        private readonly IStateProvider _stateProvider;
        private readonly ISensorCreator _sensorCreator;
        private readonly ISensorEditor _sensorEditor;
        private readonly ISensorRemover _sensorRemover;
        private readonly ISensorConflictValidator _sensorConflictValidator;

        public Sensors(ISensorProvider sensorProvider,
                       IStateProvider stateProvider,
                       ISensorCreator sensorCreator,
                       ISensorEditor sensorEditor,
                       ISensorRemover sensorRemover,
                       ISensorConflictValidator sensorConflictValidator)
        {
            _sensorProvider = sensorProvider;
            _stateProvider = stateProvider;
            _sensorCreator = sensorCreator;
            _sensorEditor = sensorEditor;
            _sensorRemover = sensorRemover;
            _sensorConflictValidator = sensorConflictValidator;
        }

        public async Task<IEnumerable<Sensor>> GetAllSensors()
        {
            return await _sensorProvider.GetAllSensors();
        }

        public async Task<Sensor> GetSensorById(Sensor sensor)
        {
            return await _sensorProvider.GetSensorById(sensor);
        }

        public async Task<IEnumerable<SensorState>> GetAsyncSensorStates()
        {
            return await _stateProvider.GetAsyncAllStates();
        }

        public IEnumerable<SensorState> GetSensorStates()
        {
            return _stateProvider.GetAllStates();
        }

        public async Task AddSensor(Sensor sensor)
        {
            Sensor conflictSensor = await _sensorConflictValidator.GetConflictingSensor(sensor);

            if(conflictSensor != null)
            {
                throw new SensorConflictException(conflictSensor, sensor);
            }

            await _sensorCreator.CreateSensor(sensor);
        }

        public async Task EditSensor(Sensor sensor)
        {
            Sensor conflictSensor = await _sensorConflictValidator.GetConflictingSensor(sensor);
            if (conflictSensor == null)
            {
                throw new SensorConflictException(conflictSensor, sensor);
            }

            await _sensorEditor.EditSensor(sensor);
        }

        public async Task DeleteSensor(Sensor sensor)
        {
            Sensor conflictSensor = await _sensorConflictValidator.GetConflictingSensor(sensor);
            if (conflictSensor == null)
            {
                throw new SensorConflictException(conflictSensor, sensor);
            }

            await _sensorRemover.RemoveSensor(sensor);
        }

        public async Task DeleteSensor(string sensorNumber)
        {
            Sensor conflictSensor = await _sensorConflictValidator.GetConflictingSensor(sensorNumber);
            if (conflictSensor == null)
            {
                throw new SensorConflictException(conflictSensor, null);
            }

            await _sensorRemover.RemoveSensor(sensorNumber);
        }
    }
}
