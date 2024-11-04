using System.Data.SqlClient;

namespace WEB_API_CUATRO_AUTH.Config.Interfaces
{
    public interface IConnection
    {
        Task<SqlConnection> DbConnectionAsync();
    }
}
