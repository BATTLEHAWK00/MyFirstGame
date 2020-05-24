using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buff
{
    public class Triumph : Buff
    {
        protected override void afterTargetSet()
        {
            base.afterTargetSet();
            unit.triumph = true;
        }
        protected override void beforeDestroy()
        {
            base.beforeDestroy();
            unit.triumph = false;
        }
        public Triumph(int rounds) : base(rounds)
        {
            BuffName = "凯旋";
        }
    }
}