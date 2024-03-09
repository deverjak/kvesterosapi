using KvesterosApi.Models;
using KvesterosApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace KvesterosApi.Controllers;

[ApiController]
[Route("api/")]
public class HikeController : ControllerBase
{
    private readonly IRepository<Hike> _hikeRepository;

    public HikeController(IRepository<Hike> hikeRepository)
    {
        _hikeRepository = hikeRepository;
    }

    [HttpGet("[controller]s")]
    public async Task<IActionResult> GetAll()
    {
        var hikes = await _hikeRepository.GetAllAsync();

        if (hikes == null)
        {
            return NotFound();
        }

        return Ok(hikes);
    }

    [HttpGet("[controller]/{id}")]
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