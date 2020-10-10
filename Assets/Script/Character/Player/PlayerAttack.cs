using UnityEngine;

namespace Script.Character.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject fireProjectilePrefab;
        [SerializeField] private GameObject earthProjectilePrefab;
        [SerializeField] private GameObject windProjectilePrefab;
        [SerializeField] private GameObject waterProjectilePrefab;

        public void FireAttack(Vector3 position)
        {
            Instantiate(fireProjectilePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), GetProjectileRotation(position));
        }

        public void EarthAttack(Vector3 position)
        {
            Instantiate(earthProjectilePrefab, position, GetProjectileRotation(position));
        }

        public void WindAttack(Vector3 position)
        {
            Instantiate(windProjectilePrefab, position, GetProjectileRotation(position));
        }

        public void WaterAttack(Vector3 position)
        {
            Instantiate(waterProjectilePrefab, position, GetProjectileRotation(position));
        }

        private Quaternion GetProjectileRotation(Vector3 position)
        {
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(position);
            Vector3 direction = Input.mousePosition - spawnPosition;
            float angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.forward);
            return Quaternion.Euler(0, 0, angle);
        }
    }
}