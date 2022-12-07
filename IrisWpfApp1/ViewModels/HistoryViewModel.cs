using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.ViewModels
{
    public class HistoryViewModel: ViewModelBase
    {
        private readonly History _history;

        public ActionType Action => _history.Action;
        public string SensorNumber => _history.SensorNumber;
        public string Username => _history.Username;

        public HistoryViewModel(History history)
        {
            _history = history;
        }
    }
}
