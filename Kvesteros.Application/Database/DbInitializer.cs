using Dapper;

namespace Kvesteros.Application.Database;

public class DbInitializer(IDbConnectionFactory dbConnectionFactory)
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;


    public async Task InitializeAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS hikes
            (
                id UUID PRIMARY KEY,
                name TEXT NOT NULL,
                slug TEXT NOT NULL,
                yearofrelease INTEGER NOT NULL
            )
        """);

        await connection.ExecuteAsync("""
            CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS hikes_slug_idx ON hikes
            USING BTREE(slug);
            """);

    }

}