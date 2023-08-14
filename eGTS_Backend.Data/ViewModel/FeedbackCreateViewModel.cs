namespace eGTS_Backend.Data.ViewModel
{
    public class FeedbackCreateViewModel
    {
        public Guid PtidorNeid { get; set; }
        public Guid PackageGymerId { get; set; }
        public short Rate { get; set; }
        public string Feedback1 { get; set; } = null!;
    }
}
