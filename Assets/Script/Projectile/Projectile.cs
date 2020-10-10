using System;
using UnityEngine;

namespace Script.Projectile
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected Elements projectileElement = Elements.NONE;
        [SerializeField][Range(1, 100)] protected float projectileDamage = 10f;
        [SerializeField][Range(1, 100)] protected float projectileSpeed = 10f;

        private void Update()
        {
            transform.Translate(Time.deltaTime * projectileSpeed * Vector3.right);
        }
        
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            var otherParentTransform = other.transform.parent;
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(projectileDamage, projectileElement);
                Destroy(gameObject);
            }
        }
    }
}