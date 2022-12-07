using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.StateProvider
{
    public interface IStateProvider
    {
        Task<IEnumerable<SensorState>> GetAsyncAllStates();

        IEnumerable<SensorState> GetAllStates();
    }
}
