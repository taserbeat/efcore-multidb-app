using Microsoft.EntityFrameworkCore;

namespace EMA.DB.Contexts
{
    /// <summary>
    /// SQLServer 用のDbContext
    /// </summary>
    public class EmaSqlServerContext : EmaDbContextBase
    {
        public EmaSqlServerContext(DbContextOptions<EmaSqlServerContext> options) : base(options)
        {

        }
    }
}
