using RPG_Smartify.DTO.Skill;
using RPG_Smartify.DTO.Weapon;
using RPG_Smartify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Smartify.DTO.Character
{
    public class GetCharacterDTO
    {

        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;

        public GetWeaponDTO Weapon { get; set; }

        public List<GetSkillDTO> skills { get; set; }

    }
}
