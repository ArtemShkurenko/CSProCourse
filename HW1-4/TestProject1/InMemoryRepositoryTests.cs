using Logistic.DAL.DataBase;
using Xunit;
using Logistic.Models;



namespace Logistic.DAL.Tests
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
            var vehicle = new Vehicle()
            {
                MaxCargoVolume = 10,
                MaxCargoWeightKg = 100,
                Name = "AE5608",
            };
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
            var vehicle = new Vehicle()
            {
                MaxCargoVolume = 10,
                MaxCargoWeightKg = 100,
                Name = "AE1506"
            };
            inMemoryRepository.Create(vehicle);
            var newVehicle = new Vehicle()
            {
                Id = vehicle.Id,
                MaxCargoVolume = 20,
                MaxCargoWeightKg = 200,
                Name = "AE1507"
            };

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
            var vehicle1 = new Vehicle()
            {
                MaxCargoVolume = 10,
                MaxCargoWeightKg = 100,
                Name = "AE1506"
            };
            var vehicle2 = new Vehicle()
            {
                MaxCargoVolume = 20,
                MaxCargoWeightKg = 200,
                Name = "AE1507"
            };
            inMemoryRepository.Create(vehicle1);
            inMemoryRepository.Create(vehicle2);

            // Act
            var result = inMemoryRepository.GetAll().ToList();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}
    
