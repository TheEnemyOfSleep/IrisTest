using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.DbContexts
{
    public class MainDbContext: DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SensorDTO> Sensors { get; set; }
        public DbSet<SensorStateDTO> States { get; set; }
        public DbSet<SignalDTO> Signals { get; set; }
        public DbSet<HistoryDTO> History { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SensorDTO>()
                .HasIndex(v => v.SensorNumber)
                .IsUnique(true);
        }
    }
}
