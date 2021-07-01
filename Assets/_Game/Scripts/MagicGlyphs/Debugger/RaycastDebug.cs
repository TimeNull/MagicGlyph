using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs.Debugger
{
    public class RaycastDebug : MonoBehaviour
    {
        [SerializeField] private float maxDistance;
        [SerializeField] private Vector3 offset;

        private void OnDrawGizmos()
        {
            RaycastHit hit;

            bool isHit = Physics.Raycast(transform.position + offset, transform.forward, out hit, maxDistance);

            if (isHit)
            {
               
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position + offset, transform.forward * hit.distance);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position + offset, transform.forward * maxDistance);
            }
        }
    }
}
