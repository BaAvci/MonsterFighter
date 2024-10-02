using System.Security.Cryptography;
using System.Linq;
using Microsoft.VisualBasic;
using System.Reflection;

namespace MonsterFighter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var monsterRace = Monster.SelectRace([]).ToString();

            //var monsterType = Type.GetType($"MonsterFighter.{monsterRace}");
            //var newMonster = Activator.CreateInstance(monsterType) as Monster;

            var arena = new Arena();
            var fightingStyle = Arena.SelectFightStyl();

            for (int i = 0; i < 2; i++)
            {
                if (fightingStyle == 1)
                {
                    arena.CreateSingleParticipants(i);
                }
                else if (fightingStyle == 2)
                {
                    arena.CreateGroupParticipants();
                }
            }

            do
            {
                arena.StartFight();
                Console.WriteLine("Wollen Sie einen weiteren Kampf ausführen?");
            }
            while (ValidationHelper.YesNoCheck());
        }
    }
}