
using Kvesteros.Application.Models;

namespace Kvesteros.Application.Repositories;
public interface IHikeRepository
{
    Task<bool> CreateAsync(Hike hike, CancellationToken cancellationToken = default);
    Task<Hike?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Hike?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<IEnumerable<Hike>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Hike hike, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
