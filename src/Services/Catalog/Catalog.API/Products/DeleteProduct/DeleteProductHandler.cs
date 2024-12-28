using System.Windows.Input;
using BuildingBlocks.CQRS;
using Marten;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid productId) : ICommand<DeleteProductResponse>;

    public record DeleteProductResponse(bool isSuccess);

    internal class DeleteProductHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResponse>
    {
        public async Task<DeleteProductResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            session.Delete<Product>(command.productId);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResponse(true);
        }
    }
}
