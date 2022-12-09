namespace VanderbiltFarms.Model
{
    public class Plot
    {
        public int? HomeownerId { get; set; }
        public int PlotId { get; set; }
        public int? StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public double? Acres { get; set; }
        public int? SquareFeet { get; set; }
        public int? Bedrooms { get; set; }
        public double? Bathrooms { get; set; }
        public string? Description { get; set; }
    }
}