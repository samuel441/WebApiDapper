using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DapperAndSwagger.Data.Context
{
    public class Seed
    {
        private static IDbConnection _dbConnection;

        public static void CreateDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //SqliteConnection.CreateFile(dbFilePath);
            _dbConnection = new SqlConnection(connectionString);
            _dbConnection.Open();

            _dbConnection.Execute(@"
                  Create table [Departamento] (
                        [Id] int NOT NULL PRIMARY KEY IDENTITY(1,1),
                        [NomeDepartamento] VARCHAR(128) NOT NULL,
                        [DepartamentoId] int,
                        FOREIGN KEY (DepartamentoId) REFERENCES Funcioanario(Id)
                    )");

            _dbConnection.Execute(@"
                    CREATE TABLE [Funcionario] (
                        [Id] Int NOT NULL PRIMARY KEY IDENTITY(1,1),
                        [Nome] VARCHAR(128) NOT NULL,
                        [DepartamentoId] INT NULL,
                    )");

            _dbConnection.Close();

        }
    }
}
