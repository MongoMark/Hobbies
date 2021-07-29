using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Hobbies.Models
{
    public class User
    {
       [Key]
       public int UserId { get; set; }
       [Required]
       [Display(Name="First Name")]
       [MinLength(2, ErrorMessage="First Name must be at least 2 characters")]
       public string FirstName{ get; set; }
       [Required]
       [Display(Name="Last Name")]
       [MinLength(2, ErrorMessage="Last Name must be at least 2 characters")]
       public string LastName{ get; set; }
       [Required]
       [EmailAddress]
       public string Email { get; set; }
       [Required]
       [DataType(DataType.Password)]
       [MinLength(8, ErrorMessage="Password must be at least 8 characers")]
       public string Password { get; set; }

       public DateTime CreatedAt { get; set; } = DateTime.Now;
       public DateTime UpdatedAt { get; set; } = DateTime.Now;

       [NotMapped]
       [Compare("Password")]
       [DataType(DataType.Password)]
       public string Confirm { get; set; }

       public List<Association> Association {get;set;}

    }
    public class LoginUser
    {
        [EmailAddress]
        [Required]
        [Display(Name="Email")]
        public string Email {get;set;}
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string Password {get;set;}
        public User Creator {get;set;}
    }
}