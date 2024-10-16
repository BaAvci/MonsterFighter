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
            var arena = new Arena();
            var fightingStyle = Arena.SelectFightStyl();

            switch (fightingStyle)
            {
                case 1:
                    // creates the amount of monsters that should fight in the single monster fight.
                    // 1v1 or 1v1v1 or 1v1v1v1
                    int monsterAmount = 2;
                    for (int i = 0; i < monsterAmount; i++)
                    {
                        arena.CreateSingleParticipants(i);
                    }
                    break;
                case 2:
                    arena.CreateGroupParticipants();
                    break;
                default:
                    break;
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