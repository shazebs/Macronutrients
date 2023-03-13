using Macronutrients.Models;
using System.Data.SqlClient;

namespace Macronutrients.Data
{
    public class FoodDataService
    {
        
        private static readonly string connectionString = @"Server=tcp:macronutrientsdbserver0.database.windows.net,1433;Initial Catalog=Macronutrients_db;Persist Security Info=False;User ID=nlpquan;Password=sukhoi0934004031!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        /// <summary>
        /// Insert a new Food into the database.
        /// </summary>
        /// <param name="food"></param>
        public static bool CreateNewFood(Food food)
        {
            food.Protein = decimal.Parse(food.Protein.ToString("F2"));
            food.Carbs = decimal.Parse(food.Carbs.ToString("F2"));
            food.Fats = decimal.Parse(food.Fats.ToString("F2"));
            if (DoesFoodExist(food))
            {
                UpdateFood(food);
                return true;
            }
            var match = GetFoodMatch(food);
            if (match != null && match.Name == food.Name && match.Protein == food.Protein && match.Carbs == food.Carbs && match.Fats == food.Fats)
            {
            }
            else
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
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Delete a food item by it's id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteFoodById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = "DELETE FROM Foods WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0) return true;
                    else return false;
                }
            }
        }

        /// <summary>
        /// Check if Food exists in database by its FoodId. 
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        public static bool DoesFoodExist(Food food)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = "SELECT COUNT(*) FROM Foods WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", food.FoodId);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    if (count > 0) return true;
                    else return false;
                }
            }
        }

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
            }
            return foods;
        }

        /// <summary>
        /// Get a Food record from database by its FoodId. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Food GetFoodbyId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM Foods WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return new Food()
                                {
                                    FoodId = (int)reader[0],
                                    Name = (string)reader[1],
                                    Protein = (decimal)reader[2],
                                    Carbs = (decimal)reader[3],
                                    Fats = (decimal)reader[4]
                                };
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// See if a Food match already exists in the database.
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        public static Food GetFoodMatch(Food food)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM Foods WHERE name = @name AND protein = @protein AND carbs = @carbs AND fats = @fats";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", food.Name);
                    command.Parameters.AddWithValue("@protein", food.Protein);
                    command.Parameters.AddWithValue("@carbs", food.Carbs);
                    command.Parameters.AddWithValue("@fats", food.Fats);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var match = new Food()
                                {
                                    FoodId = (int)reader[0],
                                    Name = (string)reader[1],
                                    Protein = (decimal)reader[2],
                                    Carbs = (decimal)reader[3],
                                    Fats = (decimal)reader[4]
                                };
                                return match;
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Update Food record in database.
        /// </summary>
        /// <param name="food"></param>
        public static void UpdateFood(Food food)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = "UPDATE Foods SET name = @name, protein = @protein, carbs = @carbs, fats = @fats WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", food.FoodId);
                    command.Parameters.AddWithValue("@name", food.Name);
                    command.Parameters.AddWithValue("@protein", food.Protein);
                    command.Parameters.AddWithValue("@carbs", food.Carbs);
                    command.Parameters.AddWithValue("@fats", food.Fats);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0) Console.WriteLine("Successful update."); 
                    else Console.WriteLine("Update failure.");  
                }
            }
        }
    }
}
