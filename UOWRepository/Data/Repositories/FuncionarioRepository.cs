using Dapper;
using DapperAndSwagger.Data.Repositories.Base;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UOWRepository.Model;

namespace UOWRepository.Data.Repositories
{
    public class FuncionarioRepository : DapperAndSwagger.Data.Repositories.Base.Base, IFuncionarioRepository
    {
        public FuncionarioRepository(IConfiguration configuration) : base(configuration) { }

        public void Add(Funcionario entity)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = "INSERT INTO Funcionario (Nome,DepartamentoId) VALUES(@Nome,@DepartamentoId)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, entity);
            }
        }

        public void Delete(Funcionario entity)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = "DELETE FROM Funcionario"
                            + " WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = entity.Id });
            }
        }

        public void Atualizar(Funcionario entity)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = "UPDATE Funcionario SET Nome = @Nome , DepartamentoId = @DepartamentoId WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Query(sQuery, entity);
            }
        }

        public Funcionario BuscarPorId(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionString);
            string sQuery = "SELECT * FROM funcionario INNER JOIN Departamento ON FUNCIONARIO.departamentoID = Departamento.iD"
                        + " WHERE funcionario.Id = @Id";
            dbConnection.Open();
            return dbConnection.Query<Funcionario, Departamento, Funcionario>(sQuery, map: (funcionario, departamento) =>
            {
                funcionario.Departamento = departamento;
                return funcionario;
            }, param: new { Id = id }).FirstOrDefault();
        }

        public List<Funcionario> FindByDepartamentoId(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionString);
            string sQuery = "SELECT * FROM funcionario INNER JOIN Departamento ON FUNCIONARIO.departamentoID = Departamento.iD"
                        + " WHERE Departamento.iD = @id";

            dbConnection.Open();
            return dbConnection.Query<Funcionario, Departamento, Funcionario>(sQuery, map: (funcionario, departamento) =>
            {
                funcionario.Departamento = departamento;
                funcionario.DepartamentoId = departamento.Id;

                return funcionario;
            }, param: new { Id = id }, splitOn: "Id,id").ToList();
        }

        public IEnumerable<Funcionario> BuscarTodos()
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionString);
            dbConnection.Open();
            return dbConnection.Query<Funcionario>("SELECT * FROM Funcionario");
        }
    }
}
