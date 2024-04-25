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
        var hikeDictionary = new Dictionary<Guid, Hike>();

        await connection.QueryAsync<Hike, HikeImage, Hike>(
            new CommandDefinition("""
            SELECT h.*, hi.id, hi.hike_id as HikeId, hi.path, hi.title FROM hikes h
            LEFT JOIN hike_images hi ON h.id = hi.hike_id
        """, cancellationToken: cancellationToken),
            (hike, image) =>
            {
                if (!hikeDictionary.TryGetValue(hike.Id, out var hikeEntry))
                {
                    hikeEntry = hike;
                    hikeEntry.Images = [];
                    hikeDictionary.Add(hikeEntry.Id, hikeEntry);
                }

                if (image != null)
                {
                    hikeEntry.Images = [.. hikeEntry.Images, image];
                }
                return hikeEntry;
            },
            splitOn: "id");

        return hikeDictionary.Values.ToList();
    }

    public async Task<Hike?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var hike = await connection.QueryFirstOrDefaultAsync<Hike>(new CommandDefinition("""
            SELECT * FROM hikes WHERE id = @id
        """, new { id }, cancellationToken: cancellationToken));

        if (hike is null)
            return null;


        var hikeImages = await connection.QueryAsync<HikeImage>(new CommandDefinition("""
            SELECT id, hike_id as HikeId, path, title FROM hike_images WHERE hike_id = @id
        """, new { id }, cancellationToken: cancellationToken));


        hike.Images = hikeImages ?? [];

        return hike;
    }

    public async Task<Hike?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var hike = await connection.QueryFirstOrDefaultAsync<Hike>(new CommandDefinition("""
            SELECT * FROM hikes WHERE slug = @slug
        """, new { slug }, cancellationToken: cancellationToken));

        if (hike is null)
            return null;


        var hikeImages = await connection.QueryAsync<HikeImage>(new CommandDefinition("""
            SELECT id, hike_id as HikeId, path, title FROM hike_images WHERE hike_id = @id
        """, new { id = hike.Id }, cancellationToken: cancellationToken));


        hike.Images = hikeImages ?? [];

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
