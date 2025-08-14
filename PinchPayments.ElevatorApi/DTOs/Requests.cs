namespace PinchPayments.ElevatorApi.DTOs
{
    public record SummonRequest(string Name, int SourceLevel, int DestinationLevel);
    public class ElevatorRouteStep
    {
        public int Level { get; set; }
        public List<string> OnBoards { get; set; } = new();
        public List<string> OffBoards { get; set; } = new();
    }
}
