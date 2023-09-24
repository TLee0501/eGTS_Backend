namespace eGTS_Backend.Data.ViewModel
{
    public class RequestCreateViewModel
    {
        public Guid GymerId { get; set; }
        public Guid ReceiverId { get; set; }
        public Guid? PackageGymerId { get; set; }
        public bool IsPt { get; set; }
    }
}
