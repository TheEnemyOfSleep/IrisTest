using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Models
{
    public enum ActionType
    {
        Create,
        Edit,
        Delete
    }

    public class History
    {
        public string SensorNumber { get; }
        public ActionType Action { get; }
        public string Username { get; }

        public History (string sensorNumber, ActionType action, string username)
        {
            SensorNumber = sensorNumber;
            Action = action;
            Username = username;
        }
    }
}
