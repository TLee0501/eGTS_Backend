namespace eGTS_Backend.Data.ViewModel
{
    public class SessionResultCreateViewModel
    {
        public Guid SessionId { get; set; }
        public int CaloConsump { get; set; }
        public string Note { get; set; } = null!;
    }
}
