using Logistic.DAL.DataBase;
using Xunit;
using Logistic.Models;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Xml.Serialization;
using AutoFixture;

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
            var testPath = Path.Combine("Resource", "CreateTest", "json_vehicle_test.xml");
            var fixture = new Fixture();
            var entities = fixture.Create<List<Vehicle>>();
            _xmlFileRepository.SaveRecords(entities, testPath);
            var result = _xmlFileRepository.ReadRecords(testPath);

            using (new AssertionScope())
            {
                result.Should().BeEquivalentTo(entities);
            }
        }
    }
}
