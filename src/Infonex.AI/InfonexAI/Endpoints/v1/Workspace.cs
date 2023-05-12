namespace WebApplication2.Endpoints.v1;

public static class Workspace
{
    public static async Task<IResult> Create(HttpContext ctx, HttpClient httpClient)
        => await Task.FromResult(Results.Ok("created"));
}