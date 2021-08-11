using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class ChecagemDeAtaqueDesseGoblinMerda : MonoBehaviour
    {
        [SerializeField] float damage, range;
        [SerializeField] LayerMask layersToAffect;
        Collider[] affectedObjects;

        private void OnEnable()
        {
            affectedObjects = Physics.OverlapSphere(transform.position, range, layersToAffect);

            foreach (Collider col in affectedObjects)
            {
                col.GetComponent<Life>().ApplyDamage(damage);
            }
        }
    }
}
