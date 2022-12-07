using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SensorProviders
{
    public interface ISensorProvider
    {
        Task<IEnumerable<Sensor>> GetAllSensors();

        Task<Sensor> GetSensorById(Sensor sensor);

        Task<Sensor> GetSensorBySensorNumber(string sensor);
    }
}
