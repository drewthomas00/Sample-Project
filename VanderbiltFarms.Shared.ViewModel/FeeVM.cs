using System.ComponentModel.DataAnnotations;
using VanderbiltFarms.Model;

namespace VanderbiltFarms.Shared.ViewModel
{
    public class FeeVM
    {
        public string? FeeID { get; set; }

        public string? PlotID { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(200, ErrorMessage = "Name must be less than 200 characters.")]
        public string? FeeDesc { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency, ErrorMessage = "Fee Amount must be a valid date")]
        public string? FeeAmt { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        [DataType(DataType.Date, ErrorMessage = "Due date must be a valid date")]
        public string? DueDate { get; set; }

        /// <summary>
        /// Run this on a VM object and pass in same type of VM. Sets current object to parameter.
        /// </summary>
        public void Copy(FeeVM f)
        {
            this.FeeID = f.FeeID ?? "";
            this.FeeDesc = f.FeeDesc;
            this.FeeAmt = f.FeeAmt;
            this.DueDate = f.DueDate;
            this.PlotID = f.PlotID;
        }

        /// <summary>
        /// Run this on a VM object. Pass in model object. Sets current object to model.
        /// </summary>
        public void Map(Fee f)
        {
            this.FeeID = f.FeeId.ToString();
            this.FeeAmt = f.Amount.ToString();
            this.PlotID = f.PlotId.ToString();
            this.DueDate = f.DueDate?.ToShortDateString();
            this.FeeDesc = f.Description;
        }

        /// <summary>
        /// Run this on a VM object. Returns matching model object.
        /// </summary>
        public Fee MapOut()
        {
            int feeID;
            var success = int.TryParse(this.FeeID, out feeID);
            if(success)
                return new Fee()
                {
                    FeeId = feeID,
                    PlotId = int.Parse(this.PlotID ?? ""),
                    Amount = double.Parse(this.FeeAmt ?? ""),
                    Description = this.FeeDesc,
                    DueDate = DateTime.Parse(this.DueDate ?? "")
                };
            return new Fee()
            {
                PlotId = int.Parse(this.PlotID ?? ""),
                Amount = double.Parse(this.FeeAmt ?? ""),
                Description = this.FeeDesc,
                DueDate = DateTime.Parse(this.DueDate ?? "")
            };
        }
    }
}
