using IMDBClone.Dtos;
using IMDBClone.Helpers;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using IMDBClone.Services.Interfaces;

namespace IMDBClone.Services
{
    public class GenreManager : IGenreManager
    {
        public IGenreRepo _repo { get; }

        public GenreManager(IGenreRepo repo)
        {
            _repo = repo;
        }


        public async Task<bool> CreateAsync(CreateGenreDto model, Guid adminId)
        {
            var genre = new Genre
            {
                Name = model.Name,
                AdminId = adminId
            };
            await _repo.CreateAsync(genre);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var genre = await _repo.GetByIdAsync(Id);
            if (genre != null)
            {
                await _repo.DeleteAsync(genre);
                await _repo.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return (await _repo.FindAllAsync()).ToList();
        }

        public async Task<Genre> GetByIdAsync(Guid Id)
        {
            return await _repo.GetByIdAsync(Id);
        }

        public async Task<bool> UpdateAsync(EditGenreDto model, Guid adminId)
        {
            Genre genre = await GetByIdAsync(model.Id);
            if(genre == null) return false;
            genre.Name = model.Name;
            genre.AdminId = adminId;
            await _repo.UpdateAsync(genre);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> IsGenreNameUnique(string name, Guid GenreId)
        {
            var result = (await _repo.FindAsync(x => x.Name == name && x.Id != GenreId))
                .ToList();
            return result.Count() == 0 ? true : false;
        }
    }
}
