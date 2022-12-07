using IrisWpfApp1.DbContexts;
using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using IrisWpfApp1.Services.SignalConflictValidator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SignalConflictValidator
{
    public class DatabaseSignalConflictValidator: ISignalConflictValidator
    {
        private readonly MainDbContextFactory _dbContextFactory;

        public DatabaseSignalConflictValidator(MainDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Signal> GetConflictingSignal(Signal signal)
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                SignalDTO signalDTO = await context.Signals.Where(v => v.Id == signal.Id).FirstOrDefaultAsync();
                if (signalDTO == null)
                {
                    return null;
                }

                return ToSignals(signalDTO);
            }
        }

        public async Task<Signal> GetConflictingSignal(int id)
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                SignalDTO signalDTO = await context.Signals.Where(v => v.Id == id).FirstOrDefaultAsync();
                if (signalDTO == null)
                {
                    return null;
                }

                return ToSignals(signalDTO);
            }
        }

        public Signal ToSignals(SignalDTO signalDTO)
        {
            return new Signal(signalDTO.Id, signalDTO.SensorNumber, signalDTO.Date, signalDTO.Status);
        }
    }
}
