using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class RootMotionFix : MonoBehaviour
    {
        private Animator anim;

        

        private void Start()
        {
            anim = GetComponent<Animator>();

        }

        private void OnAnimatorMove()
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            //Debug.Log(anim.applyRootMotion);

            // made this var to more clearly see the condition

            bool needRootMotion = (
                stateInfo.shortNameHash == AnimatorNames.BossAttack ||
                stateInfo.shortNameHash == AnimatorNames.BossWalk
                );

            if (needRootMotion)
            {
                anim.ApplyBuiltinRootMotion();
            }

        }

    }
}
