namespace eGTS_Backend.Data.ViewModel
{
    public class SuspendCreateViewModel
    {
        public Guid PackageGymerId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Reason { get; set; } = null!;
    }
}
