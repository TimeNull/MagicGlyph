using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MagicGlyphs.Characters;
using MagicGlyphs.ScriptableObjects;
    
namespace MagicGlyphs
{
    public class Life : MonoBehaviour
    {
        [SerializeField] private Enemy enemySO; // Desired object life configuration (Character, Enemy...)
        [SerializeField] private Character characterSO;
        
        private float maxLife;
        public float MaxLife { get => maxLife; }

        private float actualLife;
        public float ActualLife { get => actualLife; }

        public bool isInvulnerable { get; set; } //this is for a shield when receive damage or when collect some shield

        [Header("Unity Events")]
        
        [SerializeField] private UnityEvent OnBecomeVulnerable;
        [SerializeField] private UnityEvent OnReceiveDamage;
        [SerializeField] private UnityEvent OnResetDamage;
        [SerializeField] private UnityEvent OnDeath;

        [SerializeField] List<MonoBehaviour> OnDamageReceivers; //this could be done by Unity Event but Enum isn't showed in Unity Event inspector, so this is necessary


        System.Action schedule;


        private void Start()
        {
            if (enemySO)
            {
                maxLife = enemySO.maxLife;
            }

            if (characterSO)
            {
                maxLife = characterSO.maxLife;
            }

            actualLife = maxLife;

        }

        private void Update()
        {
            
        }

        
        public void ApplyDamage(float damage)
        {
            if (actualLife <= 0)
            {
                //ignore damage if already dead
                return;
            }
           
            actualLife -= damage;
            Debug.Log("objeto:" + transform.name + actualLife);

            if (actualLife <= 0)
                schedule += OnDeath.Invoke; //This avoid race condition when objects kill each other.
            else
                OnReceiveDamage.Invoke();

            

            var messageType = actualLife <= 0 ? Message.DEAD : Message.DAMAGED;

            for (int i = 0; i < OnDamageReceivers.Count; ++i)
            {
                var receiver = OnDamageReceivers[i] as Controller;
                receiver.ReceiveMessage(messageType);
            }

        }

        void LateUpdate()
        {
            if (schedule != null)
            {
                schedule();
                schedule = null;
            }
        }


        public void ResetDamage()
        {

        }

    }
}


