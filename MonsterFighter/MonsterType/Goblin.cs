using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFighter
{
    public class Goblin : Monster
    {

        public Goblin(string name, float attackMultiplier) : base(name, attackMultiplier)
        {
        }

        public Goblin(string name) : base(name)
        {
        }

        public Goblin(string name, int statPoints, char strongStat) : base(name, statPoints, strongStat)
        {
        }

        public override void Attack(List<Monster> enemies)
        {
            base.Attack(enemies);
            SpecialAttackCooldown++;
        }

        /// <summary>
        /// Special attack from the goblin allows him to attack twice.
        /// </summary>
        /// <param name="enemies">Viable list of targets</param>
        protected override void SpecialAttack(Monster enemy)
        {
            var atk = (AttackPower - enemy.DefencePower) * 2;
            Console.WriteLine($"{GetType().Name} makes a special attack and deals {atk} damage!");
            enemy.ReciveDamage(atk);
            SpecialAttackCooldown = 0;
        }
    }
}
