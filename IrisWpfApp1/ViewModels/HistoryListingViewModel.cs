using IrisWpfApp1.Commands;
using IrisWpfApp1.Models;
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
    public class HistoryListingViewModel: ViewModelBase
    {
        private readonly ObservableCollection<HistoryViewModel> _stories;
        private readonly HistoryStore _historyStore;
        public IEnumerable<HistoryViewModel> Stories => _stories;

        public ICommand LoadHistoryCommand { get; }

        public HistoryListingViewModel(HistoryStore historyStore)
        {
            _stories = new ObservableCollection<HistoryViewModel>();
            _historyStore = historyStore;

            LoadHistoryCommand = new LoadHistoryCommand(historyStore, this);

            _historyStore.HistoryAdded += OnHistoryAdded;
        }

        private void OnHistoryAdded(History history)
        {
            HistoryViewModel historyViewModel = new HistoryViewModel(history);
            _stories.Add(historyViewModel);
        }

        public static HistoryListingViewModel LoadViewModel(HistoryStore historyStore)
        {
            HistoryListingViewModel viewModel = new HistoryListingViewModel(historyStore);

            viewModel.LoadHistoryCommand.Execute(null);

            return viewModel;
        }

        public void UpdateStories(IEnumerable<History> stories)
        {
            _stories.Clear();

            foreach (History history in stories)
            {
                HistoryViewModel signalViewModel = new HistoryViewModel(history);
                _stories.Add(signalViewModel);
            }
        }
    }
}
