using FishMarket.Api.Data;
using FishMarket.Api.Data.Entities;
using FishMarket.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FishMarket.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FishController(MarketDbContext marketDbContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Fish))]
    public async Task<IActionResult> GetFishAsync([FromQuery] int id)
    {
        var fish = await marketDbContext.Fishes.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        if (fish is null)
        {
            return NotFound();
        }

        return Ok(fish);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Fish))]
    public async Task<IActionResult> CreateFishAsync(FishRequest request)
    {
        var fish = new Fish()
        {
            Name = request.Name,
            Price = request.Price,
            Photo = request.Photo,
        };

        await marketDbContext.Fishes.AddAsync(fish);

        await marketDbContext.SaveChangesAsync();

        return Created(new Uri($"{HttpContext.Request.Path.Value}/{fish.Id}", UriKind.Relative), fish);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateFishAsync(FishRequest request, [FromQuery] int id)
    {
        var fish = await marketDbContext.Fishes.FirstOrDefaultAsync(x => x.Id == id);

        if (fish is null)
        {
            var fishToInsert = new Fish()
            {
                Name = request.Name,
                Price = request.Price,
                Photo = request.Photo
            };

            await marketDbContext.Fishes.AddAsync(fishToInsert);
            await marketDbContext.SaveChangesAsync();

            return Created(new Uri($"{HttpContext.Request.Path.Value}/{fishToInsert.Id}", UriKind.Relative), fishToInsert);
        }

        fish.Name = request.Name;
        fish.Price = request.Price;
        fish.Photo = request.Photo;

        await marketDbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteFishAsync([FromQuery] int id)
    {
        var deletedRowCount = await marketDbContext.Fishes
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        if (deletedRowCount <= 0)
        {
            return BadRequest(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "BadRequest",
                Detail = "No fish found for the given id to delete"
            });
        }

        return NoContent();
    }

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Fish>))]
    public async Task<IActionResult> GetAllFishesAsync()
    {
        var fishes = await marketDbContext.Fishes.AsNoTracking().ToArrayAsync();
        return Ok(fishes);
    }

    [HttpGet("recent")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Fish>))]
    public IActionResult GetRecentFishesAsync()
    {
        var fishes = marketDbContext.Fishes
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .Take(3);

        return Ok(fishes);
    }
}
