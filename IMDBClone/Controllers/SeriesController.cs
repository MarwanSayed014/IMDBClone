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
    public class SeriesController : ControllerBase
    {
        public ISeriesManager _manager { get; }

        public SeriesController(ISeriesManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Series>>> GetAllAsync()
        {
            try
            {
                var Seriess = (await _manager.GetAllAsync()).ToList();
                foreach (var Series in Seriess)
                {
                    Series.CoverImgPath = "https://" + Request.Headers.Host + Series.CoverImgPath;
                }
                return Seriess;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("GetById/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Series>> GetByIdAsync(Guid id)
        {
            try
            {
                var Series = await _manager.GetByIdAsync(id);
                if (Series != null)
                    Series.CoverImgPath = "https://" + Request.Headers.Host + Series.CoverImgPath;
                return Series;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<bool>> CreateAsync([FromForm] CreateSeriesDto model)
        {
            try
            {
                if (!await _manager.IsSeriesNameUnique(model.Name, new Guid()))
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
        public async Task<ActionResult<bool>> UpdateAsync([FromForm] EditSeriesDto model)
        {
            try
            {
                if (!await _manager.IsSeriesNameUnique(model.Name, model.Id))
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
