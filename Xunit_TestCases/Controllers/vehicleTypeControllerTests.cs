using Moq;
using AutoFixture;
using FluentAssertions;
using Xunit_API.Services.Interface;
using Xunit_API.Controllers;
using Xunit_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Xunit_TestCases.Controllers
{
    public class vehicleTypeControllerTests
    {

        private readonly IFixture fixture;
        private readonly Mock<IVehicleType> vehicleInterface;
        private readonly vehicleTypeController vehicleController;

        public vehicleTypeControllerTests()
        {
            fixture = new Fixture();
            fixture.Customize<VehicleType>(composer => composer.Without(t => t.vehicle_brands));
            vehicleInterface = fixture.Freeze<Mock<IVehicleType>>();
            vehicleController = new vehicleTypeController(vehicleInterface.Object);
        }

        //Test Cases for Add vehicle Type
        [Fact]
        public void AddVechicleType_ShouldReturnOk_WhenVehicleIsNotNull()
        {
            //Arrange
            var vehicle = fixture.Create<VehicleType>();
            var returnData = fixture.Create<VehicleType>();
            vehicleInterface.Setup(t => t.AddVehicleType(vehicle)).ReturnsAsync(returnData);
            
            //Act
            var result = vehicleController.AddVehicleType(vehicle);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            vehicleInterface.Verify(t => t.AddVehicleType(vehicle), Times.Once());

        }

        [Fact]
        public void AddVehicleType_ShouldReturnNull_WhenVehicleIsNull()
        {
            VehicleType vehicle = null;

            // Act
            var result = vehicleController.AddVehicleType(vehicle);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeNull();
            vehicleInterface.Verify(t => t.AddVehicleType(It.IsAny<VehicleType>()), Times.Once());
        }

        [Fact]
        public void AddVehicleType_ShouldReturnBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var vehicle = fixture.Create<VehicleType>();
            vehicleInterface.Setup(t => t.AddVehicleType(vehicle)).Throws(new Exception("Exception Caught"));

            // Act
            var result = vehicleController.AddVehicleType(vehicle);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            vehicleInterface.Verify(t => t.AddVehicleType(vehicle), Times.Once());
        }

        //Test cases for GetAllVehicleType
        [Fact]
        public void GetAllVehicleType_ShouldReturnOk_WhenVehicleTypesNotNull()
        {
            // Arrange
            var vehicleTypes = fixture.CreateMany<VehicleType>();
            vehicleInterface.Setup(t => t.GetAllVehicleType()).ReturnsAsync(vehicleTypes);

            // Act
            var result = vehicleController.GetAllVehicleType();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            vehicleInterface.Verify(t => t.GetAllVehicleType(), Times.Once());
        }

        [Fact]
        public void GetAllVehicleType_ShouldReturnNull_WhenVehicleTypesAreNull()
        {
            // Arrange
            IEnumerable<VehicleType> vehicleTypes = null;
            vehicleInterface.Setup(t => t.GetAllVehicleType()).ReturnsAsync(vehicleTypes);

            // Act
            var result = vehicleController.GetAllVehicleType();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeNull();
            vehicleInterface.Verify(t => t.GetAllVehicleType(), Times.Once());
        }

        [Fact]
        public void GetAllVehicleType_ShouldReturnBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            vehicleInterface.Setup(t => t.GetAllVehicleType()).Throws(new Exception("Exception Caught"));

            // Act
            var result = vehicleController.GetAllVehicleType();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            vehicleInterface.Verify(t => t.GetAllVehicleType(), Times.Once());
        }
    }

}