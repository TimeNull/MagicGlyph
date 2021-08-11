using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MagicGlyphs
{
    public class ExplosivePathScript : MonoBehaviour
    {
        public Transform player;
        [SerializeField] GameObject[] explosives;
        Queue<GameObject> queueExplosives = new Queue<GameObject>();
        GameObject queueHolderGO;
        [SerializeField] float distanceToSpawn;
        float currentDistance;
        Vector3 pastPosition;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            for (int i = 0; i < explosives.Length; i++)
            {
                queueExplosives.Enqueue(explosives[i]);
                explosives[i].SetActive(false);
                explosives[i].transform.parent = null;
            }

            currentDistance = 0;
            pastPosition = player.transform.position;
        }

        private void Update()
        {
            currentDistance += Vector3.Distance(player.transform.position, pastPosition);
            pastPosition = player.transform.position;

            if (currentDistance >= distanceToSpawn)
            {
                currentDistance = 0;
                ActiveNextExplosive();
            }
        }

        void ActiveNextExplosive()
        {
            queueHolderGO = queueExplosives.Dequeue();
            queueHolderGO.transform.position = transform.position;
            if (!queueHolderGO.activeSelf)
            {
                queueHolderGO.SetActive(true);
            }
            queueExplosives.Enqueue(queueHolderGO);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            foreach (GameObject explosive in explosives)
            {
                explosive.SetActive(false);
            }
        }
    }
}
