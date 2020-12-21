using RPG_Smartify.DTO.Character;
using RPG_Smartify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Smartify.Service.CharacterService
{
   public interface ICharacterService
    {

        Task<ResponseData<List<GetCharacterDTO>>> GetAllCharacters();

        Task<ResponseData<GetCharacterDTO>> GetCharacterById(int id);

        Task<ResponseData<List<GetCharacterDTO>>> AddNewCharacter(AddCharacterDTO newChar);

        Task<ResponseData<updateCharacterDTO>> UpdateCharacter(updateCharacterDTO updChar);

        Task<ResponseData<List<GetCharacterDTO>>> DeleteCharacter(int id);
    }
}
