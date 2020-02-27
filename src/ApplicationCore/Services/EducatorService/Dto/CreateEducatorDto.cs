using Microsoft.AspNetCore.Http;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService.Dto
{
    public class CreateEducatorDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Profession { get; set; }
        //Kisa ozgecmis bilgileri
        public string Resume { get; set; }
        public IFormFile File { get; set; }

    }
}