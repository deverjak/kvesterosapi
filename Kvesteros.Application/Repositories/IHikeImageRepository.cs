using Kvesteros.Application.Models;

namespace Kvesteros.Application.Repositories;

public interface IHikeImageRepository
{
    Task<bool> CreateAsync(HikeImage image, CancellationToken cancellationToken = default);
    Task<HikeImage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<HikeImage>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(HikeImage image, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
