namespace eGTS_Backend.Data.ViewModel
{
    public class FoodAndSupplimentUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public short Ammount { get; set; }
        public string UnitOfMesuament { get; set; } = null!;
        public double Calories { get; set; }
    }
}
