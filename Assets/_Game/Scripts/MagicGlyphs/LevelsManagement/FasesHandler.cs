using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "ScenesHandler", menuName = "ScriptableObjects/ScenesHandler")]
public class FasesHandler : ScriptableObject
{
    public Levels[] Worlds;
}

[Serializable]
public class Levels
{
    public UnityEngine.Object [] levels;
    public UnityEngine.Object [] Boss;
}
