using Game;
using Script.Util;
using UnityEngine;

namespace Script.Character
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] [Range(0, 1000)] protected float baseHealth = 100f;
        [SerializeField] protected Elements element = Elements.NONE;
        [SerializeField] protected ProgressBar healthBar;

        protected float health;
        private TriggerSensor2D triggerSensor;
        private ColliderSensor2D colliderSensor;
        private Mover mover;

        protected TriggerSensor2D TriggerSensor => triggerSensor;
        protected ColliderSensor2D ColliderSensor => colliderSensor;
        protected Mover Mover => mover;

        protected void Awake()
        {
            triggerSensor = GetComponentInChildren<TriggerSensor2D>();
            colliderSensor = GetComponentInChildren<ColliderSensor2D>();
            mover = GetComponentInChildren<Mover>();
        }

        public void TakeDamage(float damageAmount, Elements damageElement)
        {
            health -= DamageCalculator.CalculateDamage(damageAmount, damageElement, element);
            if (health <= 0)
                Kill();
            else
                healthBar.UpdateProgressBar(health/baseHealth);
        }

        protected abstract void Kill();
    }
}
