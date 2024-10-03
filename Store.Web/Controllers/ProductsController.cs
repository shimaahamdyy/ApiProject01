using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Repository.Specifications.ProductSpecs;
using Store.Services.Services.ProductServices;
using Store.Services.Services.ProductServices.Dto;

namespace Store.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandsTypesDetailsDto>>> GetAllBrands()
            => Ok(await _productServices.GetAllBrandsAsync());

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandsTypesDetailsDto>>> GetAllTypes()
            => Ok(await _productServices.GetAllTypesAsync());

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAllProducts([FromQuery] ProductSpecifications input)
           => Ok(await _productServices.GetAllProductsAsync(input));

        [HttpGet]
        public async Task<ActionResult<ProductDetailsDto>> GetProductById(int? id)
           => Ok(await _productServices.GetProductByIdAsync(id));
    }
}
