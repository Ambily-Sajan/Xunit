using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit_API.Controllers;
using Xunit_API.Models;
using Xunit_API.Services.Interface;

namespace Xunit_TestCases.Controllers
{
    public class brandControllerTests
    {
        private readonly IFixture fixture;
        private readonly Mock<IBrand> brandInterface;
        private readonly brandController brandController;
        public brandControllerTests()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior()); 
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customize<brand>(composer => composer.Without(t => t.vehicle_model));
            brandInterface = fixture.Freeze<Mock<IBrand>>();
            brandController = new brandController(brandInterface.Object);
        }

        //Test Cases for GellAllBrandsOfAVehicleType
        [Fact]
        public async Task GetAllBrandsOfAVehicleType_ShouldReturnOk_WhenBrandsNotNull()
        {
            // Arrange
            var vehicleTypeId = fixture.Create<int>();
            var brandList = fixture.CreateMany<brand>();
            brandInterface.Setup(b => b.GetAllBrandsOfAVehicleType(vehicleTypeId)).ReturnsAsync(brandList);

            // Act
            var result = await brandController.GetAllBrandsOfAVehicleType(vehicleTypeId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(brandList);
            brandInterface.Verify(b => b.GetAllBrandsOfAVehicleType(vehicleTypeId), Times.Once());
        }


        [Fact]
        public async Task GetAllBrandsOfAVehicleType_ShouldReturnNull_WhenBrandsAreNull()
        {
            // Arrange
            var vehicleTypeId = fixture.Create<int>();
            IEnumerable<brand> brandList = null;
            brandInterface.Setup(b => b.GetAllBrandsOfAVehicleType(vehicleTypeId)).ReturnsAsync(brandList);

            // Act
            var result = await brandController.GetAllBrandsOfAVehicleType(vehicleTypeId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
            brandInterface.Verify(b => b.GetAllBrandsOfAVehicleType(vehicleTypeId), Times.Once());
        }
        [Fact]
        public void GetAllBrandsOfAVehicleType_ShouldReturnNotFoundObjectResult_WhenNoDataFound()
        {
            //Arrange
            var vehicleTypeId = fixture.Create<int>();
            IEnumerable<brand> brandList = null;
            brandInterface.Setup(b => b.GetAllBrandsOfAVehicleType(vehicleTypeId)).ReturnsAsync(brandList);

            //Act
            var result = brandController.GetAllBrandsOfAVehicleType(vehicleTypeId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            brandInterface.Verify(t => t.GetAllBrandsOfAVehicleType(vehicleTypeId), Times.Once());
        }

        [Fact]
        public async Task GetAllBrandsOfAVehicleType_ShouldReturnBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var vehicleTypeId = fixture.Create<int>();
            brandInterface.Setup(b => b.GetAllBrandsOfAVehicleType(vehicleTypeId)).Throws(new Exception("Exception Caught"));

            // Act
            var result = await brandController.GetAllBrandsOfAVehicleType(vehicleTypeId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            brandInterface.Verify(b => b.GetAllBrandsOfAVehicleType(vehicleTypeId), Times.Once());
        }

        //Test Cases for DeleteBrand
        [Fact]
        public async Task DeleteBrand_BrandId_ReturnsOkResult()
        {
            // Arrange
            var brandId = fixture.Create<int>();
            var delete = fixture.Create<brand>();

            brandInterface.Setup(x => x.DeleteBrand(brandId))
                                .ReturnsAsync(delete);


            // Act
            var result = await brandController.DeleteBrand(brandId);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject
                    .Value.Should().BeAssignableTo<brand>().Subject
                    .Should().BeEquivalentTo(delete);
            brandInterface.Verify(r => r.DeleteBrand(brandId), Times.Once());

        }
        [Fact]
        public async Task DeleteBrand_BrandId_ReturnsNull()
        {
            // Arrange
            var brandId = fixture.Create<int>();
            brandInterface.Setup(x => x.DeleteBrand(brandId))
                          .ReturnsAsync((brand)null);

            // Act
            var result = await brandController.DeleteBrand(brandId);

            // Assert
            result.Should().BeNull();
            brandInterface.Verify(r => r.DeleteBrand(brandId), Times.Once());
        }
        [Fact]
        public async Task DeleteBrand_ShouldReturnBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var brandId = fixture.Create<int>();
            brandInterface.Setup(x => x.DeleteBrand(brandId))
                          .Throws(new Exception("Exception Caught"));

            // Act
            var result = await brandController.DeleteBrand(brandId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            brandInterface.Verify(r => r.DeleteBrand(brandId), Times.Once());
        }


    }
}