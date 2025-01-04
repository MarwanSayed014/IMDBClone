using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class MovieGenresRepo : Repo<MovieGenres>, IMovieGenresRepo
    {
        public MovieGenresRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddMovieToGenresAsync(Guid movieId, IEnumerable<Guid> genresIds)
        {
            foreach (var id in genresIds)
                await CreateAsync(new MovieGenres
                {
                    MovieId = movieId,
                    GenreId = id
                });
        }

        public async Task RemoveMovieFromAllGenresAsync(Guid movieId)
        {
            var movirGenres = (await FindAsync(x=> x.MovieId == movieId)).ToList();
            foreach (var item in movirGenres)
            {
                await DeleteAsync(item);
            }
        }
    }
}
