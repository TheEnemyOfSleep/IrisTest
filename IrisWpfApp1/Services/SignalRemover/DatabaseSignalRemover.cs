using IrisWpfApp1.DbContexts;
using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SignalRemover
{
    public class DatabaseSignalRemover: ISignalRemover
    {
        private readonly MainDbContextFactory _dbContextFactory;

        public DatabaseSignalRemover(MainDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task RemoveSignal(Signal signal)
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                SignalDTO signalDTO = ToSensorDTO(signal);

                context.Signals.Remove(signalDTO);
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveSignal(int id)
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                SignalDTO signalDTO = await context.Signals.FirstOrDefaultAsync(v => v.Id == id);

                context.Signals.Remove(signalDTO);
                await context.SaveChangesAsync();
            }
        }


        private SignalDTO ToSensorDTO(Signal signal)
        {
            return new SignalDTO()
            {
                Id = signal.Id,
                SensorNumber = signal.SensorNumber,
                Date = signal.Date,
                Status = signal.SignalType
            };
        }
    }
}
