using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Notifications;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Users
{
    public class User:BaseEntity,IAggregateRoot
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public char Gender { get; set; }
        
        public int Age { get; set; }
        
        //meslek
        public string Profession { get; set; }

        [Required]
        [MinLength(6)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }
    }
    
}