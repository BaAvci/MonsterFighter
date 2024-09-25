using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFighter
{
    public class Goblin : Monster
    {
        public Goblin()
        {
            AttackMultiplier = 1.5f;
        }

        public Goblin(float multiplier) : base(multiplier)
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
            AttackCounter = 0;
        }
    }
}
