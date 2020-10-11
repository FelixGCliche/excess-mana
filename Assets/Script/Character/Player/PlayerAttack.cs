using System;
using DG.Tweening;
using Harmony;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace Game
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject fireProjectilePrefab;
        [SerializeField] private GameObject earthProjectilePrefab;
        [SerializeField] private GameObject windProjectilePrefab;
        [SerializeField] private GameObject waterProjectilePrefab;
        [SerializeField] private float dashLength = 5f;
        
        private InputAction aimInput;

        private void Awake()
        {
            aimInput = Finder.Inputs.Actions.Game.Aim;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        { 
            Gizmos.DrawLine(transform.parent.position, GetPointerPositionInWorld());
        }
#endif

        public void FireAttack(Vector3 position)
        {
            Instantiate(fireProjectilePrefab, position, GetProjectileRotation(position));
        }

        public void EarthAttack(Vector3 position)
        {
            Instantiate(earthProjectilePrefab, position, GetProjectileRotation(position));
        }

        public void WindAttack(Transform parentTransform)
        {
            var position = parentTransform.position;
            Instantiate(windProjectilePrefab, position, GetProjectileRotation(position));
            
            Vector3 direction = GetPointerPositionInWorld() - position;
            float hypothenuse = Mathf.Abs(Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y));
            float distance = GetFirstWallHitDistance(position, direction, dashLength);
            float lenghtMultiplier = distance / hypothenuse;
            float x = direction.x * lenghtMultiplier;
            float y = direction.y * lenghtMultiplier;
            
            parentTransform.DOMove(position + new Vector3(x, y, 0), 0.2f * lenghtMultiplier);
        }

        public void WaterAttack(Vector3 position)
        {
            Instantiate(waterProjectilePrefab, position, GetProjectileRotation(position));
        }

        private Quaternion GetProjectileRotation(Vector3 position)
        {
            Vector3 direction = GetPointerPositionInWorld() - position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            return Quaternion.Euler(0, 0, angle);
        }

        private Vector3 GetPointerPositionInWorld()
        {
            Plane plane = new Plane(Vector3.back, transform.parent.position);
            var ray = Camera.main.ScreenPointToRay(aimInput.ReadValue<Vector2>());
        
            plane.Raycast(ray, out float enter);
            return ray.GetPoint(enter);
        }
        
        private float GetFirstWallHitDistance (Vector3 spawnPointPosition, Vector3 direction, float maxLenght)
        {
            var detectedObjects = Physics2D.RaycastAll(spawnPointPosition, direction, maxLenght);

            for (int i = 0; i < detectedObjects.Length; i++)
            {
                if (detectedObjects[i].collider.GetComponent<TilemapCollider2D>() != null)
                {
                    return detectedObjects[i].distance;
                }
            }
            return maxLenght;
        }
    }
}