using LanguageExt;
using TamHac.AspNetCore.DurianApi.Domain;

namespace TamHac.AspNetCore.DurianApi.Infrastructure.durian;

public interface IDurianRepository
{
    public Task<Seq<Durian>> GetAll();

    public Task<Option<Durian>> FindById(string id);

    public Task<Unit> Insert(Durian durian);

    public Task<Unit> Update(string id, Durian durian);

    public Task<Unit> Delete(string id);
}