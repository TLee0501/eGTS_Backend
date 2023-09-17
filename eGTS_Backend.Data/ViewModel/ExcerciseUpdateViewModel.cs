namespace eGTS_Backend.Data.ViewModel
{
    public class ExcerciseUpdateViewModel
    {

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Video { get; set; }
        public bool IsDelete { get; set; }
        public int CalorieCumsumption { get; set; }
        public int RepTime { get; set; }
        public string UnitOfMeasurement { get; set; } = null!;
    }
}
