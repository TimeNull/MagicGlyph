using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class BoxcastDebug : MonoBehaviour
    {
        [SerializeField] private float maxDistance;
        [SerializeField] private Vector3 offset;

        private void OnDrawGizmos()
        {
            RaycastHit hit;

            bool isHit = Physics.BoxCast(transform.position + offset, transform.lossyScale/2, transform.forward, out hit, transform.rotation, maxDistance);

            if (isHit)
            {
                
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position + offset, transform.forward * hit.distance);
                Gizmos.DrawWireCube(transform.position + offset + transform.forward * hit.distance, transform.lossyScale);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position + offset, transform.forward * maxDistance);
                Gizmos.DrawWireCube(transform.position + offset + transform.forward * maxDistance, transform.lossyScale);
            }
        }
    }
}
