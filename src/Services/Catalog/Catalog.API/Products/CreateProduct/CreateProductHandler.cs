using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Catergory, string Description, string ImageFile, Decimal Price)
                  : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);


    internal  class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Catergory = request.Catergory,
                Description = request.Description,
                ImageFile = request.ImageFile,
            };

            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
