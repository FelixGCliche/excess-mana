using System;
using UnityEngine;

namespace Script.Projectile
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Elements projectileElement = Elements.NONE;
        [SerializeField][Range(1, 100)] private float projectileDamage = 10f;
        [SerializeField][Range(1, 100)] protected float projectileSpeed = 10f;

        private void Update()
        {
            transform.Translate(Time.deltaTime * projectileSpeed * Vector3.right);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(projectileDamage, projectileElement);
                Destroy(gameObject);
            }
        }
    }
}