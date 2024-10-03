using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specifications.ProductSpecs;
using Store.Repository.Specifications.ProductWithSpecs;
using Store.Services.Helper;
using Store.Services.Services.ProductServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandsTypesDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsNoTrackingAsync();

            var mappedBrands = _mapper.Map<IReadOnlyList<BrandsTypesDetailsDto>>(brands);

            return mappedBrands;
        }

        public async Task<PaginationResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecifications input)
        {
            var specs = new ProductWithSpecification(input);

            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecificationsAsync(specs);

            var CountSpecs = new ProductWithCountSpecification(input);

            var count = await _unitOfWork.Repository<Product, int>().GetCountSpecificationsAsync(CountSpecs);

            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products); ;

            return new PaginationResultDto<ProductDetailsDto>( input.PageSize , input.PageIndex  , count, mappedProducts);
        }
        public async Task<IReadOnlyList<BrandsTypesDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsNoTrackingAsync();

            var mappedTypes = _mapper.Map<IReadOnlyList<BrandsTypesDetailsDto>>(types);

            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? ProductId)
        {
            if (ProductId is null)
                throw new Exception("ID is Null");

            var specs = new ProductWithSpecification(ProductId);

            var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(ProductId.Value);

            if (product is null)
                throw new Exception("product Not Found");

            var mappedProduct = _mapper.Map<ProductDetailsDto>(product);
            return mappedProduct;

        }
    }
}
