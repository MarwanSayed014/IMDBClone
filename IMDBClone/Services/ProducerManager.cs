using IMDBClone.Dtos;
using IMDBClone.Helpers;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using IMDBClone.Services.Interfaces;

namespace IMDBClone.Services
{
    public class ProducerManager : IProducerManager
    {
        public IProducerRepo _repo { get; }

        public ProducerManager(IProducerRepo repo)
        {
            _repo = repo;
        }


        public async Task<bool> CreateAsync(CreateProducerDto model, Guid adminId)
        {
            //Upload ProfileImage
            var dir = Directory.GetCurrentDirectory() + "/wwwroot/Images/Producers/";
            var ex = ServerFile.GetExtension(model.File.FileName);
            var imageName = Guid.NewGuid() + ex;
            var imagePath = dir + imageName;
            var isUploaded = ServerFile.Upload(model.File, imagePath);

            if (isUploaded)
            {
                var Producer = new Producer
                {
                    Name = model.Name,
                    Brief = model.Brief,
                    DateOfBirth = model.DateOfBirth,
                    AdminId = adminId,
                    ProfileImgPath = $"/Images/Producers/{imageName}"
                };
                await _repo.CreateAsync(Producer);
                await _repo.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var Producer = await _repo.GetByIdAsync(Id);
            if (Producer != null)
            {

                ServerFile.Delete(Directory.GetCurrentDirectory() + "/wwwroot" + Producer.ProfileImgPath);
                await _repo.DeleteAsync(Producer);
                await _repo.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Producer>> GetAllAsync()
        {
            return (await _repo.FindAllAsync()).ToList();
        }

        public async Task<Producer> GetByIdAsync(Guid Id)
        {
            return await _repo.GetByIdAsync(Id);
        }

        public async Task<bool> UpdateAsync(EditProducerDto model, Guid adminId)
        {
            Producer producer = await GetByIdAsync(model.Id);
            if (producer == null) return false;
            var oldProfileImgPath = producer.ProfileImgPath;
            producer.Name = model.Name;
            producer.Brief = model.Brief;
            producer.DateOfBirth = model.DateOfBirth;
            producer.AdminId = adminId;
            producer.ProfileImgPath = oldProfileImgPath;

            if (model.File != null)
            {
                var dir = Directory.GetCurrentDirectory() + "/wwwroot/Images/Producers/";

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

                    producer.ProfileImgPath = $"/Images/Producers/{imageName}";
                }
            }
            await _repo.UpdateAsync(producer);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> IsProducerNameUnique(string name, Guid ProducerId)
        {
            var result = (await _repo.FindAsync(x => x.Name == name && x.Id != ProducerId))
                .ToList();
            return result.Count() == 0 ? true : false;
        }
    }
}
