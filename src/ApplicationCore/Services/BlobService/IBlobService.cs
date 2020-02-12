using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.BlobService
{
    public interface IBlobService
    {
        Task<string> InsertFile(IFormFile asset);
    }
}