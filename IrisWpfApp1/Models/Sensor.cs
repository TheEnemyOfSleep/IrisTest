using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Models
{
    public class Sensor
    {
        public string SensorNumber { get; set; }
        public string Location { get; set; }
        public int Charge { get; set; }
        public int SensorStateId { get; set; }

        public Sensor(string sensorNumber, string location, int charge, int sensorStateId)
        {
            SensorNumber = sensorNumber;
            Location = location;
            Charge = charge;
            SensorStateId = sensorStateId;
        }
    }
}
