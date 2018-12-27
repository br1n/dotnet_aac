using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace ActivityCenter.Models
{
    public class MainUser
    {
        [Key]
        public int UserId {get; set;}

        [Required(ErrorMessage="First Name is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "First Name must be alphabetical characters only")]
        [MinLength(2, ErrorMessage="First Name field must be a minimum length of 2 characters long.")]
        [Display(Name="First")]
        public string FirstName {get; set;}

        [Required(ErrorMessage="Last name is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Last Name must be alphabetical characters only")]
        [MinLength(2, ErrorMessage="Last Name field must be a minimum length of 2 characters long.")]
        [Display(Name="Last Name")]
        public string LastName {get; set;}

        [Required(ErrorMessage="Email is required")]
        [EmailAddress]
        [Display(Name="Email")]
        public string Email {get; set;}

        [Required(ErrorMessage="Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$", ErrorMessage = "Password must have at least 1 letter, 1 number and 1 special character")]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string Password {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;

        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        [Required(ErrorMessage="Confirm Password is required")]
        [Display(Name="Confirm Password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string ConfirmPassword {get; set;}
        public List<Activity> ActivitiesCreated {get; set;}
        public List<JoinActivity> JoinedActivities {get; set;}
        public MainUser()
        {
            JoinedActivities = new List<JoinActivity>();
        }

    }

    public class LoginUser
    {
        [EmailAddress]
        [Required]
        [Display(Name="Email")]
        public string EmailAttempt {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string PasswordAttempt {get; set;}
    }
}