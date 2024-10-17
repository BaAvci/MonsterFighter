using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFighter
{
    /// <summary>
    /// The selectable Races for the Arena
    /// </summary>
    // If you add a race here, a new class with the same name is needed that inherits from the class Monster.
    public enum BeingType
    {
        Goblin = 1,
        Ork = 2,
        Troll = 3
    }
}
