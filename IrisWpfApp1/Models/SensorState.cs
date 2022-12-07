using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Models
{
    public class SensorState
    {
        public int Id { get; set; }
        public string State { get; set; }

        public SensorState(int id, string state)
        {
            Id = id;
            State = state;
        }
    }
}
