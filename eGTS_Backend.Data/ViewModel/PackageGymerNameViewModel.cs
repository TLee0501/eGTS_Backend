namespace eGTS_Backend.Data.ViewModel
{
    public class PackageGymerNameViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid GymerId { get; set; }
        public Guid? Ptid { get; set; }
        public string? PTName { get; set; }
        public Guid? Neid { get; set; }
        public string? NEName { get; set; }
        public short? NumberOfSession { get; set; }
        public short? NumberOfMonth { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Status { get; set; } = null!;
        public bool isDelete { get; set; }
        public bool hasBodyParameter { get; set; }
    }
}
