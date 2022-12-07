using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.DbContexts
{
    public class MainDesignTimeDbContextFactory: IDesignTimeDbContextFactory<MainDbContext>
    {

        public MainDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseNpgsql("Host=localhost;Port=5433;Database=IrisTest;Username=postgres;Password=turchenyk2011").Options;
            return new MainDbContext(options);
        }
    }
}
