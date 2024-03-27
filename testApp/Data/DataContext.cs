using Microsoft.EntityFrameworkCore;

namespace testApp;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
    : base(options)
    {


    }

    public DbSet<App> Apps { get; set; }
}
