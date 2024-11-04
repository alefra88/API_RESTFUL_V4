using WEB_API_CUATRO_AUTH.Models;

namespace WEB_API_CUATRO_AUTH.Services.Interfaces
{
    public interface IAlumnoService
    {
        Task<List<AlumnoModel>> GetAsync();
        Task<AlumnoModel?> GetAsync(int id);
        Task AddAlumnoAsync(AlumnoModel alumno);
        Task UpdateAlumnoAsync(AlumnoModel alumno);
        Task DeleteAlumnoAsync(int id);
    }
}
