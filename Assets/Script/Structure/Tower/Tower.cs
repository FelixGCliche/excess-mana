
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Tower : Structure
{
    //================================ Variables
    public float radius;

    public Transform target;

    public float  fire_rate = 1f;
    private float fire_countdown = 0.0f;

    public GameObject projectile_spawn_point;

    public TowerAttacks tower_attacks;

    //================================ Methods

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.1f);
    }

    void Update()
    {
        if (target == null)
            return;
     
        Debug.DrawLine(projectile_spawn_point.transform.position, target.position, Color.green, 1.0f);

        if(fire_countdown <= 0.0f)
        {
            Shoot();
            fire_countdown = 1f / fire_rate;
        }

        fire_countdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        switch(current_element)
        {
            case Elements.WIND:
                tower_attacks.WindAttack(projectile_spawn_point.transform, target);
                break;

            case Elements.FIRE:
                tower_attacks.FireAttack(projectile_spawn_point.transform, target);
                break;

            case Elements.WATER:
                tower_attacks.WaterAttack(projectile_spawn_point.transform, target);
                break;

            case Elements.EARTH:
                tower_attacks.EarthAttack(projectile_spawn_point.transform, target);
                break;
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if(distanceToEnnemy < shortestDistance)
            {
                shortestDistance = distanceToEnnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= radius)
        {
            target = nearestEnemy.transform;
            Vector3 targetDir = target.position - this.spawnPosition;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    //================================ Accessors
    public void SetAttackRadius(float radius)
    {
        this.radius = radius;
    }

    public void SetFireRate(float rate)
    {
        this.fire_rate = rate;
    }

    public void SetFireCountdown(float countdown)
    {
        this.fire_countdown = countdown;
    }

}
