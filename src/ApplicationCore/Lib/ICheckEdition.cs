using System.Threading.Tasks;

namespace Microsoft.EgitimAPI.Lib
{
    public interface ICheckEdition
    {
        Task<bool> HaveCreateCourseRight<T>(long[] id);
    }
}