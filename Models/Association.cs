using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Hobbies.Models
{
    public class Association
    {
        public int AssociationId {get;set;}
        public int UserId {get;set;}
        public int HobbyId {get;set;}
        public User User {get;set;}
        public Hobby Hobby {get;set;}

    }
}