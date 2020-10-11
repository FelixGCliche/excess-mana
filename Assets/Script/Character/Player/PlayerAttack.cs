using System;
using DG.Tweening;
using Harmony;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;

namespace Game
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject fireProjectilePrefab;
        [SerializeField] private GameObject earthProjectilePrefab;
        [SerializeField] private GameObject windProjectilePrefab;
        [SerializeField] private GameObject waterProjectilePrefab;
        [SerializeField] private float timeBetweenAttack = 3f;
        [SerializeField] private float dashLength = 5f;
        
        private InputAction aimInput;
        private float timeBeforeNextAttack;

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

        private void Start()
        {
            timeBeforeNextAttack = 0f;
        }

        private void Update()
        {
            timeBeforeNextAttack -= Time.deltaTime;
        }

        public bool IsReadyToAttack()
        {
            return timeBeforeNextAttack <= 0;
        }

        public void FireAttack(Vector3 position)
        {
            Instantiate(fireProjectilePrefab, position, GetProjectileRotation(position));
            timeBeforeNextAttack = timeBetweenAttack;
        }

        public void EarthAttack(Vector3 position)
        {
            Instantiate(earthProjectilePrefab, position, GetProjectileRotation(position));
            timeBeforeNextAttack = timeBetweenAttack;
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
            float duration = 0.2f;
            if (lenghtMultiplier < 1)
                duration *= lenghtMultiplier;
            parentTransform.DOMove(position + new Vector3(x, y, 0), duration);
            timeBeforeNextAttack = timeBetweenAttack;
        }

        public void WaterAttack(Vector3 position)
        {
            Instantiate(waterProjectilePrefab, position, GetProjectileRotation(position));
            timeBeforeNextAttack = timeBetweenAttack;
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