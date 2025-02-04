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
    public class EpisodeController : ControllerBase
    {
        public IEpisodeManager _manager { get; }

        public EpisodeController(IEpisodeManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAll/{seasonId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Episode>>> GetAllAsync(Guid seasonId)
        {
            try
            {
                var Episodes = (await _manager.GetAllEpisodesAsync(seasonId)).ToList();
                foreach (var Episode in Episodes)
                {
                    Episode.CoverImgPath = "https://" + Request.Headers.Host + Episode.CoverImgPath;
                }
                return Episodes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("GetById/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Episode>> GetByIdAsync(Guid id)
        {
            try
            {
                var Episode = await _manager.GetByIdAsync(id);
                if (Episode != null)
                    Episode.CoverImgPath = "https://" + Request.Headers.Host + Episode.CoverImgPath;
                return Episode;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<bool>> CreateAsync([FromForm] CreateEpisodeDto model)
        {
            try
            {
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
        public async Task<ActionResult<bool>> UpdateAsync([FromForm] EditEpisodeDto model)
        {
            try
            {
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

        [HttpDelete("DeleteLastEpisode/{seasonId}")]
        public async Task<ActionResult<bool>> DeleteLastEpisodeAsync(Guid seasonId)
        {
            try
            {
                return await _manager.DeleteLastEpisodeAsync(seasonId);
            }
            catch (Exception)
            {
                return false;
            }

        }


    }
}
