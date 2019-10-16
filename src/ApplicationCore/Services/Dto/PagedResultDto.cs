using System.Collections.Generic;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.Dto
{
    public class PagedResultDto<T>
    {
        public IList<T> Results { get; set; }
        public long Count { get; set; }
    }
}