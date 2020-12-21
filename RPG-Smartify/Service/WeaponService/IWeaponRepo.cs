using RPG_Smartify.DTO.Character;
using RPG_Smartify.DTO.Weapon;
using RPG_Smartify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Smartify.Service.WeaponService
{
    public interface IWeaponRepo
    {
        Task<ResponseData<GetCharacterDTO>> AddWeapon(AddWeaponDTO newWeapon);




    }
}
