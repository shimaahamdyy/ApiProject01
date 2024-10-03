using Store.Repository.Specifications.ProductSpecs;
using Store.Services.Helper;
using Store.Services.Services.ProductServices.Dto;

namespace Store.Services.Services.ProductServices
{
    public interface IProductServices
    {
        Task<ProductDetailsDto> GetProductByIdAsync(int? ProductId);
        Task<PaginationResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecifications specs);
        Task<IReadOnlyList<BrandsTypesDetailsDto>> GetAllBrandsAsync();
        Task<IReadOnlyList<BrandsTypesDetailsDto>> GetAllTypesAsync();
    }
}
