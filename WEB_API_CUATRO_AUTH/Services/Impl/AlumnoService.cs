using WEB_API_CUATRO_AUTH.Models;
using WEB_API_CUATRO_AUTH.Services.Interfaces;
using WEB_API_CUATRO_AUTH.Repositories.Interfaces;
namespace WEB_API_CUATRO_AUTH.Services.Impl
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IAlumnoRepository _repository;
        public AlumnoService(IAlumnoRepository repository)
        {
            _repository = repository;
        }
        public async Task AddAlumnoAsync(AlumnoModel alumno)
        {
            await _repository.AddAlumnoAsync(alumno);
        }

        public async Task DeleteAlumnoAsync(int id)
        {
            await _repository.DeleteAlumnoAsync(id);
        }

        public async Task<List<AlumnoModel>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<AlumnoModel?> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task UpdateAlumnoAsync(AlumnoModel alumno)
        {
            await _repository.UpdateAlumnoAsync(alumno);
        }
    }
}
