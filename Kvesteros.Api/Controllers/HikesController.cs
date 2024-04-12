using Kvesteros.Api.Models;
using Kvesteros.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Kvesteros.Api.Controllers;

[ApiController]
public class HikesController(IRepository<Hike> hikeRepository) : ControllerBase
{
    private readonly IRepository<Hike> _hikeRepository = hikeRepository;

    [HttpPost(ApiEndpoints.Hikes.Create)]
    public async Task<IActionResult> Create([FromBody] Hike hike)
    {
        var reponse = await _hikeRepository.CreateAsync(hike);

        if (reponse is null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetById), new { id = reponse.Id }, reponse);
    }

    [HttpGet(ApiEndpoints.Hikes.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var hikes = await _hikeRepository.GetAllAsync();

        return Ok(hikes);
    }

    [HttpGet(ApiEndpoints.Hikes.Get)]
    public async Task<IActionResult> GetById(int id)
    {
        var hike = await _hikeRepository.GetByIdAsync(id);

        if (hike == null)
        {
            return NotFound();
        }

        return Ok(hike);
    }

}