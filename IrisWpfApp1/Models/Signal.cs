using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Models
{
    public enum SignalType
    {
        Alarm,
        ServiseSignal
    }

    public class Signal
    {
        public int Id { get; }
        public string SensorNumber { get; }
        public DateTime Date { get; set; }
        public SignalType SignalType { get; set; }

        public Signal(int id, string sensorNumber, DateTime date, SignalType signalType)
        {
            Id = id;
            SensorNumber = sensorNumber;
            Date = date;
            SignalType = signalType;
        }
    }
}
