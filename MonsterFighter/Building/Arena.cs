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
        private List<Monster> _monsterList;


        //Singleton instance only for learning practice. Would not exist in a real arena class.
        private static Arena? instance;
        private Arena() {}

        public static Arena GetInstance()
        {
            if (instance == null)
            {
                instance = new Arena();
            }
            return instance;
        }

        /// <summary>
        /// Let's the Player select 1v1 or groupfight.
        /// </summary>
        /// <returns></returns>
        public int SelectFightStyl()
        {
            _monsterList = new List<Monster>();
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
            Console.WriteLine($"Bitte geben Sie die Werte für das {currentMonsterNumber + 1}. Monster ein.");
            //Select the race for the monster that the user wants to create.
            BeingType race = Monster.SelectRace(_monsterList);
            _monsterList.AddRange(MonsterCreator.CreateMonsterManually(race));
        }

        /// <summary>
        /// Creates all units for the group fight.
        /// </summary>
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
                _monsterList.AddRange(MonsterCreator.CreateAllUnitsWithSameStatsManually(monsterType, unitCount));
                return;
            }

            Console.WriteLine("Wollen Sie die Statuspunkte mit einem Maximum versehen?");

            if (ValidationHelper.YesNoCheck())
            {
                _monsterList.AddRange(MonsterCreator.CreateUnitsWithDefinedMaximumStat(monsterType, unitCount));
                return;
            }
            Console.WriteLine("Die werte aller Einheiten werden zufällig generier.");
            _monsterList.AddRange(MonsterCreator.CreateAllUnitsWithNoDefinedMaximumStat(monsterType, unitCount));
        }

        /// <summary>
        /// Starts the fight between the monsters.
        /// </summary>
        public void StartFight()
        {
            var sortedMonsterList = _monsterList.OrderByDescending(o => o.Speed).ToList();
            FightManager.Fight(sortedMonsterList);
        }
    }
}
