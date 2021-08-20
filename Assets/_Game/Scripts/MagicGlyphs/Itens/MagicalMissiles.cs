using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class MagicalMissiles : MonoBehaviour
    {
        [SerializeField] float damage, velocity, rangeDetection, attackColisionRange, missileCooldown;
        public Transform root;
        [SerializeField] LayerMask layersToCollide;

        GameObject target;
        Collider[] objectsOnRange = new Collider[10], objectsAttack = new Collider[10];
        float minDist;
        int numObjectsDetected;

        bool canAttack = true;

        MeshRenderer meshObj;

        private void Start()
        {
            meshObj = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            if (canAttack)
            {
                if (target == null || !target.activeSelf)
                {
                    target = FindTarget();
                }
                else
                {
                    Attack();
                }
            }
        }


        IEnumerator Cooldown()
        {
            meshObj.enabled = false;
            canAttack = false;
            yield return new WaitForSeconds(missileCooldown);
            meshObj.enabled = true;
            canAttack = true;
        }

        void Attack()
        {
            if (!meshObj.enabled)
            {
                transform.position = root.position;
                meshObj.enabled = true;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, velocity * Time.deltaTime);

            transform.LookAt(target.transform);

            numObjectsDetected = Physics.OverlapSphereNonAlloc(transform.position, attackColisionRange, objectsAttack, layersToCollide);
            if(numObjectsDetected > 0)
            {
                for (int i = 0; i < numObjectsDetected; i++) {
                    objectsAttack[i].gameObject.GetComponent<Life>().ApplyDamage(damage);
                    target = null;                    
                }
                StartCoroutine("Cooldown");
            }

        }

        GameObject FindTarget()
        {
            if (meshObj.enabled)
            {
                meshObj.enabled = false;
            }

            numObjectsDetected = Physics.OverlapSphereNonAlloc(root.position, rangeDetection, objectsOnRange, layersToCollide);
            if (numObjectsDetected > 0)
            {                
                for (int i = 0; i < numObjectsDetected; i++)
                {
                    if (i == 0)
                    {
                        minDist = Vector3.Distance(transform.position, objectsOnRange[i].transform.position);
                        target = objectsOnRange[i].transform.gameObject;
                    }
                    else if (Vector3.Distance(transform.position, objectsOnRange[i].transform.position) < minDist)
                    {
                        minDist = Vector3.Distance(transform.position, objectsOnRange[i].transform.position);
                        target = objectsOnRange[i].transform.gameObject;
                    }
                }

                numObjectsDetected = 0;
                return target;
            }
            else
            {
                return null;
            }
            
        }

    }
}
