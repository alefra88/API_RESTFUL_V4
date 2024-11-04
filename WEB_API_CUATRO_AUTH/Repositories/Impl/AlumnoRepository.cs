using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using WEB_API_CUATRO_AUTH.Config.Interfaces;
using WEB_API_CUATRO_AUTH.Models;
using WEB_API_CUATRO_AUTH.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace WEB_API_CUATRO_AUTH.Repositories.Impl
{
    public class AlumnoRepository : IAlumnoRepository
    {
        private readonly IConnection _connection;
        private readonly ILogger<AlumnoRepository> _logger;
        public AlumnoRepository(IConnection connection, ILogger<AlumnoRepository> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task AddAlumnoAsync(AlumnoModel alumno)
        {
            var storedProcedure = "agregarAlumno";
            try
            {
                using var con = await _connection.DbConnectionAsync();
                using var command = new SqlCommand(storedProcedure, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                AddParameters(command, alumno);
                if (con.State != ConnectionState.Open)
                {
                    await con.OpenAsync();
                }
                await command.ExecuteNonQueryAsync();
                _logger.LogInformation("Alumno agregado exitosamente");
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error al agregar alumno");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al agregar alumno");
                throw;
            }
        }

        public async Task DeleteAlumnoAsync(int id)
        {
            var storedProcedure = "eliminarAlumnos";
            try
            {
                using var con = await _connection.DbConnectionAsync();
                using var command = new SqlCommand(storedProcedure, con)
                { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("id", id);
                if (con.State != ConnectionState.Open)
                {
                    await con.OpenAsync();
                }
                await command.ExecuteNonQueryAsync();
                _logger.LogInformation("Alumno eliminado exitosamente");
            }
            catch (SqlException ex) 
            {
                _logger.LogError(ex, "Error al intentar eliminar alumno");
            }
        }

        public async Task<List<AlumnoModel>> GetAsync()
        {
            var listAlumnos = new List<AlumnoModel>();
            var storedProcedure = "consultarAlumnos2";
            using(SqlConnection con = await _connection.DbConnectionAsync())
            {
                using var command = new SqlCommand(storedProcedure, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@id", -1);
                using var reader = await command.ExecuteReaderAsync();
                while(await reader.ReadAsync())
                {
                    var alumno = MapReaderAlumnos(reader);
                    listAlumnos.Add(alumno);
                }
                return listAlumnos;
            }
        }

        public async Task<AlumnoModel?> GetAsync(int id)
        {
            var storedProcedure = "consultarAlumnos2";
            try
            {
                using var con = await _connection.DbConnectionAsync();
                using var command = new SqlCommand(storedProcedure, con)
                { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@id", id);
                if(con.State != ConnectionState.Open)
                {
                    await con.OpenAsync();
                }
                using var reader = await command.ExecuteReaderAsync();
                _logger.LogInformation("lectura de info exitosa");
                if(await reader.ReadAsync())
                {
                    return MapReaderAlumnos(reader);
                }
                return null;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error al consultar alumno");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al consultar alumno");
                throw;
            }
        }

        public async  Task UpdateAlumnoAsync(AlumnoModel alumno)
        {
            var storedProcedure = "actualizarAlumnos";
            try
            {
                using var con = await _connection.DbConnectionAsync();
                using var command = new SqlCommand(storedProcedure, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                AddParameters(command, alumno);
                if (con.State != ConnectionState.Open)
                {
                    await con.OpenAsync();
                }
                await command.ExecuteNonQueryAsync();
                _logger.LogInformation("Actualizacion de alumno exitosa");
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error al consultar alumno");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al consultar alumno");
                throw;
            }
        }

        private static void AddParameters(SqlCommand command, AlumnoModel alumnos)
        {
            command.Parameters.AddWithValue("@id", alumnos.Id);
            command.Parameters.AddWithValue("@nombre", alumnos.Nombre);
            command.Parameters.AddWithValue("@primerApellido", alumnos.PrimerApellido);
            command.Parameters.AddWithValue("@segundoApellido", alumnos.SegundoApellido);
            command.Parameters.AddWithValue("@correo", alumnos.Correo);
            command.Parameters.AddWithValue("@telefono", alumnos.Telefono);
            command.Parameters.AddWithValue("@fechaNacimiento", alumnos.FechaNacimiento);
            command.Parameters.AddWithValue("@curp", alumnos.Curp);
            command.Parameters.AddWithValue("@sueldoMensual", alumnos.SueldoMensual);
            command.Parameters.AddWithValue("@idEstadoOrigen", alumnos.IdEstadoOrigen);
            command.Parameters.AddWithValue("@idEstatus", alumnos.IdEstatus);
        }

        private static AlumnoModel MapReaderAlumnos(SqlDataReader reader)
        {
            return new AlumnoModel
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre")),
                PrimerApellido = reader.IsDBNull(reader.GetOrdinal("primerApellido")) ? null : reader.GetString(reader.GetOrdinal("primerApellido")),
                SegundoApellido = reader.IsDBNull(reader.GetOrdinal("segundoApellido")) ? null : reader.GetString(reader.GetOrdinal("segundoApellido")),
                Correo = reader.IsDBNull(reader.GetOrdinal("correo")) ? null : reader.GetString(reader.GetOrdinal("correo")),
                Telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? null : reader.GetString(reader.GetOrdinal("telefono")),
                FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("fechaNacimiento")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaNacimiento")),
                Curp = reader.IsDBNull(reader.GetOrdinal("curp")) ? null : reader.GetString(reader.GetOrdinal("curp")),
                SueldoMensual = reader.IsDBNull(reader.GetOrdinal("sueldoMensual")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("sueldoMensual")),
                IdEstadoOrigen = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                IdEstatus = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
            };
        }

    }
}
