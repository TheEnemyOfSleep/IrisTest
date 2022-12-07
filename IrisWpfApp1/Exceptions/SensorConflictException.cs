using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Exceptions
{
    class SensorConflictException : Exception
    {
        public Sensor ExistingSensor { get; }
        public Sensor IncomingSensor { get; }

        public SensorConflictException(Sensor existingSensor = null, Sensor incomingSensor = null)
        {
            ExistingSensor = existingSensor;
            IncomingSensor = incomingSensor;
        }

        public SensorConflictException(string message, Sensor existingSensor = null, Sensor incomingSensor = null) : base(message)
        {
            ExistingSensor = existingSensor;
            IncomingSensor = incomingSensor;
        }

        public SensorConflictException(string message, Exception innerException, Sensor existingSensor = null, Sensor incomingSensor = null) : base(message, innerException)
        {
            ExistingSensor = existingSensor;
            IncomingSensor = incomingSensor;
        }
    }
}