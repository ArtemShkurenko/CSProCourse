using Logistic.DAL.DataBase;
using Xunit;
using Logistic.Models;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Logistics.DAL.Tests
{
    public class JsonFileRepositoryTests
    {
        private readonly JsonFileRepository<Vehicle> _jsonFileRepository;
        public JsonFileRepositoryTests()
        {
            _jsonFileRepository = new JsonFileRepository<Vehicle>();
        }
        [Fact]
        public void Read_WhenValidJson_DeserializeSuccesfully()
        {
            // Arrange
            var testFilePath = Path.Combine("Resource", "json_vehicle_test.json");
            // Act
            var result = _jsonFileRepository.ReadRecords(testFilePath);
            //Assert
            Assert.Equal("ae1506", result[0].Name);
            Assert.Equal(10, result[0].MaxCargoVolume);
        }
        [Fact]
        public void Create_WhenValidJson_SerializeSuccessfully()
        {
            // Arrange
            var testPath = Path.Combine("Resource", "CreateTest", "json_vehicle_test.json");
            var entities = new List<Vehicle>()
            {
                new Vehicle {Name = "ae1401", MaxCargoVolume= 10, MaxCargoWeightKg = 100},
                new Vehicle {Name = "ae1506", MaxCargoVolume= 12, MaxCargoWeightKg = 140},
            };
            _jsonFileRepository.SaveRecords(entities, testPath);
            var result = _jsonFileRepository.ReadRecords(testPath);

            using (new AssertionScope())
            {
                result.Should().BeEquivalentTo(entities);
            }
        }
    }
}
