
using DotNetCoeEFWebApi.Dtos;

namespace DotNetCoreEFWebApi.Services;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<ProductDto> CreateAsync(ProductDto productDto);
    Task<bool> UpadateAsync(int id, ProductDto productDto);
    Task<bool> DeleteAsync(int id);


}