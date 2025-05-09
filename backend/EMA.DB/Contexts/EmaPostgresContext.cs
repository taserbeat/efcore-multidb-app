using Microsoft.EntityFrameworkCore;

namespace EMA.DB.Contexts
{
    /// <summary>
    /// PostgreSQL 用のDbContext
    /// </summary>
    public class EmaPostgresContext : EmaDbContextBase
    {
        public EmaPostgresContext(DbContextOptions<EmaPostgresContext> options) : base(options)
        {

        }
    }
}
