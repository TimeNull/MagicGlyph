using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    GameObject cmPosition;

    Vector3 targetVector;

    private void Start()
    {
        cmPosition = Camera.main.gameObject;
    }

    void Update()
    {
        transform.LookAt(cmPosition.transform.rotation * Vector3.back + transform.position, cmPosition.transform.rotation * Vector3.down);
    }


}
