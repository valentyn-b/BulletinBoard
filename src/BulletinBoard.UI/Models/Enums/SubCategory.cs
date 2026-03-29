using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.UI.Models.Enums
{
    public enum SubCategory
    {
        // Household Appliances (100+)
        [Display(Name = "Холодильники")]
        Refrigerators = 101,

        [Display(Name = "Пральні машини")]
        WashingMachines = 102,

        [Display(Name = "Бойлери")]
        WaterHeaters = 103,

        [Display(Name = "Печі")]
        Ovens = 104,

        [Display(Name = "Витяжки")]
        RangeHoods = 105,

        [Display(Name = "Мікрохвильові печі")]
        Microwaves = 106,

        // Computer Equipment (200+)
        [Display(Name = "ПК")]
        PCs = 201,

        [Display(Name = "Ноутбуки")]
        Laptops = 202,

        [Display(Name = "Монітори")]
        Monitors = 203,

        [Display(Name = "Принтери")]
        Printers = 204,

        [Display(Name = "Сканери")]
        Scanners = 205,

        // Smartphones (300+)
        [Display(Name = "Android смартфони")]
        AndroidSmartphones = 301,

        [Display(Name = "iOS/Apple смартфони")]
        AppleSmartphones = 302,

        // Other (400+)
        [Display(Name = "Одяг")]
        Clothing = 401,

        [Display(Name = "Взуття")]
        Footwear = 402,

        [Display(Name = "Аксесуари")]
        Accessories = 403,

        [Display(Name = "Спортивне обладнання")]
        SportsEquipment = 404,

        [Display(Name = "Іграшки")]
        Toys = 405
    }
}