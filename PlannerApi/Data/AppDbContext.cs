using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext 
{
    public DbSet<Tarefa> Tarefas => Set<Tarefa>();
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
    }
}
