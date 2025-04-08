using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyDataAccess.DTOs;

namespace SurveyDataAccess.Configurations
{
    public class QuestionTypeConfiguration : IEntityTypeConfiguration<QuestionTypeDTO>
    {
        public void Configure(EntityTypeBuilder<QuestionTypeDTO> builder)
        {
            builder.ToTable("Table_QuestionTypes");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Name).HasColumnType("nvarchar(255)");
            builder.Property(s => s.Description).HasColumnType("nvarchar(500)");
            builder.HasData([
                new QuestionTypeDTO { Id = 1, Name = "Câu hỏi đóng", Description = "Câu hỏi đóng (Closed-ended question) – Chỉ có các câu trả lời sẵn." },
                new QuestionTypeDTO { Id = 2, Name = "Câu hỏi mở", Description = "Câu hỏi mở (Open-ended question) – Người dùng nhập câu trả lời." },
                new QuestionTypeDTO { Id = 3, Name = "Câu hỏi kết hợp", Description = "Câu hỏi kết hợp (Hybrid question) hoặc Câu hỏi mở rộng (Extended question) – Vừa có câu trả lời sẵn, vừa cho phép người dùng nhập câu trả lời riêng." },
                new QuestionTypeDTO { Id = 4, Name = "Câu hỏi đánh giá", Description = "Cẩu hỏi đáng giá (rating question) - Cho người dùng đánh giá mức độ trên 5 sao." },
                ]);
        }
    }
}
