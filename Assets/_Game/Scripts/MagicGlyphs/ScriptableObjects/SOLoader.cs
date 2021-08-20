using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.Weapons;
using MagicGlyphs.Player;

namespace MagicGlyphs.ScriptableObjects
{
    public class SOLoader : MonoBehaviour
    {
        [SerializeField] private GeneralAttributes generalAttributes;
        [SerializeField] private Character holder;
        private Life life;
        private Weapon weapon;
        private Controller controller;

        private void Awake()
        {
            life = GetComponent<Life>();
            controller = GetComponent<Controller>();
            weapon = GetComponentInChildren<Weapon>();


        }

        private void Start()
        {
            if (holder)
            { 
                
            }
        }


    }
}

