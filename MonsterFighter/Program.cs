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
            do
            {
                var fightingStyle = Arena.GetInstance().SelectFightStyl();

                switch (fightingStyle)
                {
                    case 1:
                        var factionAmount = 2;
                        for (int i = 0; i < factionAmount; i++)
                        {
                            Arena.GetInstance().CreateSingleParticipants(i);
                        }
                        break;
                    case 2:
                        Arena.GetInstance().CreateGroupParticipants();
                        break;
                    default:
                        break;
                }

                Arena.GetInstance().StartFight();
                Console.WriteLine("Wollen Sie einen weiteren Kampf ausführen?");
            }
            while (ValidationHelper.YesNoCheck());
        }
    }
}