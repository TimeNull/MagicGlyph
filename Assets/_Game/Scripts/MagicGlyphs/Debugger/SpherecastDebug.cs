using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class SpherecastDebug : MonoBehaviour
    {
        [SerializeField] private bool multicast;
        [SerializeField] private float maxDistance;
        [SerializeField] private int numberofCollisions;
        [SerializeField] private Vector3 offset;
        [SerializeField] private LayerMask layer;
        [SerializeField] private float radius;

        private void OnDrawGizmos()
        {
            if (!multicast)
            {
                RaycastHit hit;
                bool isHit;
                isHit = Physics.SphereCast(transform.position + offset, radius, transform.forward, out hit, maxDistance, layer);


                if (isHit)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(transform.position + offset, transform.forward * hit.distance);
                    Gizmos.DrawWireSphere(transform.position + offset + transform.forward * hit.distance, radius);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(transform.position + offset, transform.forward * maxDistance);
                    Gizmos.DrawWireSphere(transform.position + offset + transform.forward * maxDistance, radius);
                }

            }

            if (multicast)
            {
                RaycastHit[] results = new RaycastHit[numberofCollisions];
                int hitted;
                hitted = Physics.SphereCastNonAlloc(transform.position + offset, radius, transform.forward, results, maxDistance, layer);

                if(hitted > 0)
                {
                   
                    for (int i = 0; i < hitted; ++i)
                    {
                        //Debug.Log(results[i].transform);
                        
                        Gizmos.color = Color.red;
                        Gizmos.DrawRay(transform.position + offset, transform.forward * results[i].distance);
                        Gizmos.DrawWireSphere(transform.position + offset + transform.forward * results[i].distance, radius);
                    }
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(transform.position + offset, transform.forward * maxDistance);
                    Gizmos.DrawWireSphere(transform.position + offset + transform.forward * maxDistance, radius);
                }
                
            }
                

        }
    }
}
