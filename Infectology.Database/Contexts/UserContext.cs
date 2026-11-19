using Infectology.Database.Configurations.Users;
using Infectology.Library.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Infectology.Database.Contexts;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserSession> Sessions { get; set; }


    public UserContext(DbContextOptions<UserContext> dbContextOptions) : base(dbContextOptions)
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

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
    }
}
