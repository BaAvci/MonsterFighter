using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFighter
{
    public class Ork : Monster
    {
        public Ork()
        {
            AttackMultiplier = 2;
        }

        public Ork(float attackMultiplier) : base(attackMultiplier)
        {
        }

        public Ork(int statPoints, char strongStat) : base(statPoints, strongStat)
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
