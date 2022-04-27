using Microsoft.EntityFrameworkCore;

namespace BoligDataAPI.Features.Database;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options) { }
  public DbSet<Ejendom.Ejendom> Ejendomme { get; set; } = null!;
  public DbSet<Lejemaal.Lejemaal> Lejemaal { get; set; } = null!;
  public DbSet<Lejer.Lejer> Lejere { get; set; } = null!;
}