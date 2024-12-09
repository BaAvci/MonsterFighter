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
        }

        public Ork(string name, float attackMultiplier) : base(name, attackMultiplier)
        {
        }

        public Ork(string name, int statPoints, char strongStat) : base(name, statPoints, strongStat)
        {
        }

        public Ork(Monster monster) : base(monster)
        {
        }

        /// <summary>
        /// Special attack from the Ork. It should do nothing as the cooldown is endless.
        /// </summary>
        /// <param name="enemies">Viable list of targets</param>
        protected override void SpecialAttack(Monster enemy)
        {
            this.HealSelf(10);
            SpecialAttackCooldown = 0;
        }
    }
}
