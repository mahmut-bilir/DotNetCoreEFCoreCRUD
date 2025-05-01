

using AutoMapper;
using DotNetCoeEFWebApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreEFWebApi.Services;


public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ProductDto> CreateAsync(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await _context.Products.ToListAsync();

        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return product == null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task<bool> UpadateAsync(int id, ProductDto productDto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _mapper.Map(productDto, product);

        await _context.SaveChangesAsync();
        return true;
    }
}