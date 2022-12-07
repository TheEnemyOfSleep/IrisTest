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
    class LoadSignalsCommand: AsyncCommandBase
    {
        private readonly SignalsStore _signalsStore;
        private readonly SignalsListingViewModel _viewModel;

        public override async Task ExecuteAsync(object parameter)
        {
            await _signalsStore.Load();

            _viewModel.UpdateSignals(_signalsStore.Signals);
        }

        public LoadSignalsCommand(SignalsStore signalsStore, SignalsListingViewModel viewModel)
        {
            _signalsStore = signalsStore;
            _viewModel = viewModel;
        }
    }
}
