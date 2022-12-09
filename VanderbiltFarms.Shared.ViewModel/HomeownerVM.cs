using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using VanderbiltFarms.Model;

namespace VanderbiltFarms.Shared.ViewModel
{
    public class HomeownerVM
    {
        public string? HomeownerID { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(25, ErrorMessage = "Name must be less than 25 characters.")]
        public string? Name { get; set; }

        [Required]
        [Phone(ErrorMessage = "Phone number must be 10 Digits.")]
        public string? Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email address must be real.")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Birthday must be a valid date")]
        public string? Birthday { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Description must be less than 250 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// Run this on a VM object and pass in same type of VM. Sets current object to parameter.
        /// </summary>
        public void Copy(HomeownerVM h)
        {
            this.HomeownerID = h.HomeownerID ?? "";
            this.Email = h.Email;
            this.Name = h.Name;
            this.Phone = h.Phone;
            this.Birthday = h.Birthday;
            this.Description = h.Description;
        }

        /// <summary>
        /// Run this on a VM object. Pass in model object. Sets current object to model.
        /// </summary>
        public void Map(Homeowner h)
        {
            this.HomeownerID = h.HomeownerId.ToString();
            this.Name = h.FullName;
            this.Description = h.Description;
            this.Birthday = h.Birthday?.ToShortDateString();
            this.Phone = Regex.Replace(h!.Phone, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
            this.Email = h.Email;
        }

        /// <summary>
        /// Run this on a VM object. Returns matching model object.
        /// </summary>
        public Homeowner MapOut()
        {
            var success = int.TryParse(this.HomeownerID, out int homeownerID);
            if (success)
            {
                return new Homeowner()
                {
                    HomeownerId = homeownerID,
                    FullName = this.Name,
                    Description = this.Description,
                    Birthday = DateTime.Parse(this.Birthday ?? ""),
                    Phone = this.Phone!.Replace("-", string.Empty),
                    Email = this.Email
                };
            }

            return new Homeowner()
            {
                FullName = this.Name,
                Description = this.Description,
                Birthday = DateTime.Parse(this.Birthday ?? ""),
                Phone = this.Phone!.Replace("-", string.Empty),
                Email = this.Email
            };
        }
    }
}
