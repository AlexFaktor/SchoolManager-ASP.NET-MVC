using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolManager.Database.Entity.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<GroupRecord>
    {
        public void Configure(EntityTypeBuilder<GroupRecord> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder.
                HasOne(g => g.Course)
                .WithMany(c => c.Groups)
                .HasForeignKey(g => g.CourseId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.
                HasMany(g => g.Students)
                .WithOne(s => s.Group)
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
