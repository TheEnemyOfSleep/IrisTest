using IrisWpfApp1.DbContexts;
using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.HistoryCreator
{
    public class DatabaseHistoryCreator: IHistoryCreator
    {
        private readonly MainDbContextFactory _dbContextFactory;

        public DatabaseHistoryCreator(MainDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateHistory(History story)
        {
            using (MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                HistoryDTO historyDTO = ToHistoryDTO(story);
                context.History.Add(historyDTO);

                await context.SaveChangesAsync();
            }
        }

        private HistoryDTO ToHistoryDTO(History story)
        {
            return new HistoryDTO()
            {
                Action = ActionType.GetName(story.Action),
                SensorNumber = story.SensorNumber,
                Username = story.Username
        };
        }
    }
}
