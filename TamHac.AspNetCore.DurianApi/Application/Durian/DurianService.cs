using LanguageExt;
using TamHac.AspNetCore.DurianApi.Infrastructure.durian;

namespace TamHac.AspNetCore.DurianApi.Application.Durian;

public class DurianService : IDurianService
{
    private readonly IDurianRepository _repository;

    public DurianService(IDurianRepository repository)
    {
        _repository = repository;
    }

    public async Task<Seq<Domain.Durian>> GetAllDurians()
    {
        return await _repository.GetAll();
    }

    public async Task<Option<Domain.Durian>> FindById(string id)
    {
        return await _repository.FindById(id);
    }

    public async Task<Unit> Insert(Domain.Durian durian)
    {
        return await _repository.Insert(durian);
    }


    public async Task<Unit> Update(string id, Domain.Durian durian)
    {
        var durianWithUpdatedAt = durian with
        {
            UpdatedAt = DateTime.UtcNow
        };
        return await _repository.Update(id, durianWithUpdatedAt);
    }

    public async Task<Unit> Delete(string id)
    {
        return await _repository.Delete(id);
    }
}