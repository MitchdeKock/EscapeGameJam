using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Escape__.Code.Scripts.Player
{
    public class PowerUpModel
    {
        public int MoveSpeedUpgrade { get; set; } = 0;
        public int DashSpeedUpgrade { get; set; } = 0;
        public int DashCoolDownUpgrade { get; set; } = 0;
        public int DashDurationUpgrade { get; set; } = 0;
        public float RangeAttackDistanceUpgrade { get; set; } = 0;
        public float  RangeAttackDamageUpgrade { get; set; } = 0;
        public float MeleeAttackDamageUpgrade { get; set; } = 0;

    }
}
