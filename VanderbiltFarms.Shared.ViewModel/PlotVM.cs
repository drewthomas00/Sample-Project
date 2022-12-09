using System.ComponentModel.DataAnnotations;
using VanderbiltFarms.Model;

namespace VanderbiltFarms.Shared.ViewModel
{
    public class PlotVM
    {
        public string? PlotID { get; set; }

        [Required]
        [Display(Name = "Street Number")]
        [Range(0, 999999, ErrorMessage = "Street number must be a valid digit between 0 and 999999.")]
        public string? StreetNumber { get; set; }

        [Required]
        [Display(Name = "Street Name")]
        [StringLength(100, ErrorMessage = "Street name must be less than 100 characters.")]
        public string? StreetName { get; set; }

        [Required]
        [Display(Name = "Bedrooms")]
        public string? NumberOfBedrooms { get; set; }

        [Required]
        [Display(Name = "Bathrooms")]
        public string? NumberOfBathrooms { get; set; }

        [Required]
        [Display(Name = "Acres")]
        public string? PlotSize { get; set; }

        [Required]
        [Display(Name = "SQFT")]
        [Range(0,10000, ErrorMessage = "Square Feet must be between 0 and 10000.")]
        public string? SquareFeet { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Description must be less than 200 characters.")]
        public string? Description { get; set; }

        public string? HomeownerID { get; set; }

        [Display(Name = "Owner")]
        public string? HomeownerName { get; set; }

        [Display(Name = "Address")]
        public string FullAddress
        {
            get
            {
                return StreetNumber + " " + StreetName;
            }
        }

        /// <summary>
        /// Run this on a VM object and pass in same type of VM. Sets current object to parameter.
        /// </summary>
        public void Copy(PlotVM p)
        {
            this.PlotID = p.PlotID ?? "";
            this.StreetNumber = p.StreetNumber;
            this.StreetName = p.StreetName;
            this.NumberOfBedrooms = p.NumberOfBedrooms;
            this.NumberOfBathrooms = p.NumberOfBathrooms;
            this.PlotSize = p.PlotSize;
            this.Description = p.Description;
            this.SquareFeet = p.SquareFeet;
            this.HomeownerID = p.HomeownerID;
        }

        /// <summary>
        /// Run this on a VM object. Pass in model object. Sets current object to model.
        /// </summary>
        public void Map(Plot p)
        {
            this.PlotID = p.PlotId.ToString();
            this.HomeownerID = p.HomeownerId.ToString();
            this.PlotSize = p.Acres.ToString();
            this.StreetNumber = p.StreetNumber.ToString();
            this.StreetName = p.StreetName;
            this.SquareFeet = p.SquareFeet.ToString();
            this.Description = p.Description;
            this.NumberOfBathrooms = p.Bathrooms.ToString();
            this.NumberOfBedrooms = p.Bedrooms.ToString();

        }

        /// <summary>
        /// Run this on a VM object. Returns matching model object.
        /// </summary>
        public Plot MapOut()
        {
            int plotID;
            var success = int.TryParse(this.PlotID, out plotID);
            if (success)
                return new Plot()
                {
                    PlotId = plotID,
                    StreetName = this.StreetName,
                    StreetNumber = int.Parse(this.StreetNumber ?? ""),
                    SquareFeet = int.Parse(this.SquareFeet ?? ""),
                    Acres = double.Parse(this.PlotSize ?? ""),
                    Bathrooms = double.Parse(this.NumberOfBathrooms ?? ""),
                    Bedrooms = int.Parse(this.NumberOfBedrooms ?? ""),
                    Description = this.Description
                };
            return new Plot()
            {
                StreetName = this.StreetName,
                StreetNumber = int.Parse(this.StreetNumber ?? ""),
                SquareFeet = int.Parse(this.SquareFeet ?? ""),
                Acres = double.Parse(this.PlotSize ?? ""),
                Bathrooms = double.Parse(this.NumberOfBathrooms ?? ""),
                Bedrooms = int.Parse(this.NumberOfBedrooms ?? ""),
                Description = this.Description
            };
        }
    }
}
