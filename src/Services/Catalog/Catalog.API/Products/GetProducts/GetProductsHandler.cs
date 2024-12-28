using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> productsResponse);

    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
        : IQueryHandler<GetProductQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("get products data");
                var products = await session.Query<Product>().ToListAsync(cancellationToken);
                return new GetProductsResult(products);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
