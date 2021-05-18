using System.Collections.Generic;

namespace UOWRepository.Model
{
    public class Departamento
    {
        public int Id { get; set; }
        public string NomeDepartamento { get; set; }
        public List<Funcionario> Funcioanarios { get; set; }
    }
}
