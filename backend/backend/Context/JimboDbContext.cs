using Microsoft.EntityFrameworkCore;

namespace backend.Context
{
    public class JimboDbContext : DbContext
    {
        public JimboDbContext(DbContextOptions<JimboDbContext> options) : base(options)
        {
        }
    }
}