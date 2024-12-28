using BuildingBlocks.CQRS;
using Catalog.API.Models;
using FluentValidation;
using JasperFx.CodeGeneration.Frames;
using Marten;

namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Catergory, string Description, string ImageFile, Decimal Price)
                  : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Requried");
            RuleFor(x => x.Catergory).NotEmpty().WithMessage("Category Is Requried");
        }
    }

    internal class CreateProductHandler(IDocumentSession session, IValidator<CreateProductCommand> validator)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Catergory = command.Catergory,
                Description = command.Description,
                ImageFile = command.ImageFile,
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
