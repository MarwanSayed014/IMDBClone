using IMDBClone.Dtos;
using IMDBClone.Models;
using IMDBClone.Services.Interfaces;
using IMDBClone.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IMDBClone.Controllers
{
    [Route("IMDBClone/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleTypes.Admin)]
    public class DirectorController : ControllerBase
    {
        public IDirectorManager _manager { get; }

        public DirectorController(IDirectorManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Director>>> GetAllAsync()
        {
            try
            {
                var directors = (await _manager.GetAllAsync()).ToList();
                foreach (var director in directors)
                {
                    director.ProfileImgPath = "https://" + Request.Headers.Host + director.ProfileImgPath;
                }
                return directors;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("GetById/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Director>> GetByIdAsync(Guid id)
        {
            try
            {
                var director = await _manager.GetByIdAsync(id);
                if(director != null)
                    director.ProfileImgPath = "https://" + Request.Headers.Host + director.ProfileImgPath;
                return director;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<bool>> CreateAsync([FromForm] CreateDirectorDto model)
        {
            try
            {
                if (!await _manager.IsDirectorNameUnique(model.Name, new Guid()))
                {
                    ModelState.AddModelError("Name", $"{model.Name} is unavailable");
                    return BadRequest(ModelState);
                }
                if (model.File == null)
                    return false;
                Guid adminId = new Guid();
                bool result = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out adminId);
                if (result == false)
                    return false;
                return await _manager.CreateAsync(model, adminId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<bool>> UpdateAsync([FromForm] EditDirectorDto model)
        {
            try
            {
                if (!await _manager.IsDirectorNameUnique(model.Name, model.Id))
                {
                    ModelState.AddModelError("Name", $"{model.Name} is unavailable");
                    return BadRequest(ModelState);
                }
                Guid adminId = new Guid();
                bool result = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out adminId);
                if (result == false)
                    return false;
                return await _manager.UpdateAsync(model, adminId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(Guid id)
        {
            try
            {
                return await _manager.DeleteAsync(id);
            }
            catch (Exception)
            {
                return false;
            }

        }


    }
}
