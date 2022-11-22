namespace TamHac.AspNetCore.DurianApi.Domain.Core.Exception;

public class NoDurianFoundException : System.Exception
{
    public override string Message => "No durian found!";
}