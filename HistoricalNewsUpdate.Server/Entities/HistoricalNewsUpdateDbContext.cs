using Microsoft.EntityFrameworkCore;

namespace HistoricalNewsUpdate.Entities
{
    public class HistoricalNewsUpdateDbContext : DbContext
    {
        public HistoricalNewsUpdateDbContext(DbContextOptions<HistoricalNewsUpdateDbContext> options) : base(options)
        {
        }

    }
}
