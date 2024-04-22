using Kvesteros.Application.Models;
using Kvesteros.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Kvesteros.Api.Controllers;

[ApiController]
public class HikesController(IHikeRepository hikeRepository) : ControllerBase
{
    private readonly IHikeRepository _hikeRepository = hikeRepository;

    [HttpPost(ApiEndpoints.Hikes.Create)]
    public async Task<IActionResult> Create([FromBody] Hike hike)
    {
        var response = await _hikeRepository.CreateAsync(hike);

        if (!response)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetByIdOrSlug), new { idOrSlug = hike.Id }, response);
    }

    [HttpGet(ApiEndpoints.Hikes.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var hikes = await _hikeRepository.GetAllAsync();

        return Ok(hikes);
    }

    [HttpGet(ApiEndpoints.Hikes.Get)]
    public async Task<IActionResult> GetByIdOrSlug([FromRoute] string idOrSlug)
    {
        var hike = Guid.TryParse(idOrSlug, out var id)
            ? await _hikeRepository.GetByIdAsync(id)
            : await _hikeRepository.GetBySlugAsync(idOrSlug);

        if (hike == null)
        {
            return NotFound();
        }

        return Ok(hike);
    }

}