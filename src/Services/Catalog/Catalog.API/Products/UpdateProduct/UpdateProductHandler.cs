using System.Diagnostics.Eventing.Reader;
using System.Windows.Input;
using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Products.GetProducts;
using Marten;

namespace Catalog.API.Products.UpdateProduct
{

    public record UpdateProdcutCommand(Product product) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool isSucces);

    public class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProdcutCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProdcutCommand command, CancellationToken cancellationToken)
        {
            var products = await session.LoadAsync<Product>(command.product.Id, cancellationToken);
            if(products is null)
            {
                throw new ProductNotFoundException(command.product.Id);
            }

            products.Name = command.product.Name;
            products.Description = command.product.Description;

            session.Update(products);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
