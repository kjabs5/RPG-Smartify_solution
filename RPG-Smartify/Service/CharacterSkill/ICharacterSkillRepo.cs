using RPG_Smartify.DTO.Character;
using RPG_Smartify.DTO.CharacterSkill;
using RPG_Smartify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Smartify.Service.CharacterSkill
{
    public interface ICharacterSkillRepo
    {
        

        Task<ResponseData<GetCharacterDTO>> AddCharacterSkill(characterskillDTO newcharacterskill);
    }
}
