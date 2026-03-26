using BulletinBoard.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Core.Dtos
{
    public class CreateAnnouncementDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Subcategory is required")]
        public SubCategory SubCategory { get; set; }

        public string? AuthorId { get; set; }
    }
}
