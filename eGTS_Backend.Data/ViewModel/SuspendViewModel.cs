namespace eGTS_Backend.Data.ViewModel
{
    public class SuspendViewModel
    {
        public Guid Id { get; set; }
        public Guid PackageGymerId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Reson { get; set; } = null!;
        public bool IsDelete { get; set; }
    }
}
