using GamesAPI.DTOs.Games;
using GamesAPI.Models;
using GamesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesAPI.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gamesService;
        public GamesController(IGamesService gamesService) 
        {
            _gamesService = gamesService;
        }

        //dać return Created
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<ActionResult> Add(CreateGameDTO dto)
        {
            Game gameToAdd = new Game()
            {
                Name = dto.Name,
                AvgRating = dto.AvgRating,
                Description = dto.Description,
                Reviews = new List<Review>()
            };
            if (await _gamesService.Exists(gameToAdd) == false)
            {
                try
                {
                    await _gamesService.Add(gameToAdd);
                    await _gamesService.SaveChanges();
                    return Created("https://localhost:7054/api/games/add", gameToAdd);
                }
                catch
                {
                    return BadRequest("Adding failed");
                }
            }
            return BadRequest("Game already exists");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            Game? gameToRemove = await _gamesService.FindById(id);
            if(gameToRemove != null)
            {
                try
                {
                    await _gamesService.Remove(gameToRemove);
                    await _gamesService.SaveChanges();
                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }          
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("update/{id}")]
        public async Task<ActionResult> Update(int id, UpdateGameDTO dto)
        {
            Game? gameToUpdate = await _gamesService.FindById(id);
            if(gameToUpdate != null)
            {
                try
                {
                    if(dto.Description != null)
                        gameToUpdate.Description = dto.Description;
                    if(dto.Name != null)
                        gameToUpdate.Name = dto.Name;
                    if (dto.Reviews != null)
                        gameToUpdate.Reviews = dto.Reviews;
                    if (dto.AvgRating != null)
                        gameToUpdate.AvgRating = dto.AvgRating.Value;
                    await _gamesService.SaveChanges();
                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            List<Game> games = await _gamesService.GetAll();
            if(games != null)
                return Ok(games);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            Game? gameToGet = await _gamesService.FindById(id);
            if (gameToGet != null)
                return Ok(gameToGet);
            return NoContent();
        }
    }
}
