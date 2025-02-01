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
    public class ProducerController : ControllerBase
    {
        public IProducerManager _manager { get; }

        public ProducerController(IProducerManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Producer>>> GetAllAsync()
        {
            try
            {
                var producers = (await _manager.GetAllAsync()).ToList();
                foreach (var Producer in producers)
                {
                    Producer.ProfileImgPath = "https://" + Request.Headers.Host + Producer.ProfileImgPath;
                }
                return producers;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("GetById/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Producer>> GetByIdAsync(Guid id)
        {
            try
            {
                var producer = await _manager.GetByIdAsync(id);
                if(producer != null)
                    producer.ProfileImgPath = "https://" + Request.Headers.Host + producer.ProfileImgPath;
                return producer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<bool>> CreateAsync([FromForm] CreateProducerDto model)
        {
            try
            {
                if (!await _manager.IsProducerNameUnique(model.Name, new Guid()))
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
        public async Task<ActionResult<bool>> UpdateAsync([FromForm] EditProducerDto model)
        {
            try
            {
                if (!await _manager.IsProducerNameUnique(model.Name, model.Id))
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
