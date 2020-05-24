using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Buff
{
    public class LightShield : Buff
    {
        protected override void beforeDestroy()
        {
            unit.missAttack = false;
        }
        protected override void afterTargetSet()
        {
            base.afterTargetSet();
            unit.missAttack = true;
        }
        public LightShield(int rounds) : base(rounds) 
        {
            BuffName = "光盾";
        }
    }
}

