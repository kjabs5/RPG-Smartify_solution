using AutoMapper;
using Microsoft.AspNetCore.Http;
using RPG_Smartify.Data;
using RPG_Smartify.DTO.Character;
using RPG_Smartify.DTO.Weapon;
using RPG_Smartify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RPG_Smartify.Service.WeaponService
{
    public class WeaponService : IWeaponRepo
    {
        private readonly RPGdbContext _context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public WeaponService(RPGdbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }
        public async Task<ResponseData<GetCharacterDTO>> AddWeapon(AddWeaponDTO newWeapon)
        {
            ResponseData<GetCharacterDTO> res = new ResponseData<GetCharacterDTO>();
            try
            {
                character character = _context.characters.FirstOrDefault(c => c.Id == newWeapon.characterId && c.user.id == int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if(character == null)
                {
                    res.Message = "charcater not found";
                    res.success = false;
                }
                else
                {
                    Weapon weapon = new Weapon
                    {
                        Name = newWeapon.Name,
                        Damage = newWeapon.Damage,
                        characterId = newWeapon.characterId
                    };

                   await _context.Weapons.AddAsync(weapon);
                   await _context.SaveChangesAsync();
                    res.Data = mapper.Map<GetCharacterDTO>(character);

                }
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
