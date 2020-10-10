using System;
using Harmony;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Character.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject fireProjectilePrefab;
        [SerializeField] private GameObject earthProjectilePrefab;
        [SerializeField] private GameObject windProjectilePrefab;
        [SerializeField] private GameObject waterProjectilePrefab;
        
        private InputAction aimInput;

        private void Awake()
        {
            aimInput = Finder.Inputs.Actions.Game.Aim;
        }

        private void OnDrawGizmos()
        { 
            Gizmos.DrawLine(transform.parent.position, GetPointerPositionInWorld());
        }

        public void FireAttack(Vector3 position)
        {
            Instantiate(fireProjectilePrefab, position, GetProjectileRotation(position));
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
            Vector3 mousePosition = aimInput.ReadValue<Vector2>();

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
    }
}