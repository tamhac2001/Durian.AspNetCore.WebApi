using LanguageExt;

namespace TamHac.AspNetCore.DurianApi.Durian.Service;

public interface IDurianService
{
    public Task<Seq<Model.Durian>> GetAll();

    public Task<Option<Model.Durian>> FindById(string id);

    public Task<Either<Exception, Unit>> Insert(Model.Durian durian);

    public Task<Unit> Update(string id, Model.Durian durian);

    public Task<Unit> Delete(string id);
}