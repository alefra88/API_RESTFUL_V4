using System.Data.SqlClient;
using WEB_API_CUATRO_AUTH.Config.Impl;
using WEB_API_CUATRO_AUTH.Config.Interfaces;
namespace WEB_API_CUATRO_AUTH.Config.Impl
{
    public class Connection : IConnection
    {
        private readonly string _host;
        public Connection(IConfiguration configuration)=> _host = configuration
            .GetConnectionString("SqlDbConnection")
            ?? throw new ArgumentNullException(nameof(configuration));
        public async Task<SqlConnection> DbConnectionAsync()
        {
            var con = new SqlConnection(_host);
            await con.OpenAsync();
            return con;
        }
    }
}
