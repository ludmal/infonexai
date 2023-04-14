using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Endpoints.v1;

public static class AddOrUpdateContent
{
    public static async Task<IResult> Endpoint(HttpContext ctx, HttpClient httpClient, int brandId, [FromBody] ContentParserRequest request)
        => !string.IsNullOrEmpty(request.Url) switch
        {
            true => Results.Ok(await httpClient.GetStringAsync(request.Url)),
            _ => Results.BadRequest($"Url required to parse data : {brandId}")
        };
}

public sealed record AddOrUpdateContentRequest
{
    public string BrandId { get; set; } = string.Empty;
    public IEnumerable<string> Urls { get; set; } = Enumerable.Empty<string>();
}