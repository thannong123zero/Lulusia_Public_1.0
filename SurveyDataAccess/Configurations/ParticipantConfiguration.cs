using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyDataAccess.DTOs;

namespace SurveyDataAccess.Configurations
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<ParticipantDTO>
    {
        public void Configure(EntityTypeBuilder<ParticipantDTO> builder)
        {
            builder.ToTable("Table_Participants");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.FullName).HasColumnType("nvarchar(255)");
            builder.Property(s => s.PhoneNumber).HasColumnType("varchar(10)");
            builder.Property(s => s.Email).HasColumnType("varchar(255)");
            builder.Property(s => s.Note).HasColumnType("nvarchar(500)");
            builder.Property(s => s.IsActive).HasDefaultValue(true);
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedBy).HasColumnType("varchar(100)");
            builder.Property(s => s.ModifiedBy).HasColumnType("varchar(100)");
        }
    }
}
