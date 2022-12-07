using IrisWpfApp1.DbContexts;
using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SignalEditor
{
    public class DatabaseSignalEditor: ISignalEditor
    {
        private readonly MainDbContextFactory _dbContextFactory;

        public DatabaseSignalEditor(MainDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task EditSignal(Signal signal)
        {
            using (MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                SignalDTO signalDTO = ToSignalDTO(signal);

                var signalDb = await context.Signals.FirstOrDefaultAsync(v => v.Id == signalDTO.Id);
                signalDb.Date = signalDTO.Date;
                signalDb.SensorNumber = signalDTO.SensorNumber;
                signalDb.Status = signalDTO.Status;
                context.Entry<SignalDTO>(signalDb).State = EntityState.Modified;

                await context.SaveChangesAsync();
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
