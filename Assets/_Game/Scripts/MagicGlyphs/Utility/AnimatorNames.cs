using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs.Utility
{
    public static class AnimatorNames
    {
        //Boss animations
        public static readonly int BossAttack = Animator.StringToHash("StaffAttack");
        public static readonly int BossIdle = Animator.StringToHash("StaffIdle");
        public static readonly int BossWalk = Animator.StringToHash("StaffWalking");
        public static readonly int BossInvoke = Animator.StringToHash("StaffInvoke");

        //Player animations
        public static readonly int PlayerAttack = Animator.StringToHash("attack");
        public static readonly int PlayerIdle = Animator.StringToHash("idle");
        public static readonly int PlayerRun = Animator.StringToHash("run");

        
    }


}
