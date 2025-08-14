using Microsoft.AspNetCore.Mvc;
using PinchPayments.ElevatorApi.Controllers;
using PinchPayments.ElevatorApi.DTOs;
using PinchPayments.ElevatorApi.Services;

namespace PinchPayments.ElevatorApi.Tests
{
    public class ElevatorControllerTests
    {
        private readonly ElevatorController _controller;

        public ElevatorControllerTests()
        {
            var service = new ElevatorService();
            _controller = new ElevatorController(service);
        }

        [Fact]
        public void PersonA_From_Ground_ToLevel5()
        {
            //Arrange
            int initialFloor = 0;
            int destinationFloor = 5;
            var request = new List<SummonRequest>
            {
                new SummonRequest(Name:"A", SourceLevel:0, DestinationLevel: destinationFloor)
            };

            //Act
            var result = _controller.GetElevatorRoute(initialFloor, request) as OkObjectResult;

            //Assert
            var route = Assert.IsAssignableFrom<List<ElevatorRouteStep>>(result.Value);

            var source = route.Where(r => r.Level == initialFloor).First();
            var destination = route.Where(x => x.Level == destinationFloor).First();

            Assert.Single(source.OnBoards);
            Assert.Empty(source.OffBoards);

            Assert.Single(destination.OffBoards);
            Assert.Empty(destination.OnBoards);
        }

        [Fact]
        public void PersonA_From_Level6_And_PersonB_From_Level4_Both_Go_Down_To_Level1()
        {
            //Arrange
            int initialFloor = 0;
            int personASourceFloor = 6;
            int personADestinationFloor = 1;
            int personBSourceFloor = 4;
            int personBDestinationFloor = 1;

            var request = new List<SummonRequest>
            {
                new SummonRequest(Name:"A", SourceLevel:personASourceFloor, DestinationLevel: personADestinationFloor),
                new SummonRequest(Name:"B", SourceLevel:personBSourceFloor, DestinationLevel: personBDestinationFloor)
            };

            //Act
            var result = _controller.GetElevatorRoute(initialFloor, request) as OkObjectResult;

            //Assert
            var route = Assert.IsAssignableFrom<List<ElevatorRouteStep>>(result.Value);

            Assert.Equal(4, route[0].Level);
            Assert.Equal(6, route[1].Level);
            Assert.Equal(1, route[2].Level);
        }

        [Fact]
        public void PersonA_From_Level2_To_Level6_PersonB_From_Level4_To_Level0()
        {
            //Arrange
            int initialFloor = 0;
            int personASourceFloor = 2;
            int personADestinationFloor = 6;
            int personBSourceFloor = 4;
            int personBDestinationFloor = 0;

            var request = new List<SummonRequest>
            {
                new SummonRequest(Name:"A", SourceLevel:personASourceFloor, DestinationLevel: personADestinationFloor),
                new SummonRequest(Name:"B", SourceLevel:personBSourceFloor, DestinationLevel: personBDestinationFloor)
            };

            //Act
            var result = _controller.GetElevatorRoute(initialFloor, request) as OkObjectResult;

            //Assert
            var route = Assert.IsAssignableFrom<List<ElevatorRouteStep>>(result.Value);

            var offBoards = route.Where(x => x.OffBoards.Count > 0).ToList();
            var onBoards = route.Where(x => x.OnBoards.Count > 0).ToList();

            Assert.Equal(2, offBoards.Count);
            Assert.Equal(2, onBoards.Count);
        }
    }
}
