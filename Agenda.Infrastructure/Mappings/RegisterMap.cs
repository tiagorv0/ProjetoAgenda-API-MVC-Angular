using Agenda.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Mappings
{
    public class RegisterMap<T> : IEntityTypeConfiguration<T> where T : Register
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
        }
    }
}
