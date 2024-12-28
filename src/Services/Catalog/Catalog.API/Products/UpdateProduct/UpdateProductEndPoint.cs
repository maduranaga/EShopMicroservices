using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductResponse(bool isSuccess);

    public class UpdateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (Product product, ISender sender) =>
            {
                var result = await sender.Send(new UpdateProdcutCommand(product));
                var response = result.Adapt<DeletProductResponse>();
            })
           .WithName("UpdateProduct")
           .Produces<GetProdcutByIdResponse>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .WithSummary("Update Product")
           .WithDescription("Update Product");
        }
    }
}
