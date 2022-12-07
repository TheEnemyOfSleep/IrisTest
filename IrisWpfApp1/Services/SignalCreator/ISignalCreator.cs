using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SignalCreator
{
    public interface ISignalCreator
    {
        Task<Signal> CreateSignal(Signal sensor);
    }
}
