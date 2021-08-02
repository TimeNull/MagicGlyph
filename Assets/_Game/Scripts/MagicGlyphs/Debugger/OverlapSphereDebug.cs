using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class OverlapSphereDebug : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private LayerMask layer;

        private void OnDrawGizmos()
        {
            Collider[] collider = new Collider[10];
            int hitted = Physics.OverlapSphereNonAlloc(transform.position, radius, collider, layer.value); // NonAlloc: it is for user use his own array, and limit the number of collisions
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);

            for (int i = 0; i < hitted; i++)
            {
                Debug.Log(collider[i].name);
                Gizmos.color = Color.red;
                
                Gizmos.DrawWireSphere(collider[i].transform.position, radius / 5);
            }


        }

    }
}
