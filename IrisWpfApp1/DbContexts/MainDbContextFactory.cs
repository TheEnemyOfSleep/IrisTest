using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.DbContexts
{
    public class MainDbContextFactory
    {
        private readonly string _connectionString;

        public MainDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MainDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseNpgsql(_connectionString).Options;
            return new MainDbContext(options);
        }
    }
}
