using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IrisWpfApp1.DTOs
{
    public class HistoryDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SensorNumber { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public string Username { get; set; }

    }
}
