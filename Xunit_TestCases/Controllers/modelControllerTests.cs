using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using Xunit_API.Controllers;
using Xunit_API.Models;
using Xunit_API.Services.Interface;
using Model = Xunit_API.Models.Model;

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
        public async Task UpdateModel_ShouldReturnUpdatedModel_WhenEditSuccess()
        {
            // Arrange
            var modelId = fixture.Create<int>();
            var updatedModel = fixture.Create<Model>();
            var returnData = fixture.Create<Model>();
            modelInterface.Setup(c => c.UpdateModel(modelId, updatedModel)).ReturnsAsync(returnData);

            // Act
            var result = await modelController.UpdateModel(modelId, updatedModel) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeAssignableTo<Model>().And.BeEquivalentTo(returnData);

            modelInterface.Verify(t => t.UpdateModel(modelId, updatedModel), Times.Once());
        }


        [Fact]
        public void UpdateModel_ShouldReturnNull_WhenInputObjectIsNull()
        {
            //Arrange
            var modelId = fixture.Create<int>();
            Model models = null;
            modelInterface.Setup(c => c.UpdateModel(modelId, models)).ReturnsAsync((Model)null) ;

            //Act
            var result = modelController.UpdateModel(modelId, models);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            modelInterface.Verify(t => t.UpdateModel(modelId, models), Times.Never());

        }
        [Fact]
        public void UpdateModel_ShouldReturnNotFoundObjectResult_WhenNoDataFound()
        {
            //Arrange
            var modelId = fixture.Create<int>();
            var model = fixture.Create<Model>();
            modelInterface.Setup(c => c.UpdateModel(modelId, model)).Returns(Task.FromResult<Model>(null));

            //Act
            var result = modelController.UpdateModel(modelId, model);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            modelInterface.Verify(t => t.UpdateModel(modelId, model), Times.Once());
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
            result.Should().BeOfType<BadRequestObjectResult>().Subject.Value.Should().Be("Exception Caught"); ;
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
            result.Should().BeOfType<BadRequestObjectResult>().Subject.Value.Should().Be("Exception Caught"); ;
            modelInterface.Verify(m => m.GetAllModelBrand(), Times.Once());
        }
    }
}