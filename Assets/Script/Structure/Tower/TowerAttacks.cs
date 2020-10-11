using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttacks : MonoBehaviour
{
    [SerializeField] private GameObject tower_wind_projectile;
    [SerializeField] private GameObject tower_fire_projectile;
    [SerializeField] private GameObject tower_water_projectile;
    [SerializeField] private GameObject tower_earth_projectile;

    public void WindAttack(Transform projectile_spawn_point, Transform target)
    {
        Instantiate(tower_wind_projectile, projectile_spawn_point.transform.position, GetProjectileRotation(target, projectile_spawn_point));
    }

    public void FireAttack(Transform projectile_spawn_point, Transform target)
    {
        Instantiate(tower_fire_projectile, projectile_spawn_point.transform.position, GetProjectileRotation(target, projectile_spawn_point));
    }

    public void WaterAttack(Transform projectile_spawn_point, Transform target)
    {
        Instantiate(tower_water_projectile, projectile_spawn_point.transform.position, GetProjectileRotation(target, projectile_spawn_point));
    }

    public void EarthAttack(Transform projectile_spawn_point, Transform target)
    {
        Instantiate(tower_earth_projectile, projectile_spawn_point.transform.position, GetProjectileRotation(target, projectile_spawn_point));
    }


    private Quaternion GetProjectileRotation(Transform target, Transform projectile_spawn_point)
    {
        Vector3 direction = target.position - projectile_spawn_point.transform.position;
        float angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.forward);
        return Quaternion.Euler(0, 0, angle);
    }


}
