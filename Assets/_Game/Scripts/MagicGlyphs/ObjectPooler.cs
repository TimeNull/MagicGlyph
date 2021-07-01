using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    
    //all credits to Unity 3D GameKit
    public class ObjectPooler<T> where T : MonoBehaviour, Pool<T>
    {
        //Generalized monobehaviour pooling

        private List<T> instances; //List of any monobehaviour inherited objects " T " 

        private Stack<int> stack; //Stack of objects Ids (instead of pushing objects into a stack, just push the Id, since we just want to deactivate and activate objects)

        //initialize a list of T objects, that can have his length increased later on ,if necessary
        public void AddObject(int count, T prefab)
        {
            if (instances == null)
                instances = new List<T>();

            for (int i = 0; i < count; i++)
            {
                //check if this works
                instances.Add(Object.Instantiate(prefab));
                instances[i].gameObject.SetActive(false);
                instances[i].poolID = i;
                instances[i].pool = this;

                stack.Push(i);
            }
        }

        //Get the object from pool
        public T GetObject()
        {
            //here we get just the id of the object we want to activate
            int x = stack.Pop();
            instances[x].gameObject.SetActive(true);

            return instances[x];
        }

        public T GetObject(Vector3 startPos, Vector3 direction, float speed)
        {
            //here we get just the id of the object we want to activate
            int x = stack.Pop();
            instances[x].gameObject.SetActive(true);

            //mais fitas aqui

            return instances[x];
        }

        //Return object to pool
        public void FreeObject(T obj)
        {
            //here we push the id of the object, to pop it properly when we need it
            stack.Push(obj.poolID);
            instances[obj.poolID].gameObject.SetActive(false);
        }

    }

    //interface that gives unique IDs to pooling instances, so it's possible to stack just int values
    public interface Pool<T> where T : MonoBehaviour, Pool<T>
    {
        public int poolID { get; set; }

        //okay this is the only unknown property that i didn't understand
        ObjectPooler<T> pool { get; set; }
    }
}
