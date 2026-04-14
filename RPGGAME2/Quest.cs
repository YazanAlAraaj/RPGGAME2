using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGAME2
{
    internal class Quest
    {
        public int ID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RewardedXP { get; set; }
        public int RewardedGold { get; set; }

        public Quest(int ID,string Name , String Description , int RewardedXP, int RewardedGold)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.RewardedXP = RewardedXP;
            this.RewardedGold = RewardedGold;
        }
    }
}
