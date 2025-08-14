using PinchPayments.ElevatorApi.DTOs;

namespace PinchPayments.ElevatorApi.Services
{
    public class ElevatorService
    {
        public List<ElevatorRouteStep> GetElevatorRoute(int initialLevel, List<SummonRequest> requests)
        {
            // initial setup 
            var elevatorRouteStep = new List<ElevatorRouteStep>();
            var currentLevel = initialLevel;
            var remainingOnBoards = requests.ToList();
            var remainingOffBoards = new List<SummonRequest>();


            while (remainingOnBoards.Any() || remainingOffBoards.Any())
            {
                int? nextFloor = null;
                SummonRequest? targetSummon = null;
                bool isOnBoard = true;

                if (remainingOnBoards.Any())
                {
                    // Find the nearest on-board summon
                    targetSummon = remainingOnBoards.OrderBy(rob => Math.Abs(rob.SourceLevel - currentLevel)).First();
                    nextFloor = targetSummon.SourceLevel;
                    isOnBoard = true;
                }

                if (remainingOffBoards.Any())
                {
                    // Find the nearest off-board summon
                    var nearestOffBoard = remainingOffBoards.OrderBy(rob => Math.Abs(rob.DestinationLevel - currentLevel)).First();

                    // If the next on-board is further than the nearest off-board, switch to off-board
                    if (nextFloor == null || Math.Abs(nearestOffBoard.DestinationLevel - currentLevel) < Math.Abs(nextFloor.Value - currentLevel))
                    {
                        targetSummon = nearestOffBoard;
                        nextFloor = targetSummon.DestinationLevel;
                        isOnBoard = false;
                    }
                }

                // If no next floor is determined, break the loop
                currentLevel = nextFloor.HasValue ? nextFloor.Value : currentLevel;

                var step = new ElevatorRouteStep { Level = currentLevel };

                if (isOnBoard)
                {
                    // Handle on-boarding
                    var onBoardLevels = remainingOnBoards.Where(rob => rob.SourceLevel == currentLevel).ToList();
                    foreach (var obl in onBoardLevels)
                    {
                        step.OnBoards.Add(obl.Name);
                        remainingOffBoards.Add(obl);
                        remainingOnBoards.Remove(obl);
                    }
                }
                else
                {
                    // Handle off-boarding
                    var offBoardLevels = remainingOffBoards.Where(rob => rob.DestinationLevel == currentLevel).ToList();
                    foreach (var obl in offBoardLevels)
                    {
                        step.OffBoards.Add(obl.Name);
                        remainingOffBoards.Remove(obl);
                    }
                }

                // Add the step to the route
                elevatorRouteStep.Add(step);
            }
            return elevatorRouteStep;
        }
    }
}
