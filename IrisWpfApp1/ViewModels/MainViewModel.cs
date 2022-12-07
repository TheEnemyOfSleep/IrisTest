using IrisWpfApp1.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.ViewModels
{
    class MainViewModel: ViewModelBase
    {
        private readonly NavigationStore _navigationSensorStore;
        private readonly NavigationStore _navigationSignalStore;
        private readonly ViewModelBase _historyListingViewModel;

        public ViewModelBase CurrentSensorViewModel => _navigationSensorStore.CurrentViewModel;
        public ViewModelBase CurrentSignalViewModel => _navigationSignalStore.CurrentViewModel;
        public ViewModelBase CurrentHistoryViewModel => _historyListingViewModel;

        public MainViewModel(NavigationStore navigationSensorStore, NavigationStore navigationSignalStore, HistoryListingViewModel historyListingViewModel)
        {
            _navigationSensorStore = navigationSensorStore;
            _navigationSignalStore = navigationSignalStore;
            _historyListingViewModel = historyListingViewModel;

            _navigationSensorStore.CurrentViewModelChanged += OnCurrentSensorViewModelChanged;
            _navigationSignalStore.CurrentViewModelChanged += OnCurrentSignalViewModelChanged;

        }

        private void OnCurrentSensorViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentSensorViewModel));
        }

        private void OnCurrentSignalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentSignalViewModel));
        }
    }
}
