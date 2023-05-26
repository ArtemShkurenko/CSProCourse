using Logistic.Core.Services;
using Logistic.Models;
using Xunit;
using Moq;
using AutoFixture;

namespace Logistic.Core.Tests
{
    public class VehicleServiceTests
    {
        private readonly VehicleService _vehicleService;
        private readonly Mock<IRepository<Vehicle>> _repositoryMock;
        public VehicleServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Vehicle>>();
            _vehicleService = new VehicleService(_repositoryMock.Object);
        }
        [Fact]
        public void Create_CallsRepository_OnceCall()
        {
            //Arrange
            var vehicle = new Vehicle();
            //Act
            _vehicleService.Create(vehicle);
            //Assert
            _repositoryMock.Verify(x => x.Create(vehicle), Times.Once);
        }
        [Fact]
        public void GetById_CallsRepository_OnceCall()
        {
            //Arrange
            const int vehicleId = 1;
            //Act
            _vehicleService.GetById(vehicleId);
            //Assert
            _repositoryMock.Verify(x => x.GetRecordById(vehicleId), Times.Once);
        }
        [Fact]
        public void GetAll_CallsRepository_OnceCall()
        {
            //Act
            _vehicleService.GetAll();
            //Assert
            _repositoryMock.Verify(x => x.GetAll(), Times.Once);
        }
        [Fact]
        public void Delete_CallsRepository_OnceCall()
        {
            //Arrange
            const int vehicleId = 1;
            //Act
            _vehicleService.Delete(vehicleId);
            //Assert
            _repositoryMock.Verify(x => x.Delete(vehicleId), Times.Once);
        }
        [Fact]
        public void Update_CallsRepository_OnceCall()
        {
            //Arrange
            var vehicle = new Vehicle();
            //Act
            _vehicleService.Update(vehicle);
            //Assert
            _repositoryMock.Verify(x => x.Update(vehicle), Times.Once);
        }
        [Fact]
        public void LoadCargo_AddCargo_WhenCargoMeetLimits()
        {
            //Arrange
            var vehicle = new Vehicle
            {
                Id =1,
                MaxCargoVolume = 10,
                MaxCargoWeightKg = 100,
                Cargos = new List<Cargo>(),
            };
            _repositoryMock.Setup(x => x.GetRecordById(vehicle.Id)).Returns(vehicle);
            var cargo = new Cargo
            {
                Id = Guid.NewGuid(),
                Weight = 50,
                Volume = 2,
            };
            //Act
            _vehicleService.LoadCargo(cargo, vehicle.Id);
            //Assert
            Assert.Single(vehicle.Cargos);
            Assert.Equal(cargo, vehicle.Cargos.Single());
        }
        public void LoadCargo_AddCargo_WhenCaVehicleOverload()
        {
            //Arrange
            var vehicle = new Vehicle
            {
                Id = 1,
                MaxCargoVolume = 10,
                MaxCargoWeightKg = 100,
                Cargos = new List<Cargo>(),
            };
            _repositoryMock.Setup(x => x.GetRecordById(vehicle.Id)).Returns(vehicle);
            var cargo = new Cargo
            {
                Id = Guid.NewGuid(),
                Weight = 50,
                Volume = 20,
            };
            //Act+Assert
            Assert.Throws<Exception>(() => _vehicleService.LoadCargo(cargo, vehicle.Id));
        }
        [Fact]
        public void UnLoadCargo_RemoveCargo_NotIncludeCargo()
        {
            //Arrange
            var fixture = new Fixture();
            var vehicle = fixture.Create<Vehicle>();
            _repositoryMock.Setup(x => x.GetRecordById(vehicle.Id)).Returns(vehicle);
            var removeCargo = fixture.Create<Cargo>();
            vehicle.Cargos.Add(removeCargo);
            //Act
            _vehicleService.UnloadCargo(removeCargo.Id, vehicle.Id);
            //Assert
            Assert.DoesNotContain(removeCargo, vehicle.Cargos);
        }
    }
    
}