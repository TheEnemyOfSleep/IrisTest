using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.DTOs
{
    public class SensorDTO
    {
        [Key]
        public string SensorNumber { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int Charge { get; set; }

        [Required]
        public int SensorStateId { get; set; }
    }
}
