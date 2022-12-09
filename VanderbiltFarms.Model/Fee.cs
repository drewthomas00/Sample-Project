namespace VanderbiltFarms.Model
{
    public class Fee
    {
        public int PlotId { get; set; }
        public int FeeId { get; set; }
        public double? Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Description { get; set; }
    }
}