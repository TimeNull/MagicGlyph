using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.Player;

namespace MagicGlyphs.Characters
{
    [RequireComponent(typeof(PlayerController))]
    public class AnimalGlyph : MonoBehaviour //responsable by things that all animalsglyphs must do
    {
        protected PlayerController playerController; //to get ScriptableObject

        protected virtual void Start()
        {
            playerController = GetComponent<PlayerController>();
        }
    }
}
