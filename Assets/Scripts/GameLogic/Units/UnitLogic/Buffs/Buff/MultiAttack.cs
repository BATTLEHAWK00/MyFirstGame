using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buff
{
    public class MultiAttack : Buff
    {
        protected override void afterTargetSet()
        {
            base.afterTargetSet();
            unit.dualAttack = true;
        }
        protected override void beforeDestroy()
        {
            base.beforeDestroy();
            unit.dualAttack = false;
        }
        public MultiAttack(int rounds) : base(rounds)
        {
            BuffName = "连击";
        }
    }
}