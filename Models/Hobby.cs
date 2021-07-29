using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hobbies.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyId {get;set;}
        [Required]
        [Display(Name="Hobby")]
        public string HobbyName {get;set;}
        [Required]
        public string Description {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        public List<Association> Associations {get;set;}
    }
}