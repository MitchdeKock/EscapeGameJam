using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Escape__.Code.Scripts.Enemies
{
    public static class EnemyDamageFix
    {
        public static int ScaleDamage(float attack, float multiplier)
        {
            int damage =(int) MathF.Max(attack, MathF.Round(attack * multiplier));
            return damage;
        }
    }
}
