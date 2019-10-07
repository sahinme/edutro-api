namespace Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService.Dto
{
    public class CreateEducatorDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Profession { get; set; }

        //Kisa ozgecmis bilgileri
        public string Resume { get; set; }

        public long? TenantId { get; set; }
    }
}