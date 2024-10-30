using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFighter
{
    internal static class FightManager
    {

        /// <summary>
        /// Validates if the fight would ever end.
        /// </summary>
        /// <returns></returns>
        private static bool ValidateParticipants(List<Monster> monsterList)
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
        public static void Fight(List<Monster> monsterList)
        {
            var livingMonsterAmount = 0;
            var turnCounter = 0;
            var sameResultCounter = 0;

            //Get all Monsters with more than 0 hp, group them by their race and check if the list contains more than 1 race.
            while (monsterList.Where(m => m.HealthPoints > 0)
                .GroupBy(m => m.GetType().Name)
                .ToList().Count > 1)
            {
                foreach (var monster in monsterList)
                {
                    if (monster.HealthPoints <= 0)
                    {
                        continue;
                    }
                    var allLivingMonsters = monsterList.Where(m => m.HealthPoints > 0).ToList();

                    //Check if the monsteramount has changed over the last X turns.
                    if (livingMonsterAmount == allLivingMonsters.Count)
                    {
                        sameResultCounter++;
                        if (sameResultCounter == 30 && !ValidateParticipants(monsterList))
                        {
                            return;
                        }
                    }

                    var targatableUnits = allLivingMonsters
                        .Where(m => m.GetType().Name != monster.GetType().Name)
                        .ToList();

                    monster.Attack(targatableUnits);

                    //Check if all monsters of a race have died after the attack. Then print out the winner.
                    allLivingMonsters = monsterList.Where(m => m.HealthPoints > 0).ToList();
                    if (allLivingMonsters.GroupBy(m => m.GetType().Name).ToList().Count < 2)
                    {
                        var a = allLivingMonsters.GroupBy(m => m.GetType().Name).ToList().First().ToList();
                        Console.WriteLine($"Gewonnen hat die {a[0].GetType().Name} Rasse. Die Schlacht hat {turnCounter} Runden gedauert.");
                        break;
                    }
                    turnCounter++;
                }
            }
        }

    }
}
