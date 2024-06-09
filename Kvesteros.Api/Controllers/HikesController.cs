using Kvesteros.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using Kvesteros.Api.Contracts.Requests;
using Kvesteros.Api.Mappings;

namespace Kvesteros.Api.Controllers;

[ApiController]
public class HikesController(IHikeRepository hikeRepository) : ControllerBase
{
    private readonly IHikeRepository _hikeRepository = hikeRepository;

    [HttpPost(ApiEndpoints.Hikes.Create)]
    public async Task<IActionResult> Create([FromBody] CreateHikeRequest hikeRequest)
    {
        var hike = hikeRequest.MapToHike();
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

        return Ok(hikes.MapToResponse());
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

        return Ok(hike.MapToResponse());
    }

    [HttpPut(ApiEndpoints.Hikes.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateHikeRequest hikeRequest)
    {
        var hikeExists = await _hikeRepository.ExistsByIdAsync(id);

        if (!hikeExists)
        {
            return NotFound();
        }

        var hike = hikeRequest.MapToHike(id);

        var response = await _hikeRepository.UpdateAsync(hike);

        if (!response)
        {
            return BadRequest();
        }

        return Ok(hike.MapToResponse());
    }

    [HttpDelete(ApiEndpoints.Hikes.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var hikeExists = await _hikeRepository.ExistsByIdAsync(id);

        if (!hikeExists)
        {
            return NotFound();
        }

        var response = await _hikeRepository.DeleteAsync(id);

        if (!response)
        {
            return BadRequest();
        }

        return NoContent();
    }

}