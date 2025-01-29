using IMDBClone.Dtos;
using IMDBClone.Models;
using IMDBClone.Services.Interfaces;
using IMDBClone.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace IMDBClone.Controllers
{
    [Route("IMDBClone/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleTypes.Admin)]
    public class ActorController : ControllerBase
    {
        public IActorManager _manager { get; }

        public ActorController(IActorManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Actor>>> GetAllAsync()
        {
            try
            {
                var actors = (await _manager.GetAllAsync()).ToList();
                foreach (var actor in actors)
                {
                    actor.ProfileImgPath  = "https://" + Request.Headers.Host + actor.ProfileImgPath;
                }
                return actors;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("GetById/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Actor>> GetByIdAsync(Guid id)
        {
            try
            {
                var actor = await _manager.GetByIdAsync(id);
                if(actor != null)
                    actor.ProfileImgPath = "https://" + Request.Headers.Host + actor.ProfileImgPath;
                return actor;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<bool>> CreateAsync([FromForm] CreateActorDto model)
        {
            try
            {
                if (!await _manager.IsActorNameUnique(model.Name, new Guid())) 
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
        public async Task<ActionResult<bool>> UpdateAsync([FromForm] EditActorDto model)
        {
            try
            {
                if (!await _manager.IsActorNameUnique(model.Name, model.Id))
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
