using Infectology.Database.Configurations.Content;
using Infectology.Library.Entities.Content;
using Infectology.Library.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Infectology.Database.Contexts;

public class ContentContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Document> Documents { get; set; }

    public ContentContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Conventions.Remove<TableNameFromDbSetConvention>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<User>();

        modelBuilder.ApplyConfiguration(new ArticleConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
    }
}
