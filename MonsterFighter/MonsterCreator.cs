using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFighter
{
    public abstract class MonsterCreator
    {
        /// <summary>
        /// Creates a base unit with random stat, then sets all other units to the same value.
        /// </summary>
        /// <param name="monsterTyp">The Monster that should be created</param>
        /// <param name="unitCount">The amount of monsters should be created</param>
        /// <param name="_monsterList">A list of mosnters that should be filled</param>
        public static void CreateAllUnitsWithSameStatsManually(Type monsterTyp, int unitCount, ref List<Monster> _monsterList)
        {
            Monster? baseMonster = Activator.CreateInstance(monsterTyp) as Monster;
            if (baseMonster == null)
            {
                Console.WriteLine($"Monster konnte nicht erstellt werden. Fehler in der Methode {System.Reflection.MethodBase.GetCurrentMethod().Name}");
                return;
            }
            baseMonster.Name = $"{monsterTyp} {1}";
            _monsterList.Add(baseMonster);

            for (int i = 1; i < unitCount; i++)
            {
                var newMonster = Activator.CreateInstance(monsterTyp,
                    _monsterList[0].HealthPoints,
                    _monsterList[0].AttackPower,
                    _monsterList[0].DefencePower,
                    _monsterList[0].Speed) as Monster;
                if (newMonster == null)
                {
                    Console.WriteLine($"Monster konnte nicht erstellt werden. Fehler in der Methode {System.Reflection.MethodBase.GetCurrentMethod().Name}");
                    return;
                }
                newMonster.Name = $"{monsterTyp.Name} {i + 1}";
                _monsterList.Add(newMonster);
            }
            Console.WriteLine($"Sie haben nun {unitCount} Einheiten der Rasse {monsterTyp}. Alle haben die eingegebenen Status werte.");
        }

        /// <summary>
        /// Creates a defined amount of units with a preffered stat. 
        /// </summary>
        /// <param name="monsterTyp">The Monster that should be created</param>
        /// <param name="unitCount">The amount of monsters should be created</param>
        /// <param name="_monsterList">A list of mosnters that should be filled</param>
        public static void CreateUnitsWithDefinedMaximumStat(Type monsterTyp, int unitCount, ref List<Monster> _monsterList)
        {
            char prefferdStat = SelectPreferedStat();
            var maxStatPoints = Monster.defaultMaxStatPoints;
            Console.WriteLine($"Soll das standart Maximum and Statuspunkten verändert werden? Ja = 1 | Nein = 2 (Standart: {Monster.defaultMaxStatPoints})");
            if (ValidationHelper.YesNoCheck())
            {
                var minvalue = Monster.GetAllStatPropertys().Count;
                Console.WriteLine($"Wieviele Statuspunkte sollen pro Einheit verwendet werden? (Mindestens: {minvalue})");
                maxStatPoints = ValidationHelper.NumberCheck(minvalue);
            }
            for (int i = 0; i < unitCount; i++)
            {
                var newMonster = Activator.CreateInstance(monsterTyp, maxStatPoints, prefferdStat) as Monster;
                if (newMonster == null)
                {
                    Console.WriteLine($"Monster konnte nicht erstellt werden. Fehler in der Methode {System.Reflection.MethodBase.GetCurrentMethod().Name}");
                    return;
                }
                newMonster.Name = $"{monsterTyp.Name} {i + 1}";
                _monsterList.Add(newMonster);
            }
        }

        /// <summary>
        /// Create a defined amount of units with random values for it's stats.
        /// </summary>
        /// <param name="monsterTyp">The Monster that should be created</param>
        /// <param name="unitCount">The amount of monsters should be created</param>
        /// <param name="_monsterList">A list of mosnters that should be filled</param>
        public static void CreateAllUnitsWithNoDefinedMaximumStat(Type monsterTyp, int unitCount, ref List<Monster> _monsterList)
        {
            char prefferdStat = SelectPreferedStat();
            for (int i = 0; i < unitCount; i++)
            {
                var newMonster = Activator.CreateInstance(monsterTyp, -1, prefferdStat) as Monster;
                if (newMonster == null)
                {
                    Console.WriteLine($"Monster konnte nicht erstellt werden. Fehler in der Methode {System.Reflection.MethodBase.GetCurrentMethod().Name}");
                    return;
                }
                newMonster.Name = $"{monsterTyp.Name} {i + 1}";
                _monsterList.Add(newMonster);

            }
        }

        /// <summary>
        /// Lets the user create a monster manually
        /// </summary>
        /// <param name="_monsterList">A list of mosnters that should be filled</param>
        public static void CreateMonsterManually(ref List<Monster> _monsterList)
        {
            BeingType race = Monster.SelectRace(_monsterList);
            var mon = Monster.CreateMonsterManually(race);
            if (mon != null)
            {
                mon.Name = race.ToString();
                _monsterList.Add(mon);
            }
        }
        /// <summary>
        /// Let's the user select if a monster should be created with a prefferd stat.
        /// </summary>
        /// <returns>Returns the stat that should be prefferd</returns>
        private static char SelectPreferedStat()
        {
            Console.WriteLine("Soll ein Statuswert bevorzugt werden? Ja = 1 | Nein = 2");
            var prefferdStat = '\0';
            if (ValidationHelper.YesNoCheck())
            {
                Console.WriteLine("Welcher Status soll bevorzugt werden?");
                var statlist = Monster.GetAllStatPropertys();
                var viableInput = new List<char>();
                var text = "Nichts = X ";
                viableInput.Add('x');
                foreach (var stat in statlist)
                {
                    text += $" | {stat.Name} = {stat.Name.ToLower()[0]}";
                    viableInput.Add(stat.Name.ToLower()[0]);
                }
                Console.WriteLine(text);
                prefferdStat = ValidationHelper.CharInputCheck(viableInput);
            }

            return prefferdStat;
        }

    }
}
