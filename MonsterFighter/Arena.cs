using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MonsterFighter
{
    public class Arena
    {
        private List<Monster> _monsterList = [];
        public bool ViableFight { get; private set; }

        public static int SelectFightStyl()
        {
            Console.WriteLine("Bitte geben Sie die Art des Kampfes an.");
            Console.WriteLine("1 = 1v1 | 2 = Gruppenkampf");
            return ValidationHelper.CheckValueBetween(1, 2);
        }

        /// <summary>
        /// Main funktion that allows the user to create the monsters for the arena fight
        /// </summary>
        /// <param name="monsterAmount">the amount of monsters that should be created</param>
        /// <returns></returns>
        public void CreateSingleParticipants(int currentMonsterNumber)
        {
            Console.WriteLine($"Bitte geben Sie die Werte für das {currentMonsterNumber + 1} Monster ein.");
            MonsterCreator.CreateMonsterManually(ref _monsterList);
        }

        public void CreateGroupParticipants()
        {
            Console.WriteLine("Bitte wählen Sie die Rasse aus.");
            var monsterRace = Monster.SelectRace(_monsterList).ToString();

            var monsterType = Type.GetType($"MonsterFighter.{monsterRace}");

            Console.WriteLine("Wieviele Einheiten sollen für diese Rasse antreten?");
            var unitCount = ValidationHelper.NumberCheck();

            Console.WriteLine("Wollen Sie die Statuspunkte für eine Rasse selber setzten?");
            if (ValidationHelper.YesNoCheck())
            {
                MonsterCreator.CreateAllUnitsWithSameStatsManually(monsterType, unitCount, ref _monsterList);
                return;
            }

            Console.WriteLine("Wollen Sie die Statuspunkte mit einem Maximum versehen?");

            if (ValidationHelper.YesNoCheck())
            {
                MonsterCreator.CreateUnitsWithDefinedMaximumStat(monsterType, unitCount, ref _monsterList);
                return;
            }
            Console.WriteLine("Die werte aller Einheiten werden zufällig generier.");
            MonsterCreator.CreateAllUnitsWithNoDefinedMaximumStat(monsterType, unitCount, ref _monsterList);
        }

        /// <summary>
        /// Validates if the fight would ever end.
        /// </summary>
        /// <returns></returns>
        public void ValidateParticipants()
        {
            var topAttackMonster = _monsterList.MaxBy(m => m.AttackPower);
            var topDefMonster = _monsterList.MaxBy(m => m.DefencePower);
            var secondTopDefMonster = _monsterList.Distinct().OrderByDescending(m => m.DefencePower).Skip(1).First();

            var a = topAttackMonster.AttackPower <= topDefMonster.DefencePower;
            var b = topAttackMonster.AttackPower <= secondTopDefMonster.DefencePower;
            var c = topDefMonster.AttackPower <= topAttackMonster.DefencePower;

            if (a && b || a && b && c)
            {
                Console.WriteLine("Der Kampf würde unendlich lange dauern. Weshalb der Kampf nicht ausgeführt wird.");
                ViableFight = false;
            }
            ViableFight = true;
        }

        /// <summary>
        /// Starts the fight between the monsters.
        /// </summary>
        public void StartFight()
        {
            var sortedMonsterList = _monsterList.OrderByDescending(o => o.Speed).ToList();
            var livingMonsterAmount = 0;
            var turnCounter = 0;
            var changeOfMonsterAmountCoutner = 0;


            while (sortedMonsterList.Where(m => m.HealthPoints > 0).GroupBy(m => m.GetType().Name).ToList().Count > 1)
            {
                foreach (var monster in sortedMonsterList)
                {
                    if (monster.HealthPoints <= 0)
                    {
                        continue;
                    }
                    var allLivingMonsters = sortedMonsterList.Where(m => m.HealthPoints > 0).ToList();
                    if (livingMonsterAmount != allLivingMonsters.Count)
                    {
                        changeOfMonsterAmountCoutner = turnCounter;
                        livingMonsterAmount = allLivingMonsters.Count;
                    }

                    if (changeOfMonsterAmountCoutner == turnCounter - 100)
                    {
                        ValidateParticipants();
                        return;
                    }

                    var targatableUnits = allLivingMonsters
                        .Where(m => m.GetType().Name != monster.GetType().Name)
                        .ToList();
                    monster.Attack(targatableUnits);

                    allLivingMonsters = sortedMonsterList.Where(m => m.HealthPoints > 0).ToList();
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
