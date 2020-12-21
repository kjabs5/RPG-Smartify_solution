using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPG_Smartify.DTO.Weapon;
using RPG_Smartify.Model;
using RPG_Smartify.Service.WeaponService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Smartify.Controllers
{/// <summary>
/// 
/// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponRepo repo;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        public WeaponController(IWeaponRepo repo)
        {
            this.repo = repo;
        }
        /// <summary>
        /// Add a weapon by character
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddWeapon(AddWeaponDTO weapon)
        {

           var res=await repo.AddWeapon(weapon); 
            return Ok(res);
        }
    }
}
