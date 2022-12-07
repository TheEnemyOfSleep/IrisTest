using IrisWpfApp1.Exceptions;
using IrisWpfApp1.Services.SensorConflictValidators;
using IrisWpfApp1.Services.SensorProviders;
using IrisWpfApp1.Services.SignalConflictValidator;
using IrisWpfApp1.Services.SignalCreator;
using IrisWpfApp1.Services.SignalEditor;
using IrisWpfApp1.Services.SignalProvider;
using IrisWpfApp1.Services.SignalRemover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Models
{
    public class Signals
    {
        private readonly ISignalProvider _signalProvider;
        private readonly ISignalCreator _signalCreator;
        private readonly ISignalEditor _signalEditor;
        private readonly ISignalRemover _signalRemover;
        private readonly ISignalConflictValidator _signalConflictValidator;

        private readonly ISensorProvider _sensorProvider;
        private readonly ISensorConflictValidator _sensorConflictValidator;

        public Signals(ISignalProvider signalProvider,
                       ISignalCreator signalCreator,
                       ISignalEditor signalEditor,
                       ISignalRemover signalRemover,
                       ISignalConflictValidator signalConflictValidator,
                       ISensorProvider sensorProvider,
                       ISensorConflictValidator sensorConflictValidator)
        {
            _signalProvider = signalProvider;
            _signalCreator = signalCreator;
            _signalEditor = signalEditor;
            _signalRemover = signalRemover;
            _signalConflictValidator = signalConflictValidator;

            _sensorProvider = sensorProvider;
            _sensorConflictValidator = sensorConflictValidator;
        }

        public event Action<Signal> SignalAdded;
        public event Action<Signal> SignalEdited;
        public event Action<Signal> SignalDeleted;

        public async Task<IEnumerable<Signal>> GetAllSignals()
        {
            return await _signalProvider.GetAllSignals();
        }

        public async Task<Signal> AddSignal(Signal signal)
        {
            Sensor conflictSensor = await _sensorConflictValidator.GetConflictingSensor(signal.SensorNumber);
            if (conflictSensor == null)
            {
                throw new SensorConflictException(conflictSensor, null);
            }

            Signal addedSignal = await _signalCreator.CreateSignal(signal);

            OnSignalAdded(signal);
            return addedSignal;
        }

        public void OnSignalAdded(Signal signal)
        {
            SignalAdded?.Invoke(signal);
        }

        public async Task EditSignal(Signal signal)
        {
            Signal conflictSignal = await _signalConflictValidator.GetConflictingSignal(signal);
            if (conflictSignal == null)
            {
                throw new SignalConflictException(conflictSignal, signal);
            }

            await _signalEditor.EditSignal(signal);

            OnSignalEdited(signal);
        }

        public void OnSignalEdited(Signal signal)
        {
            SignalEdited?.Invoke(signal);
        }

        public async Task DeleteSignal(Signal signal)
        {
            Signal conflictSignal = await _signalConflictValidator.GetConflictingSignal(signal);
            if (conflictSignal == null)
            {
                throw new SignalConflictException(conflictSignal, signal);
            }

            await _signalRemover.RemoveSignal(signal);

            OnSignalDeleted(signal);
        }

        public async Task DeleteSignal(int id)
        {
            Signal conflictSignal = await _signalConflictValidator.GetConflictingSignal(id);
            if (conflictSignal == null)
            {
                throw new SignalConflictException(conflictSignal, null);
            }

            await _signalRemover.RemoveSignal(id);

            OnSignalDeleted(conflictSignal);
        }

        public void OnSignalDeleted(Signal signal)
        {
            SignalDeleted?.Invoke(signal);
        }
    }
}
