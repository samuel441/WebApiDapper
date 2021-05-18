using Microsoft.Extensions.Configuration;

namespace DapperAndSwagger.Data.Repositories.Base
{
    public class Base
    {
        protected string ConnectionString { get; private set; }

        public Base(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
