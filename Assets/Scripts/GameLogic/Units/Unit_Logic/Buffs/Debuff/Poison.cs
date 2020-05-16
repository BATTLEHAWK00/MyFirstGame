using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Buff
{
    public class Poison : Buff
    {
        int hpPerRound;
        public Poison(int rounds, int hpPerRound) : base(rounds)
        {
            this.hpPerRound = hpPerRound;
            BuffName = "中毒";
        }

        protected override void onNextRound()
        {
            unit.CostHP(hpPerRound);
        }
    }

}
