using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace MonsterFighter
{
    public abstract class Monster
    {
        public float HealthPoints { get; set; }
        public float AttackPower { get; set; }
        public float DefencePower { get; set; }
        public float Speed { get; set; }
        public int AttackCounter { get; set; } // Counts the amount of attacks for the next special attack.
        internal float AttackMultiplier;

        public Monster()
        {
            SetMonsterStats();
        }
        /// <summary>
        /// Create a monster with a non default attackmultiplier for an different Special Attack damage output
        /// </summary>
        /// <param name="attackMultiplier"></param>
        public Monster(float attackMultiplier)
        {
            AttackMultiplier = attackMultiplier;
            SetMonsterStats();
        }
        /// <summary>
        /// The default attack of the monster.
        /// </summary>
        /// <param name="enemy">The target</param>
        public void Attack(Monster enemy)
        {
            var atk = AttackPower - enemy.DefencePower;
            AttackCounter++;
            if (atk < 0)
            {
                atk = 0;
            }
            Console.WriteLine($"{this.GetType().Name} deals {atk} damage to {enemy.GetType().Name}.");
            enemy.ReciveDamage(atk);
        }
        /// <summary>
        /// The special attack of the monster. Deals more damage than the normal attack.
        /// </summary>
        /// <param name="enemy"></param>
        public abstract void SpecialAttack(Monster enemy);

        /// <summary>
        /// Removes the healthpoints of the monster.
        /// </summary>
        /// <param name="takenDamage"></param>
        internal void ReciveDamage(float takenDamage)
        {
            HealthPoints -= takenDamage;
            Console.WriteLine($"{this.GetType().Name} now has {HealthPoints} healthpoints.");
        }
        /// <summary>
        /// Let's the user select the race of the monster that should fight in the arena.
        /// </summary>
        /// <param name="monsterList">A list of selectable races</param>
        /// <returns></returns>
        public static BeingType SelectRace(List<Monster> monsterList)
        {
            var usableRaces = GetAllSelectableRaces(monsterList);
            var race = RaceInputCheck(usableRaces);
            return (BeingType)race;
        }
        /// <summary>
        /// Allows the user to set the stats of the monster.
        /// </summary>
        internal void SetMonsterStats()
        {
            Console.WriteLine("Eingabe der Statuswerte des Monsters:");

            Console.WriteLine("Bitte geben Sie die Lebenspunkte des Monsters ein:");
            HealthPoints = ValueInputCheck("Bitte geben Sie eine Valide Zahl für die Lebenspunkte ein:");

            Console.WriteLine("Bitte geben Sie die Angriffsstärke des Monsters ein:");
            AttackPower = ValueInputCheck("Bitte geben Sie eine Valide Zahl für die Angriffsstärke ein:");

            Console.WriteLine("Bitte geben Sie den Verteitigungswert des Monsters ein:");
            DefencePower = ValueInputCheck("Bitte geben Sie eine Valide Zahl für den Verteitigungswert ein:");

            Console.WriteLine("Bitte geben Sie die Geschwindigkeit des Monsters ein:");
            Speed = ValueInputCheck("Bitte geben Sie eine Valide Zahl für die Geschwindigkeit ein:");
        }
        /// <summary>
        /// Returns a list of selectable races.
        /// </summary>
        /// <param name="monsterList"></param>
        /// <returns></returns>
        private static List<BeingType> GetAllSelectableRaces(List<Monster> monsterList)
        {
            //Convert all enum values to a list.
            var allBeingTypes = Enum.GetValues(typeof(BeingType)).Cast<BeingType>().ToList();
            //A list of all not used enums in the monsterlist.
            var notUsedRaces = allBeingTypes.Where(m => monsterList.All(m2 => m2.GetType().Name != m.ToString())).ToList();

            var outputText = $"Folgende Rassen können noch selectiert werden:";
            foreach (var race in notUsedRaces)
            {
                outputText += $" | {(int)race} = {race}";
            }
            Console.WriteLine(outputText);
            return notUsedRaces;
        }
        /// <summary>
        /// Validate the input for the Race selection.
        /// </summary>
        /// <param name="usableRaces"></param>
        /// <returns></returns>
        private static int RaceInputCheck(List<BeingType> usableRaces)
        {
            var inputValue = -1;
            //If input value is not in race list, then reask for a propper value
            while (usableRaces.FirstOrDefault(r => (int)r == inputValue) == 0)
            {
                if (Int32.TryParse(Console.ReadLine(), out int input) && usableRaces.FirstOrDefault(r => (int)r == input) != 0)
                {
                    inputValue = input;
                    break;
                }
                Console.WriteLine("Bitte geben Sie eine Valide Zahl für die Rasse ein:");
            }
            return inputValue;
        }
        /// <summary>
        /// Checks the value if it's applyable to a parameter.
        /// </summary>
        /// <param name="displayText">The text that should be displayed if the value is not good</param>
        /// <returns></returns>
        private float ValueInputCheck(string displayText)
        {
            var inputValue = -1f;
            while (inputValue < 0)
            {
                if (float.TryParse(Console.ReadLine(), out float input) && input > 0)
                {
                    return input;
                }
                Console.WriteLine(displayText);
            }
            return inputValue;
        }

    }
}
