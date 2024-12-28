
namespace Catalog.API.Products.GetProductById
{
    public record GetProdcutByIdResponse(Product product);
    public class GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQury(id));
                var response = result.Adapt<GetProdcutByIdResponse>();
            })
            .WithName("GetProductById")
            .Produces<GetProdcutByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get ProductsById")
            .WithDescription("Get ProductsById");
        }
    }
}
