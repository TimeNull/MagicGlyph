using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class ExplosivePlayer : MonoBehaviour
    {
        [SerializeField] float explosionRay, damage;
        [SerializeField] LayerMask layersToAffect;
        Collider[] affectedObjects;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            affectedObjects = Physics.OverlapSphere(transform.position, explosionRay, layersToAffect);

            foreach (Collider col in affectedObjects)
            {
                col.GetComponent<Life>().ApplyDamage(damage);
                gameObject.SetActive(false);
            }
        }
    }
}
