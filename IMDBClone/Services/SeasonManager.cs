using IMDBClone.Dtos;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using IMDBClone.Services.Interfaces;

namespace IMDBClone.Services
{
    public class SeasonManager : ISeasonManager
    {
        public ISeasonRepo _repo { get; }

        public SeasonManager(ISeasonRepo seasonRepo)
        {
            _repo = seasonRepo;
        }


        public async Task<bool> CreateNewSeasonAsync(CreateSeasonDto model)
        {
            Season lastSeason = await _repo.GetLastSeasonAsync(model.SeriesId);
            if(lastSeason == null)
            {
                await _repo.CreateAsync(new Season
                {
                    Number = 1,
                    SeriesId = model.SeriesId,
                    ReleaseYear = model.ReleaseYear,
                    SeasonPrequelId = null
                });
            }
            else
            {
                if(model.ReleaseYear > lastSeason.ReleaseYear)
                {
                    await _repo.CreateAsync(new Season
                    {
                        Number = lastSeason.Number + 1,
                        SeriesId = model.SeriesId,
                        ReleaseYear = model.ReleaseYear,
                        SeasonPrequelId = lastSeason.Id
                    });
                }
                else
                    return false;
            }
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteLastSeasonAsync(Guid seriesId)
        {
            Season lastSeason = await _repo.GetLastSeasonAsync(seriesId);
            if (lastSeason == null) return false; 
            await _repo.DeleteAsync(lastSeason);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<Season>> GetAllSeasonsAsync(Guid seriesId)
        {
            return (await _repo.GetAllSeasonsAsync(seriesId)).ToList();
        }

        public async Task<Season> GetByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateLastAsync(EditSeasonDto model)
        {
            Season lastSeason = await _repo.GetLastSeasonAsync(model.seriesId);
            if (lastSeason == null) return false;
            if (lastSeason.SeasonPrequelId != null) 
            {
                Season prequalSeason = await GetByIdAsync((Guid)lastSeason.SeasonPrequelId);
                if (model.ReleaseYear <= prequalSeason?.ReleaseYear)
                    return false;

            }
            lastSeason.ReleaseYear = model.ReleaseYear;
            await _repo.UpdateAsync(lastSeason);
            await _repo.SaveAsync();
            return true;

        }
    }
}
