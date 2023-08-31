using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using Xunit_API.Controllers;
using Xunit_API.Models;
using Xunit_API.Services.Interface;

namespace Xunit_TestCases.Controllers
{
    public class modelControllerTests
    {
        private readonly IFixture fixture;
        private readonly Mock<IModelInterface> modelInterface;
        private readonly modelController modelController;
        public modelControllerTests()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            //fixture.Customize<Model>(composer => composer.Without(t => t.vehicle_model));
            modelInterface = fixture.Freeze<Mock<IModelInterface>>();
            modelController = new modelController(modelInterface.Object);
        }

        //Test cases for updating model
        [Fact]
        public async Task UpdateModel_ShouldReturnUpdatedModel_WhenNotNull()
        {
            // Arrange
            var modelId = fixture.Create<int>();
            var updatedModel = fixture.Create<Model>();

            modelInterface.Setup(m => m.UpdateModel(modelId, It.IsAny<Model>()))
                          .ReturnsAsync(updatedModel);

            // Act
            var result = await modelController.UpdateModel(modelId, updatedModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<Model>().Subject
                    .Should().BeEquivalentTo(updatedModel);

            modelInterface.Verify(m => m.UpdateModel(modelId, It.IsAny<Model>()), Times.Once());
        }
        [Fact]
        public async Task UpdateModel_ShouldReturnNull_WhenServiceReturnsNull()
        {
            // Arrange
            var modelId = fixture.Create<int>();
            modelInterface.Setup(m => m.UpdateModel(modelId, It.IsAny<Model>()))
                          .ReturnsAsync((Model)null);

            // Act
            var result = await modelController.UpdateModel(modelId, new Model());

            // Assert
            result.Should().BeNull();

            modelInterface.Verify(m => m.UpdateModel(modelId, It.IsAny<Model>()), Times.Once());
        }

        [Fact]
        public async Task UpdateModel_ShouldReturnBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var modelId = fixture.Create<int>();
            modelInterface.Setup(m => m.UpdateModel(modelId, It.IsAny<Model>()))
                          .Throws(new Exception("Exception Caught"));

            // Act
            var result = await modelController.UpdateModel(modelId, new Model());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            modelInterface.Verify(m => m.UpdateModel(modelId, It.IsAny<Model>()), Times.Once());
        }
        //Test cases for GetModelsAndBrands
        [Fact]
        public async Task GetModelsAndBrands_ShouldReturnModelsAndBrands_WhenNotNull()
        {
            // Arrange
            var modelsAndBrands = fixture.CreateMany<dynamic>();

            modelInterface.Setup(m => m.GetAllModelBrand())
                          .ReturnsAsync(modelsAndBrands);

            // Act
            var result = await modelController.GetModelsAndBrands();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<IEnumerable<dynamic>>().Subject
                    .Should().BeEquivalentTo(modelsAndBrands);

            modelInterface.Verify(m => m.GetAllModelBrand(), Times.Once());
        }
        [Fact]
        public async Task GetModelsAndBrands_ShouldReturnNull_WhenServiceReturnsNull()
        {
            // Arrange
            modelInterface.Setup(m => m.GetAllModelBrand())
                          .ReturnsAsync((IEnumerable<dynamic>)null);

            // Act
            var result = await modelController.GetModelsAndBrands();

            // Assert
            result.Should().BeNull();

            modelInterface.Verify(m => m.GetAllModelBrand(), Times.Once());
        }

        [Fact]
        public async Task GetModelsAndBrands_ShouldReturnBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            modelInterface.Setup(m => m.GetAllModelBrand())
                          .Throws(new Exception("Exception Caught"));

            // Act
            var result = await modelController.GetModelsAndBrands();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            modelInterface.Verify(m => m.GetAllModelBrand(), Times.Once());
        }
    }
}