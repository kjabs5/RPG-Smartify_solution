using AutoMapper;
using RPG_Smartify.DTO.Character;
using RPG_Smartify.DTO.Skill;
using RPG_Smartify.DTO.Weapon;
using RPG_Smartify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Smartify
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<character, GetCharacterDTO>()
                .ForMember(dto=>dto.skills, c=>c.MapFrom(c=>c.
                characterSkills.Select(cs=>cs.skill)));
            CreateMap<AddCharacterDTO, character>();
            CreateMap<character, updateCharacterDTO>();
            CreateMap<Weapon, GetWeaponDTO>();
            CreateMap<Skill, GetSkillDTO>();
        }
    }
}
