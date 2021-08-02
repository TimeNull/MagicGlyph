using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    //Simplified object pooler just for gameobjects, that I adapted to
    public class HarmlessPooler
    {
        public List<GameObject> instances = new List<GameObject>();

        //comparative table to list object instance IDs with simple stack IDs
        private Dictionary<int, int> id = new Dictionary<int, int>();

        protected Queue<int> queue = new Queue<int>();


        public void AddObject(int count, GameObject prefab)
        {
            for (int i = 0; i < count; i++)
            {
                int a = instances.Count;

                instances.Add(Object.Instantiate(prefab));

                id.Add(instances[a].GetInstanceID(), a);

                instances[a].SetActive(false);
                instances[a].GetComponent<Enemies.EnemyController>().whatPoolIBelong = this;

                queue.Enqueue(a);
            }
            

        }

        public GameObject GetObject()
        {
            int id = queue.Dequeue();
            
            instances[id].SetActive(true);

            return instances[id];
        }

        //here the caller does not need to worry about pushing in the right order, through the dictionary we get the right id and push it to the stack 
        public void FreeObject(GameObject obj)
        {
            int id;

            if (this.id.TryGetValue(obj.GetInstanceID(), out id))
            {
                queue.Enqueue(id);
                instances[id].SetActive(false);
            }
            else
            {
                Debug.LogError("It was not possible to free the object. Reason: object not belong to this pool");
            }

        }

        //debugger
        public void CheckTable()
        {
            for (int i = 0; i < id.Count; i++)
            {
                int id;
                this.id.TryGetValue(instances[i].GetInstanceID(), out id);

                Debug.Log($"Instance ID: {instances[i].GetInstanceID()} , ID: {id} ");
            }

        }

        public void CheckQueue()
        {
            Debug.Log($"Tamanho da fila: {queue.Count} ");

        }
    }
}
