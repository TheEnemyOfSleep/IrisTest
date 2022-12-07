using IrisWpfApp1.DbContexts;
using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SignalCreator
{
    public class DatabaseSignalCreator: ISignalCreator
    {
        private readonly MainDbContextFactory _dbContextFactory;

        public DatabaseSignalCreator(MainDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Signal> CreateSignal(Signal signal)
        {
            using (MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                SignalDTO signalDTO = ToSignalDTO(signal);
                context.Signals.Add(signalDTO);
                await context.SaveChangesAsync();


                return new Signal(signalDTO.Id, signalDTO.SensorNumber, signalDTO.Date, signalDTO.Status);
            }
        }

        private SignalDTO ToSignalDTO(Signal signal)
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
