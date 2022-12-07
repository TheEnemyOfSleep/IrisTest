using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SensorConflictValidators
{
    public interface ISensorConflictValidator
    {
        Task<Sensor> GetConflictingSensor(Sensor sensor);

        Task<Sensor> GetConflictingSensor(string sensorNumber);
    }
}
