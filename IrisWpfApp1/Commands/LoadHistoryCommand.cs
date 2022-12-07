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
    public class LoadHistoryCommand: AsyncCommandBase
    {
        private readonly HistoryStore _historyStore;
        private readonly HistoryListingViewModel _viewModel;

        public override async Task ExecuteAsync(object parameter)
        {
            await _historyStore.Load();

            _viewModel.UpdateStories(_historyStore.Stories);
        }

        public LoadHistoryCommand(HistoryStore historyStore, HistoryListingViewModel viewModel)
        {
            _historyStore = historyStore;
            _viewModel = viewModel;
        }
    }
}
