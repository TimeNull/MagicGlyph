using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MagicGlyphs.ScriptableObjects;

    
namespace MagicGlyphs
{
    public class Life : MonoBehaviour
    {
        [SerializeField] private GeneralAttributes life; // Desired object life configuration (Character, Enemy...)

        private float maxLife;
        public float MaxLife { get => maxLife; }

        private float actualLife;
        public float ActualLife { get => actualLife; }

        public bool isInvulnerable { get; set; } //this is for a shield when receive damage or when collect some shield

        [Header("Unity Events")]
        
        [SerializeField] private UnityEvent OnBecomeVulnerable;
        [SerializeField] private UnityEvent OnReceiveDamage;
        [SerializeField] private UnityEvent OnReset;
        [SerializeField] private UnityEvent OnDeath;

        [SerializeField] List<MonoBehaviour> OnDamageReceivers; //this could be done by Unity Event but Enum isn't showed in Unity Event inspector, so this is necessary


        System.Action schedule;

        

        private void Start()
        {
            life.Skill();
            maxLife = life.maxLife;

            actualLife = maxLife;

            OnReset?.Invoke();

            if (OnReset.GetPersistentEventCount() < 2)
                Debug.LogWarning("You need to add LifeBar Script 'SetMaxLifeUI' and 'SetActualLifeUI' methods in the OnReset Unity Event for properly Life UI feedback!");


        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ApplyDamage(20);
            }
        }

        
        public void ApplyDamage(float damage)
        {
            if (actualLife <= 0)
            {
                //ignore damage if already dead
                return;
            }
           
            actualLife -= damage;
           
            //Debug.Log("objeto:" + transform.name + actualLife);

            if (actualLife <= 0)
                schedule += OnDeath.Invoke; //This avoid race condition when objects kill each other.
            else
                OnReceiveDamage?.Invoke();

            

            var messageType = actualLife <= 0 ? Message.DEAD : Message.DAMAGED;

            for (int i = 0; i < OnDamageReceivers.Count; ++i)
            {
                var receiver = OnDamageReceivers[i] as Controller;
                receiver.ReceiveMessage(messageType);
            }

        }

        public void IncreaseActualLife(float value)
        {
            actualLife += value;

            if (actualLife > maxLife)
                actualLife = maxLife;
            OnReset?.Invoke();
        }

        public void SetMaxLife(float value)
        {
            maxLife = value;
            OnReset?.Invoke();
        }

        public void UpdateLife ()
        {
            maxLife = life.maxLife;
        }

        public void DeathCoins()
        {
            CoinsManager.addCoins(Random.Range(1, 7));
        }

        public void RegenPlayerWtf(float life)
        {
            if (MagicGlyphs.RegenPerKillMobs.canRegen)
            {
                Life playerLife = GameObject.FindGameObjectWithTag("Player").GetComponent<Life>();
                playerLife.IncreaseActualLife(playerLife.maxLife * (life/100));
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

        private void OnDisable()
        {
            actualLife = maxLife;
            OnReset?.Invoke();
        }

    }
}


