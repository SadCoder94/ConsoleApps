namespace RideSharing.Models
{
    public class Ride
    {
        public string RideId { get; set; }
        public string DriverId { get; set; }
        public string RiderId { get; set; }
        public double? Amount { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int? StopX { get; set; }
        public int? StopY { get; set; }

        public bool RideIsComplete() => Amount.HasValue && StopX.HasValue && StopY.HasValue;

    }
}
