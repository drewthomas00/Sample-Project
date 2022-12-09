using System.ComponentModel.DataAnnotations;
using VanderbiltFarms.Model;

namespace VanderbiltFarms.Shared.ViewModel
{
    public class TransactionVM
    {
        public string? TransactionID { get; set; }
        [Required]
        public string? FeeID { get; set; }

        [Required]
        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date, ErrorMessage = "Transaction date must be a valid date")]
        public string? TransactionDate { get; set; }

        [Required]
        [Display(Name = "Paid Amount")]
        public string? PaidAmt { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Description must be less than 200 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// Run this on a VM object and pass in transaction type of VM. Sets current object to parameter.
        /// </summary>
        public void Copy(TransactionVM t)
        {
            this.TransactionID = t.TransactionID ?? "";
            this.FeeID = t.FeeID;
            this.TransactionDate = t.TransactionDate;
            this.PaidAmt = t.PaidAmt;
            this.Description = t.Description;
        }

        /// <summary>
        /// Run this on a VM object and pass in same type of VM. Sets current object to parameter.
        /// </summary>
        public void Copy(FeeVM f)
        {
            this.FeeID = f.FeeID ?? "";
            this.TransactionDate = DateTime.Now.ToString();
            this.PaidAmt = f.FeeAmt;
            this.Description = (DateTime.Parse(f.DueDate) > DateTime.Now) ? "Paid on Time" : "Paid Late";
        }

        /// <summary>
        /// Run this on a VM object. Pass in model object. Sets current object to model.
        /// </summary>
        public void Map(Transaction t)
        {
            this.TransactionID = t.TransactionID.ToString();
            this.FeeID = t.FeeId.ToString();
            this.TransactionDate = t.TransactionDate.ToString();
            this.PaidAmt = t.Amount.ToString();
            this.Description = t.Description;
        }

        /// <summary>
        /// Run this on a VM object. Returns matching model object.
        /// </summary>
        public Transaction MapOut()
        {
            int transactionID;
            var success = int.TryParse(this.TransactionID, out transactionID);
            if (success)
                return new Transaction()
                {
                    TransactionID = transactionID,
                    FeeId = int.Parse(this.FeeID ?? ""),
                    TransactionDate = DateTime.Parse(this.TransactionDate ?? ""),
                    Amount = double.Parse(this.PaidAmt ?? ""),
                    Description = this.Description
                };
            return new Transaction()
            {
                FeeId = int.Parse(this.FeeID ?? ""),
                TransactionDate = DateTime.Parse(this.TransactionDate ?? ""),
                Amount = double.Parse(this.PaidAmt ?? ""),
                Description = this.Description
            };
        }
    }
}
