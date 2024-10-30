using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFighter.Weapon
{
    public interface IWeapon
    {
        public int AttackModifier { get; }
    }
}
