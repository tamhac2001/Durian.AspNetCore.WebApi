using LanguageExt;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TamHac.AspNetCore.DurianApi.configuration;
using TamHac.AspNetCore.DurianApi.Domain;

namespace TamHac.AspNetCore.DurianApi.Infrastructure.durian;

public class DurianRepository : IDurianRepository
{
    private readonly IMongoCollection<Durian> _durianCollection;

    public DurianRepository(IOptions<DurianDatabaseConfiguration> databaseConfig)
    {
        var mongoClient = new MongoClient(databaseConfig.Value.ConnectionString);
        var database = mongoClient.GetDatabase(databaseConfig.Value.DatabaseName);
        _durianCollection = database.GetCollection<Durian>(databaseConfig.Value.DuriansCollectionName);
    }

    public async Task<Seq<Durian>> GetAll()
    {
        return Seq.createRange(await _durianCollection.Find(_ => true).ToListAsync());
    }

    public async Task<Option<Durian>> FindById(string id)
    {
        return new Some<Durian>(await _durianCollection.Find(durian => durian.Id == id).FirstOrDefaultAsync());
    }

    public async Task<Unit> Insert(Durian durian)
    {
        return await _durianCollection.InsertOneAsync(durian).ToUnit();
    }

    public async Task<Unit> Update(string id, Durian durian)
    {
        return await _durianCollection.ReplaceOneAsync(oldDurian => durian.Id == id,
            durian).ToUnit();
    }


    public async Task<Unit> Delete(string id)
    {
        return await _durianCollection.DeleteOneAsync(durian => durian.Id == id).ToUnit();
    }
}