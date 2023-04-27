using Logistic.DAL.DataBase;
using Xunit;
using Logistic.Models;
using AutoFixture;



namespace Logistics.DAL.Tests
{
    public class InMemoryRepositoryTests
    {
         private readonly InMemoryRepository<Vehicle> inMemoryRepository;
         public InMemoryRepositoryTests()
         {
             inMemoryRepository = new InMemoryRepository<Vehicle>();
         }
        [Fact]
        public void Create_WhenValidEntity_ExpectedResult()
        {
            //Act
            inMemoryRepository.Create(new Vehicle());
            var Id = inMemoryRepository._records[0].Id;
            //Asert
            Assert.Equal(1, Id);
        }

        [Fact]
        public void GetRecordById_EntityExists_ReturnsCopyOfEntity()
        {
            // Arrange
            var fixture = new Fixture();
            var vehicle = fixture.Create<Vehicle>();
            inMemoryRepository.Create(vehicle);
            var copyId = inMemoryRepository._records[0].Id;


            // Act
            var result = inMemoryRepository.GetRecordById(copyId);

            // Assert
            Assert.Equal(copyId, result.Id);
            Assert.Equal(vehicle.Name, result.Name);
            Assert.Equal(vehicle.MaxCargoWeightKg, result.MaxCargoWeightKg);
            Assert.Equal(vehicle.MaxCargoVolume, result.MaxCargoVolume);
        }
        [Fact]
        public void Delete_EntityExists_RemovesFromRecords()
        {
            // Arrange
            var vehicle = new Vehicle();
            inMemoryRepository.Create(vehicle);
            var copyId = inMemoryRepository._records[0].Id;

            // Act
            inMemoryRepository.Delete(copyId);

            // Assert
            Assert.DoesNotContain(vehicle, inMemoryRepository._records);
        }
        [Fact]
        public void Update_WhenValidEntity_UpdatesRecord()
        {
            // Arrange
            var fixture = new Fixture();
            var vehicle = fixture.Create<Vehicle>();
            inMemoryRepository.Create(vehicle);
            var newVehicle = fixture.Create<Vehicle>();
            // Act
            inMemoryRepository.Update(newVehicle);
            var result = inMemoryRepository.GetRecordById(newVehicle.Id);

            // Assert
            Assert.Equal(newVehicle.Id, result.Id);
            Assert.Equal(newVehicle.Name, result.Name);
            Assert.Equal(newVehicle.MaxCargoWeightKg, result.MaxCargoWeightKg);
            Assert.Equal(newVehicle.MaxCargoVolume, result.MaxCargoVolume);
        }

        [Fact]
        public void GetAll_ReturnsAllRecords()
        {
            // Arrange
            var fixture = new Fixture();
            var vehicle1 = fixture.Create<Vehicle>();
            var vehicle2 = fixture.Create<Vehicle>();
            inMemoryRepository.Create(vehicle1);
            inMemoryRepository.Create(vehicle2);

            // Act
            var result = inMemoryRepository.GetAll().ToList();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}
    
