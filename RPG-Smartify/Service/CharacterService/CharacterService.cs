using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RPG_Smartify.Data;
using RPG_Smartify.DTO.Character;
using RPG_Smartify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RPG_Smartify.Service.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly RPGdbContext _context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper _imapper;
        public CharacterService(IMapper imapper,RPGdbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _imapper = imapper;
            _context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

       
       
        private int getUserId()=>int.Parse( httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
       

        public async Task<ResponseData<List<GetCharacterDTO>>> AddNewCharacter(AddCharacterDTO newChar)
        {
            ResponseData<List<GetCharacterDTO>> response = new ResponseData<List<GetCharacterDTO>>();
            character c=_imapper.Map<character>(newChar);
            c.user = await _context.Users.FirstOrDefaultAsync(u => u.id == getUserId());
            await _context.characters.AddAsync(c);
            await _context.SaveChangesAsync();
            response.Data =await _context.characters.Where(c=>c.user.id==getUserId()).Select(c=> _imapper.Map<GetCharacterDTO>(c)).ToListAsync();
            return response;
        }

        public async Task<ResponseData<List<GetCharacterDTO>>> GetAllCharacters()
        {
            ResponseData<List<GetCharacterDTO>> response = new ResponseData<List<GetCharacterDTO>>();
            try
            {
                List<character> loc = await _context.characters.Where(c => c.user.id == getUserId()).ToListAsync();
                response.Data = loc.Select(c => _imapper.Map<GetCharacterDTO>(c)).ToList();
                
            }
            catch(Exception ex)
            {
                response.success = false;
                response.Message = ex.Message;
            }
            return response;
        
        }

        public async Task<ResponseData<GetCharacterDTO>> GetCharacterById(int id)
        {
            ResponseData<GetCharacterDTO> response = new ResponseData<GetCharacterDTO>();

            try
            {
                character ch = await _context.characters.
                    Include(c=>c.weapon)
                    .Include(c=>c.characterSkills)
                    .ThenInclude(cs=>cs.skill)
                    .FirstOrDefaultAsync(c => c.Id == id && c.user.id == getUserId());
                if (ch == null)
                {
                    response.success = false;
                    response.Message = "You may not have the charcter or you may not have authority to view the record";
                    //return response;
                }
                else
                {
                    response.Data = _imapper.Map<GetCharacterDTO>(ch);
                }


              //  return response;
            }
            catch(Exception ex)
            {

                response.success = false;
                response.Message = ex.Message;
               // return response;
            }
            return response;
        }

        public async Task<ResponseData<updateCharacterDTO>> UpdateCharacter(updateCharacterDTO updChar)
        {
            ResponseData<updateCharacterDTO> response = new ResponseData<updateCharacterDTO>();
            try {
                character c = await _context.characters.Include(c=>c.user).FirstOrDefaultAsync(x => x.Id == updChar.Id);
                if (c.user.id == getUserId())
                {
                    c.Name = updChar.Name;
                    c.Class = updChar.Class;
                    c.Defense = updChar.Defense;
                    c.HitPoints = updChar.HitPoints;
                    c.Intelligence = updChar.Intelligence;
                    c.Strength = updChar.Strength;

                    response.Data = _imapper.Map<updateCharacterDTO>(c);
                }
                else
                {
                    response.Message = "character not found";
                    response.success = false;
                }
                
            }
            catch(Exception ex)
            {
                response.success = false;
                response.Message = ex.Message;

            }

            return response;

        }

        public async Task<ResponseData<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            ResponseData<List<GetCharacterDTO>> response = new ResponseData<List<GetCharacterDTO>>();
            try
            {
                character c = await _context.characters.FirstAsync(x => x.Id ==id && x.user.id==getUserId());
              if(c!=null)
                {
                    _context.characters.Remove(c);
                    await _context.SaveChangesAsync();
                    response.Data = _context.characters.Where(c=>c.Id==getUserId()).Select(c => _imapper.Map<GetCharacterDTO>(c)).ToList();
                }
              else
                {
                    response.Message = "character not found";
                    response.success = false;
                }
             

            }
            catch (Exception ex)
            {
                response.success = false;
                response.Message = ex.Message;

            }

            return response;

        }
       
    }
}
