using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.DTOs
{
    public class SignalDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SensorNumber { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public SignalType Status { get; set; }
    }
}
