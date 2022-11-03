using Agenda.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Mappings
{
    public class PhoneMap : RegisterMap<Phone>
    {
        public override void Configure(EntityTypeBuilder<Phone> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.FormattedPhone).IsRequired();

            builder.Property(x => x.DDD).IsRequired();

            builder.Property(x => x.Number).IsRequired();

            builder.HasOne(x => x.PhoneType)
                   .WithMany()
                   .HasForeignKey(x => x.PhoneTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
