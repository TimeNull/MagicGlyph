using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class AnimatorNames
    {
        //Boss triggers
        public static readonly int BossAttack = Animator.StringToHash("Attack");
        public static readonly int BossIdle = Animator.StringToHash("Idle");
        public static readonly int BossWalk = Animator.StringToHash("Walk");
        public static readonly int BossInvoke = Animator.StringToHash("Invoke");

        //Player triggers
        public static readonly int PlayerAttack = Animator.StringToHash("attack");
        public static readonly int PlayerIdle = Animator.StringToHash("idle");
        public static readonly int PlayerBackRun = Animator.StringToHash("backrun");
        public static readonly int PlayerRun = Animator.StringToHash("run");
        public static readonly int PlayerSkill = Animator.StringToHash("skill");

        //Goblin triggers
        public static readonly int GoblinAttack = Animator.StringToHash("Attack");


    }


}
