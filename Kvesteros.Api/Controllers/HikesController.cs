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
        var reponse = await _hikeRepository.CreateAsync(hike);

        if (!reponse)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetById), new { id = 1 }, reponse);
    }

    [HttpGet(ApiEndpoints.Hikes.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var hikes = await _hikeRepository.GetAllAsync();

        return Ok(hikes);
    }

    [HttpGet(ApiEndpoints.Hikes.Get)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var hike = await _hikeRepository.GetByIdAsync(id);

        if (hike == null)
        {
            return NotFound();
        }

        return Ok(hike);
    }

}