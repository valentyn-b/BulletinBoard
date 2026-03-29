using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.UI.Models.Enums
{
    public enum Category
    {
        [Display(Name = "Побутова техніка")]
        HouseholdAppliances = 1,

        [Display(Name = "Комп'ютерна техніка")]
        ComputerEquipment = 2,

        [Display(Name = "Смартфони")]
        Smartphones = 3,

        [Display(Name = "Інше")]
        Other = 4
    }
}
