using KvesterosAdminApi.Models;
using KvesterosAdminApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace KvesterosAdminApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HikeController : ControllerBase
{
    private readonly IRepository<Hike> _hikeRepository;

    public HikeController(IRepository<Hike> hikeRepository)
    {
        _hikeRepository = hikeRepository;
    }

    [HttpGet("{id}")]
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