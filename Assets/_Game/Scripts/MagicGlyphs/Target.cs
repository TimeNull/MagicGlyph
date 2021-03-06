using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    [RequireComponent(typeof(Controller))]
    public class Target : MonoBehaviour
    {
        private Collider[] OnRange;
        [SerializeField] private LayerMask layersAfected;
        private float minDist;
        private int getNearestObject = 0;
        [HideInInspector] public GameObject nearestGameObject;
        [SerializeField] private float range; // ************************************ REMEMBER TO ASSIGN THIS WITH SCRIPTABLE OBJECT
        [SerializeField] private Controller controller;

        private bool stopTrigger;

        // Update is called once per frame
        void Update()
        {
            OnRangeEnter();
        }

        //check if enemies entered the attack range
        void OnRangeEnter()
        {
            OnRange = Physics.OverlapSphere(transform.position, range, layersAfected);


            if (nearestGameObject)
            {
                transform.LookAt(new Vector3(nearestGameObject.transform.position.x, transform.position.y, nearestGameObject.transform.position.z));

                if (Vector3.Distance(nearestGameObject.transform.position, transform.position) > range || !nearestGameObject.activeSelf)
                {
                    nearestGameObject = null;
                }
            }

            if (!nearestGameObject)
            {
                GetNewGameObjectOnRange();
            }

           
        }

        void GetNewGameObjectOnRange()
        {
            if (OnRange.Length > 0)
            {

                for (int i = 0; i < OnRange.Length; i++)
                {
                    if (i == 0)
                    {
                        minDist = Vector3.Distance(transform.position, OnRange[i].transform.position);
                        getNearestObject = i;
                    }
                    else if (Vector3.Distance(transform.position, OnRange[i].transform.position) < minDist)
                    {
                        minDist = Vector3.Distance(transform.position, OnRange[i].transform.position);
                        getNearestObject = i;
                    }
                }

                nearestGameObject = OnRange[getNearestObject].gameObject;

                transform.LookAt(new Vector3(nearestGameObject.transform.position.x, transform.position.y, nearestGameObject.transform.position.z));

                //controller.target = nearestGameObject;

                //if (!stopTrigger) 
                //{
                //    controller.TriggerAttack();
                //    stopTrigger = true;
                //}


            }
            else
            {
                //if (stopTrigger)
                //{
                //    controller.TriggerAttack();
                //    stopTrigger = false;
                //}
                   
            }
        }
    }

}

