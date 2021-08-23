using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalMissileParent : MonoBehaviour
{
    [SerializeField] GameObject prefabMissile, dontDestroyObj;
    GameObject missileObj;

    private void OnEnable()
    {
        if (dontDestroyObj == null)
            dontDestroyObj = GameObject.Find("DontDestroyObjects");

        missileObj = Instantiate(prefabMissile, transform.position, Quaternion.identity);
        missileObj.GetComponent<MagicGlyphs.MagicalMissiles>().root = gameObject.transform;
        missileObj.GetComponent<MeshRenderer>().enabled = false;
        missileObj.transform.SetParent(dontDestroyObj.transform);
    }
}
