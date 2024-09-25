using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFighter
{
    public class Arena
    {
        private List<Monster> monsterList = new List<Monster>();

        /// <summary>
        /// Main funktion that allows the user to create the monsters for the arena fight
        /// </summary>
        /// <param name="monsterAmount">the amount of monsters that should be created</param>
        /// <returns></returns>
        public bool CreateSingleParticipants(int monsterAmount)
        {
            for (int i = 0; i < monsterAmount; i++)
            {
                Console.WriteLine($"Bitte geben Sie die Werte für das {i + 1} Monster ein.");
                BeingType race = Monster.SelectRace(monsterList);
                switch (race)
                {
                    case BeingType.Goblin:
                        monsterList.Add(new Goblin());
                        break;
                    case BeingType.Ork:
                        monsterList.Add(new Ork());
                        break;
                    case BeingType.Troll:
                        monsterList.Add(new Troll());
                        break;
                    default:
                        Console.WriteLine("Erstellung des Monsters ist fehlgeschlagen!");
                        break;
                }
            }
            return ValidateParticipants();
        }
        /// <summary>
        /// Validates if the fight would ever end.
        /// </summary>
        /// <returns></returns>
        private bool ValidateParticipants()
        {
            var topAttackMonster = monsterList.MaxBy(m => m.AttackPower);
            var topDefMonster = monsterList.MaxBy(m => m.DefencePower);
            var secondTopDefMonster = monsterList.Distinct().OrderByDescending(m => m.DefencePower).Skip(1).First();
            
            var a = topAttackMonster.AttackPower <= topDefMonster.DefencePower;
            var b = topAttackMonster.AttackPower <= secondTopDefMonster.DefencePower;
            var c = topDefMonster.AttackPower <= topAttackMonster.DefencePower;

            if (a && b || a && b && c)
            {
                Console.WriteLine("Der Kampf würde unendlich lange dauern. Weshalb der Kampf nicht ausgeführt wird.");
                return false;
            }
            return true;
        }
        /// <summary>
        /// Starts the fight between the monsters.
        /// </summary>
        public void StartFight()
        {
            var sortedMonsterList = monsterList.OrderByDescending(o => o.Speed).ToList();
            var turncounter = 1;
            while (sortedMonsterList.Min(x => x.HealthPoints) > 0)
            {
                for (int i = 0; i < sortedMonsterList.Count; i++)
                {
                    Console.WriteLine($"Current turn: {turncounter}");
                    Monster? target = null;
                    var attacker = sortedMonsterList[i];
                    //Set the target to be attacked
                    if (i == sortedMonsterList.Count - 1)
                    {
                        target = sortedMonsterList[0];
                    }
                    else
                    {
                        target = sortedMonsterList[i + 1];
                    }
                    // Decide between default attack and special attack
                    if (attacker.AttackCounter != 5)
                    {
                        attacker.Attack(target);
                    }
                    else
                    {
                        attacker.SpecialAttack(target);
                    }

                    if (target.HealthPoints <= 0)
                    {
                        break;
                    }
                    turncounter++;
                }
            }
        }
    }
}
