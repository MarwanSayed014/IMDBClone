using IMDBClone.Dtos;
using IMDBClone.Helpers;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using IMDBClone.Services.Interfaces;

namespace IMDBClone.Services
{
    public class DirectorManager : IDirectorManager
    {
        public IDirectorRepo _repo { get; }

        public DirectorManager(IDirectorRepo repo)
        {
            _repo = repo;
        }


        public async Task<bool> CreateAsync(CreateDirectorDto model, Guid adminId)
        {
            //Upload ProfileImage
            var dir = Directory.GetCurrentDirectory() + "/wwwroot/Images/Directors/";
            var ex = ServerFile.GetExtension(model.File.FileName);
            var imageName = Guid.NewGuid() + ex;
            var imagePath = dir + imageName;
            var isUploaded = ServerFile.Upload(model.File, imagePath);

            if (isUploaded)
            {
                var Director = new Director
                {
                    Name = model.Name,
                    Brief = model.Brief,
                    DateOfBirth = model.DateOfBirth,
                    AdminId = adminId,
                    ProfileImgPath = $"/Images/Directors/{imageName}"
                };
                await _repo.CreateAsync(Director);
                await _repo.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var Director = await _repo.GetByIdAsync(Id);
            if (Director != null)
            {

                ServerFile.Delete(Directory.GetCurrentDirectory() + "/wwwroot" + Director.ProfileImgPath);
                await _repo.DeleteAsync(Director);
                await _repo.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Director>> GetAllAsync()
        {
            return (await _repo.FindAllAsync()).ToList();
        }

        public async Task<Director> GetByIdAsync(Guid Id)
        {
            return await _repo.GetByIdAsync(Id);
        }

        public async Task<bool> UpdateAsync(EditDirectorDto model, Guid adminId)
        {
            Director director = await GetByIdAsync(model.Id);
            if (director == null) return false;
            var oldProfileImgPath = director.ProfileImgPath;
            director.Name = model.Name;
            director.Brief = model.Brief;
            director.DateOfBirth = model.DateOfBirth;
            director.AdminId = adminId;
            director.ProfileImgPath = oldProfileImgPath;

            if (model.File != null)
            {
                var dir = Directory.GetCurrentDirectory() + "/wwwroot/Images/Directors/";

                //Upload new image
                var ex = ServerFile.GetExtension(model.File.FileName);
                var imageName = Guid.NewGuid() + ex;
                var imagePath = dir + imageName;
                var isUploaded = ServerFile.Upload(model.File, imagePath);

                if (isUploaded)
                {
                    //Delete old image
                    if (!string.IsNullOrEmpty(oldProfileImgPath))
                    {
                        var oldImageName = oldProfileImgPath.Split('/').Last();
                        ServerFile.Delete(dir + oldImageName);
                    }

                    director.ProfileImgPath = $"/Images/Directors/{imageName}";
                }
            }
            await _repo.UpdateAsync(director);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> IsDirectorNameUnique(string name, Guid DirectorId)
        {
            var result = (await _repo.FindAsync(x => x.Name == name && x.Id != DirectorId))
                .ToList();
            return result.Count() == 0 ? true : false;
        }
    }
}
