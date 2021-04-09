using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUserAnsync();
        Task<AppUser> GetUserByIdAnsync(int id);

        Task<AppUser> GetUserByUserNameAsync(string username);

        // Task<IEnumerable<MemberDto>> GetMembersAnsync();

        //Getting user per page
        Task<PagedList<MemberDto>> GetMembersAnsync(UserParams userParams);
        Task<MemberDto> GetMemberByUserNameAsync(string username);

    }
}