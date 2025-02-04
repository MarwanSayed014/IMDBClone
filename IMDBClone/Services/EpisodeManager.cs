using IMDBClone.Dtos;
using IMDBClone.Helpers;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using IMDBClone.Services.Interfaces;

namespace IMDBClone.Services
{
    public class EpisodeManager : IEpisodeManager
    {
        public IEpisodeRepo _repo { get; }

        public EpisodeManager(IEpisodeRepo repo)
        {
            _repo = repo;
        }


        public async Task<bool> CreateAsync(CreateEpisodeDto model, Guid adminId)
        {
            //Upload ProfileImage
            var dir = Directory.GetCurrentDirectory() + "/wwwroot/Images/Episodes/";
            var ex = ServerFile.GetExtension(model.File.FileName);
            var imageName = Guid.NewGuid() + ex;
            var imagePath = dir + imageName;

            Episode episode = new Episode
            {
                Name = model.Name,
                Description = model.Description,
                AdminId = adminId,
                CoverImgPath = $"/Images/Episodes/{imageName}",
                SeasonId = model.SeasonId
            };

            var lastEpisode = await _repo.GetLastEpisodeAsync(model.SeasonId);
            if (lastEpisode == null) 
            {
                episode.Number = 1;
                episode.EpisodePrequelId = null;
            }
            else 
            {
                episode.Number = lastEpisode.Number + 1;
                episode.EpisodePrequelId = lastEpisode.Id;
            }

            await _repo.CreateAsync(episode);
            await _repo.SaveAsync();
            ServerFile.Upload(model.File, imagePath);

            return true;
        }

        public async Task<bool> DeleteLastEpisodeAsync(Guid seasonId)
        {
            Episode lastEpisode = await _repo.GetLastEpisodeAsync(seasonId);
            if (lastEpisode == null) return false;
            await _repo.DeleteAsync(lastEpisode);
            await _repo.SaveAsync();
            ServerFile.Delete(Directory.GetCurrentDirectory() + "/wwwroot" + lastEpisode.CoverImgPath);
            return true;
        }

        public async Task<IEnumerable<Episode>> GetAllEpisodesAsync(Guid seasonId)
        {
            return (await _repo.GetAllEpisodesAsync(seasonId)).ToList();
        }

        public async Task<Episode> GetByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(EditEpisodeDto model, Guid adminId)
        {
            Episode episode = await GetByIdAsync(model.Id);
            if (episode == null) return false;
            var oldCoverImgPath = episode.CoverImgPath;
            episode.Name = model.Name;
            episode.Description = model.Description;
            episode.AdminId = adminId;
            episode.CoverImgPath = oldCoverImgPath;

            if (model.File != null)
            {
                var dir = Directory.GetCurrentDirectory() + "/wwwroot/Images/Episodes/";

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

                    episode.CoverImgPath = $"/Images/Episodes/{imageName}";
                }
            }
            await _repo.UpdateAsync(episode);
            await _repo.SaveAsync();
            return true;
        }
    }
}
