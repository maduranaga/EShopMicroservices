using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeletProductResponse(bool isSuccess);

    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                var response = result.Adapt<DeletProductResponse>();
            })
            .WithName("DeleteProduct")
            .Produces<GetProdcutByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }
    }
}
