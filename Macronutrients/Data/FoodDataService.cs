using Macronutrients.Models;
using System.Data.SqlClient;

namespace Macronutrients.Data
{
    public class FoodDataService
    {
        private static readonly string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Macronutrients;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// Get all Nutrient records from database.
        /// </summary>
        /// <returns></returns>
        public static List<Nutrient> getNutrients()
        {
            var nutrients = new List<Nutrient>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Nutrients ORDER BY Name"; 
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nutrients.Add(new Nutrient()
                            {
                                NutrientId = (int)reader[0],
                                Name = (string)reader[1]
                            });
                        }
                    }
                }
                connection.Close(); 
            }
            return nutrients;
        }
    }
}
