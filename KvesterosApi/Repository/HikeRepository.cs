using KvesterosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KvesterosApi.Repository;

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

    public async Task AddAsync(Hike hike)
    {
        await _context.Hikes.AddAsync(hike);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Hike hike)
    {
        _context.Hikes.Update(hike);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var hike = await _context.Hikes.FindAsync(id);
        if (hike != null)
        {
            _context.Hikes.Remove(hike);
            await _context.SaveChangesAsync();
        }
    }
}