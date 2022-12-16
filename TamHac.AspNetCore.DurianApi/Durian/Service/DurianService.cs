using LanguageExt;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TamHac.AspNetCore.DurianApi.configuration;

namespace TamHac.AspNetCore.DurianApi.Durian.Service;

public class DurianService : IDurianService
{
    private readonly IMongoCollection<Model.Durian> _durianCollection;
    private readonly ILogger _logger;

    public DurianService(IOptions<DurianDatabaseConfiguration> databaseConfig, ILogger<DurianService> logger)
    {
        _logger = logger;
        var mongoClient = new MongoClient(databaseConfig.Value.ConnectionString);
        var database = mongoClient.GetDatabase(databaseConfig.Value.DatabaseName);
        _durianCollection = database.GetCollection<Model.Durian>(databaseConfig.Value.DuriansCollectionName);

        var options = new CreateIndexOptions { Unique = true };
        var codeIndexModel = new CreateIndexModel<Model.Durian>("""{"Code": 1}""", options);
        var nameIndexModel = new CreateIndexModel<Model.Durian>("""{"Name": 1}""", options);
        _durianCollection.Indexes.CreateMany(new[] { codeIndexModel, nameIndexModel });
    }

    public async Task<Seq<Model.Durian>> GetAll()
    {
        return Seq.createRange(await _durianCollection.Find(_ => true).ToListAsync());
    }


    public async Task<Option<Model.Durian>> FindById(string id)
    {
        return new Some<Model.Durian>(await _durianCollection.Find(durian => durian.Id == id).FirstOrDefaultAsync());
    }

    public async Task<Either<Exception, Unit>> Insert(Model.Durian durian)
    {
        try
        {
            return Either<Exception, Unit>.Right(await _durianCollection.InsertOneAsync(durian).ToUnit());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Either<Exception, Unit>.Left(e);
        }
    }

    public async Task<Unit> Update(string id, Model.Durian durian)
    {
        var dbDurian = await _durianCollection.Find(oldDurian => oldDurian.Id == durian.Id).FirstOrDefaultAsync();
        var durianWithOldCreatedAt = durian with
        {
            CreatedAt = dbDurian.CreatedAt
        };
        return await _durianCollection.ReplaceOneAsync(oldDurian => oldDurian.Id == id,
            durianWithOldCreatedAt).ToUnit();
    }

    public async Task<Unit> Delete(string id)
    {
        return await _durianCollection.DeleteOneAsync(durian => durian.Id == id).ToUnit();
    }
}