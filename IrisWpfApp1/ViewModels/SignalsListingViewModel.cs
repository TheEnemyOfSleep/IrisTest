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
    public class SignalsListingViewModel: ViewModelBase
    {
        private readonly ObservableCollection<SignalViewModel> _signals;
        public IEnumerable<SignalViewModel> Signals => _signals;
        private SignalsStore _signalsStore;
        private SignalViewModel _signal;

        public SignalViewModel SelectedSignal
        {
            get
            {
                return _signal;
            }
            set
            {

                if (_signal == value) return;
                _signal = value;
                OnPropertyChanged(nameof(SelectedSignal));
            }
        }


        public ICommand LoadSignalsCommand { get; }

        public ICommand CreateSignalCommand { get; }

        public ICommand EditSignalCommand { get; }

        public ICommand DeleteSignalCommand { get; }

        public SignalsListingViewModel(SignalsStore signalsStore,
                                       NavigationService createSignalViewNavigationService,
                                       NavigationService editSignalViewNavigationService)
        {
            _signalsStore = signalsStore;
            _signals = new ObservableCollection<SignalViewModel>();

            LoadSignalsCommand = new LoadSignalsCommand(signalsStore, this);
            CreateSignalCommand = new NavigateCommand(createSignalViewNavigationService);
            EditSignalCommand = new NavigateCommand(editSignalViewNavigationService);
            DeleteSignalCommand = new DeleteSignalCommand(signalsStore, this);

            _signalsStore.SignalAdded += OnSignalAdded;
            _signalsStore.SignalEdited += OnSignalEdited;
            _signalsStore.SignalDeleted += OnSignalDeleted;

        }

        private void OnSignalAdded(Signal signal)
        {
            SignalViewModel signalViewModel = new SignalViewModel(signal);
            _signals.Add(signalViewModel);
        }

        private void OnSignalEdited(Signal signal)
        {
            SignalViewModel signalViewModel = _signals.FirstOrDefault(v => v.Id == signal.Id);

            int i = _signals.IndexOf(signalViewModel);
            _signals[i] = new SignalViewModel(signal);
        }

        private void OnSignalDeleted(Signal signal)
        {
            SignalViewModel signalViewModel = _signals.FirstOrDefault(v => v.Id == signal.Id);
            _signals.Remove(signalViewModel);
        }

        public override void Dispose()
        {
            _signalsStore.SignalDeleted -= OnSignalDeleted;
            base.Dispose();
        }

        public static SignalsListingViewModel LoadViewModel(SignalsStore signalsStore,
                                                            NavigationService createSignalViewNavigationService,
                                                            NavigationService editSignalViewNavigationService)
        {
            SignalsListingViewModel viewModel = new SignalsListingViewModel(signalsStore, createSignalViewNavigationService, editSignalViewNavigationService);

            viewModel.LoadSignalsCommand.Execute(null);

            return viewModel;
        }

        public void UpdateSignals(IEnumerable<Signal> signals)
        {
            _signals.Clear();

            foreach (Signal signal in signals)
            {
                SignalViewModel signalViewModel = new SignalViewModel(signal);
                _signals.Add(signalViewModel);
            }
        }
    }
}
