namespace eGTS_Backend.Data.ViewModel
{
    public class SessionResultViewModel
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public int CaloConsump { get; set; }
        public string Note { get; set; } = null!;
        public bool IsDelete { get; set; }

    }
}
