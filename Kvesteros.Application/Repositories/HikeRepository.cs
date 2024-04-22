using Kvesteros.Application.Database;
using Kvesteros.Application.Models;
using Dapper;

namespace Kvesteros.Application.Repositories;

public class HikeRepository(IDbConnectionFactory dbConnectionFactory) : IHikeRepository
{
    readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<bool> CreateAsync(Hike hike, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(new CommandDefinition("""
            INSERT INTO hikes (id, name, description, route, slug, distance)
            VALUES (@Id, @Name, @Description, @Route, @Slug, @Distance)
        """, hike, cancellationToken: cancellationToken));
        return result > 0;
    }

    public async Task<IEnumerable<Hike>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var result = await connection.QueryAsync(new CommandDefinition("""
            SELECT * FROM hikes
        """, cancellationToken: cancellationToken));

        return result.Select(x => new Hike
        {
            Id = x.id,
            Name = x.name,
            Description = x.description,
            Route = x.route,
            Distance = x.distance
        });
    }

    public async Task<Hike?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var hike = await connection.QueryFirstOrDefaultAsync<Hike>(new CommandDefinition("""
            SELECT * FROM hikes WHERE id = @id
        """, new { id }, cancellationToken: cancellationToken));

        return hike;
    }

    public async Task<Hike?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var hike = await connection.QueryFirstOrDefaultAsync<Hike>(new CommandDefinition("""
            SELECT * FROM hikes WHERE slug = @slug
        """, new { slug }, cancellationToken: cancellationToken));

        return hike;
    }

    public async Task<bool> UpdateAsync(Hike hike, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(new CommandDefinition("""
            UPDATE hikes
            SET name = @Name, description = @Description, route = @Route, distance = @Distance
            WHERE id = @Id
        """, hike, cancellationToken: cancellationToken));
        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(new CommandDefinition("""
            DELETE FROM hikes WHERE id = @id
        """, new { id }, cancellationToken: cancellationToken));
        return result > 0;
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
            SELECT COUNT(1) FROM hikes WHERE id = @id
            """, new { id }));
    }

}
