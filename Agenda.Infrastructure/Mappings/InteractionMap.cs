using Agenda.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Mappings
{
    public class InteractionMap : RegisterMap<Interaction>
    {
        public override void Configure(EntityTypeBuilder<Interaction> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Message).IsRequired();

            builder.HasOne(x => x.InteractionType)
                   .WithMany()
                   .HasForeignKey(x => x.InteractionTypeId)
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .IsRequired()
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
