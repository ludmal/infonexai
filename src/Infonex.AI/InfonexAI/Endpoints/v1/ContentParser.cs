using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Azure.Storage.Blobs;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace WebApplication2.Endpoints.v1;

public static class ContentParser
{
    public static async Task<IResult> Endpoint(HttpContext ctx, HttpClient httpClient, int brandId, [FromBody] ContentParserRequest request)
        => !string.IsNullOrEmpty(request.Url) switch
        {
            true => Results.Ok(await GetHtmlContent(await httpClient.GetStringAsync(request.Url))),
            _ => Results.BadRequest($"Url required to parse data : {brandId}")
        };

    public static async Task UploadFile
        (BlobContainerClient containerClient, string localFilePath)
    {
        string fileName = Path.GetFileName(localFilePath);
        BlobClient blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(localFilePath, true);
    }
    
    static async Task<HtmlResponse> GetHtmlContent(string content)
    {
        var html = new HtmlDocument();
        html.LoadHtml(content);
        List<string> hrefTags = new List<string>();

        foreach (HtmlNode link in html.DocumentNode.SelectNodes("//a[@href]"))
        {
            HtmlAttribute att = link.Attributes["href"];
            hrefTags.Add(att.Value);
        }

        var body = new List<string>();
        foreach (HtmlNode link in html.DocumentNode.SelectNodes("//body//p"))
        {
            body.Add(link.InnerText);

        }
        
        var title = html.DocumentNode.SelectNodes("//title");
        var d = new HtmlResponse(title?.FirstOrDefault()?.InnerText ?? string.Empty, string.Join(" ", body), hrefTags);

        BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=infonexdata;AccountKey=X4BoZ3TCp/scA7OOMaoXKAtqTh+SlbgOaY1tUClT+wgRGdPEK+0B+g9aLpNkcjlILt/SERiflsyL+ASt7CPPog==;EndpointSuffix=core.windows.net");
        BlobContainerClient container = blobServiceClient.GetBlobContainerClient("data-files");

        var s = $"{d.Body}";
        BlobClient blob = container.GetBlobClient("test.text");
        using MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(s));
        await blob.UploadAsync(ms);
        return d;
    }
}

public sealed record HtmlResponse(string Title, string Body, IList<string> Links);
public class ContentParserRequest
{
    public string Url { get; set; } = string.Empty;
}