using Application.Common.Interfaces;
using MediatR;

namespace Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                // throw new NotFoundException(nameof(TodoItem), request.Id);
                throw new Exception();
            }

            _context.Products.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
