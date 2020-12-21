using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Smartify.DTO.Weapon
{
    public class AddWeaponDTO
    {
        public string Name { get; set; }

        public int Damage { get; set; }

        public int characterId { get; set; }
    }
}
