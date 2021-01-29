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
{/// <summary>
/// Auth Controller
/// </summary>
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : ControllerBase
    {

        //dependency injection
        private readonly IAuthRepository irepo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="irepo"></param>
        public AuthController(IAuthRepository irepo)
        {
            this.irepo = irepo;
        }

        /// <summary>
        /// Register new User
        /// </summary>
        /// <param name="user">user values</param>
        /// <returns></returns>
        [HttpPost("Register")]
        [ProducesResponseType(201, Type = typeof(int))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register(UserRegisterDTO user)
        {
           ResponseData<int> result=await irepo.Register(new User { Username = user.Username }, user.Password);
            if(!result.success)
            {
                return BadRequest(result);
            }
          
            return Ok(result);

        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="user">user values</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesDefaultResponseType]
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
