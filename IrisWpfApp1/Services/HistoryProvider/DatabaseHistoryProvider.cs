using IrisWpfApp1.DbContexts;
using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.HistoryProvider
{
    public class DatabaseHistoryProvider: IHistoryProvider
    {
        private readonly MainDbContextFactory _dbContextFactory;

        public DatabaseHistoryProvider(MainDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<History>> GetAllStories()
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<HistoryDTO> sensorsDTOs = await context.History.ToListAsync();
                return sensorsDTOs.Select(v => new History(v.SensorNumber, (ActionType)Enum.Parse(typeof(ActionType), v.Action), v.Username));
            }
        }
    }
}
