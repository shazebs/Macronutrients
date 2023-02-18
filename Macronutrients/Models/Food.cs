using System.ComponentModel.DataAnnotations;

namespace Macronutrients.Models
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Protein { get; set; }

        [Required]
        public decimal Carbs { get; set; }

        [Required]
        public decimal Fats { get; set; }
    }
}
