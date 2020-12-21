using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Smartify.Model
{
    public class characterSkill
    {
        public int characterId { get; set; }
        public character character { get; set;}

        public int SkillId { get; set; }
        public Skill skill { get; set; }
    }
}
