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
    public class DepartamentoRepository : DapperAndSwagger.Data.Repositories.Base.Base, IDepartamentoRepository
    {
        public DepartamentoRepository(IConfiguration configuration) : base(configuration) { }

        public void Add(Departamento entity)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = "INSERT INTO Departamento (NomeDepartamento)"
                                + " VALUES(@NomeDepartamento)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, entity);
            }
        }

        public void Delete(Departamento entity)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = "DELETE FROM Departamento"
                            + " WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = entity.Id });
            }
        }

        public void Atualizar(Departamento entity)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = "UPDATE Departamento SET NomeDepartamento = @NomeDepartamento WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Query(sQuery, entity);
            }
        }

        public Departamento BuscarPorId(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionString);
            string sQuery = "SELECT * FROM Departamento INNER JOIN funcionario ON Departamento.iD = FUNCIONARIO.departamentoID"
                        + " WHERE Departamento.Id = @Id";

            List<Departamento> datas = new List<Departamento>();

            dbConnection.Open();
            var t = dbConnection.Query<Departamento, Funcionario, Departamento>(sQuery, map: (departamento, funcionario) =>
                {
                    if (departamento.Funcioanarios == null)
                        departamento.Funcioanarios = new List<Funcionario>();

                    if (departamento?.Id == funcionario?.DepartamentoId)
                        departamento.Funcioanarios.Add(funcionario);

                    var r = datas.FirstOrDefault(x => x.Id == departamento.Id);

                    if (r != null)
                    {
                        r.Funcioanarios.Add(funcionario);
                    }
                    else
                    {
                        datas.Add(departamento);
                    }

                    return departamento;
                }, param: new { Id = id },splitOn:"Id").Distinct().ToList();

            return datas.FirstOrDefault();
        }

        public IEnumerable<Departamento> BuscarTodos()
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionString);
            dbConnection.Open();
            return dbConnection.Query<Departamento>("SELECT * FROM Departamento");
        }
    }
}
