using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser() { }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //[Required]
        //[Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        public virtual ICollection<Survey> Surveys { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
