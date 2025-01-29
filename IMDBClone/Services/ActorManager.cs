using IMDBClone.Dtos;
using IMDBClone.Helpers;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using IMDBClone.Services.Interfaces;

namespace IMDBClone.Services
{
    public class ActorManager : IActorManager
    {
        public IActorRepo _repo { get; }

        public ActorManager(IActorRepo repo)
        {
            _repo = repo;
        }


        public async Task<bool> CreateAsync(CreateActorDto model, Guid adminId)
        {
            //Upload ProfileImage
            var dir = Directory.GetCurrentDirectory() + "/wwwroot/Images/Actors/";
            var ex = ServerFile.GetExtension(model.File.FileName);
            var imageName = Guid.NewGuid() + ex;
            var imagePath = dir + imageName;
            var isUploaded = ServerFile.Upload(model.File, imagePath);
            
            if (isUploaded)
            {
                var actor = new Actor
                {
                    Name = model.Name,
                    Brief = model.Brief,
                    DateOfBirth = model.DateOfBirth,
                    AdminId = adminId,
                    ProfileImgPath = $"/Images/Actors/{imageName}"
                };
                await _repo.CreateAsync(actor);
                await _repo.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var actor = await _repo.GetByIdAsync(Id);
            if (actor != null)
            {

                ServerFile.Delete(Directory.GetCurrentDirectory() + "/wwwroot" + actor.ProfileImgPath);
                await _repo.DeleteAsync(actor);
                await _repo.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return (await _repo.FindAllAsync()).ToList();
        }

        public async Task<Actor> GetByIdAsync(Guid Id)
        {
            return await _repo.GetByIdAsync(Id);
        }

        public async Task<bool> UpdateAsync(EditActorDto model, Guid adminId)
        {
            Actor actor = await GetByIdAsync(model.Id);
            if (actor == null) return false;
            var oldProfileImgPath = actor.ProfileImgPath;
            actor.Name = model.Name;
            actor.Brief = model.Brief;
            actor.DateOfBirth = model.DateOfBirth;
            actor.AdminId = adminId;
            actor.ProfileImgPath = oldProfileImgPath;

            if (model.File != null) 
            {
                var dir = Directory.GetCurrentDirectory() + "/wwwroot/Images/Actors/";

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

                    actor.ProfileImgPath = $"/Images/Actors/{imageName}";
                }
            }
            await _repo.UpdateAsync(actor);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> IsActorNameUnique(string name, Guid actorId)
        {
            var result = (await _repo.FindAsync(x => x.Name == name && x.Id != actorId))
                .ToList();
            return result.Count() == 0 ? true : false;
        }
    }
}
