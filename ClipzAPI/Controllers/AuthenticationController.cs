using AutoMapper;
using ClipzAPI.ActionFilters;
using ClipzAPI.Contracts;
using ClipzAPI.DataTransferObjects;
using ClipzAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClipzAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthenticationManager _authManager;
        public AuthenticationController(IMapper mapper, UserManager<AspNetUsers> userManager, IAuthenticationManager authManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {

            var user = _mapper.Map<AspNetUsers>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRoleAsync(user, "USER");
            return StatusCode(201, user);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if(!await _authManager.ValidateUser(user))
            {
                return Unauthorized();
            }

            return Ok(new { Token = await _authManager.CreateToken() });
        }
    }
}
