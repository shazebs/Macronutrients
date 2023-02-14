namespace Macronutrients.Models
{
    public class Food
    {
        public int FoodId { get; set; }

        public string Name { get; set; }

        public Dictionary<string, double> Nutrients { get; set; }
    }
}
