using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFighter
{
    public class Troll : Monster
    {

        public Troll()
        {
            AttackMultiplier = 2.5f;
        }

        public Troll(float attackMultiplier) : base(attackMultiplier)
        {
        }

        public Troll(int statPoints, char strongStat) : base(statPoints, strongStat)
        {
        }

        public override void SpecialAttack(Monster enemy)
        {
            var atk = AttackPower * AttackMultiplier - enemy.DefencePower;
            if (atk < 0)
            {
                atk = 0;
            }
            Console.WriteLine($"{GetType().Name} makes a special attack and deals {atk} damage!");
            enemy.ReciveDamage(atk);
            SpecialAttackCooldown = 0;
        }
    }
}
