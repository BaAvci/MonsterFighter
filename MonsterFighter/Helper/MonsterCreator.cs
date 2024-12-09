using MonsterFighter.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFighter
{
    public static class MonsterCreator
    {

        /// <summary>
        /// Creates a base unit with random stat, then sets all other units to the same value.
        /// </summary>
        /// <param name="monsterTyp">The Monster that should be created</param>
        /// <param name="unitCount">The amount of monsters should be created</param>
        public static List<Monster> CreateAllUnitsWithSameStatsManually(Type monsterTyp, int unitCount)
        {
            var monsterList = new List<Monster>();
            Monster? baseMonster = Activator.CreateInstance(monsterTyp, $"{monsterTyp} 1") as Monster;

            if (baseMonster == null)
            {
                Console.WriteLine($"Monster konnte nicht erstellt werden. Fehler in der Methode {System.Reflection.MethodBase.GetCurrentMethod().Name}");
                return [];
            }

            if (baseMonster is IWeaponisable) baseMonster.EquipWeapon(new Dagger());

            //baseMonster.Name = $"{monsterTyp} {1}";
            monsterList.Add(baseMonster);

            for (int i = 1; i < unitCount; i++)
            {
                Monster? newMonster = Activator.CreateInstance(monsterTyp, baseMonster) as Monster;

                if (newMonster == null)
                {
                    Console.WriteLine($"Monster konnte nicht erstellt werden. Fehler in der Methode {System.Reflection.MethodBase.GetCurrentMethod().Name}");
                    return [];
                }
                //newMonster.Name = $"{monsterTyp.Name} {i + 1}";
                monsterList.Add(newMonster);
            }
            Console.WriteLine($"Sie haben nun {unitCount} Einheiten der Rasse {monsterTyp}. Alle haben die eingegebenen Status werte.");
            return monsterList;
        }

        /// <summary>
        /// Creates a defined amount of units with a preffered stat. 
        /// </summary>
        /// <param name="monsterTyp">The Monster that should be created</param>
        /// <param name="unitCount">The amount of monsters should be created</param>
        public static List<Monster> CreateUnitsWithDefinedMaximumStat(Type monsterTyp, int unitCount)
        {
            var monsterList = new List<Monster>();
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
                var newMonster = Activator.CreateInstance(monsterTyp, $"{monsterTyp.Name} {i + 1}", maxStatPoints, prefferdStat) as Monster;

                if (newMonster is IWeaponisable) newMonster.EquipWeapon(new Dagger());

                if (newMonster == null)
                {
                    Console.WriteLine($"Monster konnte nicht erstellt werden. Fehler in der Methode {System.Reflection.MethodBase.GetCurrentMethod().Name}");
                    return [];
                }
                monsterList.Add(newMonster);
            }
            return monsterList;
        }

        /// <summary>
        /// Create a defined amount of units with random values for it's stats.
        /// </summary>
        /// <param name="monsterTyp">The Monster that should be created</param>
        /// <param name="unitCount">The amount of monsters should be created</param>
        public static List<Monster> CreateAllUnitsWithNoDefinedMaximumStat(Type monsterTyp, int unitCount)
        {
            var monsterList = new List<Monster>();
            char prefferdStat = SelectPreferedStat();
            for (int i = 0; i < unitCount; i++)
            {
                var newMonster = Activator.CreateInstance(monsterTyp, $"{monsterTyp.Name} {i + 1}", -1, prefferdStat) as Monster;

                if (newMonster is IWeaponisable) newMonster.EquipWeapon(new Dagger());

                if (newMonster == null)
                {
                    //If an error happend, print out the method name.
                    Console.WriteLine($"Monster konnte nicht erstellt werden. Fehler in der Methode {System.Reflection.MethodBase.GetCurrentMethod().Name}");
                    return [];
                }
                monsterList.Add(newMonster);
            }
            return monsterList;
        }

        /// <summary>
        /// Lets the user create a monster manually
        /// </summary>
        /// <param name="race">The race of the monster that should be created</param>
        public static List<Monster> CreateMonsterManually(BeingType race)
        {
            var monsterList = new List<Monster>();
            var mon = Monster.CreateMonsterManually(race);
            if (mon != null)
            {
                monsterList.Add(mon);
            }
            return monsterList;
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
