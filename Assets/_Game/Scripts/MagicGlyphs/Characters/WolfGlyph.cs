using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs.Characters
{
    //a little explanation for myself (in english, so I can get better at this language)
    //this script is just an organization, I could do everything on player controller script, but that ends the components independency
    //besides that, you can use diferent scripts to just change one part of the same object
    //for example, all characters will move and attack, so why make 4 different scripts, with the same lines of code for the general abilities (move, attack etc)
    //If i can make just one general script and other that contains just the specific behaviors

    
    public class WolfGlyph : AnimalGlyph //responsable by things that only wolf must do
    {
        //playerController objects comes by base class

        //specific skills


    }
}

