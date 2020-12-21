using Microsoft.EntityFrameworkCore;
using RPG_Smartify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Smartify.Data
{
    public class RPGdbContext : DbContext
    {
        public RPGdbContext(DbContextOptions<RPGdbContext> options) : base(options)
        {

        }

        public DbSet<character> characters { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Weapon> Weapons { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<characterSkill> characterSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<characterSkill>().HasKey(cs => new { cs.characterId, cs.SkillId });
        }

    }
}
