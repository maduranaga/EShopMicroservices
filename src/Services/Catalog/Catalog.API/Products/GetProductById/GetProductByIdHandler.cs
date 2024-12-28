using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQury(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product product);

    internal class GetProductsByIdQueryHandler(IDocumentSession session, ILogger<GetProductsByIdQueryHandler> logger)
        : IQueryHandler<GetProductByIdQury, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQury request, CancellationToken cancellationToken)
        {
            logger.LogInformation("get products data");
            var products = await session.LoadAsync<Product>(request.Id, cancellationToken);

            if (products is null)
            {
                throw new ProductNotFoundException();
            }

            return new GetProductByIdResult(products);
        }
    }
}
