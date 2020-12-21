using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPG_Smartify.DTO.CharacterSkill;
using RPG_Smartify.Service.CharacterSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Smartify.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class CharacterSkillController : ControllerBase
    {
        private readonly ICharacterSkillRepo icservice;

        public CharacterSkillController(ICharacterSkillRepo icservice)
        {
            this.icservice = icservice;
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacterSkill(characterskillDTO characterskillDTO)
        {
            var res=await icservice.AddCharacterSkill(characterskillDTO);
            if (res.Data == null)
            {


                return NotFound(res);
            }
            else
            {
                return Ok(res);
            }
        }
     }
}
