using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUser()
        {
            // var users = await _userRepository.GetUserAnsync();
            
            // return Ok(_mapper.Map<IEnumerable<MemberDto>>(users));
            return  Ok(await _userRepository.GetMemberAnsync());
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUserByName(string username)
        {
            // var user = await _userRepository.GetUserByUserNameAsync(username);
            // return _mapper.Map<MemberDto>(user);

            return await _userRepository.GetMemberByUserNameAsync(username);
        }

    }
}