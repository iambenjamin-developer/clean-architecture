using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Products.Commands.UpdateProduct
{

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandValidator(IApplicationDbContext context)
        {
            _context = context;


            RuleFor(v => v.SKU)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(v => v.Description)
                .MaximumLength(500);

            RuleFor(v => v.Price)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.SKU)
                .Must(SkuIsAvailable)
                .WithMessage("'SKU' is already in use. Please enter a different value.");

            RuleFor(x => x.CategoryId)
            .Must(CategoryExists)
                .WithMessage("'CategoryId' is invalid or does not exist.");
        }

        private bool SkuIsAvailable(string sku)
        {
            bool exists = _context.Products
                        .Where(p => p.SKU.ToUpper() == sku.ToUpper())
                        .Any();

            return exists ? false : true;
        }

        private bool CategoryExists(long categoryId)
        {
            bool exists = _context.Categories
                                .Where(c => c.Id == categoryId)
                                .Any();

            return exists;
        }
    }
}
