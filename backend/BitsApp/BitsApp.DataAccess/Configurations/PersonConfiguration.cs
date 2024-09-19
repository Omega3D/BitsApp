using BitsApp.DataAccess.Enitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BitsApp.DataAccess.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<PersonEntity>
    {
        public void Configure(EntityTypeBuilder<PersonEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.DateOfBirth)
                .IsRequired();           

            builder.Property(p => p.isMarried)
                .IsRequired();

            builder.Property(p => p.Phone)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(p => p.Salary)
                .HasPrecision(18, 2)
                .IsRequired();
        }
    }
}
