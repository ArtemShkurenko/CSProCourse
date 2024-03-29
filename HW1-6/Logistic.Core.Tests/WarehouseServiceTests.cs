using Logistic.Core.Services;
using Logistic.Models;
using Xunit;
using Moq;
using AutoFixture;

namespace Logistic.Core.Tests
{
    public class WarehouseServiceTests
    {
        private readonly WarehouseService _warehouseService;
        private readonly Mock<IRepository<Warehouse>> _repositoryMock;
        public WarehouseServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Warehouse>>();
            _warehouseService = new WarehouseService(_repositoryMock.Object);
        }
        [Fact]
        public void Create_CallsRepository_OnceCall()
        {
            //Arrange
            var warehouse = new Warehouse();
            //Act
            _warehouseService.Create(warehouse);
            //Assert
            _repositoryMock.Verify(x => x.Create(warehouse), Times.Once);
        }
        [Fact]
        public void GetById_CallsRepository_OnceCall()
        {
            //Arrange
            const int warehouseId = 1;
            //Act
            _warehouseService.GetById(warehouseId);
            //Assert
            _repositoryMock.Verify(x => x.GetRecordById(warehouseId), Times.Once);
        }
        [Fact]
        public void GetAll_CallsRepository_OnceCall()
        {
            //Act
            _warehouseService.GetAll();
            //Assert
            _repositoryMock.Verify(x => x.GetAll(), Times.Once);
        }
        [Fact]
        public void Delete_CallsRepository_OnceCall()
        {
            //Arrange
            const int warehouseId = 1;
            //Act
            _warehouseService.Delete(warehouseId);
            //Assert
            _repositoryMock.Verify(x => x.Delete(warehouseId), Times.Once);
        }
        [Fact]
        public void Update_CallsRepository_OnceCall()
        {
            //Arrange
            var warehouse = new Warehouse();
            //Act
            _warehouseService.Update(warehouse);
            //Assert
            _repositoryMock.Verify(x => x.Update(warehouse), Times.Once);
        }
        [Fact]
        public void LoadCargo_AddCargo_WhenCargoAdd()
        {
            //Arrange
            var fixture = new Fixture();
            var warehouse = fixture.Create<Warehouse>();
            _repositoryMock.Setup(x => x.GetRecordById(warehouse.Id)).Returns(warehouse);
            var cargo = fixture.Create<Cargo>();
            //Act
            _warehouseService.LoadCargo(cargo, warehouse.Id);
            //Assert
            Assert.Contains(cargo, warehouse.Cargos);
        }
        [Fact]
        public void UnLoadCargo_RemoveCargo_NotIncludeCargo()
        {
            //Arrange
            var fixture = new Fixture();
            var warehouse = fixture.Create<Warehouse>();
            _repositoryMock.Setup(x => x.GetRecordById(warehouse.Id)).Returns(warehouse);
            var cargo = fixture.Create<Cargo>();
            warehouse.Cargos.Add(cargo);
            //Act
            _warehouseService.UnloadCargo(cargo.Id, warehouse.Id);
            //Assert
            Assert.DoesNotContain(cargo, warehouse.Cargos);
        }
    }
    
}