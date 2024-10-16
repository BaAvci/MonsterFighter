using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFighter
{
    public class Ork : Monster
    {
        public Ork(string name) : base(name) 
        {
            AttackMultiplier = 2;
        }

        public Ork(string name, float attackMultiplier) : base(name, attackMultiplier)
        {
        }

        public Ork(string name, int statPoints, char strongStat) : base(name, statPoints, strongStat)
        {
        }

        public override void Attack(List<Monster> enemies)
        {
            base.Attack(enemies);
            SpecialAttackCooldown = 0;
        }

    }
}
