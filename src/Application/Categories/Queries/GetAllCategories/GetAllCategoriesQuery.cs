using Application.Categories.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
    {
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "categories";

        public GetAllCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            // Si ya está en caché, devolverlo directamente
            if (_memoryCache.TryGetValue(CacheKey, out List<CategoryDto> cachedCategories))
            {
                return cachedCategories;
            }

            var dtos = await _context.Categories
                            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                            .OrderBy(x => x.Name)
                            .ToListAsync(cancellationToken);

            // Guardar en caché por 30 minutos
            _memoryCache.Set(CacheKey, dtos, TimeSpan.FromMinutes(30));

            return dtos;
        }
    }
}
