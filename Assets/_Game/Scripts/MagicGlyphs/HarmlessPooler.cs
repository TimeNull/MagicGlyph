using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    //Simplified object pooler just for gameobjects, that I adapted to
    public class HarmlessPooler : MonoBehaviour
    {
        public List<GameObject> instances;

        //comparative table to list object instance IDs with simple stack IDs
        private Dictionary<int, int> id;

        protected Stack<int> stack;


        public void AddObject(int count, GameObject prefab)
        {
            for (int i = 0; i < count; i++)
            {
                instances.Add(Instantiate(prefab));

                id.Add(instances[i].GetInstanceID(), i);

                instances[i].SetActive(false);
                stack.Push(i);
            }

        }

        public GameObject GetObject()
        {
            int id = stack.Pop();
            instances[id].SetActive(true);

            return instances[id];
        }

        //here the caller does not need to worry about pushing in the right order, through the dictionary we get the right id and push it to the stack 
        public void FreeObject(GameObject obj)
        {
            int id;

            if (this.id.TryGetValue(obj.GetInstanceID(), out id))
            {
                stack.Push(id);
                instances[id].SetActive(false);
            }
            else
            {
                Debug.LogError("It was not possible to free the object. Reason: object not find");
            }

        }

        //debugger
        public void CheckTable()
        {
            for (int i = 0; i < id.Count - 1; i++)
            {
                int id;
                this.id.TryGetValue(instances[i].GetInstanceID(), out id);

                Debug.Log($"Instance ID: {instances[i].GetInstanceID()} , ID: {id} ");
            }

        }

    }
}
