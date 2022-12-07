using IrisWpfApp1.DbContexts;
using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SignalProvider
{
    public class DatabaseSignalProvider: ISignalProvider
    {
        private readonly MainDbContextFactory _dbContextFactory;

        public DatabaseSignalProvider(MainDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Signal>> GetAllSignals()
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<SignalDTO> signalDTOs = await context.Signals.ToListAsync();

                return signalDTOs.Select(v => new Signal(v.Id, v.SensorNumber, v.Date, v.Status));
            }
        }
    }
}
