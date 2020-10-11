using System;
using UnityEngine;

namespace Script.Projectile
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Elements projectileElement = Elements.NONE;
        [SerializeField][Range(1, 100)] private float projectileDamage = 10f;
        [SerializeField][Range(1, 100)] private float projectileSpeed = 10f;
        [SerializeField][Range(0.1f, 100)] private float projectileDuration = 2f;
        [SerializeField] private bool isPiercing = false;
        [SerializeField] private bool isImmobile = false;

        private Vector3 initialLocation;

        private void Start()
        {
            initialLocation = transform.position;
        }

        private void Update()
        {
            projectileDuration -= Time.deltaTime;
            if (projectileDuration <= 0)
                Destroy(gameObject);
            if (!isImmobile)
                transform.Translate(Time.deltaTime * projectileSpeed * Vector3.right);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(projectileDamage, projectileElement);
                if (!isPiercing)
                    Destroy(gameObject);
            }
        }
    }
}