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
        public static List<Food> GetAllFoods()
        {
            var foods = new List<Food>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Foods ORDER BY name"; 
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            foods.Add(new Food()
                            {
                                FoodId = (int)reader[0],
                                Name = (string)reader[1],
                                Protein = (decimal)reader[2],
                                Carbs = (decimal)reader[3],
                                Fats = (decimal)reader[4]
                            });
                        }
                    }
                }
                connection.Close(); 
            }
            return foods;
        }

        /// <summary>
        /// Insert a new Food into the database.
        /// </summary>
        /// <param name="food"></param>
        public static void CreateNewFood(Food food)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = "INSERT INTO Foods (name, protein, carbs, fats) VALUES (@name, @protein, @carbs, @fats);";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", food.Name);
                    command.Parameters.AddWithValue("@protein", food.Protein);
                    command.Parameters.AddWithValue("@carbs", food.Carbs);
                    command.Parameters.AddWithValue("@fats", food.Fats);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

    }
}
