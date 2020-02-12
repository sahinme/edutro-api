using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Locations
{
    public class Location:BaseEntity,IAggregateRoot
    {
        public string Name { get; set; }
    }
}