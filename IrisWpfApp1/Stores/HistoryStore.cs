using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Stores
{
    public class HistoryStore
    {
        private readonly List<History> _stories;
        private readonly Stories _storiesModel;
        private readonly Lazy<Task> _initLazy;

        public IEnumerable<History> Stories => _stories;

        public HistoryStore(Stories storiesModel)
        {
            _storiesModel = storiesModel;
            _initLazy = new Lazy<Task>(() => Initialize());

            _stories = new List<History>();
        }

        public async Task Load()
        {
            await _initLazy.Value;
        }

        private async Task Initialize()
        {
            IEnumerable<History> stories = await _storiesModel.GetAllStories();

            _stories.Clear();
            _stories.AddRange(stories);
        }

        public event Action<History> HistoryAdded;

        public async Task CreateHistory(History history)
        {
            await _storiesModel.CreateHistory(history);

            _stories.Add(history);

            OnHistoryAdded(history);
        }

        private void OnHistoryAdded(History history)
        {
            HistoryAdded?.Invoke(history);
        }
    }
}
