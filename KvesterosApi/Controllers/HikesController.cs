using KvesterosApi.Models;
using KvesterosApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace KvesterosApi.Controllers;

[ApiController]
public class HikesController(IRepository<Hike> hikeRepository) : ControllerBase
{
    private readonly IRepository<Hike> _hikeRepository = hikeRepository;

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