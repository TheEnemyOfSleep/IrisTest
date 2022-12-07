using IrisWpfApp1.DbContexts;
using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.StateProvider
{
    public class DatabaseStateProvider: IStateProvider
    {
        private readonly MainDbContextFactory _dbContextFactory;

        public DatabaseStateProvider(MainDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<SensorState>> GetAsyncAllStates()
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<SensorStateDTO> sensorsDTOs = await context.States.ToListAsync();

                return sensorsDTOs.Select(v => new SensorState(v.Id, v.State));
            }
        }

        public IEnumerable<SensorState> GetAllStates()
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<SensorStateDTO> sensorsDTOs = context.States.ToList();

                return sensorsDTOs.Select(v => new SensorState(v.Id, v.State));
            }
        }
    }
}
