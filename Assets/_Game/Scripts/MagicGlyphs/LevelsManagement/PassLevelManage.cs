using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GMDelegate();
namespace MagicGlyphs
{
    public class PassLevelManage : MonoBehaviour
    {
        int enemiesQtde;
        public static GMDelegate defeatLevel;
        bool alreadyChecked;

        void Update()
        {
            enemiesQtde = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (enemiesQtde <= 0)
            {
                if (!alreadyChecked)
                    defeatLevel();

                alreadyChecked = true;
            }
            else
            {
                alreadyChecked = false;
            }
        }
    }
}
