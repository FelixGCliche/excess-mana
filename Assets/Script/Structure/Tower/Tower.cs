
using DG.Tweening;
using Harmony;
using Script.Character.Player;
using Script.Projectile;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class Tower : Structure
{
    [SerializeField] private float radius;

    [SerializeField] private Transform target;

    [SerializeField] private float  fireRate = 1f;
    private float fire_countdown = 0.0f;

    [SerializeField] private GameObject projectileSpawnPoint;

    [SerializeField] private Projectile projectile;

    public TextMesh text;

    private Player player;

    private int currentLevel = 0;

    void Start()
    {

        text.text = currentLevel.ToString();

        player = Finder.Player;
    }

    void Update()
    {
        if (target == null || Vector2.Distance(transform.position, target.transform.position) > radius)
        {
            UpdateTarget();
            return;
        }

        if(fire_countdown <= 0.0f)
        {
            Shoot();
            fire_countdown = 1f / fireRate;
        }

        fire_countdown -= Time.deltaTime;
    }

    public void SetCurrentLevel(int level)
    {
        this.currentLevel = level;
        text.text = currentLevel.ToString();
    }

    private void Shoot()
    {
        Projectile newProjectile = Instantiate(projectile, projectileSpawnPoint.transform.position, GetProjectileRotation(target, projectileSpawnPoint.transform));
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
        }
        else
        {
            target = null;
        }
    }

    private Quaternion GetProjectileRotation(Transform target, Transform projectileSpawnPoint)
    {
        Vector3 dir = target.position - projectileSpawnPoint.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }
}
