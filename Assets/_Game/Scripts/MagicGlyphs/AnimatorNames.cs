using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public static class AnimatorNames
    {
        //Boss Attacks
        public static readonly int BossAttack = Animator.StringToHash("StaffAttack");
        public static readonly int BossIdle = Animator.StringToHash("StaffIdle");
        public static readonly int BossWalk = Animator.StringToHash("StaffWalking");
        public static readonly int BossInvoke = Animator.StringToHash("StaffInvoke");
    }
}
