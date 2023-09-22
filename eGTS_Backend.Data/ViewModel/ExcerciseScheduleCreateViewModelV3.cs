namespace eGTS_Backend.Data.ViewModel
{
    public class ExcerciseScheduleCreateViewModelV3
    {
        public Guid PackageGymerID { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<DateTime> listSession { get; set; }
    }
}
