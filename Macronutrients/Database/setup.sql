-- DROP DATABASE Macronutrients;

CREATE DATABASE Macronutrients;

GO 
CREATE TABLE Macronutrients.dbo.Foods (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    protein DECIMAL(10,2) NOT NULL,
    carbs DECIMAL(10,2) NOT NULL,
    fats DECIMAL(10,2) NOT NULL
);

-- SELECT * FROM Macronutrients.dbo.Foods; 