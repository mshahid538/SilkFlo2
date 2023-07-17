using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.ViewModels.Shop.Demo
{
    public class Modal
    {
        #region Company Name
        [Required]
        [StringLength(100,
                      ErrorMessage = "Company Name must be between 1 and 100 characters in length.")]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        public string CompanyName_ErrorMessage { get; set; } = "Required";
        public bool CompanyName_IsInValid { get; set; }
        #endregion


        #region Email Address
        [Required]
        [StringLength(256,
              ErrorMessage = "Email Address must be between 1 and 256 characters in length.")]
        [DisplayName("Email Address")]

        public string Email { get; set; }
        public string Email_ErrorMessage { get; set; } = "Required";
        public bool Email_IsInValid { get; set; }
        #endregion


        #region First Name
        [Required]
        [StringLength(100,
              ErrorMessage = "First Name must be between 1 and 100 characters in length.")]
        [DisplayName("First Name")]

        public string FirstName { get; set; }

        public string FirstName_ErrorMessage { get; set; } = "Required";
        public bool FirstName_IsInValid { get; set; }
        #endregion



        #region Last Name
        [Required]
        [StringLength(100,
                      ErrorMessage = "Last Name must be between 1 and 100 characters in length.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string LastName_ErrorMessage { get; set; } = "Required";
        public bool LastName_IsInValid { get; set; }
        #endregion



        #region Phone Number
        [Required]
        [StringLength(50,
                      ErrorMessage = "Phone Number must be between 1 and 50 characters in length.")]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        public string PhoneNumber_ErrorMessage { get; set; } = "Required";
        public bool PhoneNumber_IsInValid { get; set; }
        #endregion


        #region Job Title
        [Required]
        [StringLength(100,
                      ErrorMessage = "Job Title must be between 1 and 100 characters in length.")]
        [DisplayName("Job Title")]
        public string JobTitle { get; set; }

        public string JobTitle_ErrorMessage { get; set; } = "Required";
        public bool JobTitle_IsInValid { get; set; }
        #endregion
    }
}