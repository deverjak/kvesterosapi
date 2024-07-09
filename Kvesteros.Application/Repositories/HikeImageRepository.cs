using Dapper;
using Kvesteros.Application.Database;
using Kvesteros.Application.Models;

namespace Kvesteros.Application.Repositories;

public class HikeImageRepository(IDbConnectionFactory dbConnectionFactory) : IHikeImageRepository
{
    readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    public async Task<bool> CreateAsync(HikeImage image, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(new CommandDefinition("""
            INSERT INTO hike_images (id, hike_id, title, path)
            VALUES (@Id, @HikeId, @Title, @Path)
        """, image, cancellationToken: cancellationToken));
        return result > 0;
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<HikeImage>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<HikeImage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return await connection.QueryFirstOrDefaultAsync<HikeImage>(new CommandDefinition("""
            SELECT id, hike_id as HikeId, path, title FROM hike_images WHERE id = @id
        """, new { Id = id }, cancellationToken: cancellationToken));
    }

    public Task<bool> UpdateAsync(HikeImage image, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
