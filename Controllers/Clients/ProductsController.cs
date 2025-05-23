﻿using System.Security.Claims;
using AgainPBL3.Dtos.Product;
using AgainPBL3.Interfaces;
using AgainPBL3.Mappers;
using AgainPBL3.Models;
using AgainPBL3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeThongMoiGioiDoCu.Controllers.Clients
{
    [Route("api/product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly AccountService _accountService;

        public ProductsController(
            IProductRepository productRepository,
            AccountService accountService
        )
        {
            _productRepository = productRepository;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts(
            [FromQuery] int? category_id = null,
            [FromQuery] int? user_id = null,
            [FromQuery] int page_number = 1,
            [FromQuery] int page_size = 10,
            [FromQuery] string? status = null,
            [FromQuery] string? keyword = null
        )
        {
            var result = await _productRepository.GetListProduct(
                category_id.HasValue ? category_id.Value : null,
                user_id.HasValue ? user_id.Value : null,
                status ?? "",
                keyword ?? "",
                page_number,
                page_size
            );
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByID(int id)
        {
            try
            {
                return await _productRepository.GetProductByID(id);
            }
            catch (Exception e)
            {
                return NotFound("Product not found!");
            }
            ;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProducts([FromBody] CreateProductDto product)
        {
            //var userName = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            //var userRole = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var newProduct = await _productRepository.AddProduct(product.MapToProduct(Convert.ToInt32(userId)));
            return CreatedAtAction(
                nameof(GetProductByID),
                new { id = newProduct.ProductID },
                newProduct
            );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var delProID = await _productRepository.DeleteProduct(id);
            return delProID != null
                ? Ok("Product delete success!")
                : NotFound("Product Not Found!");
        }

        [HttpPost("{id}/approved")]
        public async Task<ActionResult> ApprovedProduct(int id)
        {
            var product = await _productRepository.ApprovedProduct(id);
            return product != null
                ? Ok("Product approved success!")
                : NotFound("Product Not Found!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto product)
        {
            var updateProduct = await _productRepository.UpdateProduct(
                id,
                product.Title,
                product.Description,
                product.Price,
                product.CategoryID,
                product.Condition,
                product.Images,
                product.Location
            );
            return updateProduct != null ? Ok(updateProduct) : NotFound("Product Not Found!");
        }
    }
}
