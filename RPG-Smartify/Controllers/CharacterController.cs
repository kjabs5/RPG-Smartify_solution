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
{   /// <summary>
/// Controller for characters
/// </summary>
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class CharacterController : Controller
    {
        private readonly ICharacterService icService;
        /// <summary>
        /// Constructor default
        /// </summary>
        /// <param name="icService"> the interface for Character Service</param>
        public CharacterController(ICharacterService icService)
        {
            this.icService = icService;
        }

        /// <summary>
        /// get all characters
        /// </summary>
        /// <returns></returns>

       // [AllowAnonymous]
        [HttpGet("get-all")]
        [ProducesResponseType(200, Type =typeof( List<GetCharacterDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
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
        /// <summary>
        /// Get all characters based on user and character id
        /// </summary>
        /// <param name="id">the id of the character</param>
        /// <returns></returns>
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
        /// <summary>
        /// add a new character
        /// </summary>
        /// <param name="newChar"></param>
        /// <returns></returns>
        /// 
        
        [ProducesResponseType(201, Type = typeof(List<GetCharacterDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesDefaultResponseType]
        [HttpPost]
        public async Task<IActionResult> addCharacter(AddCharacterDTO newChar)
        {
            var loc=await icService.AddNewCharacter(newChar);
            return Ok(loc);
        }

        /// <summary>
        /// update a character
        /// </summary>
        /// <param name="newChar">the new Character</param>
        /// <returns></returns>
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
        /// <summary>
        /// Delete a character
        /// </summary>
        /// <param name="id">the id of the character</param>
        /// <returns></returns>
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
