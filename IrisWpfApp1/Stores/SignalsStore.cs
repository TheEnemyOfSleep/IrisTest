using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Stores
{
    public class SignalsStore
    {
        private readonly List<Signal> _signals;
        private readonly Signals _signalsModel;
        private readonly Lazy<Task> _initLazy;

        public IEnumerable<Signal> Signals => _signals;

        public SignalsStore(Signals signals)
        {
            _signalsModel = signals;

            _initLazy = new Lazy<Task>(() => Initialize());
            _signals = new List<Signal>();
        }

        public event Action<Signal> SignalAdded;
        public event Action<Signal> SignalEdited;
        public event Action<Signal> SignalDeleted;

        public async Task Load()
        {
            await _initLazy.Value;
        }

        private async Task Initialize()
        {
            IEnumerable<Signal> signals = await _signalsModel.GetAllSignals();

            _signals.Clear();
            _signals.AddRange(signals);
        }

        public async Task AddSignal(Signal signal)
        {
            Signal addedSignal = await _signalsModel.AddSignal(signal);
            _signals.Add(addedSignal);

            OnSignalAdded(addedSignal);
        }

        public void OnSignalAdded(Signal signal)
        {
            SignalAdded?.Invoke(signal);
        }

        public async Task EditSignal(Signal signal)
        {
            await _signalsModel.EditSignal(signal);
            int i = _signals.FindIndex(v => v.SensorNumber == signal.SensorNumber);

            _signals[i] = signal;

            OnSignalEdited(signal);
        }

        public void OnSignalEdited(Signal signal)
        {
            SignalEdited?.Invoke(signal);
        }

        public async Task DeleteSignal(Signal signal)
        {
            await _signalsModel.DeleteSignal(signal);
            _signals.Remove(signal);

            OnSignalDeleted(signal);
        }

        public async Task DeleteSignal(int signalId)
        {
            await _signalsModel.DeleteSignal(signalId);
            Signal removalItem = _signals.FirstOrDefault(v => v.Id == signalId);
            _signals.Remove(removalItem);

            OnSignalDeleted(removalItem);
        }

        public void OnSignalDeleted(Signal signal)
        {
            SignalDeleted?.Invoke(signal);
        }
    }
}
