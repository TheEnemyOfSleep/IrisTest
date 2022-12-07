using IrisWpfApp1.Models;
using IrisWpfApp1.Stores;
using IrisWpfApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Commands
{
    public class DeleteSignalCommand: AsyncCommandBase
    {
        private SignalsStore _signalsStore;
        private SignalsListingViewModel _signalsListingViewModel;

        public DeleteSignalCommand(SignalsStore signalsStore, SignalsListingViewModel signalsListingViewModel)
        {
            _signalsStore = signalsStore;
            _signalsListingViewModel = signalsListingViewModel;
            _signalsListingViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _signalsListingViewModel.SelectedSignal != null &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _signalsStore.DeleteSignal(_signalsListingViewModel.SelectedSignal.Id);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SignalsListingViewModel.SelectedSignal))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
