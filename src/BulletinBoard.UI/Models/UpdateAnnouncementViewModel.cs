using BulletinBoard.UI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.UI.Models
{
    public class UpdateAnnouncementViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть назву оголошення")]
        [MaxLength(100, ErrorMessage = "Назва не може перевищувати 100 символів")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Додайте опис оголошення")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Оберіть категорію")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Оберіть підкатегорію")]
        public SubCategory SubCategory { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}