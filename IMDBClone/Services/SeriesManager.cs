using IMDBClone.Dtos;
using IMDBClone.EventHandlers;
using IMDBClone.Helpers;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using IMDBClone.Services.Interfaces;

namespace IMDBClone.Services
{
    public class SeriesManager : ISeriesManager
    {
        public ISeriesRepo _SeriesRepo { get; }
        public IGenreRepo _genreRepo { get; }
        public ISeriesGenresRepo _SeriesGenresRepo { get; }
        public SeriesEventHandler _seriesEventHandler { get; }

        public SeriesManager(ISeriesRepo SeriesRepo, IGenreRepo genreRepo
            , ISeriesGenresRepo SeriesGenresRepo, SeriesEventHandler seriesEventHandler)
        {
            _SeriesRepo = SeriesRepo;
            _genreRepo = genreRepo;
            _SeriesGenresRepo = SeriesGenresRepo;
            _seriesEventHandler = seriesEventHandler;
        }


        public async Task<bool> CreateAsync(CreateSeriesDto model, Guid adminId)
        {
            //GenreIds valid?
            model.GenreIds = model.GenreIds.Distinct().ToList();
            if (await _genreRepo.IsExistsAsync(model.GenreIds) == false) return false;

            //Upload ProfileImage
            var dir = Directory.GetCurrentDirectory() + "/wwwroot/Images/Seriess/";
            var ex = ServerFile.GetExtension(model.File.FileName);
            var imageName = Guid.NewGuid() + ex;
            var imagePath = dir + imageName;


            var Series = new Series
            {
                Name = model.Name,
                Description = model.Description,
                ReleaseYear = model.ReleaseYear,
                AdminId = adminId,
                CoverImgPath = $"/Images/Seriess/{imageName}",
                DirectorId = model.DirectorId,
                ProducerId = model.ProducerId,
                TotalRating = 0
            };

            _SeriesRepo.SeriesCreated += _seriesEventHandler.CreateFirstSeason;

            await _SeriesRepo.CreateAsync(Series);
            await _SeriesRepo.SaveAsync();

            await _SeriesGenresRepo.AddSeriesToGenresAsync(Series.Id, model.GenreIds);
            await _SeriesGenresRepo.SaveAsync();

            ServerFile.Upload(model.File, imagePath);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var Series = await _SeriesRepo.GetByIdAsync(Id);
            if (Series != null)
            {
                ServerFile.Delete(Directory.GetCurrentDirectory() + "/wwwroot" + Series.CoverImgPath);
                await _SeriesGenresRepo.RemoveSeriesFromAllGenresAsync(Series.Id);
                await _SeriesGenresRepo.SaveAsync();
                await _SeriesRepo.DeleteAsync(Series);
                await _SeriesRepo.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Series>> GetAllAsync()
        {
            return (await _SeriesRepo.FindAllAsync()).ToList();
        }

        public async Task<Series> GetByIdAsync(Guid Id)
        {
            return await _SeriesRepo.GetByIdAsync(Id);
        }

        public async Task<bool> UpdateAsync(EditSeriesDto model, Guid adminId)
        {
            Series Series = await GetByIdAsync(model.Id);
            if (Series == null) return false;

            //GenreIds valid?
            model.GenreIds = model.GenreIds.Distinct().ToList();
            if (await _genreRepo.IsExistsAsync(model.GenreIds) == false) return false;

            var oldCoverImgPath = Series.CoverImgPath;
            Series.Name = model.Name;
            Series.Description = model.Description;
            Series.ReleaseYear = model.ReleaseYear;
            Series.AdminId = adminId;
            Series.CoverImgPath = oldCoverImgPath;
            Series.ProducerId = model.ProducerId;
            Series.DirectorId = model.DirectorId;
            
            if (model.File != null)
            {
                var dir = Directory.GetCurrentDirectory() + "/wwwroot/Images/Seriess/";

                //Upload new image
                var ex = ServerFile.GetExtension(model.File.FileName);
                var imageName = Guid.NewGuid() + ex;
                var imagePath = dir + imageName;
                var isUploaded = ServerFile.Upload(model.File, imagePath);

                if (isUploaded)
                {
                    //Delete old image
                    if (!string.IsNullOrEmpty(oldCoverImgPath))
                    {
                        var oldImageName = oldCoverImgPath.Split('/').Last();
                        ServerFile.Delete(dir + oldImageName);
                    }

                    Series.CoverImgPath = $"/Images/Seriess/{imageName}";
                }
            }

            await _SeriesRepo.UpdateAsync(Series);
            await _SeriesRepo.SaveAsync();

            await _SeriesGenresRepo.RemoveSeriesFromAllGenresAsync(Series.Id);
            await _SeriesGenresRepo.SaveAsync();

            await _SeriesGenresRepo.AddSeriesToGenresAsync(Series.Id, model.GenreIds);
            await _SeriesGenresRepo.SaveAsync();

            return true;
        }

        public async Task<bool> IsSeriesNameUnique(string name, Guid SeriesId)
        {
            var result = (await _SeriesRepo.FindAsync(x => x.Name == name && x.Id != SeriesId))
                .ToList();
            return result.Count() == 0 ? true : false;
        }
    }
}
