using System.Security.Cryptography;
using System.Linq;

namespace MonsterFighter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var monsterAmount = 2;
            var arena = new Arena();
            var vialbe = arena.CreateParticipants(monsterAmount);
            if (vialbe)
            {
                arena.StartFight();
            }
        }
    }
}
