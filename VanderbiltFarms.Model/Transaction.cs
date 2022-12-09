namespace VanderbiltFarms.Model
{
    public class Transaction
    {
        public int? TransactionID { get; set; }
        public int? FeeId { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
    }
}