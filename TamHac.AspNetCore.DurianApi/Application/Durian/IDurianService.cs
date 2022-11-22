using LanguageExt;

namespace TamHac.AspNetCore.DurianApi.Application.Durian;

public interface IDurianService
{
    public Task<Seq<Domain.Durian>> GetAllDurians();

    public Task<Option<Domain.Durian>> FindById(string id);

    public Task<Unit> Insert(Domain.Durian durian);

    public Task<Unit> Update(string id, Domain.Durian durian);

    public Task<Unit> Delete(string id);
}