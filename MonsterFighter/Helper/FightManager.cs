using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFighter
{
    internal class FightManager
    {
        private static FightManager _instance;

        public static FightManager Instance()
        {
            if (_instance == null)
            {
                _instance = new FightManager();
            }
            return _instance;
        }

        /// <summary>
        /// Validates if the fight would ever end.
        /// </summary>
        /// <returns></returns>
        public bool ValidateParticipants(List<Monster> monsterList)
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
        public void Fight(List<Monster> monsterList)
        {
            var livingMonsterAmount = 0;
            var turnCounter = 0;
            var changeOfMonsterAmountCoutner = 0;

            while (monsterList.Where(m => m.HealthPoints > 0).GroupBy(m => m.GetType().Name).ToList().Count > 1)
            {
                foreach (var monster in monsterList)
                {
                    if (monster.HealthPoints <= 0)
                    {
                        continue;
                    }
                    var allLivingMonsters = monsterList.Where(m => m.HealthPoints > 0).ToList();
                    if (livingMonsterAmount != allLivingMonsters.Count)
                    {
                        changeOfMonsterAmountCoutner = turnCounter;
                        livingMonsterAmount = allLivingMonsters.Count;
                    }

                    if (changeOfMonsterAmountCoutner == turnCounter - 100)
                    {
                        ValidateParticipants(monsterList);
                        return;
                    }

                    var targatableUnits = allLivingMonsters
                        .Where(m => m.GetType().Name != monster.GetType().Name)
                        .ToList();
                    monster.Attack(targatableUnits);

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
