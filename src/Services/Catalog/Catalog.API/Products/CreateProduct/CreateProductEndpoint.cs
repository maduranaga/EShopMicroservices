using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProdcutRequest(string Name, List<string> Catergory, string Description, string ImageFile, Decimal Price);

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products",
                      async (CreateProdcutRequest request, ISender sender) =>
                      {
                          var command = request.Adapt<CreateProductCommand>();
                          var result = await sender.Send(command);
                          var response = result.Adapt<CreateProductResponse>();
                          return Results.Created($"/products/{response.Id}", response);
                      })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("create Product");
        }
    }
}
