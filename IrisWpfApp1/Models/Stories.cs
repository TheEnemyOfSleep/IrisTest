using IrisWpfApp1.Services.HistoryCreator;
using IrisWpfApp1.Services.HistoryProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Models
{
    public class Stories
    {
        IHistoryProvider _historyProvider;
        IHistoryCreator _historyCreator;

        public Stories(IHistoryProvider historyProvider, IHistoryCreator historyCreator)
        {
            _historyProvider = historyProvider;
            _historyCreator = historyCreator;
        }

        public async Task<IEnumerable<History>> GetAllStories()
        {
            return await _historyProvider.GetAllStories();
        }

        public async Task CreateHistory(History history)
        {
            await _historyCreator.CreateHistory(history);
        }
    }
}
