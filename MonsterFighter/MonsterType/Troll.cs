using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFighter
{
    public class Troll : Monster
    {

        public Troll(string name) : base(name)
        {
            AttackMultiplier = 2.5f;
        }

        public Troll(string name, float attackMultiplier) : base(name, attackMultiplier)
        {
        }

        public Troll(string name, int statPoints, char strongStat) : base(name, statPoints, strongStat)
        {
        }
        /// <summary>
        /// Special attack from the Troll. He ignores the Defence.
        /// </summary>
        /// <param name="enemies">Viable list of targets</param>
        protected override void SpecialAttack(Monster enemy)
        {
            Console.WriteLine($"{GetType().Name} makes a special attack and deals {AttackPower} damage!");
            enemy.ReciveDamage(AttackPower);
            SpecialAttackCooldown = 0;
        }
    }
}
