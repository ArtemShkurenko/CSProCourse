﻿using Logistic.Core.Services;
using Logistic.Models;
using Xunit;
using Moq;
using Logistic.DAL.DataBase;

namespace Logistic.Core.Tests
{
    public class ReportServiceTests
    {
        private readonly string _testreportDir = Path.Combine(Directory.GetCurrentDirectory(), "Reports");
        [Fact]
        public void CreateReport_ValidReportTypeAndEntity_GetReport()
        {
           //Arrange
            var mockXmlReporitory = new Mock<IReportRepository<Vehicle>>();
            var mockJsonRepository = new Mock<IReportRepository<Vehicle>>();
            var reportService = new ReportService<Vehicle>(mockXmlReporitory.Object, mockJsonRepository.Object);
            var vehicles = new List<Vehicle>
            {
                new Vehicle{ Id = 1, MaxCargoVolume = 10, MaxCargoWeightKg = 50 }
            };
            var filePath = Path.Combine(_testreportDir, $"Vehicle-{DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss")}.json");
            //Act
            reportService.CreateReport(ReportType.Json, vehicles);
            //Assert
            mockJsonRepository.Verify(repo => repo.SaveRecords(It.IsAny<IEnumerable<IRecord>>(), filePath), Times.Once);
        }
        [Fact]
        public void LoadReport_ValidFileName_ReturnListEntity()
        {
            //Arrange
            var mockXmlReporitory = new Mock<IReportRepository<Vehicle>>();
            var mockJsonRepository = new Mock<IReportRepository<Vehicle>>();
            var reportService = new ReportService<Vehicle>(mockXmlReporitory.Object, mockJsonRepository.Object);
            var vehicles = new List<Vehicle>
            {
                new Vehicle{ Id = 1, MaxCargoVolume = 10, MaxCargoWeightKg = 50 }
            };
            var fileName = "Json_vehicle_test.json";
            var filePath = Path.Combine(_testreportDir, fileName);
            var expectedEntity = vehicles;

            //Act
            mockJsonRepository.Setup(repo => repo.ReadRecords(filePath)).Returns(expectedEntity);
            var result = reportService.LoadReport(fileName);

            //Assert
            Assert.Equal(expectedEntity, result);



        }
    }
}
