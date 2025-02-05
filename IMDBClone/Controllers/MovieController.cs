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
    public class MovieController : ControllerBase
    {
        public IMovieManager _manager { get; }

        public MovieController(IMovieManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllAsync()
        {
            try
            {
                var Movies = (await _manager.GetAllAsync()).ToList();
                foreach (var Movie in Movies)
                {
                    Movie.CoverImgPath = "https://" + Request.Headers.Host + Movie.CoverImgPath;
                }
                return Movies;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("GetById/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Movie>> GetByIdAsync(Guid id)
        {
            try
            {
                var Movie = await _manager.GetByIdAsync(id);
                if (Movie != null)
                    Movie.CoverImgPath = "https://" + Request.Headers.Host + Movie.CoverImgPath;
                return Movie;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<bool>> CreateAsync([FromForm] CreateMovieDto model)
        {
            try
            {
                if (!await _manager.IsMovieNameUnique(model.Name, new Guid()))
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
        public async Task<ActionResult<bool>> UpdateAsync([FromForm] EditMovieDto model)
        {
            try
            {
                if (!await _manager.IsMovieNameUnique(model.Name, model.Id))
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
