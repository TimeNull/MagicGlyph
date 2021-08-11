using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public static class Utility
    {
        /// <summary>
        /// Make an object look at another with delay.
        /// </summary>
        /// <param name="to"></param>
        public static void rotateTowards(Transform ownObject, Vector3 to, float turnSpeed)
        {
            Quaternion _lookRotation = Quaternion.LookRotation((to - ownObject.position).normalized);

            ownObject.rotation = Quaternion.Slerp(ownObject.rotation, _lookRotation, Time.deltaTime * turnSpeed);
        }

    }

}
