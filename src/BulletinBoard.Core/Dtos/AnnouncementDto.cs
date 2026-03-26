using BulletinBoard.Core.Enums;

namespace BulletinBoard.Core.Dtos
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public string? AuthorId { get; set; }
    }
}
