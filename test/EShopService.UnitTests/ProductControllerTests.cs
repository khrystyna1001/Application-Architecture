using Xunit;
using Microsoft.AspNetCore.Mvc;
using EShopService.API.Controllers;
using EShopService.Domain.Entities;
using System.Collections.Generic;

namespace EShopService.UnitTests
{
    public class ProductControllerTests
    {
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            // Arrange: Initialize controller before every test execution
            _controller = new ProductController();
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithListOfActiveProducts()
        {
            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.NotEmpty(products);
        }

        [Fact]
        public void GetById_ExistingId_ReturnsOkResult_WithCorrectProduct()
        {
            // Arrange
            int targetId = 1;

            // Act
            var result = _controller.GetById(targetId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var product = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(targetId, product.Id);
        }

        [Fact]
        public void GetById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            int fakeId = 999;

            // Act
            var result = _controller.GetById(fakeId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void Create_ValidProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            var newProduct = new Product 
            { 
                Name = "Mechanical Keyboard", 
                Price = 89.99M, 
                Ean = "1111222233334", 
                Sku = "KEY-MECH-01" 
            };

            // Act
            var result = _controller.Create(newProduct);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedProduct = Assert.IsType<Product>(createdResult.Value);
            Assert.True(returnedProduct.Id > 0);
            Assert.Equal("Mechanical Keyboard", returnedProduct.Name);
        }
    }
}
