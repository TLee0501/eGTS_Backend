namespace eGTS_Backend.Data.ViewModel
{
    public class ExScheduleBasicForGymerViewModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<SessionDateViewModel> sessions { get; set; }
    }
}
