using Logistic.DAL.DataBase;
using Xunit;
using Logistic.Models;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Xml.Serialization;

namespace Logistics.DAL.Tests
{
    public class XmlFileRepositoryTests
    {
        private readonly XmlFileRepository<Vehicle> _xmlFileRepository;
        public XmlFileRepositoryTests()
        {
            _xmlFileRepository = new XmlFileRepository<Vehicle>();
        }
        [Fact]
        public void Read_WhenValidXml_DeserializeSuccesfully()
        {
            // Arrange
            var testFilePath = Path.Combine("Resource", "Xml_vehicle_test.xml");

            // Act
            var result = _xmlFileRepository.ReadRecords(testFilePath);
            //Assert
            Assert.Equal("ae1504", result[0].Name);
            Assert.Equal(10, result[0].MaxCargoVolume);
        }
        [Fact]
        public void Create_WhenValidJson_SerializeSuccessfully()
        {
            // Arrange
            var testPath = Path.Combine("Resource", "CreateTest", "json_vehicle_test.json");
            var entities = new List<Vehicle>()
            {
                new Vehicle {Name = "ae1401", MaxCargoVolume= 10, MaxCargoWeightKg = 100, Cargos = new List<Cargo>()},
                new Vehicle {Name = "ae1506", MaxCargoVolume= 12, MaxCargoWeightKg = 140, Cargos = new List<Cargo>()},
            };
            _xmlFileRepository.SaveRecords(entities, testPath);
            var result = _xmlFileRepository.ReadRecords(testPath);

            using (new AssertionScope())
            {
                result.Should().BeEquivalentTo(entities);
            }
        }
    }
}
