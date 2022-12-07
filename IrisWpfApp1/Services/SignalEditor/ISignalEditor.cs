using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SignalEditor
{
    public interface ISignalEditor
    {
        Task EditSignal(Signal sensor);
    }
}
