using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace MonsterFighter
{
    public abstract class Monster
    {
        [Stat]
        public float HealthPoints { get; set; }
        [Stat]
        public float AttackPower { get; set; }
        [Stat]
        public float DefencePower { get; set; }
        [Stat]
        public float Speed { get; set; }

        public string Name {set; get; }

        public const int defaultMaxStatPoints = 100;
        public int SpecialAttackCooldown;  // Counts the amount of attacks for the next special attack.
        internal float AttackMultiplier;
        private static readonly Random random = new();
        private int minValue = 5;
        private int maxValue = 50;
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

        public Monster(int statPoints, char strongStat)
        {
            if (statPoints <= 0)
            {
                statPoints = random.Next(minValue,maxValue);
            }
            SetMonsterStats(statPoints, strongStat);
        }
        public Monster(float healthPoints, float attackPower, float defencePower, float speed)
        {
            HealthPoints = healthPoints;
            AttackPower = attackPower;
            DefencePower = defencePower;
            Speed = speed;
        }

        /// <summary>
        /// The default attack of the monster. It selects a random target in the given List.
        /// </summary>
        /// <param name="enemy">The target</param>
        public void Attack(List<Monster> enemies)
        {
            var randomTargetedMonster = random.Next(0, enemies.Count);
            var atk = AttackPower - enemies[randomTargetedMonster].DefencePower;
            SpecialAttackCooldown++;
            if (atk < 0)
            {
                atk = 0;
            }
            Console.WriteLine($"{Name} verursachte {atk} schaden an {enemies[randomTargetedMonster].Name}.");
            enemies[randomTargetedMonster].ReciveDamage(atk);
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
            Console.WriteLine($"{Name} hat nun  {HealthPoints} Lebenspunkte.");
            if (HealthPoints < 0)
            {
                Console.WriteLine($"{Name} ist gestorben.");
            }
        }
        /// <summary>
        /// Let's the user select the race of the monster that should fight in the arena.
        /// </summary>
        /// <param name="monsterList">A list of already selected races</param>
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
        private void SetMonsterStats()
        {
            Console.WriteLine("Eingabe der Statuswerte des Monsters:");

            Console.WriteLine("Bitte geben Sie die Lebenspunkte des Monsters ein:");
            HealthPoints = ValidationHelper.ValueInputCheck("Bitte geben Sie eine Valide Zahl für die Lebenspunkte ein:");

            Console.WriteLine("Bitte geben Sie die Angriffsstärke des Monsters ein:");
            AttackPower = ValidationHelper.ValueInputCheck("Bitte geben Sie eine Valide Zahl für die Angriffsstärke ein:");

            Console.WriteLine("Bitte geben Sie den Verteitigungswert des Monsters ein:");
            DefencePower = ValidationHelper.ValueInputCheck("Bitte geben Sie eine Valide Zahl für den Verteitigungswert ein:");

            Console.WriteLine("Bitte geben Sie die Geschwindigkeit des Monsters ein:");
            Speed = ValidationHelper.ValueInputCheck("Bitte geben Sie eine Valide Zahl für die Geschwindigkeit ein:");
        }

        private void SetMonsterStats(int maxStatPoints, char strongStat)
        {
            //Gets all Properties that have the Attribute "Stat"
            var allStatProperties = GetAllStatPropertys();

            //Get's the first string item in the list where the Propertyname starts with a specified character.

            SetAllMonsterStatRandom(maxStatPoints, allStatProperties);
            if (strongStat.Equals('x'))
            {
                var preferredStat = allStatProperties.Where(p => p.Name[0] == strongStat).ToList()[0].Name;
                SetSpecialtyStat(maxStatPoints, allStatProperties, preferredStat);
            }
        }

        private void SetSpecialtyStat(int maxStatPoints, List<PropertyInfo> allStatProperties, string preferredStat)
        {
            while (allStatProperties.Aggregate((p1, p2) => (float)p1.GetValue(this) > (float)p2.GetValue(this) ? p1 : p2).Name != preferredStat)
            {
                var statValue = random.NextDouble() * maxStatPoints;
                GetType().GetProperty(preferredStat).SetValue(this, (float)statValue);
            }
        }

        private void SetAllMonsterStatRandom(int maxStatPoints, List<PropertyInfo> allStatProperties)
        {
            foreach (var property in allStatProperties)
            {
                var statValue = random.NextDouble() * maxStatPoints;
                GetType().GetProperty(property.Name).SetValue(this, (float)statValue);
            }
        }
        public static Monster? CreateMonsterManually(BeingType race)
        {
            switch (race)
            {
                case BeingType.Goblin:
                    return new Goblin();
                case BeingType.Ork:
                    return new Ork();
                case BeingType.Troll:
                    return new Troll();
                default:
                    Console.WriteLine("Erstellung des Monsters ist fehlgeschlagen!");
                    return null;
            }
        }

        public static List<PropertyInfo> GetAllStatPropertys()
        {
            return typeof(Monster).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(Stat))).ToList();
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
    }
}
