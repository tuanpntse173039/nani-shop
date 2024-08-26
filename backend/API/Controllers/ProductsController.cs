using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper
        )
        {
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [Cache(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts(
            [FromQuery] ProductSpecParams productSpecParams
        )
        {
            var spec = new ProductsWithTypeAndBrandSpecification(productSpecParams);

            var countSpec = new ProductsWithFiltersForCountSpecification(productSpecParams);

            var products = await _productRepo.ListAsync(spec);

            var totalItems = await _productRepo.CountAsync(countSpec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products);

            return Ok(
                new Pagination<ProductDTO>(
                    productSpecParams.PageIndex,
                    productSpecParams.PageSize,
                    totalItems,
                    data
                )
            );
        }

        [Cache(600)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            // Product? product = await _productRepo.GetByIdAsync(id);
            var spec = new ProductsWithTypeAndBrandSpecification(id);

            var product = await _productRepo.GetEntityWithSpec(spec);

            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<Product, ProductDTO>(product));
        }

        [Cache(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [Cache(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}
