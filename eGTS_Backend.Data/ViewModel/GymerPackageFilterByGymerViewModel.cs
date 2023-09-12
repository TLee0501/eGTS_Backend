namespace eGTS_Backend.Data.ViewModel
{
    public class GymerPackageFilterByGymerViewModel
    {
        public Guid GymerId { get; set; }
        public string GymerName { get; set; }
        public List<GymerPackage> GymerPackages { get; set; }
    }

    public class GymerPackage
    {
        public Guid PackageGymerId { get; set; }
        public string PackageName { get; set; }
        public DateTime From { get; set; }
        public string Status { get; set; }
        public short? NumberOfSession { get; set; }
        public bool isUpdate { get; set; }
    }
}
