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
    public class SeasonController : ControllerBase
    {
        public ISeasonManager _manager { get; }

        public SeasonController(ISeasonManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAllSeasons/{seriesId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Season>>> GetAllSeasonsAsync(Guid seriesId)
        {
            try
            {
                return (await _manager.GetAllSeasonsAsync(seriesId)).ToList(); ;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("GetById/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Season>> GetByIdAsync(Guid id)
        {
            try
            {
                return await _manager.GetByIdAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("CreateNewSeason")]
        public async Task<ActionResult<bool>> CreateNewSeasonAsync([FromForm] CreateSeasonDto model)
        {
            try
            {
                return await _manager.CreateNewSeasonAsync(model);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPut("UpdateLastSeason")]
        public async Task<ActionResult<bool>> UpdateLastSeasonAsync([FromForm] EditSeasonDto model)
        {
            try
            {
                return await _manager.UpdateLastAsync(model);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpDelete("DeleteLastSeason/{seriesId}")]
        public async Task<ActionResult<bool>> DeleteLastSeasonAsync(Guid seriesId)
        {
            try
            {
                return await _manager.DeleteLastSeasonAsync(seriesId);
            }
            catch (Exception)
            {
                return false;
            }

        }


    }
}
