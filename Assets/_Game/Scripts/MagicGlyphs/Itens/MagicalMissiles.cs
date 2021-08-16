using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class MagicalMissiles : MonoBehaviour
    {
        [SerializeField] float damage, velocity, rangeDetection, attackColisionRange;
        [SerializeField] Transform root;
        [SerializeField] LayerMask layersToCollide;
        GameObject target;
        Collider[] objectsOnRange;
        float minDist;
        int numObjectsDetected;
        bool canAttack;


        private void Update()
        {
            if (canAttack)
            {
                if (target == null || !target.activeSelf)
                {
                    ReturnToPlayer();
                    target = FindTarget();
                }
                else
                {
                    Debug.Log("target: " + target);
                    Attack();
                }
            }
            else
            {
                ReturnToPlayer();
            }
        }

        void ReturnToPlayer()
        {
            transform.position = Vector3.MoveTowards(transform.position, root.position, velocity * Time.deltaTime);

            transform.LookAt(root);

            if (Vector3.Distance(transform.position, root.position) < 0.1f)
            {
                canAttack = true;
            }
        }

        void Attack()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, velocity * Time.deltaTime);

            transform.LookAt(target.transform);

            numObjectsDetected = Physics.OverlapSphereNonAlloc(transform.position, attackColisionRange, objectsOnRange, layersToCollide);
            if(numObjectsDetected > 0)
            {
                for (int i = 0; i <= numObjectsDetected; i++) {
                    objectsOnRange[i].gameObject.GetComponent<Life>().ApplyDamage(damage);
                    target = null;                    
                }
                canAttack = false;
            }

        }

        GameObject FindTarget()
        {
            numObjectsDetected = Physics.OverlapSphereNonAlloc(root.position, rangeDetection, objectsOnRange, layersToCollide);
            if (numObjectsDetected > 0)
            {                
                for (int i = 0; i <= numObjectsDetected; i++)
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
                Debug.Log("fazendo");
                return null;
            }
            
        }

    }
}
