// using System.Security.Principal;
// using FluentValidation;
// using Mediator;
// using Microsoft.AspNetCore.Mvc;
// using OneOf;
//
// namespace WebApplication2;
//
// public interface IProductService
// {
//     Task<OneOf<Exception, bool>> GetDetails(string name);
//     string GetName(ProductRequest request);
// }
//
// public class ProductService : IProductService
// {
//     public string GetName(ProductRequest request)
//     {
//         return request.Name;
//     }
//
//     public async Task<OneOf<Exception, bool>> GetDetails(string name) => name switch
//     {
//         "run" => await Task.FromResult(true),
//         "" => await Task.FromResult(new Exception("something goes wrong")),
//         _ => throw new Exception("Bull")
//     };
// }
//
// public static class ProductsEndpoints
// {
//     public static async Task<IResult> GetData(HttpContext ctx, IProductService productService, IMediator mediator,
//         [FromBody] ProductRequest request)
//         => (await mediator.Send(new ProductRequest("run"))).Match(
//             exception => Results.BadRequest(),
//             b => Results.Ok("nice"));
//     
//     public static async Task<IResult> GetUrlData(HttpContext ctc, HttpClient httpClient, [FromBody]HtmlRequest request)
//         => Results.Ok(await ParseHtml(request.Url, httpClient));
//
//     private static async Task<string> ParseHtml(string url, HttpClient client)
//         => await client.GetStringAsync(url);
// }
//
//
//
// public record ProductRequest(string Name) : IRequest<OneOf<Exception, bool>>;
//
// public class ProductRequestHandler : IRequestHandler<ProductRequest, OneOf<Exception, bool>>
// {
//     public async ValueTask<OneOf<Exception, bool>> Handle(ProductRequest request, CancellationToken cancellationToken)
//         => await GetDetails(request.Name);
//
//     //write a Func calculate two int
//     
//     Func<string, string> AppendName => (string input) => $"{input}-ludmal"; 
//     Func<string, string> AppendName2 = (string input) => $"{input}-ludmal-lastname";
//     
//     public string GetName(string name) => AnotherName(AppendName2, "ludmal");
//     
//     public string AnotherName(Func<string,string> func, string lastName) => func(lastName);
//     
//     static async Task<OneOf<Exception, bool>> GetDetails(string name) => name switch
//     {
//         "run" => await Task.FromResult(true),
//         "" => await Task.FromResult(new Exception("something goes wrong")),
//         _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
//     };
// }
//
// public class ProductValidator : AbstractValidator<ProductRequest>
// {
//     public ProductValidator()
//     {
//         RuleFor(_ => _.Name).NotEmpty().WithMessage("required product name");
//     }
// }
//
//
//
