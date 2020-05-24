using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Buff
{
    public class Heal : Buff
    {
        private int hpPerRound;
        protected override void onNextRound()
        {
            unit.AddHP(hpPerRound);
        }
        public Heal(int rounds, int hpPerRound) : base(rounds)
        {
            this.hpPerRound = hpPerRound;
            BuffName = "治疗";
        }
    }

}
