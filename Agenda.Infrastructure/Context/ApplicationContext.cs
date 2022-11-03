using System.Globalization;
using Agenda.Domain.Core;
using Agenda.Domain.Entities;
using Agenda.Domain.Entities.Enums;
using Agenda.Infrastructure.Mappings;
using Agenda.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Agenda.Infrastructure.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Interaction> Interactions { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<PhoneType> PhoneTypes { get; set; }
        public virtual DbSet<InteractionType> InteractionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            modelBuilder.ApplyConfiguration(new EnumerationMap<PhoneType>());
            modelBuilder.ApplyConfiguration(new EnumerationMap<UserRole>());
            modelBuilder.ApplyConfiguration(new EnumerationMap<InteractionType>());

            modelBuilder
                .Entity<PhoneType>()
                .HasData(Enumeration.GetAll<PhoneType>());

            modelBuilder
                .Entity<UserRole>()
                .HasData(Enumeration.GetAll<UserRole>());

            modelBuilder
                .Entity<InteractionType>()
                .HasData(Enumeration.GetAll<InteractionType>());

            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            var user = new User
            {
                Id = 1,
                UserName = "admin",
                Password = "Pass@123",
                Email = "admin@api.com",
                Name = "Admin Root Application",
                CreatedAt = DateTime.ParseExact("03/06/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                UserRoleId = UserRole.Admin.Id
            };

            PasswordHasher.PasswordHash(user);

            modelBuilder
                .Entity<User>()
                .HasData(user);
        }

        public class ToDoContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
        {
            public ApplicationContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<ApplicationContext>();
                builder.UseSqlServer(@"Data Source=DESKTOP-ONMGS4I;Database=AgendaDesafio4;Integrated Security=true;pooling=true");
                return new ApplicationContext(builder.Options);
            }
        }
    }
}
