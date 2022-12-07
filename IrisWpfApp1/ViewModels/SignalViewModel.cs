using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.ViewModels
{
    public class SignalViewModel: ViewModelBase
    {
        private readonly Signal _signal;

        public int Id => _signal.Id;
        public string SensorNumber => _signal.SensorNumber;
        public string Date => _signal.Date.ToString();
        public string Status => SignalType.GetName(_signal.SignalType);

        public SignalViewModel(Signal signal)
        {
            _signal = signal;
        }
    }
}
