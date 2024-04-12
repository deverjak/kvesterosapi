
using Kvesteros.Application.Models;

namespace Kvesteros.Application.Repositories;
public interface IHikeRepository
{
    Task<bool> CreateAsync(Hike hike);
    Task<Hike?> GetByIdAsync(Guid id);
    Task<Hike?> GetBySlugAsync(string slug);
    Task<IEnumerable<Hike>> GetAllAsync();
    Task<bool> UpdateAsync(Hike hike);
    Task<bool> DeleteAsync(Guid id);
}
