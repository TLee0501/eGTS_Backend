namespace eGTS_Backend.Data.ViewModel
{
    public class MealCreateViewModel
    {
        public Guid PackageGymerID { get; set; }
        public List<Guid>? MonAnSang { get; set; }
        public List<Guid>? MonAnTrua { get; set; }
        public List<Guid>? MonAnToi { get; set; }
        public List<Guid>? MonAnTruocTap { get; set; }
        public DateTime FromDatetime { get; set; }
        public DateTime ToDatetime { get; set; }
    }
}
