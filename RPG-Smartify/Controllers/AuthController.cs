using Microsoft.AspNetCore.Mvc;
using RPG_Smartify.Data;
using RPG_Smartify.DTO.Auth;
using RPG_Smartify.DTO.Register;
using RPG_Smartify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Smartify.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository irepo;

        public AuthController(IAuthRepository irepo)
        {
            this.irepo = irepo;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO user)
        {
           ResponseData<int> result=await irepo.Register(new User { Username = user.Username }, user.Password);
            if(!result.success)
            {
                return BadRequest(result);
            }
          
            return Ok(result);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            ResponseData<string> result = await irepo.Login(user.Username, user.Password);
            if (!result.success)
            {
                return BadRequest(result);
            }
            
            return Ok(result);

        }
    }
}
