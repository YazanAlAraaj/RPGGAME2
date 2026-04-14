using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGAME2
{
    internal class Monster : LivingCreature
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MaxDamage { get; set; }
        public int RewardedXP { get; set; }
        public int RewardedGold { get; set; }

        public Monster(int ID, string name, int maxDamage, int rewardedXP, int rewardedGold,int CurrentHP,int MaxHP) : base(MaxHP,CurrentHP)
        {
            this.ID = ID;
            this.Name = name;
            this.MaxDamage = maxDamage;
            this.RewardedXP = rewardedXP;
            this.RewardedGold = rewardedGold;
        }
    }
}
