using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.Lib
{
    public class CheckEdition:ICheckEdition
    {
        private readonly IAsyncRepository<Tenant> _tenatRepository;
        private readonly IAsyncRepository<Educator> _educatorRepository;
        private readonly IAsyncRepository<GivenCourse> _givenCourseRepository;

        public CheckEdition(IAsyncRepository<Tenant> tenatRepository,
            IAsyncRepository<Educator> educatorRepository,
            IAsyncRepository<GivenCourse> givenCourseRepository
            )
        {
            _tenatRepository = tenatRepository;
            _educatorRepository = educatorRepository;
            _givenCourseRepository = givenCourseRepository;
        }
        
        public async Task<bool> HaveCreateCourseRight<T>(long[] id)
        {
            if (typeof(T) == typeof(Educator))
            {
                foreach (var entityId in id)
                {
                    var educator = await _educatorRepository.GetByIdAsync(entityId);
                    if (!educator.IsPremium)
                    {
                        var count = await _givenCourseRepository.GetAll().Where(x => x.EducatorId == entityId).CountAsync();
                        if (count >=2)
                        {
                            return false;
                        }
                        return true;
                    }
                    return true;
                }
            }

            if (typeof(T) == typeof(Tenant))
            {
                foreach (var entityId in id)
                {
                    var tenant = await _tenatRepository.GetByIdAsync(entityId);
                    if (!tenant.IsPremium)
                    {
                        var count = await _givenCourseRepository.GetAll().Where(x => x.TenantId == entityId).CountAsync();
                        if (count >=2)
                        {
                            return false;
                        }
                        return true;
                    }
                    return true;
                }
            }
           
            throw new Exception("Invalid Entity !");
        }
    }
}