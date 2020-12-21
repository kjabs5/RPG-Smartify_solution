using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RPG_Smartify.Data;
using RPG_Smartify.DTO.Character;
using RPG_Smartify.DTO.CharacterSkill;
using RPG_Smartify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RPG_Smartify.Service.CharacterSkill
{
    public class CharacterSkillService : ICharacterSkillRepo
    {

        private readonly RPGdbContext _context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper _imapper;
        public CharacterSkillService(IMapper imapper, RPGdbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _imapper = imapper;
            _context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseData<GetCharacterDTO>> AddCharacterSkill(characterskillDTO newcharacterskill)
        {
            ResponseData<GetCharacterDTO> res = new ResponseData<GetCharacterDTO>();
            try
            {
                character ch = _context.characters.Include(c=>c.weapon)
                    .Include(c=>c.characterSkills).ThenInclude(cs=>cs.skill)
                    .FirstOrDefault(c => c.Id == newcharacterskill.characterId
                && c.user.id == int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
           if(ch==null)
                {
                    res.success = false;
                    res.Message = "character not found";
                }
                Skill skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newcharacterskill.SkillId);
               if (skill==null)
                {
                    res.success = false;
                    res.Message = "skill not found";
                }
                characterSkill cs = new characterSkill()
                {
                    character = ch,
                    skill = skill

                };

                await _context.characterSkills.AddAsync(cs);
                await _context.SaveChangesAsync();

                res.Data = _imapper.Map<GetCharacterDTO>(ch);
            
            }
            catch(Exception ex)
            {
                res.success = false;
                res.Message = ex.Message;
            }
            return res;
        }
    }
}
