using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.Users
{
    public class User:BaseEntity,IAggregateRoot
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public char Gender { get; set; }
        
        public int Age { get; set; }
        
        //meslek
        public string Profession { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }
    }
}