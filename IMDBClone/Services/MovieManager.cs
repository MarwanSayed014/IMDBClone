using IMDBClone.Dtos;
using IMDBClone.Helpers;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using IMDBClone.Services.Interfaces;

namespace IMDBClone.Services
{
    public class MovieManager : IMovieManager
    {
        public IMovieRepo _movieRepo { get; }
        public IGenreRepo _genreRepo { get; }
        public IMovieGenresRepo _movieGenresRepo { get; }

        public MovieManager(IMovieRepo movieRepo, IGenreRepo genreRepo
            ,IMovieGenresRepo movieGenresRepo)
        {
            _movieRepo = movieRepo;
            _genreRepo = genreRepo;
            _movieGenresRepo = movieGenresRepo;
        }


        public async Task<bool> CreateAsync(CreateMovieDto model, Guid adminId)
        {
            if (model.MoviePrequelId != null)
            {
                //Check if PrequelMovie is Prequel for another movie
                var result = (await _movieRepo.FindAsync(x => x.MoviePrequelId == model.MoviePrequelId))
                    .SingleOrDefault();
                if (result != null)
                    return false;
            }

            //GenreIds valid?
            model.GenreIds = model.GenreIds.Distinct().ToList();
            if (await _genreRepo.IsExistsAsync(model.GenreIds) == false) return false; 

            //Upload ProfileImage
            var dir = Directory.GetCurrentDirectory() + "/wwwroot/Images/Movies/";
            var ex = ServerFile.GetExtension(model.File.FileName);
            var imageName = Guid.NewGuid() + ex;
            var imagePath = dir + imageName;
           
            
            var movie = new Movie
            {
                Name = model.Name,
                Description = model.Description,
                ReleaseYear = model.ReleaseYear,
                AdminId = adminId,
                CoverImgPath = $"/Images/Movies/{imageName}",
                DirectorId = model.DirectorId,
                ProducerId = model.ProducerId,
                TotalRating = 0,
                MoviePrequelId = model.MoviePrequelId
            };

            await _movieRepo.CreateAsync(movie);
            await _movieRepo.SaveAsync();

            await _movieGenresRepo.AddMovieToGenresAsync(movie.Id, model.GenreIds); 
            await _movieGenresRepo.SaveAsync();

            ServerFile.Upload(model.File, imagePath);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var movie = await _movieRepo.GetByIdAsync(Id);
            if (movie != null)
            {
                ServerFile.Delete(Directory.GetCurrentDirectory() + "/wwwroot" + movie.CoverImgPath);
                await _movieGenresRepo.RemoveMovieFromAllGenresAsync(movie.Id);
                await _movieGenresRepo.SaveAsync();
                await _movieRepo.DeleteAsync(movie);
                await _movieRepo.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return (await _movieRepo.FindAllAsync()).ToList();
        }

        public async Task<Movie> GetByIdAsync(Guid Id)
        {
            return await _movieRepo.GetByIdAsync(Id);
        }

        public async Task<bool> UpdateAsync(EditMovieDto model, Guid adminId)
        {
            Movie movie = await GetByIdAsync(model.Id);
            if (movie == null) return false;
            if(model.MoviePrequelId == movie.Id) return false;

            if (model.MoviePrequelId != null)
            {
                if (movie.MoviePrequelId != model.MoviePrequelId)
                {
                    //Check if PrequelMovie is Prequel for another movie
                    var result = (await _movieRepo.FindAsync(x => x.MoviePrequelId == model.MoviePrequelId))
                        .SingleOrDefault();
                    if (result != null)
                        return false;
                }
            }

            //GenreIds valid?
            model.GenreIds = model.GenreIds.Distinct().ToList();
            if (await _genreRepo.IsExistsAsync(model.GenreIds) == false) return false;

            var oldCoverImgPath = movie.CoverImgPath;
            movie.Name = model.Name;
            movie.Description = model.Description;
            movie.ReleaseYear = model.ReleaseYear;
            movie.AdminId = adminId;
            movie.CoverImgPath = oldCoverImgPath;
            movie.ProducerId = model.ProducerId;
            movie.DirectorId = model.DirectorId;
            movie.MoviePrequelId = model.MoviePrequelId;

            if (model.File != null)
            {
                var dir = Directory.GetCurrentDirectory() + "/wwwroot/Images/Movies/";

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

                    movie.CoverImgPath = $"/Images/Movies/{imageName}";
                }
            }

            await _movieRepo.UpdateAsync(movie);
            await _movieRepo.SaveAsync();

            await _movieGenresRepo.RemoveMovieFromAllGenresAsync(movie.Id);
            await _movieGenresRepo.SaveAsync();

            await _movieGenresRepo.AddMovieToGenresAsync(movie.Id, model.GenreIds);
            await _movieGenresRepo.SaveAsync();

            return true;
        }

        public async Task<bool> IsMovieNameUnique(string name, Guid MovieId)
        {
            var result = (await _movieRepo.FindAsync(x => x.Name == name && x.Id != MovieId))
                .ToList();
            return result.Count() == 0 ? true : false;
        }
    }
}
