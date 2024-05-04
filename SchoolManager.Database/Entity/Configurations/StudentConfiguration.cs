using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolManager.Database.Entity.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<StudentRecord>
    {
        public void Configure(EntityTypeBuilder<StudentRecord> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(p => p.Surname)
                .IsRequired()
                .HasMaxLength(32);

            builder.HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
