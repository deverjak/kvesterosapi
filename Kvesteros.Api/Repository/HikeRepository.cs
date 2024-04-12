using Kvesteros.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Kvesteros.Api.Repository;

public class HikeRepository : IRepository<Hike>
{
    private readonly ApplicationDbContext _context;

    public HikeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Hike>> GetAllAsync()
    {
        return await _context.Hikes.ToListAsync();
    }

    public async Task<Hike?> GetByIdAsync(int id)
    {
        return await _context.Hikes.FindAsync(id);
    }

    public async Task<Hike> CreateAsync(Hike hike)
    {
        await _context.Hikes.AddAsync(hike);
        await _context.SaveChangesAsync();
        return hike;

    }

    public async Task<bool> UpdateAsync(Hike hike)
    {
        _context.Hikes.Update(hike);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var hike = await _context.Hikes.FindAsync(id);
        if (hike != null)
        {
            _context.Hikes.Remove(hike);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}