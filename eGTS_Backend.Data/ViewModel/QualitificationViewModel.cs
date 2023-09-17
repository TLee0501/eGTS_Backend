namespace eGTS_Backend.Data.ViewModel
{
    public class QualitificationViewModel
    {
        public Guid ExpertId { get; set; }
        public string? Certificate { get; set; }
        public string? Descrition { get; set; }
        public short? Experience { get; set; }
        public bool IsCetifide { get; set; }
        public bool IsDelete { get; set; }
    }
}
