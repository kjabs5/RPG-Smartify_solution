using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPG_Smartify.DTO.Character;
using RPG_Smartify.Model;
using RPG_Smartify.Service.CharacterService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RPG_Smartify.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class CharacterController : Controller
    {
        private readonly ICharacterService icService;

        public CharacterController(ICharacterService icService)
        {
            this.icService = icService;
        }



       // [AllowAnonymous]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            //  int id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = await icService.GetAllCharacters();
            if (result.Data == null)
            {
                return NotFound(result);
            }
            else
            {
                return Ok(result);
            }
            //return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           var res= await icService.GetCharacterById(id);
            if (res.Data == null)
            {

               
                return NotFound(res);
            }
            else
            {
                return Ok(res);
            }
           // return Ok(c);
        }

        [HttpPost]
        public async Task<IActionResult> addCharacter(AddCharacterDTO newChar)
        {
            var loc=await icService.AddNewCharacter(newChar);
            return Ok(loc);
        }

        [HttpPut]
        public async Task<IActionResult> updateCharacter(updateCharacterDTO newChar)
        {
            ResponseData<updateCharacterDTO> res = await icService.UpdateCharacter(newChar);
            if(res.Data==null)
            {
                return NotFound(res);
            }
            else
            {
                return Ok(res);
            }

           
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCharacter(int id)
        {
            ResponseData<List<GetCharacterDTO>> res = await icService.DeleteCharacter(id);
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
