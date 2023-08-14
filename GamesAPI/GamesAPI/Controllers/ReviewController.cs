using GamesAPI.DTOs.Reviews;
using GamesAPI.Models;
using GamesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("api/review")]
    [EnableCors("ReactApp")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public ReviewController(IReviewService reviewService, IAuthService authService, IUserService userService) 
        {
            _reviewService = reviewService;
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult> AddReview(AddReviewDTO dto)
        {
            Review reviewToAdd = new Review()
            {
                GameId = dto.GameId,
                Rating = dto.Rating,
                ReviewContent = dto.ReviewContent,
                UserId = dto.UserId
            };

            try
            {
                await _reviewService.Add(reviewToAdd);
                await _reviewService.SaveChanges();
                return Created("/api/reviews/add", reviewToAdd);
            }
            catch
            {
                return BadRequest("Cannot create review");
            }
        }

        [HttpDelete("remove/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult> RemoveReview(int id)
        {
            Review? reviewToRemove = await _reviewService.FindById(id);
            string? userName = HttpContext?.User?.Identity?.Name;
            if(userName != null)
            {
                AppUser? user = await _userService.FindByName(userName);
                if (user != null)
                {
                    if (reviewToRemove != null && reviewToRemove.UserId == user.Id)
                    {
                        try
                        {
                            await _reviewService.Remove(reviewToRemove);
                            await _reviewService.SaveChanges();
                        }
                        catch
                        {
                            return BadRequest("Error while removing review");
                        }
                    }
                    return NoContent();
                }
            }            
            else
            {
                return Unauthorized();
            }
            return BadRequest();
        }

        /// <summary>
        /// Potestować
        /// </summary>
        /// <param name="id">Id recenzji</param>
        /// <param name="dto">Obiekt z poprawionymi danymi</param>
        /// <returns></returns>

        [HttpDelete("update/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult> UpdateReview(int id, UpdateReviewDTO dto)
        {
            Review? reviewToUpdate = await _reviewService.FindById(id);
            string? userName = HttpContext?.User?.Identity?.Name;
            if (userName != null)
            {
                AppUser? user = await _userService.FindByName(userName);
                if (reviewToUpdate != null)
                {
                    if (user != null)
                    {
                        if (reviewToUpdate.UserId == user.Id)
                        {
                            try
                            {
                                if (dto.ReviewContent != null)
                                    reviewToUpdate.ReviewContent = dto.ReviewContent;
                                if (dto.Rating != null)
                                    reviewToUpdate.Rating = dto.Rating.Value;
                                if (dto.GameId != null)
                                    reviewToUpdate.GameId = dto.GameId.Value;
                                await _reviewService.SaveChanges();
                            }
                            catch
                            {
                                return BadRequest("Error while updating review");
                            }
                        }
                    }                    
                    return Unauthorized();
                }
                else
                {
                    return NotFound();
                }
            }        
            return Unauthorized();
        }
    }
}
