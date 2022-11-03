using Agenda.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Mappings
{
    public class UserMap : RegisterMap<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired();

            builder.HasIndex(x => x.UserName).IsUnique();

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Password).IsRequired();

            builder.HasOne(x => x.UserRole)
                .WithMany()
                .HasForeignKey(x => x.UserRoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
